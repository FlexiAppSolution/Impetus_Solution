using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThalesSim.Core;
using ThalesSim.Core.Message;
using System.IO.IsolatedStorage;
using System.IO;
using PANE.Framework.Utility;

namespace PANE.Framework.HSM
{
    public class ThalesInterface
    {
        

        ThalesSim.Core.TCP.WorkerClient thales;
        string thalesData = string.Empty;
        public static string Key_TPK_LMK;
        public static string Key_ZPK_LMK;
        private int _hsmHeaderLength;
        private string _hsmHost;
        private int _hsmPort;
        private static string _hsmKeyFilePath;

        public ThalesInterface()
            : this(ConfigurationHelper.ConfigItem<ThalesInterface, string>("HsmHost"), 
            ConfigurationHelper.ConfigItem<ThalesInterface, int>("HsmPort"), 
            ConfigurationHelper.ConfigItem<ThalesInterface, int>("HsmHeaderLength"))
        {
            _hsmKeyFilePath = ConfigurationHelper.ConfigItem<ThalesInterface, string>("HsmKeyFilePath");
        }

        public ThalesInterface(string hsmHost, int hsmPort, int hsmHeaderLength)
        {
            _hsmPort = hsmPort;
            _hsmHost = hsmHost;
            _hsmHeaderLength = hsmHeaderLength;
            _hsmKeyFilePath = ConfigurationHelper.ConfigItem<ThalesInterface, string>("HsmKeyFilePath");
            try
            {
                if (thales == null)
                {
                    thales = new ThalesSim.Core.TCP.WorkerClient(new System.Net.Sockets.TcpClient(hsmHost, hsmPort));
                    thales.MessageArrived += new ThalesSim.Core.TCP.WorkerClient.MessageArrivedEventHandler(thales_MessageArrived);
                    thales.InitOps();
                }
            }
            catch
            {
                throw;
            }
            
        }

        private ThalesSim.Core.Message. MessageFieldParser GenerateLongKeyParser(string keyName, int msgLength)
        {
            MessageFieldDeterminerCollection MFDC_Key = new MessageFieldDeterminerCollection();
            MFDC_Key.AddFieldDeterminer(new MessageFieldDeterminer(keyName + "_DOUBLE_X917",
                                                                   KeySchemeTable.GetKeySchemeValue(KeySchemeTable.KeyScheme.DoubleLengthKeyAnsi),
                                                                   32));
            MFDC_Key.AddFieldDeterminer(new MessageFieldDeterminer(keyName + "_DOUBLE_VARIANT",
                                                                   KeySchemeTable.GetKeySchemeValue(KeySchemeTable.KeyScheme.DoubleLengthKeyVariant),
                                                                   32));
            MFDC_Key.AddFieldDeterminer(new MessageFieldDeterminer(keyName + "_PLAIN_DOUBLE", msgLength, 32));
            MFDC_Key.AddFieldDeterminer(new MessageFieldDeterminer(keyName + "_PLAIN_SINGLE", "", 16));
            return new MessageFieldParser(keyName, MFDC_Key);
        }
        void thales_MessageArrived(ThalesSim.Core.TCP.WorkerClient sender, ref byte[] b, int len)
        {
            string s = string.Empty;

            for (int i = 0; i < len; i++)
            {
                s = s + Convert.ToChar(b[i]);
            }

            thalesData = s;
        }

        private string SendFunctionCommand(string s)
        {
            thalesData = "";
            thales.send(s);

            while (thalesData == string.Empty && thales.IsConnected)
            {
                System.Threading.Thread.Sleep(1);
            }

            if (!thales.IsConnected)
            {
                return string.Empty;
            }
            else
            {
                return thalesData;
            }
        }

        public string GenerateZPK(string zmk, out string checkDigits)
        {
            ThalesSim.Core.Message.Message msg = ProcessRequest("IA", string.Format("{0};XU1",zmk));

            const string ZPK_ZMK = "ZPK_ZMK";
            const string ZPK_LMK = "ZPK_LMK";

            ThalesSim.Core.Message.MessageFieldParserCollection keMfpc = new ThalesSim.Core.Message.MessageFieldParserCollection();

            keMfpc.AddMessageFieldParser(GenerateLongKeyParser(ZPK_ZMK, 30));
            keMfpc.AddMessageFieldParser(GenerateLongKeyParser(ZPK_LMK, 30));
            keMfpc.AddMessageFieldParser(new ThalesSim.Core.Message.MessageFieldParser("check", 6));

            keMfpc.ParseMessage(msg);

            string zpkzmk = keMfpc.GetMessageFieldByName(ZPK_ZMK).FieldValue;
            SaveKeyToStorage(KeyTypes.Zpk_Lmk, keMfpc.GetMessageFieldByName(ZPK_LMK).FieldValue);
            checkDigits = keMfpc.GetMessageFieldByName("check").FieldValue;

            return zpkzmk;
        }

        public string GenerateTPK(string tmk)
        {
            ThalesSim.Core.Message.Message msg = ProcessRequest("HC", string.Format("U{0};XU1", tmk));

            const string TPK_TMK = "TPK_TMK";
            const string TPK_LMK = "TPK_LMK";

            ThalesSim.Core.Message.MessageFieldParserCollection keMfpc = new ThalesSim.Core.Message.MessageFieldParserCollection();

            keMfpc.AddMessageFieldParser(GenerateLongKeyParser(TPK_TMK, 30));
            keMfpc.AddMessageFieldParser(GenerateLongKeyParser(TPK_LMK, 30));

            keMfpc.ParseMessage(msg);

            string tpktmk = keMfpc.GetMessageFieldByName(TPK_TMK).FieldValue;
            SaveKeyToStorage(KeyTypes.Tpk_Lmk, keMfpc.GetMessageFieldByName(TPK_LMK).FieldValue);

            return tpktmk;
        }

        public string TranslatePIN_TPK_ZPK(string pinBlock, string tpk, string zpk, string accountNo)
        {
            zpk = ReadKeyFromStorage(KeyTypes.Zpk_Lmk);//"UEE66D1AC9362CEDEF3686BDAA44B0E6D";
            tpk = ReadKeyFromStorage(KeyTypes.Tpk_Lmk);
            ThalesSim.Core.Message.Message msg = ProcessRequest("CA", string.Format("U{0}U{1}12{2}0101{3}", tpk, zpk, pinBlock, accountNo));

            ThalesSim.Core.Message.MessageFieldParserCollection keMfpc = new ThalesSim.Core.Message.MessageFieldParserCollection();

            keMfpc.AddMessageFieldParser(new MessageFieldParser("pin_length",2));
            keMfpc.AddMessageFieldParser(new MessageFieldParser("pinblock", 16));
            keMfpc.AddMessageFieldParser(new MessageFieldParser("pinblock_format", 2));

            keMfpc.ParseMessage(msg);

            string newPinBlock = keMfpc.GetMessageFieldByName("pinblock").FieldValue;

            return newPinBlock;
        }

        private ThalesSim.Core.Message.Message ProcessRequest(string command, string request)
        {
            string header = "".PadLeft(_hsmHeaderLength, '0');
            string reply = SendFunctionCommand(string.Format("{0}{1}{2}", header, command, request));
            //

            ThalesSim.Core.Message.Message msg = new ThalesSim.Core.Message.Message(reply);

            ThalesSim.Core.Message.MessageFieldParserCollection mfpc = new ThalesSim.Core.Message.MessageFieldParserCollection();
            mfpc.AddMessageFieldParser(new ThalesSim.Core.Message.MessageFieldParser("header", ConfigurationHelper.ConfigItem<ThalesInterface, int>("HsmHeaderLength")));
            mfpc.AddMessageFieldParser(new ThalesSim.Core.Message.MessageFieldParser("command", 2));
            mfpc.AddMessageFieldParser(new ThalesSim.Core.Message.MessageFieldParser("responseCode", 2));
            mfpc.ParseMessage(msg);

            //string header = mfpc.GetMessageFieldByName("header").FieldValue;
            string comm = mfpc.GetMessageFieldByName("command").FieldValue;
            string responseCode = mfpc.GetMessageFieldByName("responseCode").FieldValue;

            if (responseCode != "00")
            {
//                throw new ApplicationException(string.Format("{0} h:{1} c:{2} r:{3}",ErrorCodes.GetError(responseCode).ErrorHelp , header, command, request));
                throw new ApplicationException(string.Format("{0}", ErrorCodes.GetError(responseCode).ErrorHelp));
            }

            return msg;
        }

        public void Close()
        {
            if (thales != null)
            {
                thales.TermClient();
            }
        }


        ~ThalesInterface()
        {
            if (thales != null && thales.IsConnected)
            {
                thales.TermClient();
            }
        }

        private static void SaveKeyToStorage(KeyTypes keyType, string key)
        {
            //using (IsolatedStorageFile appFile = IsolatedStorageFile.GetMachineStoreForAssembly())
            //{
            //    using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(keyType.ToString() + ".key", System.IO.FileMode.Create, FileAccess.Write, FileShare.ReadWrite, appFile))
            //    {
            //        using (StreamWriter writer = new StreamWriter(fileStream))
            //        {
            //            writer.Write(key);
            //        }
            //    }
            //}

            string theFile = string.Format("{0}{1}.key", _hsmKeyFilePath, keyType);
            if (File.Exists(theFile))
            {
                File.Delete(theFile);
            }

            using (FileStream fileStream = File.Open(theFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(key);
                }
            }
        }

        public static void SaveKeyExchange(Byte[] sessionKey, string zmkComp1, string zmkComp2)
        {
            string zmkClear = ThalesSim.Core.Utility.XORHexStringsFull(zmkComp1, zmkComp2);
            string hexValue = string.Empty; string encryptionKey = string.Empty;
            ThalesSim.Core.Utility.ByteArrayToHexString(sessionKey, ref hexValue);

            // use only the first 32 digits
            encryptionKey = ThalesSim.Core.Cryptography.TripleDES.TripleDESDecrypt(new ThalesSim.Core.Cryptography.HexKey(zmkClear), hexValue.Substring(0, 32));

            string clear_encrypted = string.Format("{0},{1}", hexValue, encryptionKey);
            SaveKeyToStorage(KeyTypes.Zpk_Lmk, clear_encrypted);
        }

        public static string ReadKeyFromStorage(KeyTypes keyType)
        {
            string key = string.Empty;
            using (FileStream fileStream = File.Open(string.Format("{0}{1}.key", _hsmKeyFilePath, keyType), FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    key = reader.ReadToEnd();
                }
            }
            //using (IsolatedStorageFile appFile = IsolatedStorageFile.GetMachineStoreForAssembly())
            //{
            //    using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(keyType.ToString() + ".key", System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite, appFile))
            //    {
            //        using (StreamReader reader = new StreamReader(fileStream))
            //        {
            //            key = reader.ReadToEnd();
            //        }
            //    }
            //}
            return key;
        }
    }

    public enum KeyTypes
    {
        Zpk_Lmk,
        Tpk_Lmk
    }
}

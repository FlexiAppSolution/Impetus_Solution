using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EvoPaj.Base.Utility
{
    public class Txt
    {


        const int SizeBuff = 1024;

        public Txt()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string ReadFile(string filename, string path)
        {
            FileInfo theSource = new FileInfo(Path.Combine(path, filename));

            StreamReader reader = theSource.OpenText();

            // create a text variable to hold each line
            string text;
            StringBuilder content = new StringBuilder();

            // walk the file and read every line
            do
            {
                text = reader.ReadLine();
                content.AppendLine(text);
                //Console.WriteLine(text);

            } while (text != null);

            reader.Close();

            return content.ToString();
        }

        public void WriteBuffer(string filename, string path, byte[] Value)
        {
            //FileInfo theSource = new FileInfo(Path.Combine(path, filename));

            Stream outputStream = File.OpenWrite(Path.Combine(path, filename));
            // add buffered streams on top of the
            // binary streams

            BufferedStream bufferedOutput = new BufferedStream(outputStream);
            byte[] buffer = Value;

            bufferedOutput.Write(buffer, 0, buffer.Length);

            bufferedOutput.Flush();
            bufferedOutput.Close();
        }

        public void WriteFile(string filename, string path, string Value)
        {
            //FileInfo theSource = new FileInfo(Path.Combine(path, filename));

            // create a text writer to the new file
            StreamWriter writer = new StreamWriter(Path.Combine(path, filename), false);

            writer.Write(Value);

            writer.Close();
        }

        public byte[] ReadBuffer(string filePath, string fileName)
        {
            //Get the source of the file and get the string value of the path 
            FileInfo source = new FileInfo(Path.Combine(filePath, fileName));
            string path = Convert.ToString(source);

            //Start reading the file from the loacation supplied to the FileStream Constructor
            //It requires the path, open cmd, and read cmd
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            //Get the length of the file from the Filestream
            int length = (int)fileStream.Length;

            //Instantiate the byte variable using the file length
            byte[] buffer = new byte[length];

            try
            {
                int count;
                int sum = 0;

                // read until Read method returns 0 (end of the stream has been reached)
                while
                    ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;               // sum is a buffer offset for next reading

            }
            catch (Exception ex)
            {

            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }


        public byte[] ReadBuffer(string path)
        {
            byte[] buffer = new byte[1024];
            //FileInfo source = new FileInfo(Path.Combine(filePath, fileName));
            //string path = Convert.ToString(source);

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;
                buffer = new byte[length];
                int count;
                int sum = 0;

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;               // sum is a buffer offset for next reading
            }
            catch (Exception ex)
            {

            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
    }
}

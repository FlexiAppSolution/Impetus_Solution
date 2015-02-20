using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Data.SqlTypes;
using PANE.Framework.AuditTrail.Enums;
using System.Runtime.Serialization;
using PANE.Framework.AuditTrail.Attributes;

namespace PANE.Framework.Utility
{
    public class BinarySerializer
    {

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static Byte[] SerializeObject(Object obj)
        {
            Byte[] serializedObject = null;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways;
            bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full;
            bf.Serialize(ms, obj);
            serializedObject = ms.ToArray();
            ms.Close();
            return serializedObject;
        }

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <returns></returns>
        public static Object DeSerializeObject(Byte[] serializedObject)
        {
            Object deSerializedObject = null;
            if (serializedObject != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(serializedObject);
                try
                {
                    deSerializedObject = new BinaryFormatter().Deserialize(ms);
                }
                catch
                {
                }
                finally
                {
                    ms.Close();
                }
            }
            return deSerializedObject;
        }


        public static List<TrailItem> Deserialize(Byte[] databefore, Byte[] dataafter, string dataType, AuditAction action)
        {
            List<TrailItem> ofItems = new List<TrailItem>();
            Object dafter = dataafter == null ? new object() : (DeSerializeObject(dataafter));
            Object dbefore = databefore == null ? new object() : (DeSerializeObject(databefore));

            Object when = dbefore;
            if (dataafter != null && databefore == null)      //create
                when = dafter;
            if (databefore != null && dataafter == null)      //delete
                when = dbefore;

            if (when == null) return ofItems;
            foreach (PropertyInfo prop in when.GetType().GetProperties())
            {
                Type ptype = prop.PropertyType;
                object ater = null;
                object bfore = null;
                //if ((ptype.IsPrimitive || ptype.Equals(typeof(String)) || ptype.Equals(typeof(DateTime)) || ptype.IsEnum) && prop.CanWrite)
               // if (prop.GetCustomAttributes(typeof(IsLoggedAttribute), false).Length > 0)
                {
                    string before = "";
                    string after = "";
                    string name = string.Empty;//((IsLoggedAttribute)prop.GetCustomAttributes(typeof(IsLoggedAttribute), false)[0]).LoggedName;
                    if (name == string.Empty) name = prop.Name;
                    if (action != AuditAction.CREATE)
                    {
                        bfore = prop.GetValue(dbefore, null);
                        if (ptype.Equals(typeof(DateTime)))
                        {
                            DateTime noDate = (DateTime)SqlDateTime.Null;
                            before = (((DateTime)bfore).Equals(noDate) ? "None" : ((DateTime)bfore).ToString("dd-MMM-yyyy hh:mm tt"));
                        }
                        else if (ptype.Equals(typeof(Decimal)))
                        {
                            before = ((Decimal)bfore).ToString("N");
                        }
                        else if (ptype.IsClass && !ptype.Equals(typeof(String)))
                        {
                            try
                            {
                                //if (ptype.Equals(typeof(User)))
                                //{
                                //    before = ptype.GetProperty("FullName").GetValue(bfore, null).ToString();
                                //}
                                //else if (ptype.Equals(typeof(State)))
                                //{
                                //    before = ptype.GetProperty("StateName").GetValue(bfore, null).ToString();
                                //}
                                //else if (ptype.Equals(typeof(Branch)))
                                //{
                                //    before = ptype.GetProperty("BranchName").GetValue(bfore, null).ToString();
                                //}
                                //else if (ptype.Equals(typeof(Card)))
                                //{
                                //    before = ptype.GetProperty("CardNumber").GetValue(bfore, null).ToString();
                                //}
                                //else
                                {
                                    before = ptype.GetProperty("Name").GetValue(bfore, null).ToString();
                                }
                            }
                            catch
                            {
                                before = bfore == null ? "" : bfore.ToString();
                            }
                        }
                        else
                        {
                            before = bfore == null ? "" : bfore.ToString();
                        }
                    }

                    if (action != AuditAction.DELETE)
                    {
                        ater = prop.GetValue(dafter, null);
                        if (ptype.Equals(typeof(DateTime)))
                        {
                            DateTime noDate = (DateTime)SqlDateTime.Null;
                            after = (((DateTime)ater).Equals(noDate) ? "None" : ((DateTime)ater).ToString("dd-MMM-yyyy hh:mm tt"));
                        }
                        else if (ptype.Equals(typeof(Decimal)))
                        {
                            after = ((Decimal)ater).ToString("N");
                        }
                        else if (ptype.IsClass && !ptype.Equals(typeof(String)))
                        {
                            try
                            {
                                //if (ptype.Equals(typeof(User)))
                                //{
                                //    after = ptype.GetProperty("FullName").GetValue(ater, null).ToString();//new CoreSystem().Retrieve(ptype.GetProperty("ID").GetValue(ater, null), ptype), null).ToString();
                                //}
                                //else if (ptype.Equals(typeof(State)))
                                //{
                                //    after = ptype.GetProperty("StateName").GetValue(ater, null).ToString();//new CoreSystem().Retrieve(ptype.GetProperty("ID").GetValue(ater, null), ptype), null).ToString();
                                //}
                                //else if (ptype.Equals(typeof(Branch)))
                                //{
                                //    after = ptype.GetProperty("BranchName").GetValue(ater, null).ToString();//new CoreSystem().Retrieve(ptype.GetProperty("ID").GetValue(ater, null), ptype), null).ToString();
                                //}
                                //else if (ptype.Equals(typeof(Card)))
                                //{
                                //    after = ptype.GetProperty("CardNumber").GetValue(ater, null).ToString();//new CoreSystem().Retrieve(ptype.GetProperty("ID").GetValue(ater, null), ptype), null).ToString();
                                //}
                                //else
                                {
                                    after = ptype.GetProperty("Name").GetValue(ater, null).ToString();//new CoreSystem().Retrieve(ptype.GetProperty("ID").GetValue(ater, null), ptype), null).ToString();
                                }
                            }
                            catch
                            {
                                after = ater == null ? "" : ater.ToString();
                            }
                        }
                        else
                        {
                            after = ater == null ? "" : ater.ToString();
                        }
                    }
                    //Don't show "Deleted" or "ID" status.
                    //if (prop.Name != "ID" && prop.Name != "Deleted")
                    ofItems.Add(new TrailItem(name, before, after));
                }
            }
            return ofItems;

        }

    
    
        public static byte[] SerializeData(object objectBefore, object objectAfter)
        {
            return PANE.Framework.Utility.BinarySerializer.SerializeObject(ConvertToList(objectBefore, objectAfter));
        }

        public static List<TrailItem> DeSerializeObject(byte[] dataBefore, byte[] dataAfter)
        {
            return ConvertToList(DeSerializeObject(dataBefore), DeSerializeObject(dataAfter));
        }

        public static List<TrailItem> ConvertToList(object objectBefore, object objectAfter)
        {
            List<TrailItem> trailItems = new List<TrailItem>();

            if (objectBefore == null && objectAfter == null)
            {
                return trailItems;
            }

            if (objectBefore != null && objectAfter != null)
            {
                if (objectBefore.GetType() != objectAfter.GetType())
                {
                  //  throw new InvalidOperationException("The two objects must be of the same type");
                }
            }
            //Use one of them to invoke reflection.
            object dataTypeCheck = objectAfter != null ? objectAfter : objectBefore;

            //Iterate thru all the properties and make sure that the property can be logged.
            foreach (PropertyInfo propertyInfo in dataTypeCheck.GetType().GetProperties())
            {
                LoggableAttribute[] loggable = (LoggableAttribute[])propertyInfo.GetCustomAttributes(typeof(LoggableAttribute), false);
                if (loggable != null && loggable.Length > 0)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    object propertyAfter = null;
                    object propertyBefore = null;
                   
                    string strBefore = "";
                    string strAfter = "";
                    string name = !String.IsNullOrEmpty(loggable[0].LoggedName) ? loggable[0].LoggedName :
                        propertyInfo.Name;

                    if (objectBefore != null)
                    {
                        propertyBefore = propertyInfo.GetValue(objectBefore, null);
                    }
                    if (objectAfter != null)
                    {
                        propertyAfter = propertyInfo.GetValue(objectAfter, null);
                    }

                    if (propertyType.Equals(typeof(DateTime)))
                    {
                        DateTime noDate = (DateTime)SqlDateTime.Null;
                        if (propertyBefore != null)
                        {
                            strBefore = (((DateTime)propertyBefore).Equals(noDate) ? "None" : ((DateTime)propertyBefore).ToString("dd-MMM-yyyy hh:mm tt"));
                        }
                        if (propertyAfter != null)
                        {
                            strAfter = (((DateTime)propertyAfter).Equals(noDate) ? "None" : ((DateTime)propertyAfter).ToString("dd-MMM-yyyy hh:mm tt"));
                        }
                    }
                    else if (propertyType.Equals(typeof(Decimal)))
                    {
                        if (propertyBefore != null)
                        {
                            strBefore = ((Decimal)propertyBefore).ToString("N");
                        }
                        if (propertyAfter != null)
                        {
                            strAfter = ((Decimal)propertyAfter).ToString("N");
                        }

                    }
                    else if (propertyType.IsClass && !propertyType.Equals(typeof(String)))
                    {
                        //Do you need only the inner class's name property or all the data???
                        if (loggable[0].NeedOnlyNameProperty == true)
                        {
                            string innerClassName = !String.IsNullOrEmpty(loggable[0].ClassNameIdentifier) ?
                                loggable[0].ClassNameIdentifier : "Name";
                            if (propertyBefore != null)
                            {
                                try
                                {
                                    strBefore = propertyType.GetProperty(innerClassName).GetValue(propertyBefore, null).ToString();
                                }
                                catch
                                {
                                    strBefore = propertyBefore == null ? "" : propertyBefore.ToString();
                                }
                            }
                            if (propertyAfter != null)
                            {
                                try
                                {
                                    strAfter = propertyType.GetProperty(innerClassName).GetValue(propertyAfter, null).ToString();
                                }
                                catch
                                {
                                    strAfter = propertyAfter == null ? "" : propertyAfter.ToString();
                                }
                            }
                        }
                        else
                        {
                            //Remember to also check the properties of the subclass for Loggable attribute.
                            List<TrailItem> innerClassProperties = GetInnerClassProperties(propertyBefore, propertyAfter);
                            if (innerClassProperties != null && innerClassProperties.Count > 0)
                            {
                                trailItems.AddRange(innerClassProperties);
                            }
                            continue;
                        }
                    }
                    else
                    {
                        strBefore = propertyBefore == null ? "" : propertyBefore.ToString();
                        strAfter = propertyAfter == null ? "" : propertyAfter.ToString();
                    }

                    trailItems.Add(new TrailItem(name, strBefore, strAfter));
                }
            }
            return trailItems;
        }

        private static List<TrailItem> GetInnerClassProperties(object objectBefore, object objectAfter)
        {
            return ConvertToList(objectBefore, objectAfter);
        }


    }
    
    [DataContract]
    [Serializable]
    public class TrailItem
    {
        public TrailItem(string name, string valueBefore, string valueAfter)
        {
            this.Name = name;
            this.ValueBefore = valueBefore;
            this.ValueAfter = valueAfter;
        }
        public TrailItem()
            : this("", "", "")
        {
        }
        public TrailItem(string name)
            : this(name, "", "")
        {
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ValueBefore { get; set; }

        [DataMember]
        public string ValueAfter { get; set; }
    }
 
}

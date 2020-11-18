using System;

namespace SYE.Covol
{
    public static class XmlHelper
    {
        public static T ReadXML<T>(string filePath)
        {
            using (System.IO.StreamReader sw = new System.IO.StreamReader(filePath))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    return (T)reader.Deserialize(sw);
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }
                }
            }
        }

        public static T ReadXML<T>(System.IO.Stream sw)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)reader.Deserialize(sw);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}

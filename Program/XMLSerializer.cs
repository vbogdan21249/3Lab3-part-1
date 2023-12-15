using System.Xml.Serialization;

namespace series
{
    public class XMLSerializer<T> : ISerializationProvider<T> where T : class
    {   
        string FileName = "";
        readonly XmlSerializer serializer = new(typeof(List<T>));
        public XMLSerializer(string fileName)
        {
            FileName = AppDomain.CurrentDomain.BaseDirectory + fileName;
            using StreamWriter w = File.AppendText(FileName);
        }
        public List<T> Load()
        {
            using (FileStream fileStream = new(FileName, FileMode.Open))
            {
                return (List<T>)serializer.Deserialize(fileStream);
            }
        }
        public void Save(List<T> listToSave)
        {
            using (FileStream fileStream = new(FileName, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fileStream, listToSave);
            }
        }
    }
}
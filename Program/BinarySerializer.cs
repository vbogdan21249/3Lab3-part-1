using System.Runtime.Serialization.Formatters.Binary;

namespace series
{
    public class BinarySerializer<T> : ISerializationProvider<T> where T : class
    {
        string FileName;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        public BinarySerializer(string fileName)
        {
            FileName = AppDomain.CurrentDomain.BaseDirectory + fileName;
            using StreamWriter w = File.AppendText(FileName);
        }
        public void Save(List<T> obj)
        {
            using (FileStream fileStream = new(FileName, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, obj);
            }
        }
        public List<T> Load()
        {
            List<T> deserialised;
            using (FileStream fileStream = new(FileName, FileMode.Open))
            {
                deserialised = (List<T>)binaryFormatter.Deserialize(fileStream);
            }
            return deserialised;
        }
    }
}
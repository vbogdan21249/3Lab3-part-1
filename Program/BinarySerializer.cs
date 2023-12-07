using System.Runtime.Serialization.Formatters.Binary;

namespace Demo
{
#pragma warning disable SYSLIB0011
    public class BinarySerializer<T> where T : class
    {
        string FileName = "";
        BinaryFormatter formatter = new BinaryFormatter();
        public BinarySerializer(string fileName)
        {
            FileName = AppDomain.CurrentDomain.BaseDirectory + fileName;
            using StreamWriter w = File.AppendText(FileName);
        }
        public void Save(List<T> obj)
        {
            using (FileStream fileStream = new(FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, obj);
            }
        }
        public List<T> Load()
        {
            List<T> deserialised;
            using (FileStream fileStream = new(FileName, FileMode.Open))
            {
                deserialised = (List<T>)formatter.Deserialize(fileStream);
            }
            return deserialised;
        }
    }
}
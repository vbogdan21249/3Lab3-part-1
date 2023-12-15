using System.Reflection;
using System.Text;

namespace series
{
    public class CustomSerializer<T> : ISerializationProvider<T> where T : class, new()
    {
        string FileName = "";
        public CustomSerializer(string fileName)
        {
            FileName = AppDomain.CurrentDomain.BaseDirectory + fileName;
            using StreamWriter w = File.AppendText(FileName);
        }
        public List<T> Load()
        {
            List<T> reading = new();
            using (var fileStream = File.OpenRead(FileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
            {
                String line;
                var entity = new StringBuilder();
                int lineIndex = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    entity.Append(line);
                    if (line.EndsWith("}"))
                    {
                        reading.Add(DeSerialize(entity.ToString(), new T()));
                    }
                    lineIndex++;
                }
            }
            return reading;
        }
        public void Save(List<T> listToSave)
        {
            using StreamWriter writetext = new(FileName);
            int entityIndex = 0;
            while (entityIndex < listToSave.Count)
            {
                writetext.WriteLine(Serialize((object)listToSave[entityIndex]));
                entityIndex++;
            }
        }
        public static string Serialize(object obj)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.Append("{");
            var myType = obj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(obj, null);
                sb.AppendLine();
                sb.Append(prop.Name + ": " + propValue);
            }
            sb.AppendLine();
            sb.Append("}");
            return sb.ToString();
        }
        public static List<string> ExtractData(
            string text, string startString = "{", string endString = "}", bool raw = false)
        {
            var matched = new List<string>();
            var exit = false;
            while (!exit)
            {
                var indexStart = text.IndexOf(startString, StringComparison.Ordinal);
                var indexEnd = text.IndexOf(endString, StringComparison.Ordinal);
                if (indexStart != -1 && indexEnd != -1)
                {
                    if (raw)
                        matched.Add("{" + text.Substring(indexStart + startString.Length,
                                        indexEnd - indexStart - startString.Length) + "}");
                    else
                        matched.Add(text.Substring(indexStart + startString.Length,
                            indexEnd - indexStart - startString.Length));
                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                {
                    exit = true;
                }
            }
            return matched;
        }
        public static List<Data> ExtractValuesFromData(string text)
        {
            var listOfData = new List<Data>();
            var allData = ExtractData(text);
            foreach (var data in allData)
            {
                var pName = data.Substring(0, data.IndexOf(": ", StringComparison.Ordinal));
                var pValue = data.Substring(data.IndexOf(": ", StringComparison.Ordinal) + 1);
                listOfData.Add(new Data { PropertyName = pName, Value = pValue });
            }
            return listOfData;
        }
        public static T DeSerialize(string serializeData, T target)
        {
            var deserializedObjects = ExtractData(serializeData);

            foreach (var obj in deserializedObjects)
            {
                var properties = ExtractValuesFromData(obj);
                foreach (var property in properties)
                {
                    var propInfo = target.GetType().GetProperty(property.PropertyName);
                    propInfo?.SetValue(target,
                        Convert.ChangeType(property.Value, propInfo.PropertyType), null);
                }
            }
            return target;
        }
        public struct Data
        {
            public string PropertyName;
            public string Value;
        }
    }
}
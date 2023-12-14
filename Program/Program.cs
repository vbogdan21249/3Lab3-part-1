using System.Text.Json;
using goods;

namespace Demo
{
    public class Program
    {
        public static void newParagraph(string text, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            List<Goods> goodsList = new()
            {
                new Goods("Brake disc", "TRW", 1237, 32),
                new Goods("Brake disc", "Remsa", 1349, 23),
                new Goods("Brake liquid", "Bosch", 171, 76),
                new Goods("Brake disc", "Remsa", 1048, 35),
                new Goods("Brake liquid", "Ferodo", 239, 45)
            };
            
            //
            // -- JSON ---
            //
            JSONSerializer<Goods> JSONSerializer = new("json.json");
            JSONSerializer.Save(goodsList);
            goodsList.Clear();
            goodsList = JSONSerializer.Load();

            //
            // -- XML ---
            //
            XMLSerializer<Goods> xmlSerializer = new("xml.xml");
            xmlSerializer.Save(goodsList);
            goodsList.Clear();
            goodsList = xmlSerializer.Load();

            //
            // -- Custom ---
            //
            CustomSerializer<Goods> customSerializer = new("custom.txt");
            customSerializer.Save(goodsList);
            goodsList.Clear();
            goodsList = customSerializer.Load();

            //
            // -- Binary --
            //
            BinarySerializer<Goods> binarySerializer = new("binary.bin");
            binarySerializer.Save(goodsList);
            goodsList.Clear();
            goodsList = binarySerializer.Load();


        }
    }
}

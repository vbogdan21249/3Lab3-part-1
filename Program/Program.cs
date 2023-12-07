using goods;

namespace Demo
{
    public class Program()
    {
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

            JSONSerializer<Goods> JSONSerializer = new("json.json");
            JSONSerializer.Save(goodsList);
            Console.WriteLine("Saved to JSON:");
            foreach (Goods goods in goodsList)
            {
                Console.WriteLine(goods);
            }

            goodsList = JSONSerializer.Load();
            Console.WriteLine("Loaded from JSON:");
            foreach (Goods goods in goodsList)
            {
                Console.WriteLine(goods);
            }

            //CustomSerializer<Goods> customSerializer = new("custom.txt");
            //customSerializer.Save(goodsList);
            //Console.WriteLine("Saved to Custom:");
            //foreach (Goods goods in goodsList)
            //{
            //    Console.WriteLine(goods.Display());
            //}

            //goodsList = customSerializer.Load();
            //Console.WriteLine("Loaded from Custom:");
            //foreach (Goods goods in goodsList)
            //{
            //    Console.WriteLine(goods.Display());
            //}

            //XMLSerializer<Goods> xmlSerializer = new("xml.xml");
            //xmlSerializer.Save(goodsList);
            //Console.WriteLine("Saved to XML:");
            //foreach (Goods goods in goodsList)
            //{
            //    Console.WriteLine(goods.Display());
            //}

            //goodsList = xmlSerializer.Load();
            //Console.WriteLine("Loaded from Custom: ");
            //foreach (Goods goods in goodsList)
            //{
            //    Console.WriteLine(goods.Display());
            //}

            //BinarySerializer<Goods> binarySerializer = new("binary.bin");
            //binarySerializer.Save(goodsList);
            //Console.WriteLine("Saved to Binary:");
            //foreach (Goods goods in goodsList)
            //{
            //    Console.WriteLine(goods.Display());
            //}

            //goodsList = binarySerializer.Load();
            //Console.WriteLine("Loaded from Binary: ");
            //foreach (Goods goods in goodsList)
            //{
            //    Console.WriteLine(goods.Display());
            //}

        }
    }
}

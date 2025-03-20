class Program
{
    // definera klassen Product som innehåller information om en produkt
    public class Product
    {
        public string Category { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public Product(string category, string productName, decimal price)
        {
            Category = category;
            ProductName = productName;
            Price = price;
        }

        // överskriv ToString-metoden för att skriva ut informationen om produkten
        public override string ToString()
        {
            return $"Kategori: {Category}, Produktnamn: {ProductName}, Pris: {Price:F0}";
        }
    }

    // definera klassen ProductList som innehåller en lista av produkter
    public class ProductList
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void DisplayProducts()
        {
            var sortedProducts = products.OrderBy(p => p.Price).ToList();
            decimal grandTotalSum = sortedProducts.Sum(p => p.Price);

            Console.WriteLine("\nLista för alla produkter:");
            Console.WriteLine(" "); // Tom rad för att skapa en tom rad i konsolen
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{"Kategori",-20} {"Produktnamn",-30} {"Pris",-10}");
            Console.ResetColor();
            Console.WriteLine(new string('-', 60));

            foreach (var product in sortedProducts)
            {
                Console.WriteLine($"{product.Category,-20} {product.ProductName,-30} {product.Price,-10:F0}");
            }

            // skriv ut total summa för alla produkter med grandTotalSum
            Console.WriteLine(new string('-', 60));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nTotal summa för alla produkter: {grandTotalSum:F0}kr");
            Console.ResetColor();
            Console.WriteLine("-----------------------------------");
        }
    }

    static void Main(string[] args)
    {
        // skapa en ny instans av ProductList
        ProductList productList = new ProductList();

        while (true)
        {
            AddProducts(productList);
            productList.DisplayProducts();

            Console.WriteLine("Vill du lägga till fler produkter? (j/n)");
            string response = Console.ReadLine();
            if (response.ToLower() != "j") break;
        }
        Console.ForegroundColor = ConsoleColor.Yellow; 
        Console.WriteLine("Tack för att du testade programmet!");
        Console.ReadKey();
    }

    static void AddProducts(ProductList productList)
    {
        while (true)
        {
            // infotext
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Skriv in en produkt eller skriv 'q' för att avsluta.");
            Console.ResetColor();

            // fråga efter kategori genom att skriva in kategori
            string category;
            while (true)
            {
                Console.Write("Kategori: ");
                category = Console.ReadLine();
                if (category.ToLower() == "q") return;
                if (!string.IsNullOrWhiteSpace(category)) break;
                
                //om fältet är tomt, skriv ut ett felmeddelande
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fältet kan ej vara tomt.");
                Console.ResetColor();
            }

            // fråga efter produktens namn
            string productName;
            while (true)
            {
                Console.Write("Produktnamn: ");
                productName = Console.ReadLine();
                if (productName.ToLower() == "q") return;
                if (!string.IsNullOrWhiteSpace(productName)) break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fältet kan ej vara tomt.");
                Console.ResetColor();
            }

            // fråga efter priset
            decimal price = 0;
            while (true)
            {
                Console.Write("Pris: ");
                string priceInput = Console.ReadLine();
                if (priceInput.ToLower() == "q") return;

                if (decimal.TryParse(priceInput, out price))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vänligen skriv ett siffra för priset.");
                    Console.ResetColor();
                }
            }
            if (price == 0) return;

            // skapa en ny produkt och lägg till den i listan
            Product newProduct = new Product(category, productName, price);
            productList.AddProduct(newProduct);

            // skriv ut att produkten har lagts till korrekt
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Produkten har lagts till korrekt.");
            Console.ResetColor();
        }
    }
}
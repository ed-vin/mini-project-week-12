class Program
{
    // definera klassen Product som innehåller information om en produkt
    public class Product
    {
        public string Category { get; set; } // kategori
        public string ProductName { get; set; } // produktnamn
        public decimal Price { get; set; } // pris

        public Product(string category, string productName, decimal price)  // konstruktor för att skapa en ny produkt
        {
            Category = category; // sätt värdena för kategori, produktnamn och pris
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
        private List<Product> products = new List<Product>(); // skapa en lista av produkter

        public void AddProduct(Product product) // lägg till en produkt i listan
        {
            products.Add(product);
        }

        public void DisplayProducts() // skriv ut alla produkter i listan
        {
            var sortedProducts = products.OrderBy(p => p.Price).ToList();
            decimal grandTotalSum = sortedProducts.Sum(p => p.Price);

            Console.WriteLine("\nLista för alla produkter:");
            Console.WriteLine(" "); // Tom rad för att skapa en tom rad i konsolen
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{"Kategori",-20} {"Produktnamn",-30} {"Pris",-10}");
            Console.ResetColor();
            Console.WriteLine(new string('-', 60));

            foreach (var product in sortedProducts) // loopa igenom alla produkter och skriv ut informationen
            {
                Console.WriteLine($"{product.Category,-20} {product.ProductName,-30} {product.Price,-10:F0}");
            }

            // skriv ut total summa för alla produkter med grandTotalSum
            Console.WriteLine(new string('-', 60));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nTotal summa för alla produkter: {grandTotalSum:F0}kr");
            Console.ResetColor();
            Console.WriteLine("-----------------------------------");
        }
    }

    static void Main(string[] args) // huvudmetoden
    {
        // skapa en ny instans av ProductList
        ProductList productList = new ProductList();

        while (true) // loopa för att lägga till flera produkter
        {
            AddProducts(productList);
            productList.DisplayProducts();

            Console.WriteLine("Vill du lägga till fler produkter? (j/n)");
            string response = Console.ReadLine();
            if (response.ToLower() != "j") break;
        }
        // skriv ut att programmet avslutas
        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.WriteLine("Tack för att du testade programmet!");
        Console.ReadKey();
    }

    static void AddProducts(ProductList productList) // metoden för att lägga till produkter
    {
        while (true)
        {
            // infotext
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Skriv in en produkt genom följande steg | skriv 'Q' för att avsluta och visa listan.");
            Console.ResetColor();

            // fråga efter kategori genom att skriva in kategori
            string category;
            while (true) // loopa tills användaren skriver in en kategori
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
            while (true) // loopa tills användaren skriver in ett produktnamn
            {
                Console.Write("Produktnamn: ");
                productName = Console.ReadLine();
                if (productName.ToLower() == "q") return;
                if (!string.IsNullOrWhiteSpace(productName)) break; // om produktnamnet inte är tomt, bryt loopen

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fältet kan ej vara tomt."); // om produktnamnet är tomt, skriv ut ett felmeddelande
                Console.ResetColor();
            }

            // fråga efter priset
            decimal price = 0; // sätt priset till 0
            while (true) // loopa tills användaren skriver in ett pris
            {
                Console.Write("Pris: ");
                string priceInput = Console.ReadLine();
                if (priceInput.ToLower() == "q") return;

                if (decimal.TryParse(priceInput, out price))
                {
                    break; // om priset är en siffra, bryt loopen
                }
                else // om priset inte är en siffra, skriv ut ett felmeddelande
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vänligen skriv ett siffra för priset.");
                    Console.ResetColor();
                }
            }
            if (price == 0) return; // om priset är 0, bryt loopen

            // skapa en ny produkt och lägg till den i listan
            Product newProduct = new Product(category, productName, price);
            productList.AddProduct(newProduct);

            // skriv ut att produkten har lagts till korrekt
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Produkten har lagts till korrekt i listan.");
            Console.ResetColor();
        }
    }
}
using IMyFictionalShopDAL;
using System.Text.Json;

namespace DatabaseCreation
{
  internal class ClientJsonFile
  {
    public Guid Id { get; set; }
    public required string EmailAddress { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string StreetAddress { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Zip { get; set; }
  }
  internal class ProductJsonFile
  {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required string SubCategory { get; set; }
    public required string SalePrice { get; set; }
    public required string Price { get; set; }
  }

  internal class RawDataGenerator
  {
    private static List<string> _addDelInfo = new List<string>(10)
    {
      "Fragile",
      "Par avion",
      "3e étage bureau de gauche",
      "Ne pas sentir",
      "À l'intention du boss",
      "Ne pas livrer les mercredis",
      "N/A",
      "Ne pas ouvrir",
      "Urgent",
      "Gouvernemental"
    };

    public List<Client> Clients { get; set; }
    public List<Product> Products { get; set; }
    public List<Order> Orders { get; set; }

    public async Task Generate(string clientListFilePathName, string productListFilePathName)
    {
      try
      {
        Random rand = new Random();

        RandomDateGenerator dateGen = new RandomDateGenerator(
          new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
          new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc),
          rand);

        List<ClientJsonFile> clients = await LoadJsonFile<ClientJsonFile>(clientListFilePathName);
        List<ProductJsonFile> products = await LoadJsonFile<ProductJsonFile>(productListFilePathName);

        Clients = new List<Client>();
        clients.ForEach(c => 
        {
          Clients.Add(new Client()
          {
            Id = c.Id,
            EmailAdress = c.EmailAddress,
            ShipTo = new Address()
            {
              Name = new FullName()
              {
                FirstName = c.FirstName,
                LastName = c.LastName,
              },
              StreetAddress = c.StreetAddress,
              City = c.City,
              State = c.State,
              Zip = c.Zip
            }
          });
        });

        Products = new List<Product>();
        products.ForEach(p => 
        {
          Products.Add(new Product()
          {
            Id = p.Id,
            Name = p.Name,
            Category = p.Category,
            SubCategory = p.SubCategory,
            SalePrice = double.Parse(p.SalePrice, System.Globalization.CultureInfo.InvariantCulture),
            Price = double.Parse(p.Price, System.Globalization.CultureInfo.InvariantCulture)
          });
        });

        // generate 500k random orders
        Orders = new List<Order>();
        List<Product> productsTmp;
        int nbProduct;
        for(int i = 0; i < 500000; i++)
        {
          productsTmp = new List<Product>(10);
          nbProduct = rand.Next(10);

          for(int j = 0; j < nbProduct; j++)
          {
            productsTmp.Add(Products[rand.Next(Products.Count)]);
          }

          Orders.Add(new Order()
          {
            Id = Guid.NewGuid(),
            Client = Clients[rand.Next(1000)],
            OrderDateUTC = dateGen.Next(),
            AdditionalDeliveryInformation = _addDelInfo[rand.Next(10)],
            Products = productsTmp
          });
        }
      }
      catch { }
    }

    private async Task<List<T>> LoadJsonFile<T>(string path)
    {
      string jsonText = await File.ReadAllTextAsync(path);
      return JsonSerializer.Deserialize<List<T>>(jsonText);
    }

  }
}

using IMyFictionalShopDAL;
using System.Security.Cryptography;
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
    private static List<string> _addDelInfo = new List<string>()
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
      }
      catch { }
    }

    private async Task<List<T>> LoadJsonFile<T>(string path)
    {
      string jsonText = await File.ReadAllTextAsync(path);
      return JsonSerializer.Deserialize<List<T>>(jsonText);
    }

    //  public static List<Order> CreateOrdersData()
    //  {
    //    List<Order> orders = new List<Order>();


    //    for(int i = 0; i < 5000; i++)
    //    {
    //      orders.Add(GetRandomOrder());
    //    }

    //    #region dp order
    //    FullName dpFullName = new FullName()
    //    {
    //      FirstName = "David",
    //      LastName = "Pinson",
    //      MiddleName = ""
    //    };
    //    Address dpAddress = new Address()
    //    {
    //      AdditionalDeliveryInformation = "",
    //      City = "Saint-Basile-le-Grand",
    //      Country = "Canada",
    //      Name = dpFullName,
    //      StateProvinceTerritory = "QC",
    //      StreetAddress = "39 rue Savaria",
    //      ZipPostalCode = "J3N 1L8"
    //    };
    //    Order dpOrder = new Order()
    //    {
    //      BillTo = dpAddress,
    //      OrderDateUTC = DateTime.SpecifyKind(new DateTime(2018, 11, 17, 13, 47, 32), DateTimeKind.Utc),
    //      OrderId = Guid.NewGuid(),
    //      Products = new List<Product>()
    //      {
    //        new Product()
    //        {
    //          Description = "Microsoft Surface GO 8go RAM 128 go SSD",
    //          Name = "Microsoft Surface GO",
    //          ProductId = Guid.NewGuid(),
    //          UnitCost = 349.99,
    //          UnitPrice = 699.90
    //        },
    //        new Product()
    //        {
    //          Description = "Microsoft Surface GO Keyboard",
    //          Name = "Microsoft Keyboard",
    //          ProductId = Guid.NewGuid(),
    //          UnitCost = 39.99,
    //          UnitPrice = 112.99
    //        }
    //      },
    //      ShipTo = dpAddress
    //    };
    //    #endregion
    //    orders.Add(dpOrder);

    //    for(int i = 0; i < 5000; i++)
    //    {
    //      orders.Add(GetRandomOrder());
    //    }

    //    return orders;
    //  }

    //  private static Order GetRandomOrder()
    //  {
    //    return new Order()
    //    {
    //      BillTo = GetRandomAdddress(),
    //      OrderDateUTC = GetRandomOrderDateUTC(),
    //      OrderId = Guid.NewGuid(),
    //      Products = GetRandomProducts(),
    //      ShipTo = GetRandomAdddress()
    //    };
    //  }
    //  private static Address GetRandomAdddress()
    //  {
    //    byte i1 = GetIndex(10);
    //    byte i2 = GetIndex(10);
    //    byte i3 = GetIndex(10);
    //    byte i4 = GetIndex(10);
    //    byte i5 = GetIndex(10);
    //    byte i6 = GetIndex(10);

    //    return new Address()
    //    {
    //      AdditionalDeliveryInformation = _addDelInfo[i1],
    //      City = _city[i2],
    //      Country = _country[i3],
    //      Name = GetRandomFullName(),
    //      StateProvinceTerritory = _stateProvTerr[i4],
    //      StreetAddress = _streetAddress[i5],
    //      ZipPostalCode = _zipPostalCode[i6]
    //    };
    //  }
    //  private static FullName GetRandomFullName()
    //  {
    //    byte fni = GetIndex((byte)_prenom.Count);
    //    byte lni = GetIndex((byte)_nom.Count);
    //    byte mni = GetIndex((byte)_prenom.Count);

    //    return new FullName()
    //    {
    //      FirstName = _prenom[fni],
    //      LastName = _nom[lni],
    //      MiddleName = _prenom[mni]
    //    };
    //  }
    //  private static List<Product> GetRandomProducts()
    //  {
    //    List<Product> products = new List<Product>();

    //    byte nbProduct = GetIndex(15);
    //    nbProduct = nbProduct == 0 ? (byte)1 : nbProduct;

    //    for(int i = 0; i < nbProduct; i++)
    //    {
    //      products.Add(GetRandomProduct());
    //    }

    //    return products;
    //  }
    //  private static Product GetRandomProduct()
    //  {
    //    byte index = GetIndex((byte)_products.Count);
    //    return _products[index];
    //  }
    //  private static DateTime GetRandomOrderDateUTC()
    //  {
    //    int year = GetIndex(6) + 2013;
    //    int month = GetIndex(12) + 1;
    //    int day = GetIndex(27) + 1;
    //    int hour = GetIndex(24);
    //    int minute = GetIndex(60);
    //    int second = GetIndex(60);

    //    return DateTime.SpecifyKind(new DateTime(year, month, day, hour, minute, second), DateTimeKind.Utc);
    //  }

    //  private static byte GetIndex(byte containerSize)
    //  {
    //    byte[] randomNumber = new byte[1];
    //    do
    //    {
    //      _rngCsp.GetBytes(randomNumber);
    //    }
    //    while(IsFairIndex(randomNumber[0], containerSize) == false);

    //    return (byte)(randomNumber[0] % containerSize);
    //  }
    //  private static bool IsFairIndex(byte index, byte containerSize)
    //  {
    //    int fullSetsOfValues = Byte.MaxValue / containerSize;
    //    return index < containerSize * fullSetsOfValues;
    //  }
  }
}

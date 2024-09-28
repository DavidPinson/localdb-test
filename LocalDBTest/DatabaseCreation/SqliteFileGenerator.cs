using IMyFictionalShopDAL;
using Microsoft.Data.Sqlite;

namespace DatabaseCreation
{
  internal class SqliteFileGenerator
  {
    public static void Generate(string pathFileName, List<Client> clients, List<Product> products, List<Order> orders)
    {
      using(SqliteConnection connection = new SqliteConnection($"Data Source={pathFileName}"))
      {
        connection.Open();
        
        using(SqliteTransaction transaction = connection.BeginTransaction())
        using(SqliteCommand command = connection.CreateCommand())
        {
          string createClientsTableQuery = @"
            CREATE TABLE Clients
            (
              ClientId TEXT NOT NULL PRIMARY KEY,
              EmailAdress TEXT NOT NULL,
              stFirstName TEXT NOT NULL,
              stLastName TEXT NOT NULL,
              stStreetAddress TEXT NOT NULL,
              stCity TEXT NOT NULL,
              stState TEXT NOT NULL,
              stZip TEXT NOT NULL,
              btFirstName TEXT,
              btLastName TEXT,
              btStreetAddress TEXT,
              btCity TEXT,
              btState TEXT,
              btZip TEXT
            );
            CREATE UNIQUE INDEX idx_clients_emailadress ON Clients(EmailAdress);
            CREATE INDEX idx_clients_stfirstname ON Clients(stFirstName);
            CREATE INDEX idx_clients_stlastname ON Clients(stLastName);
            CREATE INDEX idx_clients_ststreetaddress ON Clients(stStreetAddress);
            CREATE INDEX idx_clients_stcity ON Clients(stCity);
            CREATE INDEX idx_clients_ststate ON Clients(stState);
            CREATE INDEX idx_clients_stzip ON Clients(stZip);
          ";
          string createProductsTableQuery = @"
            CREATE TABLE Products
            (
              ProductId TEXT NOT NULL PRIMARY KEY,
              Name TEXT NOT NULL,
              Category TEXT NOT NULL,
              SubCategory TEXT NOT NULL,
              SalePrice REAL NOT NULL,
              Price REAL NOT NULL
            );
            CREATE INDEX idx_products_name ON Products(Name);
            CREATE INDEX idx_products_category ON Products(Category);
            CREATE INDEX idx_products_subcategory ON Products(SubCategory);
          ";
          string createOrdersTableQuery = @"
            CREATE TABLE Orders
            (
              OrderId TEXT NOT NULL PRIMARY KEY,
              IdClient TEXT NOT NULL,
              OrderDateUTC TEXT NOT NULL,
              AdditionalDeliveryInformation TEXT
            );
            CREATE INDEX idx_orders_orderdateutc ON Orders(OrderDateUTC);
          ";
          string createOrderProductsTableQuery = @"
            CREATE TABLE OrderProducts
            (
              opIdOrder TEXT NOT NULL,
              opIdProduct TEXT NOT NULL,
              PRIMARY KEY(opIdOrder, opIdProduct)
            );
          ";

          command.CommandText = createClientsTableQuery;
          command.ExecuteNonQuery();
          command.CommandText = createProductsTableQuery;
          command.ExecuteNonQuery();
          command.CommandText = createOrdersTableQuery;
          command.ExecuteNonQuery();
          command.CommandText = createOrderProductsTableQuery;
          command.ExecuteNonQuery();

          command.Parameters.Add("@ClientId", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@EmailAdress", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@stFirstName", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@stLastName", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@stStreetAddress", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@stCity", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@stState", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@stZip", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@btFirstName", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@btLastName", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@btStreetAddress", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@btCity", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@btState", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@btZip", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@ProductId", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@Name", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@Category", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@SubCategory", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@SalePrice", SqliteType.Real).Value = DBNull.Value;
          command.Parameters.Add("@Price", SqliteType.Real).Value = DBNull.Value;
          command.Parameters.Add("@OrderId", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@IdClient", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@OrderDateUTC", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@AdditionalDeliveryInformation", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@opIdOrder", SqliteType.Text).Value = DBNull.Value;
          command.Parameters.Add("@opIdProduct", SqliteType.Text).Value = DBNull.Value;

          string inserClientQuery = @"
            INSERT INTO Clients
            (
              ClientId,
              EmailAdress,
              stFirstName,
              stLastName,
              stStreetAddress,
              stCity,
              stState,
              stZip,
              btFirstName,
              btLastName,
              btStreetAddress,
              btCity,
              btState,
              btZip
            )
            VALUES
            (
              @ClientId,
              @EmailAdress,
              @stFirstName,
              @stLastName,
              @stStreetAddress,
              @stCity,
              @stState,
              @stZip,
              @btFirstName,
              @btLastName,
              @btStreetAddress,
              @btCity,
              @btState,
              @btZip
            )
            ";
          string insertProductQuery = @"
            INSERT INTO Products
            (
              ProductId,
              Name,
              Category,
              SubCategory,
              SalePrice,
              Price
            )
            VALUES
            (
              @ProductId,
              @Name,
              @Category,
              @SubCategory,
              @SalePrice,
              @Price
            )
            ";
          string insertOrderQuery = @"
            INSERT INTO Orders
            (
              OrderId,
              IdClient,
              OrderDateUTC,
              AdditionalDeliveryInformation
            )
            VALUES
            (
              @OrderId,
              @IdClient,
              @OrderDateUTC,
              @AdditionalDeliveryInformation
            )
            ";
          string insertOrderProductsQuery = @"
            INSERT INTO OrderProducts
            (
              opIdOrder,
              opIdProduct
            )
            VALUES
            (
              @opIdOrder,
              @opIdProduct
            )
            ";

          command.CommandText = inserClientQuery;
          clients.ForEach(c =>
          {
            command.Parameters["@ClientId"].Value = c.Id.ToString();
            command.Parameters["@EmailAdress"].Value = c.EmailAdress;
            command.Parameters["@stFirstName"].Value = c.ShipTo.Name.FirstName;
            command.Parameters["@stLastName"].Value = c.ShipTo.Name.LastName;
            command.Parameters["@stStreetAddress"].Value = c.ShipTo.StreetAddress;
            command.Parameters["@stCity"].Value = c.ShipTo.City;
            command.Parameters["@stState"].Value = c.ShipTo.State;
            command.Parameters["@stZip"].Value = c.ShipTo.Zip;
            command.Parameters["@btFirstName"].Value = c.BillTo.Name?.FirstName ?? string.Empty;
            command.Parameters["@btLastName"].Value = c.BillTo.Name?.LastName ?? string.Empty;
            command.Parameters["@btStreetAddress"].Value = c.BillTo.StreetAddress ?? string.Empty;
            command.Parameters["@btCity"].Value = c.BillTo.City ?? string.Empty;
            command.Parameters["@btState"].Value = c.BillTo.State ?? string.Empty;
            command.Parameters["@btZip"].Value = c.BillTo.Zip ?? string.Empty;
            command.ExecuteNonQuery();
          });

          command.CommandText = insertProductQuery;
          products.ForEach(p =>
          {
            command.Parameters["@ProductId"].Value = p.Id;
            command.Parameters["@Name"].Value = p.Name;
            command.Parameters["@Category"].Value = p.Category;
            command.Parameters["@SubCategory"].Value = p.SubCategory;
            command.Parameters["@SalePrice"].Value = p.SalePrice;
            command.Parameters["@Price"].Value = p.Price;
            command.ExecuteNonQuery();
          });

          orders.ForEach(o =>
          {
            command.CommandText = insertOrderProductsQuery;
            o.Products.ForEach(p =>
            {
              command.Parameters["@opIdOrder"].Value = o.Id;
              command.Parameters["@opIdProduct"].Value = p.Id;
              command.ExecuteNonQuery();
            });

            command.CommandText = insertOrderQuery;
            command.Parameters["@OrderId"].Value = o.Id;
            command.Parameters["@IdClient"].Value = o.Client.Id;
            command.Parameters["@OrderDateUTC"].Value = o.OrderDateUTC;
            command.Parameters["@AdditionalDeliveryInformation"].Value = o.AdditionalDeliveryInformation;
            command.ExecuteNonQuery();
          });

          transaction.Commit();
        }
      }
    }
  }
}

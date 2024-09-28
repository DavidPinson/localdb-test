using IMyFictionalShopDAL;
using LiteDB;

namespace DatabaseCreation
{
  internal class DbliteFileGenerator
  {
    public DbliteFileGenerator() { }

    public static void Generate(string pathFileName, List<Client> clients, List<Product> products, List<Order> orders)
    {
      using(LiteDatabase ldb = new LiteDatabase(pathFileName))
      {
        ILiteCollection<Client> clientsCol = ldb.GetCollection<Client>("Clients");
        ILiteCollection<Product> productsCol = ldb.GetCollection<Product>("Products");
        ILiteCollection<Order> ordersCol = ldb.GetCollection<Order>("Orders");

        ldb.BeginTrans();

        clientsCol.InsertBulk(clients);
        productsCol.InsertBulk(products);
        ordersCol.InsertBulk(orders);

        clientsCol.EnsureIndex(x => x.Id, true);
        clientsCol.EnsureIndex(x => x.EmailAdress, true);

        productsCol.EnsureIndex(x => x.Id, true);
        productsCol.EnsureIndex(x => x.Name);
        productsCol.EnsureIndex(x => x.Category);
        productsCol.EnsureIndex(x => x.SubCategory);

        ordersCol.EnsureIndex(x => x.Id, true);
        ordersCol.EnsureIndex(x => x.Client);
        ordersCol.EnsureIndex(x => x.Products);
        ordersCol.EnsureIndex(x => x.OrderDateUTC);

        ldb.Commit();
      }
    }
  }
}

using System;
using System.Collections.Generic;

namespace IMyFictionalShopDAL
{
  public class Client
  {
    public Guid Id { get; set; }
    public string EmailAdress { get; set; }
    public Address ShipTo { get; set; }
    public Address BillTo { get; set; }
  }

  public class FullName
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  public class Address
  {
    public FullName Name { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
  }

  public class Product
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public double SalePrice { get; set; }
    public double Price { get; set; }
  }

  public class Order
  {
    public Guid Id { get; set; }
    public Client Client { get; set; }
    public DateTime OrderDateUTC { get; set; }
    public List<Product> Products { get; set; }
    public string AdditionalDeliveryInformation { get; set; }
  }

}

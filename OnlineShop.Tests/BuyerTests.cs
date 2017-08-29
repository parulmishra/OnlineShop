using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Tests
{
  [TestClass]
  public class BuyerTests : IDisposable
  {
    public BuyerTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=online_shop_test;";
    }

    [TestMethod]
    public void GetAll_GetsAllBuyersInEmptyDatabase_BuyerList()
    {
      List<Buyer> expected = new List<Buyer> {};
      List<Buyer> actual = Buyer.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_SavesBuyerToDatabase_Buyer()
    {
      Buyer expected = new Buyer("username", "phone", "email", "password", "creditCard");
      expected.Save();

      Buyer actual = Buyer.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindsBuyerInDatabaseById_Buyer()
    {
      Buyer expected = new Buyer("username", "phone", "email", "password", "creditCard");
      expected.Save();

      Buyer actual = Buyer.Find(expected.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesBuyerInDatabase_BuyerList()
    {
      Buyer one = new Buyer("username", "phone", "email", "password", "creditCard");
      Buyer two = new Buyer("usernametwo", "phone", "email", "password", "creditCard");
      one.Save();
      two.Save();

      one.Delete();
      List<Buyer> expected = new List<Buyer> {two};
      List<Buyer> actual = Buyer.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetOrderHistory_ReturnsListOfOrdersPurchasedByBuyer_OrderList()
    {
      Buyer newBuyer = new Buyer("username", "phone", "email", "password", "creditCard");
      newBuyer.Save();
      Order one = new Order(newBuyer.GetId(),default(DateTime));
      Order two = new Order(newBuyer.GetId(),default(DateTime));
      Order three = new Order(newBuyer.GetId(),default(DateTime));

      one.Save();
      two.Save();
      three.Save();

      one.Purchase();
      two.Purchase();

      List<Order> expected = new List<Order> {one, two};
      List<Order> actual = newBuyer.GetOrderHistory();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetCurrentOrder_ReturnsBuyersCurrentOrder_Order()
    {
      Buyer newBuyer = new Buyer("username", "phone", "email", "password", "creditCard");
      newBuyer.Save();
      Order one = new Order(newBuyer.GetId(),default(DateTime));
      Order two = new Order(newBuyer.GetId(),default(DateTime));
      Order three = new Order(newBuyer.GetId(),default(DateTime));

      one.Save();
      two.Save();
      three.Save();

      one.Purchase();
      two.Purchase();

      Order expected = three;
      Order actual = newBuyer.GetCurrentOrder();

      Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void GetAddresses_GetsBuyersAddresses_ListAddresses()
    {
      Buyer newBuyer = new Buyer("username", "phone", "email", "password", "creditCard");
      newBuyer.Save();

      Address one = new Address(newBuyer.GetId(),"street","name","city","state","country","zip");
      Address two = new Address(newBuyer.GetId(),"street2","name2","city2","state2","country2","zip2");
      one.Save();
      two.Save();

      List<Address> expected = new List<Address>{one, two};
      List<Address> actual = newBuyer.GetAddresses();

      CollectionAssert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void Delete_DeletesRelationshipsinDatabase_OrderList()
    {
      Buyer newBuyer = new Buyer("username", "phone", "email", "password", "creditCard");
      newBuyer.Save();

      Buyer newBuyer2 = new Buyer("username", "phone", "email", "password", "creditCard");
      newBuyer2.Save();

      Order one = new Order(newBuyer.GetId(),default(DateTime));
      Order two = new Order(newBuyer.GetId(),default(DateTime));
      Order three = new Order(newBuyer2.GetId(),default(DateTime));

      one.Save();
      two.Save();
      three.Save();

      newBuyer.Delete();

      List<Order> expected = new List<Order>{three};
      List<Order> actual = Order.GetAll();

      CollectionAssert.AreEqual(expected,actual);
    }

    public void Dispose()
    {
      Category.DeleteAll();
      Product.DeleteAll();
      Item.DeleteAll();
      Address.DeleteAll();
      Buyer.DeleteAll();
      Order.DeleteAll();
    }
  }
}

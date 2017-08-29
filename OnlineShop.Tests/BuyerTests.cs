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

    public void Dispose()
    {
      Category.DeleteAll();
      Product.DeleteAll();
      Item.DeleteAll();
      Buyer.DeleteAll();
      Order.DeleteAll();
    }
  }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Tests
{
  [TestClass]
  public class OrderTests : IDisposable
  {
    public OrderTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=online_shop_test;";
    }

    [TestMethod]
    public void GetAll_GetsAllOrdersInEmptyDatabase_OrderList()
    {
      List<Order> expected = new List<Order> {};
      List<Order> actual = Order.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_SavesOrderToDatabase_Order()
    {
      Order expected = new Order(0,default(DateTime),false);
      expected.Save();

      Order actual = Order.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindsOrderInDatabaseById_Order()
    {
      Order expected = new Order(0,default(DateTime),false);
      expected.Save();

      Order actual = Order.Find(expected.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesOrderInDatabase_OrderList()
    {
      Order one = new Order(0,default(DateTime),false);
      Order two = new Order(1,default(DateTime),true);
      one.Save();
      two.Save();

      one.Delete();
      List<Order> expected = new List<Order> {two};
      List<Order> actual = Order.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void AddItem_AddAndGetItems_ItemList()
    {
      Order newOrder = new Order(0,default(DateTime),false);
      newOrder.Save();

      Item newItem = new Item("small", "red", 0);
      newItem.Save();

      newOrder.AddItem(newItem.GetId());

      List<Item> expected = new List<Item>{newItem};
      List<Item> actual = newOrder.GetItems();

      CollectionAssert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void RemoveItem_RemoveItemFromJoinTable_ItemList()
    {
      Order newOrder = new Order(0,default(DateTime),false);
      newOrder.Save();

      Item newItem = new Item("small", "red", 0);
      newItem.Save();

      Item newItem2 = new Item("large", "red", 0);
      newItem2.Save();

      newOrder.AddItem(newItem.GetId());
      newOrder.AddItem(newItem2.GetId());

      newOrder.RemoveItem(newItem2.GetId());

      List<Item> expected = new List<Item>{newItem};
      List<Item> actual = newOrder.GetItems();

      CollectionAssert.AreEqual(expected,actual);
    }

    [TestMethod]
    public void RemoveAllItems_RemoveAllItemsFromJoinTableForThisOrder_ItemList()
    {
      Order newOrder = new Order(0,default(DateTime),false);
      newOrder.Save();

      Item newItem = new Item("small", "red", 0);
      newItem.Save();

      Item newItem2 = new Item("large", "red", 0);
      newItem2.Save();

      newOrder.AddItem(newItem.GetId());
      newOrder.AddItem(newItem2.GetId());

      newOrder.RemoveAllItems();


      List<Item> expected = new List<Item>{};
      List<Item> actual = newOrder.GetItems();

      CollectionAssert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void GetSubtotal_GetSubtotalOfItemsInOrder_Double()
    {
      Order newOrder = new Order(0,default(DateTime),false);
      newOrder.Save();

      Product firstProduct = new Product(1,"Adidas",100.50,"good quality","Amazon","",1);
      firstProduct.Save();

      Item newItem = new Item("small", "red", firstProduct.GetId());
      newItem.Save();

      Item newItem2 = new Item("large", "red", firstProduct.GetId());
      newItem2.Save();

      newOrder.AddItem(newItem.GetId());
      newOrder.AddItem(newItem2.GetId());

      double actual = newOrder.GetSubtotal();
      double expected = 201.00;

      Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void Purchase_PurchaseItemsInCart_Order()
    {
      Order newOrder = new Order(0,default(DateTime),false);
      newOrder.Save();

      Product firstProduct = new Product(1,"Adidas",100.50,"good quality","Amazon","",1);
      firstProduct.Save();

      Item newItem = new Item("small", "red", firstProduct.GetId());
      newItem.Save();

      Item newItem2 = new Item("large", "red", firstProduct.GetId());
      newItem2.Save();

      newOrder.AddItem(newItem.GetId());
      newOrder.Purchase();

      Order expected = newOrder;
      Order actual = Order.Find(newOrder.GetId());

      Assert.AreEqual(expected,actual);
    }

    public void Dispose()
    {
      Category.DeleteAll();
      Product.DeleteAll();
      Item.DeleteAll();
      Order.DeleteAll();
    }
  }
}

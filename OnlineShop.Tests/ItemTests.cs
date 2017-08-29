
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Tests
{
  [TestClass]
  public class ItemTests : IDisposable
  {
    public ItemTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=online_shop_test;";
    }

    [TestMethod]
    public void GetAll_GetsAllItemsInEmptyDatabase_ItemList()
    {
      List<Item> expected = new List<Item> {};
      List<Item> actual = Item.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_SavesItemToDatabase_Item()
    {
      Item expected = new Item("small", "red", 1);
      expected.Save();

      Item actual = Item.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindsItemInDatabaseById_Item()
    {
      Item expected = new Item("small", "red", 1);
      expected.Save();

      Item actual = Item.Find(expected.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesItemInDatabase_ItemList()
    {
      Item one = new Item("small", "red", 1);
      Item two = new Item("medium", "blue", 2);
      one.Save();
      two.Save();

      one.Delete();
      List<Item> expected = new List<Item> {two};
      List<Item> actual = Item.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetProductInfo_ReturnsProductOfItem_Product()
    {
      Product shoes = new Product(1, "Adidas","Shoes", 15.99, "Running shoes", "Fred Meyer", "");
      shoes.Save();

      Item myShoes = new Item("Small", "Red", shoes.GetId());
      myShoes.Save();

      Product expected = shoes;
      Product actual = myShoes.GetProductInfo();

      Assert.AreEqual(expected, actual);
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

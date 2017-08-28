
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

    public void Dispose()
    {
      Item.DeleteAll();
    }
  }
}

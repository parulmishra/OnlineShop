using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Tests
{
  [TestClass]
  public class AddressTests : IDisposable
  {
    public AddressTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=online_shop_test;";
    }

    [TestMethod]
    public void GetAll_GetsAllAddresssInEmptyDatabase_AddressList()
    {
      List<Address> expected = new List<Address> {};
      List<Address> actual = Address.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_SavesAddressToDatabase_Address()
    {
      Address expected = new Address(0,"street","name","city","state","country","zip");
      expected.Save();

      Address actual = Address.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindsAddressInDatabaseById_Address()
    {
      Address expected = new Address(0,"street","name","city","state","country","zip");
      expected.Save();

      Address actual = Address.Find(expected.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesAddressInDatabase_AddressList()
    {
      Address one = new Address(0,"street","name","city","state","country","zip");
      Address two = new Address(1,"street2","name2","city2","state2","country2","zip2");
      one.Save();
      two.Save();

      one.Delete();
      List<Address> expected = new List<Address> {two};
      List<Address> actual = Address.GetAll();

      CollectionAssert.AreEqual(expected, actual);
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Tests
{
  [TestClass]
  public class CategoryTest : IDisposable
  {
    public CategoryTest()
    {
     DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=online_shop_test;";
    }
    public void Dispose()
    {
     Category.DeleteAll();
     Product.DeleteAll();
    }
    [TestMethod]
    public void Equals_TrueForSameCategoryName_True()
    {
      //Arrange, Act
     Category firstCategory = new Category("Shirts");
     Category secondCategory = new Category("Shirts");
     //Assert
     Assert.AreEqual(firstCategory, secondCategory);
    }
    [TestMethod]
    public void Save_SavesCategoryToDatabase_CategoryList()
    {
      //Arrange
      Category category = new Category("Shirts");
      category.Save();
      //Act
      List<Category> expected = new List<Category> {category};
      List<Category> actual = Category.GetAll();
      //Assert
      CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      //Arrange
      Category testCategory = new Category("Pants");
      testCategory.Save();
      //Act
      Category savedCategory = Category.GetAll()[0];
      int actual = savedCategory.GetId();
      int expected = testCategory.GetId();
      //Assert
      Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Find_FindsCategoryInDatabase_Category()
    {
      //Arrange
      Category expected = new Category("Hats");
      expected.Save();
      //Act
      Category actual = Category.Find(expected.GetId());
      //Assert
      Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Delete_DeletesCategoryFromDatabase_CategoryList()
    {
      //Arrange
      Category testCategory1 = new Category("Socks");
      testCategory1.Save();
      Category testCategory2 = new Category("Tops");
      testCategory2.Save();
      //Act
      testCategory1.Delete();
      List<Category> actualCategoryList = Category.GetAll();
      List<Category> expectedCategoryList = new List<Category> {testCategory2};
      //Assert
      CollectionAssert.AreEqual(expectedCategoryList, actualCategoryList);
    }
    [TestMethod]
    public void GetProducts_GiveProductsForSpecificCategory_ProductList()
    {
      //Arrange
      Category testCategory1 = new Category("Socks");
      testCategory1.Save();
      int categoryId = testCategory1.GetId();
      Product newProduct = new Product(categoryId,"Adidas","shoes",100.0,"good quality","Amazon","",1);
      newProduct.Save();
      //Act

      List<Product> actualProductList = testCategory1.GetProducts();
      List<Product> expectedProductList = new List<Product>();
      
      expectedProductList.Add(newProduct);
      //Assert
      CollectionAssert.AreEqual(expectedProductList,actualProductList);
    }
  }
}

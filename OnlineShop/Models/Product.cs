using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace OnlineShop.Models
{
  public class Product
  {
    private int _id;
    private int _category_id;
    private string _brand;
    private string _name;
    double _price;
    private string _description;
    private string _seller;
    private string _image;

    public Product(int categoryId, string brand, string name, double price, string description, string seller, string image,int id= 0)
    {
      _category_id = categoryId;
      _brand = brand;
      _name = name;
      _price = price;
      _description = description;
      _seller = seller;
      _image = image;
      _id = id;
    }
    public int GetId()
    {
        return _id;
    }
    public int GetCategoryId()
    {
      return _category_id;
    }
    public string GetBrand()
    {
      return _brand;
    }
    public string GetName()
    {
      return _name;
    }
    public double GetPrice()
    {
      return _price;
    }
    public string GetDescription()
    {
      return _description;
    }
    public string GetSeller()
    {
      return _seller;
    }
    public string GetImage()
    {
      return _image;
    }
    public override bool Equals(Object otherProduct)
    {
      if (!(otherProduct is Product))
      {
        return false;
      }
      else
      {
        Product newProduct = (Product) otherProduct;
        bool idEquality = newProduct.GetId() == this._id;
        bool categoryIdEquality = newProduct.GetCategoryId() == this._category_id;
        bool brandEquality = newProduct.GetBrand() == this._brand;
        bool nameEquality = newProduct.GetName() == this._name;
        bool priceEquality = newProduct.GetPrice() == this._price;
        bool descriptionEquality = newProduct.GetDescription() == this._description;
        bool sellerEquality = newProduct.GetSeller() == this._seller;
        bool imageEquality = newProduct.GetImage() == this._image;
        return (idEquality && categoryIdEquality && brandEquality && priceEquality && descriptionEquality && sellerEquality && imageEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO products(category_id,brand,name,price,description,seller,image) VALUES (@category_id,@brand,@name,@price,@description,@seller,@image);";

      MySqlParameter categoryIdParameter = new MySqlParameter();
      categoryIdParameter.ParameterName = "@category_id";
      categoryIdParameter.Value = _category_id;
      cmd.Parameters.Add(categoryIdParameter);

      MySqlParameter brandParameter = new MySqlParameter();
      brandParameter.ParameterName = "@brand";
      brandParameter.Value = _brand;
      cmd.Parameters.Add(brandParameter);

      MySqlParameter nameParameter = new MySqlParameter();
      nameParameter.ParameterName = "@name";
      nameParameter.Value = _name;
      cmd.Parameters.Add(nameParameter);

      MySqlParameter priceParameter = new MySqlParameter();
      priceParameter.ParameterName = "@price";
      priceParameter.Value = _price;
      cmd.Parameters.Add(priceParameter);

      MySqlParameter descriptionParameter = new MySqlParameter();
      descriptionParameter.ParameterName = "@description";
      descriptionParameter.Value = _description;
      cmd.Parameters.Add(descriptionParameter);

      MySqlParameter sellerParameter = new MySqlParameter();
      sellerParameter.ParameterName = "@seller";
      sellerParameter.Value = _seller;
      cmd.Parameters.Add(sellerParameter);

      MySqlParameter imageParameter = new MySqlParameter();
      imageParameter.ParameterName = "@image";
      imageParameter.Value = _image;
      cmd.Parameters.Add(imageParameter);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Product> GetAll()
    {
      List<Product> allProducts = new List<Product>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"SELECT * FROM products;";

      var rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int category_id = rdr.GetInt32(1);
        string brand = rdr.GetString(2);
        string name = rdr.GetString(3);
        double price  = rdr.GetDouble(4);
        string description = rdr.GetString(5);
        string seller = rdr.GetString(6);
        string image = rdr.GetString(7);
        Product newProduct = new Product(category_id,brand,name,price,description,seller,image,id);
        allProducts.Add(newProduct);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allProducts;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM products; DELETE FROM items; DELETE FROM items_orders;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand(@"DELETE FROM products WHERE id=@thisId;",conn);

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      List<Item> productItems = GetItems();
      foreach(var item in productItems)
      {
        item.Delete();
      }

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    // public void DeleteByCategory()
    // {
    //   MySqlConnection conn =DB.Connection();
    //   conn.Open();
    //
    //   MySqlCommand cmd = new MySqlCommand(@"DELETE FROM products WHERE category_id=@thisId;");
    //
    //   MySqlParameter idParameter = new MySqlParameter();
    //   idParameter.ParameterName = "@thisId";
    //   idParameter.Value = _category_id;
    //   cmd.Parameters.Add(idParameter);
    //
    //   cmd.ExecuteNonQuery();
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
    public static Product Find(int id)
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM products WHERE id=@thisId";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;
      cmd.Parameters.Add(idParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int idProduct = 0;
      int idCategory = 0;
      string brand = "";
      string name = "";
      double price = 0.0;
      string description = "";
      string seller = "";
      string image = "";

      while(rdr.Read())
      {
        idProduct = rdr.GetInt32(0);
        idCategory = rdr.GetInt32(1);
        brand = rdr.GetString(2);
        name = rdr.GetString(3);
        price = rdr.GetDouble(4);
        description = rdr.GetString(5);
        seller = rdr.GetString(6);
        image = rdr.GetString(7);
      }
      var product = new Product(idCategory,brand,name,price,description,seller,image,idProduct);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return product;
    }
    public static List<Product> Search(string searchParameter)
    {
      List<Product> foundProducts = new List<Product>{};
      List<Product> categoryProducts = new List<Product>{};
      List<Category> foundCategories = new List<Category>{};

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM products WHERE brand LIKE CONCAT('%', @searchParameter, '%') OR name LIKE CONCAT('%', @searchParameter, '%'); ";

      MySqlParameter searchTermParameter = new MySqlParameter();
      searchTermParameter.ParameterName = "@searchParameter";
      searchTermParameter.Value = searchParameter;
      cmd.Parameters.Add(searchTermParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int productId = rdr.GetInt32(0);
        int categoryId = rdr.GetInt32(1);
        string productBrand = rdr.GetString(2);
        string name = rdr.GetString(3);
        double price = rdr.GetDouble(4);
        string description = rdr.GetString(5);
        string seller = rdr.GetString(6);
        string image = rdr.GetString(7);

        Product newProduct = new Product(categoryId,productBrand,name,price,description,seller,image,productId);
        foundProducts.Add(newProduct);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      MySqlConnection conn2 = DB.Connection();
      conn2.Open();

      var cmd2 = conn2.CreateCommand() as MySqlCommand;
      cmd2.CommandText = @"SELECT * FROM categories WHERE name LIKE CONCAT('%', @searchParameter, '%');";

      searchTermParameter.ParameterName = "@searchParameter";
      searchTermParameter.Value = searchParameter;
      cmd2.Parameters.Add(searchTermParameter);

      var rdr2 = cmd2.ExecuteReader() as MySqlDataReader;
      while(rdr2.Read())
      {
        int id = rdr2.GetInt32(0);
        string name = rdr2.GetString(1);
        Category newCategory = new Category(name, id);
        foundCategories.Add(newCategory);
      }
      foreach(var category in foundCategories)
      {

        foreach(var product in category.GetProducts())
        {
          categoryProducts.Add(product);
        }
      }

      List<Product> duplicatesRemoved = foundProducts.Union(categoryProducts).ToList();

      conn2.Close();
      if (conn2 != null)
      {
        conn2.Dispose();
      }
      return duplicatesRemoved;
    }
    public List<Item> GetItems()
    {
      List<Item> itemsForThisProduct = new List<Item>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items WHERE product_id=@productId;";

      MySqlParameter productIdParameter = new MySqlParameter();
      productIdParameter.ParameterName = "@productId";
      productIdParameter.Value = this._id;
      cmd.Parameters.Add(productIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
         int itemId = rdr.GetInt32(0);
         int productId = rdr.GetInt32(1);
         string size = rdr.GetString(2);
         string color = rdr.GetString(3);
         bool available = rdr.GetBoolean(4);

         Item item = new Item(size,color,productId,available,itemId);
         itemsForThisProduct.Add(item);
      }
      return itemsForThisProduct;
    }
  }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Product
  {
    private int _id;
    private int _category_id;
    private string _brand;
    private float _price;
    private string _description;
    private string _seller;

    public Product(int categoryId, string brand, float price, string description, string seller, int id= 0)
    {
      _category_id = categoryId;
      _brand = brand;
      _price = price;
      _description = description;
      _seller = seller;
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
    public float GetPrice()
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
        bool priceEquality = newProduct.GetPrice() == this._price;
        bool descriptionEquality = newProduct.GetDescription() == this._description;
        bool sellerEquality = newProduct.GetSeller() == this._seller;
        return (idEquality && categoryIdEquality && brandEquality && priceEquality && descriptionEquality && sellerEquality);
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
      cmd.CommandText = @"INSERT INTO products(category_id,brand,price,description,seller) VALUES (@category_id,@brand,@price,@description,@seller);";

      MySqlParameter brandParameter = new MySqlParameter();
      brandParameter.ParameterName = "@brand";
      brandParameter.Value = _brand;
      cmd.Parameters.Add(brandParameter);

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

      MySqlParameter categoryIdParameter = new MySqlParameter();
      categoryIdParameter.ParameterName = "@categoryId";
      categoryIdParameter.Value = _category_id;
      cmd.Parameters.Add(categoryIdParameter);

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
        float price  = rdr.GetFloat(3);
        string description = rdr.GetString(4);
        string seller = rdr.GetString(5);
        Product newProduct = new Product(category_id,brand,price,description,seller,id);
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
      cmd.CommandText = @"DELETE FROM products;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void DeleteByCategory()
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand(@"DELETE FROM products WHERE category_id=@thisId;");

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _category_id;
      cmd.Parameters.Add(idParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
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
      float price = 0.0;
      string description = "";
      string seller = "";

      while(rdr.Read())
      {
        idProduct = rdr.GetInt32(0);
        idCategory = rdr.GetInt32(1);
        brand = rdr.GetString(2);
        price = rdr.GetFloat(3);
        description = rdr.GetString(4);
        seller = rdr.GetString(5);
      }
      var product = new Product(idCategory,brand,price,description,seller,idProduct);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return product;
    }
    public static List<Product> Search(string seachParameter)
    {
      List<Product> foundProducts = new List<Product>{};
      string searchTerm = searchParameter.ToLower()[0].ToString();
      string wildCard = searchTerm + "%";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM products WHERE brand LIKE @wildCard;";

      MySqlParameter searchTermParameter = new MySqlParameter();
      searchTermParameter.ParameterName = "@wildCard";
      searchTermParameter.Value = wildCard;
      cmd.Parameters.Add(searchTermParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int productId = rdr.GetInt32(0);
        int categoryId = rdr.GetInt32(1);
        string productBrand = rdr.GetString(2);
        float price = rdr.GetFloat(3);
        string description = rdr.GetDescription(4);
        string seller = rdr.GetString(5);
        Product newProduct = new Product(categoryId,productBrand,price,description,seller,productId);
        foundProducts.Add(newProduct);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundProducts;
    }
  }
}

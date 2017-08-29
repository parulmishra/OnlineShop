using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Category
  {
    private int _id;
    private string _name;
    public Category(string name, int id=0)
    {
      _id = id;
      _name = name;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public override bool Equals(Object otherCategory)
    {
      if (!(otherCategory is Category))
      {
        return false;
      }
      else
      {
        Category newCategory = (Category) otherCategory;
        bool idEquality = newCategory.GetId() == this._id;
        bool nameEquality = newCategory.GetName() == this._name;
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO categories (name) VALUES (@name);";

      MySqlParameter nameParameter = new MySqlParameter();
      nameParameter.ParameterName = "@name";
      nameParameter.Value = _name;
      cmd.Parameters.Add(nameParameter);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories;";

      var rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Category newCategory = new Category(name, id);
        allCategories.Add(newCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM categories; DELETE FROM products; DELETE FROM items; DELETE from items_orders;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand(@"DELETE FROM categories WHERE id=@thisId;",conn);

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      List<Product> categoryProducts = GetProducts();
      foreach(var product in categoryProducts)
      {
        product.Delete();
      }

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Category Find(int id)
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories WHERE id=@thisId";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;
      cmd.Parameters.Add(idParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int idCategory = 0;
      string name = "";

      while(rdr.Read())
      {
        idCategory = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      var category = new Category(name, idCategory);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return category;
    }
    public List<Product> GetProducts(string sortParameter = "")
    {
      List<Product> productsForThisCategory = new List<Product>();
      MySqlConnection conn =DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      if (sortParameter == "priceHighToLow")
      {
        cmd.CommandText = @"SELECT * FROM products WHERE category_id=@categoryId SORT BY price DESC;";
      }
      else if (sortParameter =="priceLowToHigh")
      {
        cmd.CommandText = @"SELECT * FROM products WHERE category_id=@categoryId SORT BY price ASC;";
      }
      else if (sortParameter =="brandAlphabetical")
      {
        cmd.CommandText = @"SELECT * FROM products WHERE category_id=@categoryId SORT BY brand DESC;";
      }
      else if (sortParameter =="brandAlphabeticalReverse")
      {
        cmd.CommandText = @"SELECT * FROM products WHERE category_id=@categoryId SORT BY brand ASC;";
      }
      else
      {
        cmd.CommandText = @"SELECT * FROM products WHERE category_id=@categoryId";
      }

      MySqlParameter categoryIdParameter = new MySqlParameter();
      categoryIdParameter.ParameterName = "@categoryId";
      categoryIdParameter.Value = this._id;
      cmd.Parameters.Add(categoryIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int productId = 0;
      int categoryId = 0;
      string brand = "";
      string name = "";
      double price = 0.0;
      string description = "";
      string seller = "";
      string image = "";

      while(rdr.Read())
      {
       productId = rdr.GetInt32(0);
       categoryId = rdr.GetInt32(1);
       brand = rdr.GetString(2);
       name = rdr.GetString(3);
       price = rdr.GetDouble(4);
       description = rdr.GetString(5);
       seller = rdr.GetString(6);
       image = rdr.GetString(7);
       var product = new Product(categoryId,brand,name,price,description,seller,image,productId);
       productsForThisCategory.Add(product);
      }
      return productsForThisCategory;
    }
   }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Item
  {
    private int _id;
    private int _productId;
    private string _size;
    private string _color;
    private bool _available;

    public Item(string size, string color, int productId, bool available = true, int id = 0)
    {
      _id = id;
      _productId = productId;
      _size = size;
      _color = color;
      _available = available;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetProductId()
    {
      return _productId;
    }

    public string GetSize()
    {
      return _size;
    }

    public string GetColor()
    {
      return _color;
    }

    public bool IsAvailable()
    {
      return _available;
    }

    public override bool Equals(Object otherItem)
    {
      if(!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;

        bool idEquality = _id == newItem.GetId();
        bool productIdEquality = _productId == newItem.GetProductId();
        bool sizeEquality = _size == newItem.GetSize();
        bool colorEquality = _color == newItem.GetColor();
        bool availableEquality = _available == newItem.IsAvailable();

        return (idEquality && productIdEquality && sizeEquality && colorEquality && availableEquality);
      }
    }
    public override int GetHashCode()
    {
      return _id.GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (product_id, size, color, available) VALUES (@productId, @size, @color, @available);";

      MySqlParameter productId = new MySqlParameter();
      productId.ParameterName = "@productId";
      productId.Value = _productId;
      cmd.Parameters.Add(productId);

      MySqlParameter size = new MySqlParameter();
      size.ParameterName = "@size";
      size.Value = _size;
      cmd.Parameters.Add(size);

      MySqlParameter color = new MySqlParameter();
      color.ParameterName = "@color";
      color.Value = _color;
      cmd.Parameters.Add(color);

      MySqlParameter available = new MySqlParameter();
      available.ParameterName = "@available";
      available.Value = _available;
      cmd.Parameters.Add(available);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public Product GetProductInfo()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM products WHERE id = @productId;";

      MySqlParameter productId = new MySqlParameter();
      productId.ParameterName = "@productId";
      productId.Value = _productId;
      cmd.Parameters.Add(productId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int categoryId = 0;
      string brand = "";
      double price = 0.0;
      string description = "";
      string seller = "";
      string image = "";
      int id = 0;

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        categoryId = rdr.GetInt32(1);
        brand = rdr.GetString(2);
        price = rdr.GetDouble(3);
        description = rdr.GetString(4);
        seller = rdr.GetString(5);
        image = rdr.GetString(6);
      }
      if(conn != null)
      {
        conn.Dispose();
      }
      Product itemProduct = new Product(categoryId, brand, price, description, seller, image, id);
      return itemProduct;
    }

    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items WHERE id = @thisId;";

      MySqlParameter itemId = new MySqlParameter();
      itemId.ParameterName = "@thisId";
      itemId.Value = id;
      cmd.Parameters.Add(itemId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int readId = 0;
      int productId = 0;
      string size = "";
      string color = "";
      bool available = false;

      while(rdr.Read())
      {
        readId = rdr.GetInt32(0);
        productId = rdr.GetInt32(1);
        size = rdr.GetString(2);
        color = rdr.GetString(3);
        available = rdr.GetBoolean(4);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      Item foundItem = new Item(size, color, productId, available, readId);
      return foundItem;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";

      MySqlParameter itemId = new MySqlParameter();
      itemId.ParameterName = "@thisId";
      itemId.Value = _id;
      cmd.Parameters.Add(itemId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int productId = rdr.GetInt32(1);
        string size = rdr.GetString(2);
        string color = rdr.GetString(3);
        bool available = rdr.GetBoolean(4);

        Item newItem = new Item(size, color, productId, available, id);
        allItems.Add(newItem);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
  }
}

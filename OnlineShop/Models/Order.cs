using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Order
  {
    private int _id;
    private int _buyerId;
    private DateTime _checkoutDate;
    private bool _purchased;


    public Order(int buyerId, DateTime checkoutDate = default(DateTime), bool purchased = false, int id = 0)
    {
      _id = id;
      _buyerId = buyerId;
      _checkoutDate = checkoutDate;
      _purchased = purchased;
    }

    public int GetId()
    {
      return _id;
    }

    public DateTime GetCheckoutDate()
    {
      return _checkoutDate;
    }

    public int GetBuyerId()
    {
      return _buyerId;
    }

    public bool IsPurchased()
    {
      return _purchased;
    }

    public override bool Equals(Object otherOrder)
    {
      if(!(otherOrder is Order))
      {
        return false;
      }
      else
      {
        Order newOrder = (Order) otherOrder;

        bool idEquality = _id == newOrder.GetId();
        bool buyerIdEquality = _buyerId == newOrder.GetBuyerId();
        bool purchased = _purchased == newOrder.IsPurchased();


        return (idEquality  && purchased && buyerIdEquality);
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
      cmd.CommandText = @"INSERT INTO orders (buyer_id, checkout_date, purchased) VALUES (@buyerId, @checkoutDate, @purchased);";

      MySqlParameter buyerId = new MySqlParameter();
      buyerId.ParameterName = "@buyerId";
      buyerId.Value = _buyerId;
      cmd.Parameters.Add(buyerId);

      MySqlParameter checkoutDate = new MySqlParameter();
      checkoutDate.ParameterName = "@checkoutDate";
      checkoutDate.Value = _checkoutDate;
      cmd.Parameters.Add(checkoutDate);

      MySqlParameter purchased = new MySqlParameter();
      purchased.ParameterName = "@purchased";
      purchased.Value = _purchased;
      cmd.Parameters.Add(purchased);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddItem(int itemId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"INSERT INTO items_orders (order_id, item_id) VALUES (@orderId, @itemId);";

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@orderId";
      orderId.Value = _id;
      cmd.Parameters.Add(orderId);



      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@itemId";
      itemIdParameter.Value = itemId;
      cmd.Parameters.Add(itemIdParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Item> GetItems()
    {
      List<Item> orderItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT items.* FROM orders
      JOIN items_orders ON (orders.id = items_orders.order_id)
      JOIN items ON (items_orders.item_id = items.id)
      WHERE orders.id = @thisId;";

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@thisId";
      orderId.Value = _id;
      cmd.Parameters.Add(orderId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int productId = rdr.GetInt32(1);
        string size = rdr.GetString(2);
        string color = rdr.GetString(3);
        bool available = rdr.GetBoolean(4);

        Item newItem = new Item(size, color, productId, available, id);
        orderItems.Add(newItem);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return orderItems;
    }

    public void RemoveItem(int itemId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"DELETE FROM items_orders WHERE item_id = @itemId AND order_id = @orderId;";

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@orderId";
      orderId.Value = _id;
      cmd.Parameters.Add(orderId);

      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@itemId";
      itemIdParameter.Value = itemId;
      cmd.Parameters.Add(itemIdParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void RemoveAllItems()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"DELETE FROM items_orders WHERE order_id = @orderId;";

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@orderId";
      orderId.Value = _id;
      cmd.Parameters.Add(orderId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public double GetSubtotal()
    {
      double subTotal = 0;
      List<Item> myItems = GetItems();
      foreach(var item in myItems)
      {
        subTotal += item.GetProductInfo().GetPrice();
      }
      return subTotal;
    }

    public double GetTax()
    {
      return GetSubtotal()/9.5;
    }

    public double GetShipping()
    {
      double shippingcost = 0;
      List<Item> myItems = GetItems();
      foreach(var item in myItems)
      {
        shippingcost += 2;
      }
      return shippingcost;
    }
    public double GetGrandTotal()
    {
      return GetSubtotal() + GetTax() + GetShipping();
    }
    public void Purchase()
    {
      DateTime now = DateTime.Now;
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE orders SET purchased = @purchased, checkout_date = @checkoutDate WHERE id = @thisId;";

      MySqlParameter purchased = new MySqlParameter();
      purchased.ParameterName = "@purchased";
      purchased.Value = _purchased = true;
      cmd.Parameters.Add(purchased);

      MySqlParameter checkoutDate = new MySqlParameter();
      checkoutDate.ParameterName = "@checkoutDate";
      checkoutDate.Value = _checkoutDate = now;
      cmd.Parameters.Add(checkoutDate);

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@thisId";
      orderId.Value = _id;
      cmd.Parameters.Add(orderId);

      cmd.ExecuteNonQuery();
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Order Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM orders WHERE id = @thisId;";

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@thisId";
      orderId.Value = id;
      cmd.Parameters.Add(orderId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int readId = 0;
      int buyerId = 0;
      DateTime checkoutDate = default(DateTime);
      bool purchased = false;

      while(rdr.Read())
      {
        readId = rdr.GetInt32(0);
        buyerId = rdr.GetInt32(1);
        checkoutDate = rdr.GetDateTime(2);
        purchased = rdr.GetBoolean(3);
      }

      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
      Order foundOrder = new Order(buyerId, checkoutDate, purchased, id);
      return foundOrder;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM orders WHERE id = @thisId;";

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@thisId";
      orderId.Value = _id;
      cmd.Parameters.Add(orderId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      RemoveAllItems();
    }

    public static List<Order> GetAll()
    {
      List<Order> allOrders = new List<Order> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM orders;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int readId = rdr.GetInt32(0);
        int buyerId = rdr.GetInt32(1);
        DateTime checkoutDate = rdr.GetDateTime(2);
        bool purchased = rdr.GetBoolean(3);

        Order newOrder = new Order(buyerId, checkoutDate, purchased, readId);
        allOrders.Add(newOrder);
      }

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allOrders;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM orders; DELETE FROM items_orders;";

      cmd.ExecuteNonQuery();
      conn.Close();


      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}

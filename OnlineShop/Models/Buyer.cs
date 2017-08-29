using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Buyer
  {
    private int _id;
    private string _username;
    private string _phone;
    private string _email;
    private string _password;
    private string _creditCard;

    public Buyer(string username, string phone, string email, string password, string creditCard, int id = 0)
    {
      _id = id;
      _username = username;
      _phone = phone;
      _email = email;
      _password = password;
      _creditCard = creditCard;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetUsername()
    {
      return _username;
    }
    public string GetPhone()
    {
      return _phone;
    }
    public string GetEmail()
    {
      return _email;
    }
    public string GetPassword()
    {
      return _password;
    }
    public string GetCreditCard()
    {
      return _creditCard;
    }

    public override bool Equals(Object otherBuyer)
    {
      if(!(otherBuyer is Buyer))
      {
        return false;
      }
      else
      {
        Buyer newBuyer = (Buyer) otherBuyer;

        bool idEquality = _id == newBuyer.GetId();
        bool usernameEquality = _username == newBuyer.GetUsername();
        bool phoneEquality = _phone == newBuyer.GetPhone();
        bool emailEquality = _email == newBuyer.GetEmail();
        bool passwordEquality = _password == newBuyer.GetPassword();
        bool creditCardEquality = _creditCard == newBuyer.GetCreditCard();


        return (idEquality  && usernameEquality && phoneEquality && emailEquality && passwordEquality && creditCardEquality);
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
      cmd.CommandText = @"INSERT INTO buyers (user_name, phone, email, password, credit_card) VALUES (@username, @phone, @email, @password, @creditCard);";

      MySqlParameter username = new MySqlParameter();
      username.ParameterName = "@username";
      username.Value = _username;
      cmd.Parameters.Add(username);

      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "@phone";
      phone.Value = _phone;
      cmd.Parameters.Add(phone);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@email";
      email.Value = _email;
      cmd.Parameters.Add(email);

      MySqlParameter password = new MySqlParameter();
      password.ParameterName = "@password";
      password.Value = _password;
      cmd.Parameters.Add(password);

      MySqlParameter creditCard = new MySqlParameter();
      creditCard.ParameterName = "@creditCard";
      creditCard.Value = _creditCard;
      cmd.Parameters.Add(creditCard);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Order> GetOrderHistory()
    {
      List<Order> orderHistory = new List<Order> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM orders WHERE buyer_id = @buyerId AND purchased = @purchased;";

      MySqlParameter buyerIdParameter = new MySqlParameter();
      buyerIdParameter.ParameterName = "@buyerId";
      buyerIdParameter.Value = _id;
      cmd.Parameters.Add(buyerIdParameter);

      MySqlParameter purchasedParamater = new MySqlParameter();
      purchasedParamater.ParameterName = "@purchased";
      purchasedParamater.Value = true;
      cmd.Parameters.Add(purchasedParamater);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int readId = rdr.GetInt32(0);
        int buyerId = rdr.GetInt32(1);
        DateTime checkoutDate = rdr.GetDateTime(2);
        bool purchased = rdr.GetBoolean(3);

        Order newOrder = new Order(buyerId, checkoutDate, purchased, readId);
        orderHistory.Add(newOrder);
      }

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return orderHistory;
    }

    public static Buyer Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM buyers WHERE id = @thisId;";

      MySqlParameter orderId = new MySqlParameter();
      orderId.ParameterName = "@thisId";
      orderId.Value = id;
      cmd.Parameters.Add(orderId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int readId = 0;
      string username = "";
      string phone = "";
      string email = "";
      string password = "";
      string creditCard = "";

      while(rdr.Read())
      {
        readId = rdr.GetInt32(0);
        username = rdr.GetString(1);
        phone = rdr.GetString(2);
        email = rdr.GetString(3);
        password = rdr.GetString(4);
        creditCard = rdr.GetString(5);
      }

      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
      Buyer foundBuyer = new Buyer(username, phone, email, password, creditCard, id);
      return foundBuyer;
    }

    public Order GetCurrentOrder()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM orders WHERE buyer_id = @thisId AND purchased = @purchased;";

      MySqlParameter buyerIdParameter = new MySqlParameter();
      buyerIdParameter.ParameterName = "@thisId";
      buyerIdParameter.Value = _id;
      cmd.Parameters.Add(buyerIdParameter);

      MySqlParameter purchasedIdParameter = new MySqlParameter();
      purchasedIdParameter.ParameterName = "@purchased";
      purchasedIdParameter.Value = false;
      cmd.Parameters.Add(purchasedIdParameter);

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

      Order foundOrder = new Order(buyerId, checkoutDate, purchased, readId);
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
      return foundOrder;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM buyers WHERE id = @thisId;";

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

    public static List<Buyer> GetAll()
    {
      List<Buyer> allBuyers = new List<Buyer> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM buyers;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int readId = rdr.GetInt32(0);
        string username = rdr.GetString(1);
        string phone = rdr.GetString(2);
        string email = rdr.GetString(3);
        string password = rdr.GetString(4);
        string creditCard = rdr.GetString(5);

        Buyer newBuyer = new Buyer(username, phone, email, password, creditCard, readId);
        allBuyers.Add(newBuyer);
      }

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allBuyers;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM buyers;";

      cmd.ExecuteNonQuery();
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}

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

      List<Order> buyerOrders = GetAllOrders();
      List<Address> buyerAddresses = GetAddresses();

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      foreach(var order in buyerOrders)
      {
        order.Delete();
      }

      foreach(var address in buyerAddresses)
      {
        address.Delete();
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

    public List<Order> GetAllOrders()
    {
      List<Order> allOrders = new List<Order> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM orders WHERE buyer_id = @buyerId;";

      MySqlParameter buyerIdParameter = new MySqlParameter();
      buyerIdParameter.ParameterName = "@buyerId";
      buyerIdParameter.Value = _id;
      cmd.Parameters.Add(buyerIdParameter);

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

    public List<Address> GetAddresses()
    {
      List<Address> buyerAddresses = new List<Address>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM addresses WHERE buyer_id = @thisId;";

      MySqlParameter buyerIdParameter = new MySqlParameter();
      buyerIdParameter.ParameterName = "@thisId";
      buyerIdParameter.Value = _id;
      cmd.Parameters.Add(buyerIdParameter);

      var rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int buyerId = rdr.GetInt32(1);
        string name = rdr.GetString(2);
        string street = rdr.GetString(3);
        string city = rdr.GetString(4);
        string state = rdr.GetString(5);
        string country = rdr.GetString(6);
        string zip = rdr.GetString(7);
        Address newAddress = new Address(buyerId,name,street,city,state,country,zip,id);
        buyerAddresses.Add(newAddress);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return buyerAddresses;
    }

    public void Update(string newUsername, string newPhone, string newEmail, string newPassword, string newCreditCard)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE buyers SET user_name = @newUsername, password = @newPassword, phone = @newPhone,email = @newEmail, credit_card = @newCreditCard WHERE id = @thisId;";

      MySqlParameter usernameParameter = new MySqlParameter();
      usernameParameter.ParameterName = "@newUsername";
      usernameParameter.Value = newUsername;
      cmd.Parameters.Add(usernameParameter);

      MySqlParameter phoneParameter = new MySqlParameter();
      phoneParameter.ParameterName = "@newPhone";
      phoneParameter.Value = newPhone;
      cmd.Parameters.Add(phoneParameter);

      MySqlParameter emailParameter = new MySqlParameter();
      emailParameter.ParameterName = "@newEmail";
      emailParameter.Value = newEmail;
      cmd.Parameters.Add(emailParameter);

      MySqlParameter passwordParameter = new MySqlParameter();
      passwordParameter.ParameterName = "@newPassword";
      passwordParameter.Value = newPassword;
      cmd.Parameters.Add(passwordParameter);

      MySqlParameter creditCardParameter = new MySqlParameter();
      creditCardParameter.ParameterName = "@newCreditCard";
      creditCardParameter.Value = newCreditCard;
      cmd.Parameters.Add(creditCardParameter);

      MySqlParameter buyerIdParameter = new MySqlParameter();
      buyerIdParameter.ParameterName = "@thisId";
      buyerIdParameter.Value = _id;
      cmd.Parameters.Add(buyerIdParameter);

      _username = newUsername;
      _phone = newPhone;
      _email = newEmail;
      _password = newPassword;
      _creditCard = newCreditCard;

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

    }

    public static Buyer TryLogin(string username, string password)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM buyers WHERE user_name = @username AND password = @password;";

      MySqlParameter usernameParameter = new MySqlParameter();
      usernameParameter.ParameterName = "@username";
      usernameParameter.Value = username;
      cmd.Parameters.Add(usernameParameter);

      MySqlParameter passwordParameter = new MySqlParameter();
      passwordParameter.ParameterName = "@password";
      passwordParameter.Value = password;
      cmd.Parameters.Add(passwordParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int readId = 0;
      string readUsername = "";
      string phone = "";
      string email = "";
      string readPassword = "";
      string creditCard = "";

      while(rdr.Read())
      {
        readId = rdr.GetInt32(0);
        readUsername = rdr.GetString(1);
        phone = rdr.GetString(2);
        email = rdr.GetString(3);
        readPassword = rdr.GetString(4);
        creditCard = rdr.GetString(5);
      }

      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
      Buyer foundBuyer = new Buyer(readUsername, phone, email, readPassword, creditCard, readId);
      return foundBuyer;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM buyers; DELETE FROM orders; DELETE FROM items_orders; DELETE FROM addresses;";

      cmd.ExecuteNonQuery();
      conn.Close();

      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}

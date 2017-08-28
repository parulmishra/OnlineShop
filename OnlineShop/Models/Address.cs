using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Address
  {
    private int _id;
    private int _buyer_id;
    private string _street;
    private string _city;
    private string _state;
    private string _country;
    private string _zip;
    public Address(int buyerId,string street, string city, string state, string country, string zip, int id=0)
    {
      _buyer_id = buyerId;
      _street = street;
      _city = city;
      _state = state;
      _country = country;
      _zip = zip;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetBuyerId()
    {
      return _buyer_id;
    }
    public string GetStreet()
    {
      return _street;
    }
    public string GetCity()
    {
      return _city;
    }
    public string GetState()
    {
      return _state;
    }
    public string GetCountry()
    {
      return _country;
    }
    public string GetZip()
    {
      return _zip;
    }
    public override bool Equals(Object otherAddress)
    {
      if (!(otherAddress is Address))
      {
        return false;
      }
      else
      {
        Address newAddress = (Address) otherAddress;
        bool idEquality = newAddress.GetId() == this._id;
        bool buyerIdEquality = newAddress.GetBuyerId() == this._buyer_id;
        bool streetEquality = newAddress.GetStreet() == this._street;
        bool cityEquality = newAddress.GetCity() == this._city;
        bool countryEquality = newAddress.GetCountry() == this._country;
        bool zipEquality = newAddress.GetZip() == this._zip;
        return (idEquality && buyerIdEquality && streetEquality && cityEquality && countryEquality && zipEquality);
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
      cmd.CommandText = @"INSERT INTO addresses(buyer_id,street,city,state,country,zip) VALUES (@buyer_id,@street,@city,@state,@country,@zip);";

      MySqlParameter buyerIdParameter = new MySqlParameter();
      buyerIdParameter.ParameterName = "@buyer_id";
      buyerIdParameter.Value = _buyer_id;
      cmd.Parameters.Add(buyerIdParameter);

      MySqlParameter streetParameter = new MySqlParameter();
      streetParameter.ParameterName = "@street";
      streetParameter.Value = _street;
      cmd.Parameters.Add(streetParameter);

      MySqlParameter cityParameter = new MySqlParameter();
      cityParameter.ParameterName = "@city";
      cityParameter.Value = _city;
      cmd.Parameters.Add(cityParameter);

      MySqlParameter stateParameter = new MySqlParameter();
      stateParameter.ParameterName = "@state";
      stateParameter.Value = _state;
      cmd.Parameters.Add(stateParameter);

      MySqlParameter countryParameter = new MySqlParameter();
      countryParameter.ParameterName = "@country";
      countryParameter.Value = _country;
      cmd.Parameters.Add(countryParameter);

      MySqlParameter zipParameter = new MySqlParameter();
      zipParameter.ParameterName = "@zip";
      zipParameter.Value = _zip;
      cmd.Parameters.Add(zipParameter);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Address> GetAll()
    {
      List<Address> allAddresses = new List<Address>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM addresses;";

      var rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int buyerId = rdr.GetInt32(1);
        string street = rdr.GetString(2);
        string city = rdr.GetString(3);
        string state = rdr.GetString(4);
        string country = rdr.GetString(5);
        string zip = rdr.GetString(6);
        Address newAddress = new Address(buyerId,street,city,state,country,zip,id);
        allAddresses.Add(newAddress);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAddresses;
    }
    public void Delete()
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand(@"DELETE FROM addresses WHERE id=@thisId;");

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Address Find(int id)
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM addresses WHERE id=@thisId";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;
      cmd.Parameters.Add(idParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int addressId = 0;
      int buyerId = 0;
      string street = "";
      string city = "";
      string state = "";
      string country = "";
      string zip = "";
      while(rdr.Read())
      {
        addressId = rdr.GetInt32(0);
        buyerId = rdr.GetInt32(1);
        street = rdr.GetString(2);
        city = rdr.GetString(3);
        state = rdr.GetString(4);
        country = rdr.GetString(5);
        zip = rdr.GetString(6);
      }
      var address = new Address(buyerId,street,city,state,country,zip,id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return address;
    }
  }
}

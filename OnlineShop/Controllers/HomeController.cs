using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using System.Collections.Generic;
using System;
namespace OnlineShop.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/populate")]
    public ActionResult IndexPopulate()
    {
      Category.DeleteAll();
      Buyer.DeleteAll();

      Populate.PopulateDatabase();
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer buyer = Buyer.Find(-1);
      List<Product> featured = Product.GetAll();

      Random rnd = new Random();
      List<Product> newFeatured = new List<Product>(){};
      if(featured.Count > 0)
      {
        for(var i = 0; i < 4; i++)
        {
          newFeatured.Add(featured[rnd.Next(0, (featured.Count - 1))]);
        }
        var featuredProduct = featured[rnd.Next(0, (featured.Count - 1))];
        model.Add("featured", featuredProduct);
      }

      model.Add("categories", Category.GetAll());
      model.Add("buyer", buyer);
      model.Add("products", newFeatured);

      return View("Index", model);
    }
    [HttpGet("/{id}")]
    public ActionResult Index(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer buyer = Buyer.Find(id);
      List<Product> featured = Product.GetAll();

      Random rnd = new Random();
      List<Product> newFeatured = new List<Product>(){};
      if(featured.Count > 0)
      {
        for(var i = 0; i < 4; i++)
        {
          newFeatured.Add(featured[rnd.Next(0, (featured.Count - 1))]);
        }
        var featuredProduct = featured[rnd.Next(0, (featured.Count - 1))];
        model.Add("featured", featuredProduct);
      }

      model.Add("categories", Category.GetAll());
      model.Add("buyer", buyer);
      model.Add("products", newFeatured);

      return View("Index",model);
    }
    [HttpGet("/products/details")]
    public ActionResult ProductDetails()
    {
        return View();
    }
    [HttpGet("/category")]
    public ActionResult CategoryView()
    {
        return View("Category");
    }
    [HttpGet("/buyerform/add")]
    public ActionResult BuyerForm()
    {
        return View();
    }
    [HttpGet("/cart/{id}")]
    public ActionResult Cart(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer selectedBuyer = Buyer.Find(id);
      Order buyerOrder = selectedBuyer.GetCurrentOrder();
      List<Item> buyerItems = buyerOrder.GetItems();

      model.Add("order", buyerOrder);
      model.Add("items", buyerItems);
      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);

      return View(model);
    }

    [HttpPost("/cart/{id}/removed")]
    public ActionResult CartSingleRemove(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer selectedBuyer = Buyer.Find(id);
      Order buyerOrder = selectedBuyer.GetCurrentOrder();


      selectedBuyer.GetCurrentOrder().RemoveItem(int.Parse(Request.Form["item-id"]));
      List<Item> buyerItems = buyerOrder.GetItems();

      model.Add("order", buyerOrder);
      model.Add("items", buyerItems);
      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);


      return View("Cart", model);
    }

    [HttpPost("/cart/{id}/removed/all")]
    public ActionResult CartRemoveAll(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer selectedBuyer = Buyer.Find(id);
      Order buyerOrder = selectedBuyer.GetCurrentOrder();


      selectedBuyer.GetCurrentOrder().RemoveAllItems();
      List<Item> buyerItems = buyerOrder.GetItems();

      model.Add("order", buyerOrder);
      model.Add("items", buyerItems);
      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);


      return View("Cart", model);
    }

    [HttpPost("/order/{id}")]
    public ActionResult Order(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer selectedBuyer = Buyer.Find(id);
      Order buyerOrder = selectedBuyer.GetCurrentOrder();

      List<Item> buyerItems = buyerOrder.GetItems();

      model.Add("order", buyerOrder);
      model.Add("items", buyerItems);
      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);


      return View(model);
    }

    [HttpGet("/login")]
    public ActionResult LogIn()
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};
      Buyer selectedBuyer = Buyer.Find(0);
      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);
      model.Add("login", " ");

      return View(model);
    }

    [HttpPost("/index/login")]
    public ActionResult IndexLoggedIn(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};
      string buyerPassword = Request.Form["buyer-password"];
      string buyerName = Request.Form["buyer-name"];

      Buyer selectedBuyer = Buyer.TryLogin(buyerName,buyerPassword);

      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);
      Console.WriteLine(selectedBuyer.GetId());
      //try login
      if (selectedBuyer.GetId() == 0)
      {
        string loginFail = "User name/password not found";
        model.Add("login", loginFail);
        return View("login",model);
      }
      else{ // return to index logged in
        Random rnd = new Random();
        List<Product> featured = Product.GetAll();
        List<Product> newFeatured = new List<Product>(){};
        if(featured.Count > 0)
        {
          for(var i = 0; i < 4; i++)
          {
            newFeatured.Add(featured[rnd.Next(0, (featured.Count - 1))]);
          }
          var featuredProduct = featured[rnd.Next(0, (featured.Count - 1))];
          model.Add("featured", featuredProduct);
        }
        model.Add("products", newFeatured);
        return View("Index", model);
      }

    }

    [HttpGet("/detail")]
    public ActionResult BuyerDetail()
    {
        return View();
    }
  }
}

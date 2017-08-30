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
      Populate.PopulateDatabase();
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer buyer = Buyer.Find(0);
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
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer buyer = Buyer.Find(0);
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

      return View(model);
    }
    [HttpGet("/categories/{id}")]
    public ActionResult CategoryView(int id)
    {
      Dictionary<string,object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      List<Product> allProducts = Product.GetAll();
      Buyer newBuyer = Buyer.Find(0);
      Model.Add("categories", allCategories);
      Model.Add("selectedCategory", Category.Find(id));
      Model.Add("products",allProducts);
      Model.Add("buyer",newBuyer);
      return View("Category",Model);
    }
    [HttpGet("/products/details/{id}")]
    public ActionResult ProductDetails(int id)
    {
      Dictionary<string, object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      Model.Add("categories", allCategories);
      Buyer newBuyer = Buyer.Find(0);
      Model.Add("buyer",newBuyer);
      Product newProduct = Product.Find(id);
      Model.Add("product",newProduct);
      Model.Add("items",newProduct.GetItems());
      return View(Model);
    }

    [HttpGet("/buyerform/add")]
    public ActionResult BuyerForm()
    {
        return View();
    }
    [HttpPost("/buyerform/add")]
    public ActionResult BuyerFormAdd()
    {
      string name = Request.Form["buyer-name"];
      string password = Request.Form["buyer-password"];
      string phone = Request.Form["buyer-phone"];
      string email = Request.Form["buyer-email"];
      string cardNumber = Request.Form["buyer-card-number"];
      string addressType = Request.Form["buyer-address-type"];
      string street = Request.Form["buyer-address-street"];
      string city = Request.Form["buyer-address-city"];
      string state = Request.Form["buyer-address-state"];
      string country = Request.Form["buyer-address-country"];
      string zip = Request.Form["buyer-address-zip"];
      Buyer newBuyer = new Buyer(name,phone,email,password,cardNumber,0);
      newBuyer.Save();
      Address newAddress = new Address(newBuyer.GetId(),addressType,street,city,state,country,zip,0);
      newAddress.Save();
      return View("BuyerDetail");
    }
    [HttpGet("/cart")]
    public ActionResult Cart()
    {
        return View();
    }
    [HttpGet("/order")]
    public ActionResult Order()
    {
        return View();
    }
    [HttpGet("/login")]
    public ActionResult LogIn()
    {
        return View();
    }
    [HttpGet("/detail")]
    public ActionResult BuyerDetail()
    {
        return View();
    }
  }
}

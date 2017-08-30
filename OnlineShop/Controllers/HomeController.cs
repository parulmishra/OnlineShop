using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using System.Collections.Generic;
using System;
namespace OnlineShop.Controllers
{
  public class HomeController : Controller
  {
    [HttpPost("/populate")]
    public ActionResult IndexPopulate()
    {
      Populate.PopulateDatabase();
      Dictionary<string, object> model = new Dictionary<string,object>{};

      Buyer buyer = Buyer.Find(0);
      List<Product> featured = Product.GetAll();

      Random rnd = new Random();
      List<Product> newFeatured = new List<Product>(){};
      for(var i = 0; i < 4; i++)
      {
        newFeatured.Add(featured[rnd.Next(0, (featured.Count - 1))]);
      }

      model.Add("categories", Category.GetAll());
      model.Add("buyer", buyer);
      model.Add("products", newFeatured);

      return View(model);
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

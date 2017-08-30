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
    [HttpGet("/category/{id}")]
    public ActionResult CategoryView(int id)
    {
      Dictionary<string,object> Model = new Dictionary<string,object>();
      Category newCategory = Category.Find(id);
      Model["category"] = newCategory;
      Model["products"] = newCategory.GetProducts();
      return View("Category",Model);
    }
    [HttpGet("/products/details/{id}")]
    public ActionResult ProductDetails(int id)
    {
      Dictionary<string, object> Model = new Dictionary<string,object>();
      Product newProduct = Product.Find(id);
      Model["product"] = newProduct;
      Model["items"] = newProduct.GetItems();
      return View(Model);
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

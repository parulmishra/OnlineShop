using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using System.Collections.Generic;
using System;
namespace OnlineShop.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
        return View();
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

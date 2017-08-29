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
    [HttpGet("/products/details")]
    public ActionResult ProductDetails()
    {
        return View();
    }
    [HttpGet("/category")]
    public ActionResult Category()
    {
        return View();
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

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

    }

  }

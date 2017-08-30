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
    [HttpGet("/categories/{catId}/{buyerId}")]
    public ActionResult CategoryView(int catId, int buyerId)
    {
      Dictionary<string,object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      List<Product> allProducts = Product.GetAll();
      Buyer newBuyer = Buyer.Find(buyerId);
      Model.Add("categories", allCategories);
      Model.Add("selectedCategory", Category.Find(catId));
      Model.Add("products",allProducts);
      Model.Add("buyer",newBuyer);
      return View("Category",Model);
    }
    [HttpGet("/products/details/{prodId}/{buyerId}")]
    public ActionResult ProductDetails(int prodId, int buyerId)
    {
      Dictionary<string, object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      Model.Add("categories", allCategories);
      Buyer newBuyer = Buyer.Find(buyerId);
      Model.Add("buyer",newBuyer);
      Product newProduct = Product.Find(prodId);
      Model.Add("product",newProduct);
      Model.Add("items",newProduct.GetItems());
      return View(Model);
    }

    [HttpGet("/buyerform/add")]
    public ActionResult BuyerForm()
    {
      Dictionary<string, object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      Model.Add("categories",allCategories);
      return View(Model);
    }
    [HttpPost("/buyerdetails")]
    public ActionResult BuyerFormAdd()
    {
      Dictionary<string, object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      List<Address> addresses = new List<Address>();
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
      addresses.Add(newAddress);
      Model.Add("addresses",addresses);
      Model.Add("buyer",newBuyer);
      Model.Add("categories",allCategories);
      return View("BuyerDetail",Model);
    }
    [HttpGet("/buyers/update/{id}")]
	  public ActionResult UpdateBuyerInfoForm(int id)
	  {
      Dictionary<string,object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      List<Product> allProducts = Product.GetAll();
      Buyer newBuyer = Buyer.Find(id);
      List<Order> OrdersForThisBuyer = newBuyer.GetOrderHistory();
      Model.Add("categories", allCategories);
      Model.Add("buyer",newBuyer);
      Model.Add("products",allProducts);
      Model.Add("orders",OrdersForThisBuyer);
      return View("UpdateBuyerInfo",Model);
	  }
    [HttpPost("/buyers/update/{id}")]
	  public ActionResult UpdateBuyerInfo(int id)
	  {
      Dictionary<string, object> Model = new Dictionary<string,object>();
      List<Category> allCategories = Category.GetAll();
      Buyer newBuyer = Buyer.Find(id);
      string name = Request.Form["buyer-name"];
      string password = Request.Form["buyer-password"];
      string phone = Request.Form["buyer-phone"];
      string email = Request.Form["buyer-email"];
      string cardNumber = Request.Form["buyer-card-number"];

      newBuyer.Update(name,phone,email,password,cardNumber);
      Model.Add("categories", allCategories);
      Model.Add("buyer",newBuyer);
      return View("BuyerDetail",Model);
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
    public ActionResult OrderPost(int id)
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


      return View("Order", model);
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

    [HttpGet("/updateBuyerInfo/{id}")]
    public ActionResult updateBuyerInfo(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};
      Buyer selectedBuyer = Buyer.Find(id);
      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);

      return View("updateBuyerInfo", model);
    }

    [HttpPost("/receipt/{orderId}/{buyerId}")]
    public ActionResult Receipt(int orderId, int buyerId)
    {
      Dictionary<string, object> model = new Dictionary<string,object>{};
      Buyer selectedBuyer = Buyer.Find(buyerId);
      // model.Add("products", buyerProducts);
      model.Add("categories", Category.GetAll());
      model.Add("buyer", selectedBuyer);
      model.Add("order", Order.Find(orderId));
      model.Add("address", Address.Find(int.Parse(Request.Form["address-select"])));
      Order.Find(orderId).Purchase();

      return View(model);
    }
  }
}

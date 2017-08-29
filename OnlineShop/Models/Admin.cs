using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Admin
  {
    public static void PopulateDatabase()
    {
      // Setting up first category with buyer
      Category newCategory = new Category("Shirts");
      shirts.Save();

      Product newProduct = new Product(shirts.GetId(),"Tommy Bahama","Polo",49.99,"The best Tommy has to offer","Bahama Inc.","http://www.pngmart.com/files/3/Polo-Shirt-PNG-Image.png");
      tommyBahamaPolo.Save();

      Item newItem = new Item("Small", "Red", newProduct.GetId());
      newItem.Save();

      Item newItem2 = new Item("Large", "Blue", newProduct.GetId());
      newItem2.Save();

      Item newItem3 = new Item("Medium", "Black", newProduct.GetId());
      newItem3.Save();

      Item newItem4 = new Item("Small", "Green", newProduct.GetId());
      newItem4.Save();

      Buyer newBuyer = new Buyer("MarkCuban1992", "425-214-2222", "mcuban@gmail.com", "cubanrocks", "4444-5555-3222-6666");
      newBuyer.Save();

      Address newAddress = new Address(newBuyer.GetId(),"Home","401 Seneca St","Seattle","WA","USA","98052");
      newAddress.Save();

      Order newOrder = new Order(newBuyer.GetId(),("1999-04-20"),false);
      newOrder.Save()

      newOrder.AddItem(newItem);
      newOrder.AddItem(newItem2);

      Order newOrderPurchased = new Order(newBuyer.GetId(),("1992-04-20"),true);
      newOrderPurchased.Save();

      newOrderPurchased.AddItem(newItem3);
      newOrderPurchased.AddItem(newItem4);

      // Setting up remaining categories/products

      Category pants = new Category("Pants");
      pants.Save();

      Product pantsOne = new Product(pants.GetId(),"Levi's","Jeans",65.00,"Expensive ","Bahama Inc.","http://www.pngmart.com/files/3/Polo-Shirt-PNG-Image.png");
      tommyBahamaPolo.Save();
      Product pantsTwo = new Product(pants.GetId(),"Tommy Bahama","Polo",49.99,"The best Tommy has to offer","Bahama Inc.","http://www.pngmart.com/files/3/Polo-Shirt-PNG-Image.png");
      tommyBahamaPolo.Save();
      Product pantsThree = new Product(pants.GetId(),"Tommy Bahama","Polo",49.99,"The best Tommy has to offer","Bahama Inc.","http://www.pngmart.com/files/3/Polo-Shirt-PNG-Image.png");
      tommyBahamaPolo.Save();


      Category socks = new Category("Socks");
      socks.Save();



      Category shoes = new Category("Shoes");
      shoes.Save();


      Category hats = new Category("Hats");
      hats.Save();



      Category jackets = new Category("Jackets");
      jackets.Save();





    }

  }
}

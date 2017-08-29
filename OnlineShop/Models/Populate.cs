using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OnlineShop.Models
{
  public class Populate
  {
    public Populate()
    {
    }
    public static void PopulateDatabase()
    {
      // Setting up first category with buyer
      Category shirts = new Category("Shirts");
      shirts.Save();

      Product shirtsOne = new Product(shirts.GetId(),"Tommy Bahama","Polo",49.99,"The best Tommy has to offer","Bahama Inc.","http://www.pngmart.com/files/3/Polo-Shirt-PNG-Image.png");
      shirtsOne.Save();
      Product shirtsTwo = new Product(shirts.GetId(),"GAP","Button up",49.99,"Work work","GAP","https://cdn.shopify.com/s/files/1/0169/5130/products/tradlands_stow_lake_featured_button_up_shirts_for_women_grande.png?v=1499358188");
      shirtsTwo.Save();
      Product shirtsThree = new Product(shirts.GetId(),"Scott Vintage","T-shirt",49.99,"Vintage","Nordstrom","http://pngimg.com/uploads/tshirt/tshirt_PNG5436.png");
      shirtsThree.Save();


      Item newItem = new Item("Small", "Red", shirtsOne.GetId());
      newItem.Save();

      Item newItem2 = new Item("Large", "Blue", shirtsOne.GetId());
      newItem2.Save();

      Item newItem3 = new Item("Medium", "Black", shirtsOne.GetId());
      newItem3.Save();

      Item newItem4 = new Item("Small", "Green", shirtsOne.GetId());
      newItem4.Save();

      Buyer newBuyer = new Buyer("MarkCuban1992", "425-214-2222", "mcuban@gmail.com", "cubanrocks", "4444-5555-3222-6666");
      newBuyer.Save();

      Address newAddress = new Address(newBuyer.GetId(),"Home","401 Seneca St","Seattle","WA","USA","98052");
      newAddress.Save();

      Order newOrder = new Order(newBuyer.GetId(),(DateTime.Now),false);
      newOrder.Save();

      newOrder.AddItem(newItem.GetId());
      newOrder.AddItem(newItem2.GetId());

      Order newOrderPurchased = new Order(newBuyer.GetId(),(DateTime.Now),true);
      newOrderPurchased.Save();

      newOrderPurchased.AddItem(newItem3.GetId());
      newOrderPurchased.AddItem(newItem4.GetId());

      // Setting up remaining categories/products

      Category pants = new Category("Pants");
      pants.Save();

      Product pantsOne = new Product(pants.GetId(),"Levi's","Jeans",65.00,"Expensive jeans","Banana Republic","http://m.footaction.com/images/products/zoom/50102261_z.jpg");
      pantsOne.Save();
      Product pantsTwo = new Product(pants.GetId(),"American Eagle","Khakis",39.99,"These pants have wings!","Macy's","http://www.pngmart.com/files/3/Polo-Shirt-PNG-Image.png");
      pantsTwo.Save();
      Product pantsThree = new Product(pants.GetId(),"Sleazy's Slacks","Slacks",19.99,"The starter pair","Men's Warehouse","https://cdn.shopify.com/s/files/1/0305/5853/products/0990-17_wi_cttn_poly_dress_khaki-pl-nvy.png?v=1488473457");
      pantsThree.Save();


      Category socks = new Category("Socks");
      socks.Save();

      Product socksOne = new Product(socks.GetId(),"Gold Toe","Gym socks",7.00,"When you're on the run!","Forever 21","https://www.goldtoe.com/assets/img/catalog/product/large/711-101S-041-S.png");
      socksOne.Save();
      Product socksTwo = new Product(socks.GetId(),"Knee Toppers","Tube socks",5.00,"Keepin you warm at night","Buckle","https://cdn3.volusion.com/rkfzh.yaufw/v/vspfiles/photos/25-007-WHT-2.gif?1385472336");
      socksTwo.Save();
      Product socksThree = new Product(socks.GetId(),"Silly Socks","Polka dot socks",6.00,"When your feet are partying!","Zumies","http://www.underwearexpert.com/wp-content/uploads/2014/06/bd01-608_m.png");
      socksThree.Save();


      Category shoes = new Category("Shoes");
      shoes.Save();

      Product shoesOne = new Product(shoes.GetId(),"Reebok","Gym shoes",45.50,"When you're on the run!","Forever 21","http://hometraininguae.com/images/sportclothes/running%20shoes.png");
      shoesOne.Save();
      Product shoesTwo = new Product(shoes.GetId(),"Hikers","Hiking boots",65.00,"Tread those trails!","REI","http://pngimg.com/uploads/boots/boots_PNG7802.png");
      shoesTwo.Save();
      Product shoesThree = new Product(shoes.GetId(),"Comfeet","Slippers",9.00,"Keepin' it warm!","Payless","http://images.lifeisgood.com/Mens-Slipper-Slides_43517_2_lg.png");
      shoesThree.Save();


      Category hats = new Category("Hats");
      hats.Save();

      Product hatsOne = new Product(hats.GetId(),"MLB","Mariner's cap",13.99,"Team spirit!","The Sports Club","http://www.sportszoneelite.com/media/catalog/product/cache/1/image/940x738/9df78eab33525d08d6e5fb8d27136e95/5/3/531062104-NEW_ERA_9FIFTY_SEATTLE_MARINERS_CUSTOM-00.png");
      hatsOne.Save();
      Product hatsTwo = new Product(hats.GetId(),"Fancy Fedoras","Fedora",17.79,"Live in your basement!","Fred Meyer","http://www.pngmart.com/files/3/Fedora-PNG-Photos.png");
      hatsTwo.Save();
      Product hatsThree = new Product(hats.GetId(),"Cowboys Inc.","Cowboy hat",29.00,"Wrangle those cows!","Suit Supply","http://www.pngpix.com/wp-content/uploads/2016/07/PNGPIX-COM-Cowboy-Hat-PNG-Transparent-Image-2.png");
      hatsThree.Save();


      Category jackets = new Category("Jackets");
      jackets.Save();

      Product jacketsOne = new Product(jackets.GetId(),"EMP","Classic leather jacket",63.99,"Smoke those cigarretes!","Amazon","http://cdn.acfrg.com/i/__fit/ACfrG/productpics_fullsize/1/130109a-emp.png");
      jacketsOne.Save();
      Product jacketsTwo = new Product(jackets.GetId(),"Marker","Winter jacket",50.00,"Cut the cold!","Sports Authority","http://22867-presscdn.pagely.netdna-cdn.com/wp-content/uploads/2014/11/marker-pumphouse-jacket-2015-640x640.png");
      jacketsTwo.Save();
      Product jacketsThree = new Product(jackets.GetId(),"Vapor","Windbreaker",29.59,"For when it's a little chilly out!","Big 5","http://www.condoroutdoor.com/images/products/detail/10617_018F_2015.2.png");
      jacketsThree.Save();

    }
  }
}

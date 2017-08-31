# Online Clothing Store

#### C# Team Week Project, 8/28/2017

#### By _Ben Budinger, Jonathan Stein, Jesse Bryan, Parul Mishra_

## Description

_This site uses a SQL database to track the purcashes of clothing items in an online store_

* Buyers can register on the site and create a profile
* Buyers can log into the website
* Buyers can edit their user details
* Buyers can view their purchase history
* Buyers can search products by category
* Buyers can search products via search bar
* Buyers can add product items to their cart
* Buyers can view their cart
* Buyers can remove all items from their cart
* Buyers can remove single items from their cart
* Buyers can purchase the items in their cart
* Buyers can add multiple shipping addresses to their profile

### Website Flow

| Location | Action | Result |
|-|-|-|
| Header | Click user profile button | View user's purchase history |
| Header | Click category drop-down button | View specific category item listing |
| Header | Click search button with search text | List of products with search in description |
| Header | Click Cart (not logged in) | Register / Login Form |
| Header | Click Cart (logged in ) | Cart details page |
| Log In | User information submitted | Log user in |
| Register | User information submitted | Create new user |
| Index | Click on featured item | Product details page |
| Categories | Click on sort price | Sorts product list based on price |
| Categories | Click on product | Product details page |
| Product Details | Click Add to Cart | Add item to cart, return cart view |
| Cart | Initial View | List of items in cart with subtotal |
| Cart | Click Empty Cart | Remove all items in cart |
| Cart | Click Remove Item | Removes single item from cart |
| Cart | Click Buy | Purchase confirmation page |
| Buy | Initial View | Address, credit card, shipping fields, list of items in cart, subtotal |
| Buy | Click Confirm | Sets item in DB to purchased |

![](/schema.png)


## Setup

## To populate the database with products, go to /populate. This will clear the database and insert content into all tables.


* This website will be hosted on GitHub
* https://github.com/parulmishra/OnlineShop

##### Database Creation/Setup - Commands
CREATE DATABASE IF NOT EXISTS `online_shop` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `online_shop`;

CREATE TABLE `addresses` (
  `id` int(11) NOT NULL,
  `buyer_id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `street` varchar(255) NOT NULL,
  `city` varchar(255) NOT NULL,
  `state` varchar(255) NOT NULL,
  `country` varchar(255) NOT NULL,
  `zip` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `buyers` (
  `id` int(11) NOT NULL,
  `user_name` varchar(255) NOT NULL,
  `phone` varchar(10) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(10) NOT NULL,
  `credit_card` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `categories` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `size` varchar(255) NOT NULL,
  `color` varchar(255) NOT NULL,
  `available` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `items_orders` (
  `id` int(11) NOT NULL,
  `order_id` int(11) NOT NULL,
  `item_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;


CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `buyer_id` int(11) NOT NULL,
  `checkout_date` datetime NOT NULL,
  `purchased` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `products` (
  `id` int(11) NOT NULL,
  `category_id` int(11) NOT NULL,
  `brand` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `price` double(11,2) NOT NULL,
  `description` text NOT NULL,
  `seller` varchar(255) NOT NULL,
  `image` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

* The DB online_shop_test is used for MST Unit testing

## Technologies Used

* HTML
* CSS
* Bootstrap
* C#
* Razor
* MySQL

### License

* Copyright (c) 2017 Ben Budinger, Jonathan Stein, Jesse Bryan, Parul Mishra.
* This software is licensed under the MIT license.

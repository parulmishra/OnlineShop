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


## Setup

* This website will be hosted on GitHub
* https://github.com/parulmishra/OnlineShop

<!-- ##### Database Creation/Setup - Commands
1. CREATE DATABASE online_shop;
2. USE band_tracker;
3. CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR (255));
4. CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR (255));
5. CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id int, venue_id int);

* The DB band_tracker_test is used for MST Unit testing -->

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

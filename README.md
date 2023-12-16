# Web Store Website

## Overview

Welcome to WebStore website a dynamic platform designed to seamlessly connect sellers and customers.
Sellers showcase their products, and customers can explore, purchase, and provide valuable reviews.

## Features

### User Types

- **Sellers**
- **Customers**

### Customer Features

1. **Browse and Purchase Products:**
   - Customers can securely explore a diverse range of products and make purchases effortlessly.

2. **Provide Reviews:**
   - Share your thoughts by leaving reviews for purchased products, contributing to the community feedback.

3. **Manage Account:**
   - Keep your account details up-to-date for a personalized and accurate experience.

### Seller Features

1. **Manage Products:**
   - Sellers can effortlessly add new products and update existing inventory items.

2. **View Sales and Reviews:**
   - Access sales data and customer reviews to make informed decisions about product performance.

3. **Update Store Details:**
   - Customize store-related information to create a personalized and inviting storefront.

### Shared Features

- **Responsive User Interface:**
  - Our application boasts a responsive design, ensuring a seamless experience across various devices.

- **User Authentication and Authorization:**
  - Securely log in, with authorization tailored to grant access to relevant functionalities based on user type.

- **Account Management:**
  - All users can easily manage their accounts, fostering a transparent and personalized user experience.

## Product Features

1. **Best Rated Product Carousel:**
   - Discover top-rated products showcased prominently on the home page.

2. **Category Slider:**
   - Explore categories effortlessly on the home page. Clicking on a category reveals products of that category below.

3. **Product Card with Image Gallery:**
   - On the product page, find a product card with an image gallery featuring thumbnails. Clicking on a thumbnail displays the corresponding image in a larger view.

4. **Related Products by Category Slider:**
   - Explore related products within the same category through a convenient slider on the product page.

## Technologies Used

- **Frontend:** React.js, Bootstrap
- **Backend:** ASP.NET Core, Entity Framework
- **Database:** Microsoft SQL Server

## Getting Started

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Rom943/WebStoreProject-.NetCore-ReactJs.git
   cd WebStoreProject-.NetCore-ReactJs
   
## Backend Setup:

1. Navigate to the /WebShopFullStackProject directory.
2. Configure the database connection in appsettings.json.
3. Run migrations: dotnet ef database update
4. Start the backend server: dotnet run
   
## Frontend Setup:

1. Navigate to the /UI React directory.
2. Install dependencies: npm install
3. Start the frontend application: npm start
4. Access the Application:

 Open your web browser and visit http://localhost:3000 to experience the application.

# Usage:
## User:
Customers and sellers are both associated with the User table in a one-to-one relationship.
During registration, users can select their user type (customer, seller).
After submission, based on the user type selection, additional details are required:
Sellers need to add store names and upload store profile images.
Customers need to add shipping addresses.
Upon logging in, the website loads user-specific data and functionalities.
Click the user icon on the top-left corner to access the user profile.
User details can be modified in the user profile tab by clicking the "Edit User Profile" button.
To log out, click on log-out.

## Sellers:
Edit store details in the user profile tab.
Add or edit products in the user bar => user activity tab => Product Stock tab.
Access sales data: user activity tab => Sold Product tab.
Access product reviews: user activity tab => Rated Products tab.
Sellers cannot purchase or review products.

## Customers:
Access product reviews: user activity tab => Products Reviewed tab.
Access purchase history: user activity tab => Purchases tab.
Make purchases: The product stock quantity decreases after each purchase.
Add products to the cart: Products can be purchased or removed from the cart.
Leave reviews: On the product page, leave a review and rate the product from 0 to 5 stars. The product rank will be recalculated after each review.


## hope you injoy this project as much as i building it.
## email for contact: roma19943@gmail.com

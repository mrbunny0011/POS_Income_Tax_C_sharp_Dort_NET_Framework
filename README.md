# 💵 POS Income Tax Management System – C# .NET Framework

A powerful and user-friendly Point of Sale (POS) Management System designed for retail businesses. Built using **C# (.NET Framework)** and **SQL Server**, this system supports product categorization, role-based access, real-time stock tracking, and income tax calculations.

---

## 🔑 Login Roles & Permissions

| Role   | Access Level |
|--------|--------------|
| **Admin** | Full access to all features, including user management, sales records, and reporting. |
| **Casher** | Limited access — can make sales but cannot view user details or sales reports. |

🛡️ While adding a user, the admin defines the **role** (Admin or Casher).

---

## 📦 Product Structure

Products are organized in **three hierarchical levels**:

1. **Category** – e.g., Laptop, Mobile  
2. **Subcategory** – e.g., Infinix, Apple, Samsung  
3. **Model** – e.g., iPhone 15, iPhone 15 Pro Max

This flexible structure allows easy browsing and filtering of products.

---

## 📈 Core Features

### 🛒 Sales Management
- Sell multiple products in a single transaction
- Calculate:
  - **Total Amount**
  - **Tax Amount**
  - **Net Amount** (Total + Tax)
- Tax can be added/updated manually for each product.

### 👤 Customer Management
- Add, update, and manage customer profiles
- View purchase history for each customer
- Track when and what a customer purchased

### 👨‍💼 User Management
- Add new users with role: Admin or Casher
- Restrict access based on role (e.g., cashers cannot view user or sales details)

### 📦 Stock Tracking
- Real-time stock updates with every sale
- Monitor remaining stock quantity
- Update stock when products are restocked

### 📊 Sales Details & Reporting
- View which products were sold
- See which user (admin/casher) made the sale
- Track date, time, and quantity of each transaction

---

## 🛠️ Technologies Used

| Technology         | Description                  |
|--------------------|------------------------------|
| **C# (.NET Framework)** | For building the desktop application |
| **Windows Forms**       | GUI for POS interaction              |
| **SQL Server**          | Backend database for all records     |
| **ADO.NET**             | Database connectivity and transactions |

---

## 📂 Database Structure Highlights

- **Categories Table**
- **Subcategories Table**
- **Models Table**
- **Users Table** (with role field)
- **Customers Table**
- **Sales Table** (linked to products, customers, and users)
  

> Full SQL script available in `/Data Base/POS_Income_Tax_Script.sql`

---

## 📸 Screenshots

>Screenshots are avilable in Screenshot_of_System Folder

## 📌 Summary

This POS Income Tax Management System is designed for businesses that sell products like mobiles and laptops. It supports full product categorization (category → subcategory → model), tracks stock levels, and calculates income tax per product.

The system includes:
- Role-based login (Admin & Casher)
- Full sales tracking with tax details
- Customer management and purchase history
- Manual and dynamic tax input per item
- Real-time stock updates after each sale

Ideal for electronics stores or small businesses wanting a tax-ready POS system.

👨‍💻 Developer Info
👤 Name: Abdul Basit
📧 Email: basitbunny.222@gmail.com
🔗 GitHub: github.com/mrbunny0011

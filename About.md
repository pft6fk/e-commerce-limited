This is project spec that will implement DDD and TDD, also help me to work with client applicatoin (react) from scratch. 

This will be:

* Business requirements
* Functional requirements
* Domain model definitions
* Rules & invariants
* Non-functional requirements
* PostgreSQL notes
* Sample seed data

This is your **official spec** for the E-Commerce Limited system.

---

# 🛒 PROJECT: E-Commerce Limited

## 🎯 Goal

Build a modular monolith e-commerce system that:

* Manages products
* Manages customers
* Processes orders
* Handles payments
* Manages inventory
* Provides reporting dashboard

Architecture:

* ASP.NET Core
* Clean Architecture
* DDD tactical patterns
* EF Core
* PostgreSQL
* TDD mandatory

---

# 1️⃣ Functional Requirements

## 1. Product Catalog

System must allow:

* Create product
* Update product price
* Activate / deactivate product
* View product list
* View product details

Constraints:

* Product name required
* Price must be >= 0
* Cannot deactivate product used in active orders
* Inactive product cannot be added to order

---

## 2. Customer Management

System must allow:

* Register customer
* View customer orders
* View customer lifetime value

Constraints:

* Email must be unique
* Email format must be valid

---

## 3. Order Management

System must allow:

* Create order for customer
* Add items to order
* Remove items from order
* Pay order
* Cancel order
* Ship order
* Complete order

---

### Order Lifecycle

States:

* Pending
* Paid
* Shipped
* Completed
* Cancelled

Valid transitions:

| From    | To        |
| ------- | --------- |
| Pending | Paid      |
| Pending | Cancelled |
| Paid    | Shipped   |
| Shipped | Completed |

Invalid transitions must throw DomainException.

---

## 4. Inventory Management

System must:

* Track available quantity per product
* Prevent selling more than available
* Reduce stock when order is paid
* Increase stock if order is cancelled after payment (optional advanced)

Must use optimistic concurrency.

---

## 5. Payments

System must:

* Create payment record when order is paid
* Ensure payment amount equals order total
* Allow only one successful payment per order

---

## 6. Reporting

System must provide:

* Top 5 selling products
* Monthly revenue
* Orders grouped by status
* Average order value
* Customer lifetime value
* Low stock products

Must use advanced LINQ (GroupBy, Join, Projection).

---

# 2️⃣ Domain Model Definitions

---

# 🟡 ORDERING CONTEXT

## Aggregate Root: Order

Properties:

* OrderId Id
* CustomerId CustomerId
* OrderStatus Status
* IReadOnlyCollection<OrderItem> Items
* DateTime CreatedAt
* Money TotalPrice (calculated)

Rules:

* Cannot add item if status != Pending
* Quantity must be > 0
* Cannot pay if no items
* Cannot cancel if shipped
* TotalPrice is sum of items
* Must raise OrderPaidDomainEvent
* Must raise OrderShippedDomainEvent

---

## Entity: OrderItem

Properties:

* OrderItemId Id
* ProductId ProductId
* int Quantity
* Money UnitPrice

Rules:

* Quantity > 0
* UnitPrice >= 0

Belongs only to Order aggregate.

---

# 🟢 CATALOG CONTEXT

## Aggregate Root: Product

Properties:

* ProductId Id
* string Name
* string Description
* Money Price
* bool IsActive

Rules:

* Name required
* Price >= 0
* Cannot deactivate if used in active order

---

# 🔵 INVENTORY CONTEXT

## Aggregate Root: StockItem

Properties:

* ProductId Id
* int AvailableQuantity
* byte[] RowVersion (Postgres concurrency token)

Rules:

* Cannot reduce below zero
* IncreaseStock(int qty)
* ReduceStock(int qty)
* Must use optimistic concurrency

---

# 🟣 PAYMENTS CONTEXT

## Aggregate Root: Payment

Properties:

* PaymentId Id
* OrderId OrderId
* Money Amount
* PaymentStatus Status
* DateTime ProcessedAt

Rules:

* Amount must equal Order.TotalPrice
* Cannot process twice
* Must raise PaymentCompletedDomainEvent

---

# 🟤 CUSTOMER CONTEXT

## Aggregate Root: Customer

Properties:

* CustomerId Id
* string FirstName
* string LastName
* string Email
* DateTime RegisteredAt

Rules:

* Email required
* Email unique

---

# 🟡 VALUE OBJECTS

## Money

Properties:

* decimal Amount
* string Currency

Rules:

* Immutable
* Amount >= 0
* Same currency required for operations
* Equality by value

---

## Strongly Typed IDs

Implement:

* OrderId
* ProductId
* CustomerId
* PaymentId
* OrderItemId

Each wraps Guid.

---

# 🟠 DOMAIN EVENTS

Implement:

* OrderPaidDomainEvent
* OrderShippedDomainEvent
* PaymentCompletedDomainEvent
* StockReducedDomainEvent

---

# 3️⃣ PostgreSQL Requirements

You must configure:

* UUID as primary keys
* RowVersion using xmin OR bytea column
* Decimal precision for Money (18,2)
* Index on:

  * CustomerId in Orders
  * ProductId in OrderItems
  * OrderStatus
  * Email in Customers (unique)
  * CreatedAt for reporting

---

# 4️⃣ Application Layer Requirements

Commands:

* CreateProduct
* RegisterCustomer
* CreateOrder
* AddItemToOrder
* PayOrder
* CancelOrder
* ShipOrder

Queries:

* GetProducts
* GetOrderById
* GetOrdersByCustomer
* GetTopSellingProducts
* GetMonthlyRevenue

Must use:

* MediatR OR custom mediator
* Validation layer
* DTOs
* Pagination

---

# 5️⃣ Non-Functional Requirements

* Domain must not reference EF
* Repositories defined in Domain
* EF configuration via Fluent API
* No public setters in aggregates
* At least 80% domain test coverage
* Global exception middleware
* Structured logging
* Docker compose for PostgreSQL

---

# 6️⃣ Sample Seed Data (You Can Use This)

### Products

| Id | Name                | Price | Currency | Active |
| -- | ------------------- | ----- | -------- | ------ |
| 1  | Laptop Pro 15       | 1500  | USD      | true   |
| 2  | Mechanical Keyboard | 120   | USD      | true   |
| 3  | Gaming Mouse        | 80    | USD      | true   |
| 4  | 4K Monitor          | 600   | USD      | true   |
| 5  | USB-C Hub           | 50    | USD      | true   |

---

### Customers

| Id | Name        | Email                                   |
| -- | ----------- | --------------------------------------- |
| 1  | John Doe    | [john@demo.com](mailto:john@demo.com)   |
| 2  | Alice Smith | [alice@demo.com](mailto:alice@demo.com) |

---

### Inventory

| Product  | Quantity |
| -------- | -------- |
| Laptop   | 20       |
| Keyboard | 100      |
| Mouse    | 150      |
| Monitor  | 30       |
| Hub      | 200      |

---

### Example Orders

Order 1:

* Customer: John
* 1 Laptop
* 2 Mouse
* Status: Paid

Order 2:

* Customer: Alice
* 1 Monitor
* Status: Pending

---

# 7️⃣ Advanced LINQ Requirements

You must implement:

* GroupBy month
* Revenue sum
* Join Orders + OrderItems + Products
* Projection to DTO
* AsNoTracking
* Compiled queries
* Pagination

---

# 8️⃣ Mandatory Demonstration Tasks

You must demonstrate:

* Difference between IEnumerable and IQueryable
* Deferred execution
* Multiple enumeration problem
* Why exposing List breaks aggregate
* Optimistic concurrency exception handling
* Transaction boundary during PayOrder

---

# 🎯 After Finishing You Must Be Able To Explain

* Aggregate boundary
* Why OrderItem is not AggregateRoot
* Why repository returns aggregate, not IQueryable
* Why ValueObject is immutable
* Why domain events are raised before SaveChanges
* How Postgres handles concurrency
* How EF translates GroupBy

---

This project, if built correctly, will absolutely fix your interview feedback.

---

Next step:

Do you want:

1. Exact folder structure?
2. Database schema diagram?
3. First TDD test to write?
4. Step-by-step build order?

Choose one.

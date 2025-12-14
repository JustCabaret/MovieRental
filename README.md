# MovieRental Exercise - Solutions by Hugo Cabaret

---

## Questions & Answers

### 1. Startup Error Fix

**Question:** The app is throwing an error when we start, please help us. Also, tell us what caused the issue.

**Answer:** The application was throwing an `InvalidOperationException` with the message "Cannot consume scoped service from singleton". The problem was that `IRentalFeatures` was registered as Singleton but depends on `MovieRentalDbContext`, which is Scoped by default. This violates the DI lifetime rule where a service cannot depend on another service with a shorter lifetime. I changed the registration from `AddSingleton` to `AddScoped` to fix this.

---

### 2. Async/Await Implementation

**Question:** The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?

**Answer:** I converted the `Save` method to `SaveAsync` using async/await. The difference is that synchronous methods block the thread while waiting for database operations, wasting thread pool resources. Asynchronous methods release the thread back to the pool during I/O operations, allowing it to handle other requests, which improves scalability in web APIs, especially under high load.

---

### 3. Filter Rentals by Customer Name

**Question:** Please finish the method to filter rentals by customer name, and add the new endpoint.

**Answer:** I implemented `GetRentalsByCustomerNameAsync` using Entity Framework's async methods with `.Include()` to load related entities (Customer and Movie), avoiding N+1 query problems. The method filters by customer name and returns results asynchronously. I added a GET endpoint at `/Rental/customer/{customerName}` to expose this functionality.

---

### 4. Customer Entity & Database Normalization

**Question:** We noticed we do not have a table for customers, it is not good to have just the customer name in the rental. Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!

**Answer:** I created a `Customer` entity with navigation properties for the one-to-many relationship (one customer can have many rentals). The `Rental` entity was updated to remove the `CustomerName` string and add `CustomerId` as a foreign key plus a `Customer` navigation property. This eliminates data duplication and makes it easier to extend customer information later without changing the Rental table.

---

### 5. MovieFeatures GetAll Method Review

**Question:** In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.

**Answer:** The `GetAll` method works but doesn't handle exceptions, which is now covered by the global exception handler middleware. It could be improved by making it async with `ToListAsync()` to avoid blocking threads. If Movie has navigation properties, using `.Include()` would prevent N+1 queries. The return type could also be `IEnumerable<Movie>` instead of `List<Movie>` for better encapsulation.

---

### 6. Exception Handling Strategy

**Question:** No exceptions are being caught in this api, how would you deal with these exceptions?

**Answer:** I implemented a global exception handling middleware that intercepts all unhandled exceptions in the pipeline. This provides centralized error management with consistent error responses across endpoints. The middleware logs exceptions and returns formatted JSON responses with appropriate HTTP status codes. This removes the need for try-catch blocks in every controller action and follows the DRY principle.

---

### 7. Challenge - Payment Processing with Strategy Pattern

**Question:** We need to implement a new feature in the system that supports automatic payment processing with multiple payment providers. The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.

**Answer:** I implemented the Strategy Pattern for payment providers. Created an `IPaymentProvider` interface that all providers implement. A `PaymentProviderFactory` class instantiates the correct provider based on the payment method from the rental. This follows the Open/Closed Principle from SOLID, new payment providers can be added by creating a new class implementing the interface and adding a case to the factory, without modifying existing code.

---

## Original Exercise Description

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

 * The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
 * The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
 * Please finish the method to filter rentals by customer name, and add the new endpoint.
 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!
 * In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
 * No exceptions are being caught in this api, how would you deal with these exceptions?


### Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.

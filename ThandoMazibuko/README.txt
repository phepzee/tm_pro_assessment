1. The architecture
   Clean Architecture which includes the following layers
	-  Presentation (API controllers)
	-  Application (business logic), 
	-  Domain (core business entities and logic),
	-  Infrastructure (external concerns like databases or third-party services)
2. Motivation for adopting Clean Architecture is using the following key principles
	a. Separation of Concerns
		Each layer should have a clear and distinct responsibility.
		Changes in one layer should not necessitate changes in others.
	b. Dependency Injection
		Use DI (Dependency Injection) to manage dependencies and promote loose coupling.
		Allows for easier testing and swapping of implementations.
	c. RESTful Principles
		Use HTTP methods (GET, POST, PUT, DELETE) and status codes (200 OK, 404 Not Found, etc.) correctly.
		Follow resource-oriented URLs (e.g., /api/products).
	d. Security
		Implement authentication and authorization mechanisms (e.g., JWT tokens, OAuth).
		Secure sensitive data and API endpoints.
	e. Scalability and Performance
		Design for horizontal scalability (e.g., stateless services, caching).
		Optimize performance through efficient data access and response handling.
   Components and Patterns	
	b. Repository Pattern
		Abstracts data access and provides a consistent interface for data operations.
		Promotes testability and separation of concerns.
   Database
	a) MSSQL - Is a gold standard in the context of EF.
        b) EF Core complements this with its migration tools, designer support, and integration with development environment

3. Steps to run the application and testing with Swagger and Postman
	a) Start/Run the application and Swagger UI file will open in a web browser.
	b) You should see a user friendly interface that displays API documentation and allows you to test your API endpoints.
	c) The interface is split into two Authentication and Customers Feedback
	   - Authorization
		- Operations
			- POST: Login		: Click on this operation, next click Try it out button and then click execute button. The Token will display below
			- POST: Registration: Click on this operation to register a user, next click Try it out button and in the API Description complete the fields inside the curly braces and type Admin or User for role. Then click execute button
		- Customers Feedback
			- Operations
				- GET : GetCustomersFeedback : Click on this operation to get the customers feedback, next click Try it out button, then execute button. Only Admin users can see the feedback in Postman
				- POST: AddCustomerFeedback  : Click on this operation to create customer feedback, next click Try it out and in the API Description complete the fields inside the curly braces and click execute. Below you will see the added customer 								        feedback
							                 : You should recieve an email if you're registered as an Admin
	d) Postman
		- Download postman
		- Launch Postman from where it's installed
		- Once Postman launched, select Import in the sidebar.
		- Create a GET request by pasting Swagger URL: https://localhost:7073/api/CustomersFeedback/GetCustomersFeedback
		- Send the request by clicking the Send button.
		- In the Body section you should not see the customers feedback
		- In the Authorization Section paste the Token, you got when login in Swagger with Admin login details
		- Send the request again by clicking the Send button
		- In the Body section you should now be able to see the customers feedback






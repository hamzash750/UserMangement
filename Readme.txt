
UserManagement.API
•	Contains All Routing (API Controller and Action)
•	UserController You can see demo with swager with other two you can test with postman 
•	First Create Token With OAuth2 Process File Show in Image Also 
UserManagement.Business
•	Contains Database Logic Using Interface Service Help of Dependency Injection 
•	User Service and Interface 
•	In Service User LINQ Query to get data using context
UserManagement.Common
•	Use Some Common Function for Pagination Logic
UserManagement.Model
•	All Database Table (Model)
UserManagement.OAuth
•	Related To Token Service 
UserManagement.OAuthServer
•	OAuth2 Server for Creation and verification for token 
UserManagement.Repository
•	Use For Db Context Class 
•	Migrations
UserManagement.ViewModel
•	User View Model For api response 
1 Use Code First Approach 
o	Need To Change Connection string in UserManagement.API Project
o	Add Some Dummy Data with User and User Address
o	Run Commands to create and database and insert data
o	Add-Migration <Databaseinitialization> any name can use 
o	Update-Database <CreateDatabase>any name can use 
2 Logger 
o	Change nlog.config log file currently my e: drive set you can set your own nlog.config file
o	After Change the file and build you will see the log information in log file and when you call any api 
o	you will get text file all the interaction 

3 Create swagger api documentation when project run swagger will run 
•	Postman Collection Attach
4 Select Multi startup project 
•	First OAuthServer 
•	Second API Project
Status Code
•	For Success 200 will come with data
•	For Bad Request 400 will come
•	For Not Found Will come in exception 

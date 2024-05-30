Develop a following WEB API project with Clean Architecture 

1. Implement a simple Signup Api (~/users/signup) , which shall accept following fields save inside the database using Dapper mini ORM
Request : 
{
	"username": "waseem@gmail.com",
	"password": "dfdffdfd",
	"firstname": "Waseem",
	"lastname": "Muhammad",
	"device": "12fdr112233",
	"ipaddress": "172.24.1.56"
}
Response : 200 OK 


2.Implement a authenticate Api (~/users/authenticate) ,Using custom JWT (JSON Web Token) authentication using a .NET Core 6.0/7.0 API , Login time , IP addresses , other possible data should be safe automatically in the database . when user signed-in successfully *first time* there sould be a 5 GBP balance added automatically as a gift .

Request : 
{
	"username" : "waseem@gmail.com",
	"password" : "dfdffdfd",
	"ipaddress" : "172.23.5.67",
	"device" : "12fdr112233",
	"browser" : "chrome"
}

Response : 

{
	"firstname" : "Waseem",
	"lastname" : "Muhammad",
	"token" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}

3.Implement a Balance Api (~/users/auth/balance)

Request : 
{
	"token" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}

Response : 

{
	"balance" : 5.0
}

4. Create user interface for this APIs base on React Framework .
5. Please structure code with better architecture / comment the code accordingly 

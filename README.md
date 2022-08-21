# MoviesDB
API with .NET6, GraphQL, Hot Chocolate, Entity Framework, MSSQL, JWT Authentication &amp; Authorization and Serilog


This API simulates a movie database with simple CRUD functionalities. Since its a GraphQL API it requires queries and mutations in order to access the data required.
Some examples will be provided at the end of this file.

#### Instructions to get the project working locally :

- Restore database on a local SQL Server using the moviedb_script.sql provided in this repo
- Change "MovieDBConnection" in appsettings.Development.json to your own connection string to the database.
- Run the project and perform a POST call to https://localhost:7275/api/token/GenerateToken . This will generate a JWT Token which is needed for all requests
 
 There are two roles - Admin and User. Admin has access to all functionalities while User has only read (get) functionality.
 
 In order to get an Admin role send the following request -
  ```
    {"email":"admin@abc.com","password":"!!admin@x_321"}
  ```  
 For a user role, send the following request -
 ```
    {"email":"johndoe@abc.com","password":"%john811.ttt$"}
 ```
These details can be changed by providing different values in the database, although do note that the password is HMACSHA256 hashed in the database.

Once the request is sent, a JWT with a 1 year expiry will be provided (the length of the token is for testing purposes)

#### Sample JWT Token with Admin rights and expiry date of 21 August 2023
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIwMTY5MTU5NS0wM2Q1LTQ0MDAtOTQ2NC1mOGQzYmYyOTVhOTEiLCJpYXQiOiIyMS8wOC8yMDIyIDA3OjIwOjEwIiwiVXNlcklkIjoiMSIsIkRpc3BsYXlOYW1lIjoiU3VwZXIgQWRtaW4iLCJVc2VyTmFtZSI6IlNBIiwiRW1haWwiOiJhZG1pbkBhYmMuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2OTI2MDI0MTAsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYXVkIjoiSldUU2VydmljZUNsaWVudCJ9.iwxHq77zjgVr2hHnlVU3CmMpGGqQsP_k_d-EjxyBDZ4
```

Open up the browser and load up either BananaCakePop (https://localhost:7275/bananacakepop/ui/) or Playground(https://localhost:7275/ui/playground) both of which are configured in this project

Before sending any requests make sure to insert the Authorization Header in the HTTP Headers section as a Bearer Token with the JWT token that we previously got 
```
 "Authorization":"Bearer <your token>"
 ```
 
 Once this is done, the setup is finished and you can perform any queries or mutations (only admin has access to mutations)
 
### Query Samples:
 
#### Get All movies with optional filters and sorting
---------------------------------------------
```
  query
  {
    allMovies(where: {genre:{eq:"Thriller"}} order:{rating:DESC})
    {
       id
       name
       rating
       director
       genre
       yearReleased 
    }
  }
 ```
 
 
#### Get Top5 Rated Movies (Optional Filters)
---------------------------------------------
```
query
{
  top5RatedMovies
  {
   id
   name
   rating
   director
   genre
   yearReleased    
  }
}
```
 
 
#### Get Single Movie By ID
---------------------------------------------
```
query
{
  movieByID(id:1) {
    id
    name
    rating
    genre
    yearReleased
    director
  }
}
``` 
 
 
#### Get Single Movie By Name
---------------------------------------------
```
query
{
  movieByName(name: "Shawshank Redemption") {
    id
    name
    rating
    genre
    yearReleased
    director
  }
}
```
 
 
### Mutation Samples:

#### Save New Movie
---------------------------------------------
```
mutation($newMovie:MovieInput!){
  saveMovie(newMovie:$newMovie) {
    id
  }
}
```
*variables:*
```
{
  "newMovie": {
    "id": 0,
    "name": "Space Jam",
    "genre": "Family",
    "rating": 6.5,
    "yearReleased":1996,
    "director":"Joe Pytka"
  }
}
```
 
 
#### Update Movie
---------------------------------------------
```
mutation($updateMovie:MovieInput!){
  updateMovie(updateMovie:$updateMovie) {        
  }
}
```
*variables :*
```
{
  "updateMovie": {
    "id": 40,
    "name": "Space Jam",    
    "yearReleased":1996,
    "director":"Joe Pytka",
    "genre": "Comedy",
    "rating": 7.5
  }
}
```

 
#### Delete Movie By Name
---------------------------------------------
```
mutation($name:String!){
  deleteMovieByName(name:$name)
}
```
*variables :*
```
{
  "name": "Space Jam"
}
```
 
 
#### Delete Movie By ID
---------------------------------------------
```
mutation($id:Int!){
  deleteMovieByID(id:$id)
}
```
*variables :*
```
{
  "id": 40
}
```

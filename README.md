This Project consist of

Front end - Angular https://github.com/ust-boney/DeliVeggieApp_Assessment/tree/master/DeliVeggieFrontEnd/angular-app
 - Angular application communicates with Rest API dotnet core via Application gateway and the  json response received in angular components
   
Api Gateway - https://github.com/ust-boney/DeliVeggieApp_Assessment/tree/master/DeliVeggieApp.APIGateway
 - Made use of Ocelot Api gateway Gets the response from backend dotnet core api
   
Microservice WebApi - https://github.com/ust-boney/DeliVeggieApp_Assessment/tree/master/DeliVeggieApp.WebApi
 - WebApi communicates with the RabbitMq publish and subscribe messages with the Console app
 - 2 Api calls made to get the product listing and product details
   
Dotnet console Application - https://github.com/ust-boney/DeliVeggieApp_Assessment/tree/master/VeggieAppConsole
  - Console app communicates with the RabbitMq publish and subscribe messages with the dotnet api
  - Service layer gets the data from NoSql MongoDB

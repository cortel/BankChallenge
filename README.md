# BankChallenge

Create a REST API that can generate and return a payment overview for a simplified loan calculation

# Section 1 : Solution Structure

Done

The solution is split into 3 main folders:
1. Solution Items
2. src
3. test

# 1. Solution Items:

In the solution items, general files that affect the general solution are stored.
Currently there is the editorconfig.
What can be stored here: middleware configs, docker.compose.yml, etc.

# 2. src

In the src folder, we have the main application and relevant libraries.
In the current context, there is:
Main startup project BankChallenge.API
BankChallenge.Common
BankChallenge.DataContext
BankChallenge.Services

# 3. test

The test folder is meant to hold libraries relevant purely for testing.
There is :
BankChallenge.API.Test
BankChallenge.DataContext.Test
BankChallenge.Services.Test

In Depth analysis of each project:

# BankChallenge.API

-Program.cs -> the main method here
-Startup.cs -> relevant configuration
-Appsettings.json -> config values for IConfiguration for injection
-Logs -> Created by serilog
-Infrastructure Folder -> Meant to hold classes to see easier what you inject in service collection
  (for future use if project gets bigger)
-Controllers Folder with a PaymentController - Most important
-launchSettings.json -> default settings at startup
-The only dependency that this API project must have is Services library(needed for the endpoint service)
-can have dependency on common (enums and such)

# BankChallenge.Common

-Configuration-LoanConfiguration.cs -> the POCO class meant to hold the values from appsettings.json LoanConfig Section. Used for injecting the object in services.
-GlobalSuppressions.cs -> it is a style created through stylescop to stop certain annoying warnings.

# BankChallenge.DataContext

-Entities->Payment
The Data context was meant to have a class which is further used as an entity stored in a NCache.
The idea behind this is that all the time when the endpoint is called, we can first look in the cache to see
if we already have the same combination of : configuration -> and loan/years.
The purpose is to check the cached database so we can retrieve the result from the cache instead of calculating all the time. This is a To Do, nice to have.

# BankChallenge.Services

-Models-In/Out - Payment.cs (payment out model.)
-IPaymentService and PaymentService

# BankChallenge.API.Test

-Meant to test Api calls and check if we get 200 OK status.
-To Do -> Api Test.

# BankChallenge.DataContext.Test

-Meant to test future entity mapping, Ncache retrieval.
-To Do -> Creating Ncache environment and subsequent functionality.

# BankChallenge.Services.Test

-BaseTest class for test classes which includes test initialize and test cleanup
-PaymentService.Test - meant to test PaymentService Methods, and a "half wanna be" Integration test .



# Section 2 : Solution Flow + Challenges comments.

Done

Steps:

The BankChallenge.API is the startup project. It builds up in the Program.cs and uses Startup.cs as well.
In Program.cs the host builder is created, and in there the serilog is added and json config files. The Program.cs also uses Startup, in which we configure the service collection, and Ioptions from json files. In here we add necesary things such as controller, httpclient, configuration, the IPaymentService, and LoanConfiguration.
When the project runs and the user acceses the Api Endpoint, the PaymentServiceController from BankChallenge.API calls the PaymentService.Create method from BankChallenge.Services.
Due to the nature of business logics being dynamic, it is always good to have your repos and services be injected as an interface, and subsquently specifiy its implementation. As you can see, the endpoint uses IPaymentService, and not PaymentService. Its implementation is defined at startup, thus if the implementation changes, the only thing needed is to specify what implementation to target.

Before starting this task, I read more on the accuracy of which value type is more relevant for banking operations. I chose decimals. The only issue I had was when needing to use Math.Pow, so i made a cast there.
A funny thing: the result for monthly payment meant to be 5303,28 but my result was 
5303,2757619537767110063718886. I do not know the bank's business logic, and thus i left the number how it is, without rounding it up.
I had difficulties finding the proper formulas, and read a bit on amortization. Last time I had financial Management was in my 3rd Semester as a Global Business Enginner.
All methods and api calls are meant to be handled async(those calculations can become easily very heavy)
To Do

-Implement NCache with subsequent flow: When calling the api endpoint,
first we check in the ncache if the same combination of input-configuration exists.
If exists -> ncache retrieve else ncache add and create payment.


# Section 3 : Solution Testing

Done

- PaymentServiceTest class meant to test each calculation method, and a Create test method that serves as a "wanna be" integration test.

To Do

-Api Test call.

# Section 4 : Solution Development Process - In an Ideal world.My commits will show that I do not follow it by heart.

Steps:

1. Understand the base requirements

2. Get Calculation formulas to fit input output.

3. Organized the code mentally and on paper, and understood the degree of complexity. If the architecture got complicated,I would have made some UML Diagrams , Sequence diagrams for the flow.

4. Structure the Project in clean separate C# Project libraries, depending on the responsability of each project. Better to have a clean separation of concearns.

5. Setup the project configuration and relevant dependencies. (LoanConfig, service injected, serilog added.)

6. Ensure the business logic is covered.

7. Create the Payment Out Model -> Create the Interface/Service -> Test so called Service -> Create API endpoint  that uses the service -> Create Test Api Endpoint (to do)

8. Refurbish solution, check for loopholes, add try catch to be more defensive programming (most likely a casting error might happen between double/decimal)

9. Add Extra features :
TO DO: Create an Ncache in memory db that stores endpoint calls and results. If there is the same combination of input/output , dont go through the same calculation over again, but look in Ncache and retrieve it from there.

10. Out of Scope - Further things to improve
-Implmenet circuit braker pattern for api calls, in case they fail, and are dependent of other calls.
-Can implement an Oauth, but I do not see the reason, since the data is static, and not sensitive.

# Section 5 : Published Result to live test


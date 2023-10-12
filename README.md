# GetBoxed
The GetBoxed Project was made by:
Emil Wiltmann Andreasen,
Andreas Schelle Hansen and
Jens Christian Pedersen

Installation Guide:

NOTE: if you dont want to setup the database manually run any of the unittests and it will run a rebuild script for the database
SETUP an environmental variable named pgconn with a database connection

1. cd frontend

2. npm install

3.  ng build

4.  cd ..

5.  cd API

6.  dotnet run

Guide to use UnitTests:
Make sure the api is running can be done by repeating step 5 and 6

there is two test types API tests and playwrighttests that tests the ui

for API tests simple write in the console:

cd BoxAPITest
dotnet test

For the playwrighttests go the project folder and run MAKE sure the API is not running before you get to "dotnet test"

cd PlaywrightTests

dotnet add package Microsoft.Playwright.NUnit

dotnet build

pwsh bin/Debug/net8.0/playwright.ps1 install

then use "dotnet test" to run the test

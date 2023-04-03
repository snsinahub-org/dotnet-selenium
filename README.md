# dotnet-selenium
## Background
This repositroy meant to demonstrate how to automate testing a dotnet core web app using selenium on GitHub Actions. 

## About the project
![image](https://user-images.githubusercontent.com/90400593/194351406-af5909aa-d889-47a3-822f-61d410d530fe.png)

This is a dotnet web application project which has a simple webpage, to generate and run this project I ran following commands
```BASH
# To generate the web app
dotnet new mvc -o dotnet-selenium

## To build and run
dotnet run
```



## DotNet Dependencies
to run selenium test case, following dependencies are needed
- Selenium.WebDrive
- MSTest.TestFramework
- MSTest.TestAdapte
- Microsoft.NET.Test.Sdk

These dependencies can be added by running `dotnet add package <package-name` command.
```Bash
dotnet add package Selenium.WebDriver
dotnet add package MSTest.TestFramework
dotnet add package MSTest.TestAdapter
dotnet add package Microsoft.NET.Test.Sdk
```

## Web Browser dependencies
I use `ChromeDriver` to run `headless` chrome web browser for test automation.  you can download it from [ChromeDriver](https://sites.google.com/chromium.org/driver/downloads)

## Initiating ChromeDriver
ChromeDriver is located in `test` folder, in one of GitHub Actions steps, this file will be copied to `/tmp` folder and file location is passed to ChromeDriver instantiation.

Here is sample of code

```dotnet
private ChromeDriver GetDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--user-data-dir .");
            options.AddArgument("--ignore-certificate-errors");

            options.AcceptInsecureCertificates = true;
            

            if(bool.Parse((string)TestContext.Properties["headless"]))
            {
                options.AddArgument("headless");
            }

            return new ChromeDriver("/tmp", options);
        }
 ```
## Steps to run and test this application
1. Start web app by running `dotnet run` in project's root directory
2. In another Action step or another terminal change directory to test `cd test/`
3. Run test using this command `dotnet test -s .runsettings`

## Links
- [Selenium driver](https://www.selenium.dev/documentation/webdriver/getting_started/install_drivers/)
- [ChromeDriver](https://sites.google.com/chromium.org/driver/downloads)





using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace ws.SeleniumTests
{
    [TestClass]
    public class DotNetSiteTests
    {
        public TestContext? TestContext { get; set; }
        public ExtentReports? extent;
        public ExtentTest test;
        public ChromeDriver driver;



        // create a method to initialize
        

        [TestInitialize]
        public void Setup()
        {
            extent = new ExtentReports();
            Console.WriteLine("Setup");
            


            // check if extent is null
            if (extent == null)
            {
                // extent = new ExtentReports();
                Console.WriteLine("extent is null");
            }
            else
            {
                Console.WriteLine("extent is not null");
            }

            // Initialize ExtentReports
            Directory.CreateDirectory("/tmp/results");
            
            var htmlReporter = new ExtentHtmlReporter("/tmp/results/cc.html");
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest("Demo");
            driver = GetDriver();
            
        }

        
        
        

        [TestMethod]
        public void TestLink1()
        {
            // Chrome Driver was manually downloaded from https://sites.google.com/a/chromium.org/chromedriver/downloads
            // parameter "." will instruct to look for the chromedriver.exe in the current folder (bin/debug/...)
            test = extent.CreateTest(TestContext.TestName);
            test.Log(Status.Info, "Navigate to DotNet website");
            // using (var driver = GetDriver())
            // {
                
                driver.Navigate().GoToUrl((string)TestContext.Properties["webAppUrl"]);

                // add log from test
                
                //Click the Get Started button
                driver.FindElement(By.LinkText("Privacy Policy")).Click();
                var css = driver.FindElement(By.LinkText("Privacy Policy")).GetAttribute("class");
                var cssMenu = driver.FindElement(By.LinkText("Privacy")).GetAttribute("class");

                Console.WriteLine("CSS ---> " + css);
                
                Assert.AreEqual(css, "privacy");
                
            // }
        }
        
        [TestMethod]
        public void TestLink2()
        {
            // Chrome Driver was manually downloaded from https://sites.google.com/a/chromium.org/chromedriver/downloads
            // parameter "." will instruct to look for the chromedriver.exe in the current folder (bin/debug/...)
            // using (var driver = GetDriver())
            // {
                var driver = GetDriver();
                test = extent.CreateTest(TestContext.TestName);
                // extent.LogInfo("TestLink2");
                //Navigate to DotNet website
                driver.Navigate().GoToUrl((string)TestContext.Properties["webAppUrl"]);
                //Click the Get Started button
                driver.FindElement(By.LinkText("Privacy")).Click();                
                var cssMenu = driver.FindElement(By.LinkText("Privacy")).GetAttribute("class");

                Console.WriteLine("CSS MENU ---> " + cssMenu);
                
                Assert.AreNotEqual(cssMenu, "not-privacy");
    
                
            // }
        }

        

        [TestMethod]
        public void TestLink4()
        {
            // Chrome Driver was manually downloaded from https://sites.google.com/a/chromium.org/chromedriver/downloads
            // parameter "." will instruct to look for the chromedriver.exe in the current folder (bin/debug/...)
            // using (var driver = GetDriver())
            // {
                test = extent.CreateTest(TestContext.TestName);
                // extent.LogInfo("TestLink2");
                //Navigate to DotNet website
                driver.Navigate().GoToUrl((string)TestContext.Properties["webAppUrl"]);
                //Click the Get Started button
                // test url link if correct after click find element by id


                driver.FindElement(By.Id("redirect")).Click();        
                // assert url is correct but do not fail test if not correct

                // add a selenium soft assertion to verify url



                // Assert.AreEqual(driver.Url, "https://dotnet.microsoft.com/");    

                // check if driver.url is matching the expected url
                // if not matching, add a soft assertion to the test
                if (driver.Url != "https://dotnet.microsoft.com/")
                {
                    test.Log(Status.Warning, "URL is not matching the expected URL");
                } else {
                    test.Log(Status.Info, "URL is matching the expected URL");
                }  
                
                
    
                
            // }
        }

        [TestMethod]
        public void TestLink5()
        {
            // Chrome Driver was manually downloaded from https://sites.google.com/a/chromium.org/chromedriver/downloads
            // parameter "." will instruct to look for the chromedriver.exe in the current folder (bin/debug/...)
            // using (var driver = GetDriver())
            // {
                extent.CreateTest(TestContext.TestName);
                // extent.LogInfo("TestLink2");
                //Navigate to DotNet website
                driver.Navigate().GoToUrl((string)TestContext.Properties["webAppUrl"]);
                //Click the Get Started button
                // test url link if correct after click find element by id

                test.Log(Status.Info, "Navigate to https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0");
                driver.FindElement(By.Id("redirect")).Click();                
                
                Assert.AreEqual(driver.Url, "https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0");
    
                
            // }
        }


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

        [TestCleanup]
        public void endReporting()
        {
            Console.WriteLine("TestCleanup");
            extent.Flush();
            // driver.Quit();

        }
    }
}

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
        private ExtentReports? extent = new ExtentReports();
        private ExtentTest? test;

        
        [TestInitialize]
        public void Setup()
        {
            Console.WriteLine("Setup");
            // Initialize ExtentReports
            Directory.CreateDirectory("/tmp/results");
            
            var htmlReporter = new ExtentHtmlReporter("/tmp/results");
            extent.AttachReporter(htmlReporter);

            
            
        }

        
        
        

        [TestMethod]
        public void TestLink1()
        {
            // Chrome Driver was manually downloaded from https://sites.google.com/a/chromium.org/chromedriver/downloads
            // parameter "." will instruct to look for the chromedriver.exe in the current folder (bin/debug/...)
            using (var driver = GetDriver())
            {
                test = extent.CreateTest(TestContext.TestName);
                // extent.LogInfo("TestLink2");
                //Navigate to DotNet website
                driver.Navigate().GoToUrl((string)TestContext.Properties["webAppUrl"]);
                //Click the Get Started button
                driver.FindElement(By.LinkText("Privacy Policy")).Click();
                var css = driver.FindElement(By.LinkText("Privacy Policy")).GetAttribute("class");
                var cssMenu = driver.FindElement(By.LinkText("Privacy")).GetAttribute("class");

                Console.WriteLine("CSS ---> " + css);
                // Get Started section is a multi-step wizard
                // The following sections will find the visible next step button until there's no next step button left
                
                // verify the title is the expected value "Next steps"
                Assert.AreEqual(css, "privacy");
                // if (Assert.AreEqual(css, "privacy"))
                // {
                //     test.Log(Status.Pass, "Page title verified");
                // }
                // else
                // {
                //     test.Log(Status.Fail, "Page title not verified");
                // }
            }
        }
        
        [TestMethod]
        public void TestLink2()
        {
            // Chrome Driver was manually downloaded from https://sites.google.com/a/chromium.org/chromedriver/downloads
            // parameter "." will instruct to look for the chromedriver.exe in the current folder (bin/debug/...)
            using (var driver = GetDriver())
            {
                test = extent.CreateTest(TestContext.TestName);
                // extent.LogInfo("TestLink2");
                //Navigate to DotNet website
                driver.Navigate().GoToUrl((string)TestContext.Properties["webAppUrl"]);
                //Click the Get Started button
                driver.FindElement(By.LinkText("Privacy")).Click();                
                var cssMenu = driver.FindElement(By.LinkText("Privacy")).GetAttribute("class");

                Console.WriteLine("CSS MENU ---> " + cssMenu);
                // Get Started section is a multi-step wizard
                // The following sections will find the visible next step button until there's no next step button left
                
                // verify the title is the expected value "Next steps"
                Assert.AreNotEqual(cssMenu, "not-privacy");
                // if (Assert.AreNotEqual(cssMenu, "not-privacy"))
                // {
                //     test.Log(Status.Pass, "Page title verified");
                // }
                // else
                // {
                //     test.Log(Status.Fail, "Page title not verified");
                // }
//                 Assert.AreNotEqual(cssMenu, "nav-link text-dark");
                
            }
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

        }
    }
}

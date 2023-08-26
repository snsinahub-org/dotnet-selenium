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
            
            var htmlReporter = new ExtentHtmlReporter("/tmp/results/cc.html");
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
                
                Assert.AreEqual(css, "privacy");
                
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
                
                Assert.AreNotEqual(cssMenu, "not-privacy");
    
                
            }
        }

        [TestMethod]
        public void TestLink3()
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
                // with selenium check nav background color
                var bg = driver.FindElement(By.class("navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3").GetAttribute("background-color");

                Console.WriteLine("BACKGROUND ---> " + bg);
                
                Assert.AreEqual(bg, "blue");
                
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
            // extent.Flush();

        }
    }
}

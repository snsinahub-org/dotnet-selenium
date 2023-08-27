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
            
            
        }

        
        
        

        [TestMethod]
        public void TestLink1()
        {
            // Chrome Driver was manually downloaded from https://sites.google.com/a/chromium.org/chromedriver/downloads
            // parameter "." will instruct to look for the chromedriver.exe in the current folder (bin/debug/...)
            using NUnit.Framework;
            using OpenQA.Selenium;
            using OpenQA.Selenium.Chrome;
            using AventStack.ExtentReports;
            using AventStack.ExtentReports.Reporter;

            namespace SeleniumTests
            {
                [TestFixture]
                public class Demo
                {
                    private ExtentReports extent;
                    private ITest test;

                    [OneTimeSetUp]
                    public void SetupReporting()
                    {
                        var htmlReporter = new ExtentHtmlReporter("extent.html");
                        extent = new ExtentReports();
                        extent.AttachReporter(htmlReporter);
                    }

                    [Test]
                    public void TestLink1()
                    {
                        using (var driver = GetDriver())
                        {
                            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                            driver.Navigate().GoToUrl((string)TestContext.CurrentContext.Test.Properties["webAppUrl"]);
                            driver.FindElement(By.LinkText("Privacy Policy")).Click();
                            var css = driver.FindElement(By.LinkText("Privacy Policy")).GetAttribute("class");
                            var cssMenu = driver.FindElement(By.LinkText("Privacy")).GetAttribute("class");
                            test.Log(Status.Info, "CSS ---> " + css);
                            Assert.AreEqual(css, "privacy");
                        }
                    }

                    [Test]
                    public void TestLink2()
                    {
                        using (var driver = GetDriver())
                        {
                            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                            driver.Navigate().GoToUrl((string)TestContext.CurrentContext.Test.Properties["webAppUrl"]);
                            driver.FindElement(By.LinkText("Privacy")).Click();
                            var cssMenu = driver.FindElement(By.LinkText("Privacy")).GetAttribute("class");
                            test.Log(Status.Info, "CSS MENU ---> " + cssMenu);
                            Assert.AreNotEqual(cssMenu, "not-privacy");
                        }
                    }

                    [Test]
                    public void TestLink4()
                    {
                        using (var driver = GetDriver())
                        {
                            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                            driver.Navigate().GoToUrl((string)TestContext.CurrentContext.Test.Properties["webAppUrl"]);
                            driver.FindElement(By.Id("redirect")).Click();
                            if (driver.Url != "https://dotnet.microsoft.com/")
                            {
                                test.Log(Status.Warning, "URL is not matching the expected URL");
                            }
                            else
                            {
                                test.Log(Status.Info, "URL is matching the expected URL");
                            }
                        }
                    }

                    [Test]
                    public void TestLink5()
                    {
                        using (var driver = GetDriver())
                        {
                            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                            driver.Navigate().GoToUrl((string)TestContext.CurrentContext.Test.Properties["webAppUrl"]);
                            test.Log(Status.Info, "Navigate to https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0");
                            driver.FindElement(By.Id("redirect")).Click();
                            Assert.AreEqual(driver.Url, "https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0");
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

                        if (bool.Parse((string)TestContext.CurrentContext.Test.Properties["headless"]))
                        {
                            options.AddArgument("headless");
                        }

                        return new ChromeDriver("/tmp", options);
                    }

                    [OneTimeTearDown]
                    public void endReporting()
                    {
                        extent.Flush();
                    }
                }
            }

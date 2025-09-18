using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace LambdaTestProj.BaseSetup

{
    internal class DriverSetup

    {
        [ThreadStatic]
        public static IWebDriver driver;
        public string browser;
        public static ExtentReports extent;
        public ExtentTest test;

        public DriverSetup(string browser)

        {

            this.browser = browser;

        }


        [SetUp]

        public void SetMethod()

        {

            string username = "shraddhalaxmanzaware";

            string accessKey = "LT_etV9kk7Xpqt8Hgf4YNg4ItDKctx88YIT1Nk5mlKoLr12w5q";

            string platformName = browser == "safari" ? "macOS Catalina" : "Windows 10";

            var ltOptions = new Dictionary<string, object>()

            {

                { "username", username },

                { "accessKey", accessKey },

                { "platformName", platformName },

                { "browserName", browser },

                { "browserVersion", "dev" },

                { "resolution", "1920x1080" },

                { "build", "LambdaTestAssessment" },

                { "project", "LambdaTestAssessmentProject" },

                { "name", TestContext.CurrentContext.Test.Name },

                { "visual", true },

                { "video", true },

                { "console", true },

                { "network", true },

                { "w3c", true },

                { "plugin", "c#-nunit" }

            };

            var capabilities = new OpenQA.Selenium.Chrome.ChromeOptions();

            capabilities.AddAdditionalOption("LT:Options", ltOptions);

            driver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub"), capabilities.ToCapabilities(), TimeSpan.FromSeconds(180));

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground");

        }

        [TearDown]

        public void TearDown()

        {
            try

            {
                if (driver != null)

                {
                    Screenshot screen = ((ITakesScreenshot)driver).GetScreenshot();
                    string path = Path.Combine(@"C:\Users\SHRLAXMA\source\repos\LambdaTestProj\LambdaTestProj\Screenshots\" + TestContext.CurrentContext.Test.MethodName + ".png");

                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                    using (FileStream fs = new FileStream(path, FileMode.Create))

                    {

                        fs.Write(screen.AsByteArray, 0, screen.AsByteArray.Length);

                        fs.Flush();

                    }

                    test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                    test.AddScreenCaptureFromPath(path);

                    bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;

                    ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));

                }

            }

            catch (Exception e)

            {
                Console.WriteLine("TearDown Exception: " + e.Message);
            }

            finally

            {

                if (driver != null)

                {

                    driver.Quit();

                    driver.Dispose();

                    driver = null;

                }

            }

        }

        [OneTimeTearDown]

        public static void onetimeteardown()

        {
            extent.Flush();
        }

    }

}

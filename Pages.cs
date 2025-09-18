using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LambdaTestProj

{

    internal class Page

    {
        IWebDriver driver;
        public Page(IWebDriver driver)

        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//*[@id=\"__next\"]/div/section[2]/div/ul/li[34]/a")]
        private IWebElement simpledemoform;
        [FindsBy(How = How.XPath, Using = " //h1[text()='Simple Form Demo']")]
        private IWebElement SiteHeading;

        public void SimpleDemoForm()

        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(simpledemoform)).Click();
            wait.Until(driver => SiteHeading.Displayed);
            string str = SiteHeading.Text;
            Assert.That(driver.Url.Contains("simple-form-demo"));
            Assert.That(str == "Simple Form Demo");

        }


        [FindsBy(How = How.XPath, Using = "//*[@id=\"user-message\"]")]
        private IWebElement inputtext;
        [FindsBy(How = How.XPath, Using = "//*[@id=\"showInput\"]")]
        private IWebElement button;
        [FindsBy(How = How.XPath, Using = "//*[@id=\"message\"]")]
        private IWebElement output;
        public void EnterForm()

        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            string str = "Welcome to LambdaTest";

            wait.Until(driver => inputtext.Displayed);

            inputtext.SendKeys(str);

            wait.Until(driver => button.Displayed && button.Enabled);

            button.Click();

            wait.Until(driver => output.Displayed);

            string outputres = output.Text;

            Assert.That(str == outputres);

        }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"__next\"]/div/section[2]/div/ul/li[13]/a")]
        private IWebElement DragnDrop;
        [FindsBy(How = How.XPath, Using = "//*[@id=\"slider3\"]/div/input")]
        private IWebElement dragto95;


        public void dragdrop()

        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            wait.Until(driver => DragnDrop.Displayed && DragnDrop.Enabled);

            DragnDrop.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("arguments[0].value = 95; arguments[0].dispatchEvent(new Event('input'));", dragto95);

            string value = dragto95.GetAttribute("value");

            Assert.That(value == "95");

        }


        [FindsBy(How = How.XPath, Using = "//*[@id=\"__next\"]/div/section[2]/div/ul/li[20]/a")]

        private IWebElement inputbtn;

        [FindsBy(How = How.CssSelector, Using = "#name")]
        private IWebElement name;

        [FindsBy(How = How.CssSelector, Using = "#inputEmail4")]
        private IWebElement email;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"inputPassword4\"]")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"company\"]")]
        private IWebElement company;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"websitename\"]")]
        private IWebElement website;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"inputCity\"]")]
        private IWebElement city;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"inputAddress1\"]")]
        private IWebElement addr1;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"inputAddress2\"]")]
        private IWebElement addr2;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"inputState\"]")]
        private IWebElement state;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"inputZip\"]")]
        private IWebElement zipcode;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"seleniumform\"]/div[3]/div[1]/select")]
        private IWebElement countrydropdown;

        [FindsBy(How = How.CssSelector, Using = "#seleniumform button[type='submit']")]
        private IWebElement submitBtn;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Thanks for contacting us, we will get back to you shortly.')]")]
        private IWebElement successMessage;

        public void inputform()

        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            wait.Until(driver => inputbtn.Displayed && inputbtn.Enabled);

            inputbtn.Click();

            submitBtn.Click();

            name.SendKeys("Shraddha");

            email.SendKeys("shraddha@mail.com");

            password.SendKeys("1234");

            company.SendKeys("capgemini");

            website.SendKeys("capgemini.com");

            countrydropdown = driver.FindElement(By.XPath("//*[@id='seleniumform']/div[3]/div[1]/select"));

            SelectElement select = new SelectElement(countrydropdown);

            select.SelectByText("United States");

            city.SendKeys("NewYork City");

            addr1.SendKeys("79th fifth Avenue");

            addr2.SendKeys("Main Street");

            state.SendKeys("New York");

            zipcode.SendKeys("500010");

            submitBtn.Click();

            wait.Until(driver => successMessage.Displayed);

            Assert.That(successMessage.Text.Contains("Thanks for contacting us, we will get back to you shortly."));

        }

    }

}


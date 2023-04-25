using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V108.IndexedDB;
using System.Globalization;

namespace Summator
{
    public class SummatorTests
    {
        private WebDriver driver;
        private const string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
        IWebElement FirstNum;
        IWebElement Operation;
        IWebElement SecondNum;
        IWebElement ButtonCalc;
        IWebElement ButtonReset;
        IWebElement Result;
       

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = BaseUrl;
            FirstNum = driver.FindElement(By.Id("number1"));
            Operation = driver.FindElement(By.Id("operation"));
            SecondNum = driver.FindElement(By.Id("number2"));
            ButtonCalc = driver.FindElement(By.Id("calcButton"));
            ButtonReset = driver.FindElement(By.Id("resetButton"));
            Result = driver.FindElement(By.Id("result"));
        }
        [OneTimeTearDown]
        public void Teardown()
        { 
        driver.Quit();  
        }

        [TestCase("2", "+", "5", "Result: 7")]
        [TestCase("3", "-", "1", "Result: 2")]
        [TestCase("4", "*", "2", "Result: 8")]
        [TestCase("6", "/", "2", "Result: 3")]
        [TestCase("-6", "+", "-2", "Result: -8")]
        [TestCase("-2", "-", "-18", "Result: 16")]
        [TestCase("-4", "*", "-2", "Result: 8")]
        [TestCase("-6", "/", "-2", "Result: 3")]
        [TestCase("2", "+", "0", "Result: 2")]
        [TestCase("0", "-", "1", "Result: -1")]
        [TestCase("0", "*", "2", "Result: 0")]
        [TestCase("0", "/", "2", "Result: 0")]
        [TestCase("6", "/", "0", "Result: Infinity")]
        public void Test_ValidIntegerNumbers(string firstNum, string op, string secNum, string expectedResult)
        {
            FirstNum.SendKeys(firstNum);
            Operation.SendKeys(op);
            SecondNum.SendKeys(secNum);
            ButtonCalc.Click();
            Assert.AreEqual(Result.Text, expectedResult);
            ButtonReset.Click();
        }

        [TestCase("2.5", "+", "3.1", "Result: 5.6")]
        [TestCase("8.23", "-", "3.62", "Result: 4.61")]
        [TestCase("2.78", "*", "1.34", "Result: 3.7252")]
        [TestCase("809.15", "/", "20.89", "Result: 38.7338439445")]
        public void Test_ValidDecimalNumbers(string firstNum, string op, string secNum, string expectedResult)
        {
            FirstNum.SendKeys(firstNum);
            Operation.SendKeys(op);
            SecondNum.SendKeys(secNum);
            ButtonCalc.Click();
            Assert.AreEqual(Result.Text, expectedResult);
            ButtonReset.Click();
        }

        [TestCase("avd", "+", "!", "Result: invalid input")]
        [TestCase("", "-", "", "Result: invalid input")]
        [TestCase("Q", "*", "2", "Result: invalid input")]
        [TestCase("%", "/", "*", "Result: invalid input")]
        [TestCase("", "", "", "Result: invalid input")]
        public void Test_InvalidInput(string firstNum, string op, string secNum, string expectedResult)
        {
            FirstNum.SendKeys(firstNum);
            Operation.SendKeys(op);
            SecondNum.SendKeys(secNum);
            ButtonCalc.Click();
            Assert.AreEqual(Result.Text, expectedResult);
            ButtonReset.Click();
        }

        [TestCase("2", "", "5", "Result: invalid operation")]
        [TestCase("3", "...", "1", "Result: invalid operation")]
        [TestCase("4", "A", "2", "Result: invalid operation")]
        [TestCase("6", "0", "2", "Result: invalid operation")]
        public void Test_InvalidOperation(string firstNum, string op, string secNum, string expectedResult)
        {
            FirstNum.SendKeys(firstNum);
            Operation.SendKeys(op);
            SecondNum.SendKeys(secNum);
            ButtonCalc.Click();
            Assert.AreEqual(Result.Text, expectedResult);
            ButtonReset.Click();
        }
    }
}
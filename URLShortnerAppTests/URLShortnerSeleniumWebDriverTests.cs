using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace URLShortnerAppTests
{
    public class URLShortnerSeleniumWebDriverTests
    {
        public WebDriver driver;
        private const string BaseUrl = "https://shorturl.softuniqa.repl.co/";

        [SetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = BaseUrl;
        }

        [TearDown]
        public void CloseBrowser() 
        {
            driver.Quit();
        }

        [Test]
        public void Test_CheckTheTitle()
        {
            var pageSource = driver.PageSource;
            Assert.That(pageSource.Contains("URL Shortener"));
        }

        [Test]
        public void Test_CheckTheTitle2()
        {
            var pageTitle = driver.FindElement(By.XPath("//h1[contains(.,'URL Shortener')]"));
            Assert.That(pageTitle.Text.Contains("URL Shortener"));
        }

        [Test]
        public void Test_ShortURLsPage()
        {
            driver.FindElement(By.XPath("//a[@href='/urls'][contains(.,'Short URLs')]")).Click();
            var firstCell = driver.FindElement(By.XPath("//td[contains(.,'https://nakov.com')]"));
            var secondCell = driver.FindElement(By.XPath("//a[@class='shorturl'][contains(.,'http://shorturl.softuniqa.repl.co/go/nak')]"));
            Assert.That(firstCell.Text.Contains("https://nakov.com"));
            Assert.That(secondCell.Text.Contains("http://shorturl.softuniqa.repl.co/go/nak"));
        }
    }
}
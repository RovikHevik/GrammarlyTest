using GrammarlyTest.Logic.Interface;
using GrammarlyTest.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace GrammarlyTest.Logic
{
    internal class BrowserLogic : ITextInput, IBrowserManipulator
    {
        IWebDriver driver;
        public BrowserLogic()
        {
            try
            {
                driver = new FirefoxDriver(@"C:\Users\HohloCit\source\repos\GrammarlyTest\GrammarlyTest\");
            }
            catch (Exception)
            {
                CloseApp();
            }
        }

        public bool CloseApp()
        {
            try
            {
                driver.Close();
                driver.Quit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool InputText(TextModel model)
        {
            return OpenUrl($"https://translate.google.com/?sl=ru&tl=en&text={model.Text}&op=translate");
        }

        public bool OpenUrl(string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                return true;
            }
            catch (Exception)
            {
                CloseApp();
                return false;
            }
        }

        public bool WriteTextToPage(TextModel textModel, SiteModel siteModel)
        {
            try
            {
                OpenUrl(siteModel.Url);
                IWebElement element = null;
                if (siteModel.Selector.Contains("id:"))
                {
                    element = driver.FindElement(By.Id(siteModel.Selector.Replace("id:", "")));
                }
                else if (siteModel.Selector.Contains("class:"))
                {
                    element = driver.FindElement(By.ClassName(siteModel.Selector.Replace("class:", "")));
                }
                element.Clear();
                element.SendKeys(textModel.Text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
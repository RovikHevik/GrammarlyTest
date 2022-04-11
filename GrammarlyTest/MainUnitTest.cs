using GrammarlyTest.Logic;
using GrammarlyTest.Logic.Interface;
using GrammarlyTest.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace GrammarlyTest
{
    public class Tests
    {
        private const string PathToWord = @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE";
        private const string PathToGrammarly = @"C:\Users\Fridoleen\AppData\Local\Grammarly\DesktopIntegrations\Grammarly.Desktop.exe";
        private List<SiteModel> siteToTest = new List<SiteModel>();
        private static readonly TextModel modelToTest = new TextModel() { Text = "what r u know anout thes", ErrorCount = 2 };
        private static readonly TextModel modelToTestTwo = new TextModel() { Text = "what r u know about this", ErrorCount = 0 };
        WordLogic wordLogic;
        BrowserLogic browserLogic;
        GrammarlyLogic grammarlyLogic;

        [SetUp]
        public void Setup()
        {
            grammarlyLogic = new GrammarlyLogic(PathToGrammarly);
            siteToTest.Add(new SiteModel() { Url = "https://www.onlinetexteditor.net/", Selector = "id:compose" });
            siteToTest.Add(new SiteModel() { Url = "https://translate.google.com/?sl=en&tl=ru&op=translate", Selector = "class:er8xn" });
            siteToTest.Add(new SiteModel() { Url = "https://www.m-translate.ru/translator/text#", Selector = "id:text" });
        }

        [Test]
        public void BrowserTest()
        {
            browserLogic = new BrowserLogic();
            foreach (var site in siteToTest)
            {
                Assert.IsTrue(browserLogic.WriteTextToPage(modelToTest, site), $"Error when try to write text to page {site.Url}");
                Assert.IsTrue(grammarlyLogic.IsValueCorrect(modelToTest), $"Error when try to detect error in 10sec {site.Url}");
            }
            browserLogic.CloseApp();
        }

        [Test]
        public void DesktopTest()
        {
            wordLogic = new WordLogic(PathToWord);
            Assert.IsTrue(wordLogic.InputText(modelToTest));
            Assert.IsTrue(grammarlyLogic.IsValueCorrect(modelToTest));
            wordLogic.CloseApp();
        }

    }
}
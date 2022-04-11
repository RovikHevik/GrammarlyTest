using GrammarlyTest.Model;
using FlaUI.UIA3;
using System;
using System.Threading;
using GrammarlyTest.Logic.Interface;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core;

namespace GrammarlyTest.Logic
{
    internal class WordLogic : ITextInput
    {
        Application app;

        public WordLogic(string pathToExe)
        {
            app = DesktopLogic.StartApp(pathToExe);
        }

        public bool CloseApp()
        {
            try
            {
                app.Kill();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InputText(TextModel model)
        {
            try
            {
                using (var automation = new UIA3Automation())
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    var window = app.GetMainWindow(automation);
                    var button = Retry.Find(() => window.FindFirstDescendant(cf => cf.ByName("Новый документ")),
                    new RetrySettings
                            {
                                Timeout = TimeSpan.FromSeconds(2),
                                Interval = TimeSpan.FromMilliseconds(500)
                            }
                        ); 
                    button.Click();
                    Keyboard.Type(model.Text);
                    return true;
                }
            }
            catch (Exception)
            {
                app.Close();
                return false;
            }
        }
    }
}
using GrammarlyTest.Model;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Linq;
using System.Threading;

namespace GrammarlyTest.Logic
{
    internal class GrammarlyLogic
    {
        Application app;
        public GrammarlyLogic(string pathToExe)
        {
            app = Application.Attach(pathToExe);
        }
        
        public bool IsValueCorrect(TextModel model)
        {
            using (var automation = new UIA3Automation())
            {
                AutomationElement errorLabel = null;
                Thread.Sleep(TimeSpan.FromSeconds(3));                
                var screen = app.GetAllTopLevelWindows(automation).First();
                int count = -1;
                for (int i = 0; i < 20; i++) 
                { 
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    errorLabel = screen.FindFirstDescendant(cf => cf.ByAutomationId("AlertsCounterLabel"));
                    if (errorLabel.Name == model.ErrorCount.ToString())
                    {
                        i = 20;
                    }
                }
                int.TryParse(errorLabel.Name, out count);         
                if (count == model.ErrorCount) return true;
                else                           return false;
            }
        }
    }
}
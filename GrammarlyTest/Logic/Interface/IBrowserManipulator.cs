using GrammarlyTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarlyTest.Logic.Interface
{
    internal interface IBrowserManipulator
    {
        bool OpenUrl(string url);
        bool WriteTextToPage(TextModel textModel, SiteModel siteModel);
    }
}

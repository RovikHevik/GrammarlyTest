using GrammarlyTest.Model;

namespace GrammarlyTest.Logic.Interface
{
    internal interface IBrowserManipulator
    {
        bool OpenUrl(string url);
        bool WriteTextToPage(TextModel textModel, SiteModel siteModel);
    }
}

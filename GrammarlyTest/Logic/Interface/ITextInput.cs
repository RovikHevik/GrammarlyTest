using GrammarlyTest.Model;

namespace GrammarlyTest.Logic.Interface
{
    interface ITextInput
    {
        bool InputText(TextModel model);
        bool CloseApp();
    }
}

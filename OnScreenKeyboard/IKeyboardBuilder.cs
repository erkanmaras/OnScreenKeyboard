using System.Xml.Linq;

namespace OnScreenKeyboard
{
    //Test1
    internal interface IKeyboardBuilder
    {
        void Build(XDocument definition, Keyboard keyboard);
        void Build(string path, Keyboard keyboard);
    }
}

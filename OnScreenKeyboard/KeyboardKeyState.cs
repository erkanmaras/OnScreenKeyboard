using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnScreenKeyboard
{
    internal class KeyboardKeyState
    {
        public KeyStateAction StateAction;
        public KeyStateStyle Style;
        public string KeyCode = string.Empty;
        public string KeyCodeDeadAcute = string.Empty;
        public string KeyCodeDeadCircumflex = string.Empty;
        public string KeyCodeDeadDiaeresis = string.Empty;
        public string KeyCodeDeadGrave = string.Empty;
        public string KeyCodeDeadTilde = string.Empty;
        public string Text = string.Empty;
    }
}

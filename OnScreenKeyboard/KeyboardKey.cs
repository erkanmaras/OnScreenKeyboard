using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace OnScreenKeyboard
{
    [ToolboxItem(false)]
    public sealed class KeyboardKey : Button
    {
        private KeyStateStyle _currentStyle;
        private bool _isLocked;
        private readonly List<KeyboardKeyState> _states = new List<KeyboardKeyState>();

        public KeyboardKey()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Selectable, false);

            TabStop = false;
            Cursor = Cursors.Hand;
            Font = new Font(Font, FontStyle.Bold);
        }

        internal KeyStateStyle CurrentStyle
        {
            get { return _currentStyle; }
            set
            {
                _currentStyle = value;
                Invalidate();
            }
        }

        public bool IsLocked
        {
            get { return _isLocked; }
            set
            {
                _isLocked = value;
                Invalidate();
            }
        }

        internal void AddState(KeyboardKeyState state)
        {
            _states.Add(state);
        }

        internal KeyboardKeyState GetCurrentState()
        {
            return _states.FirstOrDefault(state => (state.Style == CurrentStyle));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var currentState = GetCurrentState();
            if (currentState != null)
            {
                Text = currentState.Text;
            }

            if (IsLocked)
            {
                ButtonRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);
                ButtonRenderer.DrawButton(e.Graphics, e.ClipRectangle, Text, Font, true, PushButtonState.Pressed);
            }
            else
            {
                base.OnPaint(e);
            }
        }

    }

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

    internal enum KeyStateAction
    {
        Alt = 4,
        AltGr = 5,
        CapsLock = 2,
        Control = 3,
        DeadAcute = 7,
        DeadCircumflex = 9,
        DeadDiaeresis = 10,
        DeadGrave = 8,
        DeadTilde = 6,
        Send = 0,
        Shift = 1
    }

    internal enum KeyStateStyle
    {
        AltGr = 2,
        AltGrShift = 3,
        Default = 0,
        Shift = 1
    }
}

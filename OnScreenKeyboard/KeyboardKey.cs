using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace OnScreenKeyboard
{
    [ToolboxItem(false)]
    public class KeyboardKey : Button, IKeyboardKey
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

        public KeyStateStyle CurrentStyle
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

        public void AddState(KeyboardKeyState state)
        {
            _states.Add(state);
        }

        public KeyboardKeyState GetCurrentState()
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

    public interface IKeyboardKey
    {
        KeyStateStyle CurrentStyle { get; }
        bool IsLocked { get; set; }
        KeyboardKeyState GetCurrentState();
        void AddState(KeyboardKeyState state);

    }

}

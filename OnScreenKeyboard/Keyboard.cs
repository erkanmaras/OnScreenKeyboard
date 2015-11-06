using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OnScreenKeyboard
{
    public class Keyboard : TableLayoutPanel
    {

        private bool _isAlt;
        private bool _isAltGr;
        private bool _isCapsLock;
        private bool _isControl;
        private bool _isDeadAcute;
        private bool _isDeadCircumflex;
        private bool _isDeadDiaeresis;
        private bool _isDeadGrave;
        private bool _isDeadTilde;
        private bool _isShift;
        private string _deadKeyCode = string.Empty;

        public Keyboard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            ControlAdded += Keyboard_ControlAdded;
            ControlRemoved += Keyboard_ControlRemoved;
            Resize += Keyboard_Resize;
        }

        public void BuildDefaultDefinition()
        {
            XDocument definition;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.Definitions.{1}", GetType().Namespace, "KeyboardDefinition_en.xml")))
            {
                Debug.Assert(stream != null, "stream != null");
                using (var reader = new StreamReader(stream))
                {
                    definition = XDocument.Load(reader);
                }
            }
            new KeyboardBuilder().Build(definition,this);
        }

        public void AddKey(KeyboardKey key, Point keyLocation, Size keySize)
        {
            LayoutManager.AddCell(key, keyLocation, keySize);
            Controls.Add(key);
        }

        public void SetGirdSize(short rows, short cols)
        {
            LayoutManager.Rows = rows;
            LayoutManager.Cols = cols;
        }

        public void PerformKeyboardLayout()
        {
            LayoutManager.PerformLayout();
        }

        private void ClearControlAltShiftState()
        {
            if (_isAlt)
            {
                ToggleAltState();
            }
            if (_isControl)
            {
                ToggleControlState();
            }
            if (_isShift)
            {
                ToggleShiftState();
            }
        }

        private void ClearDeadKeyState()
        {
            _isDeadTilde = false;
            _isDeadGrave = false;
            _isDeadAcute = false;
            _isDeadCircumflex = false;
            _isDeadDiaeresis = false;
            _deadKeyCode = string.Empty;
        }

        public void OnKeyClicked(object sender, EventArgs e)
        {
            var currentState = ((KeyboardKey)sender).GetCurrentState();
            if (currentState != null)
            {
                switch (currentState.StateAction)
                {
                    case KeyStateAction.Send:
                        string keyCodeDeadTilde;
                        if (_isDeadTilde)
                        {
                            keyCodeDeadTilde = currentState.KeyCodeDeadTilde;
                            if (keyCodeDeadTilde.Length == 0)
                            {
                                keyCodeDeadTilde = (_deadKeyCode + currentState.KeyCode);
                            }
                        }
                        else if (!_isDeadGrave)
                        {
                            if (_isDeadAcute)
                            {
                                keyCodeDeadTilde = currentState.KeyCodeDeadAcute;
                                if (keyCodeDeadTilde.Length == 0)
                                {
                                    keyCodeDeadTilde = (_deadKeyCode + currentState.KeyCode);
                                }
                            }
                            else if (_isDeadCircumflex)
                            {
                                keyCodeDeadTilde = currentState.KeyCodeDeadCircumflex;
                                if (keyCodeDeadTilde.Length == 0)
                                {
                                    keyCodeDeadTilde = (_deadKeyCode + currentState.KeyCode);
                                }
                            }
                            else if (_isDeadDiaeresis)
                            {
                                keyCodeDeadTilde = currentState.KeyCodeDeadDiaeresis;
                                if (keyCodeDeadTilde.Length == 0)
                                {
                                    keyCodeDeadTilde = (_deadKeyCode + currentState.KeyCode);
                                }
                            }
                            else
                            {
                                keyCodeDeadTilde = currentState.KeyCode;
                            }
                        }
                        else
                        {
                            keyCodeDeadTilde = currentState.KeyCodeDeadGrave;
                            if (keyCodeDeadTilde.Length == 0)
                            {
                                keyCodeDeadTilde = (_deadKeyCode + currentState.KeyCode);
                            }
                        }
                        if (_isControl)
                        {
                            keyCodeDeadTilde = ("^" + keyCodeDeadTilde);
                        }
                        if (_isAlt)
                        {
                            keyCodeDeadTilde = ("%" + keyCodeDeadTilde);
                        }
                        SendKeys.Send(keyCodeDeadTilde.Replace("{SPACE}", " "));
                        ClearControlAltShiftState();
                        ClearDeadKeyState();
                        return;
                    case KeyStateAction.Shift:
                        ToggleShiftState();
                        return;
                    case KeyStateAction.CapsLock:
                        ToggleCapsLockState();
                        return;
                    case KeyStateAction.Control:
                        ToggleControlState();
                        return;
                    case KeyStateAction.Alt:
                        ToggleAltState();
                        return;
                    case KeyStateAction.AltGr:
                        ToggleAltGrState();
                        return;
                    case KeyStateAction.DeadTilde:
                        SetDeadTildeState(currentState.KeyCode);
                        ClearControlAltShiftState();
                        return;
                    case KeyStateAction.DeadAcute:
                        SetDeadAcuteState(currentState.KeyCode);
                        ClearControlAltShiftState();
                        return;
                    case KeyStateAction.DeadGrave:
                        SetDeadGraveState(currentState.KeyCode);
                        ClearControlAltShiftState();
                        return;
                    case KeyStateAction.DeadCircumflex:
                        SetDeadCircumflexState(currentState.KeyCode);
                        ClearControlAltShiftState();
                        return;
                    case KeyStateAction.DeadDiaeresis:
                        SetDeadDiaeresisState(currentState.KeyCode);
                        ClearControlAltShiftState();
                        return;
                }
            }
        }

        private void Keyboard_ControlAdded(object sender, ControlEventArgs e)
        {
            var control = (KeyboardKey)e.Control;
            control.Click += OnKeyClicked;
        }

        private void Keyboard_ControlRemoved(object sender, ControlEventArgs e)
        {
            var control = (KeyboardKey)e.Control;
            control.Click -= OnKeyClicked;
        }

        private void SetDeadAcuteState(string key)
        {
            _isDeadAcute = true;
            _deadKeyCode = key;
        }

        private void SetDeadCircumflexState(string key)
        {
            _isDeadCircumflex = true;
            _deadKeyCode = key;
        }

        private void SetDeadDiaeresisState(string key)
        {
            _isDeadDiaeresis = true;
            _deadKeyCode = key;
        }

        private void SetDeadGraveState(string key)
        {
            _isDeadGrave = true;
            _deadKeyCode = key;
        }

        private void SetDeadTildeState(string key)
        {
            _isDeadTilde = true;
            _deadKeyCode = key;
        }

        private void SetKeyboardLock(KeyStateAction stateAction, bool value)
        {
            foreach (var control in Controls)
            {
                var keyboardKey = control as KeyboardKey;
                if (keyboardKey != null)
                {
                    var currentState = keyboardKey.GetCurrentState();
                    if (currentState != null && currentState.StateAction == stateAction)
                    {
                        keyboardKey.IsLocked = value;
                        keyboardKey.Refresh();
                    }
                }
            }
        }

        private void SetKeyboardState()
        {
            KeyStateStyle altGrShift;

            if ((_isShift ^ _isCapsLock) && _isAltGr)
            {
                altGrShift = KeyStateStyle.AltGrShift;
            }
            else if (_isShift ^ _isCapsLock)
            {
                altGrShift = KeyStateStyle.Shift;
            }
            else if (_isAltGr)
            {
                altGrShift = KeyStateStyle.AltGr;
            }
            else
            {
                altGrShift = KeyStateStyle.Default;
            }

            foreach (var control in Controls)
            {
                var keyboardKey = control as KeyboardKey;
                if (keyboardKey != null)
                {
                    keyboardKey.CurrentStyle = altGrShift;
                }
            }
        }

        private void ToggleAltGrState()
        {
            _isAltGr = !_isAltGr;
            SetKeyboardLock(KeyStateAction.AltGr, _isAltGr);
            SetKeyboardState();
        }

        private void ToggleAltState()
        {
            _isAlt = !_isAlt;
            SetKeyboardLock(KeyStateAction.Alt, _isAlt);
        }

        private void ToggleCapsLockState()
        {
            _isCapsLock = !_isCapsLock;
            SetKeyboardLock(KeyStateAction.CapsLock, _isCapsLock);
            SetKeyboardState();
        }

        private void ToggleControlState()
        {
            _isControl = !_isControl;
            SetKeyboardLock(KeyStateAction.Control, _isControl);
        }

        private void ToggleShiftState()
        {
            _isShift = !_isShift;
            SetKeyboardLock(KeyStateAction.Shift, _isShift);
            SetKeyboardState();
        }

        private void Keyboard_Resize(object sender, EventArgs e)
        {
            SuspendLayout();
            PerformControlLayout();
            ResumeLayout();
        }

        public static void ShowDialog(string caption, Point location,Size size)
        {
            new System.Threading.Tasks.Task(() =>
            {
                var form = new KeyboardDialog { Text = caption, ShownLocation = location, ClientSize = size};
                form.ShowDialog();

            }).Start();
        }

        public static void ShowDialog(string caption)
        {
            new System.Threading.Tasks.Task(() =>
            {
                var form = new KeyboardDialog { Text = caption };
                form.ShowDialog();
            }).Start();
        }

    }
}
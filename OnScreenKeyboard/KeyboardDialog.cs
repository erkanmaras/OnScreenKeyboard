using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnScreenKeyboard
{
    public class KeyboardDialog : Form
    {
        private const int WsExNoactivate = 0x8000000;
        private const int WsExToolwindow = 0x00000080;
        private const int WmNcmousemove = 0xa0;
        private const int WmNclbuttondown = 0xa1;

        private Keyboard _keyboard;
        private IntPtr _prevForegroundWindow = IntPtr.Zero;

        public Point ShownLocation;

        public KeyboardDialog()
        {
            InitializeComponent();
            _keyboard.BuildDefaultDefinition();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var baseParams = base.CreateParams;
                baseParams.ExStyle |= (WsExNoactivate | WsExToolwindow);
                return baseParams;
            }
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private void InitializeComponent()
        {
            _keyboard = new Keyboard {Dock = DockStyle.Fill};
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(900, 297);
            ShowIcon = false;
            Controls.Add(_keyboard);
            Text = "Screen Keyboard";
            ShowInTaskbar = false;
            MaximizeBox = false;
            TopMost = true;
            TopLevel = true;
            Visible = false;
            Shown += OnShownForm;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WmNclbuttondown:
                    var foregroundWindow = NativeMethods.GetForegroundWindow();
                    if (foregroundWindow == Handle)
                    {
                        NativeMethods.SetForegroundWindow(Handle);
                        if (foregroundWindow == IntPtr.Zero)
                        {
                            _prevForegroundWindow = foregroundWindow;
                        }
                    }
                    break;
                case WmNcmousemove:
                    if (NativeMethods.IsWindow(_prevForegroundWindow))
                    {
                        NativeMethods.SetForegroundWindow(_prevForegroundWindow);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void OnShownForm(object sender, EventArgs e)
        {
            Location = ShownLocation;
            Visible = true;
        }
    }
}

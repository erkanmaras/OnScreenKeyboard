using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnScreenKeyboardDemo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            keyboardControl.Build();
        }

        private void lblClickMe_Click(object sender, EventArgs e)
        {
            OnScreenKeyboard.Keyboard.ShowDialog("Keyboard Caption",
                                                 new Point(DesktopLocation.X , DesktopLocation.Y + Height),
                                                 new Size(900,290));
        }
    }
}

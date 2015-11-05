using System.Drawing;
using System.Windows.Forms;

namespace OnScreenKeyboard
{
    internal class KeyboardLayoutCell
    {
        public KeyboardLayoutCell()
        {
        }

        public KeyboardLayoutCell(Control control, Point position, Size size)
        {
            Control = control;
            Position = position;
            Size = size;
        }

        public Control Control { get; set; }

        public Point Position { get; set; }

        public Size Size { get; set; }
    }
}

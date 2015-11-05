using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OnScreenKeyboard
{
    internal class KeyboardLayoutManager
    {
        public List<KeyboardLayoutCell> Cells = new List<KeyboardLayoutCell>();
        public ILayoutContainer Container;
        public int GutterHeight = 5;
        public int GutterWidth = 5;
        public int MarginHeight = 4;
        public int MarginWidth = 4;
        public int Rows;
        public int Cols;

        public KeyboardLayoutManager(ILayoutContainer container)
        {
            Container = container;
        }

        public KeyboardLayoutManager(ILayoutContainer container, int gutterHeight, int gutterWidth, int marginHeight,
            int marginWidth, int rows, int cols)
        {
            Container = container;
            GutterHeight = gutterHeight;
            GutterWidth = gutterWidth;
            MarginHeight = marginHeight;
            MarginWidth = marginWidth;
            Rows = rows;
            Cols = cols;
        }

        public KeyboardLayoutManager(ILayoutContainer container, int rows, int cols)
        {
            Container = container;
            Rows = rows;
            Cols = cols;
        }

        public void PerformLayout()
        {
            if (Cols <= 0 || Rows <= 0)
            {
                return;
            }

            var marginSize = new Size(MarginWidth, MarginHeight);
            var gutterSize = new Size(GutterWidth, GutterHeight);
            var containerClientSize = Container.ContainerClientSize;

            var clientSize = new Size
            {
                Width = (((containerClientSize.Width - (2*gutterSize.Width)) - (marginSize.Width*(Cols - 1)))/Cols),
                Height = (((containerClientSize.Height - (2*gutterSize.Height)) - (marginSize.Height*(Rows - 1)))/Rows)
            };

            foreach (var cell in Cells)
            {
                if (cell.Control == null)
                {
                    continue;
                }
                var location = new Point
                {
                    X = (gutterSize.Width + ((clientSize.Width + marginSize.Width)*cell.Position.X)),
                    Y = (gutterSize.Height + ((clientSize.Height + marginSize.Height)*cell.Position.Y))
                };
                var controlSize = new Size();

                if (((cell.Position.X + cell.Size.Width) < Cols))
                {
                    controlSize.Width = ((cell.Size.Width*clientSize.Width) + (marginSize.Width*(cell.Size.Width - 1)));
                }
                else
                {
                    controlSize.Width = ((containerClientSize.Width - location.X) - gutterSize.Width);
                }

                if (((cell.Position.Y + cell.Size.Height) < Rows))
                {
                    controlSize.Height = ((cell.Size.Height*clientSize.Height) +(marginSize.Height*(cell.Size.Height - 1)));
                }
                else
                {
                    controlSize.Height = ((containerClientSize.Height - location.Y) - gutterSize.Height);
                }

                location.X = Math.Max(location.X, 0);
                location.Y = Math.Max(location.Y, 0);
                controlSize.Width = Math.Max(controlSize.Width, 0);
                controlSize.Height = Math.Max(controlSize.Height, 0);
                cell.Control.Bounds = new Rectangle(location, controlSize);
            }
        }

        public void AddCell(Control control, Point position, Size size)
        {
            Cells.Add(new KeyboardLayoutCell(control, position, size));
        }

        public void AddCell(Control control, Point position)
        {
            Cells.Add(new KeyboardLayoutCell(control, position, new Size(1, 1)));
        }

        public void AddCell(Control control, int col, int row)
        {
            Cells.Add(new KeyboardLayoutCell(control, new Point(col, row), new Size(1, 1)));
        }

        public void AddCell(int col, int row)
        {
            Cells.Add(new KeyboardLayoutCell(null, new Point(col, row), new Size(1, 1)));
        }
    }
}

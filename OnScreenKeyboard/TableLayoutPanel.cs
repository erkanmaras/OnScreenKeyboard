using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OnScreenKeyboard
{ 
    [ToolboxItem(false)]
    public class TableLayoutPanel : Panel, ILayoutContainer
    {
        private KeyboardLayoutManager _layoutManager;
        private bool _layoutSuspended;

        public TableLayoutPanel()
        {
             SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
             SetStyle(ControlStyles.AllPaintingInWmPaint, true);
             SetStyle(ControlStyles.SupportsTransparentBackColor, true);
             SetStyle(ControlStyles.ResizeRedraw, true);
             SetStyle(ControlStyles.UserPaint, true);
        }

        internal KeyboardLayoutManager LayoutManager
        {
            get
            {
                return _layoutManager ?? (_layoutManager = new KeyboardLayoutManager(this)
                {
                    GutterWidth = GutterSize,
                    GutterHeight = GutterSize
                });
            }
        }

        internal List<KeyboardLayoutCell> Cells
        {
            get { return LayoutManager.Cells; }
        }

        public int GutterSize { get; set; }

        public Size ContainerClientSize
        {
            get { return ClientSize; }
        }

        public void SuspendControlLayout()
        {
            _layoutSuspended = true;
        }

        public void ResumeControlLayout(bool performLayout)
        {
            _layoutSuspended = false;
            if (performLayout)
            {
                LayoutManager.PerformLayout();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!_layoutSuspended)
            {
                LayoutManager.PerformLayout();
            }
        }

        public void PerformControlLayout()
        {
            LayoutManager.GutterHeight = GutterSize;
            LayoutManager.GutterWidth = GutterSize;
            LayoutManager.PerformLayout();
        }

        public void AddControl(int index, Control control)
        {
            Controls.Add(control);
            Cells[index].Control = control;
        }

        public void AddControl(int index, Control control, Size size)
        {
            AddControl(index, control);
            Cells[index].Size = size;
        }

    }
}

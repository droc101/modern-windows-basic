// https://stackoverflow.com/questions/2487104/how-do-i-implement-a-textbox-that-displays-type-here

using System;
using System.Windows.Forms;
using System.Drawing;

namespace ModernBasicTheme
{
    class WaterMarkTextBox : TextBox
    {
        private Font oldFont = null;
        private bool waterMarkTextEnabled = false;

        #region Attributes 
        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set
            {
                _waterMarkColor = value; Invalidate();
            }
        }

        private string _waterMarkText = "Water Mark";
        public string WaterMarkText
        {
            get { return _waterMarkText; }
            set { _waterMarkText = value; Invalidate(); }
        }
        #endregion

        public WaterMarkTextBox()
        {
            JoinEvents(true);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            WaterMark_Toggel(null, null);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            Font drawFont = new Font(Font.FontFamily,
                Font.Size, Font.Style, Font.Unit); 
            SolidBrush drawBrush = new SolidBrush(WaterMarkColor);
            args.Graphics.DrawString((waterMarkTextEnabled ? WaterMarkText : Text),
                drawFont, drawBrush, new PointF(0.0F, 0.0F));
            base.OnPaint(args);
        }

        private void JoinEvents(Boolean join)
        {
            if (join)
            {
                TextChanged += new EventHandler(this.WaterMark_Toggel);
                LostFocus += new EventHandler(this.WaterMark_Toggel);
                FontChanged += new EventHandler(this.WaterMark_FontChanged);
            }
        }

        private void WaterMark_Toggel(object sender, EventArgs args)
        {
            if (Text.Length <= 0)
                EnableWaterMark();
            else
                DisbaleWaterMark();
        }

        private void EnableWaterMark()
        {
            oldFont = new Font(Font.FontFamily, Font.Size, Font.Style,
               Font.Unit);
            SetStyle(ControlStyles.UserPaint, true);
            waterMarkTextEnabled = true;
            Refresh();
        }

        private void DisbaleWaterMark()
        {
            waterMarkTextEnabled = false;
            SetStyle(ControlStyles.UserPaint, false);
            if (oldFont != null)
                Font = new Font(oldFont.FontFamily, oldFont.Size,
                    oldFont.Style, oldFont.Unit);
        }

        private void WaterMark_FontChanged(object sender, EventArgs args)
        {
            if (waterMarkTextEnabled)
            {
                oldFont = new Font(Font.FontFamily, Font.Size, Font.Style,
                    Font.Unit);
                Refresh();
            }
        }
    }
}

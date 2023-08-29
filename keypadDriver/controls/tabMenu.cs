using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace driverClasses.controls
{
    internal class tabMenu : System.Windows.Forms.Panel
    {
        public string title;
        public Color BorderColor = Color.LightGray;
        public int BorderSize = 5;
        public bool isExtended = true;

        FlowLayoutPanel flowLayoutPanel;
        roundButton titleButton;
        public tabMenu(string title, string[] options) : base()
        {
            this.Width = 500;
            this.title = title;
            titleButton = new roundButton();
            titleButton.Text = this.title;
            titleButton.BackColor = Color.FromArgb(45, 45, 45);
            titleButton.ForeColor = Color.White;
            titleButton.BorderRadius = 10;
            titleButton.BorderSize = 0;
            titleButton.BorderColor = Color.White;
            titleButton.Padding = new Padding(0, 0, this.Width / 2 + 20, 0);
            titleButton.Width = Width;
            titleButton.Height = 50;
            
            flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.WrapContents = false;
            flowLayoutPanel.Padding = new Padding(0, 0, 0, 0);

            base.BackColor = Color.Red;
            //flowLayoutPanel.BackColor = Color.Blue;
            
            //titleButton.Dock = DockStyle.Top;

            flowLayoutPanel.Controls.Add(titleButton);
            this.Controls.Add(flowLayoutPanel);

            flowLayoutPanel.Size = new Size(this.Width, this.Height);
            //titleButton.Size = new Size(flowLayoutPanel.Width, 40);

            //if (isExtended)
            //{
            //    for (int i = 0; i < options.Length; i++)
            //    {
            //        roundButton r = new roundButton();
            //        r.Text = options[i];
            //        r.BackColor = Color.FromArgb(35, 35, 35);
            //        r.Size = new Size(this.Width - 10, 40);
            //        r.ForeColor = Color.White;
            //        r.BorderRadius = 5;
            //        r.BorderSize = 3;
            //        r.Padding = new Padding(0, 0, r.Width / 2 - 5, 0);
            //        r.Margin = new Padding(8, 0, 0, 0);

            //        flowLayoutPanel.Controls.Add(r);
            //    }
            //}

        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
        //    e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        //    e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        //    //LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Aqua, Color.Blue, 90);
        //    SolidBrush brush1 = new SolidBrush(Color.Blue);
        //    Pen pen = new Pen(Color.Red, 1);

        //    using (GraphicsPath gp = new GraphicsPath())
        //    {
        //        gp.AddArc(new Rectangle(new Point(3, 3), new Size(20, 20)), 180, 90);
        //        gp.AddArc(new Rectangle(new Point(this.Width - 23, 3), new Size(20, 20)), -90, 90);
        //        gp.AddArc(new Rectangle(new Point(this.Width - 23, 43), new Size(20, 20)), 0, 90);
        //        gp.AddArc(new Rectangle(new Point(3, 43), new Size(20, 20)), 90, 90);
        //        gp.CloseFigure();

        //        //e.Graphics.FillPath(brush1, gp);
        //        e.Graphics.DrawPath(pen, gp);
        //    }

        //    base.OnPaint(e);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    class Shape
    {
        public static int R = 25;
        protected int x, y;
        protected int d_x, d_y;
        public Color lineC = Color.Black;
        public Color fillC = Color.LightPink;
        public bool is_checked = false;
        public bool is_polygon = false;
        public string text = "";

        public Shape(int x, int y) { this.x = x; this.y = y; }

        public Shape(int x, int y, string text){ this.text = text; this.x = x; this.y = y; }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public int D_X
        {
            get { return this.d_x; }
            set { this.d_x = value; }
        }

        public int D_Y
        {
            get { return this.d_y; }
            set { this.d_y = value; }
        }

        public void Draw(Graphics e)
        {
            e.FillEllipse(new SolidBrush(fillC), x - R, y - R, 2 * R, 2 * R);
            e.DrawEllipse(new Pen(lineC), x - R, y - R, 2 * R, 2 * R);
            Font drawFont = new Font("Times New Roman", 8);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.DrawString(text, drawFont, drawBrush, new Rectangle(x - 2 * R / 3, y - 2 * R / 3, 3 * R / 2, 3 * R / 2), drawFormat);
        }

        public bool IsInside(int x, int y)
        {
            return (((x - this.x) * (x - this.x) + (y - this.y) * (y - this.y)) <= R * R);
        }

        public Color FillC
        {
            get { return this.fillC; }
            set { this.fillC = value; }
        }
    }
}




using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace lab5
{
    // Form1 needs to be first in order for program display design
    public partial class Form1 : Form
    {
        // graphic_list will store each graphic object for painting
        ArrayList graphic_list = new ArrayList();
        private Point start, end;
        private bool firstClick =true;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(625, 550);
            this.listBox1.SelectedIndex = 0;
            this.listBox2.SelectedIndex = 0;
            this.listBox3.SelectedIndex = 0;
            this.radioButton1.Select();
        }

        // The Panel where we will draw the actual graphic objects
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Allows the panel to extend when the window is maximized
            panel2.Width = Width;
            panel2.Height = Height;

            Graphics g = e.Graphics;
            foreach(graphicObject Object in this.graphic_list)
            {
                Object.draw(g);
            }
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.graphic_list.Clear();
            panel2.Invalidate();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graphic_list.Count != 0)
            {
                graphic_list.RemoveAt(graphic_list.Count - 1);
                panel2.Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        // We use MouseDown to take care of any mouse button
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            // First click is used to set the start point
            if(this.firstClick)
            {
                this.start = e.Location;
                this.firstClick = false;    // No longer the first click, now we wait to assign the second location
                return;
            }
            // The second click sets the end point and resets us to first click
            this.end = e.Location;
            this.firstClick = true;

            // Sets the size of the pen
            int size = 0;
            switch(this.listBox3.SelectedIndex)
            {
                case 0:
                    size = 1;
                    break;
                case 1:
                    size = 2;
                    break;
                case 2:
                    size = 2;
                    break;
                case 3:
                    size = 4;
                    break;
                case 4: size = 5;
                    break;
                case 5:
                    size = 6;
                    break;
                case 6:
                    size = 7;
                    break;
                case 7:
                    size = 8;
                    break;
                case 8:
                    size = 9;
                    break;
                case 9:
                    size = 10;
                    break;

            }
            // We make a new pen and set the color and size;
            Pen p = new Pen(Color.Black, size);
            switch(this.listBox1.SelectedIndex)
            {
                case 0:
                    p = new Pen(Color.Black, size);
                    break;
                case 1:
                    p = new Pen(Color.Red, size);
                    break;
                case 2:
                    p = new Pen(Color.Blue, size);
                    break;
                case 3:
                    p = new Pen(Color.Green, size);
                    break;
            }
            // We make a new brush and set the color
            SolidBrush b = new SolidBrush(Color.White);
            switch(this.listBox2.SelectedIndex)
            {
                case 0:
                    b = new SolidBrush(Color.White);
                    break;
                case 1:
                    b = new SolidBrush(Color.Black);
                    break;
                case 2:
                    b = new SolidBrush(Color.Red);
                    break;
                case 3:
                    b = new SolidBrush(Color.Blue);
                    break;
                case 4:
                    b = new SolidBrush(Color.Green);
                    break;

            }

            // New brush to be used for text, will use the 4 pen colors
            SolidBrush b2 = new SolidBrush(Color.Black);
            switch (this.listBox1.SelectedIndex)
            {
                case 0:
                    b2 = new SolidBrush(Color.Black);
                    break;
                case 1:
                    b2 = new SolidBrush(Color.Red);
                    break;
                case 2:
                    b2 = new SolidBrush(Color.Blue);
                    break;
                case 3:
                    b2 = new SolidBrush(Color.Green);
                    break;
                

            }

            // Checks if the line button is selected
            if (this.radioButton1.Checked)
            {
                // Adds the graphic object Line to the array list and displays it
                this.graphic_list.Add(new Line(this.start, this.end, p));
                this.panel2.Invalidate();
            }
            // Checks if the rectangle button is checked
            if (this.radioButton2.Checked)
            {
                if (this.checkBox1.Checked && this.checkBox2.Checked)
                {
                    this.graphic_list.Add(new Rectangle(this.start, this.end, p, b));
                    this.panel2.Invalidate();
                }
                else if (this.checkBox1.Checked)
                {
                    this.graphic_list.Add(new Rectangle(this.start, this.end, null, b));
                    this.panel2.Invalidate();
                }
                else if (this.checkBox2.Checked)
                {
                    this.graphic_list.Add(new Rectangle(this.start, this.end, p, null));
                    this.panel2.Invalidate();
                }
                
            }
            // Checks if the ellipse button is checked
            if(this.radioButton3.Checked)
            {
                if (this.checkBox1.Checked && this.checkBox2.Checked)
                {
                    this.graphic_list.Add(new Ellipse(this.start, this.end, p, b));
                    this.panel2.Invalidate();
                }
                else if (this.checkBox1.Checked)
                {
                    this.graphic_list.Add(new Ellipse(this.start, this.end, null, b));
                    this.panel2.Invalidate();
                }
                else if(this.checkBox2.Checked)
                {
                    this.graphic_list.Add(new Ellipse(this.start, this.end, p, null));
                    this.panel2.Invalidate();
                }
                
            }
            // Checks if the Text button is checked
            if(this.radioButton4.Checked)
            {
                string text = this.textBox1.Text;
                Font f = new Font(this.Font, FontStyle.Regular);
                this.graphic_list.Add(new TextBox(text, b2, f, this.start, this.end));
                this.panel2.Invalidate();
            }
        }



        // graphic object class which will allow us to derive and draw
        public class graphicObject
        {
            // The draw function which all other objects will inherit
            public virtual void draw(Graphics g) { }
        }

        //==================================================================//
        // Line object
        public class Line : graphicObject
        {
            private Point start, end;
            private Pen linePen;


            // Default Constructor
            public Line(Point s, Point e, Pen lP)
            {
                this.start = s;
                this.end = e;
                this.linePen = lP;
            }

            public override void draw(Graphics g)
            {
                g.DrawLine(this.linePen, this.start, this.end);
            }
        }

        //==================================================================//
        // Rectangle Object
        public class Rectangle : graphicObject
        {
            private Point start, end;
            private Pen borderPen;
            private Brush fillBrush;

            // Default constructor
            public Rectangle(Point s, Point e, Pen b, Brush f)
            {
                this.start = s;
                this.end = e;
                this.borderPen = b;
                this.fillBrush = f;
            }

            public override void draw(Graphics g)
            {
                // Determine Location and size
                int width = Math.Abs(this.start.X - this.end.X);
                int height = Math.Abs(this.start.Y - this.end.Y);
                int startx = Math.Min(this.start.X, this.end.X);
                int starty = Math.Min(this.start.Y, this.end.Y);

                // Checks to see whether we have a border or filled rectangle or both.
                if (borderPen != null)
                {
                    g.DrawRectangle(this.borderPen, startx, starty, width, height);
                }
                if (fillBrush != null)
                {
                    g.FillRectangle(this.fillBrush, startx, starty, width, height);
                }
            }
        }
        //==================================================================//
        // Ellipse Object
        public class Ellipse : graphicObject
        {
            private Point start, end;
            private Pen borderPen;
            private Brush fillBrush;

            //Default Constructor
            public Ellipse(Point s, Point e, Pen b, Brush f)
            {
                this.start = s;
                this.end = e;
                this.borderPen = b;
                this.fillBrush = f;
            }

            public override void draw(Graphics g)
            {
                // Determine the location and size
                int width = Math.Abs(this.start.X - this.end.X);
                int height = Math.Abs(this.start.Y - this.end.Y);
                int startx = Math.Min(this.start.X, this.end.X);
                int starty = Math.Min(this.start.Y, this.end.Y);

                // Checks to see if we have a border or filled ellipse, or both
                if (borderPen != null)
                {
                    g.DrawEllipse(this.borderPen, startx, starty, width, height);
                }
                if (fillBrush != null)
                {
                    g.FillEllipse(this.fillBrush, startx, starty, width, height);
                }

            }
        }
        //==================================================================//
        // Textbox Object
        public class TextBox : graphicObject
        {
            private Point start, end;
            private Brush brush;
            private Font font;
            private string s;

            // Constructor
            public TextBox(string s, Brush b, Font f, Point st, Point e)
            {
                this.start = st;
                this.end = e;
                this.s = s;
                this.brush = b;
                this.font = f;
            }

            public override void draw(Graphics g)
            {
                // Determine the location and size
                int width = Math.Abs(this.start.X - this.end.X);
                int height = Math.Abs(this.start.Y - this.end.Y);
                int startx = Math.Min(this.start.X, this.end.X);
                int starty = Math.Min(this.start.Y, this.end.Y);

                // Draw a string at Pointf location and size of width and height
                g.DrawString(s, font, brush, new RectangleF(new Point(startx, starty), new SizeF(width, height)));
            }
        }

    }
}

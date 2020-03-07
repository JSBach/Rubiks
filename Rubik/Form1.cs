using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rubik
{
    public partial class Form1 : Form
    {
        //NOTE: x axis points right, y axis - left, z axis - from the screen
        private Point3D[] zplanesegment = new Point3D[] { new Point3D { X = 100, Y = -50, Z = 0 }, new Point3D { X = 100, Y = 50, Z = 0 } };
        private Point3D[] zplanesquare = new Point3D[] { new Point3D { X = -50, Y = -50, Z = 0 },
            new Point3D { X = -50, Y = 50, Z = 0 },
            new Point3D { X = 50, Y = 50, Z = 0 },
            new Point3D { X = 50, Y = -50, Z = 0 }};

        private Point3D[][] cube = null;

        private float screenoriginX = 300;
        private float screenoriginY = 200;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawSquares(e.Graphics, null);
        }

        private void DrawSquares(Graphics g, Point3D[] squares)
        {
            //int l = squares.Length / 4;
            var rotatedpoints = RotateY(zplanesquare, Math.PI);
            var points = ProjectOnZ(rotatedpoints);
            MoveToScreenOrigin(points);

            //drawing the origin
            //g.DrawEllipse(Pens.Black, screenoriginX, screenoriginY, 5, 5);

            //for(int i = 0; i < l; i++)
            //{
            //    float x = points[i * 4].X;
            //    float y = points[i * 4].Y;
            //    float w = Math.Abs(points[i * 4].X - points[i * 4 + 2].X);
            //    float h = 
            //    g.FillRectangle(Brushes.Green, )
            //}

            g.FillRectangle(Brushes.Red, 100, 200, 100, 100);
            g.FillRectangle(Brushes.Blue, 300, 200, -100.0F, 100);

            //g.DrawPolygon(Pens.Green, points);
            //g.DrawLines(Pens.Green, points);
        }

        private PointF[] ProjectOnZ(Point3D[] points)
        {
            PointF[] retval = new PointF[points.Length];

            for(int i = 0; i < points.Length; i++)
            {
                retval[i] = new PointF { X = points[i].X, Y = points[i].Y };
            }

            return retval;
        }

        private void MoveToScreenOrigin(PointF[] points)
        {
            for(int i = 0; i < points.Length; i++)
            {
                points[i].X += screenoriginX;
                points[i].Y = screenoriginY - points[i].Y;
            }
        }

        private Point3D[] RotateX(Point3D[] points, double angle)
        {
            Point3D[] retval = new Point3D[points.Length];
            float y, z;

            for (int i = 0; i < points.Length; i++)
            {
                y = (float)(points[i].Y * Math.Cos(angle) - points[i].Z * Math.Sin(angle));
                z = (float)(points[i].Y * Math.Sin(angle) + points[i].Z * Math.Cos(angle));

                retval[i] = new Point3D();
                retval[i].X = points[i].X;
                retval[i].Y = y;
                retval[i].Z = z;
            }

            return retval;
        }

        private Point3D[] RotateY(Point3D[] points, double angle)
        {
            Point3D[] retval = new Point3D[points.Length];
            float x, z;

            for (int i = 0; i < points.Length; i++)
            {
                x = (float)(points[i].X * Math.Cos(angle) + points[i].Z * Math.Sin(angle));
                z = (float)(-points[i].X * Math.Sin(angle) + points[i].Z * Math.Cos(angle));

                retval[i] = new Point3D();
                retval[i].X = x;
                retval[i].Y = points[i].Y;
                retval[i].Z = z;
            }

            return retval;
        }

        private Point3D[] RotateZ(Point3D[] points, double angle)
        {
            Point3D[] retval = new Point3D[points.Length];
            float x, y;

            for(int i = 0; i < points.Length; i++)
            {
                x = (float)(points[i].X * Math.Cos(angle) - points[i].Y * Math.Sin(angle));
                y = (float)(points[i].X * Math.Sin(angle) + points[i].Y * Math.Cos(angle));

                retval[i] = new Point3D();
                retval[i].X = x;
                retval[i].Y = y;
                retval[i].Z = points[i].Z;
            }

            return retval;
        }

        private Point3D[] CreateCube(Point3D center, float size)
        {
            Point3D[] retval = new Point3D[24];
            size = size / 2;
            int i = 0;

            //Front side
            retval[i++] = new Point3D { X = center.X - size, Y = center.Y + size, Z = center.Z + size };
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y + size, Z = center.Z + size };
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y - size, Z = center.Z + size };
            retval[i++] = new Point3D { X = center.X - size, Y = center.Y - size, Z = center.Z + size };

            //Right side
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y + size, Z = center.Z + size };
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y + size, Z = center.Z - size };
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y - size, Z = center.Z - size };
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y - size, Z = center.Z + size };

            //Back side
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y + size, Z = center.Z - size };
            retval[i++] = new Point3D { X = center.X - size, Y = center.Y + size, Z = center.Z - size };
            retval[i++] = new Point3D { X = center.X - size, Y = center.Y - size, Z = center.Z - size };
            retval[i++] = new Point3D { X = center.X + size, Y = center.Y - size, Z = center.Z - size };

            return retval;
        }

    }


    
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Labs
{
    public partial class FormL2 : Form
    {
        float x0;
        float x1;
        float x2;
        float x3;
        float x4;
        float y0;
        float y1;
        float y2;
        float y3;
        float y4;
        float P0;
        float P1;
        float P2;
        float P3;
        float P4;
        float k0;
        float k1;
        float k2;
        float k3;
        float k4;
        string res = "";
        Graphics g2;

        public FormL2()
        {
            InitializeComponent();
            textBoxX0.Text = "0,083";
            textBoxX1.Text = "0,472";
            textBoxX2.Text = "1,347";
            textBoxX3.Text = "2,117";
            textBoxX4.Text = "2,947";
            textBoxY0.Text = "-2,132";
            textBoxY1.Text = "-2,013";
            textBoxY2.Text = "-1,613";
            textBoxY3.Text = "-0,842";
            textBoxY4.Text = "2,973";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            res = "";
            g2 = panelG1.CreateGraphics();
            g2.Clear(panelG1.BackColor);
            x0 = float.Parse(textBoxX0.Text);
            x1 = float.Parse(textBoxX1.Text);
            x2 = float.Parse(textBoxX2.Text);
            x3 = float.Parse(textBoxX3.Text);
            x4 = float.Parse(textBoxX4.Text);
            y0 = float.Parse(textBoxY0.Text);
            y1 = float.Parse(textBoxY1.Text);
            y2 = float.Parse(textBoxY2.Text);
            y3 = float.Parse(textBoxY3.Text);
            y4 = float.Parse(textBoxY4.Text);

            //полином Лангранжа
            CalcPolinom();
            CalcKoefLangr();
            StringPolinom();
            textBoxR1.Text = res;
            DrawPolinom();
        }

        private void CalcPolinom()
        {
            P0 = 1 / ((x0 - x1) * (x0 - x2) * (x0 - x3) * (x0 - x4));
            P1 = 1 / ((x1 - x0) * (x1 - x2) * (x1 - x3) * (x1 - x4));
            P2 = 1 / ((x2 - x0) * (x2 - x1) * (x2 - x3) * (x2 - x4));
            P3 = 1 / ((x3 - x0) * (x3 - x1) * (x3 - x2) * (x3 - x4));
            P4 = 1 / ((x4 - x0) * (x4 - x1) * (x4 - x2) * (x4 - x3));
            res += "P0 = " + P0 + "P1 = " + P1 + "P2 = " + P2 + "P3 = " + P3 + "P4 = " + P4+"\r\n";
        }

        private void CalcKoefLangr()
        {
            k0 = P0 * y0;
            k1 = P1 * y1;
            k2 = P2 * y2;
            k3 = P3 * y3;
            k4 = P4 * y4;
            res += "k0 = " + k0 + "k1 = " + k1 + "k2 = " + k2 + "k3 = " + k3 + "k4 = " + k4 + "\r\n\r\n";
        }

        private void StringPolinom()
        {
            res += P0 * y0 + "*(x-( " + x1 + "))*(x-(" + x2 + "))*(x-(" + x3 + "))*(x-(" + x4 + "))\r\n" +
            P1 * y1 + "*(x-( " + x0 + "))*(x-(" + x2 + "))*(x-(" + x3 + "))*(x-(" + x4 + "))\r\n" +
            P2 * y2 + "*(x-( " + x0 + "))*(x-(" + x1 + "))*(x-(" + x3 + "))*(x-(" + x4 + "))\r\n" +
            P3 * y3 + "*(x-( " + x0 + "))*(x-(" + x1 + "))*(x-(" + x2 + "))*(x-(" + x4 + "))\r\n" +
            P4 * y4 + "*(x-( " + x0 + "))*(x-(" + x1 + "))*(x-(" + x2 + "))*(x-(" + x3 + "))";
        }

        private void DrawPolinom()
        {
            g2.TranslateTransform(panelG1.Width / 2, panelG1.Height / 2);
            g2.ScaleTransform(1, -1);
            Pen myPen1 = new Pen(Color.Blue);
            g2.DrawLine(myPen1, 0, g2.Transform.OffsetY, 0, -g2.Transform.OffsetY);
            g2.DrawLine(myPen1, 0, 0, g2.Transform.OffsetX, 0);
            Pen myPen2 = new Pen(Color.Red);
            float h2 = float.Parse(0.1 + "");
            for (float i = 0; i < 5; i += h2)
            {
                float y = k0 * (i - x1) * (i - x2) * (i - x3) * (i - x4) +
                k1 * (i - x0) * (i - x2) * (i - x3) * (i - x4) +
                k2 * (i - x0) * (i - x1) * (i - x3) * (i - x4) +
                k3 * (i - x0) * (i - x1) * (i - x2) * (i - x4) +
                k4 * (i - x0) * (i - x1) * (i - x2) * (i - x2);
                float yN = k0 * (i + h2 - x1) * (i + h2 - x2) * (i + h2 - x3) * (i + h2 - x4) +
                k1 * (i + h2 - x0) * (i + h2 - x2) * (i + h2 - x3) * (i + h2 - x4) +
                k2 * (i + h2 - x0) * (i + h2 - x1) * (i + h2 - x3) * (i + h2 - x4) +
                k3 * (i + h2 - x0) * (i + h2 - x1) * (i + h2 - x2) * (i + h2 - x4) +
                k4 * (i + h2 - x0) * (i + h2 - x1) * (i + h2 - x2) * (i + h2 - x2);
                g2.DrawLine(myPen2, float.Parse(i.ToString()) * 20, (y) * 10, (i + h2) * 20, (yN) * 10);
            }
        }
    }
}

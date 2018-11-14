using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class FormMain : Form
    {
        double X = 78.5457;
        double a = 2.751;
        double b = 1.215;
        double c = 0.1041;
        string Z = "(a-Sin(b))/(b*b+6*c)";
        string res;

        public FormMain()
        {
            InitializeComponent();
            textBoxX.Text = X + "";
            textBoxa.Text = a + "";
            textBoxb.Text = b + "";
            textBoxc.Text = c + "";
            textBoxZ.Text = Z + "";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            res = "1)    " + first(X) + "\r\n";
            res += "2)    " + "НГ = ";
            secondAsync(false);
            res += "ВГ = ";
            secondAsync(true);
            
            textBoxMain.Text = res;
        }

        private string first(double x)
        {
            double x1 = getRound(3, x);
            double dx1 = getRound(1, Math.Abs(x - x1));
            double d = 0.05;
            double sigma = dx1 / Math.Abs(x1) * 100;
            return "X1 = " + x1 + "\t dX1 = " + dx1 + "\t delta = " + d + "\t sigma = " + sigma;
        }

        private double getRound(int digs, double d)
        {
            double r;
            int magnitudeOfNumber = (int)Math.Floor(Math.Log10(d));
            int numberOfDigits = (int)Math.Ceiling(Math.Log10(d));
            if (numberOfDigits == digs)
            {
                r = d;
            }
            else
            {
                double dp = Math.Pow(10, magnitudeOfNumber);
                double dd = Math.Pow(10, digs - 1);
                r = d / dp;
                r = (Math.Round(r * dd) / dd) * dp;
            }
            return r;
        }

        private async void secondAsync(bool down)
        {
            var result = await CSharpScript.EvaluateAsync(form(down), ScriptOptions.Default.WithImports("System.Math"));
            res += result + "\r\n";
        }

        private string form(bool down)
        {
            string str = "";
            for (int i = 0; i < Z.Length; i++)
            {
                switch (Z[i])
                {
                    case 'a':
                        double aR = 0.0;
                        if (down)
                        {
                            aR = a - dBorder(a);
                        }
                        else
                        {
                            aR = a + dBorder(a);
                        }
                        str += aR;
                        break;
                    case 'b':
                        double bR = 0.0;
                        if (Z[i - 2].ToString().Length!=0&&(Z[i - 2] == 'n'|| Z[i - 2] == 's'|| Z[i - 2] == 'g'))
                        {
                            double bdigrees = b * (Math.PI / 180.0);
                            if (down)
                            {
                                bR = bdigrees - dBorder(bdigrees);
                            }
                            else
                            {
                                bR = bdigrees + dBorder(bdigrees);
                            }
                            str += bR;
                            break;
                        }
                        if (down)
                        {
                            bR = b - dBorder(b);
                        }
                        else
                        {
                            bR = b + dBorder(b);
                        }
                        str += bR;
                        break;
                    case 'c':
                        double cR = 0.0;
                        if (down)
                        {
                            cR = c - dBorder(c);
                        }
                        else
                        {
                            cR = c + dBorder(c);
                        }
                        str += cR;
                        break;
                    default:
                        str += Z[i];
                        break;
                }
            }
            str = str.Replace(',', '.');
            return str;
        }

        private double dBorder(double num)
        {
            int a = 0;
            string da = "0,";
            while (num * Math.Pow(10, 1 + a) % 10 != 0)
            {
                a++;
                da += "0";
            }
            da += "5";
            return Convert.ToDouble(da);
        }
    }
}

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
            textBoxa.Text = a+"";
            textBoxb.Text = b + "";
            textBoxc.Text = c + "";
            textBoxZ.Text = Z + "";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            res = "1)    "+first(X)+"\n\n";
            res += "2)    " +"Правила подсчета цифр:    Z = ";
            secondFAsync();

            textBoxMain.Text = res;
        }

        private string first(double x)
        {
            double x1 = getRound(3, x);
            double dx1 = getRound(1,Math.Abs(x - x1));
            double d = 0.05;
            double sigma = dx1 / Math.Abs(x1)*100;
            return "X1 = "+x1+"\t dX1 = "+dx1+"\t delta = "+d+"\t sigma = "+sigma;
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

        private async void secondFAsync()
        {
            var result = await CSharpScript.EvaluateAsync(form(), ScriptOptions.Default.WithImports("System.Math"));
        }

        private string form()
        {
            string str = "";
            for(int i = 0; i < Z.Length; i++)
            {
                switch (Z[i])
                {
                    case 'a':
                        str += a;
                        break;
                    case 'b':
                        str += b;
                        break;
                    case 'c':
                        str += c;
                        break;
                    default:
                        str += Z[i];
                        break;
                }
            }
            str=str.Replace(',', '.');
            return str;
        }
    }
}

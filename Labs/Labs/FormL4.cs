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
    public partial class FormL4 : Form
    {
        private string res = "";
        private string function = "0.5*2.7*(-Sqrt(x))-0.2*Sqrt(Pow(x,3.0))+2";
        private string fiFunction = "";
        private int k = 2;
        double temp = 0.0;

        public FormL4()
        {
            InitializeComponent();

            textBoxF.Text = function;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            function = textBoxF.Text;
            res += "fun = " + function + "  k = " + k + "\r\n";
            
            hord();
            simpleIt();

            textBoxRes.Text = res;
        }

        private void hord()
        {
            res += "Нахождение корня уравнения по методу хорд:\r\n";
            double[] x = new double[5];
            double[] f = new double[5];
            x[0] = 1;
            x[1] = 2;
            mathAsync(form(x[0], function));
            f[0] = temp;
            mathAsync(form(x[1], function));
            f[1] = temp;
            double d = 10.0;
            for (int i = 2; i < x.Length&&d>0.0022; i++)
            {
                x[i] = x[i-2] - (f[i - 2] / (f[i - 1] - f[i - 2])) * (x[i - 1] - x[i - 2]);
                mathAsync(form(x[i], function));
                f[i] = temp;
                d= Math.Abs(x[i-1] - x[i]);
            }

            res += "x0 = " + x[0] + "\t\t\t f0 = " + f[0] + "\r\n" + "x1 = " + x[1] + "\t\t\t f1 = " + f[1] + "\r\n" + "x2 = " + x[2] + "\t\t f2 = " + f[2] + "\r\n" + "x3 = " + x[3] + "\t\t f3 = " + f[3] + "\r\n" + "x4 = " + x[4] + "\t\t f4 = " + f[4] + "\r\n";
            
            res += "dx = " + d + "\r\n\r\n";
        }

        private void simpleIt()
        {
            res += "Нахождение корня уравнения по методу простых итераций:\r\n";
            double[] x = new double[5];
            double[] fi = new double[5];

            fiFunction = "(-x/" + k + "-" + function+")/"+k;
            res += "fiFun = " + fiFunction + "\r\n";
            x[0] = 1;
            mathAsync(form(x[0], fiFunction));
            fi[0] = temp;
            double d = 10.0;

            for (int i = 1; i < x.Length&&d>0.005; i++)
            {
                x[i] = fi[i-1]; ;
                mathAsync(form(x[i], fiFunction));
                fi[i] = temp;
                d = Math.Abs(x[i - 1] - x[i]);
            }
            res += "x0 = " + x[0] + "\t\t\t fi0 = " + fi[0] + "\r\n"+ "x1 = " + x[1] + "\t\t\t fi1 = " + fi[1] + "\r\n" + "x2 = " + x[2] + "\t\t\t fi2 = " + fi[2] + "\r\n" + "x3 = " + x[3] + "\t\t\t fi3 = " + fi[3] + "\r\n" + "x4 = " + x[4] + "\t\t\t fi4 = " + fi[4] + "\r\n";
            
            res += "dx = " + d + "\r\n\r\n";
        }

        private async void mathAsync(string s)
        {
            temp = Convert.ToDouble(await CSharpScript.EvaluateAsync(s, ScriptOptions.Default.WithImports("System.Math")));
        }

        private string form(double x, string f)
        {
            string res = "";
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == 'x'&&f[i+1]!='p') res += x.ToString().Replace(',', '.');
                else res += f[i];
            }
            return res;
        }
    }
}

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
    public partial class FormL3 : Form
    {
        string formula = "Sqrt(x)*Log(x)";
        int n = 36;
        double h = 0.084;
        int a = 1;
        int b = 4;
        string resultat = "";
        double temp = 0.0;

        double[] xh;
        double[] fxh;
        double[] x2h;
        double[] fx2h;
        double tZnachH;
        double tZnach2H;
        double sZnachH;
        double sZnach2H;

        public FormL3()
        {
            InitializeComponent();
            textBoxF.Text = formula;
            textBoxa.Text = a + "";
            textBoxb.Text = b + "";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                formula = textBoxF.Text;
                a = Convert.ToInt32(textBoxa.Text);
                b = Convert.ToInt32(textBoxb.Text);
            }
            catch (Exception ex)
            {
                textBoxRes.Text = "Данные введены не правильно! :( \r\n" + ex;
                return;
            }

            trapeciy();
            simpson();
            textBoxRes.Text = resultat;
        }

        private void trapeciy()
        {
            resultat += "По формуле трапеций:\r\n";
            xh = new double[n];
            xh[0] = a;
            resultat += "С шагом h: \r\n x = { ";
            for (int i = 1; i < xh.Length; i++)
            {
                xh[i] = xh[i - 1] + h;
                resultat += xh[i] + " , ";
            }
            resultat += "} \r\n\r\n";

            resultat += " f(x) = { ";
            fxh = new double[n];
            for (int i = 0; i < fxh.Length; i++)
            {
                mathAsync(form(xh[i], formula));
                fxh[i] = temp;
                resultat += fxh[i] + " , ";
            }
            resultat += "} \r\n\r\n";

            //значение h
            double sumh = 0;
            for (int i = 1; i < fxh.Length - 2; i++)
            {
                sumh += fxh[i];
            }
            tZnachH = h * ((fxh[0] + fxh[n - 1]) / 2 + sumh);
            resultat += "I(h) = " + tZnachH + "\r\n\r\n";

            x2h = new double[n / 2];
            x2h[0] = a;
            resultat += "С шагом 2h: \r\n x = { ";
            for (int i = 1; i < x2h.Length; i++)
            {
                x2h[i] = x2h[i - 1] + h * 2;
                resultat += x2h[i] + " , ";
            }
            resultat += "} \r\n\r\n";

            resultat += " f(x) = { ";
            fx2h = new double[n / 2];
            for (int i = 0; i < fx2h.Length; i++)
            {
                mathAsync(form(x2h[i], formula));
                fx2h[i] = temp;
                resultat += fx2h[i] + " , ";
            }
            resultat += "} \r\n\r\n";

            //значение 2h
            double sum2h = 0;
            for (int i = 1; i < fx2h.Length - 2; i++)
            {
                sum2h += fx2h[i];
            }
            tZnach2H = h * 2 * ((fx2h[0] + fx2h[n / 2 - 1]) / 2 + sum2h);
            resultat += "I(2h) = " + tZnach2H + "\r\n\r\n";

            resultat += "I(h) - I(2h) = " + (tZnachH - tZnach2H) / 3 + "\r\n";
            resultat += "Значение интеграла = " + tZnachH + " +- " + (tZnachH - tZnach2H) / 3 + "\r\n\r\n";
        }

        private void simpson()
        {
            resultat += "По формуле Симпсона:\r\n";
            resultat += " чет/нечет h = { ";
            double[] chetNechH = new double[n - 2];
            for(int i=0;i< chetNechH.Length; i++)
            {
                if ((i + 1) % 2 == 0)
                    chetNechH[i] = fxh[i + 1] * 2;
                else chetNechH[i] = fxh[i + 1] * 4;
                resultat += chetNechH[i] + " , ";
            }
            resultat += "} \r\n\r\n";

            resultat += " чет/нечет 2h = { ";
            double[] chetNech2H = new double[n/2 - 2];
            for (int i = 0; i < chetNech2H.Length; i++)
            {
                if ((i + 1) % 2 == 0)
                    chetNech2H[i] = fx2h[i + 1] * 2;
                else chetNech2H[i] = fx2h[i + 1] * 4;
                resultat += chetNech2H[i] + " , ";
            }
            resultat += "} \r\n\r\n";

            //значение h
            double sumh = 0;
            for (int i = 0; i < chetNechH.Length - 1; i++)
            {
                sumh += chetNechH[i];
            }
            sZnachH = h/3 * (fxh[0] +fxh[n - 1] + sumh);
            resultat += "I(h) = " + sZnachH + "\r\n\r\n";

            //значение 2h
            double sum2h = 0;
            for (int i = 0; i < chetNech2H.Length - 1; i++)
            {
                sum2h += chetNech2H[i];
            }
            sZnach2H = h / 3 * (fx2h[0] + fx2h[n/2 - 1] + sum2h);
            resultat += "I(h) = " + sZnach2H + "\r\n\r\n";

            resultat += "I(h) - I(2h) = " + (sZnachH - sZnach2H) / 15 + "\r\n";
            resultat += "Значение интеграла = " + sZnachH + " +- " + (sZnachH - sZnach2H) / 15 + "\r\n\r\n";
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
                if (f[i] == 'x') res += x.ToString();
                else res += f[i];
            }
            res = res.Replace(',', '.');
            return res;
        }
    }
}

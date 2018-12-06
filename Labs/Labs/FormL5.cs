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
    public partial class FormL5 : Form
    {
        string res = "Метод релаксации.\r\n";
        double[,] rX = new double[,] { { 1, 0.0961, 0.0674, 0.0524 }, { 0.0611, 1, 0.0872, 0.0574 }, { 0.1109, 0.0841, 1, 0.0824 }, { 0.1174, 0.0616, 0.0579, 1 } };
        int k = 9;
        int temp = 10;

        public FormL5()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            seven();
            textBoxRes.Text = res;
        }

        private void seven()
        {
            res += "Система, готовая к релаксации:\r\n";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    res += rX[i, j] + "\t";
                }
                res += "\r\n";
            }
            res += "\r\n\r\nСчитаем невязки пока не выполнится неравенство R<0.001:\r\n";

            double[,] r = new double[k, 5];
            r[0, 0] = 0.045;
            r[0, 1] = 0.0478;
            r[0, 2] = 0.1527;
            r[0, 3] = 0.0513;
            r[0,4] = max(new double[] { r[0, 0], r[0, 1], r[0, 2], r[0, 3] });
            for (int i = 1; ; i++)
            {
                r[i, 0] = Math.Round(r[i - 1, 0] - r[i - 1, temp] * rX[temp, 0],5);
                r[i, 1] = Math.Round(r[i - 1, 1] - r[i - 1, temp] * rX[temp, 1], 5);
                r[i, 2] = Math.Round(r[i - 1, 2] - r[i - 1, temp] * rX[temp, 2], 5);
                r[i, 3] = Math.Round(r[i - 1, 3] - r[i - 1, temp] * rX[temp, 3], 5);
                r[i, 4] = max(new double[] { r[i, 0], r[i, 1], r[i, 2], r[i, 3] });
                if (r[i, 4] < 0.001)
                {
                    k = i;
                    break;
                }
            }

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    res += r[i, j] + "\t";
                }
                res += "\r\n";
            }
            res += "\r\n\r\n Выделяем корни:\r\n";

            double[,] bx = new double[k, 4];
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (r[i, 4] == Math.Abs(r[i, j]))
                    {
                        bx[i, j] = r[i, j];
                    }
                    else bx[i, j] = 0;
                    res += bx[i, j] + "\t\t";
                }
                res += "\r\n";
            }
            res += "\r\n\r\n";

            double bx1 = 0.0, bx2 = 0.0, bx3 = 0.0, bx4 = 0.0;
            for (int i = 0; i < k; i++)
            {
                bx1 += bx[i, 0];
                bx2 += bx[i, 1];
                bx3 += bx[i, 2];
                bx4 += bx[i, 3];
            }
            res += "Значения корней: \r\nx1 = " + bx1 + "\tx2 = " + bx2 + "\tx3 = " + bx3 + "\tx4 = " + bx4;
        }

        private double max(double [] nums)
        {
            double res = Math.Abs(nums[0]);
            temp = 0;
            for(int i = 1; i < nums.Length; i++)
            {
                if (res < Math.Abs(nums[i]))
                {
                    res = Math.Abs(nums[i]);
                    temp = i;
                }
            }
            return res;
        }

        private double sum(double[] nums)
        {
            double res = 0.0;
            for(int i = 0; i < nums.Length; i++)
            {
                res += nums[i];
            }
            return res;
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR5
{
    public partial class FormLR5 : Form
    {
        Timer timer = new Timer();
        bool iSWork1 = false; // Показывает идёт ли поиск значения по методу Крамера
        bool iSWork2 = false; // Показывает идёт ли поиск значения по методу Жордана
        double finalDet; //Будет вычислятся главный определитель. Если он равен 0, то вычисления не производятся.

        double[,] TaskArray = new double[4, 5]; // Матрица системы. Неизменная константа для вычислений.
                                                // Генерируется при нажатии по кнопке "Вычислить"

        // Во всём проекте:
        // i - строки матрицы (rows)
        // j - столбцы матрицы (columns)

        public FormLR5()
        {
            InitializeComponent();
            timer.Interval = 50;
            timer.Tick += timer_Tick;
        }

        private void buttonCloseApp_Click(object sender, EventArgs e) => Application.Exit();

        private void buttonFindElements_Click(object sender, EventArgs e)
        {
            if (iSWork1 == false && iSWork2 == false)
            {
                Work();
                if (initArray())
                {
                    finalDet = KramerAlgorithm.MainDeterminant(TaskArray);
                    if (finalDet != 0)
                    {
                        FindElementsKramer();
                        FindElementsJordan();
                    }
                }
            }
        }

        private void Work()
        {
            buttonFindElements.Enabled = false;
            labelIsFindsElements.Visible = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (iSWork1 == false || iSWork2 == false)
            {
                timer.Stop();
                buttonFindElements.Enabled = true;
                labelIsFindsElements.Visible = false;
            }
        }

        private bool initArray()
        {
            try
            {
                TaskArray[0, 0] = Convert.ToDouble(textBox1.Text.Trim()); TaskArray[0, 1] = Convert.ToDouble(textBox2.Text.Trim()); 
                TaskArray[0, 2] = Convert.ToDouble(textBox3.Text.Trim()); TaskArray[0, 3] = Convert.ToDouble(textBox4.Text.Trim()); 
                TaskArray[0, 4] = Convert.ToDouble(textBox5.Text.Trim());

                TaskArray[1, 0] = Convert.ToDouble(textBox6.Text.Trim()); TaskArray[1, 1] = Convert.ToDouble(textBox7.Text.Trim());
                TaskArray[1, 2] = Convert.ToDouble(textBox8.Text.Trim()); TaskArray[1, 3] = Convert.ToDouble(textBox9.Text.Trim());
                TaskArray[1, 4] = Convert.ToDouble(textBox10.Text.Trim());

                TaskArray[2, 0] = Convert.ToDouble(textBox11.Text.Trim()); TaskArray[2, 1] = Convert.ToDouble(textBox12.Text.Trim());
                TaskArray[2, 2] = Convert.ToDouble(textBox13.Text.Trim()); TaskArray[2, 3] = Convert.ToDouble(textBox14.Text.Trim());
                TaskArray[2, 4] = Convert.ToDouble(textBox15.Text.Trim());

                TaskArray[3, 0] = Convert.ToDouble(textBox16.Text.Trim()); TaskArray[3, 1] = Convert.ToDouble(textBox17.Text.Trim());
                TaskArray[3, 2] = Convert.ToDouble(textBox18.Text.Trim()); TaskArray[3, 3] = Convert.ToDouble(textBox19.Text.Trim());
                TaskArray[3, 4] = Convert.ToDouble(textBox20.Text.Trim());
                return true;
            } catch
            {
                MessageBox.Show("Перепровверьте: правильно ли Вы вписали переменные.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async void FindElementsJordan() // Вычисление методом Гаусса — Жордана
        {
            iSWork2 = true;
            listBox2.Items.Clear();

            double x1 = 0, x2 = 0, x3 = 0, x4 = 0;

            await Task.Run(() =>
            {
                Tuple<double, double, double, double> FindX = JordanAlgorithm.FindJordan(TaskArray);

                x1 = FindX.Item1;
                x2 = FindX.Item2;
                x3 = FindX.Item3;
                x4 = FindX.Item4;
            });
            iSWork2 = false;

            listBox2.Items.Add("X1 = " + x1);
            listBox2.Items.Add("X2 = " + x2);
            listBox2.Items.Add("X3 = " + x3);
            listBox2.Items.Add("X4 = " + x4);

        }

        private async void FindElementsKramer() // Вычисление методом Крамера
        {
            iSWork1 = true;
            listBox1.Items.Clear();

            double x1 = -1, x2 = -1, x3 = -1, x4 = -1;

            await Task.Run(() =>
            {
                Tuple<double, double, double, double> FindX = KramerAlgorithm.FindKramer(TaskArray);

                x1 = FindX.Item1;
                x2 = FindX.Item2;
                x3 = FindX.Item3;
                x4 = FindX.Item4;
            });
            iSWork1 = false;

            listBox1.Items.Add("X1 = " + x1);
            listBox1.Items.Add("X2 = " + x2);
            listBox1.Items.Add("X3 = " + x3);
            listBox1.Items.Add("X4 = " + x4);
        }
    }
}

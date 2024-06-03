using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЛР15_1
{
    public partial class Form1 : Form
    {
        // Объявление массива и флага
        private int[] array = new int[100];
        private bool empty = false;
        // Конструктор формы
        public Form1()
        {
            InitializeComponent();
        }
        // Сортировка пузырьком
        static int BubbleSort(int[] Array)
        {
            int iterations = 0;
            for (int i = 0; i < Array.Length - 1; i++)
            {
                for (int j = i + 1; j < Array.Length; j++)
                {
                    iterations++;
                    if (Array[i] > Array[j])
                    {
                        int t = Array[i];
                        Array[i] = Array[j];
                        Array[j] = t;
                    }
                }
            }
            return iterations;
        }
        // Сортировка выбором
        static int SelectionSort(int[] Array)
        {
            int iterations = 0;
            for (int i = 0; i < Array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Array.Length; j++)
                {
                    iterations++;
                    if (Array[j] < Array[min])
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    int t = Array[i];
                    Array[i] = Array[min];
                    Array[min] = t;
                }
            }
            return iterations;
        }
        // Сортировка быстрая
        static int QuickSort(int[] Array, int Left, int Right)
        {
            int iterations = 0;
            int i = Left;
            int j = Right;
            int x = Array[(Left + Right) / 2];
            do
            {
                while (Array[i] < x)
                {
                    iterations++;
                    ++i;
                }
                while (Array[j] > x)
                {
                    iterations++;
                    --j;
                }
                if (i <= j)
                {
                    int t = Array[i];
                    Array[i] = Array[j];
                    Array[j] = t;
                    i++;
                    j--;
                }
            } while (i <= j);
            if (Left < j)
                iterations += QuickSort(Array, Left, j);
            if (i < Right)
                iterations += QuickSort(Array, i, Right);
            return iterations;
        }
        // Линейный поиск
        static int[] IndexOf(int[] Array, int Value)
        {
            int[] Out = new int[2];
            Out[0] = 0;
            Out[1] = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                Out[0]++;
                if (Array[i] == Value)
                {
                    Out[1] = i + 1;
                    return Out;
                }
            }
            Out[1] = -1;
            return Out;
        }
        // Метод дихотомии
        static int[] BinarySearch(int[] Array, int Value)
        {
            int[] Out = new int[2];
            Out[0] = 0;
            Out[1] = 0;
            int Left = 0;
            int Right = Array.Length - 1;
            while (Left <= Right)
            {
                Out[0]++;
                int mid = Left + (Right - Left) / 2;
                if (Array[mid] == Value)
                {
                    Out[1] = mid + 1;
                    return Out;
                }
                else if (Array[mid] < Value)
                    Left = mid + 1;
                else
                    Right = mid - 1;
            }
            Out[1] = -1;
            return Out;
        }
        //Создание массива и заполнение
        private void button1_Click(object sender, EventArgs e)
        {
            empty = false;
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 101);
            }

            listBox1.Items.Clear();
            foreach (var item in array)
            {
                listBox1.Items.Add(item);
            }
        }
        // Выбор сортировок
        private void button2_Click(object sender, EventArgs e)
        {
            int Iterations = 0;
            if (array[0] == 0)
            {
                MessageBox.Show("Пустой массив!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (radioButton1.Checked)
            {
                Iterations = SelectionSort(array);
                empty = true;
            }
            else if (radioButton2.Checked)
            {
                Iterations = QuickSort(array, 0, array.Length - 1);
                empty = true;
            }
            else if (radioButton3.Checked)
            {
                Iterations = BubbleSort(array);
                empty = true;
            }
            else
            {
                listBox2.Items.Clear();
                listBox2.Items.Add("Выберите тип сортировки");
                return;
            }
            listBox1.Items.Clear();
            foreach (var item in array)
            {
                listBox1.Items.Add(item);
            }
            listBox2.Items.Clear();
            listBox2.Items.Add(Iterations);
        }
        //Выбор поиска
        private void button3_Click(object sender, EventArgs e)
        {
            int[] Output = new int[2];
            int Value;
            if (array[0] == 0)
            {
                MessageBox.Show("Пустой массив!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBox1.Text, out Value) || textBox1.Text == "")
            {
                MessageBox.Show("Некорректный ввод данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Value = int.Parse(textBox1.Text);
            if (radioButton4.Checked)
            {
                Output = IndexOf(array, Value);
            }
            else if (radioButton5.Checked)
            {
                if (empty)
                {
                    Output = BinarySearch(array, Value);
                }
                else
                {
                    listBox2.Items.Clear();
                    listBox2.Items.Add("Метод дихотомии");
                    listBox2.Items.Add("работает только на");
                    listBox2.Items.Add("отсортированном массиве");
                    return;
                }
            }
            else
            {
                listBox3.Items.Clear();
                listBox3.Items.Add("Выберите тип поиска");
                return;
            }
            if (Output[1] == -1)
            {
                listBox3.Items.Clear();
                listBox3.Items.Add("Элемент не найден");
            }
            else
            {
                listBox3.Items.Clear();
                listBox3.Items.Add(Output[1]);
                listBox2.Items.Clear();
                listBox2.Items.Add(Output[0]);
            }
        }
    }
}

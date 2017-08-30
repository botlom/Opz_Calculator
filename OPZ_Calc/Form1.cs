using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string OPZ(string input)
        {
            string[] output_string = new string[0];//выходная строка(массив)
            string[] stack = new string[0];//стек

            int k = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string temp = "";//как бы это было не удивительно, но переменная для хранения числа
                //проверка на отрицательное число: зсли знак "-" в начале строки или перед знаком "-" нет числа
                if (input[i] == '-' && ((i > 0 && !Char.IsDigit(input[i - 1])) || i == 0))
                {
                    i++;
                    temp += "-";//в переменную для чисел добавляется знак "-"
                }
                if (input[i] == ',')//самый большой приоритет, его автоматически добавляют в выходную строку
                {
                    Array.Resize(ref stack, stack.Length + 1);
                    stack[stack.Length - 1] = input[i].ToString();
                }
                if (input[i] == 'n')
                {
                    Array.Resize(ref stack, stack.Length + 1);
                    stack[stack.Length - 1] = input[i].ToString();
                }
                 if (input[i] == 's')
                {
                    Array.Resize(ref stack, stack.Length + 1);
                    stack[stack.Length - 1] = input[i].ToString();
                }
                if (input[i] == 'i')
                {
                    Array.Resize(ref stack, stack.Length + 1);
                    stack[stack.Length - 1] = input[i].ToString();
                }

                if (input[i] == '^')//т.к у возведения в степень самый большой приоритет, его автоматически добавляют в выходную строку
                {
                    Array.Resize(ref stack, stack.Length + 1);
                    stack[stack.Length - 1] = input[i].ToString();
                }
                if (Char.IsDigit(input[i]))
                {
                    //найдено число и в переменную temp записываются все цифры числа, тк число может состоять не только из одной цифры
                    while (i < input.Length && Char.IsDigit(input[i]))
                        temp += input[i++].ToString();
                    i--;
                    Array.Resize(ref output_string, output_string.Length + 1);//массив выходной строки динамически увеличиваем на единицу
                    output_string[output_string.Length - 1] = temp;//добавляем число в текущий элемент выходной строки
                }
                if (input[i] == '+' || input[i] == '-')
                {
                m://т.к + и - имеют самый низкий приоритет, то при записывании этих операндов в стек 
                    //все остальные операции из стека должны переместится в выходную строку(исключение скобка) 
                    if (stack.Length != 0) //если стек еще не пуст
                    {
                        if (stack[stack.Length - 1] == "(")//если последний элемент стека открывающаяся скобка 
                        {
                            Array.Resize(ref stack, stack.Length + 1);
                            stack[stack.Length - 1] = input[i].ToString();//то в стек добавляется операция сложения или отрицания 
                        }
                        else //иначе
                        {
                            Array.Resize(ref output_string, output_string.Length + 1);
                            output_string[output_string.Length - 1] = stack[stack.Length - 1];//в выходную строку добавляется последняя операция из стека
                            Array.Resize(ref stack, stack.Length - 1);//у стека удаляется последний элемент
                            goto m;//возврат на проверку находится ли на вершине стека еще какая либо операция
                        }
                        stack[stack.Length - 1] = input[i].ToString();//на вершину стека помещается операция + или -
                    }
                    else
                    {
                        Array.Resize(ref stack, stack.Length + 1);
                        stack[stack.Length - 1] = input[i].ToString();//если стек не пуст то в стек добавляется операция сложения или отрицания
                    }
                }
                if (input[i] == '(')
                {//найдена открывающаяся скобка которая автоматически добавляется в стек
                    Array.Resize(ref stack, stack.Length + 1);
                    stack[stack.Length - 1] = input[i].ToString();
                }
                if (input[i] == ')')
                {//найдена закрывающаяся скобка
                    int k_t = stack.Length - 1;
                    while (stack[k_t] != "(")//некий цикл который начиная с конца стека перемещается влево в поисках открывающиейся скобки
                        k_t--;//после цикла здесь хранится индекс открывающейся скобки
                    int kol = 0;
                    for (int j = stack.Length - 1; j >= k_t + 1; j--)//все операции которые хранились в стеке после "(" переносятся в выходную строку
                    {
                        kol++;
                        Array.Resize(ref output_string, output_string.Length + 1);
                        output_string[output_string.Length - 1] = stack[j];
                    }
                    Array.Resize(ref stack, stack.Length - (kol + 1));//стек уменьшается на количество переданных операций
                }
                if (input[i] == '*' || input[i] == '/')
                {

                    if (stack.Length != 0)//если стек еще не пуст
                    {

                        if (stack[stack.Length - 1] != "*" && stack[stack.Length - 1] != "/")//если на вершине стека не располагается * или /
                        {
                            Array.Resize(ref stack, stack.Length + 1);
                            stack[stack.Length - 1] = input[i].ToString();
                        }
                        else  //если на вершине стека располагается * или /
                        {//то т.к приоритеты операций одинаковые... 
                            Array.Resize(ref output_string, output_string.Length + 1);
                            output_string[output_string.Length - 1] = stack[stack.Length - 1];//...элемент с вершины стека передается в выходную строку(массив)
                            stack[stack.Length - 1] = input[i].ToString();//на вершину стека становится текщая операция умножения или деления

                        }

                    }
                    else
                    {
                        Array.Resize(ref stack, stack.Length + 1);
                        stack[stack.Length - 1] = input[i].ToString();//если стек не пуст то в стек добавляется операция умножения или деления
                    }
                }
            }
            //всю Входную строку проверили теперь в стеке и в Выходной строке содержатся некоторые данные
            //объединяем выходную строку со стеком
            Array.Resize(ref output_string, output_string.Length + stack.Length);
            Array.Reverse(stack);//это просто что бы не парится и не создавать цкл который будет идти задом на перед
            //т.к иначе есть свои нюансы в которые я вдаваться не хочу
            for (int i = output_string.Length - stack.Length; i < output_string.Length; i++)
                output_string[i] = stack[k++];
            string t = "";//сформировали из выходной строки(массива) строку
            for (int i = 0; i < output_string.Length; i++)
                if (i != output_string.Length - 1)
                    t += output_string[i] + " ";
                else
                    t += output_string[i];
            return t;//передали полученную строку
        }

        public double Solution(string OPZ_String)
        {
            string[] mas = OPZ_String.Split(' ');//нарезаем строку в виде Обратной записи в массив элементов
            string temp_string;
            for (int i = 0; i < mas.Length; i++)
                switch (mas[i])
                {
                    case "n":
                        temp_string = Math.Sin(double.Parse(mas[i - 1])).ToString();
                        richTextBox1.AppendText("n(" + mas[i - 1] + ")=" + temp_string + "\n");
                        mas[i - 1] = temp_string;
                        for (int j = i; j < mas.Length - 3; j++)
                            mas[j] = mas[j + 3];
                        Array.Resize(ref mas, mas.Length - 3);
                        i -= 2;
                        break;
                    case "+"://если найдена операция сложения
                        temp_string = (double.Parse(mas[i - 2]) + double.Parse(mas[i - 1])).ToString();//выполняем сложение и переводим ее в строку
                        richTextBox1.AppendText(mas[i - 2] + "+" + mas[i - 1] + "=" + temp_string + "\n");
                        mas[i - 2] = temp_string;//на место 1-ого операнда записывается результат (как если бы a=a+b)
                        for (int j = i - 1; j < mas.Length - 2; j++)//удаляем из массива второй операнд и знак арифм действия
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);//обрезаем массив элементов на 2 удаленнх элемента
                        i -= 2;
                        break;
                    case "-"://далее все аналогично
                        temp_string = (double.Parse(mas[i - 2]) - double.Parse(mas[i - 1])).ToString();
                        richTextBox1.AppendText(mas[i - 2] + "-" + mas[i - 1] + "=" + temp_string + "\n");
                        mas[i - 2] = temp_string;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "*":
                        temp_string = (double.Parse(mas[i - 2]) * double.Parse(mas[i - 1])).ToString();
                        richTextBox1.AppendText(mas[i - 2] + "*" + mas[i - 1] + "=" + temp_string + "\n");
                        mas[i - 2] = temp_string;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "/":
                        if (double.Parse(mas[i - 1]) != 0)
                        {
                            temp_string = (double.Parse(mas[i - 2]) / double.Parse(mas[i - 1])).ToString();
                            richTextBox1.AppendText(mas[i - 2] + "/" + mas[i - 1] + "=" + temp_string + "\n");
                            mas[i - 2] = temp_string;
                            for (int j = i - 1; j < mas.Length - 2; j++)
                                mas[j] = mas[j + 2];
                            Array.Resize(ref mas, mas.Length - 2);
                            i -= 2;
                        }
                        else richTextBox1.AppendText("Division by zero");
                        break;
                    case "^":
                        temp_string = (Math.Pow(double.Parse(mas[i - 2]), double.Parse(mas[i - 1]))).ToString();
                        richTextBox1.AppendText(mas[i - 2] + "^" + mas[i - 1] + "=" + temp_string + "\n");
                        mas[i - 2] = temp_string;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case ",":
                        temp_string = double.Parse(mas[i - 2]+","+mas[i - 1]).ToString();
                        richTextBox1.AppendText(mas[i - 2] + "," + mas[i - 1] + "=" + temp_string + "\n");
                        mas[i - 2] = temp_string;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                        mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                }
            return double.Parse(mas[0]);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double result = Solution(OPZ(textBox1.Text));
            this.Text = "Ответ: " + result.ToString();
            richTextBox1.AppendText(result.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            //for (int i = 0; i < 127; i++) richTextBox1.Text += i +"="+ (char)i+"\n";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((((((e.KeyChar == 14 || e.KeyChar == 15) || (e.KeyChar <= 39 || e.KeyChar >= 59) && e.KeyChar != 8) && e.KeyChar != 94) && e.KeyChar!=115) && e.KeyChar!=105) && e.KeyChar!=110) e.Handled = true;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}

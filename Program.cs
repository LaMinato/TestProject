using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        //Класс ячеек
        class Coords
        {
            public int x { set; get; } //Координата Х ячейки
            public int y { set; get; } //Координата Y ячейки
            public bool avail { set; get; }//Признак доступности ячейки
            public int val { set; get; } //Значение ячейки

            public Coords(int newX, int newY, bool newAvail, int newVal)
            {
                x = newX;
                y = newY;
                avail = newAvail;
                val = newVal;
            }
        }
        static void Main(string[] args)
        {
            int[,] arr = new int[3,3];//Изначальный массив
            string result = "";
            //Заводим множество пройденных ячеек
            HashSet<int> walked = new HashSet<int>();
            //Заполняем исходный массив
            Console.WriteLine("Заполните матрицу 3х3 значениями от 1 до 9. Заполнять требуется тремя строками по 3 значения, разделенных пробелами");
            for (int i = 0; i < 3; i++)
            {
                string[] line = Console.ReadLine().Split(' ');
                arr[i, 0] = Convert.ToInt32(line[0]);
                arr[i, 1] = Convert.ToInt32(line[1]);
                arr[i, 2] = Convert.ToInt32(line[2]);
            }
            //Вывод изначальной матрицы
            for (int i=0; i<3; i++)
            {
                for (int j = 0; j < 3; j++)
                    Console.Write($"{arr[i, j]}  ");
                Console.WriteLine();
            }
            //Задаем стартовую ячейку
            int curi = 0; int curj = 0;
            //Записываем значение этой ячейки в результат
            result = arr[curi, curj].ToString();
            walked.Add(arr[curi, curj]);
            //Пока не пройдем все ячейки
            while (walked.Count() < 9)
            {
                Coords[] coords = new Coords[4];
                if (curi - 1 < 0) coords[0] = new Coords(curi - 1, curj, false, 0);
                else if (walked.Contains(arr[curi - 1, curj])) coords[0] = new Coords(curi - 1, curj, false, 0);
                else coords[0] = new Coords(curi - 1, curj, true, arr[curi - 1, curj]);

                if (curi + 1 > 2) coords[1] = new Coords(curi + 1, curj, false, 0);
                else if (walked.Contains(arr[curi + 1, curj])) coords[1] = new Coords(curi + 1, curj, false, 0);
                else coords[1] = new Coords(curi + 1, curj, true, arr[curi + 1, curj]);

                if (curj - 1 < 0) coords[2] = new Coords(curi, curj - 1, false, 0);
                else if (walked.Contains(arr[curi, curj - 1])) coords[2] = new Coords(curi, curj - 1, false, 0);
                else coords[2] = new Coords(curi, curj - 1, true, arr[curi, curj - 1]);

                if (curj + 1 > 2) coords[3] = new Coords(curi, curj + 1, false, 0);
                else if (walked.Contains(arr[curi, curj + 1])) coords[3] = new Coords(curi, curj + 1, false, 0);
                else coords[3] = new Coords(curi, curj + 1, true, arr[curi, curj + 1]);

                Coords forward = new Coords(coords[0].x, coords[0].y, coords[0].avail, coords[0].val);
                for (int i = 1; i < coords.Length; i++)
                {
                    if (coords[i].avail && coords[i].val > forward.val)
                        forward = coords[i];
                }
                curi = forward.x; curj = forward.y;
                walked.Add(forward.val);
                result += arr[curi, curj].ToString();
            }

            Console.WriteLine($"Итоговый результат: {result}");
            Console.ReadLine();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace ShellSort
{
    class Program
    {
        
        static readonly string _base = @"C:\Users\jows1\source\repos\ShellSort\DIgits";

        #region Fill
        static void FillFile(string peaceOfPatch, int count) // ex: \1 - 100.txt
        {
            Random rnd = new Random();

            string patch = _base + peaceOfPatch;

            try
            {
                using (StreamWriter ws = new StreamWriter(patch))
                {
                    for (int i = 0; i < count; i++)
                        ws.Write(rnd.Next(1, 1000) + ",");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        #endregion EndOfFill
        static void ShellSort(int n, int[] mass, out ulong count) // Где n - число элементов.
        {
            int i, j, step;
            int tmp;
            count = 0;

            for (step = n / 2; step > 0; step /= 2) // Внешний цикл оперирующий шагами
                for(i = step; i < n; i++) // Вложенный цикл, указывающий элементы образованные шагами
                {
                    tmp = mass[i];
                    for(j = i; j >= step; j -=step) // Ещё один вложенный цикл. Обычная сортировкаа вставками
                    {
                        count++;
                        if (tmp < mass[j - step])
                            mass[j] = mass[j - step];
                        else
                            break;
                    }
                    mass[j] = tmp;
                }
        }

        static void ReadDidits(string peaceOfPatch, out int[] array)
        {
            List<int> bigList = new List<int>();
            string patch = _base + peaceOfPatch;
            string[] data;

            try
            {
                using (StreamReader sr = new StreamReader(patch))
                    data = sr.ReadToEnd().Split(',');
            }catch (Exception ex) { Console.WriteLine(ex.Message); array = null; return; }

            for (int i = 0; i < data.Length - 1; i++)
                bigList.Add(int.Parse(data[i]));

            array = bigList.ToArray();
        }
        static void Main()
        {           

            ulong count = 0;
            Stopwatch sw = new Stopwatch();
            int[] bigArray;


            for (int i = 1; i < 51; i++)
            {
                string peacePatch = $@"\{i} - {i * 100}.txt";

                ReadDidits(peacePatch, out bigArray);

                sw.Start();
                ShellSort(bigArray.Length, bigArray, out count);
                sw.Stop();

                Console.WriteLine($"Done for {i * 100}el." + " Time - " + sw.Elapsed.TotalMilliseconds + " Iteration - " + count);
            }

            Console.ReadKey();
        }
    }
}

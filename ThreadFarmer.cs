using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2__ASP_MVC_
{
    internal class ThreadFarmer
    {
        private static char[,] field;
        public void Task1()
        {
            Console.Write("Укажите размер поля по оси X: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Укажите размер поля по оси Y: ");
            int y = Convert.ToInt32(Console.ReadLine());
            field = new char[y, x];

            AutoResetEvent[] waitHandlers = new AutoResetEvent[2];
            for (int i = 0; i < waitHandlers.Length; i++)
            {
                waitHandlers[i] = new AutoResetEvent(false);
            }

            Thread thread1 = new Thread((o) =>
            {
                if (o != null && o is AutoResetEvent)
                {
                    for (int i = 0; i < field.GetLength(0); i++)
                    {
                        for (int j = 0; j < field.GetLength(1); j++)
                        {
                            //if (field[i, j] == 'О')
                            //{
                            //    continue;
                            //}
                            //Console.WriteLine(field[i, j] = 'X');
                            field[i, j] = 'X';
                            Thread.Sleep(10);
                        }
                    }
                    ((AutoResetEvent)o).Set();
                }
            });
            thread1.Start(waitHandlers[0]);

            Thread thread2 = new Thread((o) =>
            {
                if (o != null && o is AutoResetEvent)
                {
                    for (int i = field.GetLength(1) - 1; i >= 0; i--)
                    {
                        for (int j = field.GetLength(0) - 1; j >= 0; j--)
                        {
                            //if (field[i, j] == 'X')
                            //{
                            //    continue;
                            //}
                            //Console.WriteLine(field[i, j] = 'О');
                            field[i, j] = 'О';
                            Thread.Sleep(10);
                        }
                        ((AutoResetEvent)o).Set();
                    }
                }
            });
            thread2.Start(waitHandlers[1]);

            WaitHandle.WaitAll(waitHandlers);
        }

        public void Task2()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}

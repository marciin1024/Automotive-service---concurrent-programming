using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PW_projekt
{
    class Program
    {
        private static int pojazdy = 20;
        static Semaphore mechanika;
        static Semaphore diagnostyka;
        static Semaphore lakier;
        static Semaphore ekran;
        static Thread[] samochody;
        static Queue<Thread> kolejkaMechanika = new Queue<Thread>();
        static Queue<Thread> kolejkaDiagnostyka = new Queue<Thread>();
        static Queue<Thread> kolejkaLakier = new Queue<Thread>();
        static Queue<Thread> DoNaprawy = new Queue<Thread>();

        static void Main(string[] args)
        {
            mechanika = new Semaphore(2, 2);
            diagnostyka = new Semaphore(1, 1);
            lakier = new Semaphore(1, 1);
            ekran = new Semaphore(1, 1);
            Random rand = new Random();
            samochody = new Thread[pojazdy];

            int j;
            samochody = new Thread[pojazdy];
            for (int i = 0; i < pojazdy; i++)
            {
                samochody[i] = new Thread(Maszyny);
                j = i + 1;
                samochody[i].Name = "Samochod " + j;
                Random rnd = new Random();

                samochody[i].Start();
                Thread.Sleep(400);
            }
            Console.ReadKey();
        }

        public static void Maszyny()
        {
            int licznik = 0;
            Random rand = new Random();
            Random rnd = new Random();
            int x = rnd.Next(1, 4);

            if (x == 1)
            {

                ekran.WaitOne();
                Console.WriteLine("{0} czeka na mechanike", Thread.CurrentThread.Name);
                ekran.Release();
            }
            else if (x == 2)
            {

                ekran.WaitOne();
                Console.WriteLine("{0} czeka na diagnostyke", Thread.CurrentThread.Name);
                ekran.Release();
            }
            else
            {

                ekran.WaitOne();
                Console.WriteLine("{0} czeka na lakier", Thread.CurrentThread.Name);
                ekran.Release();
            }

            if (x == 1)
            {
                mechanika.WaitOne();
                ekran.WaitOne();
                Console.WriteLine("{0} - mechanika", Thread.CurrentThread.Name);
                ekran.Release();
                Thread.Sleep(rand.Next(2500, 2900));

                Random random = new Random();
                int s = random.Next(1, 3);
                if (s == 1)
                {
                    ekran.WaitOne();
                    Console.WriteLine("{0} jedzie na parking", Thread.CurrentThread.Name);
                    ekran.Release();
                    mechanika.Release();
                    Thread.Sleep(rand.Next(2700, 3300));
                    mechanika.WaitOne();
                    ekran.WaitOne();
                    Console.WriteLine("{0} - mechanika", Thread.CurrentThread.Name);
                    ekran.Release();
                    Thread.Sleep(rand.Next(2500, 2900));
                    ekran.WaitOne();
                    Console.WriteLine("{0} opuszcza mechanike", Thread.CurrentThread.Name);
                    ekran.Release();
                    mechanika.Release();

                }
                else
                {
                    ekran.WaitOne();
                    Console.WriteLine("{0} opuszcza mechanike", Thread.CurrentThread.Name);
                    ekran.Release();
                    mechanika.Release();
                }
            }

            else if (x == 2)
            {
                diagnostyka.WaitOne();
                ekran.WaitOne();
                Console.WriteLine("{0} - diagnostyka", Thread.CurrentThread.Name);
                ekran.Release();
                Thread.Sleep(rand.Next(700, 900));
                int y = rand.Next(1, 3);

                if (y == 1)
                {
                    ekran.WaitOne();
                    Console.WriteLine("{0} wymaga naprawy", Thread.CurrentThread.Name);
                    ekran.Release();
                    licznik++;
                    diagnostyka.Release();
                    ekran.WaitOne();
                    ekran.Release();
                    ekran.WaitOne();
                    Console.WriteLine("{0} opuszcza diagnostyke", Thread.CurrentThread.Name);
                    ekran.Release();
                    mechanika.WaitOne();
                    ekran.WaitOne();
                    Console.WriteLine("{0} - mechanika", Thread.CurrentThread.Name);
                    ekran.Release();
                    Thread.Sleep(rand.Next(2500, 2900));
                    mechanika.Release();
                    ekran.WaitOne();
                    Console.WriteLine("{0} opuszcza mechanike", Thread.CurrentThread.Name);
                    ekran.Release();
                }
                else
                {
                    ekran.WaitOne();
                    Console.WriteLine("{0} jest gotowy do opuszczenia diagnostyki", Thread.CurrentThread.Name);
                    ekran.Release();
                    ekran.WaitOne();
                    Console.WriteLine("{0} opuszcza diagnostyke", Thread.CurrentThread.Name);
                    ekran.Release();
                    diagnostyka.Release();
                }
            }
            else
            {
                lakier.WaitOne();
                ekran.WaitOne();
                Console.WriteLine("{0} - lakier", Thread.CurrentThread.Name);
                ekran.Release();
                Thread.Sleep(rand.Next(1500, 1900));
                ekran.WaitOne();
                Console.WriteLine("{0} opuszcza lakier", Thread.CurrentThread.Name);
                ekran.Release();
                lakier.Release();
            }
        }
    }
}



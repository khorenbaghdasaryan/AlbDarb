using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace ConsoleApp5
{
    class P
    {
        public void p()
        {
            Process p;
            try
            {
                p = Process.GetProcessById(12364);
            }
            catch
            {
                Console.WriteLine("Sorry");
                return;
            }
            Console.WriteLine("threads used by :{0}", p.ProcessName);
            ProcessThreadCollection t = p.Threads;
            foreach (ProcessThread item in t)
            {
                Console.WriteLine(item.Id + " ");
                Console.WriteLine(item.ThreadState.ToString() + " ");
                Console.WriteLine(item.PriorityLevel + " ");
                Console.WriteLine(item.StartTime.ToShortTimeString());

            }
        }
        public void p1()
        {
            Thread t = Thread.CurrentThread;
            t.Name = "TTTT";
            Console.WriteLine("Main() : " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Name of current Context : {0}", Thread.GetDomain().FriendlyName);
            //Console.WriteLine("Id of current Context : {0}", ???);
            Console.WriteLine("Has thread started : {0}", t.IsAlive);
            Console.WriteLine("Priority Level : {0}", t.Priority);
            Console.WriteLine("Threade state : {0}", t.ThreadState);
        }

        public void p2()
        {
            var time = DateTime.Now;
            //p3();
            Thread t = new Thread(new ThreadStart(new P().p3));
            t.Start();
            for (int i = 0; i < 100; i++)
                Console.WriteLine("##########");
            Console.WriteLine(DateTime.Now - time);
        }
        public void p3()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Doing .........................................");
            }
        }

        [Obsolete]
        public void p4()
        {
            Thread t1 = new Thread(new ThreadStart(p5));
            Thread t2 = new Thread(new ThreadStart(p6));
            t1.Start();
            //t1.Join();
            t2.Start();
            //t1.IsBackground = true;
            //t2.IsBackground = true;
            //t2.Suspend();
            //t2.Resume();
            //t2.Abort();

        }
        private void p5()
        {
            Thread.Sleep(2000);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("increment: " + i);
            }
        }
        private void p6()
        {
            Thread.Sleep(2000);
            for (int i = 100; i >= 0; i--)
            {
                Console.WriteLine("-----decrement: " + i);
            }
        }

        public void p7()
        {
            Thread t = new Thread(new ThreadStart(p8));
            t.Start();
            t.Join();
            t.IsBackground = true;
            
        }

        private void p8()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("*****TM*****");
            }
        }
        public void p9()
        {
            Console.WriteLine("Main():" + Thread.CurrentThread.ManagedThreadId);
            A ob = new A();
            Thread t = new Thread(new ParameterizedThreadStart(p10));
            t.Start(ob);

        }
        public class A
        {
            public int a = 5, b = 9;
        }
        public void p10(object A)
        {
            Console.WriteLine("Add():" + Thread.CurrentThread.ManagedThreadId);
            A ob = (A)A;
            Console.WriteLine("{0} + {1} = {2}", ob.a, ob.b, ob.a + ob.b);
        }
        public void p11()
        {
            Thread t1 = new Thread(new ThreadStart(p12));
            Thread t2 = new Thread(new ThreadStart(p13));
            Thread t3 = new Thread(new ThreadStart(p14));
            t1.Priority = ThreadPriority.Normal;
            t2.Priority = ThreadPriority.Normal;
            t3.Priority = ThreadPriority.Lowest;
            t1.Start();
            t2.Start();
            t3.Start();
            
        }

        private void p12()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("********************");
            }
        }

        private void p13()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("---------------------");
            }
        }

        private void p14()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("//////////////////////");
            }
        }

        public void p15()
        {
            Thread t1 = new Thread(new ThreadStart(p16));
            t1.Name = "TOne";
            t1.Start();
            Console.WriteLine("Started" + t1.Name);
            Thread t2 = new Thread(new ThreadStart(p17));
            t2.Name = "TTwo";
            t2.Start();
            Console.WriteLine("Started" + t2.Name);
        }
        int c = 0;
        private void p16()
        {
            
            while (c < 100)
            {
                lock (this)
                {
                    c++;
                    Console.WriteLine("{0}, increment: {1}", Thread.CurrentThread.Name, c);
                }
            }
        }

        private void p17()
        {
            //int c = 0;
            while (c < 100)
            {
                lock (this)
                {
                    c++;
                    Console.WriteLine("..{0}, increment: {1}", Thread.CurrentThread.Name, c);
                }
            }
        }

        static int x = 0;
        static object locker = new object();
        public void p18()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(p19);
                thread.Name = "Hosq " + i.ToString();
                thread.Start();
            }
        }

        private void p19(object obj)
        {
            //Monitor.Enter(this);
            //lock (this)
            {
                x = 1;
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                    x++;
                    Thread.Sleep(100);
                }
            }
            //Monitor.Exit(this);
        }

        public void p20()
        {
            Thread t1 = new Thread(new ThreadStart(p21));
            t1.Name = "TOne";
            t1.Start();
            Console.WriteLine("Started" + t1.Name);
            Thread t2 = new Thread(new ThreadStart(p22));
            t2.Name = "TTwo";
            t2.Start();
            Console.WriteLine("Started" + t2.Name);
        }

        private void p21()
        {
            while (c < 100)
            {
                //Monitor.Enter(this);
                //try
                {
                    c++;
                    Console.WriteLine("{0}, increment: {1}", Thread.CurrentThread.Name, c);
                }
                //finally
                {
                    //Monitor.Exit(this);
                }
            }
        }
        private void p22()
        {
            Console.WriteLine("TTwo ==< " + Monitor.TryEnter(this));
            while (c < 100)
            {
                Monitor.Enter(this);
                //try
                {
                    Console.WriteLine("TTwo ==< " + Monitor.TryEnter(this));
                    c++;
                    Console.WriteLine("..{0}, increment: {1}", Thread.CurrentThread.Name, c);
                }
                //finally
                {
                    Monitor.Exit(this);
                }
            }
        }

        public void p23()
        {
            Thread t1 = new Thread(new ThreadStart(p24));
            t1.Name = "TOne";
            t1.Start();
            Console.WriteLine("Started" + t1.Name);
            Thread t2 = new Thread(new ThreadStart(p25));
            t2.Name = "TTwo";
            t2.Start();
            Console.WriteLine("Started" + t2.Name);
        }

        private void p24()
        {
            while (c < 100)
            {
                lock (this)
                {
                    Monitor.Pulse(this);
                    c++;
                    Console.WriteLine("{0}, increment: {1}", Thread.CurrentThread.Name, c);
                    Monitor.Wait(this);
                }
            }
        }

        private void p25()
        {
            while (c < 100)
            {
                lock (this)
                {
                    Monitor.Pulse(this);
                    c++;
                    Console.WriteLine(".. {0}, increment: {1}", Thread.CurrentThread.Name, c);
                    Monitor.Wait(this);
                }
            }
        }

        public void p26()
        {
            Thread t1 = new Thread(new ThreadStart(p24));
            t1.Name = "TOne";
            t1.Start();
            Console.WriteLine("Started" + t1.Name);
            Thread t2 = new Thread(new ThreadStart(p25));
            t2.Name = "TTwo";
            t2.Start();
            Console.WriteLine("Started" + t2.Name);
        }

        private void p27()
        {
            //lock (this)
            {
                while (c < 100)
                {
                    //Monitor.Pulse(this);
                    //if (c == 20)
                        Monitor.Wait(this,1000);
                    c++;
                    Console.WriteLine("{0}, increment: {1}", Thread.CurrentThread.Name, c);
                    //Monitor.Wait(this);
                }
            }
        }

        private void p28()
        {
            //lock (this)
            {
                while (c < 100)
                {
                    //Monitor.Pulse(this);
                    //if (c == 50)
                        Monitor.Wait(this);
                    c++;
                    Console.WriteLine(" .. {0}, increment: {1}", Thread.CurrentThread.Name, c);
                    //Monitor.Wait(this);
                }
            }
        }

        Mutex mutex = new Mutex();
        public void p29()
        {
            Thread t1 = new Thread(new ThreadStart(p30));
            t1.Name = "TOne";
            t1.Start();
            Console.WriteLine("Started" + t1.Name);
            Thread t2 = new Thread(new ThreadStart(p31));
            t2.Name = "TTwo";
            t2.Start();
            Console.WriteLine("Started" + t2.Name);
        }

        private void p30()
        {
            mutex.WaitOne();
            {
                while (c < 100)
                {                   
                    c++;
                    Console.WriteLine("{0}, increment: {1}", Thread.CurrentThread.Name, c);                  
                }
            }
            mutex.ReleaseMutex();
        }

        private void p31()
        {
            mutex.WaitOne();
            {
                while (c < 100)
                {
                    c++;
                    Console.WriteLine(" .. {0}, increment: {1}", Thread.CurrentThread.Name, c);
                }
            }
            mutex.ReleaseMutex();
        }

        Semaphore semaphore = new Semaphore(1, 1);
        public void p32()
        {
            Thread t1 = new Thread(new ThreadStart(p33));
            t1.Name = "TOne";
            t1.Start();
            Console.WriteLine("Started" + t1.Name);
            Thread t2 = new Thread(new ThreadStart(p34));
            t2.Name = "TTwo";
            t2.Start();
            Console.WriteLine("Started" + t2.Name);
        }
        private void p33()
        {
            {
                while (c < 100)
                {
                    semaphore.WaitOne();
                    c++;
                    Console.WriteLine("{0}, increment: {1}", Thread.CurrentThread.Name, c);
                    semaphore.Release();
                }
            }
        }
        private void p34()
        {
            {
                while (c < 100)
                {
                    semaphore.WaitOne();
                    c++;
                    Console.WriteLine(" .. {0}, increment: {1}", Thread.CurrentThread.Name, c);
                    semaphore.Release();
                }
            }
        }

        static AutoResetEvent auto = new AutoResetEvent(false);
        public void p35()
        {
            Thread t1 = new Thread(new ThreadStart(p36));
            t1.Start();
            Thread t2 = new Thread(new ThreadStart(p37));
            t2.Start();
        }

        private void p36()
        {
            while (c < 100)
            {
                auto.WaitOne();
                c++;
                Console.WriteLine(" .. {0}, increment: {1}", Thread.CurrentThread.Name, c);
                auto.Set();
                //auto.Reset();
            }
        }

        private void p37()
        {
            while (c < 100)
            {
                auto.WaitOne();
                c++;
                Console.WriteLine(" {0}, increment: {1}", Thread.CurrentThread.Name, c);
                auto.Set();
                //auto.Reset();
            }
        }
        public void p38()
        {
            System.Threading.Timer timer = new System.Threading.Timer(p39, null, 0, 2000);
            Console.ReadKey();
        }

        private void p39(object state)
        {
            Console.WriteLine(DateTime.Now);
            GC.Collect();
        }
        public void p40()
        {
            Console.WriteLine();
            System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(p41), null, 0, 2000);
            Console.ReadKey();
            timer.Dispose();
        }

        private void p41(object state)
        {
            Console.WriteLine("The current time is {0} on thread {1}",
            DateTime.Now, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
        }
        public void Timers()
        {
            var t1 = new System.Timers.Timer(1000);
            t1.AutoReset = true;
            t1.Elapsed += TimerAction;
            t1.Start();
            Thread.Sleep(10000);
            t1.Stop();
            t1.Dispose();
        }

        private void TimerAction(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("System.Timers.Timer {0:T}", e.SignalTime);
        }

        //const int iter = 20000;
        //public void p42()
        //{
        //    Stopwatch sw = Stopwatch.StartNew();
        //    System.IO.File.WriteAllText("test.tex", new string('*', 3000000));
        //    Console.WriteLine(sw.Elapsed);
        //}
    }
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            new P().Timers();
        }
    }
}

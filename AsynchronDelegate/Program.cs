using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronDelegate
{
    public delegate int Del1(int x, int y);
    public delegate void Del2();
    class DelegateAsynchron
    {
        public void Met1()
        {
            Console.WriteLine($"Main() thread = {Thread.CurrentThread.ManagedThreadId}");
            Del1 del1 = new Del1(Add);
            IAsyncResult async = del1.BeginInvoke(10, 10, null, null);
            Console.WriteLine("Doing more work in Main()!");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("#########"+i);
            }
            int answer = del1.EndInvoke(async);
            Console.WriteLine($"10+10={answer}");
        }

        private int Add(int x, int y)
        {
            Console.WriteLine($"Add() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine($"          ------------------" + i);
            }
            Thread.Sleep(1000);
            return x + y;
        }
        static int N = 30;
        public void Met2()
        {
            Console.WriteLine($"Main() thread = {Thread.CurrentThread.ManagedThreadId}");
            Del2 del2f1 = new Del2(f1);
            del2f1.BeginInvoke(null, null);
            Del2 del2f2 = new Del2(f2);
            del2f2.BeginInvoke(null, null);
            Del2 del2f3 = new Del2(f3);
            del2f3.BeginInvoke(null, null);
            Del2 del2f4 = new Del2(f4);
            del2f4.BeginInvoke(null, null);
            Del2 del2f5 = new Del2(f5);
            del2f5.BeginInvoke(null, null);
            Del2 del2f6 = new Del2(f6);
            del2f6.BeginInvoke(null, null);
            Del2 del2f7 = new Del2(f7);
            del2f7.BeginInvoke(null, null);
            Del2 del2f8 = new Del2(f8);
            del2f8.BeginInvoke(null, null);
            Del2 del2f9 = new Del2(f9);
            del2f9.BeginInvoke(null, null);
            Thread.Sleep(1000);
            Console.WriteLine($"Main() thread = {Thread.CurrentThread.ManagedThreadId}");
        }
        private void f1()
        {
            Console.WriteLine($"f1() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("#########" + i);
            }
        }
        private void f2()
        {
            Console.WriteLine($"f2() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("         ----------" + i);
            }
        }
        private void f3()
        {
            Console.WriteLine($"f3() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("                     +++++++++++" + i);
            }
        }

        private void f4()
        {
            Console.WriteLine($"f4() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("                                 ==========" + i);
            }
        }

        private void f5()
        {
            Console.WriteLine($"f5() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("                                         ///////////" + i);
            }
        }

        private void f6()
        {
            Console.WriteLine($"f6() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("                                                    6666666666" + i);
            }
        }

        private void f7()
        {
            Console.WriteLine($"f7() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("                                                             7777777777" + i);
            }
        }

        private void f8()
        {
            Console.WriteLine($"f7() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("                                                                         8888888888888" + i);
            }
        }

        private void f9()
        {
            Console.WriteLine($"f7() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("                                                                                     9999999999999999" + i);
            }
        }
    }
    class DelSyn
    {
        public delegate int Del1(int x, int y);
        public void Met()
        {
            Console.WriteLine($"Main() thread = {Thread.CurrentThread.ManagedThreadId}");
            Del1 del1 = new Del1(Add);
            IAsyncResult async = del1.BeginInvoke(10, 10, null, null);
            //while (!async.IsCompleted)
            //{
            //    Console.WriteLine("Doing more work in Main()!");
            //}
            while (!async.AsyncWaitHandle.WaitOne(2, true))
            {
                Console.WriteLine("Doing more work in Main()!");
            }
            int answer = del1.EndInvoke(async);
            Console.WriteLine($"10+10={answer}");
        }

        private int Add(int x, int y)
        {
            Console.WriteLine($"Add() thread = {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5);
            return x + y;
        }

        Del1 del2;
        public void Met2()
        {
            Console.WriteLine($"Main() thread = {Thread.CurrentThread.ManagedThreadId}");
            del2 = new Del1(Add2);
            IAsyncResult async = del2.BeginInvoke(10, 10, new AsyncCallback(m1), "barev #######");
            Console.WriteLine("Doing more work in Main()!");
            //for (int i = 0; i <= 30; i++)
            //{
            //    Console.WriteLine("#######" + i);
            //}
            Thread.Sleep(1000);
        }

        private int Add2(int x, int y)
        {
            Console.WriteLine($"Add() thread = {Thread.CurrentThread.ManagedThreadId}");
            //for (int i = 0; i <= 30; i++)
            //{
            //    Console.WriteLine("           !!!!!!!" + i);
            //}
            return x + y;
        }

        private void m1(IAsyncResult ar)
        {
            Console.WriteLine($"m1() thread = {Thread.CurrentThread.ManagedThreadId}");
            AsyncResult resultc = (AsyncResult)ar;
            Del1 dd = (Del1)resultc.AsyncDelegate;
            Console.WriteLine("sum = " + dd.EndInvoke(resultc));
            Console.WriteLine(ar.AsyncState);
        }
    }

    class TP
    {
        public void Met3()
        {
            int nWorker, nPort;
            ThreadPool.GetMaxThreads(out nWorker, out nPort);
            Console.WriteLine($"Max Threads : {nWorker}, I/O : {nPort}");
            //
            Console.WriteLine($"Main() thread = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 8; i++)
            {
                ThreadPool.QueueUserWorkItem(m2);
            }
            Thread.Sleep(1000);
        }

        public void m2(object state)
        {
            Console.WriteLine($"m2() thread = {Thread.CurrentThread.ManagedThreadId}");
        }


        static object ob = new object();//for synchronization
        public static void Met4()
        {
            Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");
            Task task1 = new Task(m3);
            Task task2 = new Task(m3);
            task1.Start();
            task2.Start();
            for (int i = 0; i <= 30; i++)
            {
                Console.WriteLine($"####### {i}");
            }
            Thread.Sleep(100);
            //task1.Wait();
            //task2.Wait();
            //Task.WaitAll(task1, task2);
            //Task.WaitAny(task1, task2);
        }
        private static void m3()
        {
            Console.WriteLine($"Start Task Id = {Task.CurrentId}");
            Console.WriteLine($"Thread m3() end: {Task.CurrentId}");
            lock(ob)
            for (int i = 0; i <= 30; i++)
            {
                Console.WriteLine($"     Task {Task.CurrentId} ... {i}");
            }
            Console.WriteLine($"End task id {Task.CurrentId} ");
            Console.WriteLine($"Thread m3 end: {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    class P
    {
        public void Met4()
        {
            Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");
            Parallel.Invoke(m1, m2, m3);
        }
        public int N = 30;
        public void m1()
        {
            for (int i = 0; i <= N; i++)
            {
                Console.WriteLine("!!!!!!" + 1);
            }
        }
        public void m2()
        {
            for (int i = 0; i <= N; i++)
            {
                Console.WriteLine("        +++++++" + 1);
            }
        }
        public void m3()
        {
            for (int i = 0; i <= N; i++)
            {
                Console.WriteLine("                =========" + 1);
            }
        }

        int[] ar;
        public void Met5()
        {
            Console.WriteLine("Main starting.");
            ar = new int[50000];
            for (int i = 0; i < ar.Length; i++)
            {
                ar[i] = i;
            }
            Parallel.For(0, ar.Length, m4);
            Console.WriteLine("Main ending.");
        }
        private void m4(int i)
        {
            if (ar[i] < 10000)
            {
                ar[i] = 0;
                Console.WriteLine("!!!!!!");
            }
            if (ar[i] < 20000 & ar[i] > 10000)
            {
                ar[i] = 100;
                Console.WriteLine("       +++++++");
            }
            if (ar[i] < 30000 & ar[i] > 20000)
            {
                ar[i] = 300;
                Console.WriteLine("                 -----------");
            }
            if (ar[i] < 40000 & ar[i] > 30000)
            {
                ar[i] = 300;
                Console.WriteLine("                 -----------");
            }
            if (ar[i] > 40000)
            {
                ar[i] = 400;
                Console.WriteLine("                                 ==============");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            new AsyncAwait().Met7();
        }
    }
}

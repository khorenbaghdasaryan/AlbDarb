using System;
using System.Collections.Generic;
using System.Linq;
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
    class Program
    {
        static void Main(string[] args)
        {
            new DelegateAsynchron().Met2();
        }
    }
}

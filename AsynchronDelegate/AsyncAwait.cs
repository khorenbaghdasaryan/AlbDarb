using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronDelegate
{
    class AsyncAwait
    {
        async void m1()
        {
            Console.WriteLine($"m1 Thread: {Thread.CurrentThread.ManagedThreadId}");
            string s = await m2();
            Console.WriteLine($"m1 Thread after: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(s);
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"     end async {i}");
            }
        }

        private Task<string> m2()
        {
            Console.WriteLine($"m2 Thread: {Thread.CurrentThread.ManagedThreadId}");
            return Task.Run(() =>
            {
                Console.WriteLine($"m2 Task.Run: {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i <= 30; i++)
                {
                    Console.WriteLine("     !!!!!!" + i);
                }
                return "barev";
            });
        }
        public void Met6()
        {
            Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");
            m1();
            for (int i = 0; i <= 30; i++)
            {
                Console.WriteLine($"###### {i}");
            }
            Console.WriteLine("End Main");
            Thread.Sleep(1000);
        }

        public async Task m3()
        {
            Console.WriteLine($"m3 Thread: {Thread.CurrentThread.ManagedThreadId}");
            Task task1 = Task.Run(() =>
            {
                Console.WriteLine($"m3 task1 start : {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i <= 30; i++)
                {
                    Console.WriteLine($"     !!!!!! {i}");
                }
            });

            Task task2 = Task.Run(() =>
            {
                Console.WriteLine($"m3 task2 start: {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i <= 30; i++)
                {
                    Console.WriteLine($"     ******* {i}");
                }
            });

            await Task.WhenAll(new[] { task1, task2 });
            Console.WriteLine($"m3 end {Thread.CurrentThread.ManagedThreadId}");
        }

        public void Met7()
        {
            Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");
            Task task = m3();
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine("#####");
            }
            task.Wait();
        }
    }
}

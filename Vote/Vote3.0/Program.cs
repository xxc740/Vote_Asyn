using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vote3._0
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime Now = DateTime.Now;
            Func<decimal, int, decimal> f = (data, ms) =>
            {
                Console.WriteLine("SubProcess thread started ! Current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("SubProcess thread IsBackground " + Thread.CurrentThread.IsBackground);
                Thread.Sleep(ms);
                Console.WriteLine("SubProcess thread completed !");
                return ++data;
            };

            var ar = f.BeginInvoke(1, 200, (r) =>
            {
                if (r == null)
                {
                    throw new ArgumentException("argument is null");
                }

                Console.WriteLine("the SubProcess return value: {0}", f.EndInvoke(r));
                Console.WriteLine("the SubProcess total runtime: {0}", (DateTime.Now - Now).TotalSeconds);
                Console.WriteLine("Now this is Main thread !");
            }, null);

            while (!ar.IsCompleted)
            {
                Console.WriteLine("main thread wating current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(300);
            }

            Console.ReadKey();
        }
    }
}

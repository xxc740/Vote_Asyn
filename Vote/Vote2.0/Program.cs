using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vote2._0
{
    class Program
    {
        public delegate decimal TakeDelegate(decimal data, int ms);
        public static DateTime Now = DateTime.Now;

        static void Main(string[] args)
        {
            TakeDelegate dl = SubProcess;

            var ar = dl.BeginInvoke(1, 300, AsyncCallBack, dl);

            while (!ar.IsCompleted)
            {
                Console.WriteLine("main thread wating current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(50);
            }

            Console.ReadKey();
        }

        private static decimal SubProcess(decimal data, int ms)
        {
            Console.WriteLine("SubProcess thread started ! Current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("SubProcess thread IsBackground " + Thread.CurrentThread.IsBackground);
            Thread.Sleep(ms);
            Console.WriteLine("SubProcess thread completed !");
            return ++data;
        }

        //回调函数来处理委托所指向的方法的执行结果
        private static void AsyncCallBack(IAsyncResult ar)
        {
            if (ar == null)
            {
                throw new ArgumentException("ar is null");
            }

            TakeDelegate dl = ar.AsyncState as TakeDelegate;

            decimal result = dl.EndInvoke(ar);
            Console.WriteLine("the SubProcess return value: {0}", result);
            Console.WriteLine("the SubProcess total runtime: {0}", (DateTime.Now - Now).TotalSeconds);
            Console.WriteLine("Now this is Main thread !");
        }
    }
}

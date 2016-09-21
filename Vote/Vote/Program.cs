using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vote
{
    class Program
    {
        public delegate decimal TakeDelegate(decimal data, int ms);

        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;

            TakeDelegate d1 = SaveBankAccount;

            //BeginInvoke方法可以使用线程异步地执行委托所指向的方法
            IAsyncResult ar = d1.BeginInvoke(1, 200, null, null);

            //asyncResult来判断异步调用是否完成
            while (!ar.IsCompleted)
            {
                Console.WriteLine("main thread wating current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("main thread IsAlive " + Thread.CurrentThread.IsAlive);
                Thread.Sleep(50);
            }

            //调用EndInvoke方法来完成异步调用。
            decimal result = d1.EndInvoke(ar);
            Console.WriteLine("the SaveBankAccount return value: {0}", result);
            Console.WriteLine("the SaveBankAccount total runtime: {0}", (DateTime.Now - now).TotalSeconds);
            Console.WriteLine("Now this is Main thread !");
            Console.ReadKey();
        }

        private static decimal SaveBankAccount(decimal data, int ms)
        {
            Console.WriteLine("SaveBankAccount thread started ! Current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("SaveBankAccount thread IsBackground " + Thread.CurrentThread.IsBackground);
            Thread.Sleep(ms);
            Console.WriteLine("SaveBankAccount thread completed !");
            return ++data;
        }
    }
}

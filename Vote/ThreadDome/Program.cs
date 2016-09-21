using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDome
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime Now = DateTime.Now;

            Thread t1 = new Thread(() =>
            {
                Console.WriteLine("Running in a thread t1");
                Func<decimal, int, decimal> f = (data, ms) =>
                {
                    Console.WriteLine("SaveBankAccountPersonA thread started! current run at threadID: " +
                                      Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("SaveBankAccountPersonA thread IsBackground " + Thread.CurrentThread.IsBackground);
                    Thread.Sleep(ms);
                    Console.WriteLine("SaveBankAccountPersonA thread completed!");
                    return ++data;
                };

                var ar = f.BeginInvoke(1, 200, (r) =>
                {
                    if (r == null)
                    {
                        throw new ArgumentException("argument is null");
                    }

                    Thread.Sleep(1000);
                    Console.WriteLine("AsycyCallBackCurrentMoneyPersonA: {0}", f.EndInvoke(r));
                    Console.WriteLine("AsycyCallBackRunTimePersonA: {0}", (DateTime.Now - Now).TotalSeconds);
                    Console.WriteLine("AsycyCallBackSaveBankAccountPersonA thread IsBackground " +
                                      Thread.CurrentThread.IsBackground);
                }, null);

                while (!ar.IsCompleted)
                {
                    Console.WriteLine("thread t1 wating current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(50);
                }
            });

            Thread t2 = new Thread(() =>
            {
                Console.WriteLine("Running in a thread t2");
                Func<decimal, int, decimal> f = (data, ms) =>
                {

                    Console.WriteLine("SaveBankAccountPersonB thread started! current run at threadID: " +
                                      Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("SaveBankAccountPersonB thread IsBackground " + Thread.CurrentThread.IsBackground);
                    Thread.Sleep(ms);
                    Console.WriteLine("SaveBankAccountPersonB thread completed!");
                    return ++data;
                };

                var ar = f.BeginInvoke(1,200, (r) =>
                {
                    if (r == null)
                    {
                        throw new ArgumentException("argument is null");
                    }

                    Console.WriteLine("AsycyCallBackCurrentMoneyPersonB: {0}", f.EndInvoke(r));
                    Console.WriteLine("AsycyCallBackRunTimePersonB: {0}", (DateTime.Now - Now).TotalSeconds);
                    Console.WriteLine("AsycyCallBackSaveBankAccountPersonB thread IsBackground " +
                                      Thread.CurrentThread.IsBackground);
                },null);

                while (!ar.IsCompleted)
                {
                    Console.WriteLine("thread t2 wating current run at treadID: " + Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(50);
                }
            });

            t1.Start();
            t2.Start();

            //Abort具有立即终止线程的作用
            t1.Abort();

            Console.WriteLine("this thread is end !");
            Console.WriteLine("Total time: {0}",(DateTime.Now - Now).TotalSeconds);
            Console.ReadKey();
        }
    }
}

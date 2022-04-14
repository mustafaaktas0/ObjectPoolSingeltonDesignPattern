using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectPoolSingeltonDesignPattern
{
   public  class ClientThread
    {

        ObjectPool instance = ObjectPool.getInstance;

        public void StartProgram()
        {

            Thread th1 = new Thread(new ThreadStart(Thread1));
            Thread th2 = new Thread(new ThreadStart(Thread2));
            Thread th3 = new Thread(new ThreadStart(Thread3));
            Thread th4 = new Thread(new ThreadStart(Thread4));
            Thread th5 = new Thread(new ThreadStart(Thread5));
            Thread th6 = new Thread(new ThreadStart(Thread6));
            Thread th7 = new Thread(new ThreadStart(Thread7));
            Thread th8 = new Thread(new ThreadStart(Thread8));
            Thread th9 = new Thread(new ThreadStart(Thread9));
            Thread th10 = new Thread(new ThreadStart(Thread10));
            Thread th11 = new Thread(new ThreadStart(Thread11));
            Thread th12 = new Thread(new ThreadStart(Thread12));
            Thread th13 = new Thread(new ThreadStart(Thread13));
            Thread th14 = new Thread(new ThreadStart(Thread14));
            Thread th15 = new Thread(new ThreadStart(Thread15));

            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();
            th5.Start();
            th6.Start();
            th7.Start();
            th8.Start();
            th9.Start();
            th10.Start();
            th11.Start();
            th12.Start();
            th13.Start();
            th14.Start();
            th15.Start();
        }

        private void Thread1()
        {
            String name = " Thread-1";
            runInstance(name);
        }

        private void Thread2()
        {
            String name = " Thread-2";
            runInstance(name);
        }

        private void Thread3()
        {
            String name = " Thread-3";
            runInstance(name);


        }
        private void Thread4()
        {
            String name = " Thread-4";
            runInstance(name);

        }
        private void Thread5()
        {
            String name = " Thread-5";
            runInstance(name);

        }
        private void Thread6()
        {
            String name = " Thread-6";
            runInstance(name);


        }
        private void Thread7()
        {
            String name = " Thread-7";
            runInstance(name);

        }
        private void Thread8()
        {
            String name = " Thread-8";
            runInstance(name);

        }
        private void Thread9()
        {
            String name = " Thread-9";
            runInstance(name);

        }
        private void Thread10()
        {
            String name = " Thread-10";
            runInstance(name);


        }
        private void Thread11()
        {
            String name = " Thread-11";
            runInstance(name);


        }
        private void Thread12()
        {
            String name = " Thread-12";
            runInstance(name);

        }
        private void Thread13()
        {
            String name = " Thread-13";
            runInstance(name);
        }
        private void Thread14()
        {
            String name = " Thread-14";
            runInstance(name);


        }
        private void Thread15()
        {
            String name = " Thread-15";
            runInstance(name);

        }

        private void runInstance(string name)
        {
            var instance = this.instance.getLog(); // nesne almaya çalısır
            if (instance == null) // eger nesne alamzsa donguye alınır
            {
                while (true)
                {
                    instance = this.instance.getLog();

                    if (instance != null) break; 
                    Thread.Sleep(10); // thread 10 sn uyur
                }
            }
            Console.WriteLine("alındı thread :"+name);
            Thread.Sleep(2000);
            this.instance.releaseLog(instance);
            Console.WriteLine("bırakıldı thread :"+name);
        }
    }
}

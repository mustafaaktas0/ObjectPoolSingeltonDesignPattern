using System;

namespace ObjectPoolSingeltonDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientThread th = new ClientThread();
            th.StartProgram();
        }
    }
}

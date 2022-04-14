using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPoolSingeltonDesignPattern
{
    public sealed class ObjectPool
    {
        private List<Log> avaiable = new List<Log>(); // clıent pool tutulan yer
        private List<Log> ınUse = new List<Log>(); // clıent pool kullanılan objelerin tutulduğu yer

        private static readonly object lockObject = new object();
        private static ObjectPool instance = null;
        public static ObjectPool getInstance
        {
            get
            {
                lock (lockObject) // multithread safe 
                {
                    if (instance == null)
                    {
                        instance = new ObjectPool();  // pool bir kere instance oluşsun.
                    }
                    return instance;
                }
            }
        }
        public int maxSize = 10;
        private ObjectPool()
        {
            for (int i = 0; i < maxSize; i++)
            {
                avaiable.Add(new Log());
            }

        }


        public Log getLog()
        {

            lock (lockObject)
            {
                if (avaiable.Count > 0)
                {
                    var client = avaiable[0];
                    ınUse.Add(client);
                    avaiable.RemoveAt(0);
                    return client;
                }
                return null;
            }
        }

        public void releaseLog(Log cl)
        {
            lock (lockObject)
            {


                ınUse.Remove(cl);
                avaiable.Add(cl);
            }
        }
    }
}

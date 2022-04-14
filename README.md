# ObjectPoolSingeltonDesignPattern

Object pool singeton design pattern C# örnek uygulaması



## Başlık 

____
Object pool,  dağıtık sistemlerde veya yazılım geliştirici tarafından
yönetilmesi zor olan nesnelerde  kullanılabilecek bir tasarım desenidir.
Creational (Yaratıcı, nesnelerin oluşturulmasına yönelik) desenler
içerisinde yer almaktadır. İstenilen nesnelerin sürekli olarak üretilmesi yerine, 
 başlangıçta bir havuzu oluşturulur ve bu havuz nesneler ile doldurulur.

*_Singleton_*: Bu tasarım örüntüsündeki amaç, 
bir class’tan sadece bir instance yaratılmasını sağlar.
Yani herhangi bir class’tan bir instance yaratılmak istendiğinde,
eğer daha önce yaratılmış bir instance yoksa yeni yaratılır.
 Daha önce yaratılmış ise var olan instance kullanılır.

*_ObjectPool_*: Kullanamak için oluştruduğum nesne havuzu 

*_Log_*: Pahalı veya somutlaştırılması yavaş olan yeniden kullanılabilir nesneler.

*_ClientThread_*: Havuzdaki nesneleri kullanmak isteyen sınıf.

[Blackwasp](http://www.blackwasp.co.uk/ObjectPool.aspx)
____
## Seneryo
Nesne havuzunda önceden hazırlanmış 10 nesne tutulur.
Ancak 10 nesneden  fazla nesneye izin verilmez. 10 nesne kullanımdaysa,
talep eden diğer nesnelerin beklemesi sağlanır. Kod çalıştığında 15 thread çalıştırılır. Bu 15 threadten yalnızca 10'u nesne alabilir.
Kalan 5 thread nesne alana kadar beklemeye alınır. Daha sonra bu 5 thread de nesne havuzu nesne isteğine yanıt verdiğinde çalışmaya devam eder.

## Singelton Yapımı


* Singleton sınıfın sadece bir adet örneğini
referans gösteren private static bir değişken
tanımlanır.


-----------------
        private static ObjectPool instance = null;

-----------------

* Herhangi bir sınıfın bu
sınıfın örnek bir nesnesini yaratmasını
engellemek için private constructor (yapılandırıcı) tanımlanmalıdır.

-----------------
         private ObjectPool()
        {
            for (int i = 0; i < maxSize; i++)
            {
                avaiable.Add(new Log());
            }

        }

-----------------

* Singleton sınıfının sadece örneğine
erişebilen public static bir metot oluşturulması
gerekir. Bu metot eğer sınıfın örneği
oluşturulmadıysa oluşturur ve oluşturulmuş olan
örneği döndürür. Burada multithread uygulamalarda nesnenin her
zaman tek örneğinin yaratılacağını garanti
olması için  için thread
senkronizasyon mekanizmalarından [lock()](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock) keywordu kullanılır.

-----------------
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
-----------------

## ObjectPool Yapımı


* Kullanılan objeler ve objelerin yerini tutmak için listeler tanımlanır.

-----------------
        private List<Log> avaiable = new List<Log>();//objelerin tutulduğu pool
        private List<Log> ınUse = new List<Log>();//clıent pool kullanılan objele tutulduğu yer

-----------------

* Proje ilk çalıştırıldığında maxSize kadar objeyi havuz içerisinde tanımlanır

-----------------
               private ObjectPool()
        {
            for (int i = 0; i < maxSize; i++)
            {
                avaiable.Add(new Log());
            }

        }

-----------------

* *getLog* metodu ile her obje almaya çalıştığında senkronizasyon için [lock()](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock) keywordu kullanılır. Bu method objelerin tutulduğu nesneler listesinden (avaiable) ilk nesneyi alıp kullanılan nesneler listesine (ınUse) ekler ve havuz listesinden (avaiable) o nesneyi siler.

-----------------
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
-----------------

* *releaseLog* metodu ile her obje almaya çalıştığında senkronizasyon için [lock()](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock) keywordu kullanılır. Bu method serbest bırakılacak objeyi kullanılanlar listesinden (ınUse) silip havuz listesine (avaiable) tekrar ekler.

-----------------
         public void releaseLog(Log cl)
        {
            lock (lockObject)
            {


                ınUse.Remove(cl);
                avaiable.Add(cl);
            }
        }
-----------------


=== Referanslar

* http://www.blackwasp.co.uk/ObjectPool.aspx[Blackwasp]
* https://alimozdemir.com/posts/design-pattern-serisi-2-object-pool/ [Alim
Özdemir]
* https://plantuml.com/[Plant UML]
* https://asciidoclive.com/edit/scratch/1[asciidoc]
* https://real-world-plantuml.com/[Real World Plant UML]

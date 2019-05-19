using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //设计模式,单例
            PrintLog("-------------这是单例模式---------------------");
            {
                Console.WriteLine("单例模式的第一种做法");
                //ProductInfoOne productInfo1 = ProductInfoOne.CreateInstance();
                //ProductInfoOne productInfo2 = ProductInfoOne.CreateInstance();
                //通过上面这个做法，就知道了，这个是单例。
                Console.WriteLine("上面这种做法有什么问题呢");
                //
                Console.WriteLine("先插一句,什么是懒汉式，饿汉式");
                Console.WriteLine("懒汉式：利用延迟加载的思想，用到了才去创建实例（或者去内存中找，有了就不new)");
                Console.WriteLine("饿汉式：不管你内存有没有，我都直接去new");
                Console.WriteLine("一般平时编程中，用懒汉式的情况多一点");
                //
            }
            //

            //PrintLog("接下来就是看看刚刚那种情况有什么问题。");
            //{
            //    for (var i = 0; i < 5; i++)
            //    {
            //        Task.Run(() =>
            //        {
            //            ProductInfoOne.CreateInstance();
            //        });
            //    }
            //    //也就是说，在多线程的情况下，还是有可能不是单例。那要怎么解决呢，接下来看第二种实现
            //}

            PrintLog("......多线程的单例...");
            {
                for (var i = 0; i < 5; i++)
                {
                    Task.Run(() =>
                    {
                        ProductInfoTwo.CreateInstance();
                    });
                }
            }

            PrintLog("多线程的单例,人家说的双if加lock是怎么回事呢");
            {
                
                 //这里是双if+lock的。

            }

            Console.ReadKey();



        }

        public static void PrintLog(string msg)
        {
            Console.WriteLine("");
            Console.WriteLine(msg);
        }
    }

    /// <summary>
    /// 产品信息列表
    /// </summary>

    public  class ProductInfoOne
    {
         private static ProductInfoOne _instacne;

        /// <summary>
        /// 单例，构造函数私有
        /// </summary>
        private ProductInfoOne()
        {
            Console.WriteLine($"创建了一次{this.GetType().Name}");
        }

        public static ProductInfoOne CreateInstance()
        {
            if (_instacne == null)
            {
                _instacne=new ProductInfoOne();
            }
            return _instacne;
            
        }
        //怎么判断是否是单例呢。
    }


    public class ProductInfoTwo
    {
        private readonly static object Object = new object();

        private static ProductInfoTwo _instance { get; set; }

        private ProductInfoTwo()
        {
            Console.WriteLine($"创建了一次{this.GetType().Name}");
        }

        public static ProductInfoTwo CreateInstance()
        {
            //外面这个lock，当第一个线程过来的时候，第二个线程，第三个线程过来的时候，
            //加上这个lock，第一个线程进来，就知道给_instance赋值了,所以后面就不要在这里继续排队了。
            //这样也可能提高效率。所以，这样子，就引出了我们双if+lock的写法了。
            lock (Object)
            {
                //如果这里代码这么写的话呢，可能会有问题，为什么呢
                //你自己想想，
                if (_instance == null)
                {
                    _instance = new ProductInfoTwo();
                }
            }
            return _instance;
        }
    }

    public class ProductInfoThree
    {
        private readonly static object Object = new object();

        private static ProductInfoThree _instance { get; set; }

        private ProductInfoThree()
        {
            Console.WriteLine($"创建了一次{this.GetType().Name}");
        }

        public static ProductInfoThree CreateInstance()
        {
            //外面这个lock，当第一个线程过来的时候，第二个线程，第三个线程过来的时候，
            //加上这个lock，第一个线程进来，就知道给_instance赋值了,所以后面就不要在这里继续排队了。
            //这样也可能提高效率。所以，这样子，就引出了我们双if+lock的写法了。
            if (_instance == null)
            {
                //锁这个东西，就是类似于排队，要排队在搞，如果你都获得到instance了，那就没必要排队了。
                lock (Object)
                {
                    //如果这里代码这么写的话呢，可能会有问题，为什么呢
                    //你自己想想，
                    if (_instance == null)
                    {
                        _instance = new ProductInfoThree();
                    }
                }
            }
            return _instance;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonUseCase
{
    class Program
    {
        /// <summary>
        /// 单例模式的应用案例
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var minlevel = ConfigV1.GetInstance()["MinLevel"];
            Console.WriteLine(minlevel);
            var maxLevel = ConfigV1.GetInstance()["MaxLevel"];
            Console.WriteLine(maxLevel);
            Console.ReadKey();
        }
    }


    //配置文件读取类，应该用单例。

    public class ConfigV1
    {
        private static object _lockObj = new object();
        private static ConfigV1 _instance;

        private static Dictionary<string,string> _configItems =new Dictionary<string, string>();

        private ConfigV1()
        {
            Init();
            Console.WriteLine($"{this.GetType().Name}被构造一次");
        }

        public static  ConfigV1 GetInstance()
        {
            if (_instance == null)
            {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance=new ConfigV1();
                    }
                }
            }
            return _instance;
        }


        public string  this[string item]
        {
            get
            {
                return _configItems[item];
            }
        }

        private  void Init()
        {
             var config=ConfigurationManager.AppSettings.AllKeys;
             foreach(var key in config)
            {
                _configItems.Add(key,ConfigurationManager.AppSettings[key]);
            }
        }

         


    }

}

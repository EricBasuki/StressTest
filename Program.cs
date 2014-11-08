using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;

namespace StressTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int TotalThread = Convert.ToInt16(ConfigurationSettings.AppSettings["TotalThread"]);
            int Looping = Convert.ToInt16(ConfigurationSettings.AppSettings["Looping"]);
            string DBServer = ConfigurationSettings.AppSettings["DBServer"];
            string DBUserName = ConfigurationSettings.AppSettings["DBUserName"];
            string DBPassword = ConfigurationSettings.AppSettings["DBPassword"];
            string DBInitialCatalog = ConfigurationSettings.AppSettings["DBInitialCatalog"];

            Worker[] x = new Worker[TotalThread];
            //Worker x = new Worker("MyId", Looping, DBServer, DBInitialCatalog, DBUserName, DBPassword);
            Thread[] thread=new Thread[TotalThread];

            for (int i = 0; i < TotalThread; i++)
            {
                x[i] = new Worker("MyId", Looping, DBServer, DBInitialCatalog, DBUserName, DBPassword);
                thread[i] = new Thread(new ThreadStart(x[i].DoTest));
                thread[i].Start();
            }



            Thread.Sleep(60000);
            Console.WriteLine(thread[0].IsAlive);
            Thread.Sleep(5000);
        }
    }
}

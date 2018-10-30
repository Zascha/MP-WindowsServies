﻿using Topshelf;

namespace MP.WindowsServices.ServiceInstance
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(conf =>
            {
                conf.Service<ServiceInstance>(s =>
                {
                    s.ConstructUsing(() => new ServiceInstance());
                    s.WhenStarted(serv => serv.Start());
                    s.WhenStopped(serv => serv.Stop());
                }).UseNLog(LoggerFactory.Instance);
                conf.SetServiceName("PostScanerService");
                conf.SetDisplayName("PostScaner Service");
                conf.StartAutomaticallyDelayed();
            });
        }
    }
}

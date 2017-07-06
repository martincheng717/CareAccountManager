using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CareGateway.Note.Controller;

namespace Tests.Controller
{
    public class BaseFixture<T>:IDisposable
    {
        public IContainer Container { get; set; }
        public ContainerBuilder Builder { get; set; }
        public BaseFixture()
        {
            Builder = new ContainerBuilder();
            Builder.RegisterType<T>()
                 .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
        public void Dispose()
        {
        }
    }
}

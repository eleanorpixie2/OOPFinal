using ClassLibraryFinal;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestFinal.NinjectModules;
using ClassLibraryFinal.NinjectModules;

namespace UnitTestFinal
{
    public class TestShippingServiceProvider : IServiceProvider
    {
        IKernel kernel;

        public TestShippingServiceProvider()
        {
            kernel = new StandardKernel(new ShippingServiceModule(), new ShippingServiceTestModule());
        }

        public object GetService(Type serviceType)
        {
            switch(serviceType.Name)
            {
                case "IShippingService": 
                    //Defer to ninject for test injection
                    return kernel.Get<IShippingService>();
                    
                default:
                    return null;

            }
        }
    }
}

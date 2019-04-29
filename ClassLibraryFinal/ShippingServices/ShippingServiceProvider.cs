using ClassLibraryFinal.NinjectModules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFinal
{
    /// <summary>
    /// Provides a default shipping service to clients
    /// </summary>
    public class ShippingServiceProvider : IServiceProvider
    {
        IKernel kernel;

        public ShippingServiceProvider()
        {
            kernel = new StandardKernel(new ShippingServiceModule());
        }

        public object GetService(Type serviceType)
        {
            if (serviceType is IShippingService)
            {
                //Probably remove this in production
                //until then use ninject
                return kernel.Get<IShippingService>();
            }

            return null;
        }
    }
}
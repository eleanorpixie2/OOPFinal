using ClassLibraryFinal;
using Moq;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFinal.NinjectModules
{
    /// <summary>
    /// Injects Fakes and Mocks for unti Tests
    /// </summary>
    class ShippingServiceTestModule : NinjectModule
    {
        FakeProductList FakeProductList;    //Create a fake product list

        public ShippingServiceTestModule()
        {
            //Mock a fake product
            var moqProduct = new Mock<IProduct>();
            moqProduct.Setup(m => m.Price).Returns(1);
            moqProduct.Setup(m => m.ShippingWeight).Returns(1);
            moqProduct.Setup(m => m.TaxRate).Returns(0);

            FakeProductList = new FakeProductList() { moqProduct.Object }; 
        }


        public override void Load()
        {
            this.Bind<List<IProduct>>().To<FakeProductList>();
            this.Bind<IShippingLocation>().To<FakeShippingLocation>();
        }
    }

    class FakeProductList : List<IProduct>
    {

    }

    class FakeShippingLocation : IShippingLocation
    {
        public uint StartZipCode { get; }

        public uint DestinationZipCode { get; }

        public FakeShippingLocation()
        {
            this.StartZipCode = 60605;
            this.DestinationZipCode = 60805;
        }
    }


}

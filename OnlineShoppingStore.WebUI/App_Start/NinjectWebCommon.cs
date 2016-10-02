[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnlineShoppingStore.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnlineShoppingStore.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace OnlineShoppingStore.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    
    using OnlineShoppingStore.Domain;
    using OnlineShoppingStore.Domain.Abstract;
    using Moq;
    using System.Collections.Generic;
    using OnlineShoppingStore.Domain.Entities;
    using OnlineShoppingStore.Domain.Concrete;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);

                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind<IAuthentication>().To<FormsAuthenticationProvider>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();

            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //        new Product{ Name= "Football", Price=5},
            //        new Product{ Name= "Surf Board", Price=79},
            //        new Product{ Name= "Running Shoes", Price=25}
            //});
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }        
    }
}

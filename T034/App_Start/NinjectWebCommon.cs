using System.Configuration;
using T034.Api;
using T034.Api.DataAccess;
using T034.Api.Services;
using T034.Api.Services.Administration;
using T034.Api.Services.Vgiik;
using T034.Repository;
using T034.Tools.Auth;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(T034.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(T034.App_Start.NinjectWebCommon), "Stop")]

namespace T034.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IAuthentication>().To<CustomAuthentication>().InRequestScope();

            kernel.Bind<IBaseDb>().ToMethod(c => new NhDbFactory(ConnectionString).CreateBaseDb());
            kernel.Bind<IRepository>().To<Repository.Repository>().InRequestScope();


            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();
            kernel.Bind<IFileService>().To<FileService>().InRequestScope();

            kernel.Bind<IPublicationService>().To<PublicationService>().InRequestScope();
        }

        private static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["DatabaseFile"].ConnectionString; } }
    }
}

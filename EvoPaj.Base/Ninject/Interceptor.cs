using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;
using System.Web;

namespace EvoPaj.Base.Ninject
{
    public class Interceptor
    {
        //private const string INTERCEPTOR = ":::INTERCEPT:";
        private static IKernel kernel;
        public static IKernel Instance
        {
            get
            {
                if (kernel == null)
                {
                    kernel = new StandardKernel();
                    //kernel.Load((IModule)Activator.CreateInstance(Type.GetType(Configuration.ConfigurationManager.ExtensionModuleAssembly)));
                }
                return kernel;
            }
        }

        //public static IKernel SessionInstance
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[INTERCEPTOR] == null)
        //        {
        //            kernel = new StandardKernel();
        //            kernel.Load((IModule)Activator.CreateInstance(Type.GetType(Configuration.ConfigurationManager.ExtensionModuleAssembly)));
        //            HttpContext.Current.Session[INTERCEPTOR] = kernel;
        //        }
        //        return HttpContext.Current.Session[INTERCEPTOR] as IKernel;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;

namespace ShinyChat.Core.DI
{
    public static class DiContainer
    {
        private static WindsorContainer _container;

        public static void CreateContainer()
        {
            if (_container == null)
                _container = new WindsorContainer();
        }

        public static void DestroyContainer()
        {
            _container = null;
        }

        public static WindsorContainer Container 
        {
            get
            {
                return _container;
            }
        }
    }
}

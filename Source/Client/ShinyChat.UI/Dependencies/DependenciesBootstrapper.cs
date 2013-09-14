using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using ShinyChat.Common.Entities;
using ShinyChat.Core.Entities;
using ShinyChat.Core.DI;
using ShinyChat.Common.Logging;
using ShinyChat.Core.Logging;

namespace ShinyChat.Dependencies
{
    public class DependenciesBootstrapper
    {
        public static void Init()
        {
            // Initialize Windsor DI/IoC Container and register components/dependencies
            DiContainer.CreateContainer();

            // Register Id Counter
            DiContainer.Container.Register(Component.For<IInternalIdCounter>().ImplementedBy<InternalIdCounter>());

            // Register LogManager
            DiContainer.Container.Register(Component.For<ILoggingManager>().ImplementedBy<LoggingManager>());
        }
    }
}

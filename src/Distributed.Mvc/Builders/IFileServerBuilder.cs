using System;

namespace CityOs.FileServer.Distributed.Mvc
{
    public interface IFileServerBuilder
    {
        /// <summary>
        /// Try add singleton to the service collection
        /// </summary>
        /// <typeparam name="TInterface">The interface</typeparam>
        /// <typeparam name="TImplementation">The implementation of the interface</typeparam>
        void TryAddSingleton<TInterface, TImplementation>()
            where TImplementation : class, TInterface
            where TInterface : class;

        /// <summary>
        /// Try add singleton to the service collection
        /// </summary>
        /// <typeparam name="TInterface">The interface</typeparam>
        /// <typeparam name="TImplementation">The implementation</typeparam>
        /// <param name="implementationFactory">The factory of the implementation</param>
        void TryAddSingleton<TInterface, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TImplementation : class, TInterface
            where TInterface : class;
    }
}

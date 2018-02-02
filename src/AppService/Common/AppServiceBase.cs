using AutoMapper;

namespace CityOs.FileServer.AppService
{
    public abstract class AppServiceBase
    {
        /// <summary>
        /// Initialize a default <see cref="AppServiceBase"/>
        /// </summary>
        /// <param name="mapper">The mapper</param>
        public AppServiceBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Gets the mapper
        /// </summary>
        protected IMapper Mapper { get; }
    }
}

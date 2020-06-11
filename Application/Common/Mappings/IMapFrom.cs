using AutoMapper;

namespace Application.Common.Mappings
{
    // Special thanks to Jason Taylor & his Github project Clean Architecture!!
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.Extensions
{
    public static class AutoMapperExtension
    {
        //public static IQueryable<TDestination> MapTo<TDestination>(
        //    this IQueryable source,
        //    params Expression<Func<TDestination, object>>[] membersToExpand
        //)
        //{
        //    var data = source.ProjectTo<TDestination>(AutoMapperConfig.Get(), null, membersToExpand);

        //    return data;
        //}

        //public static T MapTo<T>(this object src)
        //{
        //    IMapper mapper = new Mapper(AutoMapperConfig.Get());
        //    return (T)mapper.Map(src, src.GetType(), typeof(T));
        //}
        //public static TDest MapTo<T, TDest>(this T src, TDest data)
        //{
        //    IMapper mapper = new Mapper(AutoMapperConfig.Get());

        //    return mapper.Map<T, TDest>(src, data);
        //}

        //public static T MapTo<T>(this T src, T data)
        //{
        //    IMapper mapper = new Mapper(AutoMapperConfig.Get());
        //    return mapper.Map<T, T>(data, src);
        //}

        //public class AutoMapperConfig
        //{
        //    public static MapperConfiguration Get()
        //    {
        //        var mappingConfig = new MapperConfiguration(mc =>
        //        {
        //            mc.AddProfile(new AuthoMapperProfile());
        //        });
        //        return mappingConfig;
        //    }
        //}

        //public class AuthoMapperProfile : Profile
        //{

        //    public AuthoMapperProfile()
        //    {

        //    }
        //}
    }
}

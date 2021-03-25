using AutoMapper;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.Controllers;
using Delivery.Metrics.Helpers;

namespace Delivery.Metrics.Profiles
{
    public class MetricsRequestProfiles : Profile
    {
        public MetricsRequestProfiles()
        {
            CreateMap<MetricsRequestDto, MetricsRequest>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.GetUnixTime()));
        }
    }
}
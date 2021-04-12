using AutoMapper;
using Delivery.Metrics.Common.Contracts;
using Delivery.Metrics.Helpers;

namespace Delivery.Metrics.Profiles
{
    public class MetricsRequestProfiles : Profile
    {
        public MetricsRequestProfiles()
        {
            CreateMap<MetricsRequestDto, MetricsRequest>()
                .ForMember(
                    dest => dest.StartTime,
                    opt => opt.MapFrom(
                        src => src.StartDate.GetUnixTime()))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(
                        src => src.EndDate.GetUnixTime()));

        }
    }
}
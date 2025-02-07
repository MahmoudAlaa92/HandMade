global using AutoMapper;
using HandMadeEcommece.Models.Data;

namespace HandMadeEcommece.helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

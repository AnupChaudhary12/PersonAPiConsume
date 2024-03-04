using AutoMapper;
using PersonAPiConsume.Models;

namespace PersonAPiConsume.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, AddPersonViewModel>().ReverseMap();
        }
    }
}

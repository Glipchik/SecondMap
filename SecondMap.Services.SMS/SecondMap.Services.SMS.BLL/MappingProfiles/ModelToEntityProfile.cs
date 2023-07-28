using AutoMapper;
using SecondMap.Services.SMS.BLL.Models;
using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.BLL.MappingProfiles
{
	public class ModelToEntityProfile : Profile
	{
		public ModelToEntityProfile()
		{
			CreateMap<Review, ReviewEntity>().ReverseMap();
			CreateMap<Schedule, ScheduleEntity>().ReverseMap();
			CreateMap<Store, StoreEntity>().ReverseMap();
			CreateMap<User, UserEntity>().ReverseMap();
		}
	}
}

using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.BLL.MappingProfiles
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

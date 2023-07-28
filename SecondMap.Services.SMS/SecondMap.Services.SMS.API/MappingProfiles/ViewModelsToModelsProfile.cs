using AutoMapper;
using SecondMap.Services.SMS.API.Dto;
using SecondMap.Services.SMS.API.ViewModels;
using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.API.MappingProfiles
{
	public class ViewModelsToModelsProfile : Profile
	{
		public ViewModelsToModelsProfile()
		{
			CreateMap<ReviewViewModel, Review>().ReverseMap();
			CreateMap<ScheduleViewModel, Schedule>().ReverseMap();
			CreateMap<StoreViewModel, Store>().ReverseMap();
			CreateMap<UserViewModel, User>().ReverseMap();

			CreateMap<Review, ReviewDto>().ReverseMap();
			CreateMap<Store, StoreDto>().ReverseMap();
			CreateMap<Schedule, ScheduleDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
		}
	}
}

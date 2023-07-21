using AutoMapper;
using SecondMap.Services.StoreManagementService.API.Dto;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.API.MappingProfiles
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

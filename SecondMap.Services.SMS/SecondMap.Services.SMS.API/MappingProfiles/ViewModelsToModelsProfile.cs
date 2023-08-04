using AutoMapper;
using SecondMap.Services.SMS.API.Dto;
using SecondMap.Services.SMS.API.ViewModels;
using SecondMap.Services.SMS.API.ViewModels.AddModels;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;
using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.API.MappingProfiles
{
	public class ViewModelsToModelsProfile : Profile
	{
		public ViewModelsToModelsProfile()
		{
			CreateMap<ReviewAddViewModel, Review>().ReverseMap();
			CreateMap<ScheduleAddViewModel, Schedule>().ReverseMap();
			CreateMap<StoreViewModel, Store>().ReverseMap();
			CreateMap<UserViewModel, User>().ReverseMap();

			CreateMap<ReviewUpdateViewModel, Review>().ReverseMap();
			CreateMap<ScheduleUpdateViewModel, Schedule>().ReverseMap();

			CreateMap<Review, ReviewDto>().ReverseMap();
			CreateMap<Store, StoreDto>(); 
			CreateMap<Schedule, ScheduleDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
		}
	}
}

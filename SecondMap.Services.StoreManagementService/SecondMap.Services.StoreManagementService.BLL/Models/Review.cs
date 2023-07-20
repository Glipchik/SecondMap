﻿using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.BLL.Models
{
	public class Review
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int StoreId { get; set; }
		public string? Description { get; set; }
		public int Rating { get; set; }

		public UserEntity? User { get; set; }
		public StoreEntity? Store { get; set; }
	}
}

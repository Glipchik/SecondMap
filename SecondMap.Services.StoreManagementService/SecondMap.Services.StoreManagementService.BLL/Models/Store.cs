﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondMap.Services.StoreManagementService.BLL.Models
{
	public class Store
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
		public int? Rating { get; set; }
		public decimal Price { get; set; }
	}
}

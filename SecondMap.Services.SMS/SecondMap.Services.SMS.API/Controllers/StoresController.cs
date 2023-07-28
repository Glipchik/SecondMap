﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.SMS.API.Dto;
using SecondMap.Services.SMS.API.ViewModels;
using SecondMap.Services.SMS.BLL.Constants;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.API.Controllers
{
	[Route(ApiEndpoints.API_CONTROLLER_ROUTE)]
	[ApiController]
	public class StoresController : ControllerBase
	{
		private readonly IStoreService _storeService;
		private readonly IMapper _mapper;

		public StoresController(IStoreService storeService, IMapper mapper)
		{
			_storeService = storeService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IEnumerable<StoreDto>>(await _storeService.GetAllAsync()));
		}

		[HttpGet(ApiEndpoints.ID)]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundStore = _mapper.Map<StoreDto>(await _storeService.GetByIdAsync(id));

			return Ok(foundStore);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] StoreViewModel storeToAdd)
		{
			var addedStore = _mapper.Map<Store>(await _storeService.AddStoreAsync(_mapper.Map<Store>(storeToAdd)));

			return Ok(addedStore);
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] StoreViewModel storeToUpdate)
		{
			var mappedStoreToUpdate = _mapper.Map<Store>(storeToUpdate);
			mappedStoreToUpdate.Id = id;

			var updatedStore = _mapper.Map<StoreDto>(await _storeService.UpdateStoreAsync(mappedStoreToUpdate));

			return Ok(updatedStore);
		}

		[HttpDelete(ApiEndpoints.ID)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await _storeService.DeleteStoreAsync(id);

			return NoContent();
		}
	}
}
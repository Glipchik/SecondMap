using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.API.Dto;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoresController : ControllerBase
	{
		private readonly IStoreService _storeService;
		private readonly IMapper _mapper;
		private readonly IValidator<StoreViewModel> _validator;

		public StoresController(IStoreService storeService, IMapper mapper, IValidator<StoreViewModel> validator)
		{
			_storeService = storeService;
			_mapper = mapper;
			_validator = validator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<List<StoreDto>>(await _storeService.GetAllAsync()));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundStore = _mapper.Map<StoreDto>(await _storeService.GetByIdAsync(id));

			return Ok(foundStore);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] StoreViewModel storeToAdd)
		{
			var validationResult = await _validator.ValidateAsync(storeToAdd);

			if (!validationResult.IsValid)
			{
				throw new ValidationFailException($"Validation failed: {validationResult.Errors[0]}");
			}

			await _storeService.AddStoreAsync(_mapper.Map<Store>(storeToAdd));

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] StoreViewModel storeToUpdate)
		{
			var validationResult = await _validator.ValidateAsync(storeToUpdate);

			if (!validationResult.IsValid)
			{
				throw new ValidationFailException($"Validation failed: {validationResult.Errors[0]}");
			}

			var mappedStoreToUpdate = _mapper.Map<Store>(storeToUpdate);
			mappedStoreToUpdate.Id = id;

			var updatedStore = _mapper.Map<StoreDto>(await _storeService.UpdateStoreAsync(mappedStoreToUpdate));

			return Ok(updatedStore);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundStore = await _storeService.GetByIdAsync(id);

			await _storeService.DeleteStoreAsync(foundStore);

			return NoContent();
		}
	}
}

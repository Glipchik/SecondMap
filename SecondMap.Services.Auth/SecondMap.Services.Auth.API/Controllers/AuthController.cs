using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.Auth.API.RequestModels;
using SecondMap.Services.Auth.Application.Services.Abstract;
using SecondMap.Services.Auth.API.Constants;

namespace SecondMap.Services.Auth.API.Controllers
{
	[Route(ApiRoutes.AUTH)]
	public class AuthController : Controller
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly IValidator<LoginRequestModel> _loginValidator;
		private readonly IValidator<RegisterRequestModel> _registerValidator;

		public AuthController(IAuthorizationService authorizationService,
			IValidator<LoginRequestModel> loginValidator,
			IValidator<RegisterRequestModel> registerValidator)
		{
			_authorizationService = authorizationService;
			_loginValidator = loginValidator;
			_registerValidator = registerValidator;
		}

		[HttpGet, Route(ApiRoutes.LOGIN)]
		public ActionResult Login(string returnUrl)
		{
			return View(new LoginRequestModel { ReturnUrl = returnUrl });
		}

		[HttpPost, Route(ApiRoutes.LOGIN)]
		public async Task<ActionResult> LoginAsync(LoginRequestModel requestModel)
		{
			var validationResult = await _loginValidator.ValidateAsync(requestModel);

			if (!validationResult.IsValid)
			{
				validationResult.Errors.ForEach(x =>
					ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

				return View(requestModel);
			}

			var authResult = await _authorizationService
				.LoginAsync(requestModel.Email, requestModel.Password);

			if (!authResult.Success)
			{
				ModelState.AddModelError(nameof(LoginRequestModel.Email), authResult.ErrorMessage!);

				return View(requestModel);
			}

			return Redirect(requestModel.ReturnUrl);
		}

		[HttpGet, Route(ApiRoutes.REGISTER)]
		public ActionResult Register(string returnUrl)
		{
			return View(new RegisterRequestModel { ReturnUrl = returnUrl });
		}

		[HttpPost, Route("register")]
		public async Task<ActionResult> Register(RegisterRequestModel requestModel)
		{
			var validationResult = await _registerValidator.ValidateAsync(requestModel);

			if (!validationResult.IsValid)
			{
				validationResult.Errors.ForEach(x =>
					ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

				return View(requestModel);
			}

			var authResult = await _authorizationService.RegisterAsync(requestModel.UserName,
				requestModel.Email, requestModel.Password);

			if (!authResult.Success)
			{
				ModelState.AddModelError(nameof(LoginRequestModel.Email), authResult.ErrorMessage!);

				return View(new RegisterRequestModel { ReturnUrl = requestModel.ReturnUrl });
			}

			return Redirect(requestModel.ReturnUrl);
		}

		[HttpGet, Route("logout")]
		public async Task<ActionResult> Logout()
		{
			if (User.Identity!.IsAuthenticated)
			{
				await HttpContext.SignOutAsync();
			}

			var myCookies = Request.Cookies.Keys;
			foreach (string cookie in myCookies)
			{
				Response.Cookies.Delete(cookie);
			}

			return Redirect("login");
		}
	}
}

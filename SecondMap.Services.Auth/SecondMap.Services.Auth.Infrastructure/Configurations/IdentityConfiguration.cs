using IdentityServer4;
using IdentityServer4.Models;
using SecondMap.Services.Auth.Domain.Entities;
using SecondMap.Services.Auth.Domain.Enums;
using SecondMap.Services.Auth.Infrastructure.Constants;

namespace SecondMap.Services.Auth.Infrastructure.Configurations
{
	public static class IdentityConfiguration
	{
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};
		}

		public static IEnumerable<ApiScope> GetApiScopes()
		{
			return new List<ApiScope>
			{
				new ApiScope(ConfigurationConstants.SMS_API_SCOPE, ConfigurationConstants.SMS_API_PUBLIC),
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource(ConfigurationConstants.SMS_API_SCOPE, ConfigurationConstants.SMS_API_PUBLIC)
				{
					Scopes = { ConfigurationConstants.SMS_API_SCOPE }
				},
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client()
				{
					ClientId = ConfigurationConstants.TEST_CLIENT_ID,
					ClientName = ConfigurationConstants.TEST_CLIENT_PUBLIC,
					ClientSecrets = { new Secret(ConfigurationConstants.TEST_CLIENT_SECRET.Sha256()) },
					RequireConsent = false,
					RedirectUris = { ConfigurationConstants.TEST_CLIENT_REDIRECT_URI },
					PostLogoutRedirectUris = { ConfigurationConstants.TEST_CLIENT_POST_LOGOUT_REDIRECT_URI },
					AllowedGrantTypes = { GrantType.AuthorizationCode },
					AllowOfflineAccess = true,
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.OfflineAccess,
						ConfigurationConstants.SMS_API_SCOPE,
					},
				},
				new Client
				{
					ClientId = ConfigurationConstants.POSTMAN_CLIENT_ID,
					ClientName = ConfigurationConstants.POSTMAN_CLIENT_PUBLIC,
					ClientSecrets = { new Secret(ConfigurationConstants.POSTMAN_CLIENT_SECRET.Sha256()) },
					RequireClientSecret = false,
					RequireConsent = false,
					RequirePkce = false,
					RedirectUris = { ConfigurationConstants.POSTMAN_CLIENT_REDIRECT_URI },
					PostLogoutRedirectUris = { ConfigurationConstants.POSTMAN_CLIENT_POST_LOGOUT_REDIRECT_URI },
					AllowAccessTokensViaBrowser = true,
					AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
					AllowOfflineAccess = true,
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.OfflineAccess,
						ConfigurationConstants.SMS_API_SCOPE,
					}
				}
			};
		}

		public static AuthUser GetTestAuthUser()
		{
			return new AuthUser
			{
				UserName = "JoeDoe",
				Email = "joeDoe@email.com"
			};
		}

		public static AuthUser GetTestAdmin()
		{
			return new AuthUser
			{
				UserName = "admin",
				Email = "admin@email.com"
			};
		}

		public static IEnumerable<string> GetRoles()
		{
			return Enum.GetValues(typeof(Roles))
				.Cast<Roles>()
				.Select(x => x.ToString());
		}
	}
}

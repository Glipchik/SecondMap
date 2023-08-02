using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondMap.Services.SMS.IntegrationTests.Utilities
{
	public static class PathBuilder
	{
		public static string BuildPath(params string[] paths)
		{
			return string.Join(' ', paths);
		}
	}
}

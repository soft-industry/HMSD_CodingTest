using System.Threading.Tasks;

namespace WebApps.ApiGateway.Extensions
{
    public static class ResponseExtensions
    {
        public static async Task<Microsoft.AspNetCore.Mvc.IActionResult> GetActionResult(this System.Net.Http.HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new Microsoft.AspNetCore.Mvc.OkObjectResult(content);
            }
            else
            {
                var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ErrorResult>(content);

                return new Microsoft.AspNetCore.Mvc.JsonResult(error)
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }
    }
}

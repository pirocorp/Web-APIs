namespace RequestService.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RequestService.Policies;

    [ApiController]
    [Route("api/[Controller]")]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy clientPolicy;

        public RequestController(ClientPolicy clientPolicy)
        {
            this.clientPolicy = clientPolicy;
        }

        /// <summary>
        /// Makes request to Response Service Response Controller with a given id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>Response of the Response Service</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> MakeRequest(int id)
        {
            HttpClient client = new HttpClient();

            //var response = await client.GetAsync($"https://localhost:7215/api/Response/{id}");

            var response = await this.clientPolicy.LinearHttpRetry.ExecuteAsync(
                () => client.GetAsync($"https://localhost:7215/api/Response/{id}"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Response Service returned SUCCESS <--");
                return this.Ok();
            }

            Console.WriteLine("--> Response Service returned FAILURE <--");
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

namespace ResponseService.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[Controller]")]
    public class ResponseController : ControllerBase
    {
        /// <summary>
        /// Returns randomly successful response. The higher the Id the probability of the successful response is also higher 
        /// </summary>
        /// <param name="id">The id of the response</param>
        /// <returns>Response</returns>
        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult> GetResponse(int id)
        {
            Random random = new Random();
            var randomInteger = random.Next(1, 101);

            if (randomInteger >= id)
            {
                Console.WriteLine("--> Failure - Generate a HTTP 500 <--");
                return await Task.FromResult(this.StatusCode(StatusCodes.Status500InternalServerError));
            }

            return await Task.FromResult(this.Ok());
        }
    }
}

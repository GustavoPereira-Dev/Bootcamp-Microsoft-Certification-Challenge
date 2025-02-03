using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using System.IO.StreamReader;
using MovieRequest;
using Task

namespace postDatabase
{
    public class fnPostDatabase
    {
        private readonly ILogger<fnPostDatabase> _logger;

        public fnPostDatabase(ILogger<fnPostDatabase> logger)
        {
            _logger = logger;
        }

        [Function("movie")]
        [CosmosDBOutput("movies", "movies", ConnectionStringSetting = "CosmosDBConnection", CreateIfNotExists = true, PartitionKey = "title")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            MovieRequest movie = null;

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            try{
                movie = JsonConvert.DeserializeObject<MovieRequest>(content);
            } catch{
                return new BadRequestObjectResult("Erro ao deserializar o objeto: " + ex.Message);  
            } 

            return JsonConvert.SerializeObject(movie);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace MongoDbDemoApi1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMongoDatabase _mongoDatabase;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMongoDatabase mongoDatabase)
        {
            _logger = logger;
            _mongoDatabase = mongoDatabase;
        }

        public List<T> InsertMany<T>(List<T> list)
        {
            var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
            collection.InsertMany(list);
            return list;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public IActionResult AddList()
        {
            List<MongoDBPostTest> list = new List<MongoDBPostTest>()
            {
                new MongoDBPostTest()
                {
                    Id = "2",
                    Body = "Test note 3",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new NoteImage
                    {
                        ImageSize = 10,
                        Url = "http://localhost/image1.png",
                        ThumbnailUrl = "http://localhost/image1_small.png"
                    }
                },
                new MongoDBPostTest()
                {
                    Id = "3",
                    Body = "Test note 4",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new NoteImage
                    {
                        ImageSize = 14,
                        Url = "http://localhost/image3.png",
                        ThumbnailUrl = "http://localhost/image3_small.png"
                    }
                }
            };

            try
            {
                 var ss=InsertMany(list);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Ok("成功");
        }
    }

    public class MongoDBPostTest
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UserId { get; set; }
        public NoteImage HeaderImage { get; set; }

    }
    public class NoteImage
    {
        public int ImageSize { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDbDemoApi1.MongodbHepler;

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
        private readonly IMongoDbBase _mongoDbBase;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoDbBase mongoDbBase)
        {
            _logger = logger;
            _mongoDbBase = mongoDbBase;
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
                 var ss= _mongoDbBase.InsertManyAsync(list);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Ok("成功");
        }

        [HttpGet]
        public async Task<IEnumerable<MongoDBPostTest>> SelectSingle()
        {
            //无条件
            var list =await _mongoDbBase.GetIEnumerableAsync<MongoDBPostTest>(e=>e.UserId==1);

            //有条件
            //var list = _context.GetList<MongoDBPostTest>(a => a.Id == "1");

            //得到单条数据,无条件
            //var list = _context.GetSingle<MongoDBPostTest>();

            //得到单条数据,有条件
            //var list = _context.GetSingle<MongoDBPostTest>(a => a.Id == "3");

            //ObjectId internalId = _mongoDbBase.GetInternalId("5bbf41651d3b66668cbb5bfc");

            //var a = _context.GetSingle<MongoDBPostTest>(note => note.Id == "5bbf41651d3b66668cbb5bfc" || note.InternalId == internalId);

            //return ResHelper.Suc(1, list, "成功");
            return list;
        }
    }
}

    public class MongoDBPostTest
    {
        [BsonId]
        // standard BSonId generated by MongoDb
        public ObjectId InternalId { get; set; }
        public string Id { get; set; }

        public string Body { get; set; } = string.Empty;

        [BsonDateTimeOptions]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public NoteImage HeaderImage { get; set; }

        public int UserId { get; set; } = 0;
    }

    public class NoteImage
    {
        public string Url { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public long ImageSize { get; set; } = 0L;
    }


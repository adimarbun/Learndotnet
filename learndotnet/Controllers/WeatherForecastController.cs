using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using learndotnet.Models;
using learndotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Producerr;

namespace learndotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ShipwreckService _shipwreckService;
        private readonly producerTest1 _producertTest1;



        public WeatherForecastController(IDatabaseSettings settings)
        {
            _shipwreckService = new ShipwreckService(settings);
            _producertTest1 = new producerTest1();
        }

        [HttpGet]
        public Task<Shipwreck> Get()
        {
            var result = _shipwreckService.GetData();
            return result;
        }



        [HttpPost]
        public ActionResult<SubmissionModel> Create(SubmissionModel submission)
        {
            submission.Created = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneConverter.TZConvert.GetTimeZoneInfo("SE Asia Standard Time"));
            var result = new SubmissionModel();
            if (ModelState.IsValid)
            {
                result = _shipwreckService.Create(submission);

            }
            return result;
        }

        [HttpPost]
        [Route("sendProducer/{msg}")]
        public ActionResult<string> SendProducer (string msg)
        {
            var producer = _producertTest1.PublishAsync(msg);
            return producer.Result;
        }
    }
}

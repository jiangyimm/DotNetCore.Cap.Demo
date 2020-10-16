using DotNetCore.Cap.Demo.Publisher.PgModels;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DotNetCore.Cap.Demo.Publisher.Controllers
{
    [ApiController]
    [Route("api/publish")]
    public class PublishController : ControllerBase
    {
        ICapPublisher _capPublisher;
        PgDbContext _pgDbContext;

        public PublishController(ICapPublisher capPublisher, PgDbContext pgDbContext)
        {
            _capPublisher = capPublisher;
            _pgDbContext = pgDbContext;
        }

        [HttpGet("string")]
        public async Task<IActionResult> PublishString()
        {
            await _capPublisher.PublishAsync("sample.rabbitmq.demo.string", "this is text!");
            return Ok();
        }

        [HttpGet("dynamic")]
        public async Task<IActionResult> PublishDynamic()
        {
            await _capPublisher.PublishAsync("sample.rabbitmq.demo.dynamic", new
            {
                Name = "jiangyi",
                Age = 28
            });
            return Ok();
        }

        [HttpGet("object")]
        public async Task<IActionResult> PublishObject()
        {
            await _capPublisher.PublishAsync("sample.rabbitmq.demo.object", new Person
            {
                Name = "jiangyi222",
                Age = 30
            });
            return Ok();
        }

        [HttpGet("transcation")]
        public async Task<IActionResult> PublishWithTranscation()
        {
            using (var trans = _pgDbContext.Database.BeginTransaction(_capPublisher, true))
            {
                try
                {
                    var apiConfig = new ApiConfig
                    {
                        ApiName = "111122",
                        ApiDesc = "223",
                        ReturnType = "1",
                        ReturnExpect = "1",
                        IsAsync = true,
                        OperCode = "999",
                        OperTime = DateTime.Now
                    };

                    await _pgDbContext.ApiConfig.AddAsync(apiConfig);
                    await _pgDbContext.SaveChangesAsync();

                    _capPublisher.Publish("sample.rabbitmq.demo.transcation", apiConfig);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }

                return Ok();
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public short Age { get; set; }
    }
}

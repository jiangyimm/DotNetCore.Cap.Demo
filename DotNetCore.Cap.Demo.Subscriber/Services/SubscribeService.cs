using DotNetCore.Cap.Demo.Subscriber.Controllers;
using DotNetCore.Cap.Demo.Subscriber.PgModels;
using DotNetCore.CAP;
using System;

namespace DotNetCore.Cap.Demo.Subscriber.Services
{
    /// <summary>
    /// Controller作为订阅者时，不用继承ICapSubscribe
    /// Service作为订阅者时，必须继承ICapSubscribe
    /// </summary>
    public class SubscriberService : ICapSubscribe
    {
        PgDbContext _pgDbContext;
        ICapPublisher _capPublisher;

        public SubscriberService(PgDbContext pgDbContext, ICapPublisher capPublisher)
        {
            _pgDbContext = pgDbContext;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 说明：Controller和Service同时订阅时，只触发了Service的消费
        /// </summary>
        /// <param name="text"></param>
        [CapSubscribe("sample.rabbitmq.demo.string")]
        public void SubscriberString(string text)
        {
            Console.WriteLine($"【SubscriberString】Subscriber invoked, Info: {text}");
        }

        [CapSubscribe("sample.rabbitmq.demo.dynamic")]
        public void SubscriberDynamic(dynamic person)
        {
            Console.WriteLine($"【SubscriberDynamic】Subscriber invoked, Info: {person.Name} {person.Age}");
        }

        [CapSubscribe("sample.rabbitmq.demo.object")]
        public void SubscriberObject(Person person)
        {
            Console.WriteLine($"【SubscriberObject】Subscriber invoked, Info: {person.Name} {person.Age}");
        }

        [CapSubscribe("sample.rabbitmq.demo.trans")]
        public void SubscriberTrans(ApiConfig apiConfig)
        {
            Console.WriteLine($"【SubscriberTrans】Subscriber invoked, Info: {apiConfig.Id}");



            using (var trans = _pgDbContext.Database.BeginTransaction())
            {
                var result = _pgDbContext.ApiConfig.Find(apiConfig.Id);
                throw new Exception("123");
            }
        }
    }
}

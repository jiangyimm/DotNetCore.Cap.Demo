using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotNetCore.Cap.Demo.Subscriber.Controllers
{
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        [NonAction]
        [CapSubscribe("sample.rabbitmq.demo.string")]
        public void SubscriberString(string text)
        {
            Console.WriteLine($"【SubscriberString】Subscriber invoked, Info: {text}");
        }

        [NonAction]
        [CapSubscribe("sample.rabbitmq.demo.dynamic")]
        public void SubscriberDynamic(dynamic person)
        {
            Console.WriteLine($"【SubscriberDynamic】Subscriber invoked, Info: {person.Name} {person.Age}");
        }

        [NonAction]
        [CapSubscribe("sample.rabbitmq.demo.object")]
        public void SubscriberObject(Person person)
        {
            Console.WriteLine($"【SubscriberObject】Subscriber invoked, Info: {person.Name} {person.Age}");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public short Age { get; set; }
    }
}

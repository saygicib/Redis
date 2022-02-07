using Microsoft.AspNetCore.Mvc;
using RedisWeb.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWeb.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string key = "names";
        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            List<string> nameList = new();
            if (db.KeyExists(key))
            {
                db.ListRange(key).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });

            }
            return View(nameList);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            if(name is not null)
            {
                db.ListRightPushAsync(key, name);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string name)
        {
            db.ListRemoveAsync(key, name).Wait();
            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RedisWeb.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWeb.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string key = "setnames";
        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            HashSet<string> nameList = new HashSet<string>();
            if (db.KeyExists(key))
            {
                var dbList = db.SetMembers(key).ToList();
                foreach (var item in dbList)
                {
                    nameList.Add(item.ToString());
                }
            }
            return View(nameList);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            if (name is not null)
            {
                db.KeyExpire(key, DateTime.Now.AddMinutes(5));
                db.SetAdd(key, name);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string name)
        {
            db.SetRemoveAsync(key, name).Wait();
            return RedirectToAction("Index");
        }
    }
}

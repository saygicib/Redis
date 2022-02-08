using Microsoft.AspNetCore.Mvc;
using RedisWeb.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWeb.Controllers
{
    public class HashTypeController : BaseController
    {
        public string hashKey { get; set; } = "sozluk";
        public HashTypeController(RedisService redisService) : base(redisService)
        {
        }

        public IActionResult Index()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            if(db.KeyExists(hashKey))
            {
                var aa = db.HashGetAll(hashKey).ToList();
                foreach (var item in aa)
                {
                    list.Add(item.Name, item.Value);
                }
            }
            return View(list);
        }
        [HttpPost]
        public IActionResult Add(string name, string value)
        {
            db.HashSet(hashKey, name, value);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string name)
        {
            db.HashDelete(hashKey, name);
            return RedirectToAction("Index");
        }
    }
}

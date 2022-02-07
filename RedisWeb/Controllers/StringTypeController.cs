using Microsoft.AspNetCore.Mvc;
using RedisWeb.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWeb.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db= _redisService.GetDb(0);
        }
        public IActionResult Index()
        { 
            db.StringSet("name", "Bugrahan SAYGICI");
            db.StringSet("uni", "Pamukkale Universitesi");
            db.StringSet("telno", "5419766556");
            db.StringIncrement("goruntulenmesayisi",1);
            return View();
        }
        public IActionResult Get()
        {
            var name = db.StringGet("name");
            var nameLength = db.StringLength("name");
            var getRange = db.StringGetRange("name",0,10);
            var uni = db.StringGet("uni");
            var no = db.StringGet("telno");
            var goruntulenmesayisi = db.StringGet("goruntulenmesayisi");

            if(name.HasValue)
            {
                ViewBag.name = name;
            }
            ViewBag.uni = uni.IsNullOrEmpty?"Bilinmiyor":uni.ToString(); 
            ViewBag.getRange = getRange.IsNullOrEmpty?"Bilinmiyor": getRange.ToString(); 
            ViewBag.no = no.IsNullOrEmpty ? "Bilinmiyor" : no.ToString();
            ViewBag.goruntulenmesayisi = goruntulenmesayisi.IsNullOrEmpty ? "Bilinmiyor" : goruntulenmesayisi.ToString();
            ViewBag.nameLength = nameLength==0 ? "Bilinmiyor" : nameLength.ToString();
            return View();
        }
    }
}

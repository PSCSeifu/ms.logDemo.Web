using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ms.logDemo.Web.Common;
using Microsoft.Extensions.Logging;

namespace ms.logDemo.Web.Controllers
{
    public class ContactController : Controller
    {
        private IHttpClientsFactory _httpClientsFactory;
        private readonly ILogger<ContactController> _logger;
        private ProjectAppSettings _projectAppSettings;
        public ContactController(ILogger<ContactController> logger,IHttpClientsFactory httpClientsFactory)
        {
            _logger = logger;
            _httpClientsFactory = httpClientsFactory;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async  Task<IActionResult> List()
        {
            var path = $"{_projectAppSettings.LocalAPIServer}/api/Contacts";
            var client = _httpClientsFactory.Client("LocalAPIServer");
            var response = await client.GetAsync(path);
            return Json(response);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

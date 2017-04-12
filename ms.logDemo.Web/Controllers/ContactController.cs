using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ms.logDemo.Web.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ms.logDemo.Web.Models.Contact;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace ms.logDemo.Web.Controllers
{
    public class ContactController : Controller
    {
        private IHttpClientsFactory _httpClientsFactory;
        private readonly ILogger<ContactController> _logger;
        private IOptions<ProjectAppSettings> _projectAppSettings;
        public ContactController(ILogger<ContactController> logger,IHttpClientsFactory httpClientsFactory, IOptions<ProjectAppSettings> projectAppSettings)
        {
            _logger = logger;
            _httpClientsFactory = httpClientsFactory;
            _projectAppSettings = projectAppSettings;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var contacts = await List();
                if (contacts != null)
                {
                    //Happy Path

                    _logger.LogInformation("Success - fetched Contact list @contacts", contacts);
                    return View(contacts);
                }
                else
                {
                    //Unhappy path 
                    _logger.LogWarning("failed to get Contacts @contacts", contacts);
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //Exception 
                _logger.LogError("Error fetching Contact List @ex", ex);
                return StatusCode(500);
            }
        }


        private async Task<ContactListVM> List()
        {
            try
            {
                var path = $"{_projectAppSettings.Value.AzureAPIServer}/api/Contact";
                var client = _httpClientsFactory.Client("AzureAPIServer");
                var response = await client.GetAsync(path);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var vm = JsonConvert.DeserializeObject<ContactListVM>(result.Result);
                    return vm;
                }
                else
                {
                    return new ContactListVM();
                }
            }
            catch (Exception)
            {
                throw;
            }
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

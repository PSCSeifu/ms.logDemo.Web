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

                    _logger.LogInformation("Success - Fetched Contact list {@Contacts}", contacts);
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

        public async Task<IActionResult> Single(int id)
        {
            try
            {
                var contact = await GetItem(id);
                if (contact != null)
                {
                    //Happy Path

                    _logger.LogInformation("Fetched Contact :  {@Contact}", contact);
                    return View("Single", contact);
                }
                else
                {
                    //Unhappy path 
                    _logger.LogWarning("failed to get Contact @contact", contact);
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //Exception 
                _logger.LogError("Error fetching Contact @ex", ex);
                return StatusCode(500);
            }
           
        }

        #region " Private "

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

       //TODO: Encrypted Parameter
        private async Task<ContactVM> GetItem(int id)
        {
            try
            {
                var path = $"{_projectAppSettings.Value.AzureAPIServer}/api/Contact/{id}";
                var client = _httpClientsFactory.Client("AzureAPIServer");
                var response = await client.GetAsync(path);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var vm = JsonConvert.DeserializeObject<ContactVM>(result.Result);
                    return vm;
                }
                else
                {
                    return new ContactVM();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}

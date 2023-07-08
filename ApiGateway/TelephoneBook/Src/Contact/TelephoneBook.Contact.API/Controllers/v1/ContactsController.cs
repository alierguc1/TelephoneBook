using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TelephoneBook.Contact.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ContactsController : ControllerBase
    {
       
        [HttpGet]
        public string Get() => ".Net Core Web API Version 2";
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;
using TelephoneBook.Contact.Application.Features.Contact.Command;
using TelephoneBook.Contact.Application.Features.Contact.Query;

namespace TelephoneBook.Contact.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ContactsController : BaseApiController
    {

        [HttpPost("CreateContact")]
        public async Task<IActionResult> CreateContact(CreateContactCommand createContactCommand)
        {
            try
            {
                return Ok(await Mediator.Send(createContactCommand));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("GetAllContactList")]
        public async Task<IActionResult> GetAllContactList()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllContactListQuery()));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}


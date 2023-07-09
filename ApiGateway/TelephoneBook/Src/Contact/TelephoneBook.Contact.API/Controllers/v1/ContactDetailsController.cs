using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TelephoneBook.Contact.Application.Features.Contact.Command;
using TelephoneBook.Contact.Application.Features.ContactDetails.Command;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Shared.Models;

namespace TelephoneBook.Contact.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ContactDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("CreateContactDetails")]
        [ProducesResponseType(typeof(ResponseDatas<ContactDetail>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDatas<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateContactDetails(CreateContactDetailsCommand createContactCommand)
        {
            try
            {
                var result = await _mediator.Send(createContactCommand);
                return ResponseDatas<ContactDetail>.Success(result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

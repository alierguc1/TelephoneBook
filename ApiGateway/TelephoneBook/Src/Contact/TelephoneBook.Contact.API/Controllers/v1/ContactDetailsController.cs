using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TelephoneBook.Contact.Application.Features.Contact.Command;
using TelephoneBook.Contact.Application.Features.Contact.Query;
using TelephoneBook.Contact.Application.Features.ContactDetails.Command;
using TelephoneBook.Contact.Application.Features.ContactDetails.Query;
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

        [HttpGet("GetAllContactDetailsList")]
        [ProducesResponseType(typeof(ResponseDatas<List<ContactDetail>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContactDetailsList()
        {
            try
            {
                var result = await _mediator.Send(new GetAllContactDetailsListQuery());
                return ResponseDatas<List<ContactDetail>>.Success(result.ToList(), (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("GetContactDetailsById/{contactId}")]
        [ProducesResponseType(typeof(ResponseDatas<List<ContactDetail>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetContactDetailsById(string contactId)
        {
            try
            {
                var result = await _mediator.Send(new GetContactDetailsByIdQuery { contactId = contactId });
                return ResponseDatas<ContactDetail>.Success(result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }

        

    }
}

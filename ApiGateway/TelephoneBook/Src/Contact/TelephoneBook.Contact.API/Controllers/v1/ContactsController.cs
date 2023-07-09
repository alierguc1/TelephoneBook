using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics.Contracts;
using System.Net;
using TelephoneBook.Contact.Application.Features.Contact.Command;
using TelephoneBook.Contact.Application.Features.Contact.Query;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Domain.ValidatableObjectModels;
using TelephoneBook.Contact.Shared.Models;

namespace TelephoneBook.Contact.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ContactsController : BaseApiController
    {

        [HttpPost("CreateContact")]
        [ProducesResponseType(typeof(ResponseDatas<Contacts>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDatas<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateContact(CreateContactCommand createContactCommand)
        {
            try
            {
                var result = await Mediator.Send(createContactCommand);
                return ResponseDatas<Contacts>.Success(result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteContact/{contactId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteContact(string contactId)
        {
            try
            {               
                var returnedValue = await Mediator.Send(new DeleteContactCommand { contactId = contactId });
                return ResponseDatas<Contacts>.Success((int)HttpStatusCode.OK,returnedValue);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası",(int)HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("GetContactById/{contactId}")]
        [ProducesResponseType(typeof(ResponseDatas<Contacts>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDatas<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetContactById(string contactId)
        {
            try
            {
                var returnedValue = await Mediator.Send(new GetContactByIdQuery { contactId = contactId });       
                return ResponseDatas<Contacts>.Success(returnedValue, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("GetAllContactList")]
        [ProducesResponseType(typeof(ResponseDatas<IList<Contacts>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContactList()
        {
            try
            {
                var result = await Mediator.Send(new GetAllContactListQuery());
                return ResponseDatas<IList<Contacts>>.Success(result.ToList(), (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }
    }
}


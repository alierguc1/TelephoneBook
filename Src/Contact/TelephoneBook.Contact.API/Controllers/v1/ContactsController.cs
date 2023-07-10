using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics.Contracts;
using System.Net;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Domain.ValidatableObjectModels;
using TelephoneBook.Contact.Infrastructure.Interfaces;
using TelephoneBook.Shared.Models;
using static TelephoneBook.Shared.Models.Response;

namespace TelephoneBook.Contact.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IContactsDetailsRepository _contactsDetailsRepository;
        private readonly IMapper _mapper;
        public ContactsController(IContactsRepository contactsRepository, IContactsDetailsRepository contactsDetailsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _contactsRepository = contactsRepository;
            _contactsDetailsRepository= contactsDetailsRepository;
        }

        [HttpPost("CreateContact")]
        [ProducesResponseType(typeof(ResponseDatas<Contacts>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDatas<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateContact([FromBody] ContactAddVO model)
        {
            try
            {
                var newContact = _mapper.Map<Contacts>(model);

                var _contact = await _contactsRepository.CreateContactAsync(newContact);

                if (_contact == null) return BadRequest();

                var result = new ResultId<string> { Id = _contact.Id };
                return ResponseDatas<ResultId<string>>.Success(result, (int)HttpStatusCode.Created);
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
                var result = await _contactsRepository.DeleteContactAsync(contactId);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası",(int)HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("GetContactById/{contactId}")]
        [ProducesResponseType(typeof(ResponseDatas<ContactsVO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDatas<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetContactById(string contactId)
        {
            try
            {
                var contact = await _contactsRepository.GetContactByIdAsync(contactId);
                if (contact == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<ContactsVO>(contact);

                var contactInfos = await _contactsDetailsRepository.GetContactDetailsByContactIdAsync(contact.Id);
                if (contactInfos != null)
                {
                    model.ContactDetailsVOs = _mapper.Map<IList<ContactDetailsVO>>(contactInfos);
                }

                return ResponseDatas<ContactsVO>.Success(model, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("GetAllContactList")]
        [ProducesResponseType(typeof(ResponseDatas<List<Contacts>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContactList()
        {
            try
            {
                var list = await _contactsRepository.GetAllContactsListAsync();

                IList<ContactsIndexVO> contacts = null;
                if (true)
                {
                    contacts = _mapper.Map<IList<ContactsIndexVO>>(list);
                }

                return ResponseDatas<IList<ContactsIndexVO>>.Success(contacts, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }
    }
}


using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ContactDetailsController : ControllerBase
    {
        private readonly IContactsDetailsRepository _contactsDetailsRepository;
        private readonly IMapper _mapper;
        public ContactDetailsController(IContactsDetailsRepository contactsDetailsRepository, IMapper mapper)
        {
            _contactsDetailsRepository = contactsDetailsRepository;
            _mapper = mapper;
        }


        [HttpPost("CreateContactDetails")]
        [ProducesResponseType(typeof(ResponseDatas<ContactDetail>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDatas<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateContactDetails([FromBody] ContactDetailsAddVO contactDetailsVO)
        {
            try
            {
                var newContactInfo = _mapper.Map<ContactDetail>(contactDetailsVO);

                var _contactInfo = await _contactsDetailsRepository.CreateContactDetailsAsync(newContactInfo);

                if (_contactInfo == null) return BadRequest();

                var result = new ResultId<string> { Id = _contactInfo.Id };
                return ResponseDatas<ResultId<string>>.Success(result, (int)HttpStatusCode.Created);
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
                var list = await _contactsDetailsRepository.GetAllContactDetailsListAsync();

                IList<ContactDetailsVO> infos = null;
                if (list != null && list.Any())
                {
                    infos = _mapper.Map<IList<ContactDetailsVO>>(list);
                }

                return ResponseDatas<IList<ContactDetailsVO>>.Success(infos, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("DeleteContactDetails/{contactId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteContactDetails(string contactId)
        {
            try
            {
                var result = await _contactsDetailsRepository.DeleteContactDetailsAsync(contactId);
                return result ? Ok() : BadRequest();
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
                var result = await _contactsDetailsRepository.GetContactDetailsByContactIdAsync(contactId);
                return ResponseDatas<ContactDetail>.Success(result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDatas<Contacts>.Fail("Iç Sunucu Hatası", (int)HttpStatusCode.BadRequest);
            }
        }

    }
}

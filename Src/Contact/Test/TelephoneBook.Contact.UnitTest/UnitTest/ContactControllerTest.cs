using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.API.Controllers.v1;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Domain.Enums;
using TelephoneBook.Contact.Domain.ValidatableObjectModels;
using TelephoneBook.Contact.Infrastructure.Concretes;
using TelephoneBook.Contact.Infrastructure.Interfaces;
using TelephoneBook.Contact.Infrastructure.Mapping;
using TelephoneBook.Shared.Models;
using Xunit;
using static TelephoneBook.Shared.Models.Response;

namespace TelephoneBook.Contact.UnitTest.UnitTest
{
    public class ContactControllerTest
    {
        private readonly Mock<IContactsRepository> _contactRepositoryMock;
        private readonly Mock<IContactsDetailsRepository> _contactDetailsRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ContactsController _contactController;
        private readonly ContactDetailsController _contactDetailsController;

        public ContactControllerTest()
        {
            _contactRepositoryMock = new Mock<IContactsRepository>();
            _contactDetailsRepositoryMock = new Mock<IContactsDetailsRepository>();

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMappings>()).CreateMapper();

            _contactController = new ContactsController(_contactRepositoryMock.Object, _contactDetailsRepositoryMock.Object, _mapper);
            _contactDetailsController = new ContactDetailsController(_contactDetailsRepositoryMock.Object, _mapper);
        }


        [Fact]
        public async Task GetAllContactDetailsList_TEST_200()
        {
            //Arrange
            _contactDetailsRepositoryMock.Setup(x => x.GetAllContactDetailsListAsync())
              .Returns(Task.FromResult(GetContactInfosFake("907f1f77bcf86cd799439046")));

            //Act
            var actionResult = await _contactDetailsController.GetAllContactDetailsList();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<ResponseDatas<IList<ContactDetailsVO>>>(objectResult.Value);
        }

        [Fact]
        public async Task GetAllContacts_TEST_200()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.GetAllContactsListAsync())
              .Returns(Task.FromResult(GetContactsFake()));

            //Act
            var actionResult = await _contactController.GetAllContactList();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<ResponseDatas<IList<ContactsIndexVO>>>(objectResult.Value);
        }

        [Fact]
        public async Task GetContactById_TEST_200()
        {
            //Arrange
            var fakeContactId = "887f1f77bcf86cd799439092";

            _contactRepositoryMock.Setup(x => x.GetContactByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetContactById(fakeContactId)));

            //Act
            var actionResult = await _contactController.GetContactById(fakeContactId);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (ResponseDatas<ContactsVO>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeContactId);
        }

        [Fact]
        public async Task GetContactById_TEST_404()
        {
            //Arrange
            var fakeContactId = "150f1f77bcf86cd799439092";

            _contactRepositoryMock.Setup(x => x.GetContactByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetContactById(fakeContactId)));

            //Act
            var actionResult = await _contactController.GetContactById(fakeContactId);

            //Assert
            var objectResult = (NotFoundResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateContact_TEST_201()
        {
            //Arrange
            var fakeContactId = "887f1f77bcf86cd799439092";
            var fakeContact = GetContactById(fakeContactId);
            var model = _mapper.Map<ContactAddVO>(fakeContact);

            _contactRepositoryMock.Setup(x => x.CreateContactAsync(It.IsAny<Contacts>()))
              .Returns(Task.FromResult(fakeContact));

            //Act
            var actionResult = await _contactController.CreateContact(model);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (ResponseDatas<ResultId<string>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.Created);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeContactId);
        }

        [Fact]
        public async Task CreateContactModelUnvalid_TEST()
        {
            //Arrange
            var model = new ContactAddVO
            {
                ContactName = "",
                ContactLastName = "lastname",
                ContactCompany = "company"
            };

            //Act
            var validationContext = new ValidationContext(model);

            var results = model.Validate(validationContext);

            //Assert
            Assert.True(results.Any(x => x.MemberNames.Contains(nameof(ContactAddVO.ContactName))));
        }

        [Fact]
        public async Task CreateContactDetails_TEST_201()
        {
            //Arrange
            var fakeContactInfoId = "507f1f77bcf86cd799439011";
            var fakeContactInfo = GetContactInfoById(fakeContactInfoId);
            var model = _mapper.Map<ContactDetailsAddVO>(fakeContactInfo);

            _contactDetailsRepositoryMock.Setup(x => x.CreateContactDetailsAsync(It.IsAny<ContactDetail>()))
              .Returns(Task.FromResult(fakeContactInfo));

            //Act
            var actionResult = await _contactDetailsController.CreateContactDetails(model);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (ResponseDatas<ResultId<string>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.Created);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeContactInfoId);
        }

        [Fact]
        public async Task CreateContactDetailsModelUnvalid_TEST()
        {
            //Arrange
            var model = new ContactDetailsVO
            {
                ContactId = "",
                ContactInfoType = (int)ContactDetailsType.ContactEmail,
                Value = ""
            };

            //Act
            var validationContext = new ValidationContext(model);

            var results = model.Validate(validationContext);

            //Assert

            Assert.True(results.Any(x => x.MemberNames.Contains(nameof(ContactDetailsVO.Value))));
            Assert.True(results.Any(x => x.MemberNames.Contains(nameof(ContactDetailsVO.ContactId))));
        }

        [Fact]
        public async Task DeleteContact_TEST_200()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.DeleteContactAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(true));

            //Act
            var actionResult = await _contactController.DeleteContact("887f1f77bcf86cd799439092");

            //Assert
            var objectResult = (OkResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteContact_TEST_400()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.DeleteContactAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(false));

            //Act
            var actionResult = await _contactController.DeleteContact("887f1f77bcf86cd799439092");

            //Assert
            var objectResult = (BadRequestResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteContactDetails_TEST_200()
        {
            //Arrange
            _contactDetailsRepositoryMock.Setup(x => x.DeleteContactDetailsAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(true));

            //Act
            var actionResult = await _contactDetailsController.DeleteContactDetails("236f1f77bcf86cd799439092");

            //Assert
            var objectResult = (OkResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteContactDetails_TEST_400()
        {
            //Arrange
            _contactDetailsRepositoryMock.Setup(x => x.DeleteContactDetailsAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(false));

            //Act
            var actionResult = await _contactDetailsController.DeleteContactDetails("236f1f77bcf86cd799439092");

            //Assert
            var objectResult = (BadRequestResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        private Contacts GetContactById(string fakeContactId)
        {
            return GetContactsFake().FirstOrDefault(x => x.Id == fakeContactId);
        }

        private ContactDetail GetContactInfoById(string fakeContactInfoId)
        {
            return GetContactInfosFake("887f1f77bcf86cd799439092")
              .FirstOrDefault(x => x.Id == fakeContactInfoId);
        }

        private List<Contacts> GetContactsFake()
        {
         return new List<Contacts>
         {
           new Contacts
           {
               ContactCompany = "Nintendo",
               ContactLastName = "hiro",
               ContactName = "x",
               Id="887f1f77bcf86cd799439092"
           },
            new Contacts
           {
               ContactCompany = "Sega",
               ContactLastName = "Akira",
               ContactName = "y",
               Id="887f1f77bcf86cd799439092"
           },
        };
        }

        private List<ContactDetail> GetContactInfosFake(string fakeContactId)
        {
            return new List<ContactDetail>
      {
       new ContactDetail
       {
           ContactDetailsType = Domain.Enums.ContactDetailsType.ContactPhone,
           ContactId = fakeContactId,
           Value = "0555555555",
           Id = "507f1f77bcf86cd799439011"
       },
       new ContactDetail
       {
           ContactDetailsType = Domain.Enums.ContactDetailsType.ContactPhone,
           ContactId = fakeContactId,
           Value = "0555555555",
           Id = "507f1f77bcf86cd799439011"
       }
      };
        }
    }
}

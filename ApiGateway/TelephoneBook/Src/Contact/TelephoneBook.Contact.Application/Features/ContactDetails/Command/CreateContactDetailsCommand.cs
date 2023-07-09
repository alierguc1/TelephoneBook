using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Application.Features.Contact.Command;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Domain.Enums;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Application.Features.ContactDetails.Command
{
    public class CreateContactDetailsCommand : IRequest<ContactDetail>
    {
        public string ContactId { get; set; }
        public ContactDetailsType ContactDetailsType { get; set; }
        public string Value { get; set; }

        public class CreateContactDetailsCommandHandler : IRequestHandler<CreateContactDetailsCommand, ContactDetail>
        {
            private readonly IContactsDetailsRepository _contactsDetailsRepository;
            private readonly IMapper _mapper;
            public CreateContactDetailsCommandHandler(IContactsDetailsRepository contactsDetailsRepository, IMapper mapper)
            {
                _contactsDetailsRepository = contactsDetailsRepository;
                _mapper = mapper;
            }
            public async Task<ContactDetail> Handle(CreateContactDetailsCommand command, CancellationToken cancellationToken)
            {
                ContactDetail contacts = new ContactDetail
                {
                    ContactId = command.ContactId,
                    ContactDetailsType = command.ContactDetailsType,
                    Value = command.Value
                };
                var mappingContactDetails = _mapper.Map<ContactDetail>(contacts);
                var creatingContactDetails = _contactsDetailsRepository.CreateContactDetailsAsync(mappingContactDetails);
                return mappingContactDetails;
            }
        }
    }
}

using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Concretes;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Application.Features.Contact.Command
{
    public class CreateContactCommand : IRequest<Contacts>
    {
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }

        public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Contacts>
        {
            private readonly IContactsRepository _contactsRepository;
            private readonly IMapper _mapper;
            public CreateContactCommandHandler(IContactsRepository contactsRepository, IMapper mapper)
            {
                _contactsRepository = contactsRepository;
                _mapper = mapper;
            }
            public async Task<Contacts> Handle(CreateContactCommand command, CancellationToken cancellationToken)
            {
                Contacts contacts = new Contacts
                {
                    ContactName = command.ContactName,
                    ContactLastName = command.ContactLastName,
                    ContactCompany = command.ContactCompany
                };
                var mappingContact = _mapper.Map<Contacts>(contacts);
                var creatingContacts = _contactsRepository.CreateContactAsync(mappingContact);
                return mappingContact;
            }
        }
    }
}

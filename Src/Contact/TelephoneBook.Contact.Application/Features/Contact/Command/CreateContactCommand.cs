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
            public CreateContactCommandHandler(IContactsRepository contactsRepository)
            {
                _contactsRepository = contactsRepository;
            }
            public async Task<Contacts> Handle(CreateContactCommand command, CancellationToken cancellationToken)
            {
                Contacts contacts = new Contacts
                {
                    ContactName = command.ContactName,
                    ContactLastName = command.ContactLastName,
                    ContactCompany = command.ContactCompany
                };
            
                var creatingContacts = _contactsRepository.CreateContactAsync(contacts);
                return contacts;
            }
        }
    }
}

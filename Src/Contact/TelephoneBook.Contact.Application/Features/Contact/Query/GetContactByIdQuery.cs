using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Application.Features.Contact.Query
{
    public class GetContactByIdQuery : IRequest<Contacts>
    {
        public string contactId { get; set; }

        public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contacts>
        {
            private readonly IContactsRepository _contactsRepository;
            public GetContactByIdQueryHandler(IContactsRepository contactsRepository)
            {
                _contactsRepository = contactsRepository;
            }
            public async Task<Contacts> Handle(GetContactByIdQuery query, CancellationToken cancellationToken)
            {
                var getByContactIdResult = await _contactsRepository.GetContactByIdAsync(query.contactId);
                return getByContactIdResult;
            }
        }
    }
}

using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Application.Features.Contact.Command;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Application.Features.Contact.Query
{
    public class GetAllContactListQuery : IRequest<IEnumerable<Contacts>>
    {
        public class GetAllContactListQueryHandler : IRequestHandler<GetAllContactListQuery, IEnumerable<Contacts>>
        {
            private readonly IContactsRepository _contactsRepository;
         
            public GetAllContactListQueryHandler(IContactsRepository contactsRepository)
            {
                _contactsRepository = contactsRepository;
            }
            public async Task<IEnumerable<Contacts>> Handle(GetAllContactListQuery command, CancellationToken cancellationToken)
            {                     
                var getAllContactList = await _contactsRepository.GetAllContactsListAsync();
                return getAllContactList;
            }
        }
    }
}

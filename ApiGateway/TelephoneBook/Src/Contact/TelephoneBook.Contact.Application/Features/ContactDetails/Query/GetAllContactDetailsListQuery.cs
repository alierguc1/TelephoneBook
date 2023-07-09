using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Application.Features.Contact.Query;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Application.Features.ContactDetails.Query
{
    public class GetAllContactDetailsListQuery : IRequest<IEnumerable<ContactDetail>>
    {
        public class GetAllContactDetailsListQueryHandler : IRequestHandler<GetAllContactDetailsListQuery, IEnumerable<ContactDetail>>
        {
            private readonly IContactsDetailsRepository _contactsDetailsRepository;

            public GetAllContactDetailsListQueryHandler(IContactsDetailsRepository contactsDetailsRepository)
            {
                _contactsDetailsRepository = contactsDetailsRepository;
            }
            public async Task<IEnumerable<ContactDetail>> Handle(GetAllContactDetailsListQuery query, CancellationToken cancellationToken)
            {
                var getAllContactList = await _contactsDetailsRepository.GetAllContactDetailsListAsync();
                return getAllContactList;
            }
        }
    }
}

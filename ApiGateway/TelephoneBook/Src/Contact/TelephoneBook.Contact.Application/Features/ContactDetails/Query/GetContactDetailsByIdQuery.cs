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
    public class GetContactDetailsByIdQuery : IRequest<ContactDetail>
    {
        public string contactId { get; set; }

        public class GetContactDetailsByIdQueryHandler : IRequestHandler<GetContactDetailsByIdQuery, ContactDetail>
        {
            private readonly IContactsDetailsRepository _contactsDetailsRepository;
            public GetContactDetailsByIdQueryHandler(IContactsDetailsRepository contactsDetailsRepository)
            {
                _contactsDetailsRepository = contactsDetailsRepository;
            }
            public async Task<ContactDetail> Handle(GetContactDetailsByIdQuery query, CancellationToken cancellationToken)
            {
                var getByContactIdResult = await _contactsDetailsRepository.GetContactDetailsByContactIdAsync(query.contactId);
                return getByContactIdResult;
            }
        }
    }
}

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

namespace TelephoneBook.Contact.Application.Features.ContactDetails.Command
{
    public class DeleteContactDetailsCommand : IRequest<bool>
    {
        public string contactId { get; set; }

        public class DeleteContactDetailsCommandHandler : IRequestHandler<DeleteContactDetailsCommand, bool>
        {
            private readonly IContactsDetailsRepository _contactsDetailsRepository;
            public DeleteContactDetailsCommandHandler(IContactsDetailsRepository contactsDetailsRepository, IMapper mapper)
            {
                _contactsDetailsRepository = contactsDetailsRepository;
            }
            public async Task<bool> Handle(DeleteContactDetailsCommand command, CancellationToken cancellationToken)
            {
                var deletingContactResult = _contactsDetailsRepository.DeleteContactDetailsAsync(command.contactId);
                return deletingContactResult.Result;
            }
        }
    }
}

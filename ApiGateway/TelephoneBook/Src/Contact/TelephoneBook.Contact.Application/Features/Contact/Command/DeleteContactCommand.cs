using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Application.Features.Contact.Command
{
    public class DeleteContactCommand : IRequest<bool>
    {
        public string contactId { get; set; }

        public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, bool>
        {
            private readonly IContactsRepository _contactsRepository;
            public DeleteContactCommandHandler(IContactsRepository contactsRepository, IMapper mapper)
            {
                _contactsRepository = contactsRepository;
            }
            public async Task<bool> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
            {
                var deletingContactResult = _contactsRepository.DeleteContactAsync(command.contactId);
                return deletingContactResult.Result;
            }
        }
    }
}

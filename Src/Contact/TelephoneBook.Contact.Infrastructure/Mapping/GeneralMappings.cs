using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Domain.ValidatableObjectModels;

namespace TelephoneBook.Contact.Infrastructure.Mapping
{
    public class GeneralMappings : Profile
    {
        public GeneralMappings()
        {
            CreateMap<Contacts, ContactsVO>().ReverseMap();
            CreateMap<Contacts, ContactsIndexVO>().ReverseMap();
            CreateMap<Contacts, ContactsEditVO>().ReverseMap();
            CreateMap<Contacts, ContactAddVO>().ReverseMap();
            CreateMap<ContactDetail, ContactDetailsVO>().ReverseMap();
            CreateMap<ContactDetail, ContactDetailsAddVO>().ReverseMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Domain.ValidatableObjectModels
{
    public class ContactsVO : IValidatableObject
    {
        public string Id { get; set; }
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }
        public IList<ContactDetailsVO> ContactDetailsVOs { get; set; } = new List<ContactDetailsVO>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(ContactName))
            {
                yield return new ValidationResult("boş bırakılamaz.", new[] { "ContactName" });
            }
            else if (string.IsNullOrEmpty(ContactLastName))
            {
                yield return new ValidationResult("boş bırakılamaz.", new[] { "ContactLastName" });
            }
            else if (string.IsNullOrEmpty(ContactCompany))
            {
                yield return new ValidationResult("boş bırakılamaz", new[] { "ContactCompany" });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Domain.ValidatableObjectModels
{
    public class ContactDetailsAddVO
    {
        public string ContactId { get; set; }
        public int ContactInfoType { get; set; }
        public string Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Value))
            {
                yield return new ValidationResult("Value değeri boş bırakılamaz.", new[] { nameof(Value) });
            }

            if (string.IsNullOrEmpty(ContactId))
            {
                yield return new ValidationResult("ContactId değeri boş bırakılamaz.", new[] { nameof(ContactId) });
            }
        }
    }
}

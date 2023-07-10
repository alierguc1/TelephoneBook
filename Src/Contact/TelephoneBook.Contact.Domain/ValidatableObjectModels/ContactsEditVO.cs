using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Domain.ValidatableObjectModels
{
    public class ContactsEditVO : IValidatableObject
    {
        public string Id { get; set; }
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }

        /// <summary>
        /// Kişi düzenleme Objesinin validasyon işlemi yapılmaktadır.  
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(ContactName))
            {
                yield return new ValidationResult("alanı boş Bırakılamaz", new[] { nameof(ContactName) });
            }
            else if (string.IsNullOrEmpty(Id))
            {
                yield return new ValidationResult("alanı boş Bırakılamaz", new[] { nameof(Id) });
            }
            else if (string.IsNullOrEmpty(ContactLastName))
            {
                yield return new ValidationResult("alanı boş Bırakılamaz", new[] { nameof(ContactLastName) });
            }
            else if (string.IsNullOrEmpty(ContactCompany))
            {
                yield return new ValidationResult("alanı boş Bırakılamaz", new[] { nameof(ContactCompany) });
            }
        }
    }
}

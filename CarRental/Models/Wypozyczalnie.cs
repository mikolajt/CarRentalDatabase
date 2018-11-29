//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Wypozyczalnie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wypozyczalnie()
        {
            this.Samochody = new HashSet<Samochody>();
        }

        [Required(ErrorMessage = "Id nie mo�e by� puste")]
        public int IDWypozyczalni { get; set; }
        [Required(ErrorMessage = "Adres nie mo�e by� pusty")]
        [MinLength(3, ErrorMessage = "Minimalna d�ugo�� adresu wynosi 3 znaki")]
        [MaxLength(250, ErrorMessage = "Maksymalna d�ugo�� adresu wynosi 255 znak�w")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Kod pocztowy nie mo�e by� pusty")]
        [DataType(DataType.PostalCode, ErrorMessage = "Podaj prawid�owy kod pocztowy")]
        public string KodPocztowy { get; set; }
        [Required(ErrorMessage = "Miasto nie mo�e by� puste")]
        [MinLength(3, ErrorMessage = "Minimalna d�ugo�� miasta wynosi 3 znaki")]
        [MaxLength(250, ErrorMessage = "Maksymalna d�ugo�� miasta wynosi 50 znak�w")]
        public string Miasto { get; set; }
        [Required(ErrorMessage = "Pa�stwo nie mo�e by� puste")]
        [MinLength(3, ErrorMessage = "Minimalna d�ugo�� pa�stwa wynosi 3 znaki")]
        [MaxLength(250, ErrorMessage = "Maksymalna d�ugo�� pa�stwa wynosi 50 znak�w")]
        public string Panstwo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Samochody> Samochody { get; set; }
    }
}

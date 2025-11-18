using System.ComponentModel.DataAnnotations;

namespace Domain.Shared.ValueObjects
{
    public class Location : ValueObject
    {
        public Location()
        {
        }

        public Location(string? street, string? number, string? neighborhood, string city, string state, string country,
            string? zipCode, string? complement)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Complement = complement;
        }

        public Location(string city, string state, string country)
        {
            City = city;
            State = state;
            Country = country;
        }

        [StringLength(255, ErrorMessage = "A rua deve ter no máximo 255 caracteres")]
        public string? Street { get; set; }

        [StringLength(20, ErrorMessage = "O número deve ter no máximo 20 caracteres")]
        public string? Number { get; set; }

        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres")]
        public string? Neighborhood { get; set; }

        [StringLength(20, ErrorMessage = "O CEP deve ter no máximo 20 caracteres")]
        public string? ZipCode { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres")]
        public string City { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório")]
        [StringLength(100, ErrorMessage = "O estado deve ter no máximo 100 caracteres")]
        public string State { get; set; }

        [Required(ErrorMessage = "O país é obrigatório")]
        [StringLength(100, ErrorMessage = "O país deve ter no máximo 100 caracteres")]
        public string Country { get; set; }

        [StringLength(255, ErrorMessage = "O complemento deve ter no máximo 255 caracteres")]
        public string? Complement { get; set; }

        public string GetFullAddress()
        {
            return $"{Street} | {Number} | {Neighborhood} | {City} | {State} | {Country} | {ZipCode}" +
                   (string.IsNullOrEmpty(Complement) ? "" : $" | {Complement}");
        }

        public string GetCityStateCountry()
        {
            return $"{City} | {State} | {Country}";
        }

        public string GetFullAddress(Location location)
        {
            return
                $"{location.Street} | {location.Number} | {location.Neighborhood} | {location.City} | {location.State} | {location.Country} | {location.ZipCode}" +
                (string.IsNullOrEmpty(location.Complement) ? "" : $" | {location.Complement}");
        }

        public string GetCityStateCountry(Location location)
        {
            return $"{location.City} | {location.State} | {location.Country}";
        }

        public Location RebuildFullAddress(string fullAddress)
        {
            var address = fullAddress.Split('|');

            Location location = new(address[0].Trim(), address[1].Trim(), address[2].Trim(), address[3].Trim(),
                address[4].Trim(), address[5].Trim(), address[6].Trim(), address[7].Trim());

            return location;
        }

        public Location RebuildCityStateCountry(string cityStateCountry)
        {
            var local = cityStateCountry.Split('|');

            Location location = new(local[0].Trim(), local[1].Trim(), local[2].Trim());

            return location;
        }
    }
}
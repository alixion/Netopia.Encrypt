namespace Netopia.Encrypt.Models;

[Serializable]
public class Address
{
    [XmlAttribute("type")] public string Type { get; set; }
    [XmlAttribute("sameasbilling")] public string SameAsBilling { get; set; }
    [XmlElement("first_name")] public string FirstName { get; set; }
    [XmlElement("last_name")] public string LastName { get; set; }
    [XmlElement("fiscal_number")] public string FiscalNumber { get; set; }
    [XmlElement("identity_number")] public string IdentityNumber { get; set; }
    [XmlElement("country")] public string Country { get; set; }
    [XmlElement("county")] public string County { get; set; }
    [XmlElement("city")] public string City { get; set; }
    [XmlElement("zip_code")] public string ZipCode { get; set; }
    [XmlElement("address")] public string AddressLine { get; set; }
    [XmlElement("email")] public string Email { get; set; }
    [XmlElement("mobile_phone")] public string MobilPhone { get; set; }
    [XmlElement("bank")] public string Bank { get; set; }
    [XmlElement("iban")] public string Iban { get; set; }
}
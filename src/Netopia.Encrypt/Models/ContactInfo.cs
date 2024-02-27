namespace Netopia.Encrypt.Models;

[Serializable]
public class ContactInfo
{
    [XmlElement("billing")] public Address Billing { get; set; }
    [XmlElement("shipping")] public Address Shipping { get; set; }
}
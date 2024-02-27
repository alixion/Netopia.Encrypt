namespace Netopia.Encrypt.Models;

[Serializable]
public class PaymentParam
{
    [XmlElement("name")] public string Name { get; set; }
    [XmlElement("value")] public string Value { get; set; }
}
namespace Netopia.Encrypt.Models;

[Serializable]
public class InvoiceItem
{
    [XmlElement("code")] public string Code { get; set; }
    [XmlElement("name")] public string Name { get; set; }
    [XmlElement("measurment")] public string Measurement { get; set; }
    [XmlElement("quantity")] public decimal Quantity { get; set; }
    [XmlElement("price")] public decimal Price { get; set; }
    [XmlElement("vat")] public decimal Vat { get; set; }
}
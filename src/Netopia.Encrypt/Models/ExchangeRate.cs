namespace Netopia.Encrypt.Models;

[Serializable]
public class ExchangeRate
{
    [XmlText] public decimal Rate { get; set; }
    [XmlAttribute("curency")] public string Currency { get; set; }
}
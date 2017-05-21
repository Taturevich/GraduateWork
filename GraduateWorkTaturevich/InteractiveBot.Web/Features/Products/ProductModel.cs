using System.ComponentModel;

namespace AimlBotWeb.Features.Products
{
    public class ProductModel
    {
        public int Id { get; set; }

        [DisplayName("Марка")]
        public string Mark { get; set; }

        [DisplayName("Сортамент")]
        public string Sortament { get; set; }

        [DisplayName("Спецификация")]
        public string Specification { get; set; }
    }
}
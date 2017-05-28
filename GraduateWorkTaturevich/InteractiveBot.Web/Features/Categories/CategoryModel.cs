using System.ComponentModel;

namespace AimlBotWeb.Features.Categories
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фото")]
        public string ImageUrl { get; set; }
    }
}
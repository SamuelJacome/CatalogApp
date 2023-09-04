using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        [DisplayName("Imagem Do Produto")]
        public IFormFile? ImageUpload { get; set; }
        public string? Image { get; set; }
        public string? Value { get; set; }
    }
}
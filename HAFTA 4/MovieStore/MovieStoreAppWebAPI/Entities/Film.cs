using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreAppWebAPI.Entities
{
    public class Film : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        public string? About { get; set; }

        public Genre Genre { get; set; } = new Genre();
        public Director Director { get; set; } = new Director();

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}

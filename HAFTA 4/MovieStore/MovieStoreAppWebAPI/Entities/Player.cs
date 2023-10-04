using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreAppWebAPI.Entities
{
    public class Player : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public ICollection<Film> Films { get; set; } = new List<Film>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreAppWebAPI.Entities
{
    public class Genre : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Film> Films { get; set; } = new List<Film>();
        public ICollection<User> UsersSelectAsFavourite { get; set; } = new List<User>();
    }
}

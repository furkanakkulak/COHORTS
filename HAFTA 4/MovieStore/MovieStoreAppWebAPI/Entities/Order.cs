using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieStoreAppWebAPI.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Film Film { get; set; } = new Film();
        public DateTime OrderedDate { get; }
        public bool IsDeleted { get; set; }
        public User User { get; set; } = new User();

        public Order()
        {
            OrderedDate = DateTime.Now;
        }
    }
}

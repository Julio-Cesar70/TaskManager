using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string ?Name { get; set; }

        [StringLength(200, MinimumLength = 3)]
        [Required]
        [Display(Name = "Descrição")]
        public string? Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ITI_Tanta_Final_Project.Models
{
    public class Grade
    {
        public int Id { get; set; }
        [Required]
        public int SessionId { get; set; }
        public Session? Session { get; set; } 

        [Required]
        public int TraineeId { get; set; }

        public User? Trainee { get; set; } 

        
        [Required(ErrorMessage = "Value is required")]
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100")]
        public int value { get; set; }


    }
}

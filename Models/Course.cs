using System.ComponentModel.DataAnnotations;

namespace ITI_Tanta_Final_Project.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Category must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Category must not exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        public int InstructorId { get; set; }
        public User? Instructor { get; set; }

        public List<Session> Sessions { get; set; } = new List<Session>();

        
        public List<User> Trainees { get; set; } = new List<User>();

       
      


    }
}

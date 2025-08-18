using System.ComponentModel.DataAnnotations;

namespace ITI_Tanta_Final_Project.Models
{
    public class User
    {
        public enum Role
        {
            Admin,
            Instructor,
            Trainee
        }

        public int Id { get; set; }        //1
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Name must not exceed 50 characters")]
        public string Name { get; set; }   //2

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }   //3

        [Required(ErrorMessage ="role is required")]
        public Role UserRole { get; set; }   //4

       

        public List<Course> studiedCourses{ get; set; }=new List<Course>();


        public List<Course> TeachingCourses { get; set; } = new List<Course>();

        public List<Grade> Grades { get; set; } = new List<Grade>();



    }
}

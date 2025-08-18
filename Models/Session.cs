using System.ComponentModel.DataAnnotations;

namespace ITI_Tanta_Final_Project.Models
{
    public class Session
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage ="courseid is required")]
        public int CourseId { get; set; }

        public Course Course { get; set; } = new Course();

        public List<Grade> Grades { get; set; } = new List<Grade>();


    }
}

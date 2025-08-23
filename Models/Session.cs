using System.ComponentModel.DataAnnotations;

namespace ITI_Tanta_Final_Project.Models
{
    public class Session
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "StartDate is required")]
        public TimeSpan StartingTime { get; set; }



        [Required(ErrorMessage = "EndDate is required")]
        public TimeSpan EndingTime { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title must not exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage ="courseid is required")]
        public int CourseId { get; set; }

        public Course Course { get; set; } = new Course();

        public List<Grade> Grades { get; set; } = new List<Grade>();

        public bool IsActive
        {
            get
            {
                return TimeSpan.Compare(DateTime.Now.TimeOfDay, StartingTime) >= 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay, EndingTime) <= 0;
            }
        }


    }
}

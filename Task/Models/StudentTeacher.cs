namespace Task.Models
{
    public class StudentTeacher
    {

        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}

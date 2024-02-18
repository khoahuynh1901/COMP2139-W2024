namespace Week04.Models.Entities
{
    public class Courses
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Semester { get; set; }
        public bool Subscribe { get; set; }

    }
}

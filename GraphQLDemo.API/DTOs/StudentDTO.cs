using GraphQLDemo.API.Models.Type;

namespace GraphQLDemo.API.DTOs
{
    public class StudentDTO : PersonType
    {
        //public Guid Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public double GPA { get; set; }
        public IEnumerable<CourseDTO> Courses { get; set; }
    }
}

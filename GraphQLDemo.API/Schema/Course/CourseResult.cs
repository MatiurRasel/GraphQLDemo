using GraphQLDemo.API.Models.Enum;

namespace GraphQLDemo.API.Schema.Course
{
    public class CourseResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }    
        public Guid InstructorId { get; set; }


    }
}

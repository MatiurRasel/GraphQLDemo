using GraphQLDemo.API.Schema.In_General.Enum;

namespace GraphQLDemo.API.Schema.Course
{
    public class CourseInputType
    {
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}

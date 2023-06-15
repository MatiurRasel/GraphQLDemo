using GraphQLDemo.API.Models.Type;

namespace GraphQLDemo.API.Schema.Instructor
{
    public class InstructorType : PersonType
    {
        public double Salary { get; set; }
    }
}

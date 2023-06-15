using GraphQLDemo.API.Models.Type;

namespace GraphQLDemo.API.Schema.Student
{
    public class StudentType : PersonType
    {
        [GraphQLName("gpa")]
        public double GPA { get; set; }
    }
}

using GraphQLDemo.API.Models.Type;
using GraphQLDemo.API.Schema.Course;

namespace GraphQLDemo.API.Schema.Instructor
{
    public class InstructorType : PersonType,ISearchResultType
    {
        public double Salary { get; set; }
    }
}

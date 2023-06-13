namespace GraphQLDemo.API.Schema
{
    public class StudentType : PersonType
    {
        [GraphQLName("gpa")]
        public double GPA { get; set; }
    }
}

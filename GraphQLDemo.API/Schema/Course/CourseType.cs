using GraphQLDemo.API.Models.Enum;
using GraphQLDemo.API.Schema.Instructor;
using GraphQLDemo.API.Schema.Student;

namespace GraphQLDemo.API.Schema.Course
{

    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        [GraphQLNonNullType]
        public InstructorType Instructor { get; set; }
        public IEnumerable<StudentType> Students { get; set; }

        //public string Description()
        //{
        //    return $"{Name} is a course of {Subject}.Instructor Of this Course " +
        //        $"is {Instructor.FirstName} {Instructor.LastName}. Top Student Of This " +
        //        $"Course {Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.FirstName} " +
        //        $"{Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.LastName}.";
        //}

        public string Description()
        {
            return $"{Name} is a course of {Subject}.Instructor Of this Course " +
                $"is {Instructor.FirstName} {Instructor.LastName}. Top Student Of This " +
                $"Course {Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.FirstName} " +
                $"{Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.LastName}.";
        }
    }
}

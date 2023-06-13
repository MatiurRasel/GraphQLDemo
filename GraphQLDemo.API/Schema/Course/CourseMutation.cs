using GraphQLDemo.API.Schema.In_General.Enum;

namespace GraphQLDemo.API.Schema.Course
{
    public class CourseMutation
    {
        private readonly List<CourseResult> _courses;
        public CourseMutation()
        {
            _courses = new List<CourseResult>();
        }
        public CourseResult CreateCourse(CourseInputType courseInput)
        {
            CourseResult courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId= courseInput.InstructorId
            };
            _courses.Add(courseType);
            return courseType;
        }

        public CourseResult UpdateCourse(Guid id, CourseInputType courseInput)
        {
            CourseResult course = _courses.FirstOrDefault(c=> c.Id == id);
            if (course == null)
            {
                throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
                //throw new Exception();
            }

            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courses.RemoveAll(c => c.Id == id) >= 1;
        }
    }
}

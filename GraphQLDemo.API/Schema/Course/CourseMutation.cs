using GraphQLDemo.API.Schema.In_General.Enum;
using GraphQLDemo.API.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Course
{
    public class CourseMutation
    {
        private readonly List<CourseResult> _courses;
        public CourseMutation()
        {
            _courses = new List<CourseResult>();
        }
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId= courseInput.InstructorId
            };
            _courses.Add(courseType);
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseType);
            
            return courseType;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
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

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            //var message = new Message<string, CourseResult>(updateCourseTopic, course);

            await topicEventSender.SendAsync(updateCourseTopic,course);

            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courses.RemoveAll(c => c.Id == id) >= 1;
        }
    }
}

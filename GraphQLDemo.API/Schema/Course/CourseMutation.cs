using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Courses;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Course
{
    public class CourseMutation
    {
        private readonly CoursesRepository _coursesRepository;
        public CourseMutation(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name ?? "--",
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,

            };
            courseDTO = await _coursesRepository.Create(courseDTO);

            CourseResult courseType = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId= courseDTO.InstructorId
            };
            
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseType);
            
            return courseType;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInput.Name ?? "--",
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,

            };

            courseDTO = await _coursesRepository.Update(courseDTO);

            CourseResult courseType = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

            string updateCourseTopic = $"{courseType.Id}_{nameof(Subscription.CourseUpdated)}";
            //var message = new Message<string, CourseResult>(updateCourseTopic, course);

            await topicEventSender.SendAsync(updateCourseTopic, courseType);

            return courseType;
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            try
            {
                return await _coursesRepository.Delete(id);
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}

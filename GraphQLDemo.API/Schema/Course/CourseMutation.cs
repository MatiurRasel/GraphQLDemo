using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Courses;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using System.Security.Claims;

namespace GraphQLDemo.API.Schema.Course
{
    public class CourseMutation
    {
        private readonly CoursesRepository _coursesRepository;
        public CourseMutation(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        [Authorize]
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, 
            [Service] ITopicEventSender topicEventSender,
            ClaimsPrincipal claimsPrincipal)
        {
            string userId = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID);
            string email = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL);
            string userName = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.USERNAME);
            string verified = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL_VERIFIED);


            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name ?? "--",
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
                CreatorId = userId

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
        [Authorize]
        public async Task<CourseResult> UpdateCourse(Guid id, 
            CourseInputType courseInput, 
            [Service] ITopicEventSender topicEventSender,
            ClaimsPrincipal claimsPrincipal)
        {
            string userId = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID);

            CourseDTO CourseDTO = await _coursesRepository.GetById(id);
            if (CourseDTO == null)
            {
                throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
            }
            if (CourseDTO.CreatorId != userId)
            {
                throw new GraphQLException(new Error("You do not have permission to update this course.", "INVALID_PERMISSION"));
            }

            CourseDTO.Name = courseInput.Name ?? "--";
            CourseDTO.Subject = courseInput.Subject;
            CourseDTO.InstructorId = courseInput.InstructorId;



            CourseDTO = await _coursesRepository.Update(CourseDTO);

            CourseResult courseType = new CourseResult()
            {
                Id = CourseDTO.Id,
                Name = CourseDTO.Name,
                Subject = CourseDTO.Subject,
                InstructorId = CourseDTO.InstructorId
            };

            string updateCourseTopic = $"{courseType.Id}_{nameof(Subscription.CourseUpdated)}";
            //var message = new Message<string, CourseResult>(updateCourseTopic, course);

            await topicEventSender.SendAsync(updateCourseTopic, courseType);

            return courseType;
        }
        [Authorize (Policy ="IsAdmin")]
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

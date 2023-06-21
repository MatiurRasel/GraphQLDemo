using FluentValidation;
using GraphQLDemo.API.Schema.Course;

namespace GraphQLDemo.API.Validators
{
    public class CourseTypeInputValidator : AbstractValidator<CourseInputType>
    {
        public CourseTypeInputValidator()
        {
            RuleFor(c => c.Name)
                .MinimumLength(3)
                .MinimumLength(50)
                .WithMessage("Course Name must between 3 and 50 characters.")
                .WithErrorCode("COURSE_NAME_LENGTH");
        }
    }
}

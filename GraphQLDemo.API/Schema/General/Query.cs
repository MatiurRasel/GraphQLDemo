using GraphQLDemo.API.Schema.Course;
using GraphQLDemo.API.Schema.Instructor;
using GraphQLDemo.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraphQLDemo.API.Schema.General
{
    public class Query
    {
        [UseDbContext(typeof(SchoolDbContext))]
        public async Task<IEnumerable<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
        {
            IEnumerable<CourseType> courses = await context.Courses
                .Where(c => c.Name.ToLower().Contains(term.ToLower()))
                .Select(x => new CourseType()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Subject = x.Subject,
                    InstructorId = x.InstructorId,
                    CreatorId = x.CreatorId,
                }).ToListAsync();

            IEnumerable<InstructorType> instructors = await context.Instructors
                .Where(c => c.FirstName.ToLower().Contains(term.ToLower()) || c.LastName.ToLower().Contains(term.ToLower()))
                .Select(c => new InstructorType()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Salary = c.Salary,
                }).ToListAsync();

            return new List<ISearchResultType>()
                .Concat(courses)
                .Concat(instructors);
        }

        [GraphQLDeprecated("This Query is deprecated.")]
        public string Instructions => "Test Text For GraphQL";
    }
}

using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Instructors
{
    public class InstructorsRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _ContextFactory;

        public InstructorsRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _ContextFactory = contextFactory;
        }

        public async Task<InstructorDTO> GetById(Guid instructorId)
        {
            using (SchoolDbContext context = _ContextFactory.CreateDbContext())
            {


                return await context.Instructors
                    //.Include(c => c.Instructor)
                    //.Include(c => c.Students)
                    .FirstOrDefaultAsync(c => c.Id == instructorId);
            }
        }
        public async Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> instructorIds)
        {
            using (SchoolDbContext context = _ContextFactory.CreateDbContext())
            {


                return await context.Instructors
                    //.Include(c => c.Instructor)
                    //.Include(c => c.Students)
                    .Where(c => instructorIds.Contains(c.Id)).ToListAsync();
            }
        }
    }
}

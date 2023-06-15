using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GraphQLDemo.API.Services
{
    public class SchoolDbContext :DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
            
        }
        public DbSet<CourseDTO> Courses { get; set; }
        public DbSet<StudentDTO> Students { get; set; }
        public DbSet<InstructorDTO> Instructors { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=F:\\All Work Merged\\GITHUB\\GraphQL\\GraphQLDemo\\GraphQLDemo.API\\Database\\school.db;");
        //}


        //dotnet ef --project "F:\All Work Merged\GITHUB\GraphQL\GraphQLDemo\GraphQLDemo.API\GraphQLDemo.API.csproj" migrations add InitialCreate
        //dotnet ef --project "F:\All Work Merged\GITHUB\GraphQL\GraphQLDemo\GraphQLDemo.API\GraphQLDemo.API.csproj" migrations remove
    }
}

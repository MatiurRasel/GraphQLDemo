using GraphQLDemo.API.Schema.Course;
using HotChocolate.Data.Filters;

namespace GraphQLDemo.API.Schema.Filters
{
    public class CourseFilterType:FilterInputType<CourseType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(x => x.Students);

            base.Configure(descriptor);
        }
    }
}

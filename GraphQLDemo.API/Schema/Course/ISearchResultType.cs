namespace GraphQLDemo.API.Schema.Course
{
    [InterfaceType("SearchResult")]
    //Interface Type When have Shared Property
    //Union Type When have non-shared property
    public interface ISearchResultType
    {
        Guid Id { get; }
    }
}

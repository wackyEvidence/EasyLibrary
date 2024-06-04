namespace EasyLibrary.API.Contracts.BookAuthor
{
    public record BookAuthorResponseFull(
        Guid Id, 
        string Name, 
        string Bio 
    );
}

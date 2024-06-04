namespace EasyLibrary.Application.Exceptions
{
    internal class BookAuthorNotFoundException : Exception
    {
        public BookAuthorNotFoundException(string message) : base(message) { }
    }
}

namespace EasyLibrary.Application.Exceptions
{
    internal class BookSeriesNotFoundException : Exception
    {
        public BookSeriesNotFoundException(string message) : base(message) { }
    }
}

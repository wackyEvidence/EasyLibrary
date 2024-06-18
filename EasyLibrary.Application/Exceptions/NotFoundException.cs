namespace EasyLibrary.Application.Exceptions
{
    public class NotFoundException <T> : Exception
    {
        public NotFoundException(Guid id) 
            : base($"{nameof(T)} with id ${id} was not found") 
        { 
        }
    }
}

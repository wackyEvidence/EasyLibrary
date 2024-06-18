using EasyLibrary.Core.Models;

namespace EasyLibrary.DTO.BookTypes
{
    public record BookTypeResponseDisplay( 
        Guid Id, 
        string Title, 
        BookSeries BookSeries, 
        PublishingHouse PublishingHouse, 
        string ISBN
    );
}

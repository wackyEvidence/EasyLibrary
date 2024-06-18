using EasyLibrary.Core;
using EasyLibrary.Core.Models;

namespace EasyLibrary.DTO.BookTypes
{
    public record BookTypeResponseFull( 
        Guid Id, 
        string Title,
        BookSeries BookSeries,
        PublishingHouse PublishingHouse, 
        List<BookAuthor> BookAuthors, 
        BookBinding Cover, 
        int PublishingYear, 
        string ISBN, 
        int PagesCount, 
        int Weight, 
        bool AvailableForIssuance, 
        int TimesIssued, 
        DateOnly AppearanceDate, 
        int MinAge
    );
}

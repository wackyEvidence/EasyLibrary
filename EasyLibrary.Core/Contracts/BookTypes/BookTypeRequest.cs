namespace EasyLibrary.DTO.BookTypes
{
    public record BookTypeRequest(
        string Title,
        Guid PublishingHouseId,
        Guid BookSeriesId,
        List<Guid> AuthorsId,
        int Cover,
        int PublishingYear,
        string ISBN,
        int PagesCount,
        int Weight,
        bool AvailableForIssuance,
        int MinAge
    );
}

﻿namespace EasyLibrary.API.Contracts
{
    public record UserResponse (
        Guid Id, 
        string Name, 
        string Surname, 
        string? Patronymic, 
        string Email
        );
}

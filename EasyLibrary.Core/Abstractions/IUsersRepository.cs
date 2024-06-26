﻿using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> Create(User user);
        Task<Guid> Delete(Guid id);
        Task<User> GetByEmail(string email);
        Task<User?> GetById(Guid id);    
        Task<List<User>> Get();
        Task<Guid> Update(Guid id, string name, string? surname, string? patronymic, string? passportNumber, string? passportSeries, DateOnly? birthDate, string email, string? phoneNumber, bool isAdmin);
    }
}
using monchotradebackend.Interface;

namespace monchotradebackend.service;

public class PasswordHashingService : IPasswordHashingService{
    public string HashPassword(string password){
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
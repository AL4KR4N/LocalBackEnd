namespace monchotradebackend.Interface; 
//inteface minuscula es palabra reservada xd
public interface IPasswordHashingService{
    string HashPassword(string Password);
}
using DeliveryApp.DTO.UserDTO;
using System.Collections.Generic;

namespace DeliveryApp.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetUsers();
        UserDto FindById(long id);
        bool AddUser(UserDto userDto);
        bool ModifyUser(UserDto user);
        TokenDto Login(LoginDto loginDto);
        List<UserDto> Unactivated();
        bool VerifyUser(long id);
        bool DismissUser(long id);
    }
}

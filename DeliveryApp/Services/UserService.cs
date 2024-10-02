using AutoMapper;
using DeliveryApp.DTO.UserDTO;
using DeliveryApp.Enums;
using DeliveryApp.Infrastructure;
using DeliveryApp.Interfaces;
using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DeliveryApp.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DeliveryAppDbContext _dbContext;
        private readonly IConfigurationSection _secretKey;
        private readonly IConfigurationSection _tokenAddress;
        private const string _pepper = "aasf3rko3W";

        string Encode(string raw)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(
                Encoding.Unicode.GetBytes(raw + _pepper));
                return Convert.ToBase64String(computedHash);
            }
        }
        public UserService(IMapper mapper, DeliveryAppDbContext dbContext, IConfiguration config)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _secretKey = config.GetSection("SecretKey");
            _tokenAddress = config.GetSection("tokenAddress");

        }
        public bool AddUser(UserDto userDto)
        {
            try
            {
                var users = _dbContext.Users.Where(s => s.Email == userDto.Email || s.Username == userDto.Username).ToList();
                if (users.Count != 0)
                    return false;
                userDto.Password = Encode(userDto.Password);
                if (userDto.Type.ToLower() == "dostavljac")
                {
                    userDto.Activated = false;
                    userDto.Status = SupplierState.NEVERIFIKOVAN;
                }
                else
                    userDto.Activated = true;
                User user = _mapper.Map<User>(userDto);
                user.PhotoUrl = userDto.PhotoUrl;
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return false;
            }

            return true;
        }

        public bool DismissUser(long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user == null)
                return false;
            SendMail(user.Email, false);
            user.Activated = false;
            user.Status = SupplierState.ODBIJEN;
            _dbContext.SaveChanges();
            return true;
        }

        public UserDto FindById(long id)
        {
            return _mapper.Map<UserDto>(_dbContext.Users.Find(id));
        }
        public void AddImage(long userId, string filePath)
        {
            User user = _dbContext.Users.Find(userId);
            if (user == null)
                return;
            user.PhotoUrl = filePath;
            _dbContext.SaveChanges();
        }

        public List<UserDto> GetUsers()
        {
            List<UserDto> users = _mapper.Map<List<UserDto>>(_dbContext.Users.ToList());
            foreach (var item in users)
            {
                item.Password = "";
            }
            return users;
        }

        public TokenDto Login(LoginDto loginDto)
        {
            var users = _dbContext.Users.Where(s => s.Username == loginDto.Username).Where(x => x.Password == Encode(loginDto.Password)).ToList();
            if (users.Count == 0)
                return null;
            List<Claim> claims = new List<Claim>();
            if (users[0].Activated)
                claims.Add(new Claim("username", users[0].Username));
            claims.Add(new Claim("id", users[0].Id.ToString()));
            claims.Add(new Claim("role", users[0].Type));
            claims.Add(new Claim("isActivated", users[0].Activated.ToString()));
            claims.Add(new Claim("Status", users[0].Status.ToString()));
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(//kreiranje JWT
                   issuer: _tokenAddress.Value, //url servera koji je izdao token
                   claims: claims, //claimovi
                   expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                   signingCredentials: signinCredentials //kredencijali za potpis
               );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new TokenDto() { Token = tokenString };
        }

        public bool ModifyUser(UserDto userdto)
        {
            User user = _dbContext.Users.Find(userdto.Id);
            if (user == null)
                return false;
            user.Firstname = userdto.Firstname;
            user.Address = userdto.Address;
            user.BirthDate = userdto.BirthDate;
            user.Email = userdto.Email;
            user.Firstname = userdto.Firstname;
            user.Lastname = userdto.Lastname;
            if (userdto.Password.Length > 4)
                user.Password = Encode(userdto.Password);
            user.Username = userdto.Username;

            _dbContext.SaveChanges();

            return true;
        }

        public List<UserDto> Unactivated()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.Where(x => x.Status == SupplierState.NEVERIFIKOVAN).ToList());
        }

        public bool VerifyUser(long id)
        {
            User user = _dbContext.Users.Find(id);
            SendMail(user.Email, true);
            if (user == null)
                return false;
            user.Activated = true;
            user.Status = SupplierState.VERIFIKOVAN;
            _dbContext.SaveChanges();
            return true;
        }

        private static void SendMail(string to, bool uspesnost)
        {
            string from = "tadicz84@gmail.com"; //From address
            string password = "gdtu pmun pfnk zayl";
            MailMessage message = new MailMessage(from, to);
            string mailbody;
            if (uspesnost)
            {
                mailbody = "Uspesna verifikacija dostavljaca";
            }
            else
            {
                mailbody = "Neuspesna verifikacija dostavljaca";
            }
            message.Subject = "Verifikacija";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = false;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(message);
            }

            catch (SmtpException ex)
            {
            }
        }
    }
}

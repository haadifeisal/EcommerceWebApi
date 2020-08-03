using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using EcommerceWebApi.Repository.Ecommerce;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;

namespace EcommerceWebApi.Domain.ServiceManager
{
    public class AuthServiceManager : IAuthServiceManager
    {
        private readonly EcommerceContext _ecommerceContext;
        private readonly ICartServiceManager _cartServiceManager;
        private readonly ICustomerServiceManager _customerServiceManager;
        private readonly AppSettings _appSettings;

        public AuthServiceManager(EcommerceContext ecommerceContext, ICartServiceManager cartServiceManager, 
            ICustomerServiceManager customerServiceManager, IOptions<AppSettings> appSettings)
        {
            _ecommerceContext = ecommerceContext;
            _cartServiceManager = cartServiceManager;
            _customerServiceManager = customerServiceManager;
            _appSettings = appSettings.Value;
        }

        public User Login(string username, string password)
        {
            var user = _ecommerceContext.User.AsNoTracking().FirstOrDefault(x => x.Username == username);
            
            if(user == null)
            {
                return null;
            }

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;

        }

        public Admin AdminLogin(string username, string password)
        {
            var admin = _ecommerceContext.Admin.AsNoTracking().FirstOrDefault(x => x.Username == username);

            if (admin == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
            {
                return null;
            }

            return admin;

        }

        public bool RegisterCustomer(CustomerRequestDto customerRequestDto)
        {
            if (UserExists(customerRequestDto.userRequestDto.Username))
            {
                return false;
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(customerRequestDto.userRequestDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = customerRequestDto.userRequestDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _ecommerceContext.Add(user);

            if (_ecommerceContext.SaveChanges() == 1)
            {
                if (_customerServiceManager.AddCustomer(user.UserId, customerRequestDto))
                {
                    if (_cartServiceManager.CreateCart(user.UserId))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool AddAdmin(UserRequestDto userRequestDto)
        {
            var admin = _ecommerceContext.Admin.AsNoTracking().FirstOrDefault(x => x.Username == userRequestDto.Username);

            if(admin == null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userRequestDto.Password, out passwordHash, out passwordSalt);

                var newAdmin = new Admin
                {
                    AdminId = Guid.NewGuid(),
                    Username = userRequestDto.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _ecommerceContext.Add(newAdmin);

                if(_ecommerceContext.SaveChanges() == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public string GenerateToken(User user, Admin admin)
        {
            if (user == null && admin == null)
            {
                return "";
            }

            var claims = new Claim[3];

            if (user != null)
            {
                claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Customer")
                };
            }

            if (admin != null)
            {
                claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                    new Claim(ClaimTypes.Name, admin.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool UserExists(string username)
        {
            var user = _ecommerceContext.User.AsNoTracking().Any(x => x.Username == username);

            if (user)
            {
                return true;
            }

            return false;
        }
    }
}

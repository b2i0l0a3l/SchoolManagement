using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Application.Util.jwt;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Util
{
    public interface IUserRole
    {
        Task<Result<string>> AddRoleToUserAndGenerateJwtKey(User user,string role,Func<User, List<Claim>, List<Claim>>? modifier = null);
    }
    public class UserRole : IUserRole
    {
        private readonly IGenerateJwtToken _jwtToken;
        private readonly UserManager<User> _user;
        public UserRole(UserManager<User> user,IGenerateJwtToken jwtToken){
            _user = user;
            _jwtToken = jwtToken;
        }

        public async Task<Result<string>> AddRoleToUserAndGenerateJwtKey(User user,string role,Func<User, List<Claim>, List<Claim>>? modifier = null)
        {
            user.Role = role;
            await _user.UpdateAsync(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role),

            };
            if (modifier != null)
            {
                claims = modifier(user, claims);

            }
            return _jwtToken.GenerateJwtToken(claims);   
        }
    }
}
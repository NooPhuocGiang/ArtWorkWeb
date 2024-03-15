using ArtWorkWeb.Service.Interfaces;
using BussinessTier;
using BussinessTier.Enums;
using BussinessTier.Payload;
using DataTier.Models;
using DataTier.Repository.Interface;
using DataTier.View.Common;
using DataTier.View.User;
using System;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ArtWorkWeb.Service.Implement
{
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork<projectSWDContext> unitOfWork, ILogger<UserService> logger, IUserRepository userRepository) : base(unitOfWork, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            Expression<Func<User, bool>> searchFilter = p =>
                p.Username.Equals(loginRequest.Username) &&
                p.Password.Equals(loginRequest.Password);

            User user = await _unitOfWork.GetRepository<User>()
                .SingleOrDefaultAsync(predicate: searchFilter);

            if (user == null) return null;

            var token = JwtUtil.GenerateJwtToken(user);

            LoginResponse loginResponse = new LoginResponse()
            {
                AccessToken = token,
                UserInfo = new UserResponse()
                {
                    IdUser = user.IdUser,
                    Username = user.Username,
                    Role = EnumUtil.ParseEnum<RoleEnum>(user.Role),
                }
            };

            return loginResponse;
        }

        public MessageViewModel UpdateProfile(ProfileUpdateRequest model)
        {
            var success = _userRepository.UpdateProfile(model);

            if (success)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Profile updated successfully"
                };
            }
            else
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "User not found or profile update failed"
                };
            }
        }
    }
}

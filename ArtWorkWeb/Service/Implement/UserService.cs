using ArtWorkWeb.Service.Interfaces;
using AutoMapper;
using BussinessTier;
using BussinessTier.Enums;
using BussinessTier.Payload;
using BussinessTier.Payload.User;
using DataTier.Models;
using DataTier.Repository.Implement;
using DataTier.Repository.Interface;
using DataTier.View.Common;
using DataTier.View.User;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ArtWorkWeb.Service.Implement
{
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork<projectSWDContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool BanUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public KeyValuePair<MessageViewModel, List<UserProfileViewModel>> GetAllUser()
        {
            var users = _userRepository.GetAllUser();
            if (users == null)
            {
                return new KeyValuePair<MessageViewModel, List<UserProfileViewModel>>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = string.Empty,
                    },null
                    );
            }
            return new KeyValuePair<MessageViewModel, List<UserProfileViewModel>>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty,
                }, users
                );
        }

        public KeyValuePair<MessageViewModel, UserProfileViewModel> GetUser(int id)
        {
            var user = _userRepository.GetUserByID(id);
            if (user == null)
            {
                return new KeyValuePair<MessageViewModel, UserProfileViewModel>
                    (
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = "Not found this user"
                    },
                    null
                    );
            }
            var response = _mapper.Map<UserProfileViewModel>(user); 

            return new KeyValuePair<MessageViewModel, UserProfileViewModel>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                }, response
                );
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
        public async Task<IPaginate<GetUserResponse>> GetAllUsers(UserFilter filter, PagingModel pagingModel)
        {
            IPaginate<GetUserResponse> response = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
                selector: x => _mapper.Map<GetUserResponse>(x),
                filter: filter,
                orderBy: x => x.OrderBy(x => x.Role),
                page: pagingModel.page,
                size: pagingModel.size
                );
            return response;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            if(request.Username.IsNullOrEmpty() || request.Password.Length < 8 || request.Email.IsNullOrEmpty())
{
                return false;
            }
            
            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Gender = request.Gender,
                Email = request.Email,
                Role = RoleEnum.User.ToString()
            };
            var existingUser =  _userRepository.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                // Tên đăng ký đã tồn tại, không thể đăng ký
                return false;
            }
            var existingEmail = _userRepository.GetUserByEmail(user.Email);
            if (existingEmail != null)
            {
                // Tên đăng ký đã tồn tại, không thể đăng ký
                return false;
            }
            var model = await _userRepository.Register(user);
            if(model == null) return false;
            return true;

        }
    }
}

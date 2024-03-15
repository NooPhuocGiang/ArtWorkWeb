using BussinessTier.Payload;
using DataTier.Repository.Implement;
using DataTier.View.Common;
using DataTier.View.Product;
using DataTier.View.User;

namespace ArtWorkWeb.Service.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);

        MessageViewModel UpdateProfile(ProfileUpdateRequest model);
    }
}
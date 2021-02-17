using AMA.Models.DTOS;
using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Services
{
    public interface IUserService
    {
        bool IsValidUserCredentials(string username, string password);
        UserRole GetUserRole(string username);
        void RegisterUser(RegisterRequest request);
        void EditUser(User user, EditUserProfileRequest request);
        void BanUser(BanUserRequest request);
        void SendMessage(SendMessageRequest request, int fromUserId);
        void MakePayment(MakePaymentRequest request, int fromUserId);
        void ChangeUserState(int userId);
        void FollowUser(InsertUserFollowRequest request);
        void RemoveUserFollow(DeleteUserFollowRequest request);
        void FollowCategory(InsertUserFollowCategoryRequest request);
        void DeleteFollowCategory(InsertUserFollowCategoryRequest request);
        void DeleteFollowSubCategory(InsertUserFollowSubCategoryRequest request);
        void FollowSubCategory(InsertUserFollowSubCategoryRequest request);
    }
}

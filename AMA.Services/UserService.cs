﻿using AMA.Models.DTOS;
using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using AMA.Services.Helpers;
using System;
using System.Linq;

namespace AMA.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryUserRole _repositoryUserRole;
        private readonly IRepositoryBan _repositoryBan;
        private readonly IRepositoryMessage _repositoryMessage;
        private readonly IRepositoryPayment _repositoryPayment;
        private readonly PasswordHasher _passwordHasher;
        public UserService(IRepositoryUser repositoryUser,
            IRepositoryUserRole repositoryUserRole,
            IRepositoryBan repositoryBan,
            IRepositoryMessage repositoryMessage,
            PasswordHasher passwordHasher)
        {
            _repositoryUser = repositoryUser;
            _repositoryUserRole = repositoryUserRole;
            _repositoryBan = repositoryBan;
            _repositoryMessage = repositoryMessage;
            _passwordHasher = passwordHasher;
        }
        public bool IsValidUserCredentials(string username, string password)
        {
            var user = _repositoryUser.TryFind(username);
            if(user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
                return result == PasswordVerificationResult.Success;
            }
            return false;
        }

        public UserRole GetUserRole(string username)
        {
            var user = _repositoryUser.TryFind(username);
            if (user != null)
                return _repositoryUserRole.Get(user.ID);
            else
                return null;
        }

        public void RegisterUser(RegisterRequest request)
        {
            var user = new User
            {
                BirthDate = request.BirthDate,
                Username = request.UserName,
                CityId = request.CityId,
                FirstName = request.FirstName,
                Gender = request.Gender,
                LastName = request.LastName,
                Mail = request.Mail,
                PasswordHash = _passwordHasher.HashPassword(request.Password)
            };

            _repositoryUser.Insert(user);

            var userRole = new UserRole
            {
                Role = Common.Enumerations.UserRole.User,
                UserId = user.ID
            };

            _repositoryUserRole.Insert(userRole);
        }

        public void EditUser(User user, EditUserProfileRequest request)
        {

            user.Mail = request.Mail;
            user.Gender = request.Gender;
            user.CityId = request.CityId;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.BirthDate = request.BirthDate;

            _repositoryUser.Update(user);
        }

        public void BanUser(BanUserRequest request)
        {
            var userBans = _repositoryBan.FindAllByUserId(request.UserId).ToList();
            var activeBan = userBans.Where(x => x.DateTimeTo > DateTime.Now).FirstOrDefault();

            if (activeBan != null)
            {
                activeBan.DateTimeTo = request.TimeTo;
                activeBan.Reason = request.Reason;
                _repositoryBan.Update(activeBan);
            }
            else
            {
                var ban = new Ban
                {
                    DateTimeFrom = request.TimeFrom,
                    DateTimeTo = request.TimeTo,
                    UserId = request.UserId,
                    Reason = request.Reason
                };

                _repositoryBan.Insert(ban);
            }
        }

        public void SendMessage(SendMessageRequest request, int fromUserId)
        {
            var messeage = new Message
            {
                Body = request.Body,
                FromUserId = fromUserId,
                ToUserId = request.ToUserId,
                Title = request.Title
            };

            _repositoryMessage.Insert(messeage);
        }

        public void MakePayment(MakePaymentRequest request, int fromUserId)
        {
            var payment = new Payment
            {
                FromUserId = fromUserId,
                ToUserId = request.ToUserId,
                Amount = request.Amount
            };
        }
    }
}
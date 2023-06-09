﻿using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterUser(UserRegisterModel model);
        Task<UserModel> ValidateUser(string email, string password);
    }
}

//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UtileDigitalSecurity.Data;
//using UtileDigitalSecurity.Models;
//using UtileDigitalSecurity.Repository;
//using UtileDigitalSecurity.ViewModels;

//namespace UtileDigitalSecurity.Services
//{
//    public class UserService
//    {
//        private ApplicationDbContext db;
//        UserRepository urepo;
//        private readonly IMapper _mapper;
//        public UserService(ApplicationDbContext db, IMapper mapper)
//        {
//            this.db = db;
//            this._mapper = mapper;
//            urepo = new UserRepository(db);
//        }

//        public async Task<IEnumerable<AddUserViewModel>> GetUsers()
//        {
//            return await urepo.GetUsers();
//        }

//        public async Task<ApplicationUser> CheckUserByEmail(string email)
//        {
//            return await urepo.CheckUserByEmail(email);
//        }

//        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
//        {
//            return await urepo.CreateUser(user);
//        }

//        public async Task<ApplicationUser> DeleteUserById(int userId)
//        {
//            return await urepo.DeleteUserById(userId);
//        }

//        public string DecryptPassword(string encrString)
//        {
//            byte[] b;
//            string decrypted;
//            try
//            {
//                b = Convert.FromBase64String(encrString);
//                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
//            }
//            catch (FormatException fe)
//            {
//                decrypted = "";
//            }
//            return decrypted;
//        }

//        internal async Task<AddUserViewModel> EditUserDataById(int userId)
//        {
//            return await urepo.EditUserDataById(userId);
//        }

//        public async Task<ApplicationUser> UpdateUser(ApplicationUser user)
//        {
//            return await urepo.UpdateUser(user);
//        }

//        public async Task<ApplicationUser> CheckUserByIdAsync(int id)
//        {
//            return await urepo.CheckUserByIdAsync(id);
//        }

//        public string EncryptPassword(string encrptPassword)
//        {
//            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(encrptPassword);
//            string encrypted = Convert.ToBase64String(b);
//            return encrypted;
//        }
//    }
//}

//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UtileDigitalSecurity.Data;
//using UtileDigitalSecurity.Models;
//using UtileDigitalSecurity.ViewModels;

//namespace UtileDigitalSecurity.Repository
//{
//    public class UserRepository
//    {
//        private ApplicationDbContext db;
//        private DbSet<ApplicationUser> Set => db.Users;

//        public UserRepository(ApplicationDbContext db)
//        {
//            this.db = db;
//        }

//        public async Task<IEnumerable<AddUserViewModel>> GetUsers()
//        {
//            return await (from i in db.Users
//                          join j in db.Roles on i.RoleId equals j.Id
//                          select new AddUserViewModel
//                          {
//                              Id = i.Id,
//                              Email = i.Email,
//                              Name = i.UserName,
//                              Password = i.PasswordHash
//                              Role = j.Name,
//                          }).ToListAsync();
//        }

//        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
//        {
//            var i = await Set.AddAsync(user);
//            await db.SaveChangesAsync();
//            return user;

//        }

//        public async Task<ApplicationUser> DeleteUserById(int userId)
//        {
//            var data = await Set.FirstOrDefaultAsync(s => s.Id == userId);
//            db.Users.Remove(data);
//            db.Entry(data).State = EntityState.Deleted;
//            await db.SaveChangesAsync();
//            return new ApplicationUser();
//        }

//        public async Task<AddUserViewModel> EditUserDataById(int userId)
//        {
//            return await (from i in db.Users
//                          join j in db.Roles on i.Id equals userId
//                          where i.RoleId == j.Id

//                          select new AddUserViewModel
//                          {
//                              Id = i.Id,
//                              Email = i.Email,
//                              Name = i.Name,
//                              Password = i.Password,
//                              Role = j.Name,
//                          }).FirstOrDefaultAsync();

//        }

//        public async Task<ApplicationUser> CheckUserByIdAsync(int id)
//        {
//            return await Set.FirstOrDefaultAsync(s => s.Id == id);
//        }

//        public async Task<ApplicationUser> UpdateUser(ApplicationUser user)
//        {
//            var record = await db.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
//            record.Name = user.Name;
//            record.RoleId = user.RoleId;
//            record.Email = user.Email;
//            record.Password = user.Password;
//            await db.SaveChangesAsync();
//            db.Entry(record).State = EntityState.Modified;
//            return record;
//        }

//        public async Task<ApplicationUser> CheckUserByEmail(string email)
//        {
//            return await Set.FirstOrDefaultAsync(s => s.Email == email);
//        }
//    }
//}

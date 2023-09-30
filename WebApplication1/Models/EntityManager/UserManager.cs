using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Models.DB;
using MyWebApplication.Models.ViewModel;
using WebApplication1.Models.DB;

namespace MyWebApplication.Models.EntityManager
{
    public class UserManager
    {
        public void AddUserAccount(UserModel user)
        {
            using (MyDBContext db = new MyDBContext())
            {
                //Add checking here if login exists

                TestUsers newSysUser = new TestUsers
                {
                    LogInName = user.LoginName,
                    CreatedBy = 1,
                    PasswordEncryptedText = user.Password, //this has to be encrypted
                    CreatedDateTime = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedDateTime = DateTime.Now
                };

                db.SystemUsers.Add(newSysUser);
                db.SaveChanges();

                int newUserId = db.SystemUsers.First(u => u.LogInName == newSysUser.LogInName).UserID;

                Users newUser = new Users
                {
                    UserID = newUserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = "1",
                    CreatedBy = 1,
                    CreatedDateTime = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedDateTime = DateTime.Now
                };

                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        public List<Users> GetAllUsers()
        {
            using (MyDBContext db = new MyDBContext())
            {
                return db.Users.ToList();
            }

        }
        public bool IsLoginNameExist(string loginName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                return db.SystemUsers.Where(u => u.LogInName.Equals(loginName)).Any();
            }
        }
    }
}

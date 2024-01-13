using PayPalDemo.Data;
using PayPalDemo.Models;

namespace PayPalDemo.Repositories
{
    public class MyRegisteredUserRepo
    {
        private readonly ApplicationDbContext _db;

        public MyRegisteredUserRepo(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void AddRegisteredUser(string email, string firstName, string lastName)
        {
            MyRegisteredUser registerUser = new MyRegisteredUser()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            _db.MyRegisteredUsers.Add(registerUser);
            _db.SaveChanges();
        }

        public string GetUsernameFromEmail(string email)
        {
            MyRegisteredUser registeredUser = _db.MyRegisteredUsers.Where((u) => u.Email == email).FirstOrDefault();
            return $"{registeredUser.FirstName} {registeredUser.LastName}";
        }
    }
}

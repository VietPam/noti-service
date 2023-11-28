using noti_service.Model;

namespace noti_service.APIs
{
    public class MyUser
    {
        public MyUser() { }
        public class UserDTOResponse
        {
            public string code { get; set; }
        }
        public async Task<bool> createUserAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            using(DataContext context = new DataContext())
            {
                SqlUser? existing_user= context.users.Where(s=>s.code == code).FirstOrDefault();
                if (existing_user != null)
                {
                    return false;
                }
                else
                {
                    SqlUser newUser= new SqlUser();
                    newUser.ID = DateTime.Now.Ticks;
                    newUser.code = code;
                    context.users.Add(newUser);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
        }

        public List<UserDTOResponse> getListUser()
        {
            return new List<UserDTOResponse>();
        }
    }
}

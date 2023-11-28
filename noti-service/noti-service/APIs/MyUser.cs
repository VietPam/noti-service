using noti_service.Model;
using Serilog;
using System.Linq.Expressions;

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
            using (DataContext context = new DataContext())
            {
                SqlUser? existing_user = context.users.Where(s => s.code == code).FirstOrDefault();
                if (existing_user != null)
                {
                    return false;
                }
                else
                {
                    SqlUser newUser = new SqlUser();
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
            using (DataContext context = new DataContext())
            {
                List<UserDTOResponse> listUser = new List<UserDTOResponse>();
                List<SqlUser> users = context.users.ToList();
                if (users.Count > 0)
                {
                    foreach (SqlUser user in users)
                    {
                        UserDTOResponse tmp = new UserDTOResponse();
                        tmp.code = user.code;
                        listUser.Add(tmp);
                    }
                    return listUser;
                }
                else
                {
                    return new List<UserDTOResponse>();
                }
            }
        }

        public async Task<bool> disconnectUserAsync(string id)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    SqlUser? user = context.users.Where(s => s.IdHub.CompareTo(id) == 0).FirstOrDefault();
                    if (user == null)
                    {
                        return false;
                    }

                    user.IdHub = "";
                    await context.SaveChangesAsync();
                    return true;

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return false;
                }
            }
        }

        public async Task<bool> updateUserAsync(string idHub, string code)
        {
            using (DataContext context = new DataContext())
            {
                try { 
                SqlUser user = context.users.Where(s=>s.code.CompareTo(code) == 0).FirstOrDefault();
                if( user == null)
                {
                    return false;
                }
                user.IdHub = idHub;
                await context.SaveChangesAsync();
                return true;
                }catch(Exception ex)
                {
                    Log.Error(ex.Message);
                    return false;
                }
            }
        }
    }
}

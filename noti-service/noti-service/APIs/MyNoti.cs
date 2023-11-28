using Microsoft.EntityFrameworkCore;
using noti_service.Model;

namespace noti_service.APIs
{
    public class NotiDTOResponse
    {
        public string time { get; set; }
        public string body { get; set; }
    }
    public class MyNoti
    {
        public MyNoti() { }

        public List<NotiDTOResponse> GetListNoti(string code)
        {
            List<NotiDTOResponse> list_noti = new List<NotiDTOResponse>();
            using (DataContext context = new DataContext())
            {
                List<SqlNoti> notis = context.notis.Include(s => s.user).Where(s => s.user.code == code).ToList();
                foreach (SqlNoti noti in notis)
                {
                    NotiDTOResponse tmp = new NotiDTOResponse();
                    tmp.time = noti.time;
                    tmp.body = noti.body;
                    list_noti.Add(tmp);
                }
                list_noti=list_noti.OrderByDescending(s => s.time).ToList();
                return list_noti;
            }
        }
        public async Task<bool> createNotiAsync(string code, string body)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(body))
            {
                return false;
            }

            using (DataContext context = new DataContext())
            {
                SqlUser? existing_user = context.users.Where(s=>s.code==code).FirstOrDefault();
                if(existing_user == null)
                {
                    return false;
                }
                else
                {
                    SqlNoti noti = new SqlNoti();
                    noti.user= existing_user;
                    noti.body= body;
                    context.notis.Add(noti);
                    await context.SaveChangesAsync();
                    return true;
                }
                //signalr o day
            }
            return false;
        }
    }
}

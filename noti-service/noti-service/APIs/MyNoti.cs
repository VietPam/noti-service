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
                List<SqlNoti> notis = context.notis.Include(s => s.user).Where(s => s.user.code==code).ToList();
                foreach (SqlNoti noti in notis)
                {
                    NotiDTOResponse tmp = new NotiDTOResponse();
                    tmp.time = noti.time;
                    tmp.body = noti.body;
                    list_noti.Add(tmp);
                }
                list_noti.OrderByDescending(s => s.time).ToList();
                return list_noti;
            }
            return new List<NotiDTOResponse>();
        }

        public bool createNotiAsync(string code, string body)
        {
            return false;
        }
    }
}

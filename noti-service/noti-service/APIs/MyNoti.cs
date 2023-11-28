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
        
        public  List<NotiDTOResponse> GetListNoti(string code)
        {
            return new List<NotiDTOResponse>() ;
        }
        
        public bool createNotiAsync(string code, string body)
        {
            return false;
        }
    }
}

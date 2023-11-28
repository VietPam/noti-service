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
            return false;
        }

        public List<UserDTOResponse> getListUser()
        {
            return new List<UserDTOResponse>();
        }
    }
}

namespace Login_Signup.Models
{
    public class OrderRequestModel
    {
        public List<int> ArtIDList { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
    }
}

namespace UserService.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string OtherData { get; set; } = null!;
    }
}

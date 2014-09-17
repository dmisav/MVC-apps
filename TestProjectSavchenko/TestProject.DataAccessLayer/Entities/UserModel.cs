namespace TestProject.DataAccessLayer.Entities
{
    public class UserModel:AbstractModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string provinceId { get; set; }
        public string countryId { get; set; }
    }
}
namespace WebApi.Models
{
    public class UserModel
    {
        /// <summary>
        /// Quantity of random users you want to create. 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Shortcode of nationality (EUR), (CHN), (AMR) O (CAN).
        /// </summary>
        public string ShortNationality { get; set; }
    }
}

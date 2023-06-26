using System.ComponentModel.DataAnnotations;

namespace SimpleLoginApi.Model
{
    
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPass { get; set; }


    }
}

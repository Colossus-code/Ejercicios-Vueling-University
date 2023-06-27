namespace DomainEntity
{
    public class UserDomainEntity
    {
        public string Username { get; set; } = string.Empty;

        public PasswordEncryptDomainEntity Password { get; set; }

        public List<OrdersDomainEntity> ? Orders { get; set; }
    }
}
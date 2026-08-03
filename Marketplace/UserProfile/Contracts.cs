namespace Marketplace.UserProfile
{
    public class Contracts
    {
        public static class V1
        {
            public class RegisterUser
            {
                public Guid UserId { get; set; }
                public required string FullName { get; set; }
                public required string DisplayName { get; set; }
            }

            public class UpdateUserFullName
            {
                public Guid UserId { get; set; }
                public required string FullName { get; set; }
            }

            public class UpdateUserDisplayName
            {
                public Guid UserId { get; set; }
                public required string DisplayName { get; set; }
            }
            public class UpdateUserProfilePhoto
            {
                public Guid UserId { get; set; }
                public required string PhotoUrl { get; set; }
            }
        }
    }
}

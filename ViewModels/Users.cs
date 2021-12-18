using Microsoft.AspNetCore.Identity;

namespace TrainReservation.ViewModels
{
    public class Users
    {
        public List<IdentityUser> AllUsers { get; set; }

        public Users()
        {
            AllUsers = new List<IdentityUser>();
        }
    }
}

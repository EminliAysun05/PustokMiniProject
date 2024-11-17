using Microsoft.AspNetCore.Identity;

namespace Pustokk.DAL.DataContext.Entities;

public class AppUser : IdentityUser
{
    public ICollection<BasketItem>? BasketItems { get; set; }
    public bool IsSubscribed { get; set; }
    public bool IsActive { get; set; }

    public required string FullName { get; set; }

    //ctorla bagli object initilaze edir
    public AppUser()
    {
        BasketItems = new List<BasketItem>();
    }

}

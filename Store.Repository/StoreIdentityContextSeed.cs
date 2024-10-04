using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmad Khaled",
                    Email = "ahmad@gmail.com",
                    UserName = "ahmadKhaled",
                    Address = new Address
                    {
                        FirstName = "Ahmad",
                        LastName = "Morshed",
                        City = "Daraa",
                        State = "Syria",
                        Street = "5",
                        PostalCode="120130"

                    }
                };
                await userManager.CreateAsync(user,"Password123!");
            }
        }
    }
}

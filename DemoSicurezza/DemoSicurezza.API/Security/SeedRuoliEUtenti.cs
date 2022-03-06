using Microsoft.AspNetCore.Identity;

namespace DemoSicurezza.API.Security
{
    public static class SeedRuoliEUtenti
    {
        public async static Task Seed(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            string utente,
            string ruolo)
        {
            await CreaRuolo(roleManager, ruolo);
            await AssegnaRuoloAdUtente(userManager, utente, ruolo);
        }


        private async static Task CreaRuolo(
            RoleManager<IdentityRole> roleManager, string ruolo)
        {
            bool esisteRuolo =
                await roleManager.RoleExistsAsync(ruolo);
            if(!esisteRuolo)
            {
                var role = new IdentityRole
                {
                    Name = ruolo
                };
                await roleManager.CreateAsync(role);
            }
        }

        private async static Task AssegnaRuoloAdUtente(
            UserManager<IdentityUser> userManager,
            string utente, string ruolo)
        {
            bool esisteUtente = await userManager.FindByEmailAsync
                (utente) != null;
            if(esisteUtente == false)
            {
                var nuovoUtente = new IdentityUser
                {
                    UserName = utente,
                    Email = utente
                };
                var result = await userManager.CreateAsync(nuovoUtente, "MiaPassword1!");
                if(result.Succeeded == true)
                {
                    await userManager.AddToRoleAsync(nuovoUtente,
                                       ruolo);
                }
            } 
        }
    }
}

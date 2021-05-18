using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;

namespace VOD.Common.Entities
{
   public class VODUser : IdentityUser /*IdentityUser is ASPNetUser Table (Microsoft generated tables) */
    {
       public string Token { get; set; } /*Any additional columns that we want to have that are missing in ASPNetUser Table */
       public DateTime TokenExpires { get; set; }

        [NotMapped]  /*Creating a property but one that doesn't exist in a table or the database*/
        public IList<Claim> Claims { get; set; } = new List<Claim>(); 
    }
}

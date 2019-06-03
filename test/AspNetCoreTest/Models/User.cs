using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Data;

namespace AspNetCoreTest.Models
{
    public class User : EntityBase<Guid>,IUser
    {

        public string Account { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }


        public string RoleName { get; set; }
    }
}

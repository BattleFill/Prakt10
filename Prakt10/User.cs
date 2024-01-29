using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prakt10
{

        internal class User : ICloneable
        {
            public int id;
            public string login;
            public string password;
            public int post;
            public User(int id, string login, string password, int post)
            {
                this.id = id;
                this.login = login;
                this.password = password;
                this.post = post;
            }

            public object Clone()
            {
                return (User)this.MemberwiseClone();
            }
        }

}

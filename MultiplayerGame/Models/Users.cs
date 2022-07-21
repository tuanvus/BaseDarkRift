using DarkRift.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerGame.Models
{
   public class User
    {
        public string username { get; set; }

        public Room room { get; set; }
        public IClient client ;

    }
}

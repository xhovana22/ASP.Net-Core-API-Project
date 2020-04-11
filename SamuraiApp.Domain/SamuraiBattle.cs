using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    //join entity of many to many relationship
    public class SamuraiBattle
    {
        public int SamuraiId { get; set; }
        public int BattleId { get; set; }
      
        public Samurai Samurai { get; set; }
        public Battle Battle { get; set; }
    }
}

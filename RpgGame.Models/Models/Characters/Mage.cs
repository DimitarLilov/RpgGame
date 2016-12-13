using RpgGame.Models;

namespace RpgGame.Models.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Mage : Character
    {
        private const int MageDefaultAwareness = 50;
        private const int MageDefaultSteps = 50;
        private const int MageDefaultMinAttack = 50;
        private const int MageDefaultMaxAttack = 50;
        private const int MageDefaultMinDefence = 50;
        private const int MageDefaultMaxDefence = 50;
        private const int MageDefaultGold = 50;
        private const int MageDefaultHealth = 50;
        private const int MageDefaultMaxHealth = 50;
        private const int MageDefaultSpeed = 50;
        private const int MageDefaultTime = 50;
        private const char MageDefaultSymbol = 'M';


        public Mage(string name, int steps, int awareness, int minAttack, int maxAttack, int minDefence, int maxDefence, int gold, int health, int maxHealth, int speed, string color, char symbol) 
            : base(name, MageDefaultSteps, MageDefaultAwareness, MageDefaultMinAttack, MageDefaultMaxAttack, MageDefaultMinDefence, MageDefaultMaxDefence, MageDefaultGold, MageDefaultHealth, MageDefaultMaxHealth, MageDefaultSpeed, color, MageDefaultSymbol)
        {
        }
    }
}

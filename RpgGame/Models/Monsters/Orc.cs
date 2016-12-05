namespace RpgGame.Models.Monsters
{
    using RpgGame.Utilities;

    public class Orc : Monster
    {
        private const int DefaultAwareness = 10;

        private const int DefaultMinAttack = 5;
        private const int DefaultMaxAttack = 25;

        private const int DefaultMinDefence = 0;
        private const int DefaultMaxDefence = 15;

        private const int DefaultGold = 10;
        private const int DefaultHealth = 20;
        private const int DefaultMaxHealth = 20;

        private const int DefaultSpeed = 14;
        private const char DefaultSymbol = 'o';

        public Orc()
            : base(DefaultAwareness,
                   DefaultMinAttack, DefaultMaxAttack,
                  DefaultMinDefence, DefaultMaxDefence,
                  DefaultGold, DefaultHealth, DefaultMaxHealth,
                  DefaultSpeed, Colors.OrcColor, DefaultSymbol, 10, 10)
        {
        }
    }
}

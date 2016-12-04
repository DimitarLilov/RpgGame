namespace RpgGame.Models.Monsters
{
    using RpgGame.Utilities;

    public class Orc : Monster
    {
        private const int DefaultAwareness = 10;

        private const int DefaultAttack = 1;
        private const int DefaultAttackChance = 20;

        private const int DefaultDefence = 0;
        private const int DefaultDefenceChance = 10;

        private const int DefaultGold = 10;
        private const int DefaultHealth = 20;
        private const int DefaultMaxHealth = 20;

        private const int DefaultSpeed = 14;
        private const char DefaultSymbol = 'o';

        public Orc(string name)
            : base(name, DefaultAwareness,
                  DefaultAttack, DefaultAttackChance,
                  DefaultDefence, DefaultDefenceChance,
                  DefaultGold, DefaultHealth, DefaultMaxHealth,
                  DefaultSpeed, Colors.OrcColor, DefaultSymbol, 10, 10)
        {
        }
    }
}

namespace RpgGame.Core.Monsters
{
    using RogueSharp.DiceNotation;

    public class Orc : Monster
    {
        public static Orc Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Orc
            {
                Attack = Dice.Roll("1D3") + (level / 3),
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.OrcColor,
                Defense = Dice.Roll("1D3") + (level / 3),
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Orc",
                Speed = 14,
                Symbol = 'o'
            };
        }
    }
}

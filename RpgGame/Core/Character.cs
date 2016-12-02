namespace RpgGame.Core
{
    using Interfaces;
    using RLNET;
    using RogueSharp;

    public class Character : ICharacter, IDrawable, IScheduleable
    {
        private int attack;
        private int attackChance;
        private int awareness;
        private int defense;
        private int defenseChance;
        private int gold;
        private int health;
        private int maxHealth;
        private string name;
        private int speed;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Attack
        {
            get { return this.attack; }
            set { this.attack = value; }
        }

        public int AttackChance
        {
            get { return this.attackChance; }
            set { this.attackChance = value; }
        }

        public int Awareness
        {
            get { return this.awareness; }
            set { this.awareness = value; }
        }

        public int Defense
        {
            get { return this.defense; }
            set { this.defense = value; }
        }

        public int DefenseChance
        {
            get { return this.defenseChance; }
            set { this.defenseChance = value; }
        }

        public int Gold
        {
            get { return this.gold; }
            set { this.gold = value; }
        }

        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public int MaxHealth
        {
            get { return this.maxHealth; }
            set { this.maxHealth = value; }
        }

        public int Speed
        {
            get { return this.speed; }

            set { this.speed = value; }
        }

        public int Time
        {
            get
            {
                return this.Speed;
            }
        }

        public RLColor Color { get; set; }

        public char Symbol { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(this.X, this.Y).IsExplored)
            {
                return;
            }

            if (map.IsInFov(this.X, this.Y))
            {
                console.Set(this.X, this.Y, this.Color, Colors.FloorBackgroundFov, this.Symbol);
            }
        }
    }
}

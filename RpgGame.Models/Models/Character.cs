using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Models
{
    public abstract class Character
    {
        [Key]
        public int Id  { get; set; }

        public Character(string name, int steps , int awareness,
            int minAttack, int maxAttack,
            int minDefence, int maxDefence,
            int gold,
            int health, int maxHealth, int speed,
            string color, char symbol)
        {
            this.Name = name;
            this.Steps = steps;
            this.Awareness = awareness;
            this.MinAttack = minAttack;
            this.MaxAttack = maxAttack;
            this.MinDefence = minDefence;
            this.MaxDefence = maxDefence;
            this.Gold = gold;
            this.Health = health;
            this.MaxHealth = maxHealth;
            this.Speed = speed;
            this.Color = color;
            this.Symbol = symbol;
        }

        public string Name { get; set; }
        public int Awareness { get; set; }
        public int Steps { get; set; }
        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }
        public int MinDefence { get; set; }
        public int MaxDefence { get; set; }
        public int Gold { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Speed { get; set; }
        public int Time => this.Speed;
        public string Color { get; set; }
        public char Symbol { get; set; }
    }
}

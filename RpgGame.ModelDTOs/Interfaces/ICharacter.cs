namespace RpgGame.ModelDTOs.Interfaces
{
    public interface ICharacter
    {
        string Name { get; }

        int Awareness { get; set; }

        int MinAttack { get; set; }
        int MaxAttack { get; set; }

        int MinDefence { get; set; }
        int MaxDefence { get; set; }

        int Gold { get; set; }

        int Health { get; set; }

        int MaxHealth { get; set; }

        int Speed { get; set; }
    }
}

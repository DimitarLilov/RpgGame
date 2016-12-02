namespace RpgGame.Interfaces
{
    using RpgGame.Core;
    using RpgGame.Systems;

    public interface IBehavior
    {
        bool Act(Monster monster, CommandSystem commandSystem);
    }
}

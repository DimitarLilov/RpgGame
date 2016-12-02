namespace RpgGame.Systems
{
    using RpgGame.Core;

    public class CommandSystem
    {
        public bool MovePlayer(Direction direction)
        {
            int x = Engine.Player.X;
            int y = Engine.Player.Y;

            switch (direction)
            {
                case Direction.Up:
                {
                    y = Engine.Player.Y - 1;
                    break;
                }

                case Direction.Down:
                {
                    y = Engine.Player.Y + 1;
                    break;
                }

                case Direction.Left:
                {
                    x = Engine.Player.X - 1;
                    break;
                }

                case Direction.Right:
                {
                    x = Engine.Player.X + 1;
                    break;
                }

                default:
                {
                    return false;
                }
            }

            if (Engine.DungeonMap.SetActorPosition(Engine.Player, x, y))
            {
                return true;
            }

            return false;
        }
    }
}

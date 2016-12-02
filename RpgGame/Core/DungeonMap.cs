namespace RpgGame.Core
{
    using RLNET;
    using RogueSharp;

    public class DungeonMap : Map
    {
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in this.GetAllCells())
            {
                this.SetConsoleSymbolForCell(mapConsole, cell);
            }
        }

        public void UpdatePlayerFieldOfView()
        {
            Player player = Engine.Player;

            this.ComputeFov(player.X, player.Y, player.Awareness, true);

            foreach (Cell cell in this.GetAllCells())
            {
                if (this.IsInFov(cell.X, cell.Y))
                {
                    this.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            if (!cell.IsExplored)
            {
                return;
            }

            if (this.IsInFov(cell.X, cell.Y))
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }
    }
}
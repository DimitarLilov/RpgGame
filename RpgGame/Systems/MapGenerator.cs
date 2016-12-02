namespace RpgGame.Systems
{
    using Core;
    using RogueSharp;

    public class MapGenerator
    {
        private readonly int width;
        private readonly int height;

        private readonly DungeonMap map;

        public MapGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.map = new DungeonMap();
        }

        public DungeonMap CreateMap()
        {
            this.map.Initialize(this.width, this.height);
            foreach (Cell cell in this.map.GetAllCells())
            {
                this.map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            foreach (Cell cell in this.map.GetCellsInRows(0, this.height - 1))
            {
                this.map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            foreach (Cell cell in this.map.GetCellsInColumns(0, this.width - 1))
            {
                this.map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            return this.map;
        }
    }
}
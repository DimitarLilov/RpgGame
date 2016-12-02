namespace RpgGame.Core
{
    using Interfaces;

    public class TreasurePile
    {
        public TreasurePile(int x, int y, ITreasure treasure)
        {
            this.X = x;
            this.Y = y;
            this.Treasure = treasure;

            IDrawable drawableTreasure = treasure as IDrawable;
            if (drawableTreasure != null)
            {
                drawableTreasure.X = x;
                drawableTreasure.Y = y;
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        public ITreasure Treasure { get; set; }
    }
}
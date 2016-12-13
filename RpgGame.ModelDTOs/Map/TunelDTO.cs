namespace RpgGame.ModelDTOs.Map
{
    public class TunelDTO
    {
        public int XStart { get; set; }

        public int YStart { get; set; }

        public int XEnd { get; set; }

        public int YEnd { get; set; }

        public DoorDTO Door;
    }
}
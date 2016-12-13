using RLNET;
using RpgGame.Utilities.Utilities;

namespace RpgGame.ModelDTOs.Monsters
{
    public class OrcDTO : MonsterDTO
    {
        public override RLColor Color => Colors.OrcColor;

        public override char Symbol => 'o';
    }
}

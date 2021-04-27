using System;
using System.Collections.Generic;


namespace monsterhunter
{
    class Armour
    {
        public List<SkillTreeAndPoints> skillTrees { get; set; }
        public int minDefense { get; set; }
        public int maxDefense { get; set; }
        public string name { get; set; }
        public int rarity { get; set; }
        public ArmourType type { get; set; }

        public Armour() { }
    }

    class HeadArmour : Armour { }
    class BodyArmour : Armour { }
    class ArmArmour : Armour { }
    class WaistArmour : Armour { }
    class LegArmour : Armour { }

    class ArmourSet
    {
        HeadArmour headArmour;
        BodyArmour bodyArmour;
        ArmArmour armArmour;
        WaistArmour waistArmour;
        LegArmour legArmour;
    }

    class SkillTreeAndPoints
    {
        public string SkillTreeName { get; set; }
        public int Points { get; set; }
    }

    enum ArmourType
    {
        Blademaster,
        Gunner,
        Both,
    }
}

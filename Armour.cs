using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
        public int priority { get; set; }
        public ArmourSlot slot { get; set; }

        public Armour() { }

        internal bool FindSkillOverlaps(List<SkillTree> skillTrees, Armour armourPiece)
        {
            foreach (var skillTree in skillTrees)
            {
                foreach (var availableSkillTrees in armourPiece.skillTrees)
                {
                    if (skillTree.name == availableSkillTrees.SkillTreeName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    class HeadArmour : Armour { }
    class BodyArmour : Armour { }
    class ArmArmour : Armour { }
    class WaistArmour : Armour { }
    class LegArmour : Armour { }

    class ArmourSet
    {
        public HeadArmour headArmour { get; set; }
        public BodyArmour bodyArmour { get; set; }
        public ArmArmour armArmour { get; set; }
        public WaistArmour waistArmour { get; set; }
        public LegArmour legArmour { get; set; }
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

    enum ArmourSlot
    {
        Head,
        Body,
        Arm,
        Waist,
        Leg,
    }

    class ArmourDb
    {
        public List<HeadArmour> HeadArmours { get; }
        public List<BodyArmour> BodyArmours { get; }
        public List<ArmArmour> ArmArmours { get; }
        public List<WaistArmour> WaistArmours { get; }
        public List<LegArmour> LegArmours { get; }

        public ArmourDb()
        {
            HeadArmours = JsonSerializer.Deserialize<List<HeadArmour>>(File.ReadAllText("HeadArmours.json"));
            BodyArmours = JsonSerializer.Deserialize<List<BodyArmour>>(File.ReadAllText("BodyArmours.json"));
            ArmArmours = JsonSerializer.Deserialize<List<ArmArmour>>(File.ReadAllText("ArmArmours.json"));
            WaistArmours = JsonSerializer.Deserialize<List<WaistArmour>>(File.ReadAllText("WaistArmours.json"));
            LegArmours = JsonSerializer.Deserialize<List<LegArmour>>(File.ReadAllText("LegArmours.json"));
        }

        public List<Armour> Armours()
        {
            var armours = new List<Armour>();
            armours.AddRange(HeadArmours);
            armours.AddRange(BodyArmours);
            armours.AddRange(ArmArmours);
            armours.AddRange(WaistArmours);
            armours.AddRange(LegArmours);

            return armours;

            // empty line for arzg
        }
    }
}

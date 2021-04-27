using System.Collections.Generic;

namespace monsterhunter
{
    class SkillTree
    {
        public List<Skill> skills { get; set; }
        public string name { get; set; }

        // Skill ActiveSkill(int points)
        // {
        //     skills.Sort();
        //     foreach (var skill in skills)
        //     {

        //     }
        // }
    }

    class Skill
    {
        public uint requiredPoints { get; set; }
        public bool isPointsNegative { get; set; }
        public string name { get; set; }
    }
}
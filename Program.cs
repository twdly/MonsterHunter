using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace monsterhunter
{
    class Program
    {

        static void Main(string[] args)
        {
            var armourDb = new ArmourDb();
            var skillTrees = JsonSerializer.Deserialize<List<SkillTree>>(File.ReadAllText("SkillTrees.json"));

            var weaponType = SelectWeaponType();
            Console.WriteLine($"\nYour selected armour type is {weaponType}.");
            var skills = InputSkills(skillTrees);
            var armourCandidates = FindArmourCandidates(skills, weaponType, armourDb);
            AssignPriority(armourCandidates, skills);
            armourCandidates = SortByPriority(armourCandidates);
            var armourSet = GenerateSet(armourCandidates);
            GenerateReport(armourSet);
            // var topSets = SelectHighestScoreSets(armours, 5);

            // foreach (var set in topSets)
            // {
            //     var report = set.generateReport();
            //     Console.WriteLine(report);
            // }
        }

        private static void GenerateReport(ArmourSet armourSet)
        {
            var armourPieces = new List<Armour>();
            armourPieces.Add(armourSet.headArmour);
            armourPieces.Add(armourSet.bodyArmour);
            armourPieces.Add(armourSet.armArmour);
            armourPieces.Add(armourSet.waistArmour);
            armourPieces.Add(armourSet.legArmour);


            Console.WriteLine("\nArmour Pieces:");
            foreach (var armourPiece in armourPieces)
            {
                PrintArmourPiece(armourPiece);
            }

            Console.WriteLine("\nCurrent Skill Points:");
            var skills = new List<SkillTreeAndPoints>();
            skills = CountSkillPoints(armourPieces);
            PrintSkillPoints(skills);

            Console.WriteLine("\nActive Skills");

            Console.WriteLine("\nRequired Points");

            Console.WriteLine("\nRecommended Points");
        }

        private static void PrintSkillPoints(List<SkillTreeAndPoints> skills)
        {
            foreach (var skill in skills)
            {
                Console.WriteLine($"{skill.SkillTreeName}: {skill.Points} points");
            }
        }

        private static List<SkillTreeAndPoints> CountSkillPoints(List<Armour> armourSet)
        {
            var skills = new List<SkillTreeAndPoints>();
            bool skillAdded = false;
            foreach (var armour in armourSet)
            {
                foreach (var skillTree in armour.skillTrees)
                {
                    foreach (var countedSkill in skills)
                    {
                        if (countedSkill.SkillTreeName == skillTree.SkillTreeName)
                        {
                            countedSkill.Points += skillTree.Points;
                            skillAdded = true;
                            break;
                        }

                    }
                    if (skillAdded == false)
                    {
                        skills.Add(skillTree);
                    }
                    else
                    {
                        skillAdded = false;
                    }
                }
            }
            return skills;
        }

        private static void PrintArmourPiece(Armour armour)
        {
            if (armour == null)
            {
                Console.WriteLine("<Empty>");
                return;
            }
            switch (armour.rarity)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 10:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
            Console.WriteLine(armour.name);
            Console.ResetColor();
        }

        private static ArmourSet GenerateSet(List<Armour> armourCandidates)
        {
            var armourSet = new ArmourSet();
            foreach (var armour in armourCandidates)
            {
                switch (armour.slot)
                {
                    case ArmourSlot.Head:
                        if (armourSet.headArmour == null)
                        {
                            armourSet.headArmour = (HeadArmour)armour;
                        }
                        break;
                    case ArmourSlot.Body:
                        if (armourSet.bodyArmour == null)
                        {
                            armourSet.bodyArmour = (BodyArmour)armour;
                        }
                        break;
                    case ArmourSlot.Waist:
                        if (armourSet.waistArmour == null)
                        {
                            armourSet.waistArmour = (WaistArmour)armour;
                        }
                        break;
                    case ArmourSlot.Arm:
                        if (armourSet.armArmour == null)
                        {
                            armourSet.armArmour = (ArmArmour)armour;
                        }
                        break;
                    case ArmourSlot.Leg:
                        if (armourSet.legArmour == null)
                        {
                            armourSet.legArmour = (LegArmour)armour;
                        }
                        break;
                }
            }
            return armourSet;
        }

        private static List<Armour> SortByPriority(List<Armour> armourCandidates)
        {
            return armourCandidates.OrderByDescending(armour => armour.priority).ToList();
        }

        private static void AssignPriority(List<Armour> armourCandidates, List<SkillTree> skills)
        {
            foreach (var armour in armourCandidates)
            {
                int priority = 0;
                foreach (var armourSkill in armour.skillTrees)
                {

                    foreach (var skill in skills)
                    {
                        if (armourSkill.SkillTreeName == skill.name)
                        {
                            priority += armourSkill.Points;
                        }
                    }

                }
                armour.priority = priority;
            }
        }

        private static List<Armour> FindArmourCandidates(List<SkillTree> skills, ArmourType weaponType, ArmourDb armourDb)
        {
            return armourDb
                .Armours()
                .Where(armourPiece => weaponType == armourPiece.type && armourPiece.FindSkillOverlaps(skills, armourPiece))
                .ToList();
        }

        private static ArmourType SelectWeaponType()
        {
            while (true)
            {
                var input = Prompt("What type of armour set would you like?");
                switch (input)
                {
                    case "blademaster":
                        return ArmourType.Blademaster;
                    case "gunner":
                        return ArmourType.Gunner;
                    default:
                        Console.WriteLine("Available armour types are 'Blademaster' and 'Gunner'\n");
                        break;
                }
            }
        }

        private static string Prompt(string question)
        {
            Console.Write($"{question}\n> ");
            return Console.ReadLine().ToLower();
        }

        private static List<SkillTree> InputSkills(List<SkillTree> skillTrees)
        {
            List<SkillTree> inputSkills = new List<SkillTree>();

            while (true)
            {
                string input = Prompt("\nPlease input your desired skills and type \"done\" when finished.");
                if (input == "done")
                {
                    if (inputSkills.Count == 0)
                    {
                        Console.WriteLine("Please input skills before moving on.");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                bool isInputAllowed = true;
                foreach (var currentSkills in inputSkills)
                {
                    if (input == currentSkills.name)
                    {
                        Console.WriteLine("This skill has already been added");
                        isInputAllowed = false;
                    }
                }

                if (isInputAllowed)
                {
                    foreach (var skill in skillTrees)
                    {
                        if (input == skill.name)
                        {
                            inputSkills.Add(skill);
                            Console.WriteLine("Skill successfully added");
                            break;
                        }
                    }
                }
            }

            int loopCount = 0;
            int listLength = inputSkills.Count;
            Console.Write("\nInput skills are");

            foreach (var skill in inputSkills)
            {
                Console.Write($" {skill.name}");
                if (loopCount == listLength - 2)
                {
                    Console.Write(" and");
                }
                else if (loopCount != listLength - 1)
                {
                    Console.Write(',');
                }
                else
                {
                    Console.WriteLine('.');
                }
                loopCount++;
            }

            return inputSkills;
        }
    }
}

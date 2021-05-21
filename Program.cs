using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace monsterhunter
{
    class Program
    {

        static void Main(string[] args)
        {
            var headArmours = JsonSerializer.Deserialize<List<HeadArmour>>(File.ReadAllText("HeadArmours.json"));
            var bodyArmours = JsonSerializer.Deserialize<List<BodyArmour>>(File.ReadAllText("BodyArmours.json"));
            var armArmours = JsonSerializer.Deserialize<List<ArmArmour>>(File.ReadAllText("ArmArmours.json"));
            var waistArmours = JsonSerializer.Deserialize<List<WaistArmour>>(File.ReadAllText("WaistArmours.json"));
            var legArmours = JsonSerializer.Deserialize<List<LegArmour>>(File.ReadAllText("LegArmours.json"));
            var skillTrees = JsonSerializer.Deserialize<List<SkillTree>>(File.ReadAllText("SkillTrees.json"));

            var weaponType = SelectWeaponType();
            Console.WriteLine($"\nYour selected armour type is {weaponType}.");
            var skills = InputSkills(skillTrees);
            // var armours = FindNonZeroArmour(skills, weaponType);
            // SortByScore(armours);
            // var topSets = SelectHighestScoreSets(armours, 5);

            // foreach (var set in topSets)
            // {
            //     var report = set.generateReport();
            //     Console.WriteLine(report);
            // }
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
                    Console.Write('.');
                }
                loopCount++;
            }

            return inputSkills;
        }
    }
}

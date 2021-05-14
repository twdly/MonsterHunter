﻿using System;
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

            // foreach (var skillTree in skillTrees)
            // {
            //     Console.Write($"\nThe skill tree {skillTree.name} has skills ");
            //     foreach (var skill in skillTree.skills)
            //     {
            //         Console.Write($"{skill.name} ");
            //     }
            // }
            // Console.Write("\n");

            var weaponType = SelectWeaponType();
            Console.WriteLine($"\nYour selected armour type is {weaponType}.");
            // var skills = InputSkills();
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
    }
}

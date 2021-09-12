using System.Collections.Generic;
using System;

namespace Choice_in_the_Numbers
{
    class Program
    {
        static string[] PossibleItems = { "[Food Supplies]", "[Building Material]", "[Crafting Material]", "[Cloth Material]", "[Junk Item]" };
        static List<string> DroppedItems = new List<string>();
        static string choiceStr;
        static int choice = 0;

        static int Hunger = 0;
        static int Health = 0;
        static int Armor = 0;
        static int HouseStatus = 0;
        static int Experience = 0;

        static bool[] StrTree = new bool[3];
        static bool[] IntTree = new bool[3];
        static bool[] LckTree = new bool[3];

        static int TurnsPerDay = 0;
        static int Turns = 0; //if you ran into issues with DeathState add:    static int DeathState = 1;    and    static bool IsDead = false;
        static int Day = 0;

        static int FoodSup = 0;
        static int BuildMat = 0;
        static int CraftMat = 0;
        static int ClothMat = 0;

        static string CurrentChosenItem;

        static string HistoryText1;
        static string HistoryText2;
        static string HistoryText3;
        static string HistoryText4;
        static string HistoryText5; //if you ran into issues with the text. add:   static string HistoryText6;

        static bool GodMode = false;
        static bool InfiniteHunger = false;
        static bool MaxArmor = false;
        static bool MaxHouseStatus = false;
        static bool InfiniteItems = false;
        static bool UpgradePoints = false;
        static int MoreTurns = 0;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            MainMenu();
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\nChoice in the Numbers");
            Console.WriteLine("\n1. [New Game] \n2. [Guide] \n3. [Settings] \n4. [Quit] ");

            choice = 5;
            while (choice > 4 || choice < 0)
            {
                Console.WriteLine("\nPlease enter your choice [1-4]: ");
                choiceStr = Console.ReadLine();

                bool choiceNull = string.IsNullOrEmpty(choiceStr);
                bool IsNumber = int.TryParse(choiceStr, out choice);

                if (!IsNumber || choiceNull) ErrorUserInput(4);
                if (!choiceNull) choice = int.Parse(choiceStr);
                if (choice > 4 || choice < 0)
                    ErrorUserInput(4);
                else
                {
                    if (choice == 1)
                        DifficultiesMenu();
                    if (choice == 2)
                        GuideMenu();
                    if (choice == 3)
                        SettingsMenu();

                    if (choice == 0 || choice == 4)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("Are you sure you want to quit the game?");
                        Console.WriteLine("1. [Yes] \n2. [No]");
                        Console.WriteLine("-----------------------------");
                        string choiceStr2 = Console.ReadLine();
                        int choice2 = 3;
                        bool choiceNull2 = string.IsNullOrEmpty(choiceStr2);
                        bool IsNumber2 = int.TryParse(choiceStr2, out choice2);

                        if (!IsNumber2 || choiceNull2) ErrorUserInput(4);
                        if (!choiceNull2) choice2 = int.Parse(choiceStr2);
                        if (choice2 > 2 || choice2 < 1)
                            ErrorUserInput(4);
                        else
                        {
                            if (choice2 == 1)
                                Environment.Exit(0);
                            if (choice2 == 2)
                                MainMenu();
                        }
                    }
                }
            }
            MainMenu();
        }

        static void DifficultiesMenu()
        {
            Console.Clear();
            Console.WriteLine("\nChoice in the Numbers\n");
            Console.WriteLine("Difficulties:");
            Console.WriteLine("\n1. [Easy] \n2. [Normal] \n3. [Hard] \n\n4. [Back] ");
            choice = 5;
            while (choice > 4 || choice < 0)
            {
                Console.WriteLine("\nPlease enter your choice [1-4]: ");
                choiceStr = Console.ReadLine();

                bool choiceNull = string.IsNullOrEmpty(choiceStr);
                bool IsNumber = int.TryParse(choiceStr, out choice);

                if (!IsNumber || choiceNull) ErrorUserInput(5);
                if (!choiceNull) choice = int.Parse(choiceStr);
                if (choice > 4 || choice < 0)
                    ErrorUserInput(5);
                else
                {
                    if (choice == 1)
                        StartupGame(1);
                    if (choice == 2)
                        StartupGame(2);
                    if (choice == 3)
                        StartupGame(3);

                    if (choice == 0 || choice == 4)
                    {
                        MainMenu();
                    }
                }
            }
        }

        static void SettingsMenu()
        {
            Console.Clear();
            Console.WriteLine("\nChoice in the Numbers\n");
            if (!GodMode) Console.WriteLine("1. [God Mode]");
            else Console.WriteLine("1. [On][God Mode]");
            if (!InfiniteHunger) Console.WriteLine("2. [No Hunger]");
            else Console.WriteLine("2. [On][No Hunger]");
            if (!MaxArmor) Console.WriteLine("3. [Max Armor]");
            else Console.WriteLine("3. [On][Max Armor]");
            if (!MaxHouseStatus) Console.WriteLine("4. [Max House Status]");
            else Console.WriteLine("4. [On][Max House Status]");
            if (!InfiniteItems) Console.WriteLine("5. [Infinite Items]");
            else Console.WriteLine("5. [On][Infinite Items]");
            if (!UpgradePoints) Console.WriteLine("6. [Max Perk Points]");
            else Console.WriteLine("6. [On][Max Perk Points]");
            if (MoreTurns == 0) Console.WriteLine("7. [+10 Turns]");
            else Console.WriteLine("7. [On][+10 Turns]");
            Console.WriteLine("\n8. [Go Back]");

            choice = 9;
            while (choice > 8 || choice < 0)
            {
                Console.WriteLine("\nPlease enter your choice [1-8]: ");
                choiceStr = Console.ReadLine();
                bool choiceNull = string.IsNullOrEmpty(choiceStr);
                bool IsNumber = int.TryParse(choiceStr, out choice);
                if (!IsNumber || choiceNull) ErrorUserInput(6);
                if (!choiceNull) choice = int.Parse(choiceStr);
                if (choice > 8 || choice < 0)
                    ErrorUserInput(6);
                else
                {
                    if (choice == 1)
                        GodMode = !GodMode;
                    if (choice == 2)
                        InfiniteHunger = !InfiniteHunger;
                    if (choice == 3)
                        MaxArmor = !MaxArmor;
                    if (choice == 4)
                        MaxHouseStatus = !MaxHouseStatus;
                    if (choice == 5)
                        InfiniteItems = !InfiniteItems;
                    if (choice == 6)
                        UpgradePoints = !UpgradePoints;
                    if (choice == 7) {
                        if (MoreTurns <= 0)
                            MoreTurns = 10;
                        else
                            MoreTurns = 0;
                    }
                    if (choice == 8 || choice == 0)
                        MainMenu();
                    SettingsMenu();
                }
            }
        }

        static void GuideMenu()
        {
            Console.Clear();
            Console.WriteLine("\nChoice in the Numbers\n");
            Console.WriteLine("1. [Gameplay]");
            Console.WriteLine("2. [In Game Stats]");
            Console.WriteLine("3. [Items]");
            Console.WriteLine("4. [Enemies]");
            Console.WriteLine("\n5. [Go Back]");

            choice = 6;
            while (choice > 5 || choice < 0)
            {
                Console.WriteLine("\nPlease enter your choice [1-5]: ");
                choiceStr = Console.ReadLine();
                bool choiceNull = string.IsNullOrEmpty(choiceStr);
                bool IsNumber = int.TryParse(choiceStr, out choice);
                if (!IsNumber || choiceNull) ErrorUserInput(7);
                if (!choiceNull) choice = int.Parse(choiceStr);
                if (choice > 5 || choice < 0)
                    ErrorUserInput(7);
                else
                {
                    if (choice == 5 || choice == 0)
                        MainMenu();
                    Console.WriteLine("\n========================================================================================\n");
                    if (choice == 1) //gameplay
                    {
                        Console.WriteLine("During the day time you will have a number of turns, each action costs a turn.");
                        Console.WriteLine("You will be presented with a choice to pick a random Item or to perform an action.");
                        Console.WriteLine("Each action would also reduce your hunger bar. Reaching 0 means losing a health point");
                        Console.WriteLine("After you run out of turns the night would fall, during this time enemies would attack.");
                        Console.WriteLine("After everynight your hunger is reduced.");
                    }
                    if (choice == 2) //stats
                    {
                        Console.WriteLine("If you reach 0% hunger or get attacked while being defenceless you lose 1 health point.");
                        Console.WriteLine("Reaching 0 health points is gameover.");
                        Console.WriteLine("After each night you gain 1 experience point which could be used to upgrade a perk.");
                        Console.WriteLine("Upgrading a perk doesn't use a turn.");
                        Console.WriteLine("Your Armor has a direct chance to protect you from an enemy attack.");
                        Console.WriteLine("Armor is maxed out at 50%.");
                        Console.WriteLine("Your House Status will help you against enemy attacks, and protect your items.");
                        Console.WriteLine("House Status is maxed out at 100%.");
                    }
                    if (choice == 3) //items
                    {
                        Console.WriteLine("Every turn you could choose one of three random items.");
                        Console.WriteLine("You could use Food Supplies to eat and recharge you hunger bar.");
                        Console.WriteLine("Other items are used for crafting/repairing.");
                        Console.WriteLine("Most of the time you won't find anything from a Junk Pile, but sometimes you would find");
                        Console.WriteLine("in the pile a random item and very rarely even more than one item.");
                        Console.WriteLine("To prevent Enemies stealing your items at night you should repair your house.");
                    }
                    if (choice == 4) //enemies
                    {
                        Console.WriteLine("Every night you will be attacked by a random enemy.");
                        Console.WriteLine("Every enemy is different and could be or more aggressive or prefer to steal Items etc.");
                        Console.WriteLine("Craftig Armor or Repairing your house will help you survive the night against enemies.");
                        Console.WriteLine("After 7 days you will encounter a Final Boss.");
                        Console.WriteLine("The Boss will test every aspect of what you have built or crafted.");
                        Console.WriteLine("The more prepared you are for The Boss the more chance you have to get the good ending");
                        Console.WriteLine("And so the less you are prepared the more likely you are to get the bad ending");
                    }
                    Console.WriteLine("\n========================================================================================");
                    Console.WriteLine("Please press any key to continue.");
                    Console.ReadKey();
                    GuideMenu();
                }
            }
        }

        static void StartupGame(int DifficultyLevel)
        {
            if (DifficultyLevel == 1)           //Easy Difficulty.
            {
                Hunger = 60;        //def 60
                Health = 4;         //def 4
                TurnsPerDay = 26;   //def 26
                FoodSup = 3;        //def 3
                BuildMat = 3;       //def 3
                CraftMat = 3;       //def 3
                ClothMat = 3;       //def 3
            }
            if (DifficultyLevel >= 2) {         //Normal and Hard Difficulties.
                Hunger = 50;        //def 50
                TurnsPerDay = 24;   //def 24
                Health = 3;         //def 3
                FoodSup = 0;        //def 0
                BuildMat = 0;       //def 0
                CraftMat = 0;       //def 0
                ClothMat = 0;       //def 0
                if (DifficultyLevel == 3)       //Hard Difficulty.
                {
                    Hunger = 40;
                    TurnsPerDay = 20;
                }
            }
            if (GodMode)
                Health = 9999;
            if (InfiniteHunger)
                Hunger = 9999;
            if (MaxArmor)
                Armor = 50;
            if (MaxHouseStatus)
                HouseStatus = 100;
            if (InfiniteItems)
            {
                FoodSup = 9999;
                BuildMat = 9999;
                CraftMat = 9999;
                ClothMat = 9999;
            }

            Armor = 0;              //def 0
            HouseStatus = 0;        //def 0
            Day = 0;                //def 0
            if (UpgradePoints) Experience = 9;
            else Experience = 0;    //def 0
            for (int i = 0; i < StrTree.Length; i++) { StrTree[i] = false; }    //resets the strength skill tree.
            for (int i = 0; i < IntTree.Length; i++) { IntTree[i] = false; }    //resets the intelligence skill tree.
            for (int i = 0; i < LckTree.Length; i++) { LckTree[i] = false; }    //resets the luck skill tree.

            HistoryText1 = null;
            HistoryText2 = null;
            HistoryText3 = null;
            HistoryText4 = null;
            HistoryText5 = null;
            Turns = TurnsPerDay + MoreTurns;
            GameMenu();
        }

        static void GameMenu()
        {
            if (MaxArmor)
                Armor = 50;
            if (MaxHouseStatus)
                HouseStatus = 100;

            if (Hunger <= 0)
            {
                if (Health <= 1)
                {
                    HistoryTextWrite("The player starved to death.");
                    PlayerDeath(2);
                } else
                {
                    HistoryTextWrite("The player almost starved to death. The player lost 1 health.");
                    Hunger = 20;
                    Health -= 1;
                }
            }
            Console.Clear();
            PlayerStatus();
            Console.WriteLine("\n\nHistory:\n");

            HistoryTextRead();

            HistoryTextWrite(null);

            GenerateRandomDrops(DroppedItems);

            Console.WriteLine("\n\n1." + DroppedItems[0] + "  2." + DroppedItems[1] + "  3." + DroppedItems[2] + "  4. Perform an action");
            Console.WriteLine("\n\n[Food Supplies] " + FoodSup + "\n[Building Material] " + BuildMat + "\n[Crafting Material] " + CraftMat + "\n[Cloth Material] " + ClothMat);

            if (Turns <= 0)
            {
                EndOfDay();
            }

            choice = 5;
            
            while (choice > 4 || choice < 0)
            {
                Console.WriteLine("Please enter your choice [1-4]: ");
                choiceStr = Console.ReadLine();

                bool choiceNull = string.IsNullOrEmpty(choiceStr);
                bool IsNumber = int.TryParse(choiceStr, out choice);

                if (!IsNumber || choiceNull) ErrorUserInput(1);
                if (!choiceNull) choice = int.Parse(choiceStr);
                if (choice > 4 || choice < 0)
                    ErrorUserInput(1);
                else
                {
                    if (choice == 1)
                        CurrentChosenItem = DroppedItems[0];
                    if (choice == 2)
                        CurrentChosenItem = DroppedItems[1];
                    if (choice == 3)
                        CurrentChosenItem = DroppedItems[2];
                    if (choice == 4)
                        ActionMenu();
                    if (choice == 0)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("Are you sure you want to return to main menu?");
                        Console.WriteLine("1. [Yes] \n2. [No]");
                        Console.WriteLine("-----------------------------");
                        string choiceStr2 = Console.ReadLine();
                        int choice2 = 3;
                        bool choiceNull2 = string.IsNullOrEmpty(choiceStr2);
                        bool IsNumber2 = int.TryParse(choiceStr2, out choice2);

                        if (!IsNumber2 || choiceNull2) ErrorUserInput(1);
                        if (!choiceNull2) choice2 = int.Parse(choiceStr2);
                        if (choice2 > 2 || choice2 < 1)
                            ErrorUserInput(1);
                        else
                        {
                            if (choice2 == 1)
                                MainMenu();
                            if (choice2 == 2)
                                GameMenu();
                        }
                    }
                 SelectedItem();
                }
            }
        }

        static void ErrorUserInput(int ToFunction)
        { //if you run into issues with ErrorUserInput or ErrorUserInput2 add the 2nd function that contains ActionMenu();
            Console.WriteLine("[Error] User Input Number Invalid, try again.");
            Console.ReadKey();
            if (ToFunction == 1)
                GameMenu();
            if (ToFunction == 2)
                ActionMenu();
            if (ToFunction == 3)
                UpgradeMenu();
            if (ToFunction == 4)
                MainMenu();
            if (ToFunction == 5)
                DifficultiesMenu();
            if (ToFunction == 6)
                SettingsMenu();
            if (ToFunction == 7)
                GuideMenu();
        }

        static void GenerateRandomDrops(List<string> DroppedItems)
        {
            if (choice < 4 || choice > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Random rd = new Random();
                    int random = rd.Next(0, 9);
                    if (random > 4) random = 4;
                    DroppedItems.Add(PossibleItems[random]);
                }
            }
        }

        static void SelectedItem()
        {
            if (CurrentChosenItem == "[Food Supplies]")
            {
                if (!IntTree[1])
                {
                    FoodSup += 1;
                    HistoryTextWrite("Found food supplies.");
                }
                if (IntTree[1])
                {
                    FoodSup += 2;
                    HistoryTextWrite("Found 2 food supplies.");
                }
                if (LckTree[0])
                {
                    int RandomDoubleDrop = new Random().Next(1, 11);
                    if (RandomDoubleDrop <= 3)
                    {
                        FoodSup += 1;
                        HistoryTextWrite("[Luck] Found more food supplies.");
                    }
                }
            }
            if (CurrentChosenItem == "[Building Material]")
            {
                BuildMat += 1;
                HistoryTextWrite("Found building materials.");
                if (LckTree[0])
                {
                    int RandomDoubleDrop = new Random().Next(1, 11);
                    if (RandomDoubleDrop <= 3)
                    {
                        BuildMat += 1;
                        HistoryTextWrite("[Luck] Found more building materials.");
                    }
                }
            }
            if (CurrentChosenItem == "[Crafting Material]")
            {
                CraftMat += 1;
                HistoryTextWrite("Found crafting materials.");
                if (LckTree[0])
                {
                    int RandomDoubleDrop = new Random().Next(1, 11);
                    if (RandomDoubleDrop <= 3)
                    {
                        CraftMat += 1;
                        HistoryTextWrite("[Luck] Found more crafting materials.");
                    }
                }
            }
            if (CurrentChosenItem == "[Cloth Material]")
            {
                ClothMat += 1;
                HistoryTextWrite("Found cloth material.");
                if (LckTree[0])
                {
                    int RandomDoubleDrop = new Random().Next(1, 11);
                    if (RandomDoubleDrop <= 3)
                    {
                        ClothMat += 1;
                        HistoryTextWrite("[Luck] Found more cloth material.");
                    }
                }
            }
            if (CurrentChosenItem == "[Junk Item]")
            {
                Random rd = new Random();
                int random = rd.Next(1, 101);
                if (random <= 40)
                {
                    if (!LckTree[2])
                        HistoryTextWrite("Couldn't find anything useful in the junk pile.");
                    else random += 40;
                }
                    if (random > 40 && random <= 55)
                {
                    if (!IntTree[1])
                    {
                        FoodSup += 1;
                        HistoryTextWrite("Found food supplies in the junk pile.");
                    }
                    if (IntTree[1])
                    {
                        FoodSup += 2;
                        HistoryTextWrite("Found 2 food supplies in the junk pile.");
                    }
                }
                if (random > 55 && random <= 70)
                {
                    BuildMat += 1;
                    HistoryTextWrite("Found leftover building materials in the junk pile.");
                }
                if (random > 70 && random <= 85)
                {
                    CraftMat += 1;
                    HistoryTextWrite("Salvaged crafting materials from the junk pile.");
                }
                if (random > 85 && random <= 96)
                {
                    ClothMat += 1;
                    HistoryTextWrite("Found dirty cloth material in the junk pile.");
                }
                if (random == 97)
                {
                    FoodSup += 10;
                    HistoryTextWrite("Found a food supply bundle in the junk pile.");
                }
                if (random == 98)
                {
                    BuildMat += 10;
                    HistoryTextWrite("Found a build materials bundle in the junk pile.");
                }
                if (random == 99)
                {
                    CraftMat += 10;
                    HistoryTextWrite("Salvaged a bundle of crafting materials from the junk pile.");
                }
                if (random == 100)
                {
                    ClothMat += 10;
                    HistoryTextWrite("Found a bundle of dirty cloth materials in the junk pile.");
                }
            }
            DroppedItems.Remove(DroppedItems[0]);
            DroppedItems.Remove(DroppedItems[0]);
            DroppedItems.Remove(DroppedItems[0]);
            Turns -= 1;
            Hunger -= 1;
            GameMenu();
        }

        static void PlayerStatus()
        {
            Console.WriteLine("\nHealth: " + Health + " Hunger: " + Hunger + "% Body Armor: " + Armor + "% House Status: " + HouseStatus + "%");
            Console.WriteLine("\nDays gone by: " + Day + " Turns left: " + Turns);
        }
        
        static void ActionMenu()
        {
            Console.Clear();
            PlayerStatus();
            Console.WriteLine("\n\n[In Action Menu] Choose one of the following actions [1-5]:\n");

            Console.WriteLine("\n1. [Eat food +5%] -1 food sup");
            if (!StrTree[2])
                Console.WriteLine("\n2. [Repair the house +7%] house status, -1 build mat, -1 craft mat");
            if (StrTree[2])
                Console.WriteLine("\n2. [Repair the house +14%] house status, -1 build mat, -1 craft mat");
            if (!IntTree[0])
                Console.WriteLine("\n3. [Craft more armor +2%] armor -1 cloth mat, -1 craft mat");
            if (IntTree[0])
                Console.WriteLine("\n3. [Craft more armor +3%] armor -1 cloth mat");
            Console.WriteLine("\n4. [Upgrade Perks]");
            Console.WriteLine("\n5. [Go Back]");
            Console.WriteLine("\n\n[Food Supplies] " + FoodSup + "\n[Building Material] " + BuildMat + "\n[Crafting Material] " + CraftMat + "\n[Cloth Material] " + ClothMat);

            choice = 6;

            while (choice > 5 || choice < 0)
            {
                Console.WriteLine("Please enter your action choice [1-5]: ");
                choiceStr = Console.ReadLine();

                bool choiceNull = string.IsNullOrEmpty(choiceStr);
                bool IsNumber = int.TryParse(choiceStr, out choice);

                if (!IsNumber || choiceNull) ErrorUserInput(2);
                if (!choiceNull) choice = int.Parse(choiceStr);
                if (choice > 5 || choice < 0)
                    ErrorUserInput(2);
                else
                {
                    if (choice == 1)
                    {
                        if (FoodSup > 0)
                        {
                            if (Hunger < 100)
                            {
                                Hunger += 5;
                                if (Hunger >= 100)
                                    Hunger = 100;
                                FoodSup -= 1;
                                Turns -= 1;
                                HistoryTextWrite("The player ate, +5% food.");
                            } else
                            {
                                HistoryTextWrite("The player is full at the moment.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough items to complete this action.");
                            Console.WriteLine("Requirements: 1 Food Supplies, you have: " + FoodSup);
                            Console.ReadKey();
                            ActionMenu();
                        }
                    }
                    if (choice == 2)
                    {
                        if (BuildMat > 0 && CraftMat > 0)
                        {
                            if (HouseStatus < 100)
                            {
                                if (Hunger >= 30)
                                {
                                    if (!StrTree[2])
                                        HouseStatus += 7;
                                    if (StrTree[2])
                                        HouseStatus += 14;
                                    if (HouseStatus >= 100)
                                        HouseStatus = 100;
                                    BuildMat -= 1;
                                    CraftMat -= 1;
                                    Turns -= 1;
                                    Hunger -= 3;
                                    if (!StrTree[2])
                                        HistoryTextWrite("The player repaired their house, +7% house status.");
                                    if (StrTree[2])
                                        HistoryTextWrite("The player repaired their house, +14% house status.");
                                } else
                                {
                                    HistoryTextWrite("The player is too hungry to repair their house.");
                                }
                            } else
                            {
                                HistoryTextWrite("The player's house is 100% built.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough items to complete this action.");
                            Console.WriteLine("Requirements: 1 Building Material, you have: " + BuildMat);
                            Console.WriteLine("Requirements: 1 Crafting Material, you have: " + CraftMat);
                            Console.ReadKey();
                            ActionMenu();
                        }
                    }
                    if (choice == 3)
                    {
                        if (!IntTree[0])
                        {
                            if (ClothMat > 0 && CraftMat > 0)
                            {
                                if (Armor < 50)
                                {
                                    if (Hunger >= 20)
                                    {
                                        Armor += 2;
                                        if (Armor >= 50)
                                            Armor = 50;
                                        ClothMat -= 1;
                                        CraftMat -= 1;
                                        Turns -= 1;
                                        Hunger -= 2;
                                        HistoryTextWrite("The player crafted more armor, +2% armor.");
                                    }
                                    else
                                    {
                                        HistoryTextWrite("The player is too hungry to craft more armor.");
                                    }
                                }
                                else
                                {
                                    HistoryTextWrite("The player's armor is maxed out.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("You do not have enough items to complete this action.");
                                Console.WriteLine("Requirements: 1 Cloth Material, you have: " + ClothMat);
                                Console.WriteLine("Requirements: 1 Crafting Material, you have: " + CraftMat);
                                Console.ReadKey();
                                ActionMenu();
                            }
                        }
                        if (IntTree[0])
                        {
                            if (ClothMat > 0)
                            {
                                if (Armor < 50)
                                {
                                    Armor += 3;
                                    if (Armor >= 50)
                                        Armor = 50;
                                    ClothMat -= 1;
                                    Turns -= 1;
                                    Hunger -= 1;
                                    HistoryTextWrite("The player crafted more armor, +3% armor.");
                                }
                                else
                                {
                                    HistoryTextWrite("The player's armor is maxed out.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("You do not have enough items to complete this action.");
                                Console.WriteLine("Requirements: 1 Cloth Material, you have: " + ClothMat);
                                Console.ReadKey();
                                ActionMenu();
                            }
                        }
                    }
                    if (choice == 4)
                    {
                        UpgradeMenu();
                    }
                    if (choice == 5)
                    {
                        GameMenu();
                    }
                    GameMenu();
                }
            }
        }

        static void UpgradeMenu()
        {
            Console.Clear(); //if you run inti problems here add {} after if and else
            Console.WriteLine("[In Perk Points Menu] You have: " + Experience + " perk points.");
            Console.WriteLine("Choose one of the next options [0-9]:");
            Console.WriteLine("\n[Strength skill tree]");
            if (!StrTree[0]) //STRENGTH
            Console.WriteLine("1. [Healthy Player] +1 health point.");
            else
            Console.WriteLine("1. [Upgraded] [Healthy Player] +1 health point.");
            if (!StrTree[1])
            Console.WriteLine("2. [Fighter] fight off every small enemy successfully.");
            else
            Console.WriteLine("2. [Upgraded] [Fighter] fight off every small enemy successfully.");
            if (!StrTree[2])
            Console.WriteLine("3. [Builder] x2 house status build.");
            else
            Console.WriteLine("3. [Upgraded] [Builder] x2 house status build.");

            Console.WriteLine("\n[Intelligence skill tree]"); //INTELLIGENCE
            if (!IntTree[0])
            Console.WriteLine("4. [Armorer] cheaper to craft armor and craft +1 armor.");
            else
            Console.WriteLine("4. [Upgraded] [Armorer] cheaper to craft armor and craft +1 armor.");
            if (!IntTree[1])
            Console.WriteLine("5. [Double Meal] find 2x food supplies.");
            else
            Console.WriteLine("5. [Upgraded] [Double Meal] find 2x food supplies.");
            if (!IntTree[2])
            Console.WriteLine("6. [One More Chance] +4 turns per day.");
            else
            Console.WriteLine("6. [Upgraded] [One More Chance] +4 turns per day.");

            Console.WriteLine("\n[Luck skill tree]"); //LUCK
            if (!LckTree[0])
            Console.WriteLine("7. [Lucky Pick] 30% to get the same item twice (doesn't apply to junk).");
            else
            Console.WriteLine("7. [Upgraded] [Lucky Pick] 30% to get the same item twice (doesn't apply to junk).");
            if (!LckTree[1])
            Console.WriteLine("8. [Peaceful Night] +25% for an enemy to not find you.");
            else
            Console.WriteLine("8. [Upgraded] [Peaceful Night] +25% for an enemy to not find you.");
            if (!LckTree[2])
            Console.WriteLine("9. [No More Waste] always get something out of a junk pile.");
            else
            Console.WriteLine("9. [Upgraded] [No More Waste] always get something out of a junk pile.");

            Console.WriteLine("\n\n0. [Go Back]");
            
            choice = 10;
            while (choice > 9 || choice < 0) //if u run into issues with this menu: def is (choice > 9 || choice < -1)
            {
                Console.WriteLine("Please enter your action choice [0-9]: ");
                choiceStr = Console.ReadLine();

                bool choiceNull = string.IsNullOrEmpty(choiceStr);
                bool IsNumber = int.TryParse(choiceStr, out choice);
                if (!IsNumber || choiceNull) ErrorUserInput(3);
                if (!choiceNull) choice = int.Parse(choiceStr);
                if (choice > 9 || choice < 0)
                    ErrorUserInput(3);
                else
                {
                    if (choice == 1)
                    {
                        if (!StrTree[0])
                        {
                            if (Experience > 0)
                            {
                                StrTree[0] = true;
                                Experience -= 1;
                                Health += 1;
                                HistoryTextWrite("The Player has upgraded the Healthy Player perk.");
                                UpgradeMenu();
                            } else
                            {
                                ErrorSkill(1);
                            }
                        } else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 2)
                    {
                        if (!StrTree[1])
                        {
                            if (Experience > 0)
                            {
                                StrTree[1] = true;
                                Experience -= 1;
                                HistoryTextWrite("The Player has upgraded the Fighter perk.");
                                UpgradeMenu();
                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 3)
                    {
                        if (!StrTree[2])
                        {
                            if (Experience > 0)
                            {
                                StrTree[2] = true;
                                Experience -= 1;
                                HistoryTextWrite("The Player has upgraded the Builder perk.");
                                UpgradeMenu();

                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 4)
                    {
                        if (!IntTree[0])
                        {
                            if (Experience > 0)
                            {
                                IntTree[0] = true;
                                Experience -= 1;
                                HistoryTextWrite("The Player has upgraded the Armorer perk.");
                                UpgradeMenu();
                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 5)
                    {
                        if (!IntTree[1])
                        {
                            if (Experience > 0)
                            {
                                IntTree[1] = true;
                                Experience -= 1;
                                HistoryTextWrite("The Player has upgraded the Double Meal perk.");
                                UpgradeMenu();
                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 6)
                    {
                        if (!IntTree[2])
                        {
                            if (Experience > 0)
                            {
                                IntTree[2] = true;
                                Experience -= 1;
                                Turns += 4;
                                TurnsPerDay += 4;
                                HistoryTextWrite("The Player has upgraded the One More Chance perk.");
                                UpgradeMenu();
                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 7)
                    {
                        if (!LckTree[0])
                        {
                            if (Experience > 0)
                            {
                                LckTree[0] = true;
                                Experience -= 1;
                                HistoryTextWrite("The Player has upgraded the Lucky Pick perk.");
                                UpgradeMenu();
                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 8)
                    {
                        if (!LckTree[1])
                        {
                            if (Experience > 0)
                            {
                                LckTree[1] = true;
                                Experience -= 1;
                                HistoryTextWrite("The Player has upgraded the Peaceful Night perk.");
                                UpgradeMenu();
                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 9)
                    {
                        if (!LckTree[2])
                        {
                            if (Experience > 0)
                            {
                                LckTree[2] = true;
                                Experience -= 1;
                                HistoryTextWrite("The Player has upgraded the No More Waste perk.");
                                UpgradeMenu();
                            }
                            else
                            {
                                ErrorSkill(1);
                            }
                        }
                        else
                        {
                            ErrorSkill(2);
                        }
                    }
                    if (choice == 0)
                    {
                        ActionMenu();
                    }
                }
            }
            Console.ReadKey();
            ActionMenu();
        }

        static void ErrorSkill(int SkillState)
        {
            if (SkillState == 1)
            {
                Console.WriteLine("[ERROR] You do not have enough skill points to upgrade this perk.");
            }
            if (SkillState == 2)
            {
                Console.WriteLine("[ERROR] This perk is already upgraded.");
            }
            Console.WriteLine("Please press any key to continue.");
            Console.ReadKey();
            UpgradeMenu();
        }

        static void PlayerDeath(int DeathState)
        {
            Console.Clear();
            if (DeathState == 1)
                Console.WriteLine("\n[Dead] You have ran out of lives.");
            if (DeathState == 2)
                Console.WriteLine("\n[Dead] You have starved to death.");
            if (DeathState == 3)
                Console.WriteLine("\n[Dead] You have starved to death in your sleep.");
            if (DeathState == 4)
                Console.WriteLine("\n[Dead] You have been killed by the vicious Wolves.");
            if (DeathState == 5)
                Console.WriteLine("\n[Dead] You have been killed by the enraged Goblins.");
            if (DeathState == 6)
                Console.WriteLine("\n[Dead] You have been killed by a hungry Bear.");
            if (DeathState == 7)
                Console.WriteLine("\n[Dead] You have been killed by a group of Bandits.");
            if (DeathState == 8)
                Console.WriteLine("\n[Dead] You have been killed by a Mysterious Creature.");
            if (DeathState == 9)
                Console.WriteLine("\n[Dead] You have been killed by the vicious Cultists.");
            if (DeathState == 10)
            {
                Console.WriteLine("\n[Bad Ending]");
                Console.WriteLine("[Dead] You have been slayed by the legendary Dragon.");
            }
            if (DeathState == 11)
            {
                Console.WriteLine("\n[Good Ending]");
                Console.WriteLine("You have slayed the legendary Dragon!");
                Console.WriteLine("You have become the new king of the island!");
            }
            if (DeathState == 12)
            {
                Console.WriteLine("\n[Neutral Ending]");
                Console.WriteLine("Both you and the legendary Dragon live to fight another day!");
                Console.WriteLine("Everyday you prepare for your next encounter with the Dragon!");
            }
            Console.WriteLine("\n\nHistory:");
            HistoryTextRead();
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            MainMenu();
        }

        static void HistoryTextWrite(string HistoryText6)
        {
            if (HistoryText6 != null)
            {
                HistoryText1 = HistoryText2;
                HistoryText2 = HistoryText3;
                HistoryText3 = HistoryText4;
                HistoryText4 = HistoryText5;
                HistoryText5 = HistoryText6;
                HistoryText6 = null;
            }
        }

        static void HistoryTextRead()
        {
            if (HistoryText1 == null) //if you run into issues with this function, add {} after if and else
                Console.WriteLine(">");
            else
                Console.WriteLine(">" + HistoryText1);
            if (HistoryText2 == null)
                Console.WriteLine(">");
            else
                Console.WriteLine(">" + HistoryText2);
            if (HistoryText3 == null)
                Console.WriteLine(">");
            else
                Console.WriteLine(">" + HistoryText3);
            if (HistoryText4 == null)
                Console.WriteLine(">");
            else
                Console.WriteLine(">" + HistoryText4);
            if (HistoryText5 == null)
                Console.WriteLine(">");
            else
                Console.WriteLine(">" + HistoryText5);
        }

        static void FightEnemy(string EnemyName, string FullEnemyName, int DeathReason)
        {
            int randomSurvivability = new Random().Next(1, 101);
            if (Armor > 1 && randomSurvivability <= Armor)
            {
                HistoryTextWrite("The player defended himself against the " + EnemyName + ".");
                HistoryTextWrite("The player's armor was torn and the " + EnemyName + " ran away.");
                Armor = 0;
                Console.ReadKey();
                return;
            }
            HistoryTextWrite("The player couldn't defend himself and is badly injured");
            if (Health > 1)
            {
                HistoryTextWrite("The player lost 1 health and the " + EnemyName + " left.");
                Health -= 1;
                Console.ReadKey();
                return;
            }
            if (Health <= 1)
            {
                HistoryTextWrite("The " + FullEnemyName + " killed the player.");
                Console.ReadKey();
                PlayerDeath(DeathReason);
            }
        }

        static void EndOfDay()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("[1 day has gone by!]");
            Console.WriteLine("+1 perk point");
            Console.WriteLine("Please press any key to continue.");
            Console.WriteLine("-----------------------------");

            int RandomEnemy = 0;
            if (Day <= 2)
            {
                RandomEnemy = new Random().Next(1, 4); //def (1, 4).
            }
            if (Day > 2 && Day < 7)
            {
                RandomEnemy = new Random().Next(2, 8); //def (2, 8).
            }
            if (Day > 6)
                BossFight(1);

            int RandomEnemySkip = 100;
            if (LckTree[1])
            {
                RandomEnemySkip = new Random().Next(1, 101);
            }
            if (RandomEnemySkip > 25)
            {
                if (RandomEnemy == 1) //Small Enemies.[cultists, rats, goblins], wolves, bears, bandits, mysterious creature
                {
                    HistoryTextWrite("The player was attacked by Cultists.");
                    int random = new Random().Next(1, 4); //def RNG (1,4)
                    if (random == 1)
                    {
                        HistoryTextWrite("The Cultists scouted the player's house.");
                        HistoryTextWrite("The Cultists couldn't spot anything useful and left.");
                        Console.ReadKey();
                    }
                    if (random == 2)
                    {
                        if (BuildMat > 1)
                        {
                            HistoryTextWrite("The Cultists scouted the player's house.");
                            int randomAct2 = 101;
                            if (HouseStatus > 1)
                            {
                                randomAct2 = new Random().Next(1, 101);

                                if (randomAct2 <= HouseStatus)
                                {
                                    HistoryTextWrite("The Cultists couldn't find the player's building material.");
                                }
                                else
                                {
                                    HistoryTextWrite("The Cultists took 2 building materials from the player. -2 build mat");
                                    BuildMat -= 2;
                                }
                            }
                            else
                            {
                                HistoryTextWrite("The Cultists took 2 building materials from the player. -2 build mat");
                                BuildMat -= 2;
                            }
                            HistoryTextWrite("The Cultists didn't find the player and left.");
                            Console.ReadKey();
                        }
                        else
                        {
                            HistoryTextWrite("The Cultists scouted the player's house.");
                            HistoryTextWrite("The Cultists couldn't find the player's building material.");
                            HistoryTextWrite("The Cultists didn't find the player and left.");
                            Console.ReadKey();
                        }
                    }
                    if (random == 3 && StrTree[1])
                    {
                        HistoryTextWrite("The Cultists spotted and attacked the player.");
                        HistoryTextWrite("[Fighter]The Player fought the Cultists off successfully.");
                        Console.ReadKey();
                    }
                    if (random == 3 && !StrTree[1])
                    {
                        HistoryTextWrite("The Cultists spotted and attacked the player.");
                        if (Armor > 19)
                        {
                            HistoryTextWrite("The Player defended himself against the Cultists successfully.");
                            HistoryTextWrite("The Cultists weakend the Player's armor and left.");
                            Armor -= 20;
                            Console.ReadKey();
                        }
                        else
                            FightEnemy("Cultists", "vicious Cultists", 9);
                    }
                }

                if (RandomEnemy == 2) //wolves
                {
                    HistoryTextWrite("The player was attacked by Wolves.");
                    int randomAct = new Random().Next(1, 3); //def (1,3)
                    if (randomAct == 1)
                    {
                        HistoryTextWrite("The Wolves scouted the player's house.");
                        HistoryTextWrite("The Wolves lost interest and left.");
                        Console.ReadKey();
                    }
                    if (randomAct == 2)
                    {
                        if (FoodSup > 2)
                        {
                            int randomAct2 = 101;
                            if (HouseStatus > 1)
                            {
                                randomAct2 = new Random().Next(1, 101);
                            }
                            if (randomAct2 <= HouseStatus)
                            {
                                HistoryTextWrite("The Wolves couldn't find the player's food supplies.");
                                HistoryTextWrite("But the Wolves found the player and attacked.");
                            }
                            else
                            {
                                HistoryTextWrite("The Wolves found the player's food supplies. -3 food");
                                HistoryTextWrite("The Wolves found the player and attacked.");
                                FoodSup -= 3;
                            }
                        }
                        else
                        {
                            HistoryTextWrite("The Wolves found the player and attacked.");
                        }
                        FightEnemy("Wolves", "vicious Wolves", 4);
                    }
                }

                if (RandomEnemy == 3) //rats
                {
                    HistoryTextWrite("The player was attacked by Rats.");
                    int randomAct = 1;
                    if (FoodSup > 5)
                    {
                        randomAct = new Random().Next(1, 3);
                    }
                    if (randomAct == 1)
                    {
                        HistoryTextWrite("The Rats ran around the player's house.");
                        HistoryTextWrite("The Rats couldn't find any food supplies and left.");
                        Console.ReadKey();
                    }
                    if (randomAct == 2 && StrTree[1])
                    {
                        HistoryTextWrite("The Rats ran around the player's house.");
                        HistoryTextWrite("[Fighter] The Player scared the Rats away.");
                        Console.ReadKey();
                    }
                    if (randomAct == 2 && !StrTree[1])
                    {
                        int randomAct2 = 101;
                        if (HouseStatus > 1)
                        {
                            randomAct2 = new Random().Next(1, 101);
                        }
                        else
                        {
                            HistoryTextWrite("The Rats ran around the player's house.");
                            HistoryTextWrite("The Rats found the player's food supplies. - 6 food");
                            HistoryTextWrite("The Rats left.");
                            FoodSup -= 6;
                            Console.ReadKey();
                        }
                        if (randomAct2 <= HouseStatus)
                        {
                            HistoryTextWrite("The Rats ran around the player's house.");
                            HistoryTextWrite("The Rats couldn't reach any food supplies.");
                            HistoryTextWrite("The Rats got frustrated because of the house's design and left.");
                            Console.ReadKey();
                        }
                        else
                        {
                            HistoryTextWrite("The Rats ran around the player's house.");
                            HistoryTextWrite("The Rats found the player's food supplies. - 6 food");
                            HistoryTextWrite("The Rats left.");
                            FoodSup -= 6;
                            Console.ReadKey();
                        }
                    }
                }

                if (RandomEnemy == 4) //goblins
                {
                    HistoryTextWrite("The player was attacked by Goblins.");
                    int randomAct = new Random().Next(1, 11);
                    if (randomAct < 3)
                    {
                        HistoryTextWrite("The Goblins couldn't find the player's house and left.");
                        Console.ReadKey();
                    }
                    if (randomAct > 2 && randomAct < 7)
                    {
                        int randomAct2 = new Random().Next(1, 101);
                        if (HouseStatus > 1)
                        {
                            randomAct = 1;
                        }
                        else if (FoodSup > 0 && BuildMat > 0 && CraftMat > 0)
                        {
                            HistoryTextWrite("The Goblins stole some items from the player.");
                            HistoryTextWrite("The Goblins left. - 1 food, - 1 build mat, - 1 craft mat");
                            FoodSup -= 1;
                            BuildMat -= 1;
                            CraftMat -= 1;
                            Console.ReadKey();
                        }
                        else
                            randomAct = 10;
                    }
                    if (randomAct > 6 && StrTree[1])
                    {
                        HistoryTextWrite("The enraged Goblins found the player and attacked.");
                        HistoryTextWrite("[Fighter]The Player fought the Goblins off successfully.");
                        Console.ReadKey();
                    }
                    if (randomAct > 6 && !StrTree[1])
                    {
                        HistoryTextWrite("The enraged Goblins found the player and attacked.");
                        FightEnemy("Goblins", "enraged Goblins", 5);
                    }
                }

                if (RandomEnemy == 5) //bear
                {
                    HistoryTextWrite("The player was attacked by a hungry Bear.");
                    int randomAct = new Random().Next(1, 5); //def is (1, 5)
                    if (randomAct == 1)
                    {
                        HistoryTextWrite("The Bear lost interest in the player's house and left.");
                        Console.ReadKey();
                    } else
                    {
                        HistoryTextWrite("The Bear spotted the player and attacked.");
                        FightEnemy("Bear", "hungry Bear", 6);
                    }
                }

                if (RandomEnemy == 6) //Bandits
                {
                    HistoryTextWrite("The player was attacked by a group of Bandits.");
                    int randomAct;
                    if (HouseStatus < 50)
                        randomAct = new Random().Next(1, 6); //def (1,6)
                    else
                        randomAct = 6;
                    if (randomAct < 4)
                    {
                        HistoryTextWrite("The Bandits found the player's house.");
                        string ReportStolenGoodsEnd = null;
                        if (FoodSup > 3)
                        {
                            ReportStolenGoodsEnd = ReportStolenGoodsEnd + " -3 food";
                            FoodSup -= 3;
                        }
                        if (CraftMat > 3)
                        {
                            if (ReportStolenGoodsEnd == null)
                                ReportStolenGoodsEnd = ReportStolenGoodsEnd + " -3 craft mat";
                            else
                                ReportStolenGoodsEnd = ReportStolenGoodsEnd + ", -3 craft mat";
                            CraftMat -= 3;
                        }
                        if (BuildMat > 3)
                        {
                            if (ReportStolenGoodsEnd == null)
                                ReportStolenGoodsEnd = ReportStolenGoodsEnd + " -3 build mat";
                            else
                                ReportStolenGoodsEnd = ReportStolenGoodsEnd + ", -3 build mat";
                            BuildMat -= 3;
                        }
                        if (ClothMat > 3)
                        {
                            if (ReportStolenGoodsEnd == null)
                                ReportStolenGoodsEnd = ReportStolenGoodsEnd + " -3 cloth mat";
                            else
                                ReportStolenGoodsEnd = ReportStolenGoodsEnd + ", -3 cloth mat";
                            ClothMat -= 3;
                        }
                        if (ReportStolenGoodsEnd != null)
                        {
                            HistoryTextWrite("The Bandits stole some items." + ReportStolenGoodsEnd);
                            HistoryTextWrite("The Bandits left.");
                        } else
                            HistoryTextWrite("The Bandits couldn't find anything in the player's house and left.");
                        Console.ReadKey();
                    }

                    if (randomAct > 3 && randomAct < 6)
                    {
                        HistoryTextWrite("The Bandits found the player and attacked.");
                        FightEnemy("Bandits", "group of Bandits", 7);
                        
                    }

                    if (randomAct == 6)
                    {
                        HistoryTextWrite("The Bandits couldn't break into the player's house.");
                        HistoryTextWrite("The Bandits left. -10 house status");
                        HouseStatus -= 10;
                    }
                }

                if (RandomEnemy == 7) //Mysterious Creature
                {
                    HistoryTextWrite("The player was attacked by a Mysterious Creature.");
                    int randomAct = new Random().Next(1, 10);
                    if (randomAct > 0 && randomAct < 5)
                    {
                        int randomAct2 = new Random().Next(1, 101);
                        if (HouseStatus >= 101)
                        {
                            HistoryTextWrite("The Mysterious Creature left.");
                        }
                        else
                        {
                            if (CraftMat >= 5 || BuildMat >= 5)
                            {
                                string ReportStolenGoodsEnd = null;
                                if (CraftMat >= 5)
                                {
                                    ReportStolenGoodsEnd = ReportStolenGoodsEnd + " -5 craft mat";
                                    CraftMat -= 5;
                                }
                                if (BuildMat >= 5)
                                {
                                    if (ReportStolenGoodsEnd == null)
                                        ReportStolenGoodsEnd = ReportStolenGoodsEnd + " -5 build mat";
                                    else
                                        ReportStolenGoodsEnd = ReportStolenGoodsEnd + ", -5 build mat";
                                    BuildMat -= 5;
                                }
                                HistoryTextWrite("The Mysterious Creature took some items." + ReportStolenGoodsEnd);
                                HistoryTextWrite("The Mysterious Creature left.");
                            }
                            else
                                HistoryTextWrite("The Mysterious Creature left.");
                            Console.ReadKey();
                        }
                    }
                    if (randomAct > 4 && randomAct < 9)
                    {
                        HistoryTextWrite("The Mysterious Creature decided to attack the player.");
                        FightEnemy("Mysterious Creature", "Mysterious Creature", 8);
                    }
                    if (randomAct > 8 && randomAct < 11)
                    {
                        HistoryTextWrite("The Mysterious Creature decided not to attack the player.");
                        HistoryTextWrite("The Mysterious Creature left.");
                    }
                }
            }
            else
            {
                HistoryTextWrite("[Luck] The player was not attacked, and got a peaceful night's sleep.");
                Console.ReadKey();
            }

            Hunger -= 20;
            if (Hunger <= 0)
            {
                if (Health <= 1)
                {
                    HistoryTextWrite("The player starved to death.");
                    PlayerDeath(3);
                }
                else
                {
                    HistoryTextWrite("The player almost starved to death. The player lost 1 health.");
                    Hunger = 20;
                    Health -= 1;
                }
            }
            Experience += 1;
            Day += 1;
            Turns = TurnsPerDay + MoreTurns;
            GameMenu();
        }

        static void BossFight(int BossType)
        {
            int BossHealth = 3;
            if (BossType == 1)
            {
                HistoryTextWrite("You are being attacked by a legendary Dragon!");
                HistoryTextWrite("The Dragon has 3 attacks left!");
                DrawBossFightUI(0);

                int randomAct2 = new Random().Next(1, 101);
                if (HouseStatus >= randomAct2)
                {
                    HistoryTextWrite("The Dragon couldn't get through your house easily!");
                    HistoryTextWrite("The Dragon has 2 attacks left! and its health decreased");
                    BossHealth -= 1;
                    HouseStatus = 0;
                }
                else
                {
                    HistoryTextWrite("The Dragon got through your house easily!");
                    HistoryTextWrite("The Dragon burned you! and you lost 1 health!");
                    HistoryTextWrite("The Dragon has 2 attacks left!");
                    Health -= 1;
                }
                DrawBossFightUI(0);

                int randomAct3 = new Random().Next(1, 101);
                if (Armor >= randomAct3)
                {
                    HistoryTextWrite("Your armor protected you against the Dragon!");
                    HistoryTextWrite("The Dragon has 1 attack left! and its health decreased");
                    BossHealth -= 1;
                    Armor = 0;
                }
                else
                {
                    HistoryTextWrite("Your armor couldn't defend you!");
                    HistoryTextWrite("The Dragon slashed you! and you lost 1 health!");
                    HistoryTextWrite("The Dragon has 1 attack left!");
                    Health -= 1;
                }
                DrawBossFightUI(0);

                HistoryTextWrite("The Dragon is exhausted of its energy!");
                int randomAct4 = new Random().Next(1, 101);
                if (50 >= randomAct4 && !StrTree[0] && !StrTree[1] && !StrTree[2])
                {
                    HistoryTextWrite("You had no means of defence!");
                    HistoryTextWrite("The Dragon attacked you with its wing! and you lost 1 health!");
                    Health -= 1;
                    DrawBossFightUI(0);
                }
                if (50 < randomAct4 || StrTree[0] && StrTree[1] && StrTree[2])
                {
                    HistoryTextWrite("You were able to defend yourself against the Dragon!");
                    if (BossHealth <= 1)
                    {
                        HistoryTextWrite("You killed the legendary Dragon!"); //[V]good ending
                        DrawBossFightUI(1);
                    }
                }
                HistoryTextWrite("The legendary Dragon was just barely able to escape!"); //[V]neutral ending
                DrawBossFightUI(2);
            }
        }

        static void DrawBossFightUI(int EndingState)
        {
            Console.Clear();
            PlayerStatus();
            Console.WriteLine("\n");
            HistoryTextRead();
            Console.WriteLine("\n\nPlease press any key to continue.");
            Console.ReadKey();
            if (Health < 1)
                PlayerDeath(10); //[V]bad ending
            else
            {
                if (EndingState == 1)
                    PlayerDeath(11);
                if (EndingState == 2)
                    PlayerDeath(12);
            }
            if (EndingState == 0)
                return;
        }
    }
}

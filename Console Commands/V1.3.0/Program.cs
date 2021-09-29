using System.Collections.Generic;
using System.Management; //you'll need to install system.management through 'NuGet' (you may also need system.management.automation with it).
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System;

namespace Console_Commands
{
    class Program
    {
        static List<string> Words = new List<string>();
        static String userInput;
        static ConsoleColor textColor = ConsoleColor.White;
        static ConsoleColor backgroundColor = ConsoleColor.Black;
        static ConsoleColor temp_textColor = textColor;
        static ConsoleColor temp_backgroundColor = backgroundColor;
        static float RAM_total = 0;
        static bool writtenRAMspeed = false;
        static string currentPath = "";
        static DateTime dt = DateTime.Now;

        static void Main()
        {
            Console.Title = "Command Console V1.3.0";
            Console.WriteLine("type 'help' for more information.");
            Console.WriteLine("Enter a command:");
            CommandsFunc();
        }
        static void CommandsFunc()
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = textColor;
            Console.Write(">>$ ");

            Words.Clear();                      //resets the 'Words' list. This system reads each word from the user's input and stores them in a list.
            userInput = Console.ReadLine();
            ProcessWords(userInput);

            if (Words.Count <= 0)
            {
                Console.WriteLine("[Error!] Please enter a command. Write 'help' for more information.");
                Pause();
            }

            if (Words[0].ToLower() == "help")       //This is an example of a command, we check if the first word is 'help'.
            {
                bool outPut_error = true;
                if (Words.Count == 1)
                {
                    Console.WriteLine("help          :    displays this message.                # help <command> - displays more information.");
                    Console.WriteLine("flags         :    displays an explanation about flags.  #");
                    Console.WriteLine("say           :    prints your message.                  #");
                    Console.WriteLine("time/date     :    prints time and date.                 #");
                    Console.WriteLine("exit/quit     :    exits the program.                    #");
                    Console.WriteLine("neofetch      :    prints more information about system. #");
                    Console.WriteLine("color         :    changes the colors of the console.    #");
                    Console.WriteLine("clear/cls     :    clears the console.                   #");
                    Console.WriteLine("path/cd       :    displays current path.                #");
                    Console.WriteLine("task/tl       :    displays running tasks                #");
                    Console.WriteLine("terminate/kill:    terminates chosen task.               #");
                    Console.WriteLine("echo          :    prints your message with properties   #");
                    Console.WriteLine("filecreate/fc :    creates a file in current directory   #");
                    Console.WriteLine("filedelete/fd :    deletes the file in current directory #");
                    Console.WriteLine("filerename/fr :    renames the file in current directory #");
                }
                if (Words.Count > 1)            //Checks if the user wrote more than one word, if it matches it displays the following:
                {
                    if (Words[1] == "date" || Words[1] == "time")
                    {
                        Console.WriteLine("time - prints time and date.");
                        Console.WriteLine("date - prints time and date.");
                        Console.WriteLine("Special flags:");
                        Console.WriteLine(" -format      :   displays the time and date format.");
                        Console.WriteLine(" -time        :   displays only the time.");
                        Console.WriteLine(" -date        :   displays only the date.");
                        outPut_error = false;
                    }

                    if (Words[1] == "help")
                    {
                        Console.WriteLine("help - displays this message.");
                        Console.WriteLine("help <command> - displays more information about the written command.");
                        Console.WriteLine("\nExample:\nhelp time        :   would display more information about time command.");
                        outPut_error = false;
                    }

                    if (Words[1] == "flags" || Words[1] == "flag")
                    {
                        Console.WriteLine("Flags are custom properties which you can apply to some commands.");
                        Console.WriteLine("Flags usually display more information or allow different uses for commands.");
                        Console.WriteLine("\nExample:\ntime -format        :   would display more information about time and date format.");
                        outPut_error = false;
                    }

                    if (Words[1] == "neofetch")
                    {
                        Console.WriteLine("neofetch - prints hardware information and program icon.");
                        Console.WriteLine("Special flags:");
                        Console.WriteLine(" -icon        :   displays only the icon.");
                        outPut_error = false;
                    }

                    if (Words[1] == "color")
                    {
                        Console.WriteLine("color - displays current colors and list of other colors.");
                        Console.WriteLine("Special flags:");
                        Console.WriteLine(" -text:x        :   changes the color of text to your choice.");
                        Console.WriteLine(" -board:x       :   changes the color of background to your choice.");
                        Console.WriteLine(" -reset         :   resets the colors back to default values.");
                        Console.WriteLine("\nExample:\ncolor -text:blue -board:darkgreen");
                        outPut_error = false;
                    }

                    if (Words[1] == "clear" || Words[1] == "cls")
                    {
                        Console.WriteLine("clear - clears the console");
                        Console.WriteLine("cls - clears the console");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }

                    if (Words[1] == "say")
                    {
                        Console.WriteLine("say - prints your message.");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }

                    if (Words[1] == "exit" || Words[1] == "quit")
                    {
                        Console.WriteLine("exit - exits the program.");
                        Console.WriteLine("quit - exits the program.");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }

                    if (Words[1] == "path" || Words[1] == "cd")
                    {
                        Console.WriteLine("path - displays current directory, and available folders/files in the same path.");
                        Console.WriteLine("cd -   displays current directory, and available folders/files in the same path.");
                        Console.WriteLine("Special properties:");
                        Console.WriteLine("\n*To change the directory write a full path or an available folder's name.");
                        Console.WriteLine(" Example:\n cd folder \n cd C:\\folder");
                        Console.WriteLine("\n*To go back one folder write '..'.");
                        Console.WriteLine(" Example:\n cd ..");
                        Console.WriteLine("\n*To access a .exe write '/' before it.");
                        Console.WriteLine(" Example:\n cd /program.exe");
                        Console.WriteLine("\n*To list all available disks write '/dl' or '/drives'.");
                        Console.WriteLine(" Example:\n cd /dl");
                        outPut_error = false;
                    }

                    if (Words[1] == "terminate" || Words[1] == "kill")
                    {
                        Console.WriteLine("terminate - terminates the chosen task.");
                        Console.WriteLine("kill - terminates the chosen task.");
                        Console.WriteLine("\n Example:\n terminate <taskID>\n");
                        Console.WriteLine("to list all available tasks write 'tasks'.");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }
                    if (Words[1] == "task" || Words[1] == "tasks" || Words[1] == "tl")
                    {
                        Console.WriteLine("tasks - lists all available tasks.");
                        Console.WriteLine("task - lists all available tasks.");
                        Console.WriteLine("tl - lists all available tasks.");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }
                    if (Words[1] == "echo")
                    {
                        Console.WriteLine("echo - prints your message with special properties.");
                        Console.WriteLine("Special properties:");
                        Console.WriteLine("\n*To change text color write {color}.");
                        Console.WriteLine(" Example:\n echo {red}Hello World");
                        Console.WriteLine("\n*To change board color write {textColor:boardColor}.");
                        Console.WriteLine(" Example:\n echo {red:white}Hello World");
                        Console.WriteLine("\n*To reset the colors write {reset}.");
                        Console.WriteLine(" Example:\n echo {red}Hello {reset}World");
                        outPut_error = false;
                    }
                    if (Words[1] == "filecreate" || Words[1] == "fc")
                    {
                        Console.WriteLine("filecreate - creates a file of your choosing in the current directory.");
                        Console.WriteLine("fc - creates a file of your choosing in the current directory.");
                        Console.WriteLine("\n Example:\n filecreate fileName.txt\n");
                        Console.WriteLine("to print the current directory write 'path'.");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }
                    if (Words[1] == "filedelete" || Words[1] == "fd")
                    {
                        Console.WriteLine("filedelete - deletes a file of your choosing in the current directory.");
                        Console.WriteLine("fd - deletes a file of your choosing in the current directory.");
                        Console.WriteLine("\n Example:\n filedelete fileName.txt\n");
                        Console.WriteLine("to print the current directory write 'path'.");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }
                    if (Words[1] == "filerename" || Words[1] == "fr")
                    {
                        Console.WriteLine("filerename - renames a file of your choosing in the current directory.");
                        Console.WriteLine("fr - renames a file of your choosing in the current directory.");
                        Console.WriteLine("in the command itself you choose what file to rename and hit enter.");
                        Console.WriteLine("then you'll be asked rename to what, there you can enter the new name.");
                        Console.WriteLine("\n Example:\n filerename fileName.txt (enter)\n fileNewName.txt");
                        Console.WriteLine("to print the current directory write 'path'.");
                        Console.WriteLine("No special flags available");
                        outPut_error = false;
                    }
                    if (outPut_error)
                        Console.WriteLine("[Error!] didn't recognize \"" + Words[1] + "\" please write 'help'.");
                }

                Pause();
            }                                   //help command

            if (Words[0].ToLower() == "say")
            {
                if (Words.Count > 1)
                {
                    int tempIndex = userInput.IndexOf(Words[0]) + 4;
                    Console.WriteLine(">" + userInput.Substring(tempIndex));
                }
                else
                    Console.WriteLine("> ");
                Pause();
            }                                    //say command

            if (Words[0].ToLower() == "exit" || Words[0].ToLower() == "quit")
            {
                Console.WriteLine("quitting...");
                Environment.Exit(1);
            }   //exit.quit command

            if (Words[0].ToLower() == "time" || Words[0].ToLower() == "date")
            {
                //Checks and activates any flags that were written into the command.
                //Example:    time -format -time -date
                bool flag_format = false;
                bool flag_onlyTime = false;
                bool flag_onlyDate = false;
                for (int i = 1; i < Words.Count; i++)
                {
                    if (Words[i].ToLower() == "-format")
                        flag_format = true;
                    if (Words[i].ToLower() == "-time")
                        flag_onlyTime = true;
                    if (Words[i].ToLower() == "-date")
                        flag_onlyDate = true;
                }

                if (flag_onlyTime || !flag_onlyDate)
                    Console.WriteLine("Time: " + dt.ToString("HH:mm") + "." + dt.ToString("ss"));
                if (flag_onlyDate || !flag_onlyTime)
                    Console.WriteLine("Date: " + dt.ToString("dd/MM/yyyy"));
                if (flag_format)
                {
                    Console.WriteLine("Time format: HH:mm.ss");
                    Console.WriteLine("Date format: dd/MM/yyyy");
                }
                Pause();
            }   //time.date command

            if (Words[0].ToLower() == "flag" || Words[0].ToLower() == "flags")
            {
                Console.WriteLine("Please write 'help flags' for more information and help.");
                Pause();
            }  //flag.flags command

            if (Words[0].ToLower() == "neofetch")
            {
                bool flag_onlyIcon = false;
                for (int i = 1; i < Words.Count; i++)
                {
                    if (Words[i].ToLower() == "-icon")
                        flag_onlyIcon = true;
                }
                neoFetch(flag_onlyIcon);
            }                                //neofetch command

            if (Words[0].ToLower() == "color")
            {
                //string flag_textColor = "white";
                //string flag_boardColor = "black";
                bool flag_changes = false;
                bool flag_board = false;

                for (int i = 1; i < Words.Count; i++)
                {
                    string temp_string = Words[i].ToLower();
                    if (temp_string.Contains("-text"))
                    {
                        int temp_index = temp_string.IndexOf("-text:");
                        string temp_short_string = temp_string.Substring((temp_index + 6));

                        if (temp_short_string == "red")
                            textColor = ConsoleColor.Red;
                        if (temp_short_string == "yellow")
                            textColor = ConsoleColor.Yellow;
                        if (temp_short_string == "green")
                            textColor = ConsoleColor.Green;
                        if (temp_short_string == "cyan")
                            textColor = ConsoleColor.Cyan;
                        if (temp_short_string == "blue")
                            textColor = ConsoleColor.Blue;
                        if (temp_short_string == "magenta")
                            textColor = ConsoleColor.Magenta;
                        if (temp_short_string == "white")
                            textColor = ConsoleColor.White;
                        if (temp_short_string == "gray")
                            textColor = ConsoleColor.Gray;

                        if (temp_short_string == "darkred")
                            textColor = ConsoleColor.DarkRed;
                        if (temp_short_string == "darkyellow")
                            textColor = ConsoleColor.DarkYellow;
                        if (temp_short_string == "darkgreen")
                            textColor = ConsoleColor.DarkGreen;
                        if (temp_short_string == "darkcyan")
                            textColor = ConsoleColor.DarkCyan;
                        if (temp_short_string == "darkblue")
                            textColor = ConsoleColor.DarkBlue;
                        if (temp_short_string == "darkmagenta")
                            textColor = ConsoleColor.DarkMagenta;
                        if (temp_short_string == "darkgray")
                            textColor = ConsoleColor.DarkGray;
                        if (temp_short_string == "black")
                            textColor = ConsoleColor.Black;

                        temp_index = 0;
                        temp_short_string = null;
                        flag_changes = true;
                    }

                    if (temp_string.Contains("-board"))
                    {
                        int temp_index = temp_string.IndexOf("-board:");
                        string temp_short_string = temp_string.Substring((temp_index + 7));
                        flag_board = true;

                        if (temp_short_string == "red")
                            backgroundColor = ConsoleColor.Red;
                        if (temp_short_string == "yellow")
                            backgroundColor = ConsoleColor.Yellow;
                        if (temp_short_string == "green")
                            backgroundColor = ConsoleColor.Green;
                        if (temp_short_string == "cyan")
                            backgroundColor = ConsoleColor.Cyan;
                        if (temp_short_string == "blue")
                            backgroundColor = ConsoleColor.Blue;
                        if (temp_short_string == "magenta")
                            backgroundColor = ConsoleColor.Magenta;
                        if (temp_short_string == "white")
                            backgroundColor = ConsoleColor.White;
                        if (temp_short_string == "gray")
                            backgroundColor = ConsoleColor.Gray;

                        if (temp_short_string == "darkred")
                            backgroundColor = ConsoleColor.DarkRed;
                        if (temp_short_string == "darkyellow")
                            backgroundColor = ConsoleColor.DarkYellow;
                        if (temp_short_string == "darkgreen")
                            backgroundColor = ConsoleColor.DarkGreen;
                        if (temp_short_string == "darkcyan")
                            backgroundColor = ConsoleColor.DarkCyan;
                        if (temp_short_string == "darkblue")
                            backgroundColor = ConsoleColor.DarkBlue;
                        if (temp_short_string == "darkmagenta")
                            backgroundColor = ConsoleColor.DarkMagenta;
                        if (temp_short_string == "darkgray")
                            backgroundColor = ConsoleColor.DarkGray;
                        if (temp_short_string == "black")
                            backgroundColor = ConsoleColor.Black;

                        temp_index = 0;
                        temp_short_string = null;
                        flag_changes = true;
                    }
                    if (temp_string.Contains("-reset"))
                    {
                        textColor = ConsoleColor.White;
                        backgroundColor = ConsoleColor.Black;
                        flag_changes = true;
                        flag_board = true;
                    }
                    Console.BackgroundColor = backgroundColor;
                    Console.ForegroundColor = textColor;
                    temp_string = null;
                }
                if (!flag_changes) {
                    printColorList();
                }
                if (flag_board)
                    Console.Clear();
                Pause();
            }                                   //color command

            if (Words[0].ToLower() == "clear" || Words[0].ToLower() == "cls")
            {
                Console.Clear();
                Pause();
            }    //clear.cls command

            if (Words[0].ToLower() == "path" || Words[0].ToLower() == "cd")
            {
                string fullPath = null;
                fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (currentPath == "")
                    currentPath = fullPath;

                int temp_index = 0;
                bool listDrives = false;
                string[] allFiles = null;
                string[] allFolders = null;

                if (Words.Count > 1)
                {
                    if (Words[1].ToLower() == "/drives" || Words[1].ToLower() == "/drive" || Words[1].ToLower() == "/dl")       // /drives./drive/dl lists all available drives
                    {
                        listDrives = true;
                    } else if (Words[1] == "..")
                    {
                        if (currentPath.Contains("\\") && currentPath.Length > 3)
                        {
                            temp_index = currentPath.LastIndexOf("\\");
                            currentPath = currentPath.Substring(0, temp_index);                         //goes back 1 directory
                            if (currentPath.Length <= 2)
                                currentPath = currentPath + "\\";                                       //corrects directory format if the path is 'C:' to 'C:\' for example.
                        }
                        else
                        {
                            Console.WriteLine("[Error!] cannot go back. Please enter a valid directory");   //lists all Drives when 'cd ..'
                            listDrives = true;
                        }
                    }
                    else
                    {
                        string userPath = userInput.Substring(userInput.IndexOf(" ") + 1);
                        if (Directory.Exists(currentPath + "\\" + userPath))                            //before changing paths, it checks if the path even exists
                        {
                            currentPath = currentPath + "\\" + userPath;
                        } else if (Directory.Exists(userPath)) {
                            if (userPath.Length == 2)
                            {
                                currentPath = userPath + "\\";
                            } else
                                currentPath = userPath;
                        } else if (userPath.IndexOf("/") == 0 && userPath.ToLower().Contains(".exe"))   //first checks if user wrote '/', then checks if input contains '.exe'
                        {
                            if (userPath.Substring(userPath.LastIndexOf(".")).ToLower() == ".exe")      //it checks first if the input contains '.exe' to stop a software crash here.
                            {
                                string temp_exe = currentPath + "\\" + userPath.Substring(1);
                                if (File.Exists(temp_exe))
                                {
                                    System.Diagnostics.Process.Start(temp_exe);                         //starts the .exe file
                                }
                                else
                                {
                                    Console.WriteLine("[Error!] \"" + userPath + "\" doesn't exist in the current context.");
                                }
                            }
                        }

                    }
                }

                if (listDrives)                                         //lists all available drives, exmaple: "C:, D:, E:"
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in drives)
                    {
                        Console.WriteLine(drive);
                    }
                } else
                {
                    allFiles = Directory.GetFiles(currentPath);
                    allFolders = Directory.GetDirectories(currentPath);
                    Console.WriteLine(currentPath + "\n");

                    for (int i = 0; i < allFolders.Length; i++)
                    {
                        if (currentPath.Length > 3)
                        {                                       //prints all folders in that directory.
                            Console.WriteLine(allFolders[i].Substring(currentPath.Length + 1));
                            
                        }
                        else                                    //corrects folder names at root directory (print without +1 to index)
                        {                                       //example: instead of 'older' it would say 'Folder'
                            Console.WriteLine(allFolders[i].Substring(currentPath.Length));
                        }
                    }
                    for (int i = 0; i < allFiles.Length; i++)
                    {
                        if (currentPath.Length > 3)
                        {                                       //prints all available file in that directory.
                            Console.WriteLine(allFiles[i].Substring(currentPath.Length + 1));
                        }
                        else
                        {                                       //corrects file names that are in root directory.(same as folders)
                            Console.WriteLine(allFiles[i].Substring(currentPath.Length));
                        }
                    }
                }
                Pause();
            }      //path.cd command

            if (Words[0].ToLower() == "task" || Words[0].ToLower() == "tasks" || Words[0].ToLower() == "tl")
            {
                printProcesses();
            } //tasks.task.tl command

            if (Words[0].ToLower() == "terminate" || Words[0].ToLower() == "kill")
            {
                Process[] allProcs = Process.GetProcesses();
                if (Words.Count <= 1)       //if user did not input anything.
                {
                    Console.WriteLine("Please choose a process ID.");
                    Console.WriteLine("To list all running tasks write 'task'.");
                } else                      //if he did input, check if the input is a number.
                {
                    if (int.TryParse(Words[1], out int ProcID)) {
                        bool foundID = false;
                        foreach (Process proc in allProcs)          //check if the process ID(ProcID) is valid and exists.
                        {
                            ProcessThreadCollection allThreads = proc.Threads;
                            if (proc.Id == ProcID && ProcID > 4)    //added a limit of only above 4 because, 0-4 IDs are system tasks.
                            {        
                                Process.GetProcessById(ProcID).Kill();
                                foundID = true;
                            }
                        }
                        if (!foundID)
                        {
                            Console.WriteLine("[Error!] couldn't find task ID:\"" + ProcID + "\" please try again.");
                        }
                    }
                }
                Pause();
            }   //terminate.kill command

            if (Words[0].ToLower() == "echo")
            {
                temp_textColor = textColor;
                temp_backgroundColor = backgroundColor;
                if (Words.Count > 1)
                {
                    int tempIndex = userInput.IndexOf(Words[0]) + 5;
                    echoMSG(userInput.Substring(tempIndex));
                }
                else
                    Console.WriteLine(" ");
                Pause();
            }                                    //echo command

            if (Words[0].ToLower() == "filecreate" || Words[0].ToLower() == "fc")
            {
                if (currentPath == "")                                              //gets this software's directory
                    currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (Words.Count < 2)
                {
                    Console.WriteLine("[Error!] please enter a file name.");        //outputs an error if the user didn't write a file name
                } else
                {
                    char[] illigalChar = { '/', '>', '<', '>', '\\', ':', '*', '|' , '\"'};
                    String fileName = userInput.Substring(userInput.IndexOf(Words[1]));

                    for (int i = 0; i < illigalChar.Length; i++)                    //checks if a file contains any of those illigal characters.
                    {
                        if (fileName.Contains(illigalChar[i]))
                        {
                            Console.WriteLine("[Error!] please enter a valid file name.");
                            Pause();
                        }
                    }
                    File.Create(@currentPath + "\\" + fileName).Close();            //creates the file
                    Console.WriteLine("\"" + fileName + "\" Was successfully created.");
                }
                Pause();
            }   //filecreate.fc command

            if (Words[0].ToLower() == "filedelete" || Words[0].ToLower() == "fd")
            {
                if (currentPath == "")                                                      //gets this software's directory
                    currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (Words.Count < 2)
                {
                    Console.WriteLine("[Error!] please enter a file name.");                //outputs an error if the user didn't write a file name
                }
                else
                {
                    char[] illigalChar = { '/', '>', '<', '>', '\\', ':', '*', '|', '\"' };
                    String fileName = userInput.Substring(userInput.IndexOf(Words[1]));

                    for (int i = 0; i < illigalChar.Length; i++)                            //checks if a file contains any of those illigal characters.
                    {
                        if (fileName.Contains(illigalChar[i]))
                        {
                            Console.WriteLine("[Error!] please enter a valid file name.");
                            Pause();
                        }
                    }

                    if (File.Exists(@currentPath + "\\" + fileName))                        //checks if file exists before deleting it
                    {
                        File.Delete(@currentPath + "\\" + fileName);                        //deletes the file
                        Console.WriteLine("\"" + fileName + "\" Was successfully deleted.");
                    }
                    else
                    {
                        Console.WriteLine("[Error!] \"" + fileName + "\" Doesn't exist in the current context, try again.");
                    }
                }
                Pause();
            }   //filedelete.fd command

            if (Words[0].ToLower() == "filerename" || Words[0].ToLower() == "fr")
            {
                if (currentPath == "")                                                      //remembers this executable's directory(default)
                    currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (Words.Count < 2)
                {
                    Console.WriteLine("[Error!] please enter a file name.");                //outputs an error if the user didn't write a file name
                }
                else
                {
                    char[] illigalChar = { '/', '>', '<', '>', '\\', ':', '*', '|', '\"' };
                    String fileName = userInput.Substring(userInput.IndexOf(Words[1]));

                    for (int i = 0; i < illigalChar.Length; i++)                            //checks if a file contains any of those illigal characters.
                    {
                        if (fileName.Contains(illigalChar[i]))
                        {
                            Console.WriteLine("[Error!] please enter a valid file name.");
                            Pause();
                        }
                    }

                    if (File.Exists(@currentPath + "\\" + fileName))                            //checks if file exists before renaming it
                    {
                        Console.Write("Rename \"" + fileName + "\" to: ");
                        String fileNameNew = Console.ReadLine();
                        
                        for (int i = 0; i < illigalChar.Length; i++)                            //checks if a the new name contains any of those illigal characters.
                        {
                            if (fileNameNew.Contains(illigalChar[i]))
                            {
                                Console.WriteLine("[Error!] please enter a valid file name.");
                                Pause();
                            }
                        }
                        String name1 = @currentPath + "\\" + fileName;                          //name1 = old file, name2 = new file
                        String name2 = @currentPath + "\\" + fileNameNew;
                        File.Move(name1, name2);                                                //renames the old file to its new name
                        Console.WriteLine("\"" + fileName + "\" Was successfully renamed to: \"" + fileNameNew +"\".");
                    }
                    else
                    {
                        Console.WriteLine("[Error!] \"" + fileName + "\" Doesn't exist in the current context, try again.");
                    }
                }
                Pause();
            }   //filerename.fr command

            //Pause(); makes sure that the CommandsFunc() won't get to this line. if CommandsFunc() gets here, it'll output an error.
            Console.WriteLine("[Error!] didn't recognize \"" + Words[0] + "\" please write 'help'.");
            Pause();
        }
        static void Pause()     //Pause() is here temporarly for the early stages of this software for debugging purposes.
        {                       //it is also here to stop CommandsFunc() from warning the user about an error. Note: if something activates this func its working.
            //Console.WriteLine("\nPlease press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            CommandsFunc();   //remove that '//' before the 3 'Console.*' lines above for debugging.
        }
        
        static void echoMSG(string user_msg)
        {
            while (user_msg.Length > 0)
            {
                Console.BackgroundColor = temp_backgroundColor;
                Console.ForegroundColor = temp_textColor;
                if (user_msg.Contains("{") && user_msg.Contains("}"))
                {
                    int indexBrace1 = user_msg.IndexOf("{");
                    int indexBrace2 = user_msg.IndexOf("}");
                    int indexColon = 0;
                    if (user_msg.Contains(":"))
                        indexColon = user_msg.IndexOf(":");
                    if (indexBrace1 < indexBrace2)
                        Console.Write(user_msg.Substring(0,indexBrace1));
                    if (indexBrace2 < indexBrace1)
                        Console.Write(user_msg.Substring(0, indexBrace2));

                    if (indexBrace1 < indexBrace2)
                    {
                        string textC = "";
                        string boardC = "";
                        if (indexBrace1 < indexColon && indexColon < indexBrace2)                       //checks if it should change color to background aswell.
                        {
                            textC = user_msg.Substring(indexBrace1 + 1, indexColon - indexBrace1 - 1).ToLower();
                            boardC = user_msg.Substring(indexColon + 1, indexBrace2 - indexColon - 1).ToLower();
                            changeColor(textC, boardC);                                     //sends what text and background color to change
                            user_msg = user_msg.Substring(indexBrace2 + 1);                 //shortens the message after each {:}
                        } else
                        {
                            textC = user_msg.Substring(indexBrace1 + 1, indexBrace2 - indexBrace1 - 1).ToLower();
                            changeColor(textC, "null");
                            user_msg = user_msg.Substring(indexBrace2 + 1);                 //shortens the message after each {}
                        }
                    } else
                    {
                        user_msg = user_msg.Substring(indexBrace1 + 1);
                    }
                }
                else
                {
                    Console.WriteLine(user_msg);
                    user_msg = "";
                    Console.BackgroundColor = backgroundColor;
                    Console.ForegroundColor = textColor;
                }
            }
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = textColor;
            Pause();
        }
        static void changeColor(string text, string background)
        {
            if (text == "red")
                temp_textColor = ConsoleColor.Red;
            if (text == "yellow")
                temp_textColor = ConsoleColor.Yellow;
            if (text == "green")
                temp_textColor = ConsoleColor.Green;
            if (text == "cyan")
                temp_textColor = ConsoleColor.Cyan;
            if (text == "blue")
                temp_textColor = ConsoleColor.Blue;
            if (text == "magenta")
                temp_textColor = ConsoleColor.Magenta;
            if (text == "white")
                temp_textColor = ConsoleColor.White;
            if (text == "gray")
                temp_textColor = ConsoleColor.Gray;

            if (text == "darkred")
                temp_textColor = ConsoleColor.DarkRed;
            if (text == "darkyellow")
                temp_textColor = ConsoleColor.DarkYellow;
            if (text == "darkgreen")
                temp_textColor = ConsoleColor.DarkGreen;
            if (text == "darkcyan")
                temp_textColor = ConsoleColor.DarkCyan;
            if (text == "darkblue")
                temp_textColor = ConsoleColor.DarkBlue;
            if (text == "darkmagenta")
                temp_textColor = ConsoleColor.DarkMagenta;
            if (text == "darkgray")
                temp_textColor = ConsoleColor.DarkGray;
            if (text == "black")
                temp_textColor = ConsoleColor.Black;

            if (background == "red")
                temp_backgroundColor = ConsoleColor.Red;
            if (background == "yellow")
                temp_backgroundColor = ConsoleColor.Yellow;
            if (background == "green")
                temp_backgroundColor = ConsoleColor.Green;
            if (background == "cyan")
                temp_backgroundColor = ConsoleColor.Cyan;
            if (background == "blue")
                temp_backgroundColor = ConsoleColor.Blue;
            if (background == "magenta")
                temp_backgroundColor = ConsoleColor.Magenta;
            if (background == "white")
                temp_backgroundColor = ConsoleColor.White;
            if (background == "gray")
                temp_backgroundColor = ConsoleColor.Gray;

            if (background == "darkred")
                temp_backgroundColor = ConsoleColor.DarkRed;
            if (background == "darkyellow")
                temp_backgroundColor = ConsoleColor.DarkYellow;
            if (background == "darkgreen")
                temp_backgroundColor = ConsoleColor.DarkGreen;
            if (background == "darkcyan")
                temp_backgroundColor = ConsoleColor.DarkCyan;
            if (background == "darkblue")
                temp_backgroundColor = ConsoleColor.DarkBlue;
            if (background == "darkmagenta")
                temp_backgroundColor = ConsoleColor.DarkMagenta;
            if (background == "darkgray")
                temp_backgroundColor = ConsoleColor.DarkGray;
            if (background == "black")
                temp_backgroundColor = ConsoleColor.Black;

            if (background == "reset" || text == "reset")
            {
                temp_textColor = ConsoleColor.White;
                temp_backgroundColor = ConsoleColor.Black;
            }
        }
        static void ProcessWords(string userInput) //this function reads a string(userInput) and converts it to individual words, and stores it in a 'Words' list.
        {
            string Temp_userInput = userInput;
            while (Temp_userInput.Length > 0)
            {
                if (Temp_userInput.Contains(" "))
                {
                    int firstSpace = Temp_userInput.IndexOf(" ");
                    while (firstSpace == 0)
                    {
                        Temp_userInput = Temp_userInput.Substring(1);
                        firstSpace = Temp_userInput.IndexOf(" ");
                    }
                    if (firstSpace > 0)
                    {
                        Words.Add(Temp_userInput.Substring(0, firstSpace));
                        Temp_userInput = Temp_userInput.Substring(firstSpace + 1);
                    }
                }
                else
                {
                    Words.Add(Temp_userInput);
                    Temp_userInput = null;
                    break;
                }
            }
        }
        static void PrintAllWords()
        {
            for (int i = 0; i < Words.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "]" + Words[i]);
            }
        }       //this is a debugging function that could be deleted. It prints all the words in the Words list
        static void GetComponent(string hwclass, string syntax)             //this function prints hardware info. *make this function private if it ever outputs errors*
        {                                                                   //'hwclass' is what hardware, 'syntax' is what do we want to know about the hardware.
            RAM_total = 0;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);
            foreach (ManagementObject mj in mos.Get())
            {
                if (Convert.ToString(mj[syntax]) != "")
                {
                    if (hwclass == "Win32_PhysicalMemory")              //if we refer to the RAM.
                    {
                        if (syntax == "Capacity")                       //coverts all memory from all sticks from bytes to GB.
                        {
                            string temp_string = Convert.ToString(mj[syntax]);
                            float temp_int = float.Parse(temp_string, System.Globalization.CultureInfo.InvariantCulture);
                            RAM_total += temp_int / 1073741824;         //1 Gigabyte = 1073741824 Bytes
                        }
                        if (syntax == "Speed" && !writtenRAMspeed)      //writes the RAM speed of the first stick only.
                        {
                            Console.Write(Convert.ToString(mj[syntax]) + "MHz");
                            writtenRAMspeed = true;                     //Note: to display all speeds remove this line.
                        }
                    }
                    if (hwclass == "Win32_OperatingSystem" && syntax == "Name")     //if we refer to the OS.
                    {
                        string temp_string = Convert.ToString(mj[syntax]);
                        int temp_index = temp_string.IndexOf("|");                  //this prints only the name of the OS and ignores other info.
                        Console.Write(temp_string.Substring(0, temp_index));
                    } else if (hwclass != "Win32_PhysicalMemory")       //if we DON'T refer to RAM or the OS, print info as normal.
                    {
                        Console.Write(Convert.ToString(mj[syntax]) + " ");
                    }
                }
            }
        }
        static void neoFetch(bool flag_onlyIcon)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n           ########   ###  ###");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("         ########    ###  ###");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("       ####         ###  ###");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("      ###     ##################");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("     ###     ##################");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    ###          ###  ###");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   ###     ##################");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ###     ##################");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ####         ###  ###");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" ########    ###  ###");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("  ########  ###  ###");
            Console.ForegroundColor = ConsoleColor.White;

            if (flag_onlyIcon)              //if we have -icon flag, go to Pause(); and ignore the rest of the function.
                Pause();

            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine("Version: 1.3.0");


            Console.Write("\nOperating system: ");
            GetComponent("Win32_OperatingSystem", "Name");

            Console.Write("\n\nOperating system version: ");
            GetComponent("Win32_OperatingSystem", "Version");

            Console.Write("\n\nDevice name: ");
            GetComponent("Win32_ComputerSystem", "Name");

            Console.Write("\n\nCPU: ");
            GetComponent("Win32_Processor", "Name");

            Console.Write("\n\nGPU: ");
            GetComponent("Win32_VideoController", "Name");

            Console.Write("\n\nRAM: ");                      //https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-physicalmemory
            GetComponent("Win32_PhysicalMemory", "Capacity");
            Console.Write(RAM_total + "GB");
            Console.Write(" @ ");
            GetComponent("Win32_PhysicalMemory", "Speed");
            writtenRAMspeed = false;                        //resets for the next time 'neofetch' would be called. (if this bool is true it won't write RAM speed)


            Console.BackgroundColor = backgroundColor;      //The background colors here and in the last lines are to make sure that console won't print another color accidently.
            Console.Write("\n\n");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  ");
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine("  ");

            Console.BackgroundColor = backgroundColor;
            Console.Write("");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("  ");
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine("  ");
            Pause();
        }
        static void printProcesses()                                        //prints a list of all running processes
        {
            Process[] allProcs = Process.GetProcesses();
            foreach (Process proc in allProcs)
            {
                ProcessThreadCollection allThreads = proc.Threads;
                string tempString = ("Process: " + proc.ProcessName + ",");
                while (tempString.Length <= 46)             //if the process's name isn't long enough add spaces to the string.
                {
                    tempString = tempString + " ";
                }
                tempString = tempString + " ID: " + proc.Id;
                Console.WriteLine(tempString);
            }
            Pause();
        }
        static void  printColorList()       //prints a list of all colors
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Current Text Color: " + textColor);
            Console.WriteLine("Current Board Color: " + backgroundColor);
            Console.WriteLine("                            ");
            Console.WriteLine("List of colors:             ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Red          ");
            printColorColon();              //prints white ':'
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("DarkRed      ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Yellow       ");
            printColorColon();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("DarkYellow   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Green        ");
            printColorColon();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("DarkGreen    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Cyan         ");
            printColorColon();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("DarkCyan     ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Blue         ");
            printColorColon();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("DarkBlue     ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Magenta      ");
            printColorColon();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("DarkMagenta  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("White        ");
            printColorColon();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Black");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" (Black)");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Gray         ");
            printColorColon();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("DarkGray     ");

            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
        }
        static void printColorColon()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(": ");
        }
    }
}
//created by https://github.com/000Daniel

using System;
class commandParser2 {
    static void Main(string[] args) 
    {
                    //the bool arguments are to check whether the user used a flag or not.
                    //for example: if the user wrote '-h' we would display the help message.
                    //Note: the '--i' in the help flag is there for debugging reasons.
                    //Note: some letter cannot be used such as '-c' so instead type '-C'.
        bool Help = parseArgsBool(args, new string[]{ "-h" , "--i" , "--help"}, "show this help message and exit");
        bool Quiet = parseArgsBool(args, new string[]{ "-q" , "--quiet"}, "don't print the 'Output' text");
        bool Information = parseArgsBool(args, new string[]{ "-i" , "--information"}, "display extra information");
        bool Colored = parseArgsBool(args, new string[]{ "-C" , "--colored"}, "color code the output");
                    //this List argument will catch every argument that the user wrote that isn't a flag.
                    //this argument should ALWAYS be declared after the flags.
        List<string> Argument = parseArgs(args, "argument1", "passes a string into the script");
                    //'help_message' commands will modify things like description and more info.
        help_message("usage","Program");
        help_message("description","C# argument parser 2.0 template");
        help_message("information","created by https://github.com/000Daniel");
                    //this if statement should not be changed and should always come after the flags,
                    //arguments and 'help_message', but before other code.
        if (Help)
        {
            help_message("call",string.Empty);
        }


                    //Examples of how to use the flags:
                    //the program will print what flags the user did or did not use.
        Console.WriteLine(string.Format("\nFlags: quiet:{0} , info:{1} , colored:{2}\n", Quiet, Information, Colored));
        
                    //as long as the user did not write '-q', print 'Output'.
        if (!Quiet)
        {
            Console.WriteLine("Output: ");
        }
                    //if the user used '-i', print extra information.
        if (Information)
        {
            Console.WriteLine(string.Format("There are {0} arguments", Argument.Count()));
        }
                    //this prints every string in the 'Argument' List.
        foreach (string arg in Argument) 
        {
                    //if the user chose '-C', color code the result.
            if (Colored)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(arg);
        }
        if (Colored)
        {
            Console.ResetColor();
        }
    }

    //this function compares the args[] to flags[] and returns false or
    //true accordingly. it also automatically adds the description of
    //those flags to 'help_message'.
    static bool parseArgsBool(string[] args, string[] flags, string desc)
    {
        string message_flags = "";
        usage_flags = usage_flags + string.Format("[{0}] ",flags.First());
        foreach (string flag in flags)
        {
            all_flags_list.Add(flag);
            if (flag != flags.Last()) 
            {
                message_flags = message_flags + flag + ", ";
            }
            else 
            {
                message_flags = message_flags + flag + " ";
            }
        }
        string message = message_flags;
            for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
            message = message + desc;
            ops_args.Add(message);
        foreach (string argument in args)
        {
            foreach (string flag in flags)
            {
                if (argument == flag)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    //this function compares all args[] to 'all_flags_list'.
    //Note: all_flags_list contains all the options that trigger the
    //      flags.
    //if 'all_flags_list' does not contain args[], those arguments will
    //return to the list that requested them.
    //this way listing all Arguments won't result in printing '-q' for
    //example.
    static List<string> parseArgs(string[] args, string name,string desc)
    {
        List<string> all_arguments_list = new List<string>();
        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);
        foreach (string argument in args) 
        {
            if (!all_flags_list.Contains(argument))
            {
                all_arguments_list.Add(argument);
            }
        }
        return all_arguments_list;
    }

    //these static variables/data-types are used in this script.
    //they should not be modified or deleted.
    static List<string> all_flags_list = new List<string>();
    static string usage_flags = "";
    static string usage = "Program";
    static string description = "";
    static List<string> pos_args = new List<string>();
    static List<string> ops_args = new List<string>();
    static List<string> more_info = new List<string>();

    //this function is the help_message that recieves data, and when
    //it recieves a call(help_message("call",string.Empty);), it will
    //print out a help message.
    //this help message should automatically include all optional
    //flags and positional arguments.
    //Note: this help_message is formatted traditionally, like any other
    //      help page/message.
    static void help_message(string operation, string message)
    {
        if (operation == "usage" && !string.IsNullOrEmpty(message))
        {
            usage = message;
        }
        if (operation == "description" && !string.IsNullOrEmpty(message))
        {
            description = "\n" + message;
        }
        if (operation == "positional argument" && !string.IsNullOrEmpty(message))
        {
           pos_args.Add(message);
        }
        if (operation == "optional argument" && !string.IsNullOrEmpty(message))
        {
            ops_args.Add(message);
        }
        if (operation == "information" && !string.IsNullOrEmpty(message))
        {
            more_info.Add(message);
        }
        if (operation == "call") {
            Console.WriteLine("usage: " + usage + " " + usage_flags);
            if (!string.IsNullOrEmpty(description))
            {
                Console.WriteLine(description);
            }
            if (pos_args.Count > 0)
            {
                Console.WriteLine("\npositional arguments:");
                foreach (string arg in pos_args) 
                {
                    Console.WriteLine("  " + arg);
                }
            }
            if (ops_args.Count > 0)
            {
                Console.WriteLine("\noptions:");
                foreach (string arg in ops_args) 
                {
                    Console.WriteLine("  " + arg);
                }
            }
            if (more_info.Count > 0)
            {
                Console.WriteLine("\ninformation:");
                foreach (string arg in more_info) 
                {
                    Console.WriteLine("  " + arg);
                }
            }
            Environment.Exit(0);
        }
    }
}
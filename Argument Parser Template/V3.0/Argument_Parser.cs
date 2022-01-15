public class AP3
{
    //this function compares the args[] to flags[] and returns false or
    //true accordingly. it also automatically adds the description of
    //those flags to 'help_message'.
    public static bool Boolean(string[] args, string[] flags, string desc)
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
    //this way listing all Arguments won't include '-q' for example.
    public static List<string> List(string[] args, string name,string desc)
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
    //they should not be modified or removed.
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
    //Note: this help_message is formatted like any other 'official'
    //      help page/message that you would find.
    public static void help_message(string operation, string message)
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
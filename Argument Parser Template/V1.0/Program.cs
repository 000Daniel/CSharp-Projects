using System;
class commandParser {
    //all of these data types will be used in this example later.
    static bool quiet = false;
    static bool repeat = false;
    static List<string> argument1 = new List<string>();
    

    static void Main(string[] args) 
    {
                //this line sends all arguments to a function that interprets them.
        parseArgs(args);

                //the quiet(-q, --quiet) argument is an example of a 'boolean' argument
        if (!quiet)
            Console.WriteLine("Output: ");

                //'argument1' is an example of a positional argument, (a string in this case).
                //this argument will run no matter what, even if the user did not write any
                //arguments.
        foreach (string arg in argument1)
        {
                //the repeat(-R, --repeat) argument is also a 'boolean' example.
            if (repeat)
                Console.WriteLine(arg);
            Console.WriteLine(arg);
        }
    }

    static void parseArgs(string[] args) {
                //in some cases the first word of an argument might be the name of the program,
                //so we make sure that, we skip that argument.
                //'first' is here to declare the first word in the next 'foreach loop'.
        bool first = true;
        
                //this 'foreach loop' will take every string separately and interpret that.
        foreach (string argument in args) 
        {
                //the help(-h, -i, --help) argument will output a help message.
                //the '-i' option is for debugging purposes, it is not used regularly.
                //this is a default template help message, formated traditionally.
            if (argument == "-h" || argument == "-i" || argument == "--help")
            {
                Console.WriteLine("usage: Program [-h] [-q] [-R] [String]");
                Console.WriteLine("");
                Console.WriteLine("C# argument parser template");
                Console.WriteLine("");
                Console.WriteLine("positional arguments:");
                Console.WriteLine("  argument1               passes a string into the script");
                Console.WriteLine("");
                Console.WriteLine("options:");
                Console.WriteLine("  -h, --help            show this help message and exit");
                Console.WriteLine("  -q, --quiet           don't print the 'Output' text");
                Console.WriteLine("  -R, --repeat          repeat the text twice");
                Console.WriteLine("");
                Console.WriteLine("  created by https://github.com/000Daniel");
                Environment.Exit(0);
            }
            else if (argument == "-q" || argument == "--quiet")
                //this is an example of how to get a boolean argument(true or false).
                //if the user doesn't type anything this argument will be false,
                //if the user does type the argument(-q, --quiet), this argument will be true.
            {
                quiet = true;
            }
            else if (argument == "-R" || argument == "--repeat")
                //this boolean argument is an example of a letter that cannot be lowercase.
                //some letters will output an error if they are lowercase, so instead the
                //argument should be a capital letter.
            {
                repeat = true;
            }
            else if (argument != null)
                //the argument that was checked but wasn't interpreted yet (optionally)would be
                //added to a list.
            {
                if (first && argument == "Program.cs") 
                {
                    //skip this argument
                } else {
                    argument1.Add(argument);
                }

                //the first word was already interpreted.
                first = false;
            }
        }
    }
}
//created by https://github.com/000Daniel
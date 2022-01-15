class Template
{
    static void Main(string[] args)
    {
                    //the bool arguments are to check whether the user used a flag or not.
                    //for example: if the user wrote '-h' the program would display a help message.
                    //Note: the '--i' in the help flag is there for debugging reasons.
                    //Note: some letter cannot be used such as '-c' so instead type '-C'.
        bool Help = AP3.Boolean(args, new string[]{ "-h" , "--i" , "--help"}, "show this help message and exit");
        bool Quiet = AP3.Boolean(args, new string[]{ "-q" , "--quiet"}, "don't print the 'Output' text");
        bool Colored = AP3.Boolean(args, new string[]{ "-C" , "--colored"}, "color code the output");
                    //this List argument will catch every argument that the user wrote that isn't a flag.
                    //this argument should ALWAYS be declared after the flags.
        List<string> Argument = AP3.List(args, "argument1", "passes a string into the script");
                    //'help_message' commands will modify things like description and more info.
        AP3.help_message("usage","Program");
        AP3.help_message("description","C# argument parser 3.0 template");
        AP3.help_message("information","created by https://github.com/000Daniel");
                    //this 'if statement' should not be changed and should always come after the flags,
                    //arguments and 'help_message', but before other code.
        if (Help)
        {
            AP3.help_message("call",string.Empty);
        }

        //How to code with the relevant data:
                    //Examples of how to use the flags:
                    //the program will print what flags the user did or did not use.
        Console.WriteLine(string.Format("\nFlags: quiet:{0} , colored:{1}\n", Quiet, Colored));

        if (!Quiet)//as long as the user did not write '-q', print 'Output'.
        {
            Console.WriteLine("Output: ");
        }
           
        if (Colored)//if the user chose '-C', color code the result.
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }


        //How to code with the relevant data:
                    //Example of how to use a positional argument:
                    //the program will print all arguments that are not flags
        foreach (string single_argument in Argument)
        {
            Console.WriteLine(single_argument);
        }

        Console.ResetColor();
    }
}
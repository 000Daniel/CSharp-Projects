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
                    //the next code demonstrates how to declare positional arguments.
                    //these arguments will be affected by their order.
                    //  for example: if 'string_Argument' comes before 'int_Argument'.
                    //  "dotnet run "John" 35"
                    //  the "John" would be assigned to 'string_Argument'
                    //  while '35' would be assigned to 'int_Argument'
        string string_Argument = AP3.String(args, "argument-string", "passes a string into the script");
        int int_Argument = AP3.Int(args, "argument-int", "passes an int32 into the script");
                    //'help_message' commands will modify things like description and more info.
        AP3.help_message("description","C# argument parser 3.1 template");
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
        Console.WriteLine("\nFlags: quiet:{0} , colored:{1}\n", Quiet, Colored);

        if (!Quiet)//as long as the user did not write '-q', print 'Output'.
        {
            Console.WriteLine("Output: ");
        }
           
        if (Colored)//if the user chose '-C', color code the result.
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        //How to code with the relevant data:
                    //Example of how to use a positional argument:
                    //this code would print all of the available positional arguments.
                    //if the user inputted anything it would print it here.
        Console.WriteLine("Name (string): " + string_Argument);
        Console.WriteLine("Age (integer): " + int_Argument);

        if (Colored)//if the user chose '-C', color code the result.
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        
        Console.ResetColor();
    }
}
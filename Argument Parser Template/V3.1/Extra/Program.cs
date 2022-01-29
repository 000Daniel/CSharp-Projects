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
                    //  for example: if 'int_Argument' comes before 'float_Argument'.
                    //  "dotnet run 20 5.5"
                    //  the 20 would be assigned to 'int_Argument'
                    //  while 5.5 would be assigned to 'float_Argument'
                    //these are the supported data types for this Argument Parser:
        short short_Argument = AP3.Short(args, "argument-short", "passes an int16 into the script");
        ushort ushort_Argument = AP3.uShort(args, "argument-ushort", "passes a uint16 into the script");
        int int_Argument = AP3.Int(args, "argument-int", "passes an int32 into the script");
        uint uint_Argument = AP3.uInt(args, "argument-uint", "passes a uint32 into the script");
        long long_Argument = AP3.Long(args, "argument-long", "passes an int64 value into the script");
        ulong ulong_Argument = AP3.uLong(args, "argument-ulong", "passes a uint64 into the script");
        float float_Argument = AP3.Float(args, "argument-float", "passes a float into the script");
        decimal decimal_Argument = AP3.Decimal(args, "argument-decimal", "passes a decimal-float value into the script");
        double double_Argument = AP3.Double(args, "argument-double", "passes a double-float value into the script");
        byte byte_Argument = AP3.Byte(args, "argument-byte", "passes a byte value into the script");
        sbyte sbyte_Argument = AP3.sByte(args, "argument-sbyte", "passes a sbyte value into the script");
        string string_Argument = AP3.String(args, "argument-string", "passes a string into the script");
        char char_Argument = AP3.Char(args, "argument-char", "passes a character into the script");
                    //this List argument will catch every argument that the user wrote.
                    //this argument should be declared after the flags, and after any other positional
                    //argument have already been declared, so that it won't store those arguments.
                    //alternatively placing this List argument before flags or other positional arguments
                    //would cause this list to include those arguments in.
        List<string> Argument = AP3.List(args, "argument-list", "passes a string into the script");
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
        Console.WriteLine("short integer: " + short_Argument);
        Console.WriteLine("short unsigned integer: " + ushort_Argument);
        Console.WriteLine("integer: " + int_Argument);
        Console.WriteLine("unsigned integer: " + uint_Argument);
        Console.WriteLine("long integer: " + long_Argument);
        Console.WriteLine("long unsigned integer: " + ulong_Argument);
        Console.WriteLine("float: " + float_Argument);
        Console.WriteLine("decimal value: " + decimal_Argument);
        Console.WriteLine("double float: " + double_Argument);
        Console.WriteLine("unsigned byte: " + byte_Argument);
        Console.WriteLine("byte: " + sbyte_Argument);
        Console.WriteLine("string: " + string_Argument);
        Console.WriteLine("character: " + char_Argument);

        if (Colored)//if the user chose '-C', color code the result.
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        
                    //the program will print all remaining arguments,
                    //(if there are any).
        foreach (string single_argument in Argument)
        {
            Console.WriteLine(single_argument);
        }

                    //writing "dotnet run 20 5" would result in '25'.
        Console.WriteLine("\nShort + uShort = {0}\n", short_Argument + ushort_Argument);

        Console.ResetColor();
    }
}
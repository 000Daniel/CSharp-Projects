class TextToBinary
{
    static bool IncludeSpaces = false;

    /*
    ======================[Over View!]======================
    Main() function handles:
        settings for 'Argument Parser'.
        handles all mentioned functions and calls them.
        checks for invalid characters.
        and handles some conversions.
    decimal_convert_binary() function handles:
        converts a single decimal value to binary code.
        returns a result of the conversion.
    binary_convert_decimal() function handles:
        converts a single binary code(8 chars) to decimal.
        returns a result of the conversion.
    sort_list() function handles:
        splitting strings that have spaces, and sorting it's
        list, Example:
        {"01000011 00100011"} ===> {"01000011","00100011"}
        {"58 45", "93"}       ===> {"58","45","93"}
    ErrorCall() function handles:
        prints Error message and exits the program.
    ======================[Extra info]======================
    Limitations:
        - this program cannot work with binary code higher
          or smaller than 8 characters.
        - it cannot convert a decimal value higher than 255
          to binary because of mentioned limitations.

    This software uses my Argument Parser 3.0, more about
    that here:
    https://github.com/000Daniel/CSharp-Projects/tree/main/Argument%20Parser%20Template/V3.0
    ========================================================
    */
    static void Main(string[] args)
    {
                //these lines are Argument Parser settings.
        bool Help = AP3.Boolean(args, new string[]{ "-h" , "--i" , "--help"}, "show this help message and exit");
        IncludeSpaces = AP3.Boolean(args, new string[]{ "-s" , "--includespaces"}, "prints the result with spaces between each character");
        bool Text2Binary = AP3.Boolean(args, new string[]{ "--t2b" , "--texttobinary"}, "convert a string to binary code");
        bool Text2Decimal = AP3.Boolean(args, new string[]{ "--t2d" , "--texttodecimal"}, "convert a string to a decimal value");
        bool Binary2Text = AP3.Boolean(args, new string[]{ "--b2t" , "--binarytotext"}, "convert binary code to a string");
        bool Binary2Decimal = AP3.Boolean(args, new string[]{ "--b2d" , "--binarytodecimal"}, "convert binary code to a decimal value");
        bool Decimal2Text = AP3.Boolean(args, new string[]{ "--d2t" , "--decimaltotext"}, "convert a decimal value to a string");
        bool Decimal2Binary = AP3.Boolean(args, new string[]{ "--d2b" , "--decimaltobinary"}, "convert a decimal value to binary code");
        List<string> Argument = AP3.List(args, "argument1", "passes a string into the script");
        AP3.help_message("usage","Program");
        AP3.help_message("description","Text to Binary converter");
        AP3.help_message("information","created by https://github.com/000Daniel");
        if (Help)
        {
            AP3.help_message("call",string.Empty);
        }
                //Argument Parser settings end here.

                
                //these lines make sure that the user only used 1(one) flag.
        int temp_bool_check = Convert.ToInt32(Text2Binary) + Convert.ToInt32(Text2Decimal) + Convert.ToInt32(Binary2Text) + Convert.ToInt32(Binary2Decimal) + Convert.ToInt32(Decimal2Binary) + Convert.ToInt32(Decimal2Text);
        if (temp_bool_check == 0)
        {
            ErrorCall("Please choose a format.\nFor more help write --help.");
        }
        if (temp_bool_check > 1)
        {
            ErrorCall("Please choose only a single format.\nFor more help write --help.");
        }
        
                //this makes sure that the user did not enter an invalid character into the Arguments.
                //For example, for binary the user cannot enter anything but 0, 1 or space.
                //For decimal its 0-9 and space.
        if (Binary2Decimal || Binary2Text || Decimal2Binary || Decimal2Text)
        {
            string allowedChars = "01 ";
            if (Decimal2Binary || Decimal2Text)
            {
                allowedChars = "0123456789 ";
            }
            foreach (string arg in Argument)
            {
                foreach (char c in arg) {
                    if (!allowedChars.Contains(c.ToString())) {
                        ErrorCall("Detected invalid characters!");
                    }
                }
            }
        }

                //this statement convers Text(string) into Decimal value(bytes).
                //'(int)char' function converts the character.
        if (Text2Decimal)
        {
            foreach (string x in Argument)
            {
                foreach (char y in x)
                {
                    Console.Write((int)y);
                //if the user used '-s' add a space between each character.
                    if (IncludeSpaces)
                    {
                        Console.Write(" ");
                    }
                }
            }
        }

                //this statement makes sure that the Binary code is valid by:
                //removing all spaces.
                //and checking the code's length(each code needs to be 8 chars).
                //
                //then the statement sends the VALID code into a
                //binary_convert_decimal() function.
        if (Binary2Decimal)
        {
            for (int i = 0; i < Argument.Count(); i++)
            {
                while (Argument[i].Length > 0)
                {
                    if (Argument[i].Contains(" "))
                    {
                    Argument[i] = Argument[i].Replace(" ", "");
                    }

                    if (!((float)Argument[i].Length / 8).ToString().Contains("."))
                    {
                        Console.Write(binary_convert_decimal(Argument[i]));
                        if (IncludeSpaces)
                        {
                           Console.Write(" ");
                        }
                    }
                    else
                    {
                        ErrorCall("Invalid binary code length.");
                    }
                    Argument[i] = Argument[i].Substring(8);
                }
            }
        }

                //this statement sorts all the arguments with sort_list() function.
                //then it sends the sorted arguments into decimal_convert_binary()
                //function.
        if(Decimal2Binary)
        {
            Argument = sort_list(Argument);

            foreach (string arg in Argument)
            {
                Console.Write(decimal_convert_binary(arg));

                if (IncludeSpaces)
                {
                    Console.Write(" ");
                }
            }
        }

                //this statement converts Text(string) to binary codes.
        if (Text2Binary)
        {
            foreach (string x in Argument)
            {
                foreach (char y in x)
                {
                    //1) convert each character in each argument to a decimal with
                    //'(int)char'.
                    //2) send the int to a decimal_convert_binary() function.
                    Console.Write(decimal_convert_binary(((int)y).ToString()));

                    if (IncludeSpaces)
                    {
                    Console.Write(" ");
                    }
                }
            }
        }

                //this statement makes sure that the Binary code is valid by:
                //removing all spaces.
                //and checking the code's length(each code needs to be 8 chars).
                //
                //then the statement sends the VALID code into a
                //binary_convert_decimal() function.
                //and then converts the decimal value to a character with:
                //'(char)int' function.
        if (Binary2Text)
        {
            for (int i = 0; i < Argument.Count(); i++)
            {
                while (Argument[i].Length > 0)
                {
                    if (Argument[i].Contains(" "))
                    {
                    Argument[i] = Argument[i].Replace(" ", "");
                    }

                    if (!((float)Argument[i].Length / 8).ToString().Contains("."))
                    {
                        //1) send 'Argument[i]' to binary_convert_decimal.
                        //2) convert/parse returned data to int.
                        //3) convert the int(decimal) into a character.
                        Console.Write((char)int.Parse(binary_convert_decimal(Argument[i])));
                        if (IncludeSpaces)
                        {
                           Console.Write(" ");
                        }
                    }
                    else
                    {
                        ErrorCall("Invalid binary code length.");
                    }
                    Argument[i] = Argument[i].Substring(8);
                }
            }
        }
    
                //this statement convers Decimal values(bytes) into Text(string).
                //'(char)int' function converts the decimal value.
        if (Decimal2Text)
        {
            Argument = sort_list(Argument);
            foreach (string arg in Argument)
            {
                //1) convert/parse the Argument(arg) into int.
                //2) convert the int(decimal) into a char(text).
                Console.Write((char)int.Parse(arg));
                if (IncludeSpaces)
                {
                    Console.Write(" ");
                }
            }
        }

        Console.WriteLine("\n");
    }
        //END OF Main() FUNCTION.

                //this function converts decimal values(bytes) to binary code.
                //first this funtion converts the passed Arguments to int.
                //then it does the next thing: (x = int)
                //  x - 128
                //  x - 64
                //  x - 32
                //  x - 16
                //  x - 8
                //  x - 4
                //  x - 2
                //  x - 1
                //if any of those pass it will output a 1, and commit the operation.
                //if the value will be negative output a 0, and dont commit the operation.
                //Example: (int = 195)
                //  195 - 128 = 67  (output 1 because the value isn't negative)
                //  67 - 64   = 3   (output 1)
                //  3 - 32    =-29  (output 0 and don't commit the operation)
                //  3 - 16    =-13  (output 0)
                //  3 - 8     =-5   (output 0)
                //  3 - 4     =-1   (output 0)
                //  3 - 2     = 1   (output 1)
                //  1 - 1     = 0   (output 1)
                //Decimal(195) === Binary(11000011)
                //Known limitation:
                //  this function cannot convert a decimal higher than 255 to binary,
                //  because it can only generate an 8(eight) character binary code.
    static string decimal_convert_binary(string arg)
    {
        int temp_int = 0;
        try
        {
            temp_int = int.Parse(arg);
        }
        catch
        {
            ErrorCall("Invalid decimal character!");
        }
        String result_code = "";
        if (temp_int > 255)
        {
            WarningCall("cannot convert a decimal higher than 255 to binary.");
        }
        for (int x = 128; x > 0; x = x/2)
        {
            if (temp_int >= x)
            {
                temp_int = temp_int - x;
                result_code = result_code + "1";
            }
            else
            {
                result_code = result_code + "0";
            }
        }
        return(result_code);
    }

                //this function converts binary code to decimal values(bytes).
                //it does the next thing for 8 characters:
                //  char[1] * 1
                //              +
                //  char[2] * 2
                //              +
                //  char[3] * 4
                //              +
                //  char[4] * 8
                //              +
                //  char[5] * 16
                //              +
                //  char[6] * 32
                //              +
                //  char[7] * 64
                //              +
                //  char[8] * 128
                //
                //Example: (binary code = 11000011)
                //  1 * 1
                //        +
                //  1 * 2
                //        +
                //  0 * 4
                //        +
                //  0 * 8
                //        +
                //  0 * 16
                //        +
                //  0 * 32
                //        +
                //  1 * 64
                //        +
                //  1 * 128
                //
                //Binary(11000011) === Decimal(195)
    static string binary_convert_decimal(string Argument)
    {
        int result_Decimal = 0;
        int multiplyBy = 1;
        for (int x = 7; x >= 0; x--)
        {
            result_Decimal += int.Parse(Argument.Substring(x,1)) * multiplyBy;
            multiplyBy = multiplyBy * 2;
        }
        return(result_Decimal.ToString());
    }

                //this function sorts passed arguments and returns them.
                //it will separate an argument with spaces and add it to the list.
                //Example:
                //  {"72 101", "121", " 33  "}
                //will sort to:
                //  {"72","101","121","33"}
    static List<String> sort_list(List<string> Argument)
    {
        List<string> return_list = new List<string>();
        for (int i =0; i < Argument.Count(); i++)
        {
            while (Argument[i].Contains(" "))
            {
                int spaceIndex = Argument[i].IndexOf(" ");
                if (spaceIndex == 0)
                {
                    Argument[i] = Argument[i].Substring(1);
                }
                else
                {
                    return_list.Add(Argument[i].Substring(0,spaceIndex));
                    Argument[i] = Argument[i].Substring(spaceIndex);
                }
            }
            if (Argument[i].Length > 0)
            {
                return_list.Add(Argument[i]);
                Argument[i] = "";
            }
        }
        if (return_list.Count() > 0)
        {
            return(return_list);
        }
        else
        {
            return(new List<string>());
        }
    }
                
                //this function handles Error messages.
                //this would be called when the user did something wrong.
                //it will print the message in RED and exit the program.
    static void ErrorCall(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(String.Format("Error! {0}", message));
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Environment.Exit(0);
    }

                //this function handles Warning messages.
                //this would be called when the user did something wrong.
                //it will print the message in Yellow.
    static void WarningCall(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(String.Format("Warning! {0}", message));
        Console.ResetColor();
    }
}
//created by https://github.com/000Daniel

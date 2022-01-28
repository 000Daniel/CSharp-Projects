class SimpleEncryption
{
    static void Main(string[] args)
    {
                //This software uses my Argument Parser 3.0, more about that here:
                //https://github.com/000Daniel/CSharp-Projects/tree/main/Argument%20Parser%20Template/V3.0
                //
                //these lines are Argument Parser settings.
        bool Help = AP3.Boolean(args, new string[]{ "-h" , "--i" , "--help"}, "show this help message and exit");
        bool Encrypt = AP3.Boolean(args, new string[]{ "-e" , "--encrypt"}, "encrypt the passed string");
        bool Decrypt = AP3.Boolean(args, new string[]{ "-D" , "--decrypt"}, "decrypt the passed string");
        List<string> Argument = AP3.List(args, "argument1", "passes a string into the script");
        AP3.help_message("usage","Program");
        AP3.help_message("description","a simple encryption and decryption algorithem");
        AP3.help_message("information","when a '+' is defined said string would be used as the key");
        AP3.help_message("information","created by https://github.com/000Daniel");
        if (Help)
        {
            AP3.help_message("call",string.Empty);
        }
                //Argument Parser settings end here.


                //these lines make sure that the user only used 1(one) flag.  
        int number_of_flags = Convert.ToInt32(Encrypt) + Convert.ToInt32(Decrypt);
        if (number_of_flags == 0)
        {
            ErrorCall("Please choose an operation.\nFor more information, write --help.");
        }
        if (number_of_flags > 1)
        {
            ErrorCall("Please choose only a single operation.\nFor more information, write --help.");
        }


                //the default encryption key is 1.
                //the 'result' list would contain the encrypted or decrypted message.
        int encryption_key = 1;
        List<string> result = new List<string>();

                //check if a string starts with '+'.
                //if it does this string would be used as the new encryption key.
                //  Note: numbers after '+' would be added, while letters would
                //        be converted to Bytes(decimal value) and multiplied by
                //        the existing key.
                //
                //  For example:
                //      +2A
                //  would result in:
                //      1(default value) + 2(our number) * 65(A=65)
        foreach (string single_argument in Argument)
        {
            if (single_argument.Contains("+") && single_argument.IndexOf("+") == 0)
            {
                string single_argument_short = single_argument.Substring(1);

                foreach (char x in single_argument_short)
                {
                    if ((int)x >= 48 && ((int)x) <= 57)
                    {
                        encryption_key += int.Parse(x.ToString());
                    }
                    else
                    {
                        encryption_key += (int)x;
                    }
                }
            }
        }

                //if the string does not contain a '+' in the beginning de/encrypt it.
                //take each character from each Argument that is not a flag:
                //  convert said char to int with '(int)x'.
                //  add or subtract the encryption_key value.
                //  convert the new int back to char with '(char)x'.
                //  then convert said new char to string and add it to a list(result).
        foreach (string single_argument in Argument)
        {
            if (!single_argument.Contains("+") || (single_argument.Contains("+") && single_argument.IndexOf("+") != 0))
            {
                foreach (char x in single_argument)
                {
                    if (Encrypt)
                    {
                        result.Add(((char)((int)x + encryption_key)).ToString());
                    }
                    if (Decrypt)
                    {
                        result.Add(((char)((int)x - encryption_key)).ToString());
                    }
                }
            }
        }

                //print the result of the de/encryption.
        foreach (string y in result)
        {
            Console.Write(y);
        }
        Console.WriteLine("");
    }

            //this function handles errors.
            //it prints a message colored red and exits.
    static void ErrorCall(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(String.Format("Error! {0}", message));
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Environment.Exit(0);
    }
}
//created by https://github.com/000Daniel
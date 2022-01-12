# Argument Parser 2 in C# (template)

This script accepts and interprets launch arguments. <br />
<br />
## Usage:
   make sure that you are running DOTNET 6.0, else you will need to adjust the script. <br />
   make sure that your Main function contains (string[] args). <br />
   copy the code from 'Program.cs' into your own script. <br />
   follow the examples and comments in the script to understand more about it. <br />
<br />
   to run the script simply write in console: <br />
   `dotnet run Program.cs "Hello World" -q -C` <br />
<br />
   this script also automatically generated a help page, example: <br />
   `dotnet run Program.cs --i` <br />
<br />
## Extra info:
   Note: do not copy the code from this section. its formatting might cause errors in IDEs. <br />
         instead copy the code from 'Program.cs'. <br />
   inside the Main function you can declare optional flags: <br />
   `bool <name> =  parseArgsBool(args, new string[]{ <triggers> }      , <description> );                    ` <br />
   `bool Quiet =   parseArgsBool(args, new string[]{ "-q" , "--quiet"}, "don't print the 'Output' text");` <br />
   you can also declare a positional list argument: <br />
   `List<string> <name>   = parseArgs(args, <name>      , <description> );                      ` <br />
   `List<string> Argument = parseArgs(args, "argument1", "passes a string into the script");` <br />
   Note: the positional arguments have to be declared after the optional flags have. <br />
   running this script with 'dotnet run' command will result in one of the arguments being `Program.cs`. <br />
   this issue could be fixed by building(to an executable) this script. <br />
<br />
<br />
<br />
<br />
This software was programmed in Visual Studio Code (.Net 6.0).
> Note: This software was a coding exercise, so the examples provided may not suit your needs.
<br />
<br />
Publish/Release dates: 12.01.2022

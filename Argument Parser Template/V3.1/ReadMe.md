# Argument Parser 3.1 in C# (template)

This script accepts and interprets launch arguments. <br />
<br />
## Usage:
   make sure that you are running DOTNET 6.0, else you will need to adjust the script. <br />
   make sure that your Main function contains (string[] args). <br />
   copy `Argument_Parser.cs` into your workspace folder. <br />
   copy the code from `Program.cs` into your own script. <br />
   follow the examples and comments in the script to understand more about it. <br />
<br />
   to run the script simply write in the console one of the following lines: <br />
   `dotnet run "John" 35` <br />
   `dotnet run "Joseph" 27 -q` <br />
   `dotnet run "Bob" 55 -q -C` <br />
<br />
   this script also automatically generated a help page, example: <br />
   `dotnet run --i` <br />
<br />
## Change log:
   In the help page, 'usage' automatically generates the Process name. <br />
   Added better postional Arguments support: <br />
   Now each positional argument has it's own location in the command chain, for more info <br />
   Read about positional arguments in `Program.cs` or in `Program.cs` located in `Extra` folder. <br />
   `List` positional argument can still be used without order.  <br />
<br />
   Available Data types for positional arguments:
```
      short
      ushort
      int
      uint
      long
      ulong
      float
      decimal
      double
      byte
      sbyte
      string
      char
```

## Extra info:
   Note: do not copy the code from this section. its formatting might cause errors in IDEs. <br />
         instead copy the code from 'Program.cs'. <br />
   inside the Main function you can declare optional flags: <br />
   `bool <name> =  AP3.Boolean(args, new string[]{ <triggers> }      , <description> );                    ` <br />
   `bool Quiet =   AP3.Boolean(args, new string[]{ "-q" , "--quiet"}, "don't print the Output text");` <br />
   you can also declare a positional list argument: <br />
   `int <name>            = AP3.int(args,  <name>      , <description> );` <br />
   `int int_Argument      = AP3.Int(args,  "argument-int", "passes an int32 into the script");` <br />
   `List<string> <name>   = AP3.List(args, <name>      , <description> );                      ` <br />
   `List<string> Argument = AP3.List(args, "argument1", "passes a string into the script");` <br />
   Note: the positional arguments have to be declared after the optional flags were. <br />
<br />
<br />
<br />
<br />
This software was programmed in Visual Studio Code (.Net 6.0).
> Note: This code was a coding exercise, so the examples provided may not suit your needs.
<br />
<br />
Publish/Release dates: 29.01.2022

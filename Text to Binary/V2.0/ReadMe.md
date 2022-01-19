# Text to Binary - Version 2.0

This program can convert: <br />
   Text to Binary code <br />
   Text to Decimal values <br />
   Binary code to Text <br />
   Binary code to Decimal values <br />
   Decimal values to Text <br />
   Decimal values to Binary Code <br />
<br />
## Usage:
   Available Flags: <br />
      `-h, --i, --help          shows a help page` <br />
      `-s, --includespaces      prints spaces between each character` <br />
      `-t2b, --texttobinary     convert a string to binary code` <br />
      `-t2d, --texttodecimal    convert a string to a decimal value` <br />
      `-b2t, --binarytotext     convert binary code to a string` <br />
      `-b2d, --binarytodecimal  convert binary code to a decimal value` <br />
      `-d2t, --decimaltotext    convert a decimal value to a string` <br />
      `-d2b, --decimaltobinary  convert a decimal value to binary code` <br />
   Examples: <br />
   `dotnet run --t2b "Hello World"` <br />
   `dotnet run --t2b "Hello World" -s` <br />
   `dotnet run --b2t "00111010 00101001"` <br />
   `dotnet run --t2d "Hello World" -s` <br />
   `dotnet run --b2d "01101000 01101001" -s` <br />
   `dotnet run --d2t "104 105"` <br />
   `dotnet run --d2b "104 105" -s` <br />
<br />
## Extra info:
   make sure that you are running DOTNET 6.0, else you will need to adjust the program. <br />
   follow the examples and comments in the source code to understand more about the program. <br />
   This software uses my Argument Parser 3.0, [more about that here.](https://github.com/000Daniel/CSharp-Projects/tree/main/Argument%20Parser%20Template/V3.0)
<br />
<br />
<br />
<br />
This software was programmed in Visual Studio Code (.Net 6.0).
> Note: This program was a coding exercise, so expect issues and or limitations with it.
<br />
<br />
Publish/Release dates: 19.01.2022

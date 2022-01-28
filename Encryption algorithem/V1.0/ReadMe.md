# Encryption and Decryption Algorithem

This program can encrypt or decrypt a message. <br />
<br />
## Usage:
   Available Flags: <br />
      `-h, --i, --help        shows a help page` <br />
      `-e, --encrypt          encrypts the passed string` <br />
      `-D, --decrypt          decrypts the passed string` <br />
   Examples: <br />
      `dotnet run -e "Hello World" +1` <br />
      `dotnet run -D "Jgnnq\"Yqtnf" +1` <br />
      `dotnet run -e "Hello World" +encryption-key-here` <br />
      `dotnet run -D "ߛ߸߿߿ࠂ޳ߪࠂࠅ߿߷" +encryption-key-here` <br />
<br />
## Extra info:
   some characters in the message should be written with `\` before them. <br />
   for example: `!` should be `\!`, or `"` should be `\"` <br />
   for a custom encryption key write a '+' and then the rest of the key. <br />
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
Publish/Release dates: 28.01.2022

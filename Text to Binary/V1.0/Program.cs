using System.Text;

string userInput = "";
string result = "";

Main();

void Main () {                      //asks the user what they want to convert...
    Console.Write("\n[1] Text to Binary. \n[2] Binary to Text. \n[3] Text to Bytes. \n[4] Binary to Bytes.\nEnter a number>> ");
    userInput = Console.ReadLine().ToString();
    
    string allowedChars = "1234";           //this makes sure that the user chose a valid option.
        foreach (char c in userInput) {
            if (!allowedChars.Contains(c.ToString()) || userInput.Length != 1) {
                Console.WriteLine("\nError! please choose a valid option.");
                Console.ReadKey();
                Main();
            } else {
                askUser(int.Parse(userInput));
            }
        }
}

void askUser (int userChoice) {     //gets the user's input('userInput') and converts it.
    Console.Write("Enter a string>> ");
    userInput = Console.ReadLine().ToString();
    List<string> userInputList = new List<string>();
    bool invalidChars = false;


    if (userChoice == 2 | userChoice == 4) {        //if the user converts binaries, limit the text to only "0,1, ".
        string allowedChars = "01 ";                //rest of the function is after this if statement(referred as First 'if').
        foreach (char c in userInput) {
            if (!allowedChars.Contains(c.ToString())) {
                invalidChars = true;
                break;
            }
        }
        

        while (userInput.Contains(" ") && !invalidChars) {           //adds every sequence of numbers into a list.
            int spaceIndex = userInput.IndexOf(" ");
            if (spaceIndex == 0) {
                userInput = userInput.Substring(1);
            } 
            else 
            {
                string tmpUserInput = userInput.Substring(0,spaceIndex);        //this checks that the binary code's length is valid.
                if (tmpUserInput.Length >= 6 && tmpUserInput.Length <= 8) {
                    userInputList.Add(tmpUserInput);
                    userInput = userInput.Substring(spaceIndex + 1);
                } else {
                    Console.WriteLine("\nError! please enter a valid binary code.");
                    Console.ReadKey();
                    Main();
                }
            }
        }


        if (userInput.Length > 1 && !invalidChars) {                 //adds the LAST sequence and resets the 'userInput'.
                if (userInput.Length >= 6 && userInput.Length <= 8) { //this checks that the binary code's length is valid.
                    userInputList.Add(userInput);
                    userInput = "";
                } else {
                    Console.WriteLine("\nError! please enter a valid binary code.");
                    Console.ReadKey();
                    Main();
                }
        }
    }
    if (invalidChars) {     //the First 'if' has to break/finish first before returning to the 'Main()' funcion.
                            //this is done to avoid a bug where the First 'if' would continue after failing once.
            Console.WriteLine("\nError! please enter valid characters.");
            Console.ReadKey();
            Main();
        }

    if (userChoice == 1) {              //###CONVERTS TEXT TO BINARY###
        convertString(userInput);
        Console.WriteLine(result + "\n");
    }

    if (userChoice == 2) {              //###CONVERTS BINARY TO TEXT###
        for (int i = 0; i < userInputList.Count(); i++) {       //checks if the words' length is valid.
            Console.Write(convertBinary(userInputList[i]));
        }
        Console.Write("\n");
    }

    if (userChoice == 3) {              //###CONVERTS TEXT TO BYTES###
        convertStringBytes(userInput);
        Console.WriteLine(result + "\n");
    }

    if (userChoice == 4) {              //###CONVERTS BINARY TO BYTES###
        for (int i = 0; i < userInputList.Count(); i++) {
            convertBinaryBytes(userInputList[i]);
        }
        Console.Write("\n");
    }
}

void convertString(string str) {
    result = "";
    foreach(char ch in str)
        {
            result += Convert.ToString((int)ch,2) + " ";        //this turns the character to binary.
        }
    return;
}

string convertBinary(string str){
    List<byte> bytes = new List<byte>();
    for (int i = 0; i < str.Length; i += str.Length)
        bytes.Add(Convert.ToByte(str.Substring(i, str.Length), 2));     //this converts the binary code to characters.
    return Encoding.ASCII.GetString(bytes.ToArray());
}

void convertStringBytes(string str) {
    result = "";
    foreach(char ch in str)
        {
            result += Convert.ToByte((int)ch) + " ";            //this converts text to bytes.
        }
    return;
}

void convertBinaryBytes(string str){
    List<byte> bytes = new List<byte>();
    for (int i = 0; i < str.Length; i += str.Length) {
        bytes.Add(Convert.ToByte(str.Substring(i, str.Length), 2));     //this converts the binary code to bytes.
        Console.Write(bytes[i] + " ");
    }
}
//created by https://github.com/000Daniel

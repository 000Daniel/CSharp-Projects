using System.Reflection;

        //this is a 'Person class' list.
var people_list = new List<Person>();

        //here the program finds a file named 'List of people', in the same directory
        //as the executable, and adds it's contents into a string array.
string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] loaded_config_file = File.ReadAllLines(@currentPath + "/List of people");
int config_max_lines = loaded_config_file.Count();

        //for each line in the file:
for (int i = 0; i < config_max_lines; i++)
{
        //if the line that the program is checking contains "ID:",
        //store that in a 'persons_id' string and remove the "ID:" part of it.
        //convert the left number to uint and assign it to 'add_new_person.id'
    if (loaded_config_file[i].Contains("ID:"))
    {
        string persons_id = loaded_config_file[i].Substring(loaded_config_file[i].IndexOf("ID:") + 3);
        var add_new_person = new Person();
        add_new_person.id = uint.Parse(persons_id);

        //for the next 3 lines check if they contain a Name,Last name or Age.
        //then assign to 'add_new_person' the new data accordingly.
        //if one of those lines have "ID:" start counting for a new Person().
        for (int y = 1; y < 4; y++)
        {
            if (loaded_config_file[i + y].Contains("ID:"))
            {
                y = 5;
                break;
            }

            if (loaded_config_file[i + y].Contains("Name:"))
            {
                    //gets the "Name:" line, removes the "Name:" and stores the string afterwards.
                string persons_name = loaded_config_file[i + y].Substring(loaded_config_file[i + y].IndexOf("Name:") + 5);
                    //removes all spaces in the beginning of the string.
                while (persons_name.Contains(" ") && persons_name.IndexOf(" ") == 0)
                {
                    persons_name = persons_name.Substring(1);
                }
                    //adds that name to 'add_new_person'.
                add_new_person.name = persons_name;
            }

            if (loaded_config_file[i + y].Contains("Last:"))
            {
                string persons_last_name = loaded_config_file[i + y].Substring(loaded_config_file[i + y].IndexOf("Last:") + 5);
                
                while (persons_last_name.Contains(" ") && persons_last_name.IndexOf(" ") == 0)
                {
                    persons_last_name = persons_last_name.Substring(1);
                }
                add_new_person.last_name = persons_last_name;
            }

            if (loaded_config_file[i + y].Contains("Age:"))
            {
                string persons_age = loaded_config_file[i + y].Substring(loaded_config_file[i + y].IndexOf("Age:") + 4);
                add_new_person.age = Convert.ToUInt32(persons_age);
            }
        }
        //this adds the person with ID,name,last_name and age to a list.
        people_list.Add(add_new_person);
    }
}

        //prints all people in people_list and their data.
foreach (Person x in people_list)
{
    Console.WriteLine("Name: " + x.name);
    Console.WriteLine("Last Name: " + x.last_name);
    Console.WriteLine("Age: " + x.age);
    Console.WriteLine("ID: " + x.id);
}

        //this line sorts 'people_list' by their names.
people_list = people_list.OrderBy(Person => Person.name).ToList();

Console.WriteLine("\nSorted list by names:\n");
        //prints all people's names and IDs in people_list (sorted).
foreach (Person x in people_list)
{
    Console.WriteLine("Name: {0}    ID: {1}", x.name, x.id);
}
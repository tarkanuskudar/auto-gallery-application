using System;
using System.Collections.Generic;

using System.Text;

namespace AutoGalleryApplication
{
    class Tools
    {
        static public bool Plate(string data)
        {
            //Prerequisite: The entered license plate must have a minimum of 7 and a maximum of 9 digits, the first two digits must be letters, the 6th and 7th(5th and 6th index) digits must be numbers and the 3rd digit (2nd index) of the plates must be letters.
            if (data.Length > 6 && data.Length < 10
                && IsNumber(data.Substring(0, 2))
                && IsLetter(data.Substring(2, 1)))
            {
                //If the appropriate conditions are met for plates in 11A1111 format, it is a plate.
                if (data.Length == 7 && IsNumber(data.Substring(3)))
                {
                    return true;
                }
                //It is a license plate if the appropriate conditions are met for 11AA111 and 11AA1111 format plates.
                else if (data.Length < 9 && IsLetter(data.Substring(3, 1)) && IsNumber(data.Substring(4)))
                {
                    return true;
                }
                //11AAA11 is a plate if the appropriate conditions are met for plates in 11AAA111 and 11AAA1111 formats.
                else if (IsLetter(data.Substring(3, 2)) && IsNumber(data.Substring(5)))
                {
                    return true;
                }
            }
            return false;    // If none of these conditions are met, it is not a plate.
        }



        //For the string methods to work, we got the data in string type, then we can compare the element at 0.index as char.
        //We checked it according to the value in the ASCII table.
        static public bool IsLetter(string data)
        {
            data = data.ToUpper();

            for (int i = 0; i < data.Length; i++)
            {
                int code = (int)data[i];//Get the character's value in the ASCII code table.
                if ((code >= 65 && code <= 90) != true)//method returns false if uppercase letters are entered other than the values in the ASCII table.
                {
                    return false;
                }
            }

            return true;
        }

        //foreach should be able to check its place in the ASCII table when more than one number is entered, so bool check
        // we created the value.

        static public bool IsNumber(string input)
        {
            foreach (char data in input)
            {
                if (!Char.IsNumber(data))
                {
                    return false;
                }
            }
            return true;
        }

        static public int TakeNumber(string message)
        {
            int number;

            do
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Incorrect login made, try again.");
                }

            } while (true);

        }
        static public CAR_TYPE TCarType()
        {
            Console.WriteLine("Car type: ");
            Console.WriteLine("For SUV 1");
            Console.WriteLine("For Hatchback  2");
            Console.WriteLine("For Sedan  3");

            while (true)
            {
                Console.Write("Your Choise: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return CAR_TYPE.SUV;

                    case "2":
                        return CAR_TYPE.Hatchback;

                    case "3":
                        return CAR_TYPE.Sedan;

                    default:
                        Console.WriteLine("Incorrect login made, try again.");
                        break;
                }
            }
        }
    }
}

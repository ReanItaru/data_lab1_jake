using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace data_lab1_jake
{
    class Program
    {
        static void Main(string[] args)
        {
            // variables
            List<string> flags = new List<string>();
            List<string> numberFlags = new List<string>();
            bool verbose = false;
            bool hex = false;
            bool binary = false;
            bool parity = false;
            bool set = false;
            bool unset = false;
            bool runs = false;
            bool gaps = false;
            bool converse = false;
            bool inverse = false;
            int index = 0;
            int numberIndex = -1;
            bool flagCheck = false;
            bool running = false;
            bool error = false;
            int inputNumber = 0;
            int checkNumber = 0;
            int numberIndexCheck = -1;



            //break input down into characters 
            foreach (string st in args)
            {
                flags.AddRange(Regex.Split(st, ""));
            }

            //break input down into strings for number checking
            foreach (string st in args)
            {
                numberFlags.AddRange(Regex.Split(st, " "));
            }

            //flag appropriate methods based on characters used
            foreach (string st in flags)
            {
                do
                {
                    if (index < flags.Count)
                    {
                        if (flags[index] == "-" || flagCheck)
                        {
                            if (flags[index + 1] != "")
                            {
                                flagCheck = true;
                                running = true;
                                switch (flags[index + 1].ToLower())
                                {
                                    case "v":
                                        verbose = true;
                                        break;
                                    case "h":
                                        hex = true;
                                        break;
                                    case "b":
                                        binary = true;
                                        break;
                                    case "p":
                                        parity = true;
                                        break;
                                    case "s":
                                        set = true;
                                        break;
                                    case "u":
                                        unset = true;
                                        break;
                                    case "r":
                                        runs = true;
                                        break;
                                    case "g":
                                        gaps = true;
                                        break;
                                    case "c":
                                        converse = true;
                                        break;
                                    case "i":
                                        inverse = true;
                                        break;
                                    case "-":
                                        break;
                                    default:
                                        error = true;
                                        break;
                                }
                            }
                            else
                            {
                                running = false;
                                flagCheck = false;
                            }

                        }
                        else
                        {
                            if ((index + 1) < flags.Count)
                            {
                                if (flags[index] == "" && flags[index + 1] != "")
                                {
                                    numberIndex++;
                                }
                                try
                                {
                                    inputNumber = Convert.ToInt32(numberFlags[numberIndex]);

                                    //if the code hasn't exited to the catch block, that means it is a valid number and didn't have a "-" in front of it, now checking to see if it's the only int input
                                    if (inputNumber != checkNumber && checkNumber == 0)
                                    {
                                        checkNumber = inputNumber;
                                        
                                    }
                                    else if (inputNumber != checkNumber && checkNumber != 0)
                                        error = true;
                                    else if (inputNumber == checkNumber && checkNumber != 0)
                                    {
                                        if (numberIndex != numberIndexCheck && numberIndexCheck == -1)
                                        {
                                            numberIndexCheck = numberIndex;
                                        }
                                        else if (numberIndex != numberIndexCheck && numberIndexCheck != -1)
                                        {
                                            error = true;
                                        }
                                    }
                                }
                                catch
                                {
                                    //if(index < flags.Count)
                                    if (flags[index + 1] != "")
                                    {
                                        if (char.IsDigit(flags[index + 1], 0))
                                        {
                                            error = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                index++;
                } while (running);
                if (error)
                    break;
            }

            //display to screen
            if (!error)
            {
                if (hex)
                    Hexadecimal(verbose, inputNumber);
                if (binary)
                    Binary(verbose, inputNumber);
                if (parity)
                    Parity(verbose, inputNumber);
                if (set)
                    Set(verbose, inputNumber);
                if (unset)
                    Unset(verbose, inputNumber);
                if (runs)
                    Runs(verbose, inputNumber);
                if (gaps)
                    Gaps(verbose, inputNumber);
                if (converse)
                    Converse(verbose, inputNumber);
                if (inverse)
                    Inverse(verbose, inputNumber);
            }
            else
                Console.WriteLine("An invalid input was detected, try again");
                        
            //pause if using debugger, otherwise continue if in cmd
            if (Debugger.IsAttached)
            {
                Console.Write("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
        static private void Hexadecimal(bool verb, int inputNum)
        {
            if (verb)
            {
                Console.WriteLine("Hexadecimal: 0x{0:X8}", inputNum);
            }
            else
            {
                Console.Write("0x{0:X8} ", inputNum);
            }
        }
        static private void Binary(bool verb, int inputNum)
        {

            if (verb)
            {
                Console.WriteLine("Binary: {0}",Convert.ToString(inputNum,2));
            }
            else
            {
                Console.Write("{0} ", Convert.ToString(inputNum, 2));
            }
        }
        static private void Parity(bool verb, int inputNum)
        {
            if (verb)
            {
                Console.WriteLine("Parity: {0}", (inputNum % 2));
            }
            else
            {
                Console.Write("{0}", (inputNum % 2));
            }
        }
        static private void Set(bool verb, int inputNum)
        {
            uint byMask = 0x1;
            uint tempResult = 0;
            int result = 0;            

            for (int i = 0; i < 32; i++)
            {
                tempResult = (uint)(inputNum & byMask);
                if (tempResult != 0)
                {
                    result++;
                }
                byMask <<= 1;
            }

            if (verb)
            {
                Console.WriteLine("Set: {0}", result);
            }
            else
            {
                Console.Write("{0} ", result);
            }
        }
        static private void Unset(bool verb, int inputNum)
        {

            uint byMask = 0x1;
            uint tempResult = 0;
            int result = 0;

            for (int i = 0; i < 32; i++)
            {
                tempResult = (uint)(inputNum & byMask);
                if (tempResult == 0)
                {
                    result++;
                }
                byMask <<= 1;
            }

            if (verb)
            {
                Console.WriteLine("Unset: {0}", result);
            }
            else
            {
                Console.Write("{0} ", result);
            }
        }
        static private void Runs(bool verb, int inputNum)
        {
            uint byMask = 0x1;
            uint tempResult = 0;
            int result = 0;
            bool running = false;

            for( int i = 0; i < 32; i++)
            {
                tempResult = (uint)(byMask & inputNum);
                if (tempResult != 0)
                {
                    if (!running)
                    {
                        result++;
                        running = true;
                    }
                }
                else
                    running = false;
                byMask <<= 1;
            }

            if (verb)
            {
                Console.WriteLine("Runs: {0}", result);
            }
            else
            {
                Console.Write("{0} ", result);
            }
        }       
        
        static private void Gaps(bool verb, int inputNum)
        {
            uint byMask = 0x1;
            uint tempResult = 0;
            int result = 0;
            bool running = false;

            for (int i = 0; i < 32; i++)
            {
                tempResult = (uint)(byMask & inputNum);
                if (tempResult == 0)
                {
                    if (!running)
                    {
                        result++;
                        running = true;
                    }
                }
                else
                    running = false;
                byMask <<= 1;
            }

            if (verb)
            {
                Console.WriteLine("Gaps: {0}", result);
            }
            else
            {
                Console.Write("{0} ", result);
            }
        }
        static private void Converse(bool verb, int inputNum)
        {
            uint byMask = 0xFFFFFFFF;
            uint result = 0;

            result = (uint)(inputNum ^ byMask);

            if (verb)
            {
                Console.WriteLine("Converse: {0}", result);
            }
            else
            {
                Console.Write("{0} ", result);
            }
        }
        static private void Inverse(bool verb, int inputNum)
        {
            uint mask = 0x1;
            uint leastResult = 0;
            string endResult = "";

            for(int i = 0; i < 32; i++)
            {
                leastResult = (uint)(inputNum & mask);
                endResult += leastResult == 0 ? "0" : "1";
                mask <<= 1;
            }

            if (verb)
            {
                Console.WriteLine("Inverse: {0}", endResult);
            }
            else
            {
                Console.Write("{0} ", endResult);
            }
        }
    }
}
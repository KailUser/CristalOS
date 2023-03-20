using System;    # Import Sytem library
using System.Collections.Generic;    # Import Generic library
using System.Text;    # Import text library
using Sys = Cosmos.System;    # Import Cosmos library

namespace CristalOS    # Define namespace
{
    public class Kernel : Sys.Kernel    # Define class Kernel
    {
        private List<string> mCmdHistory = new List<string>();    # Declare list of string
        private List<Process> mProcesses = new List<Process>();    # Declare list of process
        private int mNextPID = 0;    # initialize variable mNextPID with 0
        protected override void BeforeRun()    # BeforeRun function
        {
            Console.Clear();    # Clear the console
            Console.WriteLine("CristalBoot booted successfully.");    # Print message on console
        }

        protected override void Run()    # Run function
        {
            Console.Write("CristalOS >> ");    # Print message on console
            string input = Console.ReadLine();    # Take input from user
            mCmdHistory.Add(input);    # add input in mCmdHistory list

            string[] parts = input.Split(' ');    # split input string into parts
            string command = parts[0];    # collect the first part of input

            switch (command)    # switch command
            {
                case "exit":    # if command is exit
                    Console.WriteLine("Exited CristalOS.");    # print message on console
                    Cosmos.System.Power.Shutdown();    # Shutdown the sytem
                    break;

                case "cmd":    # if command is cmd
                    Console.WriteLine("Entering command prompt...");    # print message on console
                    while (true)
                    {
                        Console.Write("CristalOS (cmd) >> ");    # print message on console
                        string cmdInput = Console.ReadLine();    # take input from user
                        if (cmdInput == "exit")
                            break;

                        ExecuteCommand(cmdInput);    # Execute command
                    }
                    Console.WriteLine("Exited command prompt.");    # print message on console
                    break;

                case "top":    # if command is top
                    Console.WriteLine("PID\tName\t\tMemory Usage");    # print message on console
                    foreach (Process p in mProcesses)
                    {
                        Console.WriteLine("{0}\t{1}\t\t{2} KB", p.PID, p.Name, p.MemoryUsage);    # Print messages on console
                    }
                    break;

                case "print":    # if command is print
                    if (parts.Length > 1)
                    {
                        Console.WriteLine(string.Join(" ", parts, 1, parts.Length - 1));    # Print messages on console
                    }
                    else
                    {
                        Console.WriteLine("Error: no message specified");    # Print messages on console
                    }
                    break;

                default:    # default case
                    Console.WriteLine("Error: command not recognized");    # Print messages on console
                    break;
            }
        }

        private void ExecuteCommand(string cmdInput)    # ExecuteCommand function
        {
            string[] parts = cmdInput.Split(' ');    # split input string
            string cmd = parts[0];    # collect the first part of input
            switch (cmd)    # Switch command
            {
                case "echo":    # if command is echo
                    Console.WriteLine(string.Join(" ", parts, 1, parts.Length - 1));    # Print messages on console
                    break;

                case "exit":    # if command is exit
                    break;

                default:    # Default case
                    Console.WriteLine("Error: command not recognized");    # Print messages on console
                    break;
            }
        }
    }

    public class Process    # Define class Process
    {
        public int PID { get; set; }    # integer variable
        public string Name { get; set; }    # string variable
        public int MemoryUsage { get; set; }    # integer variable
    }
}

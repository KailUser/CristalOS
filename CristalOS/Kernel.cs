using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CristalOS
{
    public class Kernel : Sys.Kernel
    {
        private List<string> mCmdHistory = new List<string>();
        private List<Process> mProcesses = new List<Process>();
        private int mNextPID = 0;

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("CristalBoot booted successfully.");
        }

        protected override void Run()
        {
            Console.Write("CristalOS >> ");
            string input = Console.ReadLine();
            mCmdHistory.Add(input);

            string[] parts = input.Split(' ');
            string command = parts[0];

            switch (command)
            {
                case "exit":
                    Console.WriteLine("Exited CristalOS.");
                    Cosmos.System.Power.Shutdown();
                    break;

                case "cmd":
                    Console.WriteLine("Entering command prompt...");
                    while (true)
                    {
                        Console.Write("CristalOS (cmd) >> ");
                        string cmdInput = Console.ReadLine();
                        if (cmdInput == "exit")
                            break;

                        ExecuteCommand(cmdInput);
                    }
                    Console.WriteLine("Exited command prompt.");
                    break;

                case "top":
                    Console.WriteLine("PID\tName\t\tMemory Usage");
                    foreach (Process p in mProcesses)
                    {
                        Console.WriteLine("{0}\t{1}\t\t{2} KB", p.PID, p.Name, p.MemoryUsage);
                    }
                    break;

                case "print":
                    if (parts.Length > 1)
                    {
                        Console.WriteLine(string.Join(" ", parts, 1, parts.Length - 1));
                    }
                    else
                    {
                        Console.WriteLine("Error: no message specified");
                    }
                    break;

                default:
                    Console.WriteLine("Error: command not recognized");
                    break;
            }
        }

        private void ExecuteCommand(string cmdInput)
        {
            string[] parts = cmdInput.Split(' ');
            string cmd = parts[0];
            switch (cmd)
            {
                case "echo":
                    Console.WriteLine(string.Join(" ", parts, 1, parts.Length - 1));
                    break;

                case "exit":
                    break;

                default:
                    Console.WriteLine("Error: command not recognized");
                    break;
            }
        }
    }

    public class Process
    {
        public int PID { get; set; }
        public string Name { get; set; }
        public int MemoryUsage { get; set; }
    }
}

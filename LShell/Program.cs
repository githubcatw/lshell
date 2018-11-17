using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace LShell
{
    class Program
    {
        /// <summary>
        /// Is module installed?
        /// </summary>
        /// <param name="modName">Name of module without extension.</param>
        /// <returns>Is module installed?</returns>
        bool ModuleInstalled(string modName)
        {
            string dir = Directory.GetCurrentDirectory() + "/modules/" + modName;
            if (File.Exists(dir + ".exe") || File.Exists(dir + ".bat") || File.Exists(dir + ".cmd")) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Get a module's full path.
        /// </summary>
        /// <param name="modName">Name of module without extension.</param>
        /// <returns>The module full path.</returns>
        string ModuleFullPath(string modName)
        {
            string dir = Directory.GetCurrentDirectory() + @"\modules\" + modName;
            if (File.Exists(dir + ".exe"))
            {
                return dir + ".exe";
            }
            else if (File.Exists(dir + ".bat"))
            {
                return dir + ".bat";
            }
            else if (File.Exists(dir + ".cmd"))
            {
                return dir + ".cmd";
            }
            else
            {
                return "no such module exists";
            }
        }

        static void Main(string[] args)
        {
            // this is used to execute non static Display var
            Program pg = new Program();
            pg.Display();
        }

        /// <summary>
        /// Display UI.
        /// </summary>
        void Display()
        {
            string pcname = Environment.GetEnvironmentVariable("computername");
            string username = Environment.GetEnvironmentVariable("username");
            Console.Title = username + "@" + pcname;
            Console.Write(username + "@" + pcname + ":" + Directory.GetCurrentDirectory() + "$ ");
            string s = Console.ReadLine();
            Process(s);
        }

        /// <summary>
        /// Process input.
        /// </summary>
        /// <param name="s">input</param>
        void Process(string s)
        {
            //Console.WriteLine(ModuleInstalled(s));
            if (ModuleInstalled(s))
            {
                RunModule(s);
            } else
            {
                RunCommand(s);
            }
            Display();
        }

        /// <summary>
        /// Runs a module.
        /// </summary>
        /// <param name="mod">Module name.</param>
        /// <see cref="RunCommand(string)"/> for running commands.
        void RunModule(string mod)
        {
            //Console.WriteLine("RunModule was executed with this module: " + mod + " (" + ModuleFullPath(mod) + ")");
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = ModuleFullPath(mod);
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            Console.WriteLine(output);
        }

        /// <summary>
        /// Runs a command.
        /// </summary>
        /// <param name="cmd">Command name.</param>
        /// <see cref="RunModule(string)"/> for running modules.
        void RunCommand(string cmd)
        {
            //Console.WriteLine("RunCommand was executed with this command: " + cmd);
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C " + cmd;
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            Console.WriteLine(output);
        }
    }
}

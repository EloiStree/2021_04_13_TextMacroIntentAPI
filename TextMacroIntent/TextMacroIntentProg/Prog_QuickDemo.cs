using System;
using System.Collections.Generic;
using System.Text;
using TextMacroIntent;

namespace TextMacroIntentProg
{
    public class Prog_QuickDemo
    {
        public static I_TextMacroInputAll textInput;
   

        static void Main(string[] args)
        {
            TextMacroIntentWithCSThread thread = new TextMacroIntentWithCSThread(out textInput, 1);


            Interpreter_ConsoleDebugger debug = new Interpreter_ConsoleDebugger();
            Interpreter_RubixCubeCommands rubix = new Interpreter_RubixCubeCommands();
            rubix.AddRotationListener(UserRequestedRotation);
            debug.AddConsoleMessageDebugger(DebugMessage);

            textInput.AddInterpreter(rubix);
            textInput.AddInterpreter(debug);

            textInput.ExecuteAt(new CommandLine("debuglog at now + 10s"), DateTime.Now.AddSeconds(10.0));
            textInput.ExecuteIn(new CommandLine("debuglog in now + 14s"), 15000);

            Console.WriteLine("Hello World!");
            bool doublethread = false;
            start = DateTime.Now;
            while (true)
            {
                Console.WriteLine("Enter command:");
                string text = Console.ReadLine();
                Console.WriteLine("+>" + text);
                textInput.Execute(text);

            }
        }



      
        static DateTime start = DateTime.Now;
        private static void DebugMessage(string message)
        {
            double t = (DateTime.Now - start).TotalMilliseconds;
            Console.WriteLine(string.Format("LOG{0:000000}|{1}", (uint)t, message));
        }

        private static void UserRequestedRotation(Interpreter_RubixCubeCommands.RotationRequested request)
        {
            Console.WriteLine("RUBIX|" + request.ToString());
        }

    }
}

using System;
using System.Threading;
using TextMacroIntent;

namespace TextMacroIntentProg
{
    class Program
    {
        public static CommandRelayByTimeSubstraction relayIn;
        public static CommandRelayAtTime relayAt;
        public static LoopCountCollection<I_CommandLine> loopCollection;
        public static LoopCountCollection<I_InterpretorCompiledAction> loopCompiledCollection;

        public static LoopDateTimeCollection<I_InterpretorCompiledAction> timeLopperCollection;
        public static LoopDateTimeBean<I_InterpretorCompiledAction> loopTest;


        static void Main(string[] args)
        {



            Interpreter_RubixCubeCommands rubix = new Interpreter_RubixCubeCommands();
            Interpreter_ConsoleDebugger debug = new Interpreter_ConsoleDebugger();
            TextMacroIntentDefault textToIntent = new TextMacroIntentDefault();
            textToIntent.AddInterpreter(rubix);
            textToIntent.AddInterpreter(debug);


            loopCollection = new LoopCountCollection<I_CommandLine>();
            LoopCountBean<I_CommandLine> loopBean = new LoopCountBean<I_CommandLine>(6000, new CommandLine("debuglog yo"));
            loopCollection.AddLoopersListener(textToIntent.GetDirectExecutor().Execute);
            loopCollection.AddLoop(ref loopBean);
            loopBean.SetLoopAsActive(true);

            loopCompiledCollection = new LoopCountCollection<I_InterpretorCompiledAction>();
            loopCompiledCollection.AddLoopersListener(ExecuteCompiledBean);
            I_InterpretorCompiledAction compiledAction = textToIntent.GetCompiledAccessTo(new CommandLine("debuglog compiled yo"));
            if (compiledAction != null) { 
                LoopCountBean<I_InterpretorCompiledAction> loopCompiledBean;
                loopCompiledCollection.CreateLoop(10000, compiledAction, out loopCompiledBean);
                loopCompiledBean.SetLoopAsActive(true);
            }
            timeLopperCollection = new LoopDateTimeCollection<I_InterpretorCompiledAction>();
            LoopDateTimeBean<I_InterpretorCompiledAction>  c;
            timeLopperCollection.CreateLoop(10000, compiledAction, out c);
            timeLopperCollection.AddLoopersListener(ExecuteCompiledBean);
            c.SetLoopAsActive(true);
            //loopTest = new LoopDateTimeBean<I_InterpretorCompiledAction>(10000, compiledAction);
            //loopTest.SetLoopAsActive(true);


            CommandLineRelayDefault relay = new CommandLineRelayDefault();
            relay.AddListener(textToIntent.GetDirectExecutor().Execute);
            relayIn = new CommandRelayByTimeSubstraction(relay);
            relayIn.AddCmdCountdownIn(1000, "debuglog 1 second");
            relayIn.AddCmdCountdownIn(5000, "debuglog 5 second");
            relayIn.AddCmdCountdownIn(10000, "debuglog 10 second");
            relayIn.AddCmdCountdownIn(60000, "debuglog 60 second");
            relayIn.AddCmdCountdownIn(170000, "debuglog 170 second");
            relayIn.AddCmdCountdownIn(180000, "debuglog 180 second");


            I_BlackBoxTime lune = new DelayDateTime(9000, DateTime.Now);
            relay = new CommandLineRelayDefault();
            relay.AddListener(textToIntent.GetDirectExecutor().Execute);
            relayAt = new CommandRelayAtTime(relay);
            relayAt.PushIn(DateTime.Now.AddSeconds(3), "debuglog at+ 3 second");
            relayAt.PushIn(DateTime.Now.AddSeconds(15),
                           "debuglog at+ 15 second");
            relayAt.PushIn(lune, "debuglog at+ lune received messsage");







            rubix.AddRotationListener(UserRequestedRotation);
            debug.AddConsoleMessageDebugger(DebugMessage);

            I_InterpretorCompiledAction cTestRubix = rubix.TryToGetCompiledAction(
                new CommandLine( "rubix:if"));

            cTestRubix.Execute();

            Console.WriteLine("Hello World!");
            bool doublethread=false;
            start = DateTime.Now;
            if (doublethread)
            {
                ThreadPool.QueueUserWorkItem(CheckTimeDependence, (int)1);

                while (true)
                {
                    Console.WriteLine("Enter command:");
                    string text = Console.ReadLine();
                    Console.WriteLine("+>" + text);
                    textToIntent.GetDirectExecutor().Execute(text);

                }
            }
            else { 
                CheckTimeDependence(1);
            }
        }

        private static void ExecuteCompiledBean(I_InterpretorCompiledAction cmd)
        {
            if (cmd != null)
                cmd.Execute();
        }

      

        private static void CheckTimeDependence(object state)
        {
            start = DateTime.Now;

            int t = (int)state;

            while (true)
            {

                //if (relayIn != null)
                //    relayIn.CheckHolderThatAreFinishSinceLastTime();
                //if (relayAt != null)
                //    relayAt.CheckHolderThatAreFinishSinceLastTime();
                //if (loopCollection != null)
                //    loopCollection.CheckHolderThatAreFinishSinceLastTime();
                //if (loopCompiledCollection != null)
                //    loopCompiledCollection.CheckHolderThatAreFinishSinceLastTime();
                if (timeLopperCollection != null)
                    timeLopperCollection.CheckHolderThatAreFinishSinceLastTime();

                //if (loopTest != null) {
                //    int ping;
                //    loopTest.RefreshAndCountPing(out ping);
                //    if (ping > 0) DebugMessage("Humm");
                //}
                Thread.Sleep(t);
            }
        }
       static DateTime start = DateTime.Now;
        private static void DebugMessage(string message)
        {
            double t = (DateTime.Now - start).TotalMilliseconds;
            Console.WriteLine(string.Format("LOG{0:000000}|{1}" ,(uint)t, message));
        }

        private static void UserRequestedRotation(Interpreter_RubixCubeCommands.RotationRequested request)
        {
            Console.WriteLine("RUBIX|" + request.ToString());
        }

        private static void Ping(object state)
        {
            float t = (float)state;

            while (true)
            {

                Console.WriteLine("Ping");
                Thread.Sleep((int)(t * 1000f));
            }
        }
    }
}

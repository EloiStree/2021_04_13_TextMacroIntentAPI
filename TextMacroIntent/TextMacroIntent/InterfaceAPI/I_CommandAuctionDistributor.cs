using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TextMacroIntent
    
    ;

namespace TextMacroIntent
{
    public interface I_CommandLine
    {
        string GetLine();
    }
    public interface I_CommandLineEnumList
    {
        IEnumerable<I_CommandLine> GetLines();
    }

    public interface I_Interpreter {

        bool CanInterpreterUnderstand(ref I_CommandLine command);
        void TranslateToActionsWithStatus(ref I_CommandLine command, ref I_ParsingStatus succedToExecute);
        I_InterpretorCompiledAction TryToGetCompiledAction(I_CommandLine commandToExecute);
        I_InterpretorCompiledAction TryToGetCompiledAction(I_CommandLine commandToExecute, ref I_ParsingStatus succedToExecute);
    }



    public interface I_InterpretorDirectAccess
    {
        I_Interpreter GetInterpreter();
        void TryToTranslateAndExecute(I_CommandLine command);
        void TryToTranslateAndExecute(I_CommandLine command, ref I_ParsingStatus result);
    }

    public interface I_InterpreterMetaInformation {
        string GetName();
        string WhatWillYouDoWith(ref I_CommandLine command);
        string[] GetExamplesOfExpectedCommandsFormat();
        string GetUrlOfDocumetationStartPoint();
        string GetDeveloperContactInfo();

    }


    /// <summary>
    /// This inteface has a double aim be able to test the interpreter and document the way to use the Intepreter
    /// </summary>
    public interface I_InterpreterMetaTDD
    {
        IEnumerable<I_CommandLine> GetPerfectCommandLine();
        IEnumerable<I_CommandLine> GetAlmostPerfectCommandLine();
        IEnumerable<I_CommandLine> GetShouldWorkCommandLine();
        IEnumerable<I_CommandLine> GetCouldWorkCommandLine();
        IEnumerable<I_CommandLine> GetShouldNotWorkCommandLine();
    }

    /// <summary>
    /// Allow to ask interpretor to compiled if possible a command or to just have a direct access with command ready to be pushed to it if not support.
    /// </summary>
    public interface I_InterpretorCompiledAccess : I_InterpretorDirectAccess
    {

        I_CommandLine GetLinkedCommandLine();
        void Execute();
        void Execute(ref I_ParsingStatus status);
    }
    public interface I_InterpretorEnumCompiledAccess
    {

        IEnumerable<I_CommandLine> GetLinkedCommandLines();
        IEnumerable<I_InterpretorCompiledAccess> GetLinkedCompiledCommandLines();
        void Execute();
        void Execute(ref I_ParsingStatus status);
    }

    public interface I_InterpretorEnumCompiledAction
    {

        IEnumerable<I_InterpretorCompiledAction> GetLinkedCompiledCommandLines();
        void Execute();
        void Execute(ref I_ParsingStatus status);
    }


    /// <summary>
    /// If take in charge, code that is ready to be executed based on a commandline previously given.
    /// </summary>
    public interface I_InterpretorCompiledAction
    {
        void Execute();
        void Execute(ref I_ParsingStatus status);
    }



    public interface I_CommandLineAt
    {
         I_CommandLine GetCommand();
         I_BlackBoxTime GetWhenToExecute();
    }
    /// <summary>
    /// I don't want to know what this time but give me a time proper to this program that I need to execute.
    /// Why Blackbox ? Because I don't care of what happenen behind.
    /// In aim to later be able to synchronise  time between computers
    /// </summary>
    public interface I_BlackBoxTime {
        DateTime GetTime();
    }


    public interface I_CommandAuctionDistributor{
       
        bool SeekForFirstTaker(I_CommandLine command,out bool foundInterpreter, out I_Interpreter interpreter);
        void AddInterpreter(I_Interpreter interpreter);
        void RemoveInterpreter(I_Interpreter interpreter);

    }

    public interface I_ThreadDependantGroup {
        I_ThreadDependant [] GetThreadDependancies();
    }
    public interface I_ThreadDependant
    {
        void ToIncludeInLoopThreadToWork();
    }
    public interface I_CoroutineDependant
    {
        void ExecuteInParallel(IEnumerator toExecuteInParallel);
    }

   
    public interface I_CommandLineCoroutineExecutor
    {
        //SHOULD I CODE THIS ONE....
         void ExecuteOnAfterAnOther(IEnumerable<I_CommandLine> commandLines, out I_ParsingStatus exeStatus);
        
    }

    public interface I_CommandLineAuctionToExecutor : I_CommandLineDirectExecutor
    {
        public void AddInterpreter(I_Interpreter rubix);
        public void RemoveInterpreter(I_Interpreter rubix);
        public void FindInterpreter(I_CommandLine command, out bool found, out I_Interpreter interpreter);
        public I_InterpretorCompiledAction GetCompiledAccessTo(I_CommandLine commandLine);

    }


    public interface I_CommandLineRelay {

         void Push(I_CommandLine commandLine);
         void Push(I_CommandLineEnumList commandLines);
    }

    public interface I_ParsingStatus
    {
        bool HasFinish();
        bool HasSucced();
        bool HadError();
        string GetErrorInformation();
        void SetAsFinish(bool succed);
        void SetAsFail(string message = "");
    }


    public interface I_CommandLineDirectExecutor
    {

        void Execute(string commandLine);
        void Execute(string[] commandLine);
        void Execute(I_CommandLine commandLine);
        void Execute(IEnumerable<I_CommandLine> commandLines);
        void Execute(I_CommandLineEnumList commandLines);

    }
 
    public interface I_CommandLineDirectExecutorWithReturn
    {

        void Execute(string commandLine, out I_ParsingStatus exeStatus);
        void Execute(string[] commandLine, out I_ParsingStatus exeStatus);
        void Execute(I_CommandLine commandLine, out I_ParsingStatus exeStatus);
        void Execute(IEnumerable<I_CommandLine> commandLines, out I_ParsingStatus exeStatus);
        void Execute(I_CommandLineEnumList commandLines, out I_ParsingStatus exeStatus);

    }
    public interface I_CommandLineDelayExecutor
    {
        void ExecuteAt(I_CommandLine commandLine, I_BlackBoxTime when);
        void ExecuteAt(I_CommandLine commandLine, DateTime when);
        void ExecuteIn(I_CommandLine commandLine, float milliseconds);
    }

    public interface I_CommandLineComplexDelayExecutor {


        //IF IT IS RISKY TO CONSUME RESOURCE OF THE THREAD... THAT SHOULD BE MANAGE BY THE DEVELOPPER
        //public void SetLoop(string loopName,I_CommandLine cmd, bool onState);
        //public void RemoveLoop(string loopName);


        public void SendRicochetNow(I_CommandLine cmd, params uint[] timeFromStartInMilliseconds);
        public void SendRicochet(I_CommandLine cmd, DateTime timeStart, params uint[] timeFromStartInMilliseconds);

    }





    public interface I_CommandLineCompilated 
    {

        void Compile(string command, out I_InterpretorCompiledAction compilationRegistered);
        void Compile(I_CommandLine command, out I_InterpretorCompiledAction compilationRegistered);
        void Compile(string command, out I_InterpretorCompiledAccess compilationRegistered);
        void Compile(I_CommandLine command, out I_InterpretorCompiledAccess compilationRegistered);
       
        void Compile(I_CommandLineEnumList commandLines, out I_InterpretorEnumCompiledAction compilationRegistered);
        void Compile(I_CommandLineEnumList commandLines, out I_InterpretorEnumCompiledAccess compilationRegistered);

    }


}

using System;
using System.Collections.Generic;
using System.Text;
using TextMacroIntent.Core.CommandLineType;

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
        void TranslateToActionsWithStatus(ref I_CommandLine command, ref I_ExecutionStatus succedToExecute);
        I_InterpretorCompiledAction TryToGetCompiledAction(I_CommandLine m_commandToExecute);
    }



    public interface I_InterpretorDirectAccess
    {
        I_Interpreter GetInterpreter();
        void TryToTranslateAndExecute(I_CommandLine command);
        void TryToTranslateAndExecute(I_CommandLine command, ref I_ExecutionStatus result);
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
        void Execute(ref I_ExecutionStatus status);
    }


    /// <summary>
    /// If take in charge, code that is ready to be executed based on a commandline previously given.
    /// </summary>
    public interface I_InterpretorCompiledAction
    {
        void Execute();
        void Execute(ref I_ExecutionStatus status);
    }



    public interface I_CommandLineAt
    {
        public I_CommandLine GetCommand();
        public I_BlackBoxTime GetWhenToExecute();
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
    public interface I_CommandLineWaiterExecutor
    {

        public void Execute(string commandLine, out I_ExecutionStatus exeStatus);
        public void Execute(string[] commandLine, out I_ExecutionStatus exeStatus);
        public void Execute(I_CommandLine commandLine, out I_ExecutionStatus exeStatus);
        public void Execute(IEnumerable<I_CommandLine> commandLines, out I_ExecutionStatus exeStatus);
        public void Execute(I_CommandLineEnumList commandLines, out I_ExecutionStatus exeStatus);

    }
    public interface I_CommandLineDirectExecutor
    {

        public void Execute(string commandLine );
        public void Execute(string[] commandLine);
        public void Execute(I_CommandLine commandLine);
        public void Execute(IEnumerable<I_CommandLine> commandLines);
        public void Execute(I_CommandLineEnumList commandLines);

    }
    public interface I_CommandLineExecutorFrequently : I_CommandLineDirectExecutor
    {
        public void Compile(string command);
        public void Compile(I_CommandLine command);
        public void Compile(I_CommandLineEnumList commandLines);

        public void Compile(string command, out I_InterpretorCompiledAction compilationRegistered);
        public void Compile(I_CommandLine command, out I_InterpretorCompiledAction compilationRegistered);
        public void Compile(I_CommandLineEnumList commandLines, out I_InterpretorCompiledAction compilationRegistered);

    }


    public interface I_CommandLineRelay {

        public void Push(I_CommandLine commandLine);
    }






    public interface I_ExecutionStatus
    {
        bool HasFinish();
        bool HasSucced();
        bool HadError();
        string GetErrorInformation();
        void SetAsFinish(bool succed);
        void SetAsFail(string message = "");
    }
}

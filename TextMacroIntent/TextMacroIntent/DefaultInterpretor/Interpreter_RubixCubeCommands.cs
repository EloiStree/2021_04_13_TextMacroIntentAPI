using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class Interpreter_RubixCubeCommands : AbstractInterpreter
    {
        public RotationRequestedCall m_request;



        public void AddRotationListener(RotationRequestedCall listener)
        {
            m_request += listener;
        }
        public void RemoveRotationListener(RotationRequestedCall listener)
        {
            m_request -= listener;
        }

        public override bool CanInterpreterUnderstand(ref I_CommandLine command)
        {
            return StartWith(ref command, "rubix:");
        }



        public override void TranslateToActionsWithStatus(ref I_CommandLine command, ref I_ParsingStatus succedToExecute)
        {

            string[] token = Split(command.GetLine(),":");
            RotationRequested rotation;
            bool found;
            GetRotationFromTokens(token, out found, out rotation);
            if (found)
            {

                if (succedToExecute != null)
                    succedToExecute.SetAsFinish(true);
                if (m_request != null)
                {
                    m_request(rotation);
                }
            }
            else {

                if (succedToExecute != null)
                    succedToExecute.SetAsFinish(false);
            }

        }


        private void GetRotationFromTokens(string[] token, out bool found, out RotationRequested rotationResult)
        {
            rotationResult = null;
            found = false;
            RubixCubeRotationPossible rotation;
            if (token.Length > 1 && token[0].Trim().ToLower() == "rubix")
            {
                PerspectiveType local = PerspectiveType.FromOrigine;

                if (token.Length == 2)
                {
                    bool converted;
                    GetRotationFrom(token[1], out converted, out rotation);
                    if (converted)
                    {
                        rotationResult = new RotationRequested(local, rotation);
                        found = true;
                    }

                }
                if (token.Length == 3)
                {
                    bool converted;
                    GetRotationFrom(token[2], out converted, out rotation);
                    GetPointOfView(token[1], out local);
                    if (converted)
                    {
                        rotationResult = new RotationRequested(local, rotation);
                        found = true;
                    }

                }

            }

        }

        private void GetPointOfView(string text, out PerspectiveType pointOfView)
        {
            if (text == "g" || text == "global" ||
                text == "c" || text == "camera" ||
                text == "d" || text == "display") { pointOfView = PerspectiveType.FromDisplayView; return; }
            pointOfView = PerspectiveType.FromOrigine;

        }

        private void GetRotationFrom(string text, out bool converted, out RubixCubeRotationPossible rotation)
        {
            converted = true;
            rotation = RubixCubeRotationPossible.F;
            switch (text.ToLower())
            {
                case "f": rotation = RubixCubeRotationPossible.F; break;
                case "r": rotation = RubixCubeRotationPossible.R; break;
                case "u": rotation = RubixCubeRotationPossible.U; break;
                case "b": rotation = RubixCubeRotationPossible.B; break;
                case "l": rotation = RubixCubeRotationPossible.L; break;
                case "d": rotation = RubixCubeRotationPossible.D; break;

                case "if": rotation = RubixCubeRotationPossible.iF; break;
                case "ir": rotation = RubixCubeRotationPossible.iR; break;
                case "iu": rotation = RubixCubeRotationPossible.iU; break;
                case "ib": rotation = RubixCubeRotationPossible.iB; break;
                case "il": rotation = RubixCubeRotationPossible.iL; break;
                case "id": rotation = RubixCubeRotationPossible.iD; break;

                case "face": rotation = RubixCubeRotationPossible.F; break;
                case "right": rotation = RubixCubeRotationPossible.R; break;
                case "up":      rotation = RubixCubeRotationPossible.U; break;
                case "back": rotation = RubixCubeRotationPossible.B; break;
                case "left": rotation = RubixCubeRotationPossible.L; break;
                case "down": rotation = RubixCubeRotationPossible.D; break;

                case "counterface": rotation = RubixCubeRotationPossible.iF; break;
                case "counterright": rotation = RubixCubeRotationPossible.iR; break;
                case "counterup": rotation = RubixCubeRotationPossible.iU; break;
                case "counterback": rotation = RubixCubeRotationPossible.iB; break;
                case "counterleft": rotation = RubixCubeRotationPossible.iL; break;
                case "counterdown": rotation = RubixCubeRotationPossible.iD; break;

                case "!face": rotation = RubixCubeRotationPossible.iF; break;
                case "!right": rotation = RubixCubeRotationPossible.iR; break;
                case "!up": rotation = RubixCubeRotationPossible.iU; break;
                case "!back": rotation = RubixCubeRotationPossible.iB; break;
                case "!left": rotation = RubixCubeRotationPossible.iL; break;
                case "!down": rotation = RubixCubeRotationPossible.iD; break;

                case "cf": rotation = RubixCubeRotationPossible.iF; break;
                case "cr": rotation = RubixCubeRotationPossible.iR; break;
                case "cu": rotation = RubixCubeRotationPossible.iU; break;
                case "cb": rotation = RubixCubeRotationPossible.iB; break;
                case "cl": rotation = RubixCubeRotationPossible.iL; break;
                case "cd": rotation = RubixCubeRotationPossible.iD; break;

                case "nf": rotation = RubixCubeRotationPossible.iF; break;
                case "nr": rotation = RubixCubeRotationPossible.iR; break;
                case "nu": rotation = RubixCubeRotationPossible.iU; break;
                case "nb": rotation = RubixCubeRotationPossible.iB; break;
                case "nl": rotation = RubixCubeRotationPossible.iL; break;
                case "nd": rotation = RubixCubeRotationPossible.iD; break;

                case "!f": rotation = RubixCubeRotationPossible.iF; break;
                case "!r": rotation = RubixCubeRotationPossible.iR; break;
                case "!u": rotation = RubixCubeRotationPossible.iU; break;
                case "!b": rotation = RubixCubeRotationPossible.iB; break;
                case "!l": rotation = RubixCubeRotationPossible.iL; break;
                case "!d": rotation = RubixCubeRotationPossible.iD; break;
                default:
                    converted = false;
                    break;
            }
        }

        public override I_InterpretorCompiledAction TryToGetCompiledAction(I_CommandLine command)
        {
            string[] token =  Split(command.GetLine(), ":"); 
            RotationRequested rotation;
            bool found;
            GetRotationFromTokens(token, out found, out rotation);
            if (found && rotation != null)
            {
                return new CompiledRotationRequest(this, rotation);
            }
            return null;
        }
       

        public void Push(RotationRequested m_rotation)
        {
            if (m_request != null && m_rotation != null)
                m_request(m_rotation);
        }

      

        public class CompiledRotationRequest : I_InterpretorCompiledAction
        {
            public Interpreter_RubixCubeCommands m_source;
            public RotationRequested m_rotation;

            public CompiledRotationRequest(Interpreter_RubixCubeCommands source, RotationRequested rotation)
            {
                m_source = source;
                m_rotation = rotation;
            }

            public void Execute()
            {
                I_ParsingStatus s = null;
                Execute(ref s);
            }

            public void Execute(ref I_ParsingStatus status)
            {
                try
                {

                    m_source.Push(m_rotation);
                    if (status != null)
                        status.SetAsFinish(true);

                }
                catch (Exception e)
                {
                    if (status != null)
                        status.SetAsFail(e.StackTrace);
                }
            }
        }

        public class RotationRequested
        {
            public PerspectiveType m_pointOfView;
            public RubixCubeRotationPossible m_rotation;

            public RotationRequested(PerspectiveType pointOfView, RubixCubeRotationPossible rotation)
            {
                m_pointOfView = pointOfView;
                m_rotation = rotation;
            }

            public override string ToString()
            {
                return string.Format("[RR:{0},{1}]", m_pointOfView, m_rotation);
            }
        }

        public enum PerspectiveType { FromOrigine, FromDisplayView }
        public enum RubixCubeRotationPossible
        {
            F, R, U, B, L, D, iF, iR, iU, iB, iL, iD
        }

        public delegate void RotationRequestedCall(RotationRequested request);
    }
}

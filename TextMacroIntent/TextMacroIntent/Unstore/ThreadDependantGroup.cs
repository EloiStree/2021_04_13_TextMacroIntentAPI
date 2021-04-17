using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent.Unstore
{
    public class ThreadDependantGroup : I_ThreadDependantGroup, I_ThreadDependant
    {
        public List<I_ThreadDependant> m_threadDependancies = new List<I_ThreadDependant>();
        public void Add(params I_ThreadDependant[] dependancies) {
            for (int i = 0; i < dependancies.Length; i++)
            {
                Add(dependancies[i]);
            }
        }
        public void Add(I_ThreadDependant dependancy) { m_threadDependancies.Add(dependancy); }
        public void Remove(I_ThreadDependant dependancy) { m_threadDependancies.Remove(dependancy); }

        public I_ThreadDependant [] GetThreadDependancies()
        {
            return m_threadDependancies.ToArray();
        }


        public void ToIncludeInLoopThreadToWork()
        {
            for (int i = 0; i < m_threadDependancies.Count ; i++)
            {
                m_threadDependancies[i].ToIncludeInLoopThreadToWork();
            }
        }
    }
}

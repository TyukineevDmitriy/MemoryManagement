using System;
using System.Diagnostics;

namespace MemoryManagement
{
    public class Timer : Stopwatch, IDisposable
    {
        public Timer StartOver()
        {
            Reset();
            Start();
            return this;
        }
        public void Dispose()
        {
            Stop();
        }
        public Timer Continue()
        {
            Start();
            return this;
        }
    }
}

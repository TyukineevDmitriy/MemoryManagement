using System;
using System.Diagnostics;

namespace MemoryManagement
{
    public class Timer : Stopwatch, IDisposable
    {
        public new Timer Start()
        {
            base.Start();
            return this;
        }
        public void Dispose()
        {
            Stop();
        }
        public Timer Continue()
        {
            base.Start();
            return this;
        }
    }
}

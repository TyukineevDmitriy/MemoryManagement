using System;
using System.Diagnostics;

namespace MemoryManagement
{
    public class Timer : IDisposable
    {
        private Stopwatch Stopwatch;
        public long ElapsedMilliseconds { get; set; }

        public Timer Start()
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            return this;
        }
        public void Dispose()
        {
            ElapsedMilliseconds = Stopwatch.ElapsedMilliseconds;
            Stopwatch.Stop();
        }
        public Timer Continue()
        {
            Stopwatch.Start();
            return this;
        }
    }
}

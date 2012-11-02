using System;
using System.Threading;

namespace GeoAPI.DataStructures
{
    /// <summary>
    /// A thread synchronization lock which tries to wait on the contended resource by 
    /// spinning the waiting thread in user-mode, but will yield execution of the waiting thread
    /// if the wait is too long (2500 cycles).
    /// </summary>
    /// <remarks>
    /// WARNING: This lock is not reentrant - calling <see cref="Enter"/> twice without calling <see cref="Exit"/>
    /// in-between on the same thread will deadlock the thread.
    /// </remarks>
    public class YieldingSpinLock
    {
        private const Int32 Free = 0;
        private const Int32 Owned = 1;

        private static readonly Boolean IsSingleCpuMachine = Environment.ProcessorCount == 1;

        // The state is not marked with 'volatile' since only atomic methods
        // are used to read and write it.
        private Int32 _state;

        /// <summary>
        /// Acquires the lock if it is free, or waits on it if it is not.
        /// </summary>
        /// <remarks>
        /// The waiting thread will spin (by calling <see cref="Thread.SpinWait"/>) for at most 2500 cycles,
        /// in steps of 10 cycles, at which point the thread is forced to yield execution with 
        /// <see cref="Thread.Sleep(int)"><c>Thread.Sleep(1)</c></see>.
        /// </remarks>
        public void Enter()
        {
            Thread.BeginCriticalRegion();

            // Spin wait loop
            while (true)
            {
                // If Free, set to Owned and return
                if (Interlocked.Exchange(ref _state, Owned) == Free)
                {
                    return;
                }

                Int64 _loopCount = 0;

                // If Owned, spin by reading state and checking for Free
                while (Thread.VolatileRead(ref _state) != Free)
                {
                    // Spin if we have a multi-CPU system, and we haven't been spinning for long
                    // otherwise, yield the thread in a safe way (N.B. that SwitchToThread() in Kernel32 
                    // isn't safe for a hosted CLR, like in SQL Server)
                    if (IsSingleCpuMachine || Interlocked.Increment(ref _loopCount) % 250 == 0)
                    {
                        // Note: Waiting for Thread.Yield...
                        // See: http://www.bluebytesoftware.com/blog/PermaLink,guid,1c013d42-c983-4102-9233-ca54b8f3d1a1.aspx
                        Thread.Sleep(1);
                    }
                    else
                    {
                        Thread.SpinWait(10);
                    }
                }
            }
        }

        /// <summary>
        /// Releases the lock.
        /// </summary>
        public void Exit()
        {
            Interlocked.Exchange(ref _state, Free);
            Thread.EndCriticalRegion();
        }
    }

}

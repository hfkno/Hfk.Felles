using System;

namespace Hfk.Felles.Environment
{
    /// <summary>
    ///     Tracks the memory usage of an application over time, reporting the max memory usage.
    /// </summary>
    public class MemoryUsage
    {
        /// <summary>
        ///     Creates a new instance of the memory usage tracker.
        /// </summary>
        public MemoryUsage() : this("Memory Usage Tracker")
        {
        }

        /// <summary>
        ///     Creates a named memory usage tracker.
        /// </summary>
        /// <param name="name"></param>
        public MemoryUsage(string name)
        {
            Name = name;
            MemUsedStart = TotalMemoryUsed;
            MemUsedMax = MemUsedStart;
        }

        /// <summary>
        ///     The name of the tracker.
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        ///     The memory used at start of operations.
        /// </summary>
        public long MemUsedStart { get; protected set; }

        /// <summary>
        ///     The max memory used.
        /// </summary>
        public long MemUsedMax { get; protected set; }

        /// <summary>
        ///     Reports teh memory used during operations.
        /// </summary>
        public string Rapport
        {
            get
            {
                var memUsedEnd = TotalMemoryUsed;

                var used = memUsedEnd - MemUsedStart;
                var usedStr = used > 1000000
                    ? String.Format("Used {0}{1}", used/1000000, "mb")
                    : String.Format("Used {0}{1}", used/1000, "kb");

                var maxStr = MemUsedMax > 1000000
                    ? String.Format("Max {0}{1}", MemUsedMax/1000000, "mb")
                    : String.Format("Max {0}{1}", MemUsedMax/1000, "kb");

                return " {0}  {1}".FormatWith(usedStr, maxStr);
            }
        }

        /// <summary>
        ///     Reports the total memory used.
        /// </summary>
        protected virtual long TotalMemoryUsed
        {
            get { return GC.GetTotalMemory(true); }
        }

        /// <summary>
        ///     Reports the total memory consuption without GC.
        /// </summary>
        protected virtual long TotalMemoryWithoutGarbageCollection
        {
            get { return GC.GetTotalMemory(false); }
        }

        /// <summary>
        ///     Updates 
        /// </summary>
        public void Update()
        {
            var used = TotalMemoryWithoutGarbageCollection;
            if (used > MemUsedMax) MemUsedMax = used;
        }
    }
}
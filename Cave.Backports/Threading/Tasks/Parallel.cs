﻿#if NET40 || NET45 || NET46 || NET47 || NETSTANDARD20
#elif NET20 || NET35 || NETSTANDARD10

using System.Collections.Generic;

namespace System.Threading.Tasks
{
    /// <summary>
    /// Provides backports of the Parallel class in net &gt; 4.
    /// </summary>
    public class Parallel
    {
        #region static class

        /// <summary>
        /// Executes a for loop in which iterations may run in parallel.
        /// </summary>
        /// <param name="fromInclusive">The start index, inclusive.</param>
        /// <param name="toExclusive">The end index, exclusive.</param>
        /// <param name="action">The delegate that is invoked once per iteration.</param>
        public static void For(int fromInclusive, int toExclusive, Action<int> action)
        {
            using (Runner<int> instance = new Runner<int>(Environment.ProcessorCount << 2, action))
            {
                for (int i = fromInclusive; i < toExclusive; i++)
                {
                    instance.Start(i);
                }
            }
        }

        /// <summary>Executes a foreach operation.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="concurrentTasks">The concurrent tasks.</param>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(int concurrentTasks, IEnumerable<T> items, Action<T> action)
        {
            using (Runner<T> instance = new Runner<T>(concurrentTasks, action))
            {
                foreach (T item in items)
                {
                    instance.Start(item);
                }
            }
        }

        /// <summary>Executes a foreach operation in which up to <see cref="Environment.ProcessorCount"/> * 4 iterations may run in parallel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(IEnumerable<T> items, Action<T> action)
        {
            using (Runner<T> instance = new Runner<T>(Environment.ProcessorCount << 2, action))
            {
                foreach (T item in items)
                {
                    instance.Start(item);
                }
            }
        }
        #endregion
        class Runner<T> : IDisposable
        {
            readonly int concurrentTasks;
            readonly List<Exception> exceptions = new List<Exception>();
            readonly Action<T> action;
            readonly AutoResetEvent completed = new AutoResetEvent(false);
            int currentTasks;

            public Runner(int concurrentTasks, Action<T> action)
            {
                this.concurrentTasks = concurrentTasks;
                this.action = action;
            }

            void Run(object item)
            {
                try
                {
                    action((T)item);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    throw;
                }
                finally
                {
                    Interlocked.Decrement(ref currentTasks);
                    completed.Set();
                }
            }

            internal void Start(T item)
            {
                Interlocked.Increment(ref currentTasks);
                while (currentTasks >= concurrentTasks)
                {
                    completed.WaitOne();
                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions.ToArray());
                }
#if NETSTANDARD10
                Task.Factory.StartNew(Run, item, TaskCreationOptions.None);
#else
                ThreadPool.QueueUserWorkItem(Run, item);
#endif
            }

            public void Dispose()
            {
                while (currentTasks > 0)
                {
                    completed.WaitOne();
                }
                (completed as IDisposable)?.Dispose();
            }
        }
    }
}

#else
#error No code defined for the current framework or NETXX version define missing!
#endif
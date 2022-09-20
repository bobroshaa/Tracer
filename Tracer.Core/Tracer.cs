using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Tracer.Core
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, ThreadInfo> _methodsDictionary;
        private Stack<MethodInfo> _stackMethodsInfo;

        public Tracer()
        {
            _methodsDictionary = new ConcurrentDictionary<int, ThreadInfo>();
        }
        
        TraceResult ITracer.GetTraceResult()
        {
            var threads = new List<ThreadInfo>();
            foreach (var thread in _methodsDictionary.Values)
            {
                thread.EndThread();
                threads.Add(thread);
            }
            return new TraceResult(threads);
        }

        void ITracer.StartTrace()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1)!.GetMethod();
            var methodInfo = new MethodInfo(method.Name, method.DeclaringType.Name);
            Thread thread = Thread.CurrentThread;
            _methodsDictionary.TryAdd(thread.ManagedThreadId, new ThreadInfo(thread.ManagedThreadId));

            if (_stackMethodsInfo == null)
                _stackMethodsInfo = new Stack<MethodInfo>();
            _stackMethodsInfo.Push(methodInfo);
        }

        void ITracer.StopTrace()
        {
            Thread thread = Thread.CurrentThread;
            MethodInfo method = _stackMethodsInfo.Pop();
            method.EndMethod();
            if (_stackMethodsInfo.Count == 0)
            {
                ThreadInfo currentThread = _methodsDictionary[thread.ManagedThreadId];
                currentThread.methods.Add(method);
            }
            else
            {
                MethodInfo prevResult = _stackMethodsInfo.Pop();
                if (prevResult.ListOfMethods == null)
                {
                    prevResult.ListOfMethods = new List<MethodInfo>();
                }
                prevResult.ListOfMethods.Add(method);
                _stackMethodsInfo.Push(prevResult);
            }
        }
    }
}

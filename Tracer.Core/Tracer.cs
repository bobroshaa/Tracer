using System.Collections.Concurrent;
using System.Diagnostics;
namespace Tracer.Core
{
    public class Tracer : ITracer
    {
        private TraceResult result;
        private ConcurrentDictionary<int, ThreadInfo> _methodsDictionary = new ConcurrentDictionary<int, ThreadInfo>();
        private ConcurrentStack<MethodInfo> _stackMethodsInfo = new ConcurrentStack<MethodInfo>();

        TraceResult ITracer.GetTraceResult()
        {
            result = new TraceResult(_methodsDictionary);
            return result;
        }

        void ITracer.StartTrace()
        {
            MethodInfo method = new MethodInfo();
            StackTrace stackTrace = new StackTrace(); 
            method.MethodName = stackTrace.GetFrame(1).GetMethod().Name;
            method.ClassName = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name;
            _stackMethodsInfo.Push(method);
            Thread thread = Thread.CurrentThread;
            if (!_methodsDictionary.ContainsKey(thread.ManagedThreadId))
            {
                _methodsDictionary.TryAdd(thread.ManagedThreadId, new ThreadInfo(thread.ManagedThreadId));
            }
        }

        void ITracer.StopTrace()
        {
            _stackMethodsInfo.TryPop(out MethodInfo method);
            method.EndMethod();
            if (_stackMethodsInfo.IsEmpty)
            {
                Thread thread = Thread.CurrentThread;
                _methodsDictionary.TryGetValue(thread.ManagedThreadId, out ThreadInfo currentThread);
                currentThread.methods.Add(method);
            }
            else
            {
                _stackMethodsInfo.TryPop(out MethodInfo prevResult);
                prevResult.ListOfMethods.Add(method);
                _stackMethodsInfo.Push(prevResult);
            }
        }
    }
}

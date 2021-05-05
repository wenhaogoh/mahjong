using System;
using System.Threading;

public class ThreadSafeRandom
{
    [ThreadStatic] private static Random Local;
    public static Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}
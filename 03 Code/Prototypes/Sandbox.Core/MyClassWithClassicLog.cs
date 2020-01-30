using System;
using System.Threading.Tasks;
using Bluehands.Diagnostics.LogExtensions;

namespace Sandbox.Core
{
    public class MyClassWithClassicLog
    {
        private readonly MyWorkerClassWithClassicLog m_MyWorkerClass;
        private readonly Log<MyClassWithClassicLog> m_Log = new Log<MyClassWithClassicLog>();

        public MyClassWithClassicLog()
        {
            m_MyWorkerClass = new MyWorkerClassWithClassicLog();
        }

        public async Task<string> MyComposingMethod(string text)
        {
            m_Log.Correlation = Guid.NewGuid().ToString();
            using (m_Log.AutoTrace(() => $"The text parameter is '{text}'"))
            {
                try
                {
                    await m_MyWorkerClass.MyAsyncWorkerMethod(text);
                    m_MyWorkerClass.MySyncWorkerMethod(text);
                    return text;
                }
                catch (Exception ex)
                {
                    m_Log.Error("Something goes wrong", ex);
                    throw;
                }
            }
        }

        public void MyThrowingExceptionMethod()
        {
            try
            {
                throw new InvalidOperationException("Message of invalid operation");
            }
            catch (Exception ex)
            {
                m_Log.Error("Unexpected error", ex);
            }
        }

    }
}
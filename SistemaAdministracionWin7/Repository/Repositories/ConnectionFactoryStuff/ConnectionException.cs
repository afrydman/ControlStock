using System;

namespace Repository.ConnectionFactoryStuff
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message, Exception exceptionThrown) : base(String.Format("{0}. Mensaje: {1}", message, exceptionThrown.Message), exceptionThrown) { }
    }
}

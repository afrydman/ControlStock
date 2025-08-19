using System;
using System.Collections.Generic;
using System.Linq;
using SharedForms.ConsoleWriterService;

namespace SharedForms.LogService
{
    public static class Log
    {
        private static List<Tuple<String, ILogger>> _listLoggers = new List<Tuple<string, ILogger>>();
        private static ConsoleWriter _consoleWriter;

        public static void New(string loggerId, string file, ConsoleWriter consoleWriter = null)
        {
            if(consoleWriter == null) consoleWriter = new ConsoleWriterNull();
            _consoleWriter = consoleWriter;

            if (_listLoggers.Any(n => n.Item2.FilePath == file && n.Item1 != loggerId)) throw new Exception("Ya hay un logger escribiendo en ese archivo, pero con un id diferente");
            if (_listLoggers.Any(n => n.Item2.FilePath != file && n.Item1 == loggerId)) throw new Exception("Ya hay un logger escribiendo con ese id, pero en un distinto archivo");
            if (_listLoggers.Any(n => n.Item2.FilePath == file && n.Item1 == loggerId)) return; //Vuelve porque ya va a utilizar la instancia creada.
            _listLoggers.Add(new Tuple<string, ILogger>(loggerId, new Logger(file)));
            
        }

        public static ILogger Get(string loggerId)
        {
            var match = _listLoggers.FirstOrDefault(n => n.Item1 == loggerId);
            if(match == null) throw new Exception("Para obtener el Logger debe crearlo primero haciendo Log.New()");
            return match.Item2;
        }

        public static void Write(string loggerId, string message)
        {
            Get(loggerId).Log(message);
            _consoleWriter.printWithDateTime(message);
        }

        public static void Close(string loggerId)
        {
            var match = _listLoggers.FirstOrDefault(n => n.Item1 == loggerId);
            if (match == null) return;
            match.Item2.Close();
            _listLoggers.Remove(match);
        }
    }
}

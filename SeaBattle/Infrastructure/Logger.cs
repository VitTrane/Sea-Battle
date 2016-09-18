using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class FileLogger : ILogger
    {
        private string _pathDirectoryLogs = AppDomain.CurrentDomain.BaseDirectory + "Logs";

        public  void WriteLineError(string message) 
        {
            try
            {
                if (!Directory.Exists(_pathDirectoryLogs))
                    Directory.CreateDirectory(_pathDirectoryLogs);

                string path = Path.Combine(_pathDirectoryLogs, String.Format("Errors {0}.txt", DateTime.Now.ToShortDateString()));
                if (!File.Exists(path))
                    File.Create(path);

                using (var streamWriter = new StreamWriter(path, true, Encoding.Default))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.WriteLine("-----------------------");
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

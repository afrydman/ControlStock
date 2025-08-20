using System;
using System.Threading;
using System.Windows.Forms;
using Services;
using Helper.LogService;

namespace Central
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]


        static void Main()
        {



            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "fc-A80737C0-8A80-4464-8F7C-AB65428B26F5", out createdNew))
            {
                if (createdNew)
                {
                    try
                    {
                        // Initialize logger using config settings
                        LoggerConfig.InitializeModuleLogger("Central", LogLevel.Info);
                        LoggerConfig.InitializeCommonLoggers();
                        
                        Log.WriteInfo("Central", "=== Central application starting ===");
                        
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new padre());
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("El sistema detecto un error y se cerrara", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        // Log using new logger
                        if (Log.Exists("Central"))
                        {
                            Log.WriteFatal("Central", "Fatal error in application", e2);
                        }
                        
                        // Also log via HelperService which now uses the new logger
                        HelperService.WriteException(e2);
                        Application.Exit();

                    }
                }
                else
                {
                    MessageBox.Show("Ya hay una instancia abierta del sistema", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



           
          
        }
    }
}

using System;
using System.Threading;
using System.Windows.Forms;
using Services;


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
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new padre());
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("El sistema detecto un error y se cerrara", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        HelperService.writeLog("Error: " + e2.ToString(), true);
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

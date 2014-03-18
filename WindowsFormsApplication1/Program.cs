using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Interface;
using Ninject;

namespace WindowsFormsApplication1
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var forms = new Form1();
            BootStrapper.BootStrapper.Initialize((kernel) =>
            {

            });
            Application.Run(forms);
        }

    }
}

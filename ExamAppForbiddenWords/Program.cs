namespace ExamAppForbiddenWords
{
    internal static class Program
    {
       /* private static Mutex _syncObject;
        private const string _syncObjectName = "{E663FA11-AE0D-480e-9FCA-4BE9B8CDB4E9}";*/
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            
           /* bool createdNew;
            _syncObject = new Mutex(true, _syncObjectName, out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Программа уже запущена.");// не обязательно
                return;
            }*/




            ApplicationConfiguration.Initialize();
            Application.Run(new FormSearchForbiddenWords());
        }
    }
}
namespace GrupoE
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            // Al cerrar la aplicación se guardan los datos de cada almacén.
            Almacenes.ClientesAlmacen.Guardar();
            Almacenes.ProductosAlmacen.Guardar();
            Almacenes.DepositosAlmacen.Guardar();
            Almacenes.MercaderiasAlmacen.Guardar();
            Almacenes.OrdenesRecepcionAlmacen.Guardar();
        }
    }
}
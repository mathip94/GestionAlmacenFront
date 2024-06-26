namespace Dto
{
    public class Articulo
    {
        public int id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }

        public Articulo (string nombre, string descripcion, string codigo, double precioVenta, int stock)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Codigo = codigo;
            PrecioVenta = precioVenta;
            Stock = stock;
        }

        public Articulo() { }
    }
}

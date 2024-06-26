namespace Models
{
    public class PaginacionModel
    {
        public int PaginaActual { get; set; }
        public int TamañoPagina { get; set; }
        public int TotalDeItems { get; set; }
        public int PaginasTotales => (int)System.Math.Ceiling((decimal)TotalDeItems / TamañoPagina);
    }
}

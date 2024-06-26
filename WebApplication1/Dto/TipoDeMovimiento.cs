using WebApplication1.Dto;

namespace Dto
{
    public class TipoDeMovimiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public TipoMovimiento TipoMovimiento { get; set; }

        public TipoDeMovimiento(string nombre)
        {
            this.Nombre = nombre;
        }

        public TipoDeMovimiento() { }
    }
}

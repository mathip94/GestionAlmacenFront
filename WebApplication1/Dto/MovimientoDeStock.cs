namespace Dto
{
    public class MovimientoDeStock
    {
        public int Id { get; set; }
        public DateTime FechaYHora { get; set; }
        public int ArticuloId { get; set; }
        public int TipoDeMovimientoId { get; set; }
        public string MailUsuario { get; set; }
        public int CantidadDeUnidadesMovidas { get; set; }

        public MovimientoDeStock(int id, DateTime fechaYHora, int articuloId, int tipoDeMovimientoId, string usuarioEmail, int cantidadDeUnidadesMovidas)
        {
            Id = id;
            FechaYHora = fechaYHora;
            ArticuloId = articuloId;
            TipoDeMovimientoId = tipoDeMovimientoId;
            MailUsuario = usuarioEmail;
            CantidadDeUnidadesMovidas = cantidadDeUnidadesMovidas;
        }

        public MovimientoDeStock() { }
    }
}

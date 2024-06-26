using Dto;
using HttpAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Models;
using Servicios;
using System.Reflection;

namespace WebApplication1.Controllers
{
    public class MovimientoDeStockController : Controller
    {
        private readonly IServicioArticulo _servicioArticulo;
        private readonly IServicioTipoDeMovimiento _servicioTipoDeMovimiento;
        private readonly IServicioMovimientoDeStock _servicioMovimientoDeStock;

        public MovimientoDeStockController(IServicioArticulo servicioArticulo, IServicioTipoDeMovimiento servicioTipoDeMovimiento, IServicioMovimientoDeStock servicioMovimientoDeStock)
        {
            _servicioArticulo = servicioArticulo;
            _servicioTipoDeMovimiento = servicioTipoDeMovimiento;
            _servicioMovimientoDeStock = servicioMovimientoDeStock;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }
            ViewBag.Articulos = _servicioArticulo.GetAll().Articulos;
            ViewBag.TiposDeMovimiento = _servicioTipoDeMovimiento.GetAll().Tipos;
            foreach (Articulo a in ViewBag.Articulos)
            {
                Console.WriteLine(a.id);
            }
            
            return View(new MovimientoDeStockViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(MovimientoDeStockViewModel model)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }

            try
            {
                var movimientoDto = new MovimientoDeStock
                {
                    FechaYHora = DateTime.Now,
                    ArticuloId = model.ArticuloId,
                    TipoDeMovimientoId = model.TipoDeMovimientoId,
                    MailUsuario = HttpContext.Session.GetString("email"),
                    CantidadDeUnidadesMovidas = model.Movimiento.CantidadDeUnidadesMovidas
                };

                var movimientoDtoCreated = _servicioMovimientoDeStock.Add(movimientoDto);
                TempData["Exito"] = "Movimiento de stock registrado correctamente";
                return RedirectToAction(nameof(Crear));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            ViewBag.Articulos = _servicioArticulo.GetAll().Articulos;
            
            ViewBag.TiposDeMovimiento = _servicioTipoDeMovimiento.GetAll().Tipos;
            return View(model);
        }

        public IActionResult Index(int articuloId, int tipoDeMovimientoId, int pagina = 1, int tamañoPagina = 5)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }
            try
            {
                var articulos = _servicioArticulo.GetAll().Articulos;
                var tiposDeMovimiento = _servicioTipoDeMovimiento.GetAll().Tipos;

                var movimientoResponse = new MovimientoDeStockResponseDto();
                int totalDeItems = 0;
                bool busquedaRealizada = false;

                if (articuloId != 0 && tipoDeMovimientoId != 0)
                {
                    movimientoResponse = _servicioMovimientoDeStock.GetMovimientosPorArticuloYTipo(articuloId, tipoDeMovimientoId, (pagina - 1) * tamañoPagina, tamañoPagina);
                    totalDeItems = movimientoResponse.Count;
                    busquedaRealizada = true;
                }

                ViewBag.Articulos = articulos;
                ViewBag.TiposDeMovimiento = tiposDeMovimiento;


                var model = new MovimientoDeStockConsultaViewModel
                {
                    ArticuloId = articuloId,
                    TipoDeMovimientoId = tipoDeMovimientoId,
                    Movimientos = movimientoResponse.Movimientos,
                    Paginacion = new PaginacionModel
                    {
                        PaginaActual = pagina,
                        TamañoPagina = tamañoPagina,
                        TotalDeItems = movimientoResponse.Count
                    },
                    BusquedaRealizada = busquedaRealizada
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View("Index");
        }

        public IActionResult ConsultaPorFechas(DateTime? fechaInicio, DateTime? fechaFin, int pagina = 1, int tamañoPagina = 5)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }
            try
            {
                var articulosResponse = new ArticuloResponseDto();
                int totalDeItems = 0;
                bool busquedaRealizada = false;

                if (fechaInicio.HasValue && fechaFin.HasValue)
                {
                    var fechaFinAdjusted = fechaFin.Value.Date.AddDays(1).AddTicks(-1);

                    articulosResponse = _servicioMovimientoDeStock.GetArticuloConMovimientosEnRangoDeFechas(fechaInicio.Value, fechaFinAdjusted, (pagina - 1) * tamañoPagina, tamañoPagina);
                    totalDeItems = articulosResponse.Count;
                    busquedaRealizada = true;
                }

                var model = new ArticuloConMovimientosViewModel
                {
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    Articulos = articulosResponse.Articulos,
                    Paginacion = new PaginacionModel
                    {
                        PaginaActual = pagina,
                        TamañoPagina = tamañoPagina,
                        TotalDeItems = totalDeItems
                    },
                    BusquedaRealizada = busquedaRealizada
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View("Index");
        }

        public IActionResult Resumen()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }
            var resumenResponse = _servicioMovimientoDeStock.GetResumenMovimientos();

            var model = new ResumenMovimientosViewModel
            {
                ResumenPorAño = resumenResponse.ResumenPorAño.Select(r => new MovimientoDeStockResumenViewModel
                {
                    Año = r.Año,
                    Detalles = r.Detalles.Select(d => new DetalleMovimientoViewModel
                    {
                        TipoMovimiento = d.TipoMovimiento,
                        Cantidad = d.Cantidad
                    }),
                    TotalAnual = r.TotalAnual
                })
            };

            return View(model);
        }


    }

}

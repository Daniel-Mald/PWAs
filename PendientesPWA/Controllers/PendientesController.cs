using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PendientesPWA.Models.DTOs;
using PendientesPWA.Models.Entities;
using PendientesPWA.Services;

namespace PendientesPWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendientesController : ControllerBase
    {
        FokinpendientesContext _context;
        IHubContext<PendientesHub> _hubContext;
        public PendientesController(FokinpendientesContext context, IHubContext<PendientesHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post(PendienteDTO _dto)
        {
            if (string.IsNullOrWhiteSpace(_dto.Descripcion))
            {
                return BadRequest();
            }
            PendienteDTO p = new PendienteDTO()
            {
                Descripcion = _dto.Descripcion,
                Estado = 0
            };
            _context.Add(p);
            _context.SaveChanges();

            //informar con signalr
            await _hubContext.Clients.All.SendAsync("NuevoPendiente", p);
            return Ok();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.Asf;
            return Ok(data.Select(X => new PendienteDTO
            {
                Id = X.Id,
                Descripcion = X.Descripcion,
                Estado = X.Estado
            }));
        }
        [HttpPut("/EditarDescripcion")]
        public async Task<IActionResult> Put(PendienteDTO _dto)
        {
            var pendiente = _context.Asf.Find(_dto.Id);
            if (pendiente == null) { return NotFound(); }

            pendiente.Descripcion = _dto.Descripcion;
            int total = _context.SaveChanges();

            //Notificar con signalR

            if (total > 0)
            {
                await _hubContext.Clients.All.SendAsync("PendienteEditado",
                    new
                    {
                        pendiente.Id,
                        pendiente.Descripcion
                    });

            }

            return Ok();
        }
        [HttpPut("/EditarEstado")]
        public IActionResult PutEstado(PendienteDTO _dto)
        {
            if(_dto.Id == null) { return BadRequest(); }
            var pen = _context.Asf.Find(_dto.Id);
            pen.Estado = (int)_dto.Estado;
            _context.Update(pen);
            _context.SaveChanges();
            return Ok();

        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var pen = _context.Asf.Find(id);
            _context.Remove(pen);
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("PendienteEliminado", pen.Id);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificacionesPush.Models;
using NotificacionesPush.Service;
using WebPush;

namespace NotificacionesPush.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        NotificationService notificationService;
        public NotificacionesController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        private static List<PushSubscription> listaSuscriptores = new();
        [HttpPost]
        public IActionResult Suscribir(ClienteDTO cliente)
        {
            PushSubscription pushSubscription = new PushSubscription();
            pushSubscription.Endpoint = cliente.Endpoint;
            pushSubscription.P256DH = cliente.Keys.P256dh;
            pushSubscription.Auth = cliente.Keys.Auth;
            listaSuscriptores.Add(pushSubscription);
            return Ok();
        }
        [HttpGet("enviar/{message}")]
        public async Task<IActionResult> EnviarNotificacion(string messaje)
        {
            
            foreach (var item in listaSuscriptores)
            {
                await notificationService.EnviarNotificacion(item, messaje);
            }
            return Ok();
        }
        //[HttpPost]
        //public IActionResult Desuscribir()
        //{
        //    return Ok();
        //}

    }
}

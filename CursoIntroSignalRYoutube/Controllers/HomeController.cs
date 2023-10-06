using CursoIntroSignalRYoutube.Hubs;
using CursoIntroSignalRYoutube.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace CursoIntroSignalRYoutube.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<ChatHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat(int id)
        {
            return View(id);
        }

        public async Task<IActionResult> Notification(int receiverId, int senderId)
        {
            await _hubContext.Clients.All.SendAsync($"ReceiveMessage{receiverId}", senderId, receiverId, "Teste notificação").ConfigureAwait(false);

            return RedirectToAction(nameof(Chat), new { id = senderId });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
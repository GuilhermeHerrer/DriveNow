using DriveNow.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace DriveNow.MVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly HttpClient _apiClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ClientesController(IHttpClientFactory httpClientFactory)
        {
            _apiClient = httpClientFactory.CreateClient("DriveNowApi");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index()
        {
            var resp = await _apiClient.GetAsync("api/Clientes");
            if (!resp.IsSuccessStatusCode) return View(new List<ClienteView>());

            var json = await resp.Content.ReadAsStringAsync();
            var clientes = JsonSerializer.Deserialize<List<ClienteView>>(json, _jsonOptions);
            return View(clientes);
        }
        [HttpGet]
        public IActionResult CriarCliente()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CriarCliente(ClienteView c)
        {
            if (!ModelState.IsValid) return View(c);

            if (c.FotoUpload != null)
            {
                string pastaImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens", "clientes");

                if (!Directory.Exists(pastaImagens)) Directory.CreateDirectory(pastaImagens);

                string nomeArquivo = Guid.NewGuid().ToString() + "-" + c.FotoUpload.FileName;
                string caminhoCompleto = Path.Combine(pastaImagens, nomeArquivo);

                using (var fileStream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await c.FotoUpload.CopyToAsync(fileStream);
                }

                c.FotoUrl = "/imagens/clientes/" + nomeArquivo;
            }

            var clienteDto = new
            {
                c.Id,
                c.Nome,
                c.Cpf,
                c.Email,
                c.FotoUrl
            };

            var content = new StringContent(JsonSerializer.Serialize(clienteDto), Encoding.UTF8, "application/json");

            var resp = await _apiClient.PostAsync("api/Clientes", content);

            if (!resp.IsSuccessStatusCode) return View(c);


            return RedirectToAction("Index");
        }
    }
}

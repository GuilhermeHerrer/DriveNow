using DriveNow.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace DriveNow.MVC.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly HttpClient _apiClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public VeiculosController(IHttpClientFactory httpClientFactory)
        {
            _apiClient = httpClientFactory.CreateClient("DriveNowApi");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index()
        {
            var resp = await _apiClient.GetAsync("api/Veiculos");
            if (!resp.IsSuccessStatusCode) return View(new List<VeiculoView>());

            var json = await resp.Content.ReadAsStringAsync();
            var clientes = JsonSerializer.Deserialize<List<VeiculoView>>(json, _jsonOptions);
            return View(clientes);
        }
        [HttpGet]
        public IActionResult CriarVeiculo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CriarVeiculo(VeiculoView v)
        {
            if (!ModelState.IsValid) return View(v);

            ModelState.Remove("Id");
            ModelState.Remove("FotoUrl");   

            if (v.FotoUpload != null)
            {
                string pastaImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens", "veiculos");

                if (!Directory.Exists(pastaImagens)) Directory.CreateDirectory(pastaImagens);

                string nomeArquivo = Guid.NewGuid().ToString() + "-" + v.FotoUpload.FileName;
                string caminhoCompleto = Path.Combine(pastaImagens, nomeArquivo);

                using (var fileStream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await v.FotoUpload.CopyToAsync(fileStream);
                }

                v.FotoUrl = "/imagens/veiculos/" + nomeArquivo;
            }

            var veiculoDto = new
            {
                v.FotoUrl,
                v.Nome,
                v.ValorDiaria,
                v.Placa,
                v.AgenciaId,
            };

            var content = new StringContent(JsonSerializer.Serialize(veiculoDto), Encoding.UTF8, "application/json");

            var resp = await _apiClient.PostAsync("api/Veiculos", content);

            if (!resp.IsSuccessStatusCode) return View(v);


            return RedirectToAction("Index");
        }
    }
}

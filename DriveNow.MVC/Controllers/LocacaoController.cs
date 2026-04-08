using DriveNow.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace DriveNow.MVC.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly HttpClient _apiClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LocacaoController(IHttpClientFactory httpClientFactory)
        {
            _apiClient = httpClientFactory.CreateClient("DriveNowApi");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ActionResult> Index() 
        {
            var resp = await _apiClient.GetAsync("api/Locacoes");
            if (!resp.IsSuccessStatusCode) return View(new List<LocacaoView>());

            var json = await resp.Content.ReadAsStringAsync();
            var locacoes = JsonSerializer.Deserialize<List<LocacaoView>>(json, _jsonOptions);
            return View(locacoes);
        }

        public async Task<IActionResult> CriarLocacao()
        {
            await PreencherViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarLocacao(LocacaoView l)
        {
            if (!ModelState.IsValid) { await PreencherViewBag(); return View(l); }

            var content = new StringContent(JsonSerializer.Serialize(l), Encoding.UTF8, "application/json");
            var resp = await _apiClient.PostAsync("api/Locacoes", content);

            if (!resp.IsSuccessStatusCode)
            {
                await PreencherViewBag();
                return View(l);
            }
            return RedirectToAction("Index");
        }

        private async Task PreencherViewBag()
        {
            var respClientes = await _apiClient.GetAsync("api/Clientes");
            var jsonClientes = await respClientes.Content.ReadAsStringAsync();
            var clientes = JsonSerializer.Deserialize<List<ClienteView>>(jsonClientes, _jsonOptions);

            var respVeiculos = await _apiClient.GetAsync("api/Veiculos");
            var jsonVeiculos = await respVeiculos.Content.ReadAsStringAsync();
            var veiculos = JsonSerializer.Deserialize<List<VeiculoView>>(jsonVeiculos, _jsonOptions);

            ViewBag.Clientes = new SelectList(clientes, "Id", "Nome");
            ViewBag.Veiculos = new SelectList(veiculos, "Id", "Nome");
        }
    }
}

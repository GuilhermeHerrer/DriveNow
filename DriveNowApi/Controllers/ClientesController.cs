using AutoMapper;
using DriveNowApi.Data;
using DriveNowApi.DTOs;
using DriveNowApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DriveNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClientesController(AppDbContext context, IMapper mapper, IWebHostEnvironment web)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = web;
        }

        [HttpGet]
        public async Task<IActionResult> GetCliente() {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteId(int id) {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> PostCliente(ClienteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var cliente = _mapper.Map<Cliente>(dto);

            if (await _context.Clientes.AnyAsync(c => c.Cpf == cliente.Cpf || c.Email == cliente.Email)) {
                return BadRequest("O CPF ou E-mail informada já está cadastrado!");
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(ClienteDTO dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clienteExistente = await _context.Clientes.FindAsync(id);

            if (clienteExistente == null) return NotFound("Cliente não encontrado.");

            _mapper.Map(dto, clienteExistente);
            _context.Update(clienteExistente);
            await _context.SaveChangesAsync();

            return Ok(clienteExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound("Cliente não encontrado.");
            
            _context.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}

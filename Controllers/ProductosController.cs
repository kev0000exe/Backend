using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

[Route("api/productos")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _productoRepository;

    public ProductosController(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productos = await _productoRepository.GetAllAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null) return NotFound();
        return Ok(producto);
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> Create([FromBody] Producto producto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _productoRepository.AddAsync(producto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
    {
        if (id != producto.Id) return BadRequest("IDs no coinciden");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _productoRepository.UpdateAsync(producto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null) return NotFound();

        await _productoRepository.DeleteAsync(id);
        return NoContent();
    }
}

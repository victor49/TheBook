using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Models;
using Thebook.Services;

namespace Thebook.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _reportesService;

        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("prestamosActivos")]
        public async Task<IEnumerable<PrestamoGetDto>> GetPrestamosActivos()
        {
            return await _reportesService.GetPrestamosActivos();
        }
        
        [HttpGet("prestamosPorUsuario/{id}")]
        public async Task<IEnumerable<PrestamoGetDto>> GetPrestamoPorUsuario(int id)
        {
            return await _reportesService.GetPrestamosPorUsuario(id);
        }
    }
}

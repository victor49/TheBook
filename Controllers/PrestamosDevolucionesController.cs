using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Repository;
using Thebook.Services;

namespace Thebook.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class PrestamosDevolucionesController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamosDevolucionesController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        //[HttpGet]


        [HttpPost]
        public async Task<ActionResult<PrestamoGetDto>> Add(PrestamoInsertDto prestamoInsertDto)
        {
            var prestamoDto = await _prestamoService.Add(prestamoInsertDto);

            return prestamoDto == null ? NotFound() : Ok(prestamoInsertDto);
        }
    }
}

using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeCap.Application.DTOs.Response;
using SafeCap.Domain.Entities;
using SafeCap.Infrastructure.Context;
using SafeCap.Application.DTOs.Request;

namespace SafeCap.Presentation.Controllers
{
    [ApiController]
    [Route("api/readings")]
    [Tags("Leituras do Sensor")]
    public class SensorReadingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SensorReadingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<List<SensorReadingResponse>>> GetAll()
        {
            var readings = await _context.SensorReadings.ToListAsync();
            var readingDtos = _mapper.Map<List<SensorReadingResponse>>(readings);
            return Ok(readingDtos);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<SensorReadingResponse>> GetById(Guid id)
        {
            var reading = await _context.SensorReadings.FindAsync(id);
            if (reading == null) return NotFound();
            var readingDto = _mapper.Map<SensorReadingResponse>(reading);
            return Ok(readingDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SensorReadingResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<SensorReadingResponse>> Create(SensorReadingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Temperature == null && request.Humidity == null && request.Light == null)
                return BadRequest("Pelo menos um valor de sensor deve ser informado.");

            var reading = _mapper.Map<SensorReading>(request);
            _context.SensorReadings.Add(reading);
            await _context.SaveChangesAsync();

            var readingDto = _mapper.Map<SensorReadingResponse>(reading);
            return CreatedAtAction(nameof(GetById), new { id = readingDto.Id }, readingDto);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, NoUserReadingRequest request)
        {
            var existingReading = await _context.SensorReadings.FindAsync(id);
            if (existingReading == null) return NotFound("Leitura não encontrada.");

            existingReading.Temperature = request.Temperature;
            existingReading.Humidity = request.Humidity;
            existingReading.Light = request.Light;

            _context.SensorReadings.Update(existingReading);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reading = await _context.SensorReadings.FindAsync(id);
            if (reading == null) return NotFound("Leitura não encontrada.");

            _context.SensorReadings.Remove(reading);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

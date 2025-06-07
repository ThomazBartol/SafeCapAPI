using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeCap.Domain.Entities;
using SafeCap.Infrastructure.Context;
using SafeCap.Application.DTOs.Response;
using SafeCap.Application.DTOs.Request;
using AutoMapper;

namespace SafeCap.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Tags("Usuários")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        { 
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<List<UserResponse>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos = _mapper.Map<List<UserResponse>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserResponse>> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            var userDto = _mapper.Map<UserResponse>(user);
            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserResponse>> Create(UserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(request);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserResponse>(user);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }
    }
}

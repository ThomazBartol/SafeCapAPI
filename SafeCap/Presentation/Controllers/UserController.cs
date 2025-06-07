using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeCap.Infrastructure.Context;
using SafeCap.Application.DTOs.Response;
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
    }
}

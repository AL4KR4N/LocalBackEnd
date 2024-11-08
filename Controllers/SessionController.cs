using monchotradebackend.models.entities;
using monchotradebackend.models.dtos; 
using monchotradebackend.Interface;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*
    Endpoints implementados
    
    Post Login
    Post Signup/CreateUser
*/


namespace monchotradebackend.controllers{
    
    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase{
        private readonly IRepository<User, int> _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        public SessionController(IRepository<User, int> userRepository, IPasswordHashingService passwordHashingService){
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request){
            var existingUser = await _userRepository.GetQueryable().FirstOrDefaultAsync(u => u.Email == request.Email);
          

            if (existingUser == null)
                return BadRequest("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash))
            {
                return BadRequest("Wrong user or password");
            }

            return Ok(existingUser);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            bool emailExists = await _userRepository.GetQueryable().AnyAsync(u => u.Email == newUser.Email);

            if (emailExists)
            {
                return BadRequest("Email already in use.");
            }

            newUser.PasswordHash = _passwordHashingService.HashPassword(newUser.PasswordHash);

            await _userRepository.InsertAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return Ok(newUser);
        }

    }
}


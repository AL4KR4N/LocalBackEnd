using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using monchotradebackend.Interface;
using monchotradebackend.models.entities;
using monchotradebackend.models.dtos;
using Microsoft.AspNetCore.JsonPatch;
using System.Drawing;

namespace monchotradebackend.controllers
{
    [ApiController]
    [Route("exchanges")]
    public class ExchangesController : ControllerBase
    {
        private readonly IRepository<Exchange, int> _dbRepository;
        private readonly IRepository<User, int> _dbUserRepository;
        private readonly IRepository<Product, int> _dbProductRepository;
        private readonly ILogger<ExchangesController> _logger;

        public ExchangesController(
            IRepository<Exchange, int> dbRepository,
            ILogger<ExchangesController> logger,
            IRepository<User, int> dbUserRepository,
            IRepository<Product, int> dbProductRepository)
        {
            _dbRepository = dbRepository;
            _logger = logger;
            _dbUserRepository = dbUserRepository;
            _dbProductRepository = dbProductRepository;

        }

        [HttpGet]
        public async Task<ActionResult<List<ExchangeDto>>> GetAllExchanges()
        {
            try
            {
                var exchanges = await _dbRepository.GetQueryable()
                    .Include(e => e.InitiatorUser)
                    .Include(e => e.ReceiverUser)
                    .Include(e => e.InitiatorProduct)
                    .Include(e => e.ReceiverProduct)
                    .Select(e => new ExchangeDto
                    {
                        Id = e.Id,
                        InitiatorUserId = e.InitiatorUserId,
                        ReceiverUserId = e.ReceiverUserId,
                        InitiatorProductId = e.InitiatorProductId,
                        ReceiverProductId = e.ReceiverProductId,
                        CreatedAt = e.CreatedAt,
                        UpdatedAt = e.UpdatedAt,
                        Status = e.Status.ToString(),
                        RejectionReason = e.RejectionReason,
                        Notes = e.Notes,
                        InitiatorUserName = e.InitiatorUser.Name,
                        ReceiverUserName = e.ReceiverUser.Name,
                        InitiatorProductName = e.InitiatorProduct.Name,
                        ReceiverProductName = e.ReceiverProduct.Name
                    })
                    .ToListAsync();

                return Ok(exchanges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching exchanges data.");
                return StatusCode(500, "An error occurred while fetching exchanges data.");
            }
        }

        [HttpGet("user/{userid}")]
        public async Task<ActionResult<List<ExchangeDto>>> GetExchangesByUserId(int userid)
        {
        try
        {
            var exchanges = await _dbRepository.GetQueryable()
                .Include(e => e.InitiatorUser)
                .Include(e => e.ReceiverUser) 
                .Include(e => e.InitiatorProduct)
                .Include(e => e.ReceiverProduct)
                .Where(u => u.InitiatorUserId == userid || u.ReceiverUserId == userid)
                .Select(e => new ExchangeDto
                {
                    Id = e.Id,
                    InitiatorUserId = e.InitiatorUserId,
                    ReceiverUserId = e.ReceiverUserId,
                    InitiatorProductId = e.InitiatorProductId,
                    ReceiverProductId = e.ReceiverProductId,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    Status = e.Status.ToString(),
                    RejectionReason = e.RejectionReason,
                    Notes = e.Notes,
                    InitiatorUserName = e.InitiatorUser.Name,
                    ReceiverUserName = e.ReceiverUser.Name,
                    InitiatorProductName = e.InitiatorProduct.Name,
                    ReceiverProductName = e.ReceiverProduct.Name
                })
                .ToListAsync();

            return Ok(exchanges);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching exchanges data by user id.");
            return StatusCode(500, "An error occurred while fetching exchanges data by user id.");
        }
        }

        [HttpGet("user/initiator/{userid}")]
        public async Task<ActionResult<List<ExchangeDto>>> GetInitiatorExchangesByUserId(int userid)
        {
        try
        {
            var exchanges = await _dbRepository.GetQueryable()
                .Include(e => e.InitiatorUser)
                .Include(e => e.ReceiverUser) 
                .Include(e => e.InitiatorProduct)
                .Include(e => e.ReceiverProduct)
                .Where(u => u.InitiatorUserId == userid)
                .Select(e => new ExchangeDto
                {
                    Id = e.Id,
                    InitiatorUserId = e.InitiatorUserId,
                    ReceiverUserId = e.ReceiverUserId,
                    InitiatorProductId = e.InitiatorProductId,
                    ReceiverProductId = e.ReceiverProductId,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    Status = e.Status.ToString(),
                    RejectionReason = e.RejectionReason,
                    Notes = e.Notes,
                    InitiatorUserName = e.InitiatorUser.Name,
                    ReceiverUserName = e.ReceiverUser.Name,
                    InitiatorProductName = e.InitiatorProduct.Name,
                    ReceiverProductName = e.ReceiverProduct.Name
                })
                .ToListAsync();

            return Ok(exchanges);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching exchanges data by user id.");
            return StatusCode(500, "An error occurred while fetching exchanges data by user id.");
        }
        }


        [HttpGet("user/receiver/{userid}")]
        public async Task<ActionResult<List<ExchangeDto>>> GetReceiverExchangesByUserId(int userid)
        {
        try
        {
            var exchanges = await _dbRepository.GetQueryable()
                .Include(e => e.InitiatorUser)
                .Include(e => e.ReceiverUser) 
                .Include(e => e.InitiatorProduct)
                .Include(e => e.ReceiverProduct)
                .Where(u =>u.ReceiverUserId == userid)
                .Select(e => new ExchangeDto
                {
                    Id = e.Id,
                    InitiatorUserId = e.InitiatorUserId,
                    ReceiverUserId = e.ReceiverUserId,
                    InitiatorProductId = e.InitiatorProductId,
                    ReceiverProductId = e.ReceiverProductId,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    Status = e.Status.ToString(),
                    RejectionReason = e.RejectionReason,
                    Notes = e.Notes,
                    InitiatorUserName = e.InitiatorUser.Name,
                    ReceiverUserName = e.ReceiverUser.Name,
                    InitiatorProductName = e.InitiatorProduct.Name,
                    ReceiverProductName = e.ReceiverProduct.Name
                })
                .ToListAsync();

            return Ok(exchanges);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching exchanges data by user id.");
            return StatusCode(500, "An error occurred while fetching exchanges data by user id.");
        }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeDto>> GetExchangeById(int id)
        {
            try
            {
                var exchange = await _dbRepository.GetQueryable()
                    .Include(e => e.InitiatorUser)
                    .Include(e => e.ReceiverUser)
                    .Include(e => e.InitiatorProduct)
                    .Include(e => e.ReceiverProduct)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (exchange == null)
                {
                    return NotFound($"Exchange with ID {id} not found.");
                }

                var exchangeDto = new ExchangeDto
                {
                    Id = exchange.Id,
                    InitiatorUserId = exchange.InitiatorUserId,
                    ReceiverUserId = exchange.ReceiverUserId,
                    InitiatorProductId = exchange.InitiatorProductId,
                    ReceiverProductId = exchange.ReceiverProductId,
                    CreatedAt = exchange.CreatedAt,
                    UpdatedAt = exchange.UpdatedAt,
                    Status = exchange.Status.ToString(),
                    RejectionReason = exchange.RejectionReason,
                    Notes = exchange.Notes,
                    InitiatorUserName = exchange.InitiatorUser.Name,
                    ReceiverUserName = exchange.ReceiverUser.Name,
                    InitiatorProductName = exchange.InitiatorProduct.Name,
                    ReceiverProductName = exchange.ReceiverProduct.Name
                };

                return Ok(exchangeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching exchange with ID {Id}", id);
                return StatusCode(500, "An error occurred while fetching exchange data.");
            }
        }


       


        [HttpPut]         
        public async Task<ActionResult> CreateExchange(ExchangeCreationDto newExchange)         
        {             
            try             
            {
                // Get the receiver's user ID from the product
                var receiverProduct = await _dbProductRepository.GetQueryable()
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == newExchange.ReceiverProductId);

                if (receiverProduct == null)
                {
                    return BadRequest("Receiver product does not exist.");
                }

                // Validate that users exist                 
                var initiator = await _dbUserRepository.GetByIdAsync(newExchange.InitiatorUserId);                 
                var receiver = await _dbUserRepository.GetByIdAsync(receiverProduct.User.Id);                  
                
                if (initiator == null || receiver == null)                 
                {                     
                    return BadRequest("One or both users do not exist.");                 
                }                  

                // Validate initiator product exists and belongs to initiator
                var initiatorProduct = await _dbProductRepository.GetByIdAsync(newExchange.InitiatorProductId);                 
                
                if (initiatorProduct == null)                 
                {                     
                    return BadRequest("Initiator product does not exist.");                 
                }                  

                if (initiatorProduct.UserId != newExchange.InitiatorUserId)                     
                {                     
                    return BadRequest("Product does not belong to the initiator user.");                 
                }                  
                var exchange = new Exchange                 
                {                     
                    InitiatorUserId = newExchange.InitiatorUserId,                     
                    ReceiverUserId = receiverProduct.User.Id,                     
                    InitiatorProductId = newExchange.InitiatorProductId,                     
                    ReceiverProductId = newExchange.ReceiverProductId,                     
                    Notes = newExchange.Notes,                     
                    CreatedAt = DateTime.UtcNow,                     
                    Status = ExchangeStatus.Pending,                     
                    InitiatorUser = initiator,                     
                    ReceiverUser = receiver,                     
                    InitiatorProduct = initiatorProduct,                     
                    ReceiverProduct = receiverProduct                 
                };                  

                await _dbRepository.InsertAsync(exchange);                 
                await _dbRepository.SaveChangesAsync();                  

                return Ok(new { id = exchange.Id, message = "Exchange created successfully" });             
            }             
            catch (Exception ex)             
            {                 
                _logger.LogError(ex, "Error creating new exchange. InitiatorUserId: {InitiatorUserId}, ReceiverProductId: {ReceiverProductId}",                     
                    newExchange.InitiatorUserId, newExchange.ReceiverProductId);                 
                return StatusCode(500, "An error occurred while creating new exchange.");             
            }         
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateExchange(int id, [FromBody] JsonPatchDocument<ExchangeDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    return BadRequest("Patch document cannot be null");
                }

                var exchange = await _dbRepository.GetByIdAsync(id);
                
                if (exchange == null)
                {
                    return NotFound($"Exchange with ID {id} not found");
                }

                var exchangeDto = new ExchangeDto
                {
                    Id = exchange.Id,
                    InitiatorUserId = exchange.InitiatorUserId,
                    ReceiverUserId = exchange.ReceiverUserId,
                    InitiatorProductId = exchange.InitiatorProductId,
                    ReceiverProductId = exchange.ReceiverProductId,
                    Status = exchange.Status.ToString(),
                    RejectionReason = exchange.RejectionReason,
                    Notes = exchange.Notes
                };

                patchDoc.ApplyTo(exchangeDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                exchange.Status = Enum.Parse<ExchangeStatus>(exchangeDto.Status);  // String a enum  
                exchange.RejectionReason = exchangeDto.RejectionReason;
                exchange.Notes = exchangeDto.Notes;
                exchange.UpdatedAt = DateTime.Now;

                await _dbRepository.UpdateAsync(exchange);
                await _dbRepository.SaveChangesAsync();

                return Ok(exchangeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating exchange.");
                return StatusCode(500, "An error occurred while updating exchange.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExchange(int id)
        {
            try
            {
                var exchange = await _dbRepository.GetByIdAsync(id);
                if (exchange == null)
                {
                    return NotFound($"Exchange with ID {id} not found.");
                }

                await _dbRepository.DeleteAsync(exchange);
                await _dbRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting exchange with ID {Id}.", id);
                return StatusCode(500, "An error occurred while deleting exchange.");
            }
        }
    }
}
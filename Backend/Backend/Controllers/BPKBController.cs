using Backend.Models;
using Backend.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BPKBController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public BPKBController(CoreDbContext context) { 
            _context = context;
        }

        [HttpGet("locations")]
        public async Task<ActionResult<IEnumerable<MsStorageLocation>>> GetLocations()
        {
            return await _context.MsStorageLocations.ToListAsync();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var query = from u in _context.MsUsers
                        where EF.Functions.Collate(u.UserName, "SQL_Latin1_General_CP1_CS_AS") == request.Username
                        select u;
            var user = query.SingleOrDefault();
            if (user == null || user.Password != request.Password || user.IsActive == false)
            {
                return Unauthorized(new { 
                    Message = "Please Check Your Username and Password!"
                });
            }
            return Ok(new
            {
                Message = "Login Success!",
                UserId = user.UserId
            });
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<TrBpkb>>> GetTransactions()
        {
            return await _context.TrBpkbs.ToListAsync();
        }

        [HttpGet("transactions/{agreementNumber}")]
        public async Task<IActionResult> GetTransaction(string agreementNumber)
        {
            var transaction = _context.TrBpkbs.Include(t => t.Location).SingleOrDefault(t => t.AgreementNumber == agreementNumber);
            if(transaction == null)
            {
                return NotFound(new
                {
                    Message = "Transaction Not Found!"
                });
            }
            return Ok(transaction);
        }

        [HttpPost("transactions")]
        public async Task<IActionResult> InsertTransaction(TrBpkb request)
        {
            _context.TrBpkbs.Add(request);
            _context.SaveChanges();
            bool exists = await _context.TrBpkbs
            .AnyAsync(b => b.AgreementNumber == request.AgreementNumber);
            if (exists)
            {
                return Ok(new
                {
                    Message = "Transaction with Agreement Number " + request.AgreementNumber + " has been Inserted Successfully!"
                });
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Failed to Insert Data!"
                });
            }

        }

        [HttpPut("transactions")]
        public async Task<IActionResult> UpdateTransaction(TrBpkb request)
        {
            var existingTrBpkb = await _context.TrBpkbs.FindAsync(request.AgreementNumber);
            if (existingTrBpkb == null)
            {
                return NotFound(new
                {
                    Message = "Transaction Not Found!"
                });
            }

            existingTrBpkb.BpkbNo = request.BpkbNo;
            existingTrBpkb.BranchId = request.BranchId;
            existingTrBpkb.BpkbDate = request.BpkbDate;
            existingTrBpkb.FakturNo = request.FakturNo;
            existingTrBpkb.FakturDate = request.FakturDate;
            existingTrBpkb.LocationId = request.LocationId;
            existingTrBpkb.PoliceNo = request.PoliceNo;
            existingTrBpkb.BpkbDateIn = request.BpkbDateIn;
            existingTrBpkb.LastUpdatedBy = request.LastUpdatedBy;
            existingTrBpkb.LastUpdatedOn = request.LastUpdatedOn;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Transaction with Agreement Number " + request.AgreementNumber + " has been Updated Successfully!"
            });

        }
    }
}

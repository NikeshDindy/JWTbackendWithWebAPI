using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using System.Security.Claims;

namespace JWTbackendWithWebAPI.Controllers
{
    public class DemoController:ControllerBase
    {
        // 1️⃣ Anyone with a valid token
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var name = User.Identity?.Name;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var dept = User.FindFirst("Department")?.Value;
            var age = User.FindFirst("Age")?.Value;

            return Ok(new
            {
                Message = "Your profile info",
                Name = name,
                Email = email,
                Role = role,
                Department = dept,
                Age = age
            });
        }

        // 2️⃣ Role-based Authorization
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok("Only Admins can see this dashboard.");
        }

        // 3️⃣ Policy-based Authorization (Age >= 18)
        [Authorize(Policy = "AdultOnly")]
        [HttpGet("adult-section")]
        public IActionResult AdultSection()
        {
            return Ok("You are 18+ and allowed in the adult section.");
        }

        // 4️⃣ Combined Role + Policy (Admin + Finance dept)
        [Authorize(Roles = "Admin", Policy = "FinanceOnly")]
        [HttpGet("financial-report")]
        public IActionResult FinancialReport()
        {
            return Ok("You are an Admin in Finance. Here’s the financial report 📊.");
        }
    }
}

using API.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class AcadimicYearsLevelsController : Controller
    {
        // constructor with dbcontext
        private readonly SchoolManagementSystemContext _context;
        public AcadimicYearsLevelsController(SchoolManagementSystemContext context)
        {
            _context = context;
        }

        
    }
}

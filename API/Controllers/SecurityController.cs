using API.Module;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SecurityController : Controller
    {
        private readonly SchoolManagementSystemContext _context;
        public SecurityController(SchoolManagementSystemContext context)
        {
            _context = context;
        }

        // Login 


        // register


        // email activation


    }
}

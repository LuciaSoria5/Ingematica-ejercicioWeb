using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EjercicioWeb_SoriaLucia.Pages
{
    public class RedesModel : PageModel
    {
        private readonly ILogger<RedesModel> _logger;

        public RedesModel(ILogger<RedesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}

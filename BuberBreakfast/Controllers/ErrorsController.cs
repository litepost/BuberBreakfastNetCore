using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast;

public class ErrorsController: ControllerBase {
    [Route("/error")]
    public IActionResult Error() {
        return Problem();
    }
}

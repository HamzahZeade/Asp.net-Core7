using Microsoft.AspNetCore.Mvc;

namespace Temp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TService> : ControllerBase
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly TService CurrentService;
        public BaseController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            CurrentService = GetService<TService>();
        }


        protected T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

    }
}

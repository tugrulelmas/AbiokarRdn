using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AbiokaRdn.Host.Controllers
{
    public abstract class BaseReadController<T> : BaseApiController where T : DTO
    {
        private readonly IReadService<T> readService;

        public BaseReadController(IReadService<T> readService) {
            this.readService = readService;
        }
        
        [HttpGet]
        public virtual IActionResult Get() {
            var result = readService.GetAll();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public virtual IActionResult Get(Guid id) {
            var result = readService.Get(id);
            return Ok(result);
        }
        
        [HttpGet]
        public virtual IActionResult Get(int page, int limit, string order) {
            var result = readService.GetWithPage(page, limit, order);
            return Ok(result);
        }
    }
}

using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AbiokaRdn.Host.Controllers
{
    public abstract class BaseCrudController<T> : BaseReadController<T> where T : DTO
    {
        private readonly ICrudService<T> crudService;

        public BaseCrudController(ICrudService<T> crudService)
            : base(crudService) {
            this.crudService = crudService;
        }

        [HttpPost]
        public IActionResult Add([FromBody]T entity) {
            crudService.Add(entity);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) {
            crudService.Delete(id);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]T entity) {
            crudService.Update(entity);

            return Ok();
        }
    }
}

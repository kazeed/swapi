using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using swplus.api.Db;
using swplus.api.Models;
using System;
using System.Collections.Generic;

namespace swplus.api.Controllers
{
    [ApiController]
    [Route("api/character")]
    public class CharacterController : ControllerBase
    {
        private readonly ILogger<CharacterController> logger;
        private readonly ICharacterRepository repository;

        public CharacterController(ILogger<CharacterController> logger, ICharacterRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<Character>))]
        public IActionResult GetAll()
        {
            logger.LogInformation("Entered GET /api/character");
            var list = repository.GetAll();

            return new OkObjectResult(list);
        }

        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(Character))]
        public IActionResult GetById(Guid id)
        {
            logger.LogInformation("Entered GET /api/character/{id}");
            var existing = repository.GetById(id);

            return new OkObjectResult(existing);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Character))]
        public IActionResult Create([FromBody] Character item)
        {
            logger.LogInformation("Entered POST /api/character");
            // Todo Validate item
            var added =  repository.Add(item);

            return new CreatedAtRouteResult(nameof(Create), added);
        }

        [HttpPut("id")]
        [ProducesResponseType(200, Type = typeof(Character))]
        public IActionResult Edit(Guid id, [FromBody] Character item)
        {
            logger.LogInformation("Entered POST /api/character");
            // Todo Validate item
            var edited = repository.Edit(id, item);

            return new OkObjectResult(edited);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public IActionResult Delete(Guid id)
        {
            repository.Delete(id);

            return new NoContentResult();
        }
    }
}

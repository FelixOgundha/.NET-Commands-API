using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdAPI.Models;

namespace CmdAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        // we use controller base if we are not using views
        private readonly CommandContext _context;

        public CommandsController(CommandContext context){
            _context=context;
        }

        //GET:          api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands(){
            return _context.CommandItems;
        }

        //GET:          api/commands/n
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id){
            var commandItem = _context.CommandItems.Find(id);

            if(commandItem is null){
                return NotFound();
            }

            return commandItem;
        }

        //POST          api/commands
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command){
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("PostCommandItem",new Command{Id = command.Id},command);
        }

    }
}
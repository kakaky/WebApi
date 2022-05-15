using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParanaBancoPB.Controllers
{
    [Route("ParanaBanco/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
     

        private readonly DataContext _context;
        //Carrega todo o banco de dados e coloca na memória com o nome da variável context
        // a tabela do banco de dados fica dentro da variável context
        public CadastroController (DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Cadastro>>> Get() {
            return Ok(await _context.Cadastros.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Cadastro>>> GetUsuario(int id)
        {
            var usuario = await _context.Cadastros.FindAsync(id);
            if(usuario == null)
            {
                return BadRequest("Usuario não encontrado!");
            }
            return Ok(usuario);
        }
        [HttpPost]
        public async Task<ActionResult<List<Cadastro>>> AddUsuario(Cadastro usuario) {

            _context.Cadastros.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(await _context.Cadastros.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Cadastro>>> UpdateUsuario(Cadastro request)
        {
            var dbusuario = await _context.Cadastros.FindAsync(request.Id);
            if (dbusuario == null)
            {
                return BadRequest("Usuario não encontrado!");
            }
            dbusuario.Name = request.Name;
            dbusuario.Email = request.Email;
            dbusuario.Senha = request.Senha;
            dbusuario.Role = request.Role;
            await _context.SaveChangesAsync();
            return Ok(await _context.Cadastros.ToListAsync());
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cadastro>>> DeleteUsuario(int id)
        {
            var dbusuario = await _context.Cadastros.FindAsync(id);
            if (dbusuario == null)
            {
                return BadRequest("Usuario não encontrado!");
            }
            _context.Cadastros.Remove(dbusuario);
             await _context.SaveChangesAsync();
            return Ok(await _context.Cadastros.ToListAsync());
        }
    }
}

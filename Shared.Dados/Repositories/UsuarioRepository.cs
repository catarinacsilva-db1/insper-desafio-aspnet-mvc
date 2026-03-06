using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _db;

        public UsuarioRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UsuarioModel> AdicionarAsync(UsuarioModel usuario)
        {
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return usuario;
        }

        public async Task<UsuarioModel> AtualizarAsync(UsuarioModel usuario)
        {
            var usuarioExistente = await BuscarPorIdAsync(usuario.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Sobrenome = usuario.Sobrenome;
                usuarioExistente.NomeSocial = usuario.NomeSocial;
                usuarioExistente.DataNascimento = usuario.DataNascimento;
                usuarioExistente.Cpf = usuario.Cpf;
                usuarioExistente.Senha = usuario.Senha;

                await _db.SaveChangesAsync();
                return usuarioExistente;
            }
            return usuario;
        }

        public async Task<UsuarioModel> AtualizaStatusAsync(UsuarioModel usuario)
        {
            usuario.Ativo = !usuario.Ativo;
            await _db.SaveChangesAsync();
            return usuario;
        }

        public async Task<List<UsuarioModel>> ListarTodosAsync()
        {
            return await _db.Usuarios.AsNoTracking().ToListAsync();
        }
    
        public async Task RemoverAsync(UsuarioModel usuario)
        {
            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();
        }
        public async Task<UsuarioModel> BuscarPorIdAsync(int id)
        {
            return await _db.Usuarios.FindAsync(id);
        }

        public async Task<bool> ExisteCpfAsync(string cpf, int id = 0)
        {
            if (id == 0)
            {
                return await _db.Usuarios.AnyAsync(u => u.Cpf == cpf);
            }
            return await _db.Usuarios.AnyAsync(u => u.Cpf == cpf && u.Id != id);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
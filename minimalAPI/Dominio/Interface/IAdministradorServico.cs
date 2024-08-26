using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidade;

namespace minimal_api.Dominio.Interface
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDTO);
        Administrador Incluir(Administrador administrador);
        List<Administrador> Todos(int? pagina);
        Administrador? BuscarPorId(int id);

    }
}

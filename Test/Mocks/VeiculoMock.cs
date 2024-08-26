using minimal_api.Dominio.Entidade;
using minimal_api.Dominio.Interface;

namespace Test.Mocks
{
    internal class VeiculoMock : IVeiculoServico
    {
        private static List<Veiculo> veiculos = new List<Veiculo>()
        {
            new Veiculo
            {
                Id = 1,
                Nome = "Fusca",
                Marca = "Volkswagen",
                Ano = 1999
            }
        };
        public void Apagar(Veiculo veiculo)
        {
            var veiculoExistente = veiculos.FirstOrDefault(v => v.Id == veiculo.Id);
            if (veiculoExistente != null)
            {
                veiculos.Remove(veiculoExistente);
            }
            else
            {
                throw new ArgumentException("Veículo não encontrado.");
            }
        }

        public void Atualizar(Veiculo veiculo)
        {
            var veiculoExistente = veiculos.FirstOrDefault(v => v.Id == veiculo.Id);
            if (veiculoExistente != null)
            {
                veiculoExistente.Nome = veiculo.Nome;
                veiculoExistente.Marca = veiculo.Marca;
                veiculoExistente.Ano = veiculo.Ano;
            }
            else
            {
                throw new ArgumentException("Veículo não encontrado.");
            }
        }

        public Veiculo? BuscarPorId(int id)
        {
            return veiculos.Find(x => x.Id == id);
        }

        public void Incluir(Veiculo veiculo)
        {
            veiculo.Id = veiculos.Count() + 1;
            veiculos.Add(veiculo);
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
            return veiculos;
        }
    }
}

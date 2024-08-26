using minimal_api.Dominio.Entidade;

namespace Test.Domain
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {
            //Arrange
            var veic = new Veiculo();

            //Act
            veic.Id = 1;
            veic.Nome = "Fusca";
            veic.Marca = "Volkswagen";
            veic.Ano = 1999;

            //Assert
            Assert.AreEqual(1, veic.Id);
            Assert.AreEqual("Fusca", veic.Nome);
            Assert.AreEqual("Volkswagen", veic.Marca);
            Assert.AreEqual(1999, veic.Ano);
        }
    }
}

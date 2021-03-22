using Animais.Controllers;
using Animais.Data;
using Animais.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace AnimaisTest
{
    public class ControllerAnimaisTest
    {
        public AnimaisController _animaisController;

        public ControllerAnimaisTest()
        {
            _animaisController = new AnimaisController(new AnimalStore());
        }

        [Fact]
        public void TesteGetListaAnimais()
        {
            var responseGet = _animaisController.Get().Result;

            var responseList = Assert.IsType<OkObjectResult>(responseGet).Value;

            var count = Assert.IsType<List<Animal>>(responseList);
            Assert.Equal(3, count.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TesteVerificaIdsAnimais(int id)
        {
            var responseGetId = _animaisController.GetAnimalId(id);

            Assert.IsType<OkObjectResult>(responseGetId);
        }

        [Fact]
        public void TesteInsercaoDeAnimalEBuscaDoAnimalInserido()
        {
            var modelo = new Animal
            {
                Nome = "Macaco",
                Tipo = "Mamifero"
            };

            var modelo2 = new Animal
            {
                Nome = "Pica-Pau",
                Tipo = "Ave"
            };

            var responsePost = _animaisController.Post(modelo);
            var responsePost2 = _animaisController.Post(modelo2);

            var responseGet = _animaisController.Get().Result;

            var responseGetList = Assert.IsType<OkObjectResult>(responseGet).Value;
            var listaAnimais = Assert.IsType<List<Animal>>(responseGetList);

            Assert.IsType<OkResult>(responsePost);
            Assert.IsType<OkResult>(responsePost2);
            Assert.Equal(5, listaAnimais.Count);
            Assert.Equal(3, listaAnimais.FindIndex(x => x == modelo));
            Assert.Equal(4, listaAnimais.FindIndex(x => x == modelo2));
        }

        [Theory]
        [InlineData("Pardal")]
        [InlineData("Tigre")]
        [InlineData("Onca")]
        public void TesteBuscaDeAnimaisAtuaisEDeNovasIncercoes(string nome)
        {
            var responseGetName = _animaisController.GetAnimalNome(nome).Result;

            var model = new Animal()
            {
                Nome = "Baleia",
                Tipo = "Mamifero"
            };

            var responsePost = _animaisController.Post(model);

            var responseGetResult = _animaisController.GetAnimalNome(model.Nome).Result;

            var responseGetValue = Assert.IsType<OkObjectResult>(responseGetResult).Value;

            var listaAnimais = Assert.IsType<List<object>>(responseGetValue);


            Assert.IsType<OkResult>(responsePost);
            Assert.IsType<OkObjectResult>(responseGetName);
            Assert.NotNull(listaAnimais);
        }

        [Fact]
        public void TesteAtualizacaoDeAnimal()
        {
            var modelo = new Animal()
            {
                Id = 1,
                Nome = "Papagaio",
                Tipo = "Ave"
            };

            var responsePut = _animaisController.Put(modelo.Id, modelo);

            var responseGet = _animaisController.GetAnimalId(modelo.Id);

            var responseValue = Assert.IsType<OkObjectResult>(responseGet).Value;

            var responseAnimal = Assert.IsType<Animal>(responseValue);

            Assert.IsType<OkObjectResult>(responsePut);
            Assert.Equal(responseAnimal.Nome, modelo.Nome);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TesteDeletarAnimal(int id)
        {
            var resultResponse = _animaisController.Delete(id);

            Assert.IsType<OkResult>(resultResponse);

            var responseGetId = _animaisController.GetAnimalId(id);

            Assert.IsType<NotFoundObjectResult>(responseGetId);
        }
    }
}
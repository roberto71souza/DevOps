using Animais.Data;
using Animais.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animais.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimaisController : ControllerBase
    {
        private AnimalStore _animalStore { get; set; }

        public AnimaisController(AnimalStore animal)
        {
            _animalStore = animal;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var lista = _animalStore.ListarAnimais();
            if (lista.Count > 0)
            {
                return Ok(lista);
            }
            return NotFound("Lista Vazia");
        }

        [HttpGet("GetAnimalId/{id}")]
        public ActionResult GetAnimalId(int id)
        {
            if (id > 0)
            {
                var buscaAnimalId = _animalStore.BuscarAnimalId(id);
                if (buscaAnimalId != null)
                {
                    return Ok(buscaAnimalId);
                }
                return NotFound("Animal nao encontrado");
            }

            return BadRequest();
        }

        [HttpGet("GetAnimalNome/{nome}")]
        public ActionResult GetAnimalNome(string nome)
        {
            if (!string.IsNullOrWhiteSpace(nome))
            {
                var buscaAnimaisNome = _animalStore.BuscarAnimalNome(nome);
                if (buscaAnimaisNome.Count() > 0)
                {
                    return Ok(buscaAnimaisNome);
                }
            }
            return NotFound($"Animal {nome} não encontrado!");
        }

        [HttpPost]
        public ActionResult Post(Animal modelo)
        {
            _animalStore.AdicionarAnimal(modelo);
            return Ok();
        }

        [HttpPut("atualizar/{id}")]
        public ActionResult Put(int id, Animal modelo)
        {
            var animal = _animalStore.BuscarAnimalId(id);

            if (animal != null)
            {
                _animalStore.AtualizarAnimal(animal,modelo);
                return Ok("Animal atualizado");
            }
            return NotFound("Animal nao encontrado");
        }

        [HttpDelete("ExcluirAnimal/{id}")]
        public ActionResult Delete(int id)
        {
            var animal = _animalStore.BuscarAnimalId(id);
            if (animal != null)
            {
                _animalStore.ExcluirAnimal(animal);
                return Ok();
            }
            return NotFound("Animal nao encontrado");
        }
    }
}

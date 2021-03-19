using Animais.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animais.Data
{
    public class AnimalStore
    {
        private List<Animal> AnimaisList { get; set; }
        public AnimalStore()
        {
            AnimaisList = new List<Animal> { new Animal { Id=1,Nome="Pardal",Tipo="Ave"},
            new Animal { Id=2,Nome="Tigre",Tipo="Mamifero"},
            new Animal { Id=3,Nome="Onca",Tipo="Mamifero"}};
        }
        public List<Animal> ListarAnimais()
        {
            var result = this.AnimaisList.ToList();
            return result;
        }
        public void AdicionarAnimal(Animal modelo)
        {
            var idcount = AnimaisList.Count();
            idcount++;
            modelo.Id = idcount;
            AnimaisList.Add(modelo);
        }

        public void AtualizarAnimal(Animal animalAntigo, Animal animalNovo)
        {
            animalAntigo.Nome = animalNovo.Nome;
            animalAntigo.Tipo = animalNovo.Tipo;
        }

        public Animal BuscarAnimalId(int id)
        {
            var buscaId = AnimaisList.FirstOrDefault(x => x.Id == id);
            return buscaId;
        }
        public IEnumerable<object> BuscarAnimalNome(string nome)
        {
            var buscaNome = from animal in AnimaisList where animal.Nome.ToLower().Contains(nome.ToLower()) select new { Nome = animal.Nome, Tipo = animal.Tipo };
            return buscaNome;
        }

        public void ExcluirAnimal(Animal modelo)
        {
            AnimaisList.Remove(modelo);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ParadaListViewModel : BaseListViewModel<ParadaWrapper>
    {
        public ParadaListViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        protected override Task ExecuteItemClickCommand(ParadaWrapper item)
            => _navigationService.NavigateToAsync<ParadaDetailViewModel>(item);
        protected override Task<IEnumerable<ParadaWrapper>> GetDataAsync()
        {
            var posto1 = new ParadaWrapper
            {
                Title = "Posto Costa Brava",
                Description = "Rodovia Régis Bittencourt",
                Numero = "Km 67",
                Cep = "83420000",
                Rating = 4,
                Telefone = "4136721683"
            };
            var posto2 = new ParadaWrapper
            {
                Title = "Graal Alemão",
                Description = "Rodovia Presidente Dutra - BR 116",
                Numero = "Km 12",
                Cep = "12800000",
                Rating = 4,
                Telefone = "1231471195"
            };
            var posto3 = new ParadaWrapper
            {
                Title = "Frango Assado",
                Description = "Rodovia dos Bandeirantes",
                Numero = "Km 34",
                Cep = "7700000",
                Rating = 5,
                Telefone = "1144474800"
            };
            var posto4 = new ParadaWrapper
            {
                Title = "Posto Campeao 38",
                Description = "Rodovia dos Bandeirantes",
                Numero = "Km 38",
                Cep = "7750000",
                Rating = 5,
                Telefone = "1150805519"
            };
            var posto5 = new ParadaWrapper
            {
                Title = "Posto de parada Graal 56 Sul",
                Description = "Rodovia dos Bandeirantes"
            };
            var list = new List<ParadaWrapper>
            {
                posto1, posto2, posto3, posto4, posto5, posto1, posto2, posto3, posto4, posto5
            };
            return Task.FromResult<IEnumerable<ParadaWrapper>>(list);
        }
    }
}

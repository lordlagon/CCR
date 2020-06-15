using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CuponsViewModel : BaseListViewModel<CupomWrapper>
    {
        public CuponsViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        protected override Task<IEnumerable<CupomWrapper>> GetDataAsync()
        {
            var posto1 = new CupomWrapper
            {
                Title = "Vale Banho: 1",
                Description = "DBT00001",
            };
            var posto2 = new CupomWrapper
            {
                Title = "Vale Pernoite: 5",
                Description = "SLT00001",
            };
            var posto3 = new CupomWrapper
            {
                Title = "Vale Alimentação Buffet: 1",
                Description = "APF00001",
            };
            var posto4 = new CupomWrapper
            {
                Title = "Vale Aliementação PF: 6",
                Description = "ABT00001"
            };
            var posto5 = new CupomWrapper
            {
                Title = "Vale Banho: 2",
                Description = "DBT00051"
            };
            var list = new List<CupomWrapper>
            {
                posto1, posto2, posto3, posto4, posto5, posto1, posto2, posto3, posto4, posto5
            };
            return Task.FromResult<IEnumerable<CupomWrapper>>(list);
        }
    }
}

using CIDER.MVVMBase;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class MapRouteViewModel:ViewModelBase
    {
        DataProvider _data;
        private ApplicationIdCredentialsProvider _apiKey;
        public MapRouteViewModel(DataProvider data)
        {
            _data = data;

            APIKey = new ApplicationIdCredentialsProvider(data.APIKey);
        }

        public ApplicationIdCredentialsProvider APIKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }
    }
}

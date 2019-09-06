using CIDER.MVVMBase;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class MapTimedViewModel:ViewModelBase
    {
        private ApplicationIdCredentialsProvider _apiKey;
        private DataProvider _data;
        public MapTimedViewModel(DataProvider data)
        {
            _data = data;

            //set the api key read from the key file
            APIKey = new ApplicationIdCredentialsProvider(data.APIKey);
        }

        public ApplicationIdCredentialsProvider APIKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }
    }
}

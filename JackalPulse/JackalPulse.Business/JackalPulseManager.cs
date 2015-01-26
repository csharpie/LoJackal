using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackalPulse.Business
{
    public class JackalPulseManager
    {
        public readonly PublicApi _api;

        public JackalPulseManager(string uri)
        {
            _api = new PublicApi(uri);
        }

        public double GetTimeInterval(string confirmTimerIntervalRoute)
        {
            return _api.GetTimerInterval(confirmTimerIntervalRoute);
        }

        public void Post(string confirmOnlineRoute)
        {
            _api.Save(confirmOnlineRoute);
        }
    }
}

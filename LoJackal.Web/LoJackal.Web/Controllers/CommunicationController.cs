using LoJackal.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

namespace LoJackal.Web.Controllers
{
    public class CommunicationController : ApiController
    {
        private double _timeInterval = 0;

        public CommunicationController()
        {
            _timeInterval = Convert.ToDouble(new Models.LoJackal().Configurations.Where(k => k.Key == "TimeInterval").First().Value);
        }

        [HttpGet]
        public double ConfirmTimeInterval(HttpRequestMessage request)
        {
            return _timeInterval;
        }

        [HttpPost]
        public double ConfirmOnline(HttpRequestMessage request, [FromUri]string computerName)
        {
            var res = new HttpResponseMessage();

            var ip = GetIP(request);

            using (var db = new Models.LoJackal())
            {
                var communication = new Communication()
                {
                    ComputerName = computerName,
                    IPAddress = ip,
                    LastCommunicated = DateTime.UtcNow
                };

                db.Communications.Add(communication);

                var saved = db.SaveChanges();

                if (saved == 1)
                {
                    return _timeInterval;
                }
                else
                {
                    return _timeInterval / 2;
                }
            }
        }

        private string GetIP(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else
            {
                return null;
            }
        }
    }
}

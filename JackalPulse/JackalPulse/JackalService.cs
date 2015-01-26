using JackalPulse.Business;
using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace JackalPulse
{
    public partial class JackalService : ServiceBase
    {
        private readonly double _defaultInterval;
        private readonly Timer _timer;
        private readonly JackalPulseManager _manager;
        private readonly string _confirmTimerIntervalRoute;
        private readonly string _confirmOnlineRoute;

        public JackalService()
        {
            InitializeComponent();

            _defaultInterval = Double.Parse(ConfigurationManager.AppSettings["DefaultInterval"]);
            _confirmTimerIntervalRoute = ConfigurationManager.AppSettings["ConfirmTimeIntervalRoute"];
            _confirmOnlineRoute = ConfigurationManager.AppSettings["ConfirmOnlineRoute"];
            
            
            var uri = ConfigurationManager.AppSettings["Uri"];

            _manager = new JackalPulseManager(uri);
            _timer = new Timer();
            _timer.Elapsed += OnTimedEvent;
        }

        protected override void OnStart(string[] args)
        {
            _timer.Interval = _manager.GetTimeInterval(_confirmTimerIntervalRoute);
            _timer.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _manager.Post(_confirmOnlineRoute);
        }
    }
}

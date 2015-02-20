using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Timers;
using EvoPaj.Logic;

namespace EvoPaj.Logic.Utility
{
    public class EmailEngine
    {
        private Timer timer = null;
        public EmailEngine()
        {

            this.timer = new Timer();
            this.timer.AutoReset = false;
            this.timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
        }

        public void Start()
        {            
            this.timer.Start();
        }

        public void Stop()
        {           
            if (this.timer != null)
            {
                this.timer.Stop();
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();
            try
            {
                //new PANE.ERRORLOG.Error().LogToFile(new Exception("Before entering the main method for pending message"));
                new EmailSystem().SendPendingEmails();
                this.timer.Interval = 1000 * Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimerInterval"]);
            }
            catch(Exception ex)
            {
                new PANE.ERRORLOG.Error().LogToFile(ex);
            }
            finally
            {
                this.timer.Start();
            }
        }
    }
}

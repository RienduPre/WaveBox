using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using WaveBox.Core.Injected;
using WaveBox.Service.Services.Cron;
using WaveBox.Service.Services.Cron;
using WaveBox.Static;

namespace WaveBox.Service.Services
{
	class CronService : IService
	{
		private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public string Name { get { return "cron"; } set { } }

		public bool Required { get { return true; } set { } }

		public CronService()
		{
		}

		public bool Start()
		{
			// FOR NOW: Use the pre-existing delayed operation queues.  Once more work has been done, cron will
			// run a unified timer and spin each of these off as needed

			// Start podcast download queue
			DownloadQueue.FeedChecks.queueOperation(new FeedCheckOperation(0));
			DownloadQueue.FeedChecks.startQueue();

			// Start session scrub operation
			SessionScrub.Queue.queueOperation(new SessionScrubOperation(0));
			SessionScrub.Queue.startQueue();
			return true;
		}

		public bool Stop()
		{
			// Stop all queues
			DownloadQueue.FeedChecks = null;
			SessionScrub.Queue = null;
			return true;
		}
	}
}

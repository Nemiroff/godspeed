﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Microsoft.Practices.Composite.Events;
using NAppUpdate.Framework;
using NAppUpdate.Framework.Sources;
using Neurotoxin.Godspeed.Presentation.Infrastructure;
using Neurotoxin.Godspeed.Shell.Constants;
using Neurotoxin.Godspeed.Shell.Events;
using Neurotoxin.Godspeed.Shell.Helpers;
using Neurotoxin.Godspeed.Shell.Interfaces;
using Neurotoxin.Godspeed.Shell.Views.Dialogs;

namespace Neurotoxin.Godspeed.Shell.Controllers
{
    public class NotificationService
    {
        private const string NEWVERSIONAVAILABLEMESSAGEKEY = "NewVersionAvailableMessage";

        private readonly Queue<NotifyUserMessageEventArgs> _messageQueue = new Queue<NotifyUserMessageEventArgs>();

        private readonly IEventAggregator _eventAggregator;
        private readonly IUserSettingsProvider _userSettings;
        private readonly IWorkHandler _workHandler;

        public NotificationService(IEventAggregator eventAggregator, IStatisticsViewModel statistics, IUserSettingsProvider userSettings, IWorkHandler workHandler)
        {
            _eventAggregator = eventAggregator;
            _userSettings = userSettings;
            _workHandler = workHandler;

            eventAggregator.GetEvent<ShellInitializedEvent>().Subscribe(OnShellInitialized);
        }

        private void OnShellInitialized(ShellInitializedEventArgs obj)
        {
            foreach(var message in _messageQueue)
            {
                _eventAggregator.GetEvent<NotifyUserMessageEvent>().Publish(message);
            }
            if (_userSettings.UseVersionChecker)
            {
                CheckForNewerVersion();
            }
        }

        private void CheckForNewerVersion()
        {
            var feedReader = new NauXmlFeedReaderEx();
            var updManager = UpdateManager.Instance;
            updManager.UpdateSource = new SimpleWebSource(ConfigurationManager.AppSettings["UpdateSourceUrl"]);
            updManager.UpdateFeedReader = feedReader;
            updManager.ReinstateIfRestarted();
            updManager.Config.UpdateProcessName = "GODspeed";
            updManager.Config.UpdateExecutableName = "godspeed.exe";
            if (updManager.State == UpdateManager.UpdateProcessState.NotChecked)
            {
                _workHandler.Run(() =>
                {
                    updManager.CheckForUpdates();
                    return string.Compare(feedReader.LatestVersionOnServer, App.ApplicationVersion, StringComparison.InvariantCultureIgnoreCase) == 1 && updManager.UpdatesAvailable > 0;
                }, hasUpdate =>
                {
                    if (!hasUpdate) return;
                    var args = new NotifyUserMessageEventArgs(NEWVERSIONAVAILABLEMESSAGEKEY, MessageIcon.Info, MessageCommand.OpenDialog, typeof(AutoUpdateDialog), MessageFlags.Ignorable, feedReader.LatestVersionOnServer);
                    _eventAggregator.GetEvent<NotifyUserMessageEvent>().Publish(args);
                });
            }
        }

        public void QueueMessage(NotifyUserMessageEventArgs message)
        {
            _messageQueue.Enqueue(message);
        }

        public void QueueMessages(IEnumerable<NotifyUserMessageEventArgs> messages)
        {
            foreach (var message in messages)
            {
                QueueMessage(message);
            }
        }
    }
}
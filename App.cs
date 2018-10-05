﻿using Player.Instances;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Player
{
	public partial class App : Application, ISingleInstanceApp
	{
		public static event EventHandler<InstanceEventArgs> NewInstanceRequested;

		[STAThread]
		public static void Main()
		{
			Settings.AppPath = Environment.GetCommandLineArgs()[0].Substring(0, Environment.GetCommandLineArgs()[0].LastIndexOf("\\") + 1);
			AppDomain.CurrentDomain.ProcessExit += (_, __) => Hook.Events.Dispose();
			AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
				MessageBox.Show($"Unhandled {e.ExceptionObject}\r\n", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			if (Instance<App>.InitializeAsFirstInstance("ElephantIPC_soheilkd"))
			{
				App application = new App();
				application.InitializeComponent();
				application.Run();
				Instance<App>.Cleanup();
			}
		}

		public bool SignalExternalCommandLineArgs(IList<string> args)
		{
			args.Remove(Environment.GetCommandLineArgs()[0]);
			NewInstanceRequested?.Invoke(this, new InstanceEventArgs(args));
			return true;
		}
	}
}

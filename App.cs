﻿using Library.Instances;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Player
{
	public partial class App : Application, ISingleInstanceApp
	{
		public static event EventHandler<InstanceEventArgs> NewInstanceRequested;
		public static readonly string Path = @"C:\Program Files\soheilkd\Player\";

		[STAThread]
		public static void Main()
		{
			AppDomain.CurrentDomain.ProcessExit += (_, __) => Library.Hook.Events.Dispose();
			if (Instance<App>.InitializeAsFirstInstance("ElephantIPC_soheilkd"))
			{
				var application = new App();
				application.InitializeComponent();
				Current.Resources["WindowsColor"] = new SolidColorBrush(Library.Controls.Colors.WindowsColor);
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

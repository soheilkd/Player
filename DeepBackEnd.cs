﻿using Player.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using System.Windows.Threading;

namespace Player.DeepBackEnd
{
	internal enum WM
	{
		NULL = 0x0000, CREATE = 0x0001, DESTROY = 0x0002, MOVE = 0x0003, SIZE = 0x0005, ACTIVATE = 0x0006,
		SETFOCUS = 0x0007, KILLFOCUS = 0x0008, ENABLE = 0x000A, SETREDRAW = 0x000B, SETTEXT = 0x000C, GETTEXT = 0x000D,
		GETTEXTLENGTH = 0x000E, PAINT = 0x000F, CLOSE = 0x0010, QUERYENDSESSION = 0x0011, QUIT = 0x0012, QUERYOPEN = 0x0013,
		ERASEBKGND = 0x0014, SYSCOLORCHANGE = 0x0015, SHOWWINDOW = 0x0018, ACTIVATEAPP = 0x001C, SETCURSOR = 0x0020, MOUSEACTIVATE = 0x0021,
		CHILDACTIVATE = 0x0022, QUEUESYNC = 0x0023, GETMINMAXINFO = 0x0024, WINDOWPOSCHANGING = 0x0046, WINDOWPOSCHANGED = 0x0047,
		CONTEXTMENU = 0x007B, STYLECHANGING = 0x007C, STYLECHANGED = 0x007D, DISPLAYCHANGE = 0x007E, GETICON = 0x007F, SETICON = 0x0080,
		NCCREATE = 0x0081, NCDESTROY = 0x0082, NCCALCSIZE = 0x0083, NCHITTEST = 0x0084, NCPAINT = 0x0085, NCACTIVATE = 0x0086,
		GETDLGCODE = 0x0087, SYNCPAINT = 0x0088, NCMOUSEMOVE = 0x00A0, NCLBUTTONDOWN = 0x00A1, NCLBUTTONUP = 0x00A2, NCLBUTTONDBLCLK = 0x00A3,
		NCRBUTTONDOWN = 0x00A4, NCRBUTTONUP = 0x00A5, NCRBUTTONDBLCLK = 0x00A6, NCMBUTTONDOWN = 0x00A7, NCMBUTTONUP = 0x00A8, NCMBUTTONDBLCLK = 0x00A9,
		SYSKEYDOWN = 0x0104, SYSKEYUP = 0x0105, SYSCHAR = 0x0106, SYSDEADCHAR = 0x0107, COMMAND = 0x0111, SYSCOMMAND = 0x0112,
		LBUTTONDOWN = 0x0201, LBUTTONUP = 0x0202, LBUTTONDBLCLK = 0x0203, RBUTTONDOWN = 0x0204, RBUTTONUP = 0x0205, RBUTTONDBLCLK = 0x0206,
		MBUTTONDOWN = 0x0207, MBUTTONUP = 0x0208, MBUTTONDBLCLK = 0x0209, MOUSEWHEEL = 0x020A, MOUSEHWHEEL = 0x020E, MOUSEMOVE = 0x0200,
		XBUTTONDOWN = 0x020B, XBUTTONUP = 0x020C, XBUTTONDBLCLK = 0x020D, CAPTURECHANGED = 0x0215, ENTERSIZEMOVE = 0x0231, EXITSIZEMOVE = 0x0232,
		IME_SETCONTEXT = 0x0281, IME_NOTIFY = 0x0282, IME_CONTROL = 0x0283, IME_COMPOSITIONFULL = 0x0284, IME_SELECT = 0x0285, IME_CHAR = 0x0286,
		IME_REQUEST = 0x0288, IME_KEYDOWN = 0x0290, IME_KEYUP = 0x0291, NCMOUSELEAVE = 0x02A2, USER = 0x0400, TRAYMOUSEMESSAGE = 0x800, APP = 0x8000,
		DWMCOMPOSITIONCHANGED = 0x031E, DWMNCRENDERINGCHANGED = 0x031F, DWMCOLORIZATIONCOLORCHANGED = 0x0320,
		DWMWINDOWMAXIMIZEDCHANGE = 0x0321, DWMSENDICONICTHUMBNAIL = 0x0323, DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326
	}
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		public delegate IntPtr MessageHandler(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
		[DllImport("shell32.dll", EntryPoint = "CommandLineToArgvW", CharSet = CharSet.Unicode)]
		private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);
		[DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
		private static extern IntPtr _LocalFree(IntPtr hMem);
		internal enum ShellAddToRecentDocsFlags
		{
			Pidl = 0x001,
			Path = 0x002,
		}
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "1"), DllImport("shell32.dll", CharSet = CharSet.Ansi)]
		public static extern void SHAddToRecentDocs(ShellAddToRecentDocsFlags flag, [MarshalAs(UnmanagedType.LPStr)] string path);
		public static string[] CommandLineToArgvW(string cmdLine)
		{
			IntPtr argv = IntPtr.Zero;
			try
			{
				argv = _CommandLineToArgvW(cmdLine, out int numArgs);
				if (argv == IntPtr.Zero)
					throw new Win32Exception();
				var result = new string[numArgs];
				for (int i = 0; i < numArgs; i++)
				{
					IntPtr currArg = Marshal.ReadIntPtr(argv, i * Marshal.SizeOf(typeof(IntPtr)));
					result[i] = Marshal.PtrToStringUni(currArg);
				}
				return result;
			}
			finally
			{
				IntPtr p = _LocalFree(argv);
			}
		}
	}

}

namespace Player.DeepBackEnd.InstanceManagement
{
	public class InstanceEventArgs : EventArgs
	{
		private InstanceEventArgs() { }
		public InstanceEventArgs(IList<string> args) { _Args = args; }
		private IList<string> _Args { get; set; }
		public string this[int index] => Args[index];
		public int ArgsCount => _Args.Count;
		public string[] Args => _Args.ToArray();
	}
	public interface ISingleInstanceApp
	{
		bool SignalExternalCommandLineArgs(IList<string> args);
	}

	public static class Instance<TApplication>
				where TApplication : Application, ISingleInstanceApp
	{
		private const string Delimiter = ":";
		private const string ChannelNameSuffix = "SingeInstanceIPCChannel";
		private const string RemoteServiceName = "SingleInstanceApplicationService";
		private const string IpcProtocol = "ipc://";
		private static Mutex singleInstanceMutex;
		private static IpcServerChannel channel;
		public static IList<string> CommandLineArgs { get; private set; }

		public static bool InitializeAsFirstInstance(string uniqueName)
		{
			CommandLineArgs = GetCommandLineArgs(uniqueName);
			string applicationIdentifier = uniqueName + Environment.UserName;
			string channelName = String.Concat(applicationIdentifier, Delimiter, ChannelNameSuffix);
			singleInstanceMutex = new Mutex(true, applicationIdentifier, out bool firstInstance);
			if (firstInstance)
				CreateRemoteService(channelName);
			else
				SignalFirstInstance(channelName, CommandLineArgs);
			return firstInstance;
		}
		public static void Cleanup()
		{
			if (singleInstanceMutex != null)
			{
				singleInstanceMutex.Close();
				singleInstanceMutex = null;
			}
			if (channel != null)
			{
				ChannelServices.UnregisterChannel(channel);
				channel = null;
			}
		}
		private static IList<string> GetCommandLineArgs(string uniqueApplicationName)
		{
			string[] args = null;
			if (AppDomain.CurrentDomain.ActivationContext == null)
				args = Environment.GetCommandLineArgs();
			else
			{
				string appFolderPath = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), uniqueApplicationName);
				string cmdLinePath = Path.Combine(appFolderPath, "cmdline.txt");
				if (File.Exists(cmdLinePath))
				{
					try
					{
						using (TextReader reader = new StreamReader(cmdLinePath, System.Text.Encoding.Unicode))
							args = NativeMethods.CommandLineToArgvW(reader.ReadToEnd());
						File.Delete(cmdLinePath);
					}
					catch (IOException) { }
				}
			}
			if (args == null)
				args = new string[] { };

			return new List<string>(args);
		}
		private static void CreateRemoteService(string channelName)
		{
			BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider
			{
				TypeFilterLevel = TypeFilterLevel.Full
			};
			IDictionary props = new Dictionary<string, string>
			{
				["name"] = channelName,
				["portName"] = channelName,
				["exclusiveAddressUse"] = "false"
			};
			channel = new IpcServerChannel(props, serverProvider);
			ChannelServices.RegisterChannel(channel, true);
			IPCRemoteService remoteService = new IPCRemoteService();
			RemotingServices.Marshal(remoteService, RemoteServiceName);
		}
		private static void SignalFirstInstance(string channelName, IList<string> args)
		{
			IpcClientChannel secondInstanceChannel = new IpcClientChannel();
			ChannelServices.RegisterChannel(secondInstanceChannel, true);
			string remotingServiceUrl = IpcProtocol + channelName + "/" + RemoteServiceName;
			IPCRemoteService firstInstanceRemoteServiceReference = (IPCRemoteService)RemotingServices.Connect(typeof(IPCRemoteService), remotingServiceUrl);
			firstInstanceRemoteServiceReference?.InvokeFirstInstance(args);
		}
		private static object ActivateFirstInstanceCallback(object arg)
		{
			IList<string> args = arg as IList<string>;
			ActivateFirstInstance(args);
			return null;
		}
		private static void ActivateFirstInstance(IList<string> args)
		{
			if (Application.Current == null)
				return;
			((TApplication)Application.Current).SignalExternalCommandLineArgs(args);
		}
		private class IPCRemoteService : MarshalByRefObject
		{
			public void InvokeFirstInstance(IList<string> args)
			{
				if (Application.Current != null)
					Application.Current.Dispatcher.BeginInvoke(
						DispatcherPriority.Normal, new DispatcherOperationCallback(Instance<TApplication>.ActivateFirstInstanceCallback), args);
			}
			public override object InitializeLifetimeService() => null;
		}
	}
}

namespace Player.Taskbar
{
	public class Command : ICommand
	{
#pragma warning disable CS0067 //Suppres never used warning
		public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
		public event EventHandler Raised;
		public bool CanExecute(object parameter) => true;
		public void Execute(object parameter) => Raised?.Invoke(this, null);
	}
	public class Thumb
	{
		public event EventHandler PlayPressed;
		public event EventHandler PausePressed;
		public event EventHandler PrevPressed;
		public event EventHandler NextPressed;

		public ThumbButtonInfo PlayThumb = new ThumbButtonInfo()
		{
			Description = "Play",
			ImageSource = Images.GetBitmap(Glyph.Play)
		};
		public ThumbButtonInfo PauseThumb = new ThumbButtonInfo()
		{
			Description = "Pause",
			ImageSource = Images.GetBitmap(Glyph.Pause)
		};
		public ThumbButtonInfo PreviousThumb = new ThumbButtonInfo()
		{
			Description = "Previous",
			ImageSource = Images.GetBitmap(Glyph.Previous)
		};
		public ThumbButtonInfo NextThumb = new ThumbButtonInfo()
		{
			Description = "Next",
			ImageSource = Images.GetBitmap(Glyph.Next)
		};

		private Command PlayHandler = new Command();
		private Command PauseHandler = new Command();
		private Command PrevHandler = new Command();
		private Command NextHandler = new Command();
		public TaskbarItemInfo Info { get; } = new TaskbarItemInfo();

		public Thumb()
		{
			PlayHandler.Raised += (sender, e) => PlayPressed?.Invoke(sender, e);
			PauseHandler.Raised += (sender, e) => PausePressed?.Invoke(sender, e);
			PrevHandler.Raised += (sender, e) => PrevPressed?.Invoke(sender, e);
			NextHandler.Raised += (sender, e) => NextPressed?.Invoke(sender, e);
			PreviousThumb.Command = PrevHandler;
			NextThumb.Command = NextHandler;
			PlayThumb.Command = PlayHandler;
			PauseThumb.Command = PauseHandler;

			Info.ThumbButtonInfos.Clear();
			Info.ThumbButtonInfos.Add(PreviousThumb);
			Info.ThumbButtonInfos.Add(PlayThumb);
			Info.ThumbButtonInfos.Add(NextThumb);
		}

		public void SetPlayingState(bool IsPlaying = false) => Info.ThumbButtonInfos[1] = IsPlaying ? PauseThumb : PlayThumb;
		public void SetProgressState(TaskbarItemProgressState state) => Info.ProgressState = state;
		public void SetProgressValue(double value) => Info.ProgressValue = value;
	}
}
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MIUIPlusWindowSizeChanger
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static App Instance;

		public readonly string preferenceDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/MIUIPlusWindowSizeChanger";
		public readonly string preferenceFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/MIUIPlusWindowSizeChanger/preference.txt";

		private Mutex mutex;
		private TaskbarIcon taskbarIcon;
		private WindowSizeChanger windowSizeChanger;

		public float TitleBarThreshold
		{
			get
			{
				return windowSizeChanger.GetTitleBarThreshold();
			}
			set
			{
				if (TitleBarThreshold != value)
					windowSizeChanger.SetTitleBarThreshold(value);
			}
		}

		public int BorderThreshold
		{
			get
			{
				return windowSizeChanger.GetBorderThreshold();
			}
			set
			{
				if (BorderThreshold != value)
					windowSizeChanger.SetBorderThreshold(value);
			}
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			bool ret;
			mutex = new Mutex(true, "StikyNotesAPP", out ret);

			if (!ret)
			{
				MessageBox.Show("已经运行了一个程序。", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
				Environment.Exit(0);
			}

			base.OnStartup(e);

			Instance = this;

			windowSizeChanger = new WindowSizeChanger();

			DirectoryInfo directoryInfo = new DirectoryInfo(preferenceDirectoryPath);
			if (!directoryInfo.Exists)
				directoryInfo.Create();

			FileInfo info = new FileInfo(preferenceFilePath);
			if (!info.Exists)
			{
				using FileStream fileWrite = info.Create();
				byte[] strBytes = (new UTF8Encoding(true)).GetBytes("0.15 5");
				fileWrite.Write(strBytes, 0, strBytes.Length);
				fileWrite.Flush();
			}
			ReadPreference();

			MainLoop();

			taskbarIcon = FindResource("TaskBar") as TaskbarIcon;
		}

		public async void ReadPreference()
		{
			using FileStream fileStream = new FileStream(preferenceFilePath, FileMode.Open);
			byte[] strBytes = new byte[fileStream.Length];
			await fileStream.ReadAsync(strBytes, 0, (int)fileStream.Length);
			string str = (new UTF8Encoding(true)).GetString(strBytes);
			if(str != string.Empty)
			{
				int tempIndex = str.IndexOf(' ');
				TitleBarThreshold = Convert.ToSingle(str.Substring(0, tempIndex));
				BorderThreshold = Convert.ToInt32(str.Substring(tempIndex + 1, str.Length - tempIndex - 1));
			}
		}

		public async void WritePreference(float titleBarThreshold,int borderThreshold)
		{
			using FileStream fileStream = new FileStream(preferenceFilePath, FileMode.Create,FileAccess.Write);
			byte[] strBytes = (new UTF8Encoding(true)).GetBytes(string.Format("{0:F2} {1:D}", titleBarThreshold, borderThreshold));
			await fileStream.WriteAsync(strBytes, 0, strBytes.Length);
			fileStream.Flush();
		}

		private async void MainLoop()
		{
			await Task.Run(() =>
			{
				windowSizeChanger.Run();
			});
		}

		private void StopLoop()
		{
			windowSizeChanger.Stop();
		}
		protected override void OnExit(ExitEventArgs e)
		{
			StopLoop();
			taskbarIcon.Dispose();
			base.OnExit(e);
		}

		private void Preference_Click(object sender, RoutedEventArgs e)
		{
			Current.MainWindow.Show();
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Current.Shutdown();
		}
	}
}

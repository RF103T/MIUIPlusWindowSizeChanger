using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIUIPlusWindowSizeChanger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.Hide();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.ShowInTaskbar = false;
			e.Cancel = true;
			this.Hide();
		}

		private void Window_Activated(object sender, EventArgs e)
		{
			this.ShowInTaskbar = true;
		}

		private void titleBarThresholdSlider_ChildChanged(object sender, EventArgs e)
		{
			Windows.UI.Xaml.Controls.Slider slider = (sender as WindowsXamlHost).Child as Windows.UI.Xaml.Controls.Slider;
			if (slider != null)
			{
				slider.Minimum = 0.1;
				slider.StepFrequency = 0.01;
				slider.Maximum = 0.6;
				slider.ValueChanged += (sender, e) =>
				{
					titleBarThresholdLabel.Content = string.Format("{0:F2}", e.NewValue);
					App.Instance.TitleBarThreshold = (float)e.NewValue;
				};
				slider.Value = App.Instance.TitleBarThreshold;
			}
		}

		private void borderThresholdSlider_ChildChanged(object sender, EventArgs e)
		{
			Windows.UI.Xaml.Controls.Slider slider = (sender as WindowsXamlHost).Child as Windows.UI.Xaml.Controls.Slider;
			if (slider != null)
			{
				slider.Minimum = 1;
				slider.StepFrequency = 1;
				slider.Maximum = 20;
				slider.ValueChanged += (sender, e) =>
				{
					borderThresholdLabel.Content = string.Format("{0:D}", (int)e.NewValue);
					App.Instance.BorderThreshold = Convert.ToInt32(e.NewValue);
				};
				slider.Value = App.Instance.BorderThreshold;
			}
		}

		private void exitButon_ChildChanged(object sender, EventArgs e)
		{
			Windows.UI.Xaml.Controls.Button button = (sender as WindowsXamlHost).Child as Windows.UI.Xaml.Controls.Button;
			if (button != null)
			{
				button.Content = "退出";
				button.CornerRadius = new Windows.UI.Xaml.CornerRadius(3);
				button.Click += (sender, e) =>
				{
					App.Current.Shutdown();
				};
			}
		}

		private void applyButon_ChildChanged(object sender, EventArgs e)
		{
			Windows.UI.Xaml.Controls.Button button = (sender as WindowsXamlHost).Child as Windows.UI.Xaml.Controls.Button;
			if (button != null)
			{
				button.Content = "应用";
				button.CornerRadius = new Windows.UI.Xaml.CornerRadius(3);
				button.Click += (sender, e) =>
				{
					App.Instance.WritePreference(Convert.ToSingle(titleBarThresholdLabel.Content), Convert.ToInt32(borderThresholdLabel.Content));
				};
			}
		}

		private void githubCoreLink_ChildChanged(object sender, EventArgs e)
		{
			Windows.UI.Xaml.Controls.HyperlinkButton button = (sender as WindowsXamlHost).Child as Windows.UI.Xaml.Controls.HyperlinkButton;
			if (button != null)
			{
				button.Content = "Core";
				button.NavigateUri = new Uri("https://github.com/RF103T/WindowSizeChangerCore");
			}
		}

		private void githubGUILink_ChildChanged(object sender, EventArgs e)
		{
			Windows.UI.Xaml.Controls.HyperlinkButton button = (sender as WindowsXamlHost).Child as Windows.UI.Xaml.Controls.HyperlinkButton;
			if (button != null)
			{
				button.Content = "GUI";
				button.NavigateUri = new Uri("https://github.com/RF103T/MIUIPlusWindowSizeChanger");
			}
		}
	}
}

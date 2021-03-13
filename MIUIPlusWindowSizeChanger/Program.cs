public class Program
{
	[System.STAThreadAttribute()]
	public static void Main()
	{
		using (new MIUIPlusWindowSizeChanger_UWP.App())
		{
			MIUIPlusWindowSizeChanger.App app = new MIUIPlusWindowSizeChanger.App();
			app.InitializeComponent();
			app.Run();
		}
	}
}

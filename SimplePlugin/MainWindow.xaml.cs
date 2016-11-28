using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using PluginContracts;
using SimplePlugin.Loader;

namespace SimplePlugin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Dictionary<string, IPlugin> _Plugins;
        LoaderHelper loaderHelper;


        public MainWindow()
		{
			InitializeComponent();

            _Plugins = new Dictionary<string, IPlugin>();
			//ICollection<IPlugin> plugins = PluginLoader.LoadPlugins("Plugins");
			ICollection<IPlugin> plugins = GenericPluginLoader<IPlugin>.LoadPlugins("Plugins");
			foreach(var item in plugins)
			{
				_Plugins.Add(item.Name, item);

				//Button b = new Button();
				//b.Content = item.Name;
				//b.Click += b_Click;
                pluginsListBox.Items.Add(new ListBoxItem() { Content = item.Name});
				//PluginGrid.Children.Add(b);
			}
            loaderHelper = new LoaderHelper(new List<IPlugin>(_Plugins.Values), new WPFLog(contentTextBox, listView.Items));
            loaderHelper.FilesPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), Properties.Settings.Default.FilesPath);
            loaderHelper.Start();
        }

        private void b_Click(object sender, RoutedEventArgs e)
		{
			Button b = sender as Button;
			if(b != null)
			{
				string key = b.Content.ToString();
				if(_Plugins.ContainsKey(key))
				{
					IPlugin plugin = _Plugins[key];
					//plugin.Do();
				}
			}
		}
	}
}

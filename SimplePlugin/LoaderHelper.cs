using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginContracts;
using SimplePlugin.Log;
using System.IO;

namespace SimplePlugin.Loader
{
    public class LoaderHelper
    {
        private List<IPlugin> _loaderPlugins;
        private Timer _timer;
        private ILog _log;
        private List<string> lockedFiles;
        public string FilesPath { get; set; }

        public LoaderHelper(List<IPlugin> loaderPlugins, ILog log)
        {
            _loaderPlugins = loaderPlugins;
            _log = log;
            lockedFiles = new List<string>();
        }
        private void ScanPath(object State)
        {
            DirectoryInfo di = new DirectoryInfo(FilesPath);
            string[] extensions = GetPluginExtensions();
            FileInfo[] Files = di.EnumerateFiles()
                .Where(f => extensions.Contains(f.Extension.ToLower()))
                .ToArray();
            if (Files != null && Files.Length == 0)
            {
                _log.AddLine("Files don't exist");
            }
            else
            {
                foreach (var fi in Files)
                {
                    if (lockedFiles.IndexOf(fi.FullName) == -1)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                        {
                            lockedFiles.Add(fi.FullName);
                            IPlugin plugin = ChoosePluginByExtension(Path.GetExtension(fi.FullName));
                            if (plugin != null)
                            {
                                IList<TradeFlow> items = plugin.ExctractData(fi.FullName);
                                _log.AddItems(items);
                            }

                            _log.AddLine(fi.FullName);
                            File.Delete(fi.FullName);
                            lockedFiles.Remove(fi.FullName);
                        }));
                    }
                }
            }
        }

        public string[] GetPluginExtensions()
        {
            return _loaderPlugins.Select(x => x.Extension).ToArray();
        }

        public IPlugin ChoosePluginByExtension(string ext)
        {
            IPlugin res_p = null;
            foreach (var p in _loaderPlugins)
            {
                if (p.Extension == ext)
                {
                    res_p = p;
                    return p;
                }
            }
            return res_p;
        }
        public void Start()
        {
            TimerCallback timeCB = new TimerCallback(ScanPath);
            _timer = new Timer(timeCB, null, 0, 1000);
        }
        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}

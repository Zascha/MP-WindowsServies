using System;
using System.Collections.Generic;
using System.IO;
using MP.WindowsServices.Common.FileSystemHelpers;
using MP.WindowsServices.FileStorageObserver.Helpers;
using MP.WindowsServices.FileStorageObserver.Interfaces;

namespace MP.WindowsServices.FileStorageObserver
{
    public class LocalFileSystemObserver : IFileStorageObserver
    {
        private readonly IFileSystemHelper _fileSystemHelper;
        private readonly AppConfigHelper _appConfigHelper;

        private List<FileSystemWatcher> _fileSystemWatchers;

        public event EventHandler<FileSystemEventArgs> FileAdded;

        public LocalFileSystemObserver()
        {
            _fileSystemHelper = new LocalFileSystemHelper();
            _appConfigHelper = new AppConfigHelper();

            InitFileSystemWatchersDictionary();
        }

        private void OnFileAdded(object sender, FileSystemEventArgs e)
        {
            if (_appConfigHelper.FileNameRegex.IsMatch(e.Name))
            {
                FileAdded?.Invoke(this, e);
            }
        }

        #region Private methods

        /// <summary>
        /// Initializes FileSystemWatchers with paths enumerated in App.config file, observableFolders section
        /// </summary>
        private void InitFileSystemWatchersDictionary()
        {
            foreach (var path in _appConfigHelper.ObservableFolders)
            {
                if (!_fileSystemHelper.IsValidDirectoryPath(path))
                    throw new ArgumentException($"Invalid directory path: {path}");

                var watcher = new FileSystemWatcher(path);
                watcher.EnableRaisingEvents = true;
                watcher.Changed += new FileSystemEventHandler(OnFileAdded);

                _fileSystemWatchers.Add(watcher);
            }
        }

        #endregion
    }
}

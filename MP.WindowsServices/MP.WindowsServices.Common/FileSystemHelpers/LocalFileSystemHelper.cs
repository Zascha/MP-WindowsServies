using System;
using System.IO;

namespace MP.WindowsServices.Common.FileSystemHelpers
{
    public class LocalFileSystemHelper : IFileSystemHelper
    {
        #region Directories methods

        public bool IsValidDirectoryPath(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                return false;
            }

            try
            {
                Path.GetFullPath(directoryPath);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool DoesDirectoryExist(string directoryPath)
        {
            return !string.IsNullOrEmpty(directoryPath) && Directory.Exists(directoryPath);
        }

        public void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath));

            if (!DoesDirectoryExist(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        #endregion

        #region File methods

        public bool IsValidFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            return IsValidDirectoryPath(filePath) && string.IsNullOrEmpty(GetFileExtention(filePath));
        }

        public string GetFileExtention(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            return Path.GetExtension(filePath);
        }

        #endregion
    }
}

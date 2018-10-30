﻿using MP.WindowsServices.Common.FileSystemHelpers.Interfaces;
using System;
using System.IO;

namespace MP.WindowsServices.Common.FileSystemHelpers
{
    internal class LocalFileHelper : IFileHelper
    {
        public bool IsValidFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(GetFileExtention(filePath)))
            {
                return false;
            }

            try
            {
                Path.GetFullPath(filePath);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public string GetFileExtention(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            return Path.GetExtension(filePath);
        }

        public string GetFileDirectory(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            return Path.GetDirectoryName(filePath);
        }

        public string GetFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            return Path.GetFileName(filePath);
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}

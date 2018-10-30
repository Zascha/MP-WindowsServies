﻿namespace MP.WindowsServices.Common.FileSystemHelpers.Interfaces
{
    public interface IFileHelper
    {
        bool IsValidFilePath(string filePath);

        string GetFileName(string filePath);

        string GetFileExtention(string filePath);

        string GetFileDirectory(string filePath);

        void DeleteFile(string filePath);
    }
}

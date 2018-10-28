namespace MP.WindowsServices.Common.FileSystemHelpers
{
    public interface IFileSystemHelper
    {
        bool IsValidDirectoryPath(string directoryPath);

        bool DoesDirectoryExist(string directoryPath);

        void CreateDirectoryIfNotExists(string directoryPath);

        bool IsValidFilePath(string filePath);

        string GetFileName(string filePath);

        string GetFileExtention(string filePath);

        string GetFileDirectory(string filePath);

        void DeleteFile(string filePath);
    }
}

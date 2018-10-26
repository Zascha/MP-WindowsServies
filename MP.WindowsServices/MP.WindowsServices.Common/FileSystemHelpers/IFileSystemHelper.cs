namespace MP.WindowsServices.Common.FileSystemHelpers
{
    public interface IFileSystemHelper
    {
        bool IsValidDirectoryPath(string directoryPath);

        bool DoesDirectoryExist(string directoryPath);

        void CreateDirectoryIfNotExists(string directoryPath);

        bool IsValidFilePath(string filePath);

        string GetFileExtention(string filePath);
    }
}

using System.Threading.Tasks;

namespace MP.WindowsServices.Common.FileSystemHelpers.Interfaces
{
    public interface IFileAccessMonitor
    {
        Task EnsureFileIsReadyForAccess(string fileName);
    }
}

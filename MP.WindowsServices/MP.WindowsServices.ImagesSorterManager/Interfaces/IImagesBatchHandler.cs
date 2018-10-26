namespace MP.WindowsServices.ImagesManager.Interfaces
{
    public interface IImagesBatchHandler
    {
        void ProcceddImagesBatch(object sender, NewBatchEventArgs e);
    }
}

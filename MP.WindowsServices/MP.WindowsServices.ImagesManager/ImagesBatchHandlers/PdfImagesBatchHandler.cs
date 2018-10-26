using MP.WindowsServices.ImagesManager.Interfaces;
using System;

namespace MP.WindowsServices.ImagesManager.ImagesBatchHandlers
{
    public class PdfImagesBatchHandler : IImagesBatchHandler
    {
        private readonly IImagesBatchProvider _imagesBatchProvider;

        public PdfImagesBatchHandler(IImagesBatchProvider imagesBatchProvider)
        {
            _imagesBatchProvider = imagesBatchProvider ?? throw new ArgumentNullException(nameof(imagesBatchProvider));
            //_imagesBatchProvider.NewBatchIsFormed += 
        }

        public void ProcceddImagesBatch(object sender, NewBatchEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

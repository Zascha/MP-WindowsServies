using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MP.WindowsServices.Common;
using MP.WindowsServices.Common.FileSystemHelpers;
using MP.WindowsServices.Common.Interfaces;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace MP.WindowsServices.ImagesManager.ImagesBatchHandlers
{
    public class PdfImagesBatchHandler : IWorkflowStepExecutor
    {
        private const string MigraDocFilenamePrefix = "base64:";
        private const string PdfDocumentsDirectoryName = "PdfDocuments";
        private const string PdfExtention = ".pdf";

        private readonly Regex _onlyLettersRegex;
        private readonly Document _pdfDocument;
        private readonly PdfDocumentRenderer _pdfRenderer;
        private readonly IFileSystemHelper _fileSystemHelper;

        public PdfImagesBatchHandler(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));

            _pdfDocument = new Document();
            _pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            _onlyLettersRegex = new Regex("[a-zA-Zа-яА-ЯёЁ]+", RegexOptions.Compiled);
        }

        public event EventHandler<FileStoragePipelineEventArgs> StepExecuted;

        private void OnStepExecuted(object sender, FileStoragePipelineEventArgs e)
        {
            StepExecuted?.Invoke(this, e);
        }

        public void HandlePreviousStepResult(object sender, FileStoragePipelineEventArgs args)
        {
            if (args.BatchFilePaths == null)
                throw new ArgumentNullException(nameof(args.BatchFilePaths));

            if (!args.BatchFilePaths.Any())
                throw new ArgumentException(nameof(args.BatchFilePaths), "The pathed batch contains no files.");

            foreach (var file in args.BatchFilePaths)
            {
                AddImageToDocument(file);
            }

            var outputFilePath = GetOutputPath(args.BatchFilePaths.First());
            SavePdf(outputFilePath);

            OnStepExecuted(this, args);
        }

        #region Private methods

        private void AddImageToDocument(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            var section = _pdfDocument.AddSection();
            section.AddImage(filePath);
        }

        private void SavePdf(string outputPath)
        {
            _pdfRenderer.Document = _pdfDocument;
            _pdfRenderer.RenderDocument();
            _pdfRenderer.PdfDocument.Save(outputPath);
        }

        private string GetMigraDocImageFileName(string filePath)
        {
            return $"{MigraDocFilenamePrefix}{File.ReadAllBytes(filePath).ToString()}";
        }

        private string GetOutputPath(string filePath)
        {
            var fileDirectory = _fileSystemHelper.GetFileDirectory(filePath);
            var fileName = _onlyLettersRegex.Match(_fileSystemHelper.GetFileName(filePath)).Value.Trim();

            var pdfDirectoryName = Path.Combine(fileDirectory, PdfDocumentsDirectoryName);
            var pdfDocName = $"{fileName}_{DateTime.UtcNow.ToBinary()}{PdfExtention}";

            _fileSystemHelper.CreateDirectoryIfNotExists(pdfDirectoryName);

            return Path.Combine(pdfDirectoryName, pdfDocName);
        }

        #endregion
    }
}

using MP.WindowsServices.Common.ConfigurationHelper;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MP.WindowsServices.FileStorageObserver.Helpers
{
    public class AppConfigHelper
    {
        private const char Delimeter = ',';

        private readonly AppConfiguration _appConfiguration = new AppConfiguration();

        private IEnumerable<string> _fileExtentionFilters;
        private Regex _fileNameTemplate;

        public IEnumerable<string> FileExtentionFilters
        {
            get
            {
                if (_fileExtentionFilters == null)
                {
                    _fileExtentionFilters = _appConfiguration.FileFilters
                                                             .Split(Delimeter)
                                                             .Select(item => item.Trim());
                }

                return _fileExtentionFilters;
            }
        }

        public Regex FileNameRegex
        {
            get
            {
                if (_fileNameTemplate == null)
                {
                    _fileNameTemplate = new Regex(_appConfiguration.FileNameTemplate, RegexOptions.Compiled);
                }

                return _fileNameTemplate;
            }
        }

        public IEnumerable<string> ObservableFolders
        {
            get
            {
                return _appConfiguration.ObservableFolders.Select(item => item.Path);
            }
        }
    }
}

using System;
using System.Linq;
using AdventOfCode.Business.Helpers;
using AdventOfCode.Business.Managers.Models;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Business.Managers
{
    public class AdventOfCodeManager : IAdventOfCodeManager
    {
        private const string InputFilePath = "../../adventofcode/AdventOfCode.Concerns/CalibrationInput.txt";

        private readonly ILogger<AdventOfCodeManager> _logger;
        private readonly IFileManager _fileManager;

        public AdventOfCodeManager(
            ILogger<AdventOfCodeManager> logger,
            IFileManager fileManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public AdventOfCodeResultModel HandleCalibrationCount()
        {
            // Setup the total count
            var fileTotalCount = 0;

            // Setup the result model
            var resultModel = new AdventOfCodeResultModel();

            // Get the input file
            using var streamReader = _fileManager.StreamReader(InputFilePath);
            while (!streamReader.EndOfStream)
            {
                // Read the line
                var line = streamReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    _logger.LogInformation("Current line is empty");

                    // Exception for unit testing purposes - would normally only log this
                    resultModel.ExceptionCode = "AdventOfCodeManager_HandleCalibrationCount_EmptyLine";
                    return resultModel;
                }

                // Get the numbers from the line
                var lineNumbers = line.Where(char.IsDigit).ToArray();
                if (!lineNumbers.Any())
                {
                    _logger.LogInformation("Current line doesn't contain numbers");

                    continue;
                }

                // Calculate the total count for the line
                var charNumbersAsString = lineNumbers.First().ToString() + lineNumbers.Last().ToString();
                var lineTotalCount = int.Parse(charNumbersAsString);

                _logger.LogInformation("Current line '{0}' total count: '{1}'", line, lineTotalCount);

                // Update the file total count
                fileTotalCount += lineTotalCount;
            }

            // Return
            resultModel.Calibration = fileTotalCount;
            return resultModel;
        }
    }
}

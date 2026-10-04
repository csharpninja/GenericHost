using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericHostInConsoleApp
{
    public class PrintRandomNumber : IPrintRandomNumber
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PrintRandomNumber> _logger;

        public PrintRandomNumber(IConfiguration configuration,ILogger<PrintRandomNumber> logger)
        {
            _configuration = configuration;
            _logger = logger;
                
        }
        public void PrintNumber()
        {
            var upperLimitNumber = _configuration.GetValue<int>("UpperLimitNumber");
            var randomNumber = Random.Shared.Next(0, upperLimitNumber +1);
            
            _logger.LogInformation("PrintRandomNumber : {randomNumber}", randomNumber);
            
        }

        
    }
}

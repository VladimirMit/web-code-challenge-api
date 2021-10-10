using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognizant.CodeChallenge.Api.Models
{
    public class CreateSolutionDto
    {
        public int TaskId { get; set; }
        public string Code { get; set; }
        public string LanguageName { get; set; }
    }
}

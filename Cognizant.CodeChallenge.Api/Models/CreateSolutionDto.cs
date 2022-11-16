namespace Application.CodeChallenge.Api.Models
{
    public class CreateSolutionDto
    {
        public int TaskId { get; set; }
        public string Code { get; set; }
        public string LanguageName { get; set; }
    }
}

namespace Cognizant.CodeChallenge.Domain.ValueObjects
{
    public class CodeTaskTestCase
    {
        private CodeTaskTestCase()
        {
        }
        
        public CodeTaskTestCase(string inptuValue, string outputValue) 
        {
            InputValue = inptuValue;
            OutputValue = outputValue;
        }

        public string InputValue { get; }

        public string OutputValue { get; }
    }
}

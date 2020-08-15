namespace Pygma.Common.Models.ErrorHandling
{
    public class ValidationError
    {
        public ValidationError(string field, string errorMessage)
        {
            this.Field = field;
            this.Message = errorMessage;
        }

        public string Field { get; }

        public string Message { get; }
    }
}

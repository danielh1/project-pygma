namespace TrFoil.Backbone.Common.Models.ErrorHandling
{
    public class ValidationError
    {
        private string _key;
        private string _errorMessage;

        public ValidationError(string key, string errorMessage)
        {
            this._key = key;
            this._errorMessage = errorMessage;
        }

        public string Field { get; set; }
        public string Message { get; set; }
    }
}

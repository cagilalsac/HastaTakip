using DataAccess.Results.Bases;

namespace DataAccess.Results
{
    /*
    Result result = new SuccessResult("İşlem başarılı.");
    Result result = new SuccessResult();
    */
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
        }

        public SuccessResult() : base(true, "")
        {
        }
    }
}

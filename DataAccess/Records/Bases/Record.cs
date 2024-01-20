namespace DataAccess.Records.Bases
{
    public abstract class Record
    {
        public int Id { get; set; }  // primary key
        public string? Guid { get; set; }
    }
}

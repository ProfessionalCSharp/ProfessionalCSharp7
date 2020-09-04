namespace Records
{
    public record MutableRecord(string Title)
    {
        public string? Value { get; set; }

        public override int GetHashCode() => Title.GetHashCode();
    }
}

namespace DataBindingSamples.Models
{
    public class Book : BindableBase
    {
        public int BookId { get; set; }
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Publisher { get; set; }

        public override string ToString() => Title;
    }
}

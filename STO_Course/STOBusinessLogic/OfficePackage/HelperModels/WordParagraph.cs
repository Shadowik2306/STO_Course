namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class WordParagraph
    {
        public List<(string, WordTextProperties)> Texts { get; set; } = new();

        public WordTextProperties? TextProperties { get; set; }

        public List<List<(string, WordTextProperties)>> RowTexts { get; set; } = new();
    }
}

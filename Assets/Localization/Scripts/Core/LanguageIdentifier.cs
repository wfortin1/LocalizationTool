namespace Localization
{
    public class LanguageIdentifier
    {
        public string DisplayName;
        public string UniqueIdentifier; 
        
        public LanguageIdentifier(string displayName, string uniqueIdentifier)
        {
            DisplayName = displayName;
            UniqueIdentifier = uniqueIdentifier;
        }
    }
}

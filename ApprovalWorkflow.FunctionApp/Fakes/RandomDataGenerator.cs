namespace ApprovalWorkflow.FunctionApp.Fakes
{
    public class RandomDataGenerator
    {
        private static readonly string[] FirstNames = {
            "John", "Jane", "Alex", "Emily", "Chris", "Katie", "Mike", "Laura", "Robert", "Linda"
        };

        private static readonly string[] LastNames = {
            "Smith", "Johnson", "Brown", "Taylor", "Anderson", "Thomas", "Jackson", "White"
        };

        private static readonly string[] Comments = {
            "Great job!", "I love this!", "This is awesome.", "Needs improvement.", "Interesting idea.",
            "Could be better.", "Totally agree with this.", "This made my day!", "Thanks for sharing."
        };

        private static readonly string[] EmailDomains = {
            "example.com", "mail.com", "demo.org", "test.net"
        };

        private static readonly Random _random = new Random();

        public static string GenerateName()
        {
            string first = FirstNames[_random.Next(FirstNames.Length)];
            string last = LastNames[_random.Next(LastNames.Length)];
            return $"{first} {last}";
        }

        public static string GenerateEmail(string name)
        {
            string domain = EmailDomains[_random.Next(EmailDomains.Length)];
            string emailUser = name.ToLower().Replace(" ", ".");
            return $"{emailUser}@{domain}";
        }

        public static string GenerateComment()
        {
            return Comments[_random.Next(Comments.Length)];
        }
    }

}

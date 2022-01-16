using System.Text;

namespace TestSkeleton_Selenium.NET_Screenplay.Drivers
{
    static class RegisterEncodingFirefox
    {
        public static void RegisterEncodingPage437()
        {
            CodePagesEncodingProvider.Instance.GetEncoding(437);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}

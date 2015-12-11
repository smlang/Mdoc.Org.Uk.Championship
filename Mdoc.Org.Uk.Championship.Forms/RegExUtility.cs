using System;
using System.Text;

namespace Mdoc.Org.Uk.Championship.Forms
{
    internal static class RegExUtility
    {
        internal static string ToTextBox(string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            if (xmlString != null)
            {
                foreach (string line in xmlString.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string line2 = line.Trim();
                    if (line2 != String.Empty)
                    {
                        if (sb.Length != 0)
                        {
                            sb.Append("\r\n");
                        }
                        sb.Append(line2);
                    }
                }
            }
            return sb.ToString();
        }

        internal static string FromTextBox(string textBoxString)
        {
            return textBoxString.Replace("\r\n", "\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Mdoc.Org.Uk.Championship.Library
{
    internal static class Web
    {
        internal static List<String> Download(String sourceUrl, IUserInteraction ui)
        {
            WebRequest request = WebRequest.Create(sourceUrl);
            request.Credentials = CredentialCache.DefaultCredentials;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response != null)
                {
                    try
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            ui.ShowError(String.Format("Failed to download {0}, {1}", sourceUrl,
                                                       response.StatusDescription));
                            return null;
                        }

                        using (Stream stream = response.GetResponseStream())
                        {
                            if (stream != null)
                            {
                                try
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        try
                                        {
                                            return GetLinks(reader, sourceUrl, ui);
                                        }
                                        finally
                                        {
                                            reader.Close();
                                        }
                                    }
                                }
                                finally
                                {
                                    stream.Close();
                                }
                            }
                        }
                    }
                    finally
                    {
                        response.Close();
                    }
                }
            }
            return null;
        }

        internal static Boolean Validate(String sourceUrl, out string message)
        {
            Uri uriResult;
            if (!Uri.TryCreate(sourceUrl, UriKind.Absolute, out uriResult))
            {
                message = String.Format("The URL '{0}' is not valid", sourceUrl);
                return false;
            }

            if ((uriResult.Scheme != Uri.UriSchemeHttp) && (uriResult.Scheme != Uri.UriSchemeHttps)) 
            {
                message = String.Format("The URL '{0}' must use http or https", sourceUrl);
                return false;
            } 

            message = String.Empty;
            return true;
        }

        private static List<String> GetLinks(StreamReader reader, String sourceUrl, IUserInteraction ui)
        {
            List<String> links = new List<String>();

            Int32 choice = ui.GetChoice(
                1,
                String.Format("Should '{0}' be downloaded", sourceUrl),
                "Yes",
                "No");

            if (choice == 1)
            {
                links.Add(sourceUrl);
            }

            Regex anchorRegex = new Regex(Settings.Default.AnchorRegex,
                                        RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

            Uri uri = new Uri(sourceUrl);
            String rootPath = uri.AbsolutePath;
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf('/') + 1);

            while (!reader.EndOfStream)
            {
                String line = reader.ReadLine();
                if (line != null)
                {
                    line = line.Trim();

                    foreach (Match m in anchorRegex.Matches(line))
                    {
                        String url = m.Groups[1].Value;
                        String description = m.Groups[2].Value.Trim();

                        int defaultChoice = 2; // No
                        if (description.Equals("Results", StringComparison.InvariantCultureIgnoreCase))
                        {
                            defaultChoice = 1; // Yes
                        }

                        Int32 choice2 = ui.GetChoice(
                            defaultChoice,
                            String.Format("Should '{0}' be downloaded", description),
                            "Yes",
                            "No");

                        if (choice2 == 1)
                        {
                            if (url.StartsWith("http", true, null))
                            {
                                links.Add(url);
                            }
                            else if (url.StartsWith("/"))
                            {
                                UriBuilder b = new UriBuilder(uri);

                                int queryStart = url.IndexOf("?");
                                if (queryStart == -1)
                                {
                                    b.Path = url;
                                    b.Query = String.Empty;
                                }
                                else
                                {
                                    b.Path = url.Substring(0, queryStart);
                                    b.Query = HttpUtility.HtmlDecode(url.Substring(queryStart + 1)) ?? String.Empty;
                                }
                                links.Add(b.ToString());
                            }
                            else if (url.StartsWith("#"))
                            {
                                UriBuilder b = new UriBuilder(uri) { Fragment = url.Substring(1) };
                                links.Add(b.ToString());
                            }
                            else
                            {
                                UriBuilder b = new UriBuilder(uri) { Path = rootPath + url };
                                links.Add(b.ToString());
                            }
                        }
                    }
                }
            }

            return links;
        }

        internal static void DownloadResults(StreamWriter writer, String resultUrl, IUserInteraction ui)
        {
            WebRequest request = WebRequest.Create(resultUrl);
            request.Credentials = CredentialCache.DefaultCredentials;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response != null)
                {
                    try
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            ui.ShowError(String.Format("Failed to download {0}, {1}", resultUrl,
                                                       response.StatusDescription));
                            return;
                        }

                        using (Stream stream = response.GetResponseStream())
                        {
                            if (stream != null)
                            {
                                try
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        try
                                        {
                                            Regex preStartTagRegex = new Regex(@"\<PRE\>", RegexOptions.IgnoreCase);
                                            Regex preEndTagRegex = new Regex(@"\<\/PRE\>", RegexOptions.IgnoreCase);
                                            Boolean inPre = false;

                                            Regex blockEndTagRegex =
                                                new Regex(@"\<((\/(H[1-9]|P|TR|DIV|TITLE|PRE))|BR)[^>]*\>",
                                                          RegexOptions.Multiline | RegexOptions.IgnoreCase);
                                            Regex trEndTagRegex = new Regex(@"\<\/TD[^>]*\>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                                            Regex endTagRegex = new Regex(@"\<\/[^>]*\>", RegexOptions.Multiline);
                                            Regex tagRegex = new Regex(@"\<[^>]*\>", RegexOptions.Multiline);

                                            while (!reader.EndOfStream)
                                            {
                                                String line = reader.ReadLine();
                                                if (line != null)
                                                {
                                                    line = line.Trim();
                                                    if (inPre && preEndTagRegex.IsMatch(line))
                                                    {
                                                        inPre = false;
                                                    }
                                                    else if (preStartTagRegex.IsMatch(line))
                                                    {
                                                        inPre = true;
                                                    }

                                                    if (!inPre)
                                                    {
                                                        line = trEndTagRegex.Replace(line, "|");
                                                        line = blockEndTagRegex.Replace(line, "\r\n");
                                                        line = endTagRegex.Replace(line, " ");
                                                        line = tagRegex.Replace(line, "");
                                                    }
                                                    else
                                                    {
                                                        writer.Write("\r\n");
                                                    }

                                                    writer.Write(line);
                                                }
                                            }
                                            writer.WriteLine();
                                        }
                                        finally
                                        {
                                            reader.Close();
                                        }
                                    }
                                }
                                finally
                                {
                                    stream.Close();
                                }
                            }
                        }
                    }
                    finally
                    {
                        response.Close();
                    }
                }
            }
        }
    }
}
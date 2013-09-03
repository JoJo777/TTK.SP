
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TTK.SP.Core
{
    class Logging
    {
        static string DocumentLibraryName = "Style Library";
        static string LogFileName = "TTK.SP.Core.Log.txt";

        internal static void WriteToLog(SPContext context, Exception exception)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            string errors = exception.Source + " " + exception.Message + " " + exception.StackTrace;

            SPFile files = context.Web.GetFile("/" + DocumentLibraryName + "/" + LogFileName);

            if (files.Exists)
            {
                byte[] fileContents = files.OpenBinary();
                string newContents = enc.GetString(fileContents) + Environment.NewLine + errors;
                files.SaveBinary(enc.GetBytes(newContents));
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(ms, uniEncoding))
                    {
                        sw.Write(errors);
                    }

                    SPFolder LogLibraryFolder = SPContext.Current.Web.Folders[DocumentLibraryName];
                    LogLibraryFolder.Files.Add(LogFileName, ms.ToArray(), false);
                }
            }

            SPContext.Current.Web.Update();
        }

        internal static void WriteToLog(SPContext context, string message)
        {
            context.Web.AllowUnsafeUpdates = true;

            ASCIIEncoding enc = new ASCIIEncoding();
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            SPFile files = context.Web.GetFile("/" + DocumentLibraryName + "/" + LogFileName);

            if (files.Exists)
            {
                byte[] fileContents = files.OpenBinary();
                string newContents = enc.GetString(fileContents) + Environment.NewLine + message;
                files.SaveBinary(enc.GetBytes(newContents));
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(ms, uniEncoding))
                    {
                        sw.Write(message);
                    }

                    SPFolder LogLibraryFolder = SPContext.Current.Web.Folders[DocumentLibraryName];
                    LogLibraryFolder.Files.Add(LogFileName, ms.ToArray(), false);
                }
            }

            files.Update();

            context.Web.AllowUnsafeUpdates = false;
        }
    }
}

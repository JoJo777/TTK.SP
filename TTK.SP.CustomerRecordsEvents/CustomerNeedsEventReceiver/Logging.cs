using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TTK.SP.CustomerRecordsEvents.CustomerNeedsEventReceiver
{
    class Logging
    {
        static string DocumentLibraryName = "Style Library";
        static string LogFileName = "CustomerEventReceiver.Log.txt";

        internal static void WriteToLog(SPWeb web, Exception exception)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            string errors = exception.Source + " " + exception.Message + " " + exception.StackTrace;

            SPFile files = web.GetFile("/" + DocumentLibraryName + "/" + LogFileName);

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

                    SPFolder LogLibraryFolder = web.Folders[DocumentLibraryName];
                    LogLibraryFolder.Files.Add(LogFileName, ms.ToArray(), false);
                }
            }

            web.Update();
        }

        internal static void WriteToLog(SPWeb web, string message)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            string errors = message;

            SPFile files = web.GetFile("/" + DocumentLibraryName + "/" + LogFileName);

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

                    SPFolder LogLibraryFolder = web.Folders[DocumentLibraryName];
                    LogLibraryFolder.Files.Add(LogFileName, ms.ToArray(), false);
                }
            }

            web.Update();
        }
    }
}

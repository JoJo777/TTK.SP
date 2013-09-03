using DocumentFormat.OpenXml.Packaging;
using Microsoft.Office.Word.Server.Conversions;
using Microsoft.SharePoint;
using System;
using System.IO;

namespace TTK.SP.CustomerRecordsEvents.Customers2CustomerRecord
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class Customers2CustomerRecord : SPItemEventReceiver
    {
        static string CustomerRecordsDocumentLibrary = "Customer Records";
        static string CustomerRecordsDocumentFolder = "customerrecord";

        static string ContentType = "CustomerRecord";

        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            CreateCustomerInRecords(properties);
        }

        /// <summary>
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            CreateCustomerInRecords(properties);
        }

        private void CreateCustomerInRecords(SPItemEventProperties properties)
        {
            try
            {
                properties.Web.AllowUnsafeUpdates = true;
                Logging.WriteToLog(properties.Web, "CreateCustomerInRecords started");

                string docxFileNameFullPath = Converter(properties);
                ConvertDotxToDocx(properties.Web, docxFileNameFullPath, docxFileNameFullPath.Replace(".docx", ".pdf"));
            }
            catch (Exception ex)
            {
                Logging.WriteToLog(properties.Web, ex.Message);
            }
            finally
            {
                properties.Web.AllowUnsafeUpdates = false;
            }
        }

        protected void ConvertDotxToDocx(SPWeb web, string inputFile, string outputFile)
        {
            string wordAutomationServiceName = "Word Automation Service";

            var conversionJob = new ConversionJob(wordAutomationServiceName);
            conversionJob.UserToken = web.CurrentUser.UserToken;
            conversionJob.Name = "TTK.Document.Conversion.CustomerRecord." + inputFile;

            conversionJob.Settings.OutputFormat = SaveFormat.PDF;
            conversionJob.UserToken = web.Site.UserToken;

            conversionJob.AddFile(inputFile, outputFile);

            conversionJob.Start();
        }

        public static void OpenAndAddToWordprocessingStream(Stream stream, string txt)
        {
            // Open a WordProcessingDocument based on a stream.
            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(stream, true);
        }

        private string Converter(SPItemEventProperties properties)
        {
            // find the template url and open
            SPList list = properties.Web.Lists[CustomerRecordsDocumentLibrary];
            // this always uses root folder
            SPFolder folder = properties.Web.Folders[CustomerRecordsDocumentFolder];
            SPFileCollection fcol = folder.Files;

            // find the template url and open

            //string sTemplate = "/" + CustomerRecordsDocumentFolder + "/Forms/template.dotx"; //cannot use the one in forms folder???
            string sTemplate = "/_cts/" + ContentType + "/template.dotx";
            SPFile spfDotx = properties.Web.GetFile(sTemplate);
            byte[] binFileDotx = spfDotx.OpenBinary();
            // Url for file to be created

            string destFile = fcol.Folder.Url + "/" + properties.ListItem["Title"] + " " + properties.ListItem["FirstName"] + ".docx";

            MemoryStream documentStream;

            using (Stream tplStream = spfDotx.OpenBinaryStream())
            {
                documentStream = new MemoryStream((int)tplStream.Length);
                CopyStream(tplStream, documentStream);
                documentStream.Position = 0L;
            }

            using (WordprocessingDocument template = WordprocessingDocument.Open(documentStream, true))
            {
                template.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);

                MainDocumentPart mainPart = template.MainDocumentPart;

                mainPart.DocumentSettingsPart.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", new Uri(@"c:\nonssense\xxx.dotx", UriKind.Absolute));

                mainPart.Document.Save();
            }

            SPFile addedFile = fcol.Add(destFile, documentStream, true);
            
            SPItem newItem = addedFile.Item;
            newItem["ContentType"] = ContentType;
            addedFile.Item["ContentType"] = ContentType;
            
            //0x010100728E6ABBF6164BBC9A96D73680ED991B006E7E7B2E66671049AA392BFF57A7E72C still won't work
            //newItem["ContentTypeId"] = "0x010100728E6ABBF6164BBC9A96D73680ED991B";
            //addedFile.Item["ContentTypeId"] = "0x010100728E6ABBF6164BBC9A96D73680ED991B";

            //update content type
            newItem.Update();
            addedFile.Item.SystemUpdate();

            addedFile.Item["Accountant"] = properties.ListItem["Accountant"];
            addedFile.Item["Business"] = properties.ListItem["Business"];
            addedFile.Item["Correspondance Preference"] = properties.ListItem["CorrespondancePreference"];
            addedFile.Item["Date Captured"] = properties.ListItem["DateCaptured"];

            newItem["Accountant"] = properties.ListItem["Accountant"];
            newItem["Business"] = properties.ListItem["Business"];
            newItem["Correspondance Preference"] = properties.ListItem["CorrespondancePreference"];
            newItem["Date Captured"] = properties.ListItem["DateCaptured"];

            newItem["DOB"] = properties.ListItem["DoB"];
            newItem["Email"] = properties.ListItem["Email"];
            newItem["Gender"] = properties.ListItem["Gender"];
            newItem["Home"] = properties.ListItem["Home"];
            newItem["Mobile"] = properties.ListItem["Mobile"];
            newItem["Marital"] = properties.ListItem["Marital"];
            newItem["Qantas"] = properties.ListItem["Qantas"];
            newItem["RACV"] = properties.ListItem["RACV"];
            newItem["Referrer"] = properties.ListItem["Referrer"];
            newItem["Residential"] = properties.ListItem["Residential"];
            newItem["Solicitor"] = properties.ListItem["Solicitor"];
            newItem["Will"] = properties.ListItem["Will"];
            newItem["First Name"] = properties.ListItem["FirstName"];
            newItem["Accountant P"] = properties.ListItem["AccountantP"];
            newItem["Business P"] = properties.ListItem["BusinessP"];
            newItem["Correspondance Preference P"] = properties.ListItem["CorrespondancePreferenceP"];
            newItem["Date of Birth P"] = properties.ListItem["DoBP"];
            newItem["Email P"] = properties.ListItem["EmailP"];
            newItem["First Name P"] = properties.ListItem["FirstNameP"];
            newItem["Gender P"] = properties.ListItem["GenderP"];
            newItem["Home P"] = properties.ListItem["HomeP"];
            newItem["Marital P"] = properties.ListItem["MaritalP"];
            newItem["Mobile P"] = properties.ListItem["MobileP"];
            newItem["Qantas P"] = properties.ListItem["QantasP"];
            newItem["RACVP"] = properties.ListItem["RACVP"];
            newItem["Smoker P"] = properties.ListItem["SmokerP"];
            newItem["Smoker"] = properties.ListItem["Smoker"];
            newItem["Solicitor P"] = properties.ListItem["SolicitorP"];
            newItem["Sur Name P"] = properties.ListItem["SurNameP"];
            newItem["Will P"] = properties.ListItem["WillP"];
            newItem["Residential P"] = properties.ListItem["ResidentialP"];
            //newItem["Father"] = properties.ListItem["Father"];
            //newItem["Father DOB"] = properties.ListItem["FatherDOB"];
            //newItem["Father Health"] = properties.ListItem["FatherHealth"];
            //newItem["Mother"] = properties.ListItem["Mother"];
            //newItem["Mother DOB"] = properties.ListItem["MotherDOB"];
            //newItem["Mother Health"] = properties.ListItem["MotherHealth"];
            //newItem["Brother"] = properties.ListItem["Brother"];
            //newItem["Brother DOB"] = properties.ListItem["BrotherDOB"];
            //newItem["Brother Health"] = properties.ListItem["BrotherHealth"];
            //newItem["Brother 2"] = properties.ListItem["Brother2"];
            //newItem["Brother 2 DOB"] = properties.ListItem["Brother2DOB"];
            //newItem["Brother Health 2"] = properties.ListItem["BrotherHealth2"];
            //newItem["Sister"] = properties.ListItem["Sister"];
            //newItem["Sister DOB"] = properties.ListItem["SisterDOB"];
            //newItem["Sister Health"] = properties.ListItem["SisterHealth"];
            //newItem["Sister 2"] = properties.ListItem["Sister2"];
            //newItem["Sister DOB 2"] = properties.ListItem["SisterDOB2"];
            //newItem["Sister Health 2"] = properties.ListItem["SisterHealth2"];
            //newItem["Father P"] = properties.ListItem["FatherP"];
            //newItem["Father PDOB"] = properties.ListItem["FatherPDOB"];
            //newItem["Father Health 1"] = properties.ListItem["FatherHealth1"];
            //newItem["Mother P"] = properties.ListItem["MotherP"];
            //newItem["Mother PDOB"] = properties.ListItem["MotherPDOB"];
            //newItem["Mother Health P"] = properties.ListItem["MotherHealthP"];
            //newItem["Brother P"] = properties.ListItem["BrotherP"];
            //newItem["Brother PDOB"] = properties.ListItem["BrotherPDOB"];
            //newItem["Brother Health P"] = properties.ListItem["BrotherHealthP"];
            //newItem["Brother 2 P"] = properties.ListItem["Brother2P"];
            //newItem["Brother 2 PDOB"] = properties.ListItem["Brother2PDOB"];
            //newItem["Brother Health 2 P"] = properties.ListItem["BrotherHealth2P"];
            //newItem["Sister P"] = properties.ListItem["SisterP"];
            //newItem["Sister PDOB"] = properties.ListItem["SisterPDOB"];
            //newItem["Sister P Health"] = properties.ListItem["SisterHealthP"];
            //newItem["Sister 2 P"] = properties.ListItem["Sister2P"];
            //newItem["Sister 2 PDOB"] = properties.ListItem["Sister2PDOB"];
            //newItem["Sister Health 2 P"] = properties.ListItem["SisterHealth2P"];
            //newItem["Dependants First 1"] = properties.ListItem["DependantsFirst1"];
            //newItem["Dependants Education Occupation Level 1"] = properties.ListItem["DependantsEducationOccupationLevel1"];
            //newItem["Dependants School University 1"] = properties.ListItem["DependantsSchoolUniversity1"];
            //newItem["Dependants Surname 1"] = properties.ListItem["DependantsSurName1"];
            //newItem["Dependants"] = properties.ListItem["Dependants1"];
            //newItem["Relation 1"] = properties.ListItem["Relation1"];
            //newItem["Dependants Health 1"] = properties.ListItem["DependantsHealth1"];
            //newItem["Dependants First 2"] = properties.ListItem["DependantsFirst2"];
            //newItem["Dependants Surname 2"] = properties.ListItem["DependantsSurName2"];
            //newItem["Dependants 2"] = properties.ListItem["Dependants2"];
            //newItem["Relation 2"] = properties.ListItem["DependantsSurName1ddRelation2"]; //??
            //newItem["Dependants Education Occupation Level 2"] = properties.ListItem["DependantsEducationOccupationLevel2"];
            //newItem["Dependants School University 2"] = properties.ListItem["DependantsSchoolUniversity2"];
            //newItem["Dependants Health 2"] = properties.ListItem["DependantsHealth2"];
            //newItem["Dependants First 3"] = properties.ListItem["DependantsFirst3"];
            //newItem["Dependants Surname 3"] = properties.ListItem["DependantsSurName3"];
            //newItem["Dependants 3"] = properties.ListItem["Dependants3"];
            //newItem["Relation 3"] = properties.ListItem["Relation3"];
            //newItem["Dependants Education Occupation Level 3"] = properties.ListItem["DependantsEducationOccupationLevel3"];
            //newItem["Dependants School University 3"] = properties.ListItem["DependantsSchoolUniversity3"];
            //newItem["Dependants Health 3"] = properties.ListItem["DependantsHealth3"];
            //newItem["Dependants First 4"] = properties.ListItem["DependantsFirst4"];
            //newItem["Dependants Surname 4"] = properties.ListItem["DependantsSurName4"];
            //newItem["Dependants 4"] = properties.ListItem["Dependants4"];
            //newItem["Relation 4"] = properties.ListItem["Relation4"];
            //newItem["Dependants Education Occupation Level 4"] = properties.ListItem["DependantsEducationOccupationLevel4"];
            //newItem["Dependants School University 4"] = properties.ListItem["DependantsSchoolUniversity4"];
            //newItem["Dependants Health 4"] = properties.ListItem["DependantsHealth4"];
            //newItem["Dependants First 5"] = properties.ListItem["DependantsFirst5"];
            //newItem["Dependants Surname 5"] = properties.ListItem["DependantsSurName5"];
            //newItem["Dependants 5"] = properties.ListItem["Dependants5"];
            //newItem["Relation 5"] = properties.ListItem["Relation5"];
            //newItem["Dependants Education Occupation Level 5"] = properties.ListItem["DependantsEducationOccupationLevel5"];
            //newItem["Dependants School University 5"] = properties.ListItem["DependantsSchoolUniversity5"];
            //newItem["Dependants Health 5"] = properties.ListItem["DependantsHealth5"];
            //newItem["Job Title 1"] = properties.ListItem["JobTitle1"];
            //newItem["Employment Status"] = properties.ListItem["EmploymentStatus"];
            //newItem["Employer"] = properties.ListItem["Employer"];
            //newItem["Hours Per Week"] = properties.ListItem["HoursPerWeek"];
            //newItem["Qualifications"] = properties.ListItem["Qualifications"];
            //newItem["Remuneration"] = properties.ListItem["Remuneration"];
            //newItem["Duties"] = properties.ListItem["Duties"];
            //newItem["Job Title P"] = properties.ListItem["JobTitleP"];
            //newItem["Employment Status P"] = properties.ListItem["EmploymentStatusP"];
            //newItem["Employer P"] = properties.ListItem["EmployerP"];
            //newItem["Hours Per Week P"] = properties.ListItem["HoursPerWeekP"];
            //newItem["Qualifications P"] = properties.ListItem["QualificationsP"];
            //newItem["Remuneration P"] = properties.ListItem["RemunerationP"];
            //newItem["Duties P"] = properties.ListItem["DutiesP"];
            //newItem["Health Condition"] = properties.ListItem["HealthCondition"];
            //newItem["Health Condition P"] = properties.ListItem["HealthConditionP"];
            //newItem["House"] = properties.ListItem["House"];
            //newItem["Contents"] = properties.ListItem["Contents"];
            //newItem["Super"] = properties.ListItem["Supe"];
            //newItem["Cash"] = properties.ListItem["Cash"];
            //newItem["Shares"] = properties.ListItem["Shares"];
            //newItem["Investment Properties"] = properties.ListItem["InvestmentProperties"];
            //newItem["Business Value"] = properties.ListItem["BusinessValue"];
            //newItem["Potential Inheritance"] = properties.ListItem["PotentialInheritance"];
            //newItem["Mortgage"] = properties.ListItem["Mortgage"];
            //newItem["Personal Loans"] = properties.ListItem["PersonalLoans"];
            //newItem["Credit Card Debt"] = properties.ListItem["CreditCardDebt"];
            //newItem["Investment Loans"] = properties.ListItem["InvestmentLoans"];
            //newItem["Leases"] = properties.ListItem["Leases"];
            //newItem["Business Debt"] = properties.ListItem["BusinessDebt"];
            //newItem["Liabilities"] = properties.ListItem["Liabilites"];
            //newItem["House P"] = properties.ListItem["HouseP"];
            //newItem["Contents P"] = properties.ListItem["ContentsP"];
            //newItem["Super P"] = properties.ListItem["SuperP"];
            //newItem["Cash P"] = properties.ListItem["CashP"];
            //newItem["Shares P"] = properties.ListItem["SharesP"];
            //newItem["Investment Properties P"] = properties.ListItem["InvestmentPropertiesP"];
            //newItem["Business Value P"] = properties.ListItem["BusinessValueP"];
            //newItem["Potential Inheritance P"] = properties.ListItem["PotentialInheritanceP"];
            //newItem["Mortgage P"] = properties.ListItem["MortgageP"];
            //newItem["Personal Loans P"] = properties.ListItem["PersonalLoansP"];
            //newItem["Credit Card Debt P"] = properties.ListItem["CreditCardDebtP"];
            //newItem["Investment Loans P"] = properties.ListItem["InvestmentLoansP"];
            //newItem["Leases P"] = properties.ListItem["LeasesP"];
            //newItem["Business Debt P"] = properties.ListItem["BusinessDebtP"];
            //newItem["Liabilities P"] = properties.ListItem["LiabilitesP"];
            //newItem["House J"] = properties.ListItem["HouseJ"];
            //newItem["Contents J"] = properties.ListItem["ContentsJ"];
            //newItem["Super J"] = properties.ListItem["SuperJ"];
            //newItem["Cash J"] = properties.ListItem["CashJ"];
            //newItem["Shares J"] = properties.ListItem["SharesJ"];
            //newItem["Investment Properties J"] = properties.ListItem["InvestmentPropertiesJ"];
            //newItem["Business Value J"] = properties.ListItem["BusinessValueJ"];
            //newItem["Potential Inheritance J"] = properties.ListItem["PotentialInheritanceJ"];
            //newItem["Mortgage J"] = properties.ListItem["MortgageJ"];
            //newItem["Personal Loans J"] = properties.ListItem["PersonalLoansJ"];
            //newItem["Credit Card Debt J"] = properties.ListItem["CreditCardDebtJ"];
            //newItem["Investment Loans J"] = properties.ListItem["InvestmentLoansJ"];
            //newItem["Leases J"] = properties.ListItem["LeasesJ"];
            //newItem["Business Debt J"] = properties.ListItem["BusinessDebtJ"];
            //newItem["Liabilities J"] = properties.ListItem["LiabilitesJ"];
            //newItem["Income"] = properties.ListItem["Income"];
            //newItem["Client Income"] = properties.ListItem["ClientIncome"];
            //newItem["Partner Income"] = properties.ListItem["PartnerIncome"];
            //newItem["Income Protection"] = properties.ListItem["IncomeProtection"];
            //newItem["Income Protection C"] = properties.ListItem["IncomeProtectionC"];
            //newItem["Income Protection P"] = properties.ListItem["IncomeProtectionP"];
            //newItem["Life Cover"] = properties.ListItem["LifeCover"];
            //newItem["Disable"] = properties.ListItem["Disable"];
            //newItem["Trauma"] = properties.ListItem["Trauma"];
            //newItem["Other Insured"] = properties.ListItem["OtherInsured"];
            //newItem["Life Cover P"] = properties.ListItem["LifeCoverP"];
            //newItem["Disable P"] = properties.ListItem["DisableP"];
            //newItem["Trauma P"] = properties.ListItem["TraumaP"];
            //newItem["Other Insured P"] = properties.ListItem["OtherInsuredP"];
            //newItem["Life Cover C"] = properties.ListItem["LifeCoverC"];
            //newItem["Disable C"] = properties.ListItem["DisableC"];
            //newItem["Trauma C"] = properties.ListItem["TraumaC"];
            //newItem["Other Insured C"] = properties.ListItem["OtherInsuredC"];

            newItem.Update();
            addedFile.Item.Update();
            addedFile.Update();

            return (string)addedFile.Item[SPBuiltInFieldId.EncodedAbsUrl];
        }

        public void CopyStream(Stream source, Stream target)
        {
            if (source != null)
            {
                using (MemoryStream mstream = source as MemoryStream)
                {
                    if (mstream != null) mstream.WriteTo(target);
                    else
                    {
                        byte[] buffer = new byte[2048];
                        int length = buffer.Length, size;

                        while ((size = source.Read(buffer, 0, length)) != 0)
                            target.Write(buffer, 0, size);
                    }
                }
            }
        }

        private static string GetFirstAndOnlyValueFromChoice(string choiceColumnValue)
        {
            string[] choices = null;
            if (choiceColumnValue != null)
            {
                choices = choiceColumnValue.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
            }
            return choices[0];
        }

        private static SPFieldMultiChoiceValue CreateMultiValue(string input)
        {
            SPFieldMultiChoiceValue values = new SPFieldMultiChoiceValue();
            values.Add(input);

            return values;
        }

        public static SPListItem OptimizedAddItem(SPList list)
        {
            const string EmptyQuery = "0";
            SPQuery q = new SPQuery { Query = EmptyQuery };
            return list.GetItems(q).Add();
        }

    }
}

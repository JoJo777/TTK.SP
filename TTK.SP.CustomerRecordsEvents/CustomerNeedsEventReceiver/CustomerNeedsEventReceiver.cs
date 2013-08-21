using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.IO;
using Microsoft.Office.Word.Server.Service;
using Microsoft.Office.Word.Server.Conversions;

namespace TTK.SP.CustomerRecordsEvents.CustomerNeedsEventReceiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class CustomerNeedsEventReceiver : SPItemEventReceiver
    {
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

        //static string CustomerRecordsDocumentLibrary = "Customer Records";
        //static string ContentType = "Needs Analysis Content Type";

        static string CustomerRecordsDocumentLibrary = "XXXList";
        static string ContentType = "XXXContentType";

        protected void ConvertDotxToDocx(SPWeb web, string inputFile, string outputFile)
        {
            //var wordAutomationProxy = (WordServiceApplicationProxy)SPServiceContext.Current.GetDefaultProxy(typeof(WordServiceApplicationProxy));

            string wordAutomationServiceName = "Word Automation Service";
            ConversionJobSettings jobSettings = new ConversionJobSettings();

            jobSettings.OutputFormat = SaveFormat.Document;

            ConversionJob job = new ConversionJob(wordAutomationServiceName, jobSettings);
            job.UserToken = web.Site.UserToken;

            job.AddFile(inputFile, outputFile);

            job.Start();
        }

        private void CreateCustomerInRecords(SPItemEventProperties properties)
        {
            try
            {
                properties.Web.AllowUnsafeUpdates = true;

                Logging.WriteToLog(properties.Web, "CreateCustomerInRecords started");

                SPList customerRecord = properties.Web.Lists[CustomerRecordsDocumentLibrary];

                // this always uses root folder
                SPFileCollection spFileCollection = customerRecord.RootFolder.Files;

                // find the template url and open
                Logging.WriteToLog(properties.Web, "DocumentTemplateUrl: " + customerRecord.ContentTypes[ContentType].DocumentTemplateUrl);
                
                string sTemplate = customerRecord.ContentTypes[ContentType].DocumentTemplateUrl;
                string destFile = spFileCollection.Folder.Url + "/" + properties.ListItem["Title"] + properties.ListItem["FirstName"] + ".docx";

                ConvertDotxToDocx(properties.Web, sTemplate, destFile);

                // create the document and get SPFile/SPItem for new document
                SPFile addedFile = properties.Web.GetFile(destFile);

                //set the ct
                addedFile.Item["ContentType"] = ContentType;

                addedFile.Item["Title"] = properties.ListItem["Title"];

                //addedFile.Item["DateCaptured"] = properties.ListItem["DateCaptured"];
                //addedFile.Item["Referrer"] = properties.ListItem["Referrer"];

                addedFile.Item["XXXFirstName"] = properties.ListItem["FirstName"];

                addedFile.Item["XXXDoB"] = properties.ListItem["DoB"];
                addedFile.Item["XXXGender"] = GetFirstAndOnlyValueFromChoice(properties.ListItem["Gender"].ToString());
                //addedFile.Item["Marital"] = GetFirstAndOnlyValueFromChoice(properties.ListItem["Marital"].ToString());
                //addedFile.Item["CorrespondancePreference"] = GetFirstAndOnlyValueFromChoice(properties.ListItem["CorrespondancePreference"].ToString());

                addedFile.Item.SystemUpdate(); //Update does not work  http://aarebrot.net/blog/2011/03/how-to-set-the-content-type-of-a-new-file-in-a-document-library-when-uploading-said-file-through-code/

                addedFile.Update();

                Logging.WriteToLog(properties.Web, "CreateCustomerInRecords done");

            }
            catch (Exception ex)
            {
                Logging.WriteToLog(properties.Web, "ex.Message:  " + ex.Message + " ex.StackTrace: " + ex.StackTrace);
            }
            finally
            {
                properties.Web.AllowUnsafeUpdates = false;
            }

            //SPListItem item = OptimizedAddItem(customerRecord);

            //top
            ////item["DateCaptured"] = properties.ListItem["DateCaptured"];
            //item["Referrer"] = properties.ListItem["Referrer"];



            ////item["Title"] = properties.ListItem[""];

            //item["FirstName"] = properties.ListItem["FirstName"];


            //item["DoB"] = properties.ListItem[""];
            //item["Gender"] = properties.ListItem[""];


            //item["MaritalStatus"] = properties.ListItem[""];
            //item["Residential"] = properties.ListItem[""];
            //item["Business"] = properties.ListItem[""];
            //item["Home"] = properties.ListItem[""];
            //item["Email"] = properties.ListItem[""];
            //item["CorrespondancePreference"] = properties.ListItem[""];
            //item["Will"] = properties.ListItem[""];
            //item["Qantas"] = properties.ListItem[""];
            //item["RACV"] = properties.ListItem[""];
            //item["Solicitor"] = properties.ListItem[""];
            //item["Accountant"] = properties.ListItem[""];
            //item["Smoker"] = properties.ListItem[""];

            ////Tab2
            //if (IsPartnerActive())
            //{
            //    item["SurNameP"] = properties.ListItem[""];
            //    item["FirstNameP"] = properties.ListItem[""];

            //    item["DoBP"] = properties.ListItem[""];
            //    item["GenderP"] = properties.ListItem[""];

            //    item["MaritalStatusP"] = properties.ListItem[""];
            //    item["ResidentialP"] = properties.ListItem[""];
            //    item["BusinessP"] = properties.ListItem[""];
            //    item["HomeP"] = properties.ListItem[""];
            //    item["EmailP"] = properties.ListItem[""];
            //    item["CorrespondancePreferenceP"] = properties.ListItem[""];
            //    item["WillP"] = properties.ListItem[""];
            //    item["QantasP"] = properties.ListItem[""];
            //    item["RACVP"] = properties.ListItem[""];
            //    item["SolicitorP"] = properties.ListItem[""];
            //    item["AccountantP"] = properties.ListItem[""];
            //    item["SmokerP"] = properties.ListItem[""];
            //}

            ////tab3
            //item["Father"] = properties.ListItem[""];
            //item["FatherDOB"] = properties.ListItem[""];
            //item["FatherHealth"] = properties.ListItem[""];

            //item["Mother"] = properties.ListItem[""];
            //item["MotherDOB"] = properties.ListItem[""];
            //item["MotherHealth"] = properties.ListItem[""];

            //item["Brother"] = properties.ListItem[""];
            //item["BrotherDOB"] = properties.ListItem[""];
            //item["BrotherHealth"] = properties.ListItem[""];

            //item["Brother2"] = properties.ListItem[""];
            //item["Brother2DOB"] = properties.ListItem[""];
            //item["BrotherHealth2"] = properties.ListItem[""];

            //item["Sister"] = properties.ListItem[""];
            //item["SisterDOB"] = properties.ListItem[""];
            //item["SisterHealth"] = properties.ListItem[""];

            //item["Sister2"] = properties.ListItem[""];
            //item["SisterDOB2"] = properties.ListItem[""];
            //item["SisterHealth2"] = properties.ListItem[""];


            //item["FatherP"] = properties.ListItem[""];
            //item["FatherPDOB"] = properties.ListItem[""];
            //item["FatherHealthP"] = properties.ListItem[""];

            //item["MotherP"] = properties.ListItem[""];
            //item["MotherPDOB"] = properties.ListItem[""];
            //item["MotherHealthP"] = properties.ListItem[""];

            //item["BrotherP"] = properties.ListItem[""];
            //item["BrotherPDOB"] = properties.ListItem[""];
            //item["BrotherHealthP"] = properties.ListItem[""];

            //item["Brother2P"] = properties.ListItem[""];
            //item["Brother2PDOB"] = properties.ListItem[""];
            //item["BrotherHealth2P"] = properties.ListItem[""];

            //item["SisterP"] = properties.ListItem[""];
            //item["SisterPDOB"] = properties.ListItem[""];
            //item["SisterHealthP"] = properties.ListItem[""];

            //item["Sister2P"] = properties.ListItem[""];
            //item["Sister2PDOB"] = properties.ListItem[""];
            //item["SisterHealth2P"] = properties.ListItem[""];

            ////tab5 dependant details
            //item["DependantsFirst1"] = properties.ListItem[""];
            //item["DependantsSurName1"] = properties.ListItem[""];
            //item["Dependants1"] = properties.ListItem[""];
            //item["Relation1"] = properties.ListItem[""];
            //item["DependantsEducationOccupationLevel1"] = properties.ListItem[""];
            //item["DependantsSchoolUniversity1"] = properties.ListItem[""];
            //item["DependantsHealth1"] = properties.ListItem[""];

            //item["DependantsFirst2"] = properties.ListItem[""];
            //item["DependantsSurName2"] = properties.ListItem[""];
            //item["Dependants2"] = properties.ListItem[""];
            //item["Relation2"] = properties.ListItem[""];
            //item["DependantsEducationOccupationLevel2"] = properties.ListItem[""];
            //item["DependantsSchoolUniversity2"] = properties.ListItem[""];
            //item["DependantsHealth2"] = properties.ListItem[""];

            //item["DependantsFirst3"] = properties.ListItem[""];
            //item["DependantsSurName3"] = properties.ListItem[""];
            //item["Dependants3"] = properties.ListItem[""];
            //item["Relation3"] = properties.ListItem[""];
            //item["DependantsEducationOccupationLevel3"] = properties.ListItem[""];
            //item["DependantsSchoolUniversity3"] = properties.ListItem[""];
            //item["DependantsHealth3"] = properties.ListItem[""];

            //item["DependantsFirst4"] = properties.ListItem[""];
            //item["DependantsSurName4"] = properties.ListItem[""];
            //item["Dependants4"] = properties.ListItem[""];
            //item["Relation4"] = properties.ListItem[""];
            //item["DependantsEducationOccupationLevel4"] = properties.ListItem[""];
            //item["DependantsSchoolUniversity4"] = properties.ListItem[""];
            //item["DependantsHealth4"] = properties.ListItem[""];

            //item["DependantsFirst5"] = properties.ListItem[""];
            //item["DependantsSurName5"] = properties.ListItem[""];
            //item["Dependants5"] = properties.ListItem[""];
            //item["Relation5"] = properties.ListItem[""];
            //item["DependantsEducationOccupationLevel5"] = properties.ListItem[""];
            //item["DependantsSchoolUniversity5"] = properties.ListItem[""];
            //item["DependantsHealth5"] = properties.ListItem[""];

            ////tab6 Occupation
            //item["JobTitle"] = properties.ListItem[""];
            //item["EmploymentStatus"] = properties.ListItem[""];
            //item["Employer"] = properties.ListItem[""];
            //item["HoursPerWeek"] = properties.ListItem[""];
            //item["Qualifications"] = properties.ListItem[""];
            //item["Remuneration"] = properties.ListItem[""];
            //item["Duties"] = properties.ListItem[""];

            //item["JobTitleP"] = properties.ListItem[""];
            //item["EmploymentStatusP"] = properties.ListItem[""];
            //item["EmployerP"] = properties.ListItem[""];
            //item["HoursPerWeekP"] = properties.ListItem[""];
            //item["QualificationsP"] = properties.ListItem[""];
            //item["RemunerationP"] = properties.ListItem[""];
            //item["DutiesP"] = properties.ListItem[""];

            ////tab7 Health
            //item["HealthCondition"] = properties.ListItem[""];
            //item["HealthConditionP"] = properties.ListItem[""];

            ////tab8 Income
            //item["House"] = properties.ListItem[""];
            //item["Contents"] = properties.ListItem[""];
            //item["Super"] = properties.ListItem[""];
            //item["Cash"] = properties.ListItem[""];
            //item["Shares"] = properties.ListItem[""];
            //item["InvestmentProperties"] = properties.ListItem[""];
            //item["BusinessValue"] = properties.ListItem[""];
            //item["PotentialInheritance"] = properties.ListItem[""];
            //item["Mortgage"] = properties.ListItem[""];
            //item["PersonalLoans"] = properties.ListItem[""];
            //item["CreditCardDebt"] = properties.ListItem[""];
            //item["InvestmentLoans"] = properties.ListItem[""];
            //item["Leases"] = properties.ListItem[""];
            //item["BusinessDebt"] = properties.ListItem[""];
            //item["Liabilites"] = properties.ListItem[""];

            //item["HouseP"] = properties.ListItem[""];
            //item["ContentsP"] = properties.ListItem[""];
            //item["SuperP"] = properties.ListItem[""];
            //item["CashP"] = properties.ListItem[""];
            //item["SharesP"] = properties.ListItem[""];
            //item["InvestmentPropertiesP"] = properties.ListItem[""];
            //item["BusinessValueP"] = properties.ListItem[""];
            //item["PotentialInheritanceP"] = properties.ListItem[""];
            //item["MortgageP"] = properties.ListItem[""];
            //item["PersonalLoansP"] = properties.ListItem[""];
            //item["CreditCardDebtP"] = properties.ListItem[""];
            //item["InvestmentLoansP"] = properties.ListItem[""];
            //item["LeasesP"] = properties.ListItem[""];
            //item["BusinessDebtP"] = properties.ListItem[""];
            //item["LiabilitesP"] = properties.ListItem[""];

            //item["HouseJ"] = properties.ListItem[""];
            //item["ContentsJ"] = properties.ListItem[""];
            //item["SuperJ"] = properties.ListItem[""];
            //item["CashJ"] = properties.ListItem[""];
            //item["SharesJ"] = properties.ListItem[""];
            //item["InvestmentPropertiesJ"] = properties.ListItem[""];
            //item["BusinessValueJ"] = properties.ListItem[""];
            //item["PotentialInheritanceJ"] = properties.ListItem[""];
            //item["MortgageJ"] = properties.ListItem[""];
            //item["PersonalLoansJ"] = properties.ListItem[""];
            //item["CreditCardDebtJ"] = properties.ListItem[""];
            //item["InvestmentLoansJ"] = properties.ListItem[""];
            //item["LeasesJ"] = properties.ListItem[""];
            //item["BusinessDebtJ"] = properties.ListItem[""];
            //item["LiabilitesJ"] = properties.ListItem[""];

            //item["Income"] = properties.ListItem[""];

            //item["ClientIncome"] = properties.ListItem[""];
            //item["PartnerIncome"] = properties.ListItem[""];

            ////tab9 - Insurance
            //item["IncomeProtection"] = properties.ListItem[""];
            //item["LifeCover"] = properties.ListItem[""];
            //item["Disable"] = properties.ListItem[""];
            //item["Trauma"] = properties.ListItem[""];

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
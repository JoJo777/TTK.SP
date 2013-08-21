using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace TTK.SP.Core.Features
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("113cce44-8b40-4777-add5-03f8e444765b")]
    public class SPCoreEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        static string CustomerList = "Customer2";
        static string CustomerRecordsDocumentLibrary = "Customer Records";
        static string ContentType = "Needs Analysis Content Type";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var web = (SPWeb)properties.Feature.Parent as SPWeb;

            MoveAndSetCustomerWizardFile(web);

            SetupXXXCustomerRecordDocumentLibrary(web);

            SetupCustomerRecordDocumentLibrary(web);
        }

        private void SetupXXXCustomerRecordDocumentLibrary(SPWeb web)
        {
            try
            {
                web.AllowUnsafeUpdates = true;

                SPList customerRecordsDocumentLibrary = web.Lists["XXXList"];
                customerRecordsDocumentLibrary.ContentTypesEnabled = true;

                //customerRecordsDocumentLibrary.ContentTypes["Document"].Delete();

                customerRecordsDocumentLibrary.ContentTypes["XXXContentType"].DocumentTemplate = "/_cts/XXXContentType/template.dotx";
                
                customerRecordsDocumentLibrary.ContentTypes["XXXContentType"].Update();
                customerRecordsDocumentLibrary.Update();
            }
            catch (Exception ex)
            {
                //do stuff
                throw new Exception(ex.Message);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
            }
        }

        //If Deployed in a feature, Word Ignores the Content Type When Saving?
        //http://msdn.microsoft.com/en-us/library/aa543733(v=office.14).aspx
        private void SetupCustomerRecordDocumentLibrary(SPWeb web)
        {
            try
            {
                web.AllowUnsafeUpdates = true;

                SPList customerRecordsDocumentLibrary = web.Lists[CustomerRecordsDocumentLibrary];
                customerRecordsDocumentLibrary.ContentTypesEnabled = true;

                customerRecordsDocumentLibrary.ContentTypes["Document"].Delete();

                customerRecordsDocumentLibrary.ContentTypes[ContentType].DocumentTemplate = "/_cts/NeedsAnalysisContentType/NeedsAnalysis.docx";

                customerRecordsDocumentLibrary.ContentTypes[ContentType].Update();
                customerRecordsDocumentLibrary.Update();
            }
            catch (Exception ex)
            {
                //do stuff
                throw new Exception(ex.Message);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
            }
        }

        private void MoveAndSetCustomerWizardFile(SPWeb web)
        {
            try
            {
                web.AllowUnsafeUpdates = true;
                // /Style Library/Module/CustomForms/CustomerWizard.txt
                SPFile file = web.GetFile("Style Library/Module/CustomForms/CustomerWizard.aspx");

                if (file == null) //moved already
                    return;

                file.MoveTo("/Lists/" + CustomerList + "/CustomerWizard.aspx", true);

                file.Update();

                SPList list = web.Lists[CustomerList];

                list.NavigateForFormsPages = true;

                SPContentType ct = list.ContentTypes["ListFieldsContentType"];

                ct.NewFormUrl = "/Lists/" + CustomerList + "/CustomerWizard.aspx";
                ct.DisplayFormUrl = "/Lists/" + CustomerList + "/CustomerWizard.aspx";
                ct.EditFormUrl = "/Lists/" + CustomerList + "/CustomerWizard.aspx";

                ct.Update();
                list.Update();
            }
            catch (Exception ex)
            {
                //do stuff
                throw new Exception(ex.Message);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}


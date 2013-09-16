using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace TTK.SP.NeedsAnalysis.WP
{
    [ToolboxItemAttribute(false)]
    public partial class NeedsAnalysis : WebPart
    {
        const int WizardTabsClient0 = 0;
        const int WizardTabsPartner1 = 1;
        const int WizardTabsClientFamily2 = 2;
        const int WizardTabsPartnerFamily3 = 3;
        const int WizardTabsOccupation4 = 4;
        const int WizardTabsHealth5 = 5;
        const int WizardTabsFinance6 = 6;
        const int WizardTabsInsurance7 = 7;

        static string CustomerList = "Customers";
        static string NewBusinessRegisterList = "New Business Register";
        static string UnderwritingRegisterList = "Underwriting Register";

        int ListItemId = 0;
        int VersionNumber = -1;

        CultureInfo australiaCulture = new CultureInfo("en-AU");

        public NeedsAnalysis()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected override void CreateChildControls()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                SetDates();

                ListItemId = GetItemId();
                VersionNumber = GetVersionedItem();

                if (ListItemId > 0)
                    if (VersionNumber > 0)
                        LoadCustomerVersionedData(ListItemId, VersionNumber); //read only
                    else
                        LoadCustomerData(ListItemId);
                else
                {
                    lbDateCaptured.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    ddNewBusinessRegister.SelectedValue = "In Progress"; ///default for new.
                    ddUnderWritingRegister.SelectedValue = "Not Started"; ///default for new.
                }
            }

            EnablePartner(IsPartnerActive());

            ErrorLabel.Visible = false;
            ErrorLabel.Text = "";
        }

        private int GetItemId()
        {
            string ID = Page.Request.QueryString["ID"];

            if (!string.IsNullOrEmpty(ID))
            {
                int intToParse = 0;
                Int32.TryParse(ID, out intToParse);
                return intToParse;
            }
            else
                return 0; //New Record
        }

        private void LoadCustomerData(int customerListItemId)
        {
            SPListItem customerListItem = SPContext.Current.Web.Lists[CustomerList].Items.GetItemById(customerListItemId);

            Logging.WriteToLog(SPContext.Current, "starting load: ID=" + ListItemId);

            //assign to fields
            //top
            lbDateCaptured.Text = ((DateTime)customerListItem["DateCaptured"]).ToString("dd/MM/yyyy");
            txtReferrer.Text = GetEmptyStringIfNull(customerListItem["Referrer"]);

            //Tab1
            txtLastName.Text = customerListItem["Title"].ToString();

            ddNewBusinessRegister.SelectedValue = GetEmptyStringIfNull(customerListItem["NewBusinessRegister"]);
            ddUnderWritingRegister.SelectedValue = GetEmptyStringIfNull(customerListItem["UnderwritingRegister"]);

            txtFirstName.Text = GetEmptyStringIfNull(customerListItem["FirstName"]);

            txtDOB.Text = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["DoB"]).ToString(), australiaCulture).Date.ToString().Replace(" 12:00:00 AM", "");
            txtDOB.Text = (txtDOB.Text.Substring(1, 1) != "/") ? txtDOB.Text : "0" + txtDOB.Text; //zero pad

            //calDOB.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["DoB"]).ToString(), australiaCulture); ;

            ddGender.SelectedValue = customerListItem["Gender"].ToString();

            ddMarital.SelectedValue = customerListItem["MaritalStatus"].ToString();
            txtResidential.Text = GetEmptyStringIfNull(customerListItem["Residential"]);
            txtBusiness.Text = GetEmptyStringIfNull(customerListItem["Business"]);
            txtHome.Text = GetEmptyStringIfNull(customerListItem["Home"]);
            txtMobile.Text = GetEmptyStringIfNull(customerListItem["Mobile"]);

            txtEmail.Text = GetEmptyStringIfNull(customerListItem["Email"]);
            ddCorrespondancePreference.SelectedValue = GetEmptyStringIfNull(customerListItem["CorrespondancePreference"]);

            cbWill.Checked = (bool)customerListItem["Will"];
            txtQantas.Text = GetEmptyStringIfNull(customerListItem["Qantas"]);
            txtRACV.Text = GetEmptyStringIfNull(customerListItem["RACV"]);
            txtSolicitor.Text = GetEmptyStringIfNull(customerListItem["Solicitor"]);
            txtAccountant.Text = GetEmptyStringIfNull(customerListItem["Accountant"]);
            ddSmoker.SelectedValue = GetEmptyStringIfNull(customerListItem["Smoker"]);

            //Tab2
            //MUST ALWAYS LOAD PARTNER DETAILS

            txtSurNameP.Text = GetEmptyStringIfNull(customerListItem["SurNameP"]);
            txtFirstNameP.Text = GetEmptyStringIfNull(customerListItem["FirstNameP"]);

            txtDOBP.Text = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["DoBP"]).ToString(), australiaCulture).Date.ToString().Replace(" 12:00:00 AM", "");
            txtDOBP.Text = (txtDOBP.Text.Substring(1, 1) != "/") ? txtDOBP.Text : "0" + txtDOBP.Text; //zero pad

            //calDOBP.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["DoBP"]).ToString(), australiaCulture);
            ddGenderP.SelectedValue = GetEmptyStringIfNull(customerListItem["GenderP"]);

            ddMaritalP.SelectedValue = GetEmptyStringIfNull(customerListItem["MaritalStatusP"]);
            txtResidentialP.Text = GetEmptyStringIfNull(customerListItem["ResidentialP"]);

            txtBusinessP.Text = GetEmptyStringIfNull(customerListItem["BusinessP"]);
            txtHomeP.Text = GetEmptyStringIfNull(customerListItem["HomeP"]);
            txtMobileP.Text = GetEmptyStringIfNull(customerListItem["MobileP"]);

            txtEmailP.Text = GetEmptyStringIfNull(customerListItem["EmailP"]);
            ddCorrespondencePreferenceP.SelectedValue = GetEmptyStringIfNull(customerListItem["CorrespondancePreferenceP"]);

            cbWillP.Checked = (bool)customerListItem["WillP"];
            txtQantasP.Text = GetEmptyStringIfNull(customerListItem["QantasP"]);
            txtRACVP.Text = GetEmptyStringIfNull(customerListItem["RACVP"]);
            txtSolicitorP.Text = GetEmptyStringIfNull(customerListItem["SolicitorP"]);
            txtAccountantP.Text = GetEmptyStringIfNull(customerListItem["AccountantP"]);
            ddSmokerP.SelectedValue = customerListItem["SmokerP"].ToString();


            //tab3
            txtFather.Text = GetEmptyStringIfNull(customerListItem["Father"]);
            txtFatherDOB.Text = GetTextBoxDateString(customerListItem, "FatherDOB");
            //calFather.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["FatherDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbFatherHealth, customerListItem["FatherHealth"]);

            txtMother.Text = GetEmptyStringIfNull(customerListItem["Mother"]);
            txtMotherDOB.Text = GetTextBoxDateString(customerListItem, "MotherDOB");
            //calMother.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["MotherDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbMotherHealth, customerListItem["MotherHealth"]);

            txtBrother.Text = GetEmptyStringIfNull(customerListItem["Brother"]);
            txtBrotherDOB.Text = GetTextBoxDateString(customerListItem, "BrotherDOB");
            //calBrother.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["BrotherDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbBrotherHealth, customerListItem["BrotherHealth"]);

            txtBrother2.Text = GetEmptyStringIfNull(customerListItem["Brother2"]);
            txtBrother2DOB.Text = GetTextBoxDateString(customerListItem, "Brother2DOB");
            ///calBrother2.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Brother2DOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbBrotherHealth2, customerListItem["BrotherHealth2"]);

            txtSister.Text = GetEmptyStringIfNull(customerListItem["Sister"]);
            txtSisterDOB.Text = GetTextBoxDateString(customerListItem, "SisterDOB");
            //calSister.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["SisterDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbSisterHealth, customerListItem["SisterHealth"]);

            txtSister2.Text = GetEmptyStringIfNull(customerListItem["Sister2"]);
            txtSister2DOB.Text = GetTextBoxDateString(customerListItem, "SisterDOB2");
            //calSister2.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["SisterDOB2"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbSisterHealth2, customerListItem["SisterHealth2"]);

            //tab4
            //MUST ALWAYS LOAD PARTNER DETAILS
            txtFatherP.Text = GetEmptyStringIfNull(customerListItem["FatherP"]);
            txtFatherPDOB.Text = GetTextBoxDateString(customerListItem, "FatherPDOB");
            //calFatherP.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["FatherPDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbFatherHealthP, customerListItem["FatherHealthP"]);

            txtMotherP.Text = GetEmptyStringIfNull(customerListItem["MotherP"]);
            txtMotherPDOB.Text = GetTextBoxDateString(customerListItem, "MotherPDOB");
            //calMotherP.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["MotherPDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbMotherHealthP, customerListItem["MotherHealthP"]);

            txtBrotherP.Text = GetEmptyStringIfNull(customerListItem["BrotherP"]);
            txtBrotherPDOB.Text = GetTextBoxDateString(customerListItem, "BrotherPDOB");
            //calBrotherP.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["BrotherPDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbBrotherHealthP, customerListItem["BrotherHealthP"]);

            txtBrother2P.Text = GetEmptyStringIfNull(customerListItem["Brother2P"]);
            txtBrother2PDOB.Text = GetTextBoxDateString(customerListItem, "Brother2PDOB");
            //calBrother2P.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Brother2PDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbBrotherHealth2P, customerListItem["BrotherHealth2P"]);

            txtSisterP.Text = GetEmptyStringIfNull(customerListItem["SisterP"]);
            txtSisterPDOB.Text = GetTextBoxDateString(customerListItem, "SisterPDOB");
            //calSisterP.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["SisterPDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbSisterHealthP, customerListItem["SisterHealthP"]);

            txtSister2P.Text = GetEmptyStringIfNull(customerListItem["Sister2P"]);
            txtSister2PDOB.Text = GetTextBoxDateString(customerListItem, "Sister2PDOB");
            //calSister2P.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Sister2PDOB"]).ToString(), australiaCulture);
            SetCheckBoxValues(cbSisterHealth2P, customerListItem["SisterHealth2P"]);


            //tab5 dependant details
            txtDependantsFirst1.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst1"]);
            txtDependantsSurName1.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName1"]);
            txtDependants1DOB.Text = GetTextBoxDateString(customerListItem, "Dependants1");
            //calDependants1.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Dependants1"]).ToString(), australiaCulture); 
            ddRelation1.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation1"]);
            txtDependantsEducationOccupationLevel1.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel1"]);
            txtDependantsSchoolUniversity1.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity1"]);
            SetCheckBoxValues(cbDependantsHealth1, customerListItem["DependantsHealth1"]);

            txtDependantsFirst2.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst2"]);
            txtDependantsSurName2.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName2"]);
            txtDependants2DOB.Text = GetTextBoxDateString(customerListItem, "Dependants2");
            //calDependants2.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Dependants2"]).ToString(), australiaCulture); 
            ddRelation2.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation2"]);
            txtDependantsEducationOccupationLevel2.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel2"]);
            txtDependantsSchoolUniversity2.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity2"]);
            SetCheckBoxValues(cbDependantsHealth2, customerListItem["DependantsHealth2"]);

            txtDependantsFirst3.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst3"]);
            txtDependantsSurName3.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName3"]);
            txtDependants3DOB.Text = GetTextBoxDateString(customerListItem, "Dependants3");
            //calDependants3.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Dependants3"]).ToString(), australiaCulture);
            ddRelation3.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation3"]);
            txtDependantsEducationOccupationLevel3.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel3"]);
            txtDependantsSchoolUniversity3.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity3"]);
            SetCheckBoxValues(cbDependantsHealth3, customerListItem["DependantsHealth3"]);

            txtDependantsFirst4.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst4"]);
            txtDependantsSurName4.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName4"]);
            txtDependants4DOB.Text = GetTextBoxDateString(customerListItem, "Dependants4");
            //calDependants4.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Dependants4"]).ToString(), australiaCulture); 
            ddRelation4.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation4"]);
            txtDependantsEducationOccupationLevel4.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel4"]);
            txtDependantsSchoolUniversity4.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity4"]);
            SetCheckBoxValues(cbDependantsHealth4, customerListItem["DependantsHealth4"]);

            txtDependantsFirst5.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst5"]);
            txtDependantsSurName5.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName5"]);
            txtDependants5DOB.Text = GetTextBoxDateString(customerListItem, "Dependants5");
            //calDependants5.SelectedDate = Convert.ToDateTime(GetEmptyStringIfNull(customerListItem["Dependants5"]).ToString(), australiaCulture);
            ddRelation5.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation5"]);
            txtDependantsEducationOccupationLevel5.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel5"]);
            txtDependantsSchoolUniversity5.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity5"]);
            SetCheckBoxValues(cbDependantsHealth5, customerListItem["DependantsHealth5"]);

            //tab6 Occupation
            txtJobTitle.Text = GetEmptyStringIfNull(customerListItem["JobTitle"]);
            ddEmploymentStatus.SelectedValue = GetEmptyStringIfNull(customerListItem["EmploymentStatus"]);
            txtEmployer.Text = GetEmptyStringIfNull(customerListItem["Employer"]);
            txtHoursPerWeek.Text = GetEmptyStringIfNull(customerListItem["HoursPerWeek"]);
            txtQualifications.Text = GetEmptyStringIfNull(customerListItem["Qualifications"]);
            txtRemuneration.Text = GetEmptyStringIfNull(customerListItem["Remuneration"]);
            txtDuties.Text = GetEmptyStringIfNull(customerListItem["Duties"]);

            txtJobTitleP.Text = GetEmptyStringIfNull(customerListItem["JobTitleP"]);
            ddEmploymentStatusP.SelectedValue = GetEmptyStringIfNull(customerListItem["EmploymentStatusP"]);
            txtEmployerP.Text = GetEmptyStringIfNull(customerListItem["EmployerP"]);
            txtHoursPerWeekP.Text = GetEmptyStringIfNull(customerListItem["HoursPerWeekP"]);
            txtQualificationsP.Text = GetEmptyStringIfNull(customerListItem["QualificationsP"]);
            txtRemunerationP.Text = GetEmptyStringIfNull(customerListItem["RemunerationP"]);
            txtDutiesP.Text = GetEmptyStringIfNull(customerListItem["DutiesP"]);

            //tab7 Health
            SetCheckBoxValues(cbHealthCondition, customerListItem["HealthCondition"]);
            SetCheckBoxValues(cbHealthConditionP, customerListItem["HealthConditionP"]);

            //tab8 Income
            txtHouse.Text = GetEmptyStringIfNull(customerListItem["House"]);
            txtContents.Text = GetEmptyStringIfNull(customerListItem["Contents"]);
            txtSuper.Text = GetEmptyStringIfNull(customerListItem["Super"]);
            txtCash.Text = GetEmptyStringIfNull(customerListItem["Cash"]);
            txtShares.Text = GetEmptyStringIfNull(customerListItem["Shares"]);
            txtInvestmentProperties.Text = GetEmptyStringIfNull(customerListItem["InvestmentProperties"]);
            txtBusinessValue.Text = GetEmptyStringIfNull(customerListItem["BusinessValue"]);
            txtPotentialInheritance.Text = GetEmptyStringIfNull(customerListItem["PotentialInheritance"]);
            txtMortgage.Text = GetEmptyStringIfNull(customerListItem["Mortgage"]);
            txtPersonalLoans.Text = GetEmptyStringIfNull(customerListItem["PersonalLoans"]);
            txtCreditCardDebt.Text = GetEmptyStringIfNull(customerListItem["CreditCardDebt"]);
            txtInvestmentLoans.Text = GetEmptyStringIfNull(customerListItem["InvestmentLoans"]);
            txtLeases.Text = GetEmptyStringIfNull(customerListItem["Leases"]);
            txtBusinessDebt.Text = GetEmptyStringIfNull(customerListItem["BusinessDebt"]);
            txtLiabilites.Text = GetEmptyStringIfNull(customerListItem["Liabilites"]);

            txtHouseP.Text = GetEmptyStringIfNull(customerListItem["HouseP"]);
            txtContentsP.Text = GetEmptyStringIfNull(customerListItem["ContentsP"]);
            txtSuperP.Text = GetEmptyStringIfNull(customerListItem["SuperP"]);
            txtCashP.Text = GetEmptyStringIfNull(customerListItem["CashP"]);
            txtSharesP.Text = GetEmptyStringIfNull(customerListItem["SharesP"]);
            txtInvestmentPropertiesP.Text = GetEmptyStringIfNull(customerListItem["InvestmentPropertiesP"]);
            txtBusinessValueP.Text = GetEmptyStringIfNull(customerListItem["BusinessValueP"]);
            txtPotentialInheritanceP.Text = GetEmptyStringIfNull(customerListItem["PotentialInheritanceP"]);
            txtMortgageP.Text = GetEmptyStringIfNull(customerListItem["MortgageP"]);
            txtPersonalLoansP.Text = GetEmptyStringIfNull(customerListItem["PersonalLoansP"]);
            txtCreditCardDebtP.Text = GetEmptyStringIfNull(customerListItem["CreditCardDebtP"]);
            txtInvestmentLoansP.Text = GetEmptyStringIfNull(customerListItem["InvestmentLoansP"]);
            txtLeasesP.Text = GetEmptyStringIfNull(customerListItem["LeasesP"]);
            txtBusinessDebtP.Text = GetEmptyStringIfNull(customerListItem["BusinessDebtP"]);
            txtLiabilitesP.Text = GetEmptyStringIfNull(customerListItem["LiabilitesP"]);

            txtHouseJ.Text = GetEmptyStringIfNull(customerListItem["HouseJ"]);
            txtContentsJ.Text = GetEmptyStringIfNull(customerListItem["ContentsJ"]);
            txtSuperJ.Text = GetEmptyStringIfNull(customerListItem["SuperJ"]);
            txtCashJ.Text = GetEmptyStringIfNull(customerListItem["CashJ"]);
            txtSharesJ.Text = GetEmptyStringIfNull(customerListItem["SharesJ"]);
            txtInvestmentPropertiesJ.Text = GetEmptyStringIfNull(customerListItem["InvestmentPropertiesJ"]);
            txtBusinessValueJ.Text = GetEmptyStringIfNull(customerListItem["BusinessValueJ"]);
            txtPotentialInheritanceJ.Text = GetEmptyStringIfNull(customerListItem["PotentialInheritanceJ"]);
            txtMortgageJ.Text = GetEmptyStringIfNull(customerListItem["MortgageJ"]);
            txtPersonalLoansJ.Text = GetEmptyStringIfNull(customerListItem["PersonalLoansJ"]);
            txtCreditCardDebtJ.Text = GetEmptyStringIfNull(customerListItem["CreditCardDebtJ"]);
            txtInvestmentLoansJ.Text = GetEmptyStringIfNull(customerListItem["InvestmentLoansJ"]);
            txtLeasesJ.Text = GetEmptyStringIfNull(customerListItem["LeasesJ"]);
            txtBusinessDebtJ.Text = GetEmptyStringIfNull(customerListItem["BusinessDebtJ"]);
            txtLiabilitesJ.Text = GetEmptyStringIfNull(customerListItem["LiabilitesJ"]);

            ddIncome.SelectedValue = GetEmptyStringIfNull(customerListItem["Income"]);

            txtClientIncome.Text = GetEmptyStringIfNull(customerListItem["ClientIncome"]);
            txtPartnerIncome.Text = GetEmptyStringIfNull(customerListItem["PartnerIncome"]);

            //tab9 - Insurance
            txtIncomeProtection.Text = GetEmptyStringIfNull(customerListItem["IncomeProtection"]);
            txtLifeCover.Text = GetEmptyStringIfNull(customerListItem["LifeCover"]);
            txtDisable.Text = GetEmptyStringIfNull(customerListItem["Disable"]);
            txtTrauma.Text = GetEmptyStringIfNull(customerListItem["Trauma"]);
            txtOtherInsured.Text = GetEmptyStringIfNull(customerListItem["OtherInsured"]);

            txtIncomeProtectionP.Text = GetEmptyStringIfNull(customerListItem["IncomeProtectionP"]);
            txtLifeCoverP.Text = GetEmptyStringIfNull(customerListItem["LifeCoverP"]);
            txtDisableP.Text = GetEmptyStringIfNull(customerListItem["DisableP"]);
            txtTraumaP.Text = GetEmptyStringIfNull(customerListItem["TraumaP"]);
            txtOtherInsuredP.Text = GetEmptyStringIfNull(customerListItem["OtherInsuredP"]);

            txtIncomeProtectionC.Text = GetEmptyStringIfNull(customerListItem["IncomeProtectionC"]);
            txtLifeCoverC.Text = GetEmptyStringIfNull(customerListItem["LifeCoverC"]);
            txtDisableC.Text = GetEmptyStringIfNull(customerListItem["DisableC"]);
            txtTraumaC.Text = GetEmptyStringIfNull(customerListItem["TraumaC"]);
            txtOtherInsuredC.Text = GetEmptyStringIfNull(customerListItem["OtherInsuredC"]);

            Logging.WriteToLog(SPContext.Current, "loaded");
        }

        private void LoadCustomerVersionedData(int customerListItemId, int versionNumber)
        {
            //if (item.HasPublishedVersion)
            //if (null != item.Versions && item.Versions.Count > 1)
            SPListItemVersion customerListItem = (SPListItemVersion)SPContext.Current.Web.Lists[CustomerList].GetItemById(customerListItemId).Versions.GetVersionFromID(versionNumber);

            lbVersion.Visible = true;
            lbVersion.Text = "Version: " + customerListItem.VersionLabel;

            DisableControlsForVersion();

            Logging.WriteToLog(SPContext.Current, "starting load: ID=" + ListItemId);

            //assign to fields
            //top
            lbDateCaptured.Text = ((DateTime)customerListItem["DateCaptured"]).ToString("dd/MM/yyyy");
            txtReferrer.Text = GetEmptyStringIfNull(customerListItem["Referrer"]);

            //Tab1
            txtLastName.Text = customerListItem["Title"].ToString();

            ddNewBusinessRegister.SelectedValue = GetEmptyStringIfNull(customerListItem["NewBusinessRegister"]);
            ddUnderWritingRegister.SelectedValue = GetEmptyStringIfNull(customerListItem["UnderwritingRegister"]);

            txtFirstName.Text = GetEmptyStringIfNull(customerListItem["FirstName"]);

            txtDOB.Text = GetTextBoxDateStringVersion(customerListItem, "DoB");
            //calDOB.SelectedDate = (DateTime)customerListItem["DoB"];
            ddGender.SelectedValue = customerListItem["Gender"].ToString();

            ddMarital.SelectedValue = customerListItem["MaritalStatus"].ToString();
            txtResidential.Text = GetEmptyStringIfNull(customerListItem["Residential"]);
            txtBusiness.Text = GetEmptyStringIfNull(customerListItem["Business"]);
            txtHome.Text = GetEmptyStringIfNull(customerListItem["Home"]);
            txtMobile.Text = GetEmptyStringIfNull(customerListItem["Mobile"]);

            txtEmail.Text = GetEmptyStringIfNull(customerListItem["Email"]);
            ddCorrespondancePreference.SelectedValue = GetEmptyStringIfNull(customerListItem["CorrespondancePreference"]);

            cbWill.Checked = (bool)customerListItem["Will"];
            txtQantas.Text = GetEmptyStringIfNull(customerListItem["Qantas"]);
            txtRACV.Text = GetEmptyStringIfNull(customerListItem["RACV"]);
            txtSolicitor.Text = GetEmptyStringIfNull(customerListItem["Solicitor"]);
            txtAccountant.Text = GetEmptyStringIfNull(customerListItem["Accountant"]);
            ddSmoker.SelectedValue = GetEmptyStringIfNull(customerListItem["Smoker"]);

            //MUST ALWAYS LOAD PARTNER DETAILS
            txtSurNameP.Text = GetEmptyStringIfNull(customerListItem["SurNameP"]);
            txtFirstNameP.Text = GetEmptyStringIfNull(customerListItem["FirstNameP"]);

            txtDOBP.Text = GetTextBoxDateStringVersion(customerListItem, "DoBP");
            //calDOBP.SelectedDate = (DateTime)customerListItem["DoBP"];

            ddGenderP.SelectedValue = GetEmptyStringIfNull(customerListItem["GenderP"]);

            ddMaritalP.SelectedValue = GetEmptyStringIfNull(customerListItem["MaritalStatusP"]);
            txtResidentialP.Text = GetEmptyStringIfNull(customerListItem["ResidentialP"]);

            txtBusinessP.Text = GetEmptyStringIfNull(customerListItem["BusinessP"]);
            txtHomeP.Text = GetEmptyStringIfNull(customerListItem["HomeP"]);
            txtMobileP.Text = GetEmptyStringIfNull(customerListItem["MobileP"]);

            txtEmailP.Text = GetEmptyStringIfNull(customerListItem["EmailP"]);
            ddCorrespondencePreferenceP.SelectedValue = GetEmptyStringIfNull(customerListItem["CorrespondancePreferenceP"]);

            cbWillP.Checked = (bool)customerListItem["WillP"];
            txtQantasP.Text = GetEmptyStringIfNull(customerListItem["QantasP"]);
            txtRACVP.Text = GetEmptyStringIfNull(customerListItem["RACVP"]);
            txtSolicitorP.Text = GetEmptyStringIfNull(customerListItem["SolicitorP"]);
            txtAccountantP.Text = GetEmptyStringIfNull(customerListItem["AccountantP"]);
            ddSmokerP.SelectedValue = customerListItem["SmokerP"].ToString();


            //tab3
            txtFather.Text = GetEmptyStringIfNull(customerListItem["Father"]);
            txtFatherDOB.Text = GetTextBoxDateStringVersion(customerListItem, "FatherDOB");
            //calFather.SelectedDate = (DateTime)customerListItem["FatherDOB"];
            SetCheckBoxValues(cbFatherHealth, customerListItem["FatherHealth"]);

            txtMother.Text = GetEmptyStringIfNull(customerListItem["Mother"]);
            txtMotherDOB.Text = GetTextBoxDateStringVersion(customerListItem, "MotherDOB");
            //calMother.SelectedDate = (DateTime)customerListItem["MotherDOB"];
            SetCheckBoxValues(cbMotherHealth, customerListItem["MotherHealth"]);

            txtBrother.Text = GetEmptyStringIfNull(customerListItem["Brother"]);
            txtBrotherDOB.Text = GetTextBoxDateStringVersion(customerListItem, "BrotherDOB");
            //calBrother.SelectedDate = (DateTime)customerListItem["BrotherDOB"];
            SetCheckBoxValues(cbBrotherHealth, customerListItem["BrotherHealth"]);

            txtBrother2.Text = GetEmptyStringIfNull(customerListItem["Brother2"]);
            txtBrother2DOB.Text = GetTextBoxDateStringVersion(customerListItem, "Brother2DOB");
            //calBrother2.SelectedDate = (DateTime)customerListItem["Brother2DOB"];
            SetCheckBoxValues(cbBrotherHealth2, customerListItem["BrotherHealth2"]);

            txtSister.Text = GetEmptyStringIfNull(customerListItem["Sister"]);
            txtSisterDOB.Text = GetTextBoxDateStringVersion(customerListItem, "SisterDOB");
            //calSister.SelectedDate = (DateTime)customerListItem["SisterDOB"];
            SetCheckBoxValues(cbSisterHealth, customerListItem["SisterHealth"]);

            txtSister2.Text = GetEmptyStringIfNull(customerListItem["Sister2"]);
            txtSister2DOB.Text = GetTextBoxDateStringVersion(customerListItem, "SisterDOB2");
            //calSister2.SelectedDate = (DateTime)customerListItem["SisterDOB2"];
            SetCheckBoxValues(cbSisterHealth2, customerListItem["SisterHealth2"]);

            //tab4

            txtFatherP.Text = GetEmptyStringIfNull(customerListItem["FatherP"]);
            txtFatherPDOB.Text = GetTextBoxDateStringVersion(customerListItem, "FatherPDOB");
            //calFatherP.SelectedDate = (DateTime)customerListItem["FatherPDOB"];
            SetCheckBoxValues(cbFatherHealthP, customerListItem["FatherHealthP"]);

            txtMotherP.Text = GetEmptyStringIfNull(customerListItem["MotherP"]);
            txtMotherPDOB.Text = GetTextBoxDateStringVersion(customerListItem, "MotherPDOB");
            //calMotherP.SelectedDate = (DateTime)customerListItem["MotherPDOB"];
            SetCheckBoxValues(cbMotherHealthP, customerListItem["MotherHealthP"]);

            txtBrotherP.Text = GetEmptyStringIfNull(customerListItem["BrotherP"]);
            txtBrotherPDOB.Text = GetTextBoxDateStringVersion(customerListItem, "BrotherPDOB");
            //calBrotherP.SelectedDate = (DateTime)customerListItem["BrotherPDOB"];
            SetCheckBoxValues(cbBrotherHealthP, customerListItem["BrotherHealthP"]);

            txtBrother2P.Text = GetEmptyStringIfNull(customerListItem["Brother2P"]);
            txtBrother2PDOB.Text = GetTextBoxDateStringVersion(customerListItem, "Brother2PDOB");
            //calBrother2P.SelectedDate = (DateTime)customerListItem["Brother2PDOB"];
            SetCheckBoxValues(cbBrotherHealth2P, customerListItem["BrotherHealth2P"]);

            txtSisterP.Text = GetEmptyStringIfNull(customerListItem["SisterP"]);
            txtSisterPDOB.Text = GetTextBoxDateStringVersion(customerListItem, "SisterPDOB");
            //calSisterP.SelectedDate = (DateTime)customerListItem["SisterPDOB"];
            SetCheckBoxValues(cbSisterHealthP, customerListItem["SisterHealthP"]);

            txtSister2P.Text = GetEmptyStringIfNull(customerListItem["Sister2P"]);
            txtSister2PDOB.Text = GetTextBoxDateStringVersion(customerListItem, "Sister2PDOB");
            //calSister2P.SelectedDate = (DateTime)customerListItem["Sister2PDOB"];
            SetCheckBoxValues(cbSisterHealth2P, customerListItem["SisterHealth2P"]);

            //tab5 dependant details
            txtDependantsFirst1.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst1"]);
            txtDependantsSurName1.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName1"]);
            txtDependants1DOB.Text = GetTextBoxDateStringVersion(customerListItem, "Dependants1");
            //calDependants1.SelectedDate = (DateTime)customerListItem["Dependants1"];
            ddRelation1.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation1"]);
            txtDependantsEducationOccupationLevel1.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel1"]);
            txtDependantsSchoolUniversity1.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity1"]);
            SetCheckBoxValues(cbDependantsHealth1, customerListItem["DependantsHealth1"]);

            txtDependantsFirst2.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst2"]);
            txtDependantsSurName2.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName2"]);
            txtDependants2DOB.Text = GetTextBoxDateStringVersion(customerListItem, "Dependants2");
            //calDependants2.SelectedDate = (DateTime)customerListItem["Dependants2"];
            ddRelation2.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation2"]);
            txtDependantsEducationOccupationLevel2.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel2"]);
            txtDependantsSchoolUniversity2.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity2"]);
            SetCheckBoxValues(cbDependantsHealth2, customerListItem["DependantsHealth2"]);

            txtDependantsFirst3.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst3"]);
            txtDependantsSurName3.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName3"]);
            txtDependants3DOB.Text = GetTextBoxDateStringVersion(customerListItem, "Dependants3");
            //calDependants3.SelectedDate = (DateTime)customerListItem["Dependants3"];
            ddRelation3.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation3"]);
            txtDependantsEducationOccupationLevel3.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel3"]);
            txtDependantsSchoolUniversity3.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity3"]);
            SetCheckBoxValues(cbDependantsHealth3, customerListItem["DependantsHealth3"]);

            txtDependantsFirst4.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst4"]);
            txtDependantsSurName4.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName4"]);
            txtDependants4DOB.Text = GetTextBoxDateStringVersion(customerListItem, "Dependants4");
            //calDependants4.SelectedDate = (DateTime)customerListItem["Dependants4"];
            ddRelation4.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation4"]);
            txtDependantsEducationOccupationLevel4.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel4"]);
            txtDependantsSchoolUniversity4.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity4"]);
            SetCheckBoxValues(cbDependantsHealth4, customerListItem["DependantsHealth4"]);

            txtDependantsFirst5.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst5"]);
            txtDependantsSurName5.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName5"]);
            txtDependants5DOB.Text = GetTextBoxDateStringVersion(customerListItem, "Dependants5");
            //calDependants5.SelectedDate = (DateTime)customerListItem["Dependants5"];
            ddRelation5.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation5"]);
            txtDependantsEducationOccupationLevel5.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel5"]);
            txtDependantsSchoolUniversity5.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity5"]);
            SetCheckBoxValues(cbDependantsHealth5, customerListItem["DependantsHealth5"]);

            //tab6 Occupation
            txtJobTitle.Text = GetEmptyStringIfNull(customerListItem["JobTitle"]);
            ddEmploymentStatus.SelectedValue = GetEmptyStringIfNull(customerListItem["EmploymentStatus"]);
            txtEmployer.Text = GetEmptyStringIfNull(customerListItem["Employer"]);
            txtHoursPerWeek.Text = GetEmptyStringIfNull(customerListItem["HoursPerWeek"]);
            txtQualifications.Text = GetEmptyStringIfNull(customerListItem["Qualifications"]);
            txtRemuneration.Text = GetEmptyStringIfNull(customerListItem["Remuneration"]);
            txtDuties.Text = GetEmptyStringIfNull(customerListItem["Duties"]);

            txtJobTitleP.Text = GetEmptyStringIfNull(customerListItem["JobTitleP"]);
            ddEmploymentStatusP.SelectedValue = GetEmptyStringIfNull(customerListItem["EmploymentStatusP"]);
            txtEmployerP.Text = GetEmptyStringIfNull(customerListItem["EmployerP"]);
            txtHoursPerWeekP.Text = GetEmptyStringIfNull(customerListItem["HoursPerWeekP"]);
            txtQualificationsP.Text = GetEmptyStringIfNull(customerListItem["QualificationsP"]);
            txtRemunerationP.Text = GetEmptyStringIfNull(customerListItem["RemunerationP"]);
            txtDutiesP.Text = GetEmptyStringIfNull(customerListItem["DutiesP"]);

            //tab7 Health
            SetCheckBoxValues(cbHealthCondition, customerListItem["HealthCondition"]);
            SetCheckBoxValues(cbHealthConditionP, customerListItem["HealthConditionP"]);

            //tab8 Income
            txtHouse.Text = GetEmptyStringIfNull(customerListItem["House"]);
            txtContents.Text = GetEmptyStringIfNull(customerListItem["Contents"]);
            txtSuper.Text = GetEmptyStringIfNull(customerListItem["Super"]);
            txtCash.Text = GetEmptyStringIfNull(customerListItem["Cash"]);
            txtShares.Text = GetEmptyStringIfNull(customerListItem["Shares"]);
            txtInvestmentProperties.Text = GetEmptyStringIfNull(customerListItem["InvestmentProperties"]);
            txtBusinessValue.Text = GetEmptyStringIfNull(customerListItem["BusinessValue"]);
            txtPotentialInheritance.Text = GetEmptyStringIfNull(customerListItem["PotentialInheritance"]);
            txtMortgage.Text = GetEmptyStringIfNull(customerListItem["Mortgage"]);
            txtPersonalLoans.Text = GetEmptyStringIfNull(customerListItem["PersonalLoans"]);
            txtCreditCardDebt.Text = GetEmptyStringIfNull(customerListItem["CreditCardDebt"]);
            txtInvestmentLoans.Text = GetEmptyStringIfNull(customerListItem["InvestmentLoans"]);
            txtLeases.Text = GetEmptyStringIfNull(customerListItem["Leases"]);
            txtBusinessDebt.Text = GetEmptyStringIfNull(customerListItem["BusinessDebt"]);
            txtLiabilites.Text = GetEmptyStringIfNull(customerListItem["Liabilites"]);

            txtHouseP.Text = GetEmptyStringIfNull(customerListItem["HouseP"]);
            txtContentsP.Text = GetEmptyStringIfNull(customerListItem["ContentsP"]);
            txtSuperP.Text = GetEmptyStringIfNull(customerListItem["SuperP"]);
            txtCashP.Text = GetEmptyStringIfNull(customerListItem["CashP"]);
            txtSharesP.Text = GetEmptyStringIfNull(customerListItem["SharesP"]);
            txtInvestmentPropertiesP.Text = GetEmptyStringIfNull(customerListItem["InvestmentPropertiesP"]);
            txtBusinessValueP.Text = GetEmptyStringIfNull(customerListItem["BusinessValueP"]);
            txtPotentialInheritanceP.Text = GetEmptyStringIfNull(customerListItem["PotentialInheritanceP"]);
            txtMortgageP.Text = GetEmptyStringIfNull(customerListItem["MortgageP"]);
            txtPersonalLoansP.Text = GetEmptyStringIfNull(customerListItem["PersonalLoansP"]);
            txtCreditCardDebtP.Text = GetEmptyStringIfNull(customerListItem["CreditCardDebtP"]);
            txtInvestmentLoansP.Text = GetEmptyStringIfNull(customerListItem["InvestmentLoansP"]);
            txtLeasesP.Text = GetEmptyStringIfNull(customerListItem["LeasesP"]);
            txtBusinessDebtP.Text = GetEmptyStringIfNull(customerListItem["BusinessDebtP"]);
            txtLiabilitesP.Text = GetEmptyStringIfNull(customerListItem["LiabilitesP"]);

            txtHouseJ.Text = GetEmptyStringIfNull(customerListItem["HouseJ"]);
            txtContentsJ.Text = GetEmptyStringIfNull(customerListItem["ContentsJ"]);
            txtSuperJ.Text = GetEmptyStringIfNull(customerListItem["SuperJ"]);
            txtCashJ.Text = GetEmptyStringIfNull(customerListItem["CashJ"]);
            txtSharesJ.Text = GetEmptyStringIfNull(customerListItem["SharesJ"]);
            txtInvestmentPropertiesJ.Text = GetEmptyStringIfNull(customerListItem["InvestmentPropertiesJ"]);
            txtBusinessValueJ.Text = GetEmptyStringIfNull(customerListItem["BusinessValueJ"]);
            txtPotentialInheritanceJ.Text = GetEmptyStringIfNull(customerListItem["PotentialInheritanceJ"]);
            txtMortgageJ.Text = GetEmptyStringIfNull(customerListItem["MortgageJ"]);
            txtPersonalLoansJ.Text = GetEmptyStringIfNull(customerListItem["PersonalLoansJ"]);
            txtCreditCardDebtJ.Text = GetEmptyStringIfNull(customerListItem["CreditCardDebtJ"]);
            txtInvestmentLoansJ.Text = GetEmptyStringIfNull(customerListItem["InvestmentLoansJ"]);
            txtLeasesJ.Text = GetEmptyStringIfNull(customerListItem["LeasesJ"]);
            txtBusinessDebtJ.Text = GetEmptyStringIfNull(customerListItem["BusinessDebtJ"]);
            txtLiabilitesJ.Text = GetEmptyStringIfNull(customerListItem["LiabilitesJ"]);

            ddIncome.SelectedValue = GetEmptyStringIfNull(customerListItem["Income"]);

            txtClientIncome.Text = GetEmptyStringIfNull(customerListItem["ClientIncome"]);
            txtPartnerIncome.Text = GetEmptyStringIfNull(customerListItem["PartnerIncome"]);

            //tab9 - Insurance
            txtIncomeProtection.Text = GetEmptyStringIfNull(customerListItem["IncomeProtection"]);
            txtLifeCover.Text = GetEmptyStringIfNull(customerListItem["LifeCover"]);
            txtDisable.Text = GetEmptyStringIfNull(customerListItem["Disable"]);
            txtTrauma.Text = GetEmptyStringIfNull(customerListItem["Trauma"]);
            txtOtherInsured.Text = GetEmptyStringIfNull(customerListItem["OtherInsured"]);

            txtIncomeProtectionP.Text = GetEmptyStringIfNull(customerListItem["IncomeProtectionP"]);
            txtLifeCoverP.Text = GetEmptyStringIfNull(customerListItem["LifeCoverP"]);
            txtDisableP.Text = GetEmptyStringIfNull(customerListItem["DisableP"]);
            txtTraumaP.Text = GetEmptyStringIfNull(customerListItem["TraumaP"]);
            txtOtherInsuredP.Text = GetEmptyStringIfNull(customerListItem["OtherInsuredP"]);

            txtIncomeProtectionC.Text = GetEmptyStringIfNull(customerListItem["IncomeProtectionC"]);
            txtLifeCoverC.Text = GetEmptyStringIfNull(customerListItem["LifeCoverC"]);
            txtDisableC.Text = GetEmptyStringIfNull(customerListItem["DisableC"]);
            txtTraumaC.Text = GetEmptyStringIfNull(customerListItem["TraumaC"]);
            txtOtherInsuredC.Text = GetEmptyStringIfNull(customerListItem["OtherInsuredC"]);

            Logging.WriteToLog(SPContext.Current, "loaded");
        }

        private void DisableControlsForVersion()
        {
            lbDateCaptured.Enabled = false;
            txtReferrer.Enabled = false;

            //Tab1
            txtLastName.Enabled = false;

            ddNewBusinessRegister.Enabled = false;
            ddUnderWritingRegister.Enabled = false;

            txtFirstName.Enabled = false;

            txtDOB.Enabled = false;
            //calDOB.Enabled = false;
            ddGender.Enabled = false;

            ddMarital.Enabled = false;
            txtResidential.Enabled = false;
            txtBusiness.Enabled = false;
            txtHome.Enabled = false;
            txtMobile.Enabled = false;

            txtEmail.Enabled = false;
            ddCorrespondancePreference.Enabled = false;

            cbWill.Enabled = false;
            txtQantas.Enabled = false;
            txtRACV.Enabled = false;
            txtSolicitor.Enabled = false;
            txtAccountant.Enabled = false;
            ddSmoker.Enabled = false;

            txtSurNameP.Enabled = false;
            txtFirstNameP.Enabled = false;

            txtDOBP.Enabled = false;
            //calDOBP.Enabled = false;
            ddGenderP.Enabled = false;

            ddMaritalP.Enabled = false;
            txtResidentialP.Enabled = false;

            txtBusinessP.Enabled = false;
            txtHomeP.Enabled = false;
            txtMobileP.Enabled = false;

            txtEmailP.Enabled = false;
            ddCorrespondencePreferenceP.Enabled = false;

            cbWillP.Enabled = false;
            txtQantasP.Enabled = false;
            txtRACVP.Enabled = false;
            txtSolicitorP.Enabled = false;
            txtAccountantP.Enabled = false;
            ddSmokerP.Enabled = false;


            //tab3
            txtFather.Enabled = false;
            txtFatherDOB.Enabled = false;
            //calFather.Enabled = false;
            cbFatherHealth.Enabled = false;

            txtMother.Enabled = false;
            txtMotherDOB.Enabled = false;
            //calMother.Enabled = false;
            cbMotherHealth.Enabled = false;

            txtBrother.Enabled = false;
            txtBrotherDOB.Enabled = false;
            //calBrother.Enabled = false;
            cbBrotherHealth.Enabled = false;

            txtBrother2.Enabled = false;
            txtBrother2DOB.Enabled = false;
            //calBrother2.Enabled = false;
            cbBrotherHealth2.Enabled = false;

            txtSister.Enabled = false;
            txtSisterDOB.Enabled = false;
            //calSister.Enabled = false;
            cbSisterHealth.Enabled = false;

            txtSister2.Enabled = false;
            txtSister2DOB.Enabled = false;
            //calSister2.Enabled = false;
            cbSisterHealth2.Enabled = false;

            txtFatherP.Enabled = false;
            txtFatherPDOB.Enabled = false;
            //calFatherP.Enabled = false;
            cbFatherHealthP.Enabled = false;

            txtMotherP.Enabled = false;
            txtMotherPDOB.Enabled = false;
            //calMotherP.Enabled = false;
            cbMotherHealthP.Enabled = false;

            txtBrotherP.Enabled = false;
            txtBrotherPDOB.Enabled = false;
            //calBrotherP.Enabled = false;
            cbBrotherHealthP.Enabled = false;

            txtBrother2P.Enabled = false;
            txtBrother2PDOB.Enabled = false;
            //calBrother2P.Enabled = false;
            cbBrotherHealth2P.Enabled = false;

            txtSisterP.Enabled = false;
            txtSisterPDOB.Enabled = false;
            //calSisterP.Enabled = false;
            cbSisterHealthP.Enabled = false;

            txtSister2P.Enabled = false;
            txtSister2PDOB.Enabled = false;
            //calSister2P.Enabled = false;
            cbSisterHealth2P.Enabled = false;

            //tab5 dependant details
            txtDependantsFirst1.Enabled = false;
            txtDependantsSurName1.Enabled = false;
            txtDependants1DOB.Enabled = false;
            //calDependants1.Enabled = false;
            ddRelation1.Enabled = false;
            txtDependantsEducationOccupationLevel1.Enabled = false;
            txtDependantsSchoolUniversity1.Enabled = false;
            cbDependantsHealth1.Enabled = false;

            txtDependantsFirst2.Enabled = false;
            txtDependantsSurName2.Enabled = false;
            txtDependants2DOB.Enabled = false;
            //calDependants2.Enabled = false;
            ddRelation2.Enabled = false;
            txtDependantsEducationOccupationLevel2.Enabled = false;
            txtDependantsSchoolUniversity2.Enabled = false;
            cbDependantsHealth2.Enabled = false;

            txtDependantsFirst3.Enabled = false;
            txtDependantsSurName3.Enabled = false;
            txtDependants3DOB.Enabled = false;
            //calDependants3.Enabled = false;
            ddRelation3.Enabled = false;
            txtDependantsEducationOccupationLevel3.Enabled = false;
            txtDependantsSchoolUniversity3.Enabled = false;
            cbDependantsHealth3.Enabled = false;

            txtDependantsFirst4.Enabled = false;
            txtDependantsSurName4.Enabled = false;
            txtDependants4DOB.Enabled = false;
            //calDependants4.Enabled = false;
            ddRelation4.Enabled = false;
            txtDependantsEducationOccupationLevel4.Enabled = false;
            txtDependantsSchoolUniversity4.Enabled = false;
            cbDependantsHealth4.Enabled = false;

            txtDependantsFirst5.Enabled = false;
            txtDependantsSurName5.Enabled = false;
            txtDependants5DOB.Enabled = false;
            //calDependants5.Enabled = false;
            ddRelation5.Enabled = false;
            txtDependantsEducationOccupationLevel5.Enabled = false;
            txtDependantsSchoolUniversity5.Enabled = false;
            cbDependantsHealth5.Enabled = false;

            //tab6 Occupation
            txtJobTitle.Enabled = false;
            ddEmploymentStatus.Enabled = false;
            txtEmployer.Enabled = false;
            txtHoursPerWeek.Enabled = false;
            txtQualifications.Enabled = false;
            txtRemuneration.Enabled = false;
            txtDuties.Enabled = false;

            txtJobTitleP.Enabled = false;
            ddEmploymentStatusP.Enabled = false;
            txtEmployerP.Enabled = false;
            txtHoursPerWeekP.Enabled = false;
            txtQualificationsP.Enabled = false;
            txtRemunerationP.Enabled = false;
            txtDutiesP.Enabled = false;

            //tab7 Health
            cbHealthCondition.Enabled = false;
            cbHealthConditionP.Enabled = false;

            //tab8 Income
            txtHouse.Enabled = false;
            txtContents.Enabled = false;
            txtSuper.Enabled = false;
            txtCash.Enabled = false;
            txtShares.Enabled = false;
            txtInvestmentProperties.Enabled = false;
            txtBusinessValue.Enabled = false;
            txtPotentialInheritance.Enabled = false;
            txtMortgage.Enabled = false;
            txtPersonalLoans.Enabled = false;
            txtCreditCardDebt.Enabled = false;
            txtInvestmentLoans.Enabled = false;
            txtLeases.Enabled = false;
            txtBusinessDebt.Enabled = false;
            txtLiabilites.Enabled = false;

            txtHouseP.Enabled = false;
            txtContentsP.Enabled = false;
            txtSuperP.Enabled = false;
            txtCashP.Enabled = false;
            txtSharesP.Enabled = false;
            txtInvestmentPropertiesP.Enabled = false;
            txtBusinessValueP.Enabled = false;
            txtPotentialInheritanceP.Enabled = false;
            txtMortgageP.Enabled = false;
            txtPersonalLoansP.Enabled = false;
            txtCreditCardDebtP.Enabled = false;
            txtInvestmentLoansP.Enabled = false;
            txtLeasesP.Enabled = false;
            txtBusinessDebtP.Enabled = false;
            txtLiabilitesP.Enabled = false;

            txtHouseJ.Enabled = false;
            txtContentsJ.Enabled = false;
            txtSuperJ.Enabled = false;
            txtCashJ.Enabled = false;
            txtSharesJ.Enabled = false;
            txtInvestmentPropertiesJ.Enabled = false;
            txtBusinessValueJ.Enabled = false;
            txtPotentialInheritanceJ.Enabled = false;
            txtMortgageJ.Enabled = false;
            txtPersonalLoansJ.Enabled = false;
            txtCreditCardDebtJ.Enabled = false;
            txtInvestmentLoansJ.Enabled = false;
            txtLeasesJ.Enabled = false;
            txtBusinessDebtJ.Enabled = false;
            txtLiabilitesJ.Enabled = false;

            ddIncome.Enabled = false;

            txtClientIncome.Enabled = false;
            txtPartnerIncome.Enabled = false;

            //tab9 - Insurance
            txtIncomeProtection.Enabled = false;
            txtLifeCover.Enabled = false;
            txtDisable.Enabled = false;
            txtTrauma.Enabled = false;
            txtOtherInsured.Enabled = false;

            txtIncomeProtectionP.Enabled = false;
            txtLifeCoverP.Enabled = false;
            txtDisableP.Enabled = false;
            txtTraumaP.Enabled = false;
            txtOtherInsuredP.Enabled = false;

            txtIncomeProtectionC.Enabled = false;
            txtLifeCoverC.Enabled = false;
            txtDisableC.Enabled = false;
            txtTraumaC.Enabled = false;
            txtOtherInsuredC.Enabled = false;
        }

        private int GetVersionedItem()
        {
            //if "VersionNo" in url then get that version
            string versionNoUrl = Page.Request.QueryString["VersionNo"];
            int versionNo = 0;

            if (!string.IsNullOrEmpty(versionNoUrl))
            {
                Int32.TryParse(versionNoUrl, out versionNo);
                return versionNo;
            }
            else
                return -1;
        }

        private string GetEmptyStringIfNull(object item)
        {
            if (null == item)
                return "";
            else
                return item.ToString();
        }

        private void SetCheckBoxValues(CheckBoxList checkBoxList, object item)
        {
            if (null == item)
                return;

            SPFieldMultiChoiceValue typedValue = new SPFieldMultiChoiceValue(item.ToString());
            for (int i = 0; i < typedValue.Count; i++)
            {
                checkBoxList.Items.FindByText(typedValue[i]).Selected = true;
            }
        }

        private void SetDates()
        {
            txtDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDOBP.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtFatherDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtMotherDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtBrotherDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtBrother2DOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtSisterDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtSister2DOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtFatherPDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtMotherPDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtBrotherPDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtBrother2PDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtBrother2PDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtSisterPDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtSister2PDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDependants1DOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDependants2DOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDependants3DOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDependants4DOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDependants5DOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            //calDOB.SelectedDate = DateTime.Now.Date;
            //calDOBP.SelectedDate = DateTime.Now.Date;
            //calBrother.SelectedDate = DateTime.Now.Date;
            //calBrother2.SelectedDate = DateTime.Now.Date;
            //calBrotherP.SelectedDate = DateTime.Now.Date;
            //calBrother2P.SelectedDate = DateTime.Now.Date;
            //calDependants1.SelectedDate = DateTime.Now.Date;
            //calDependants2.SelectedDate = DateTime.Now.Date;
            //calDependants2.SelectedDate = DateTime.Now.Date;
            //calDependants3.SelectedDate = DateTime.Now.Date;
            //calDependants4.SelectedDate = DateTime.Now.Date;
            //calDependants5.SelectedDate = DateTime.Now.Date;
            //calFather.SelectedDate = DateTime.Now.Date;
            //calFatherP.SelectedDate = DateTime.Now.Date;
            //calMother.SelectedDate = DateTime.Now.Date;
            //calMotherP.SelectedDate = DateTime.Now.Date;
            //calSister.SelectedDate = DateTime.Now.Date;
            //calSister2.SelectedDate = DateTime.Now.Date;
            //calSister2P.SelectedDate = DateTime.Now.Date;
            //calSisterP.SelectedDate = DateTime.Now.Date;
        }

        protected void wizNeeds_NextButtonClick(object sender, System.Web.UI.WebControls.WizardNavigationEventArgs e)
        {
            bool canMoveToNextStep = false;

            EnablePartner(IsPartnerActive());

            //use moveto to skip

            switch (wizNeeds.ActiveStepIndex)
            {
                case WizardTabsClient0: //0
                    canMoveToNextStep = PersonalValidation();

                    if (IsPartnerActive())
                        wizNeeds.ActiveStepIndex = WizardTabsPartner1; //go to partner
                    else
                        wizNeeds.ActiveStepIndex = WizardTabsClientFamily2; //go to family

                    break;
                case WizardTabsPartner1: //1
                    canMoveToNextStep = PartnerValidation();

                    break;

                case WizardTabsClientFamily2:
                    canMoveToNextStep = ClientFamilyDetailsValidation();

                    if (IsPartnerActive())
                        wizNeeds.ActiveStepIndex = WizardTabsPartnerFamily3; //go to partner
                    else
                        wizNeeds.ActiveStepIndex = WizardTabsOccupation4; //always goes here

                    break;

                case WizardTabsPartnerFamily3:
                    canMoveToNextStep = PartnerFamilyDetailsValidation();
                    break;

                case WizardTabsOccupation4:
                    canMoveToNextStep = OccupationValidation();
                    break;

                case WizardTabsHealth5:
                    canMoveToNextStep = HealthValidation();
                    break;

                case WizardTabsFinance6:
                    canMoveToNextStep = FinanceValidation();
                    break;

                case WizardTabsInsurance7:
                    canMoveToNextStep = InsuranceValidation();
                    break;

                default:
                    Console.WriteLine("error");
                    break;
            }

            if (!canMoveToNextStep)
            {
                e.Cancel = true;
                return;
            }
        }

        private void EnablePartner(bool partnerActive)
        {
            //Tab2 - WizardTabsPartner1
            txtSurNameP.Enabled = partnerActive;
            txtFirstNameP.Enabled = partnerActive;

            txtDOBP.Enabled = partnerActive;
            //calDOBP.Enabled = partnerActive;
            ddGenderP.Enabled = partnerActive;

            ddMaritalP.Enabled = partnerActive;
            txtResidentialP.Enabled = partnerActive;
            txtBusinessP.Enabled = partnerActive;
            txtMobileP.Enabled = partnerActive;
            txtHomeP.Enabled = partnerActive;
            txtEmailP.Enabled = partnerActive;
            ddCorrespondencePreferenceP.Enabled = partnerActive;
            cbWillP.Enabled = partnerActive;
            txtQantasP.Enabled = partnerActive;
            txtRACVP.Enabled = partnerActive;
            txtSolicitorP.Enabled = partnerActive;
            txtAccountantP.Enabled = partnerActive;
            ddSmokerP.Enabled = partnerActive;

            //RequiredFieldValidator11.Enabled = partnerActive;
            //RequiredFieldValidator12.Enabled = partnerActive;
            //RequiredFieldValidator21.Enabled = partnerActive;
            //RequiredFieldValidator14.Enabled = partnerActive;
            //RequiredFieldValidator15.Enabled = partnerActive;

            //RequiredFieldValidator19.Enabled = partnerActive;

            //RequiredFieldValidator16.Enabled = partnerActive;
            //RequiredFieldValidator17.Enabled = partnerActive;
            //RequiredFieldValidator18.Enabled = partnerActive;
            //RequiredFieldValidator20.Enabled = partnerActive;

            //Partner family details
            txtFatherP.Enabled = partnerActive;
            txtFatherPDOB.Enabled = partnerActive;
            //calFatherP.Enabled = partnerActive;
            cbFatherHealthP.Enabled = partnerActive;

            txtMotherP.Enabled = partnerActive;
            txtMotherPDOB.Enabled = partnerActive;
            //calMotherP.Enabled = partnerActive;
            cbMotherHealthP.Enabled = partnerActive;

            txtBrotherP.Enabled = partnerActive;
            txtBrotherPDOB.Enabled = partnerActive;
            //calBrotherP.Enabled = partnerActive;
            cbBrotherHealthP.Enabled = partnerActive;

            txtBrother2P.Enabled = partnerActive;
            txtBrother2PDOB.Enabled = partnerActive;
            //calBrother2P.Enabled = partnerActive;
            cbBrotherHealth2P.Enabled = partnerActive;

            txtSisterP.Enabled = partnerActive;
            txtSisterPDOB.Enabled = partnerActive;
            //calSisterP.Enabled = partnerActive;
            cbSisterHealthP.Enabled = partnerActive;

            txtSister2P.Enabled = partnerActive;
            txtSister2PDOB.Enabled = partnerActive;
            //calSister2P.Enabled = partnerActive;
            cbSisterHealth2P.Enabled = partnerActive;


            //Occupation
            txtJobTitleP.Enabled = partnerActive;
            ddEmploymentStatusP.Enabled = partnerActive;
            txtEmployerP.Enabled = partnerActive;
            txtHoursPerWeekP.Enabled = partnerActive;
            txtQualificationsP.Enabled = partnerActive;
            txtRemunerationP.Enabled = partnerActive;
            txtDutiesP.Enabled = partnerActive;

            //health
            cbHealthConditionP.Enabled = partnerActive;

            txtHouseJ.Enabled = partnerActive;
            txtContentsJ.Enabled = partnerActive;
            txtSuperJ.Enabled = partnerActive;
            txtCashJ.Enabled = partnerActive;
            txtSharesJ.Enabled = partnerActive;
            txtInvestmentPropertiesJ.Enabled = partnerActive;
            txtBusinessValueJ.Enabled = partnerActive;
            txtPotentialInheritanceJ.Enabled = partnerActive;
            txtMortgageJ.Enabled = partnerActive;
            txtPersonalLoansJ.Enabled = partnerActive;
            txtCreditCardDebtJ.Enabled = partnerActive;
            txtInvestmentLoansJ.Enabled = partnerActive;
            txtLeasesJ.Enabled = partnerActive;
            txtBusinessDebtJ.Enabled = partnerActive;
            txtLiabilitesJ.Enabled = partnerActive;


            //insured
            txtIncomeProtectionP.Enabled = partnerActive;
            txtLifeCoverP.Enabled = partnerActive;
            txtDisableP.Enabled = partnerActive;
            txtTraumaP.Enabled = partnerActive;
            txtOtherInsuredP.Enabled = partnerActive;
        }

        private bool IsPartnerActive()
        {
            VersionNumber = GetVersionedItem();

            if (VersionNumber >= 0)
                return false;   //do not enable if a version

            if (ddMarital.SelectedValue == "Single" || ddMarital.SelectedValue == "")
                return false;
            else
                return true;
        }

        private bool InsuranceValidation()
        {
            return true;
        }

        private bool FinanceValidation()
        {
            return true;
        }

        private bool HealthValidation()
        {
            return true;
        }

        private bool OccupationValidation()
        {
            return true;
        }

        private bool PartnerFamilyDetailsValidation()
        {
            return true;
        }

        private bool ClientFamilyDetailsValidation()
        {
            return true;
        }

        private bool PartnerValidation()
        {
            return true;
        }

        private bool PersonalValidation()
        {
            return true;
        }

        protected void wizNeeds_FinishButtonClick(object sender, System.Web.UI.WebControls.WizardNavigationEventArgs e)
        {
            VersionNumber = GetVersionedItem();

            if (VersionNumber == -1)
                SaveCustomer();


            //Detect if Modal
            //can view versions in Modal
            string isDlg = Page.Request.QueryString["IsDlg"];
            if (!String.IsNullOrEmpty(isDlg) && isDlg == "1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "SP.UI.ModalDialog.commonModalDialogClose(1, 1);", true);
            }
            else
            {
                Literal litControl = new Literal();
                phForLiteral.Controls.Add(litControl);

                litControl.Text = "<script type='text/javascript'> window.location ='/Lists/" + CustomerList + "'; </script>";
            }
        }

        private void SaveCustomer()
        {
            try
            {
                SPContext.Current.Web.AllowUnsafeUpdates = true;

                SPList listCustomer = SPContext.Current.Web.Lists[CustomerList];
                ListItemId = GetItemId();

                SPListItem item = GetItemAsNewOrUpdate(listCustomer, ListItemId);

                if (item.DisplayName == "New Item" || FieldsHaveChanged(item))
                {

                    ModifyNewBusinessRegister(txtLastName.Text, txtFirstName.Text, Convert.ToDateTime(lbDateCaptured.Text));
                    ModifyUnderWritingRegister(txtLastName.Text, txtFirstName.Text);

                    //top
                    item["DateCaptured"] = Convert.ToDateTime(lbDateCaptured.Text, australiaCulture);
                    item["Referrer"] = txtReferrer.Text;

                    //Tab1
                    item["Title"] = txtLastName.Text;

                    item["NewBusinessRegister"] = ddNewBusinessRegister.SelectedValue;
                    item["UnderwritingRegister"] = ddUnderWritingRegister.SelectedValue;

                    item["FirstName"] = txtFirstName.Text;

                    item["DoB"] = Convert.ToDateTime(txtDOB.Text, australiaCulture).Date;  //calDOB.SelectedDate.Date;
                    item["Gender"] = ddGender.SelectedValue;

                    item["MaritalStatus"] = ddMarital.SelectedValue;
                    item["Residential"] = txtResidential.Text;
                    item["Business"] = txtBusiness.Text;
                    item["Home"] = txtHome.Text;
                    item["Mobile"] = txtMobile.Text;

                    item["Email"] = txtEmail.Text;
                    item["CorrespondancePreference"] = ddCorrespondancePreference.SelectedValue;
                    item["Will"] = cbWill.Checked;
                    item["Qantas"] = txtQantas.Text;
                    item["RACV"] = txtRACV.Text;
                    item["Solicitor"] = txtSolicitor.Text;
                    item["Accountant"] = txtAccountant.Text;
                    item["Smoker"] = ddSmoker.SelectedValue;

                    //Tab2
                    if (IsPartnerActive())
                    {
                        item["SurNameP"] = txtSurNameP.Text;
                        item["FirstNameP"] = txtFirstNameP.Text;

                        item["DoBP"] = Convert.ToDateTime(txtDOBP.Text, australiaCulture).Date; //calDOBP.SelectedDate.Date;
                        item["GenderP"] = ddGenderP.SelectedValue;

                        item["MaritalStatusP"] = ddMaritalP.SelectedValue;
                        item["ResidentialP"] = txtResidentialP.Text;
                        item["BusinessP"] = txtBusinessP.Text;
                        item["HomeP"] = txtHomeP.Text;
                        item["MobileP"] = txtMobileP.Text;

                        item["EmailP"] = txtEmailP.Text;
                        item["CorrespondancePreferenceP"] = ddCorrespondencePreferenceP.SelectedValue;

                        item["WillP"] = cbWillP.Checked;
                        item["QantasP"] = txtQantasP.Text;
                        item["RACVP"] = txtRACVP.Text;
                        item["SolicitorP"] = txtSolicitorP.Text;
                        item["AccountantP"] = txtAccountantP.Text;
                        item["SmokerP"] = ddSmokerP.SelectedValue;
                    }

                    //tab3
                    item["Father"] = txtFather.Text;
                    item["FatherDOB"] = Convert.ToDateTime(txtFatherDOB.Text, australiaCulture).Date;
                    item["FatherHealth"] = GetSPListItemsFromCheckBoxes(cbFatherHealth);

                    item["Mother"] = txtMother.Text;
                    item["MotherDOB"] = Convert.ToDateTime(txtMotherDOB.Text, australiaCulture).Date; //calMother.SelectedDate.Date;
                    item["MotherHealth"] = GetSPListItemsFromCheckBoxes(cbMotherHealth);

                    item["Brother"] = txtBrother.Text;
                    item["BrotherDOB"] = Convert.ToDateTime(txtBrotherDOB.Text, australiaCulture).Date; //calBrother.SelectedDate.Date;
                    item["BrotherHealth"] = GetSPListItemsFromCheckBoxes(cbBrotherHealth);

                    item["Brother2"] = txtBrother2.Text;
                    item["Brother2DOB"] = Convert.ToDateTime(txtBrother2DOB.Text, australiaCulture).Date; //calBrother2.SelectedDate.Date;
                    item["BrotherHealth2"] = GetSPListItemsFromCheckBoxes(cbBrotherHealth2);

                    item["Sister"] = txtSister.Text;
                    item["SisterDOB"] = Convert.ToDateTime(txtSisterDOB.Text, australiaCulture).Date; //calSister.SelectedDate.Date;
                    item["SisterHealth"] = GetSPListItemsFromCheckBoxes(cbSisterHealth);

                    item["Sister2"] = txtSister2.Text;
                    item["SisterDOB2"] = Convert.ToDateTime(txtSister2DOB.Text, australiaCulture).Date; //calSister2.SelectedDate.Date;
                    item["SisterHealth2"] = GetSPListItemsFromCheckBoxes(cbSisterHealth2);

                    //tab4
                    if (IsPartnerActive())
                    {
                        item["FatherP"] = txtFatherP.Text;
                        item["FatherPDOB"] = Convert.ToDateTime(txtFatherPDOB.Text, australiaCulture).Date; //calFatherP.SelectedDate.Date;
                        item["FatherHealthP"] = GetSPListItemsFromCheckBoxes(cbFatherHealthP);

                        item["MotherP"] = txtMotherP.Text;
                        item["MotherPDOB"] = Convert.ToDateTime(txtMotherPDOB.Text, australiaCulture).Date; //calMotherP.SelectedDate.Date;
                        item["MotherHealthP"] = GetSPListItemsFromCheckBoxes(cbMotherHealthP);

                        item["BrotherP"] = txtBrotherP.Text;
                        item["BrotherPDOB"] = Convert.ToDateTime(txtBrotherPDOB.Text, australiaCulture).Date; //calBrotherP.SelectedDate.Date;
                        item["BrotherHealthP"] = GetSPListItemsFromCheckBoxes(cbBrotherHealthP);

                        item["Brother2P"] = txtBrother2P.Text;
                        item["Brother2PDOB"] = Convert.ToDateTime(txtBrother2PDOB.Text, australiaCulture).Date; //calBrother2P.SelectedDate.Date;
                        item["BrotherHealth2P"] = GetSPListItemsFromCheckBoxes(cbBrotherHealth2P);

                        item["SisterP"] = txtSisterP.Text;
                        item["SisterPDOB"] = Convert.ToDateTime(txtSisterPDOB.Text, australiaCulture); //calSisterP.SelectedDate.Date;
                        item["SisterHealthP"] = GetSPListItemsFromCheckBoxes(cbSisterHealthP);

                        item["Sister2P"] = txtSister2P.Text;
                        item["Sister2PDOB"] = Convert.ToDateTime(txtSister2PDOB.Text, australiaCulture).Date; //calSister2P.SelectedDate.Date;
                        item["SisterHealth2P"] = GetSPListItemsFromCheckBoxes(cbSisterHealth2P);
                    }

                    //tab5 dependant details
                    item["DependantsFirst1"] = txtDependantsFirst1.Text;
                    item["DependantsSurName1"] = txtDependantsSurName1.Text;
                    item["Dependants1"] = Convert.ToDateTime(txtDependants1DOB.Text, australiaCulture).Date; //calDependants1.SelectedDate.Date;
                    item["Relation1"] = ddRelation1.SelectedValue;
                    item["DependantsEducationOccupationLevel1"] = txtDependantsEducationOccupationLevel1.Text;
                    item["DependantsSchoolUniversity1"] = txtDependantsSchoolUniversity1.Text;
                    item["DependantsHealth1"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth1);

                    item["DependantsFirst2"] = txtDependantsFirst2.Text;
                    item["DependantsSurName2"] = txtDependantsSurName2.Text;
                    item["Dependants2"] = Convert.ToDateTime(txtDependants2DOB.Text, australiaCulture).Date; //calDependants2.SelectedDate.Date;
                    item["Relation2"] = ddRelation2.SelectedValue;
                    item["DependantsEducationOccupationLevel2"] = txtDependantsEducationOccupationLevel2.Text;
                    item["DependantsSchoolUniversity2"] = txtDependantsSchoolUniversity2.Text;
                    item["DependantsHealth2"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth2);

                    item["DependantsFirst3"] = txtDependantsFirst3.Text;
                    item["DependantsSurName3"] = txtDependantsSurName3.Text;
                    item["Dependants3"] = Convert.ToDateTime(txtDependants3DOB.Text, australiaCulture).Date; //calDependants3.SelectedDate.Date;
                    item["Relation3"] = ddRelation3.SelectedValue;
                    item["DependantsEducationOccupationLevel3"] = txtDependantsEducationOccupationLevel3.Text;
                    item["DependantsSchoolUniversity3"] = txtDependantsSchoolUniversity3.Text;
                    item["DependantsHealth3"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth3);

                    item["DependantsFirst4"] = txtDependantsFirst4.Text;
                    item["DependantsSurName4"] = txtDependantsSurName4.Text;
                    item["Dependants4"] = Convert.ToDateTime(txtDependants4DOB.Text, australiaCulture).Date; //calDependants4.SelectedDate.Date;
                    item["Relation4"] = ddRelation4.SelectedValue;
                    item["DependantsEducationOccupationLevel4"] = txtDependantsEducationOccupationLevel4.Text;
                    item["DependantsSchoolUniversity4"] = txtDependantsSchoolUniversity4.Text;
                    item["DependantsHealth4"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth4);

                    item["DependantsFirst5"] = txtDependantsFirst5.Text;
                    item["DependantsSurName5"] = txtDependantsSurName5.Text;
                    item["Dependants5"] = Convert.ToDateTime(txtDependants5DOB.Text, australiaCulture).Date; //calDependants5.SelectedDate.Date;
                    item["Relation5"] = ddRelation5.SelectedValue;
                    item["DependantsEducationOccupationLevel5"] = txtDependantsEducationOccupationLevel5.Text;
                    item["DependantsSchoolUniversity5"] = txtDependantsSchoolUniversity5.Text;
                    item["DependantsHealth5"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth5);

                    //tab6 Occupation
                    item["JobTitle"] = txtJobTitle.Text;
                    item["EmploymentStatus"] = ddEmploymentStatus.SelectedValue;
                    item["Employer"] = txtEmployer.Text;
                    item["HoursPerWeek"] = txtHoursPerWeek.Text;
                    item["Qualifications"] = txtQualifications.Text;
                    item["Remuneration"] = txtRemuneration.Text;
                    item["Duties"] = txtDuties.Text;

                    item["JobTitleP"] = txtJobTitleP.Text;
                    item["EmploymentStatusP"] = ddEmploymentStatusP.SelectedValue;
                    item["EmployerP"] = txtEmployerP.Text;
                    item["HoursPerWeekP"] = txtHoursPerWeekP.Text;
                    item["QualificationsP"] = txtQualificationsP.Text;
                    item["RemunerationP"] = txtRemunerationP.Text;
                    item["DutiesP"] = txtDutiesP.Text;

                    //tab7 Health
                    item["HealthCondition"] = GetSPListItemsFromCheckBoxes(cbHealthCondition);
                    item["HealthConditionP"] = GetSPListItemsFromCheckBoxes(cbHealthConditionP);

                    //tab8 Income
                    item["House"] = txtHouse.Text;
                    item["Contents"] = txtContents.Text;
                    item["Super"] = txtSuper.Text;
                    item["Cash"] = txtCash.Text;
                    item["Shares"] = txtShares.Text;
                    item["InvestmentProperties"] = txtInvestmentProperties.Text;
                    item["BusinessValue"] = txtBusinessValue.Text;
                    item["PotentialInheritance"] = txtPotentialInheritance.Text;
                    item["Mortgage"] = txtMortgage.Text;
                    item["PersonalLoans"] = txtPersonalLoans.Text;
                    item["CreditCardDebt"] = txtCreditCardDebt.Text;
                    item["InvestmentLoans"] = txtInvestmentLoans.Text;
                    item["Leases"] = txtLeases.Text;
                    item["BusinessDebt"] = txtBusinessDebt.Text;
                    item["Liabilites"] = txtLiabilites.Text;

                    item["HouseP"] = txtHouseP.Text;
                    item["ContentsP"] = txtContentsP.Text;
                    item["SuperP"] = txtSuperP.Text;
                    item["CashP"] = txtCashP.Text;
                    item["SharesP"] = txtSharesP.Text;
                    item["InvestmentPropertiesP"] = txtInvestmentPropertiesP.Text;
                    item["BusinessValueP"] = txtBusinessValueP.Text;
                    item["PotentialInheritanceP"] = txtPotentialInheritanceP.Text;
                    item["MortgageP"] = txtMortgageP.Text;
                    item["PersonalLoansP"] = txtPersonalLoansP.Text;
                    item["CreditCardDebtP"] = txtCreditCardDebtP.Text;
                    item["InvestmentLoansP"] = txtInvestmentLoansP.Text;
                    item["LeasesP"] = txtLeasesP.Text;
                    item["BusinessDebtP"] = txtBusinessDebtP.Text;
                    item["LiabilitesP"] = txtLiabilitesP.Text;

                    item["HouseJ"] = txtHouseJ.Text;
                    item["ContentsJ"] = txtContentsJ.Text;
                    item["SuperJ"] = txtSuperJ.Text;
                    item["CashJ"] = txtCashJ.Text;
                    item["SharesJ"] = txtSharesJ.Text;
                    item["InvestmentPropertiesJ"] = txtInvestmentPropertiesJ.Text;
                    item["BusinessValueJ"] = txtBusinessValueJ.Text;
                    item["PotentialInheritanceJ"] = txtPotentialInheritanceJ.Text;
                    item["MortgageJ"] = txtMortgageJ.Text;
                    item["PersonalLoansJ"] = txtPersonalLoansJ.Text;
                    item["CreditCardDebtJ"] = txtCreditCardDebtJ.Text;
                    item["InvestmentLoansJ"] = txtInvestmentLoansJ.Text;
                    item["LeasesJ"] = txtLeasesJ.Text;
                    item["BusinessDebtJ"] = txtBusinessDebtJ.Text;
                    item["LiabilitesJ"] = txtLiabilitesJ.Text;

                    item["Income"] = ddIncome.SelectedValue;

                    item["ClientIncome"] = txtClientIncome.Text;
                    item["PartnerIncome"] = txtPartnerIncome.Text;

                    //tab9 - Insurance
                    item["IncomeProtection"] = txtIncomeProtection.Text;
                    item["LifeCover"] = txtLifeCover.Text;
                    item["Disable"] = txtDisable.Text;
                    item["Trauma"] = txtTrauma.Text;
                    item["OtherInsured"] = txtOtherInsured.Text;

                    item["IncomeProtectionP"] = txtIncomeProtectionP.Text;
                    item["LifeCoverP"] = txtLifeCoverP.Text;
                    item["DisableP"] = txtDisableP.Text;
                    item["TraumaP"] = txtTraumaP.Text;
                    item["OtherInsuredP"] = txtOtherInsuredP.Text;

                    item["IncomeProtectionC"] = txtIncomeProtectionC.Text;
                    item["LifeCoverC"] = txtLifeCoverC.Text;
                    item["DisableC"] = txtDisableC.Text;
                    item["TraumaC"] = txtTraumaC.Text;
                    item["OtherInsuredC"] = txtOtherInsuredC.Text;

                    Logging.WriteToLog(SPContext.Current, "starting");

                    item.Update();
                    Logging.WriteToLog(SPContext.Current, "done");
                }

                Literal litControl = new Literal();
                phForLiteral.Controls.Add(litControl);

                litControl.Text = "<script type='text/javascript'> window.location ='/Lists/" + CustomerList + "'; </script>";
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = ex.Message;

                Logging.WriteToLog(SPContext.Current, ex);
            }
            finally
            {
                SPContext.Current.Web.AllowUnsafeUpdates = false;
            }
        }

        private bool FieldsHaveChanged(SPListItem item)
        {
            if (((DateTime)item["DateCaptured"]).ToString("dd/MM/yyyy") != lbDateCaptured.Text)
                return true;

            if (GetEmptyStringIfNull(item["Referrer"]) != txtReferrer.Text)
                return true;

            //Tab1
            if (GetEmptyStringIfNull(item["Title"]) != txtLastName.Text)
                return true;

            if (GetEmptyStringIfNull(item["NewBusinessRegister"]) != ddNewBusinessRegister.SelectedValue)
                return true;

            if (GetEmptyStringIfNull(item["UnderwritingRegister"]) != ddUnderWritingRegister.SelectedValue)
                return true;

            if (GetEmptyStringIfNull(item["FirstName"]) != txtFirstName.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["DoB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtDOB.Text)
                //if (Convert.ToDateTime(GetEmptyStringIfNull(item["DoB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != calDOB.SelectedDate.ToString("dd/MM/yyyy"))
                return true;

            if (GetEmptyStringIfNull(item["Gender"]) != ddGender.SelectedValue)
                return true;

            if (GetEmptyStringIfNull(item["MaritalStatus"]) != ddMarital.SelectedValue)
                return true;
            if (GetEmptyStringIfNull(item["Residential"]) != txtResidential.Text)
                return true;
            if (GetEmptyStringIfNull(item["Business"]) != txtBusiness.Text)
                return true;
            if (GetEmptyStringIfNull(item["Home"]) != txtHome.Text)
                return true;
            if (GetEmptyStringIfNull(item["Mobile"]) != txtMobile.Text)
                return true;

            if (GetEmptyStringIfNull(item["Email"]) != txtEmail.Text)
                return true;
            if (GetEmptyStringIfNull(item["CorrespondancePreference"]) != ddCorrespondancePreference.SelectedValue)
                return true;
            if ((bool)item["Will"] != cbWill.Checked)
                return true;
            if (GetEmptyStringIfNull(item["Qantas"]) != txtQantas.Text)
                return true;
            if (GetEmptyStringIfNull(item["RACV"]) != txtRACV.Text)
                return true;
            if (GetEmptyStringIfNull(item["Solicitor"]) != txtSolicitor.Text)
                return true;
            if (GetEmptyStringIfNull(item["Accountant"]) != txtAccountant.Text)
                return true;
            if (GetEmptyStringIfNull(item["Smoker"]) != ddSmoker.SelectedValue)
                return true;


            //Tab2
            if (IsPartnerActive())
            {
                if (GetEmptyStringIfNull(item["SurNameP"]) != txtSurNameP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["FirstNameP"]) != txtFirstNameP.Text)
                    return true;

                if (Convert.ToDateTime(GetEmptyStringIfNull(item["DoBP"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtDOBP.Text)
                    //if (Convert.ToDateTime(GetEmptyStringIfNull(item["DoBP"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != calDOBP.SelectedDate.ToString("dd/MM/yyyy"))
                    return true;

                if (GetEmptyStringIfNull(item["GenderP"]) != ddGenderP.SelectedValue)
                    return true;

                if (GetEmptyStringIfNull(item["MaritalStatusP"]) != ddMaritalP.SelectedValue)
                    return true;
                if (GetEmptyStringIfNull(item["ResidentialP"]) != txtResidentialP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["BusinessP"]) != txtBusinessP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["HomeP"]) != txtHomeP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["MobileP"]) != txtMobileP.Text)
                    return true;

                if (GetEmptyStringIfNull(item["EmailP"]) != txtEmailP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["CorrespondancePreferenceP"]) != ddCorrespondencePreferenceP.SelectedValue)
                    return true;

                if ((bool)item["WillP"] != cbWillP.Checked)
                    return true;
                if (GetEmptyStringIfNull(item["QantasP"]) != txtQantasP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["RACVP"]) != txtRACVP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["SolicitorP"]) != txtSolicitorP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["AccountantP"]) != txtAccountantP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["SmokerP"]) != ddSmokerP.SelectedValue)
                    return true;
            }

            //tab3
            if (GetEmptyStringIfNull(item["Father"]) != txtFather.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["FatherDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtFatherDOB.Text)
                //if (Convert.ToDateTime(GetEmptyStringIfNull(item["FatherDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != calFather.SelectedDate.ToString("dd/MM/yyyy"))
                return true;

            if (GetEmptyStringIfNull(item["FatherHealth"]) != GetSPListItemsFromCheckBoxes(cbFatherHealth).ToString())
                return true;

            if (GetEmptyStringIfNull(item["Mother"]) != txtMother.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["MotherDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtMotherDOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["MotherHealth"]) != GetSPListItemsFromCheckBoxes(cbMotherHealth).ToString())
                return true;

            if (GetEmptyStringIfNull(item["Brother"]) != txtBrother.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["BrotherDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtBrotherDOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["BrotherHealth"]) != GetSPListItemsFromCheckBoxes(cbBrotherHealth).ToString())
                return true;

            if (GetEmptyStringIfNull(item["Brother2"]) != txtBrother2.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["Brother2DOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtBrother2DOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["BrotherHealth2"]) != GetSPListItemsFromCheckBoxes(cbBrotherHealth2).ToString())
                return true;

            if (GetEmptyStringIfNull(item["Sister"]) != txtSister.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["SisterDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtSisterDOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["SisterHealth"]) != GetSPListItemsFromCheckBoxes(cbSisterHealth).ToString())
                return true;

            if (GetEmptyStringIfNull(item["Sister2"]) != txtSister2.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["SisterDOB2"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtSister2DOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["SisterHealth2"]) != GetSPListItemsFromCheckBoxes(cbSisterHealth2).ToString())
                return true;

            if (IsPartnerActive())
            {
                if (GetEmptyStringIfNull(item["FatherP"]) != txtFatherP.Text)
                    return true;


                if (Convert.ToDateTime(GetEmptyStringIfNull(item["FatherPDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtFatherPDOB.Text)
                    return true;

                if (GetEmptyStringIfNull(item["FatherHealthP"]) != GetSPListItemsFromCheckBoxes(cbFatherHealthP).ToString())
                    return true;

                if (GetEmptyStringIfNull(item["MotherP"]) != txtMotherP.Text)
                    return true;

                if (Convert.ToDateTime(GetEmptyStringIfNull(item["MotherPDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtMotherPDOB.Text)
                    return true;

                if (GetEmptyStringIfNull(item["MotherHealthP"]) != GetSPListItemsFromCheckBoxes(cbMotherHealthP).ToString())
                    return true;

                if (GetEmptyStringIfNull(item["BrotherP"]) != txtBrotherP.Text)
                    return true;

                if (Convert.ToDateTime(GetEmptyStringIfNull(item["BrotherPDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtBrotherPDOB.Text)
                    return true;

                if (GetEmptyStringIfNull(item["BrotherHealthP"]) != GetSPListItemsFromCheckBoxes(cbBrotherHealthP).ToString())
                    return true;

                if (GetEmptyStringIfNull(item["Brother2P"]) != txtBrother2P.Text)
                    return true;

                if (Convert.ToDateTime(GetEmptyStringIfNull(item["Brother2PDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtBrother2PDOB.Text)
                    return true;

                if (GetEmptyStringIfNull(item["BrotherHealth2P"]) != GetSPListItemsFromCheckBoxes(cbBrotherHealth2P).ToString())
                    return true;

                if (GetEmptyStringIfNull(item["SisterP"]) != txtSisterP.Text)
                    return true;

                if (Convert.ToDateTime(GetEmptyStringIfNull(item["SisterPDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtSisterPDOB.Text)
                    return true;

                if (GetEmptyStringIfNull(item["SisterHealthP"]) != GetSPListItemsFromCheckBoxes(cbSisterHealthP).ToString())
                    return true;

                if (GetEmptyStringIfNull(item["Sister2P"]) != txtSister2P.Text)
                    return true;

                if (Convert.ToDateTime(GetEmptyStringIfNull(item["Sister2PDOB"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtSister2PDOB.Text)
                    return true;

                if (GetEmptyStringIfNull(item["SisterHealth2P"]) != GetSPListItemsFromCheckBoxes(cbSisterHealth2P).ToString())
                    return true;
            }

            //tab5 dependant details
            if (GetEmptyStringIfNull(item["DependantsFirst1"]) != txtDependantsFirst1.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSurName1"]) != txtDependantsSurName1.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["Dependants1"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtDependants1DOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["Relation1"]) != ddRelation1.SelectedValue)
                return true;
            if (GetEmptyStringIfNull(item["DependantsEducationOccupationLevel1"]) != txtDependantsEducationOccupationLevel1.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSchoolUniversity1"]) != txtDependantsSchoolUniversity1.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsHealth1"]) != GetSPListItemsFromCheckBoxes(cbDependantsHealth1).ToString())
                return true;

            if (GetEmptyStringIfNull(item["DependantsFirst2"]) != txtDependantsFirst2.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSurName2"]) != txtDependantsSurName2.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["Dependants2"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtDependants2DOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["Relation2"]) != ddRelation2.SelectedValue)
                return true;
            if (GetEmptyStringIfNull(item["DependantsEducationOccupationLevel2"]) != txtDependantsEducationOccupationLevel2.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSchoolUniversity2"]) != txtDependantsSchoolUniversity2.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsHealth2"]) != GetSPListItemsFromCheckBoxes(cbDependantsHealth2).ToString())
                return true;

            if (GetEmptyStringIfNull(item["DependantsFirst3"]) != txtDependantsFirst3.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSurName3"]) != txtDependantsSurName3.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["Dependants3"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtDependants3DOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["Relation3"]) != ddRelation3.SelectedValue)
                return true;
            if (GetEmptyStringIfNull(item["DependantsEducationOccupationLevel3"]) != txtDependantsEducationOccupationLevel3.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSchoolUniversity3"]) != txtDependantsSchoolUniversity3.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsHealth3"]) != GetSPListItemsFromCheckBoxes(cbDependantsHealth3).ToString())
                return true;

            if (GetEmptyStringIfNull(item["DependantsFirst4"]) != txtDependantsFirst4.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSurName4"]) != txtDependantsSurName4.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["Dependants4"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtDependants4DOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["Relation4"]) != ddRelation4.SelectedValue)
                return true;
            if (GetEmptyStringIfNull(item["DependantsEducationOccupationLevel4"]) != txtDependantsEducationOccupationLevel4.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSchoolUniversity4"]) != txtDependantsSchoolUniversity4.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsHealth4"]) != GetSPListItemsFromCheckBoxes(cbDependantsHealth4).ToString())
                return true;

            if (GetEmptyStringIfNull(item["DependantsFirst5"]) != txtDependantsFirst5.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSurName5"]) != txtDependantsSurName5.Text)
                return true;

            if (Convert.ToDateTime(GetEmptyStringIfNull(item["Dependants5"]).ToString(), australiaCulture).ToString("dd/MM/yyyy") != txtDependants5DOB.Text)
                return true;

            if (GetEmptyStringIfNull(item["Relation5"]) != ddRelation5.SelectedValue)
                return true;
            if (GetEmptyStringIfNull(item["DependantsEducationOccupationLevel5"]) != txtDependantsEducationOccupationLevel5.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsSchoolUniversity5"]) != txtDependantsSchoolUniversity5.Text)
                return true;
            if (GetEmptyStringIfNull(item["DependantsHealth5"]) != GetSPListItemsFromCheckBoxes(cbDependantsHealth5).ToString())
                return true;

            //tab6 Occupation
            if (GetEmptyStringIfNull(item["JobTitle"]) != txtJobTitle.Text)
                return true;
            if (GetEmptyStringIfNull(item["EmploymentStatus"]) != ddEmploymentStatus.SelectedValue)
                return true;
            if (GetEmptyStringIfNull(item["Employer"]) != txtEmployer.Text)
                return true;
            if (GetEmptyStringIfNull(item["HoursPerWeek"]) != txtHoursPerWeek.Text)
                return true;
            if (GetEmptyStringIfNull(item["Qualifications"]) != txtQualifications.Text)
                return true;
            if (GetEmptyStringIfNull(item["Remuneration"]) != txtRemuneration.Text)
                return true;
            if (GetEmptyStringIfNull(item["Duties"]) != txtDuties.Text)
                return true;

            if (IsPartnerActive())
            {
                if (GetEmptyStringIfNull(item["JobTitleP"]) != txtJobTitleP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["EmploymentStatusP"]) != ddEmploymentStatusP.SelectedValue)
                    return true;
                if (GetEmptyStringIfNull(item["EmployerP"]) != txtEmployerP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["HoursPerWeekP"]) != txtHoursPerWeekP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["QualificationsP"]) != txtQualificationsP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["RemunerationP"]) != txtRemunerationP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["DutiesP"]) != txtDutiesP.Text)
                    return true;
            }


            //tab7 Health
            if (GetEmptyStringIfNull(item["HealthCondition"]) != GetSPListItemsFromCheckBoxes(cbHealthCondition).ToString())
                return true;
            if (IsPartnerActive())
            {
                if (GetEmptyStringIfNull(item["HealthConditionP"]) != GetSPListItemsFromCheckBoxes(cbHealthConditionP).ToString())
                    return true;
            }

            //tab8 Income
            if (GetEmptyStringIfNull(item["House"]) != txtHouse.Text)
                return true;
            if (GetEmptyStringIfNull(item["Contents"]) != txtContents.Text)
                return true;
            if (GetEmptyStringIfNull(item["Super"]) != txtSuper.Text)
                return true;
            if (GetEmptyStringIfNull(item["Cash"]) != txtCash.Text)
                return true;
            if (GetEmptyStringIfNull(item["Shares"]) != txtShares.Text)
                return true;
            if (GetEmptyStringIfNull(item["InvestmentProperties"]) != txtInvestmentProperties.Text)
                return true;
            if (GetEmptyStringIfNull(item["BusinessValue"]) != txtBusinessValue.Text)
                return true;
            if (GetEmptyStringIfNull(item["PotentialInheritance"]) != txtPotentialInheritance.Text)
                return true;
            if (GetEmptyStringIfNull(item["Mortgage"]) != txtMortgage.Text)
                return true;
            if (GetEmptyStringIfNull(item["PersonalLoans"]) != txtPersonalLoans.Text)
                return true;
            if (GetEmptyStringIfNull(item["CreditCardDebt"]) != txtCreditCardDebt.Text)
                return true;
            if (GetEmptyStringIfNull(item["InvestmentLoans"]) != txtInvestmentLoans.Text)
                return true;
            if (GetEmptyStringIfNull(item["Leases"]) != txtLeases.Text)
                return true;
            if (GetEmptyStringIfNull(item["BusinessDebt"]) != txtBusinessDebt.Text)
                return true;
            if (GetEmptyStringIfNull(item["Liabilites"]) != txtLiabilites.Text)
                return true;

            if (GetEmptyStringIfNull(item["HouseP"]) != txtHouseP.Text)
                return true;
            if (GetEmptyStringIfNull(item["ContentsP"]) != txtContentsP.Text)
                return true;
            if (GetEmptyStringIfNull(item["SuperP"]) != txtSuperP.Text)
                return true;
            if (GetEmptyStringIfNull(item["CashP"]) != txtCashP.Text)
                return true;
            if (GetEmptyStringIfNull(item["SharesP"]) != txtSharesP.Text)
                return true;
            if (GetEmptyStringIfNull(item["InvestmentPropertiesP"]) != txtInvestmentPropertiesP.Text)
                return true;
            if (GetEmptyStringIfNull(item["BusinessValueP"]) != txtBusinessValueP.Text)
                return true;
            if (GetEmptyStringIfNull(item["PotentialInheritanceP"]) != txtPotentialInheritanceP.Text)
                return true;
            if (GetEmptyStringIfNull(item["MortgageP"]) != txtMortgageP.Text)
                return true;
            if (GetEmptyStringIfNull(item["PersonalLoansP"]) != txtPersonalLoansP.Text)
                return true;
            if (GetEmptyStringIfNull(item["CreditCardDebtP"]) != txtCreditCardDebtP.Text)
                return true;
            if (GetEmptyStringIfNull(item["InvestmentLoansP"]) != txtInvestmentLoansP.Text)
                return true;
            if (GetEmptyStringIfNull(item["LeasesP"]) != txtLeasesP.Text)
                return true;
            if (GetEmptyStringIfNull(item["BusinessDebtP"]) != txtBusinessDebtP.Text)
                return true;
            if (GetEmptyStringIfNull(item["LiabilitesP"]) != txtLiabilitesP.Text)
                return true;

            if (GetEmptyStringIfNull(item["HouseJ"]) != txtHouseJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["ContentsJ"]) != txtContentsJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["SuperJ"]) != txtSuperJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["CashJ"]) != txtCashJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["SharesJ"]) != txtSharesJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["InvestmentPropertiesJ"]) != txtInvestmentPropertiesJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["BusinessValueJ"]) != txtBusinessValueJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["PotentialInheritanceJ"]) != txtPotentialInheritanceJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["MortgageJ"]) != txtMortgageJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["PersonalLoansJ"]) != txtPersonalLoansJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["CreditCardDebtJ"]) != txtCreditCardDebtJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["InvestmentLoansJ"]) != txtInvestmentLoansJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["LeasesJ"]) != txtLeasesJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["BusinessDebtJ"]) != txtBusinessDebtJ.Text)
                return true;
            if (GetEmptyStringIfNull(item["LiabilitesJ"]) != txtLiabilitesJ.Text)
                return true;

            if (GetEmptyStringIfNull(item["Income"]) != ddIncome.SelectedValue)
                return true;

            if (GetEmptyStringIfNull(item["ClientIncome"]) != txtClientIncome.Text)
                return true;
            if (GetEmptyStringIfNull(item["PartnerIncome"]) != txtPartnerIncome.Text)
                return true;

            //tab9 - Insurance
            if (GetEmptyStringIfNull(item["IncomeProtection"]) != txtIncomeProtection.Text)
                return true;
            if (GetEmptyStringIfNull(item["LifeCover"]) != txtLifeCover.Text)
                return true;
            if (GetEmptyStringIfNull(item["Disable"]) != txtDisable.Text)
                return true;
            if (GetEmptyStringIfNull(item["Trauma"]) != txtTrauma.Text)
                return true;
            if (GetEmptyStringIfNull(item["OtherInsured"]) != txtOtherInsured.Text)
                return true;

            if (IsPartnerActive())
            {
                if (GetEmptyStringIfNull(item["IncomeProtectionP"]) != txtIncomeProtectionP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["LifeCoverP"]) != txtLifeCoverP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["DisableP"]) != txtDisableP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["TraumaP"]) != txtTraumaP.Text)
                    return true;
                if (GetEmptyStringIfNull(item["OtherInsuredP"]) != txtOtherInsuredP.Text)
                    return true;
            }

            if (GetEmptyStringIfNull(item["IncomeProtectionC"]) != txtIncomeProtectionC.Text)
                return true;
            if (GetEmptyStringIfNull(item["LifeCoverC"]) != txtLifeCoverC.Text)
                return true;
            if (GetEmptyStringIfNull(item["DisableC"]) != txtDisableC.Text)
                return true;
            if (GetEmptyStringIfNull(item["TraumaC"]) != txtTraumaC.Text)
                return true;
            if (GetEmptyStringIfNull(item["OtherInsuredC"]) != txtOtherInsuredC.Text)
                return true;

            return false;
        }

        private void ModifyNewBusinessRegister(string customerSurname, string customerFirstname, DateTime NeedsAnalysisDate)
        {
            try
            {
                string ClientName = customerFirstname + " " + customerSurname;

                SPQuery myQuery = new SPQuery();

                myQuery.Query = "<Where><Eq><FieldRef Name='Client'></FieldRef><Value Type='Text'>" + ClientName + "</Value></Eq></Where>";

                SPListItemCollection newBusinessRegisterSPListItemCollection = SPContext.Current.Web.Lists[NewBusinessRegisterList].GetItems(myQuery);

                if (newBusinessRegisterSPListItemCollection.Count > 0) //updates
                {
                    if (ddNewBusinessRegister.SelectedValue == "Complete" || ddNewBusinessRegister.SelectedValue == "Inactive")
                    {
                        SPListItem updateItem = newBusinessRegisterSPListItemCollection[0];
                        updateItem["Status"] = "Yes";

                        updateItem.Update();
                    }
                    else
                    {
                        if (ddNewBusinessRegister.SelectedValue == "In Progress")
                        {
                            SPListItem updateItem = newBusinessRegisterSPListItemCollection[0];
                            updateItem["Status"] = "No";

                            updateItem.Update();
                        }
                    }
                }
                else //add new record
                {
                    if (ddNewBusinessRegister.SelectedValue == "In Progress")
                    {
                        SPListItem newBusinessRegister = OptimizedAddItem(SPContext.Current.Web.Lists[NewBusinessRegisterList]);

                        newBusinessRegister["Client"] = ClientName;
                        newBusinessRegister["Need_x0020_Analysis"] = NeedsAnalysisDate;
                        newBusinessRegister["Status"] = "No";

                        newBusinessRegister.Update();
                    }
                    if (ddNewBusinessRegister.SelectedValue == "Complete")
                    {
                        SPListItem newBusinessRegister = OptimizedAddItem(SPContext.Current.Web.Lists[NewBusinessRegisterList]);

                        newBusinessRegister["Client"] = ClientName;
                        newBusinessRegister["Need_x0020_Analysis"] = NeedsAnalysisDate;
                        newBusinessRegister["Status"] = "Yes";

                        newBusinessRegister.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = ex.Message;

                Logging.WriteToLog(SPContext.Current, ex);
            }
            finally
            {
                SPContext.Current.Web.AllowUnsafeUpdates = false;
            }
        }

        private void ModifyUnderWritingRegister(string customerSurname, string customerFirstname)
        {
            try
            {
                string ClientName = customerFirstname + " " + customerSurname;

                SPQuery myQuery = new SPQuery();

                myQuery.Query = "<Where><Eq><FieldRef Name='Client'></FieldRef><Value Type='Text'>" + ClientName + "</Value></Eq></Where>";

                SPListItemCollection underWritingRegisterSPListItemCollection = SPContext.Current.Web.Lists[UnderwritingRegisterList].GetItems(myQuery);

                if (underWritingRegisterSPListItemCollection.Count > 0)
                {
                    if (ddUnderWritingRegister.SelectedValue == "Complete" || ddUnderWritingRegister.SelectedValue == "Not Started")
                    {
                        SPListItem updateItem = underWritingRegisterSPListItemCollection[0];
                        updateItem["Status"] = "Yes";

                        updateItem.Update();
                    }
                    else
                        if (ddUnderWritingRegister.SelectedValue == "In Progress")
                        {
                            SPListItem updateItem = underWritingRegisterSPListItemCollection[0];
                            updateItem["Status"] = "No";

                            updateItem.Update();
                        }
                }
                else
                {
                    if (ddUnderWritingRegister.SelectedValue == "In Progress")
                    {
                        //add new record
                        SPListItem newUnderWritingRegister = OptimizedAddItem(SPContext.Current.Web.Lists[UnderwritingRegisterList]);

                        newUnderWritingRegister["Client"] = ClientName;
                        newUnderWritingRegister["Status"] = "No";

                        newUnderWritingRegister.Update();
                    }
                    if (ddUnderWritingRegister.SelectedValue == "Complete")
                    {
                        //add new record
                        SPListItem newUnderWritingRegister = OptimizedAddItem(SPContext.Current.Web.Lists[UnderwritingRegisterList]);

                        newUnderWritingRegister["Client"] = ClientName;
                        newUnderWritingRegister["Status"] = "Yes";

                        newUnderWritingRegister.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = ex.Message;

                Logging.WriteToLog(SPContext.Current, ex);
            }
            finally
            {
                SPContext.Current.Web.AllowUnsafeUpdates = false;
            }
        }

        private SPListItem GetItemAsNewOrUpdate(SPList listCustomer, int listItemId)
        {
            if (listItemId == 0)
                return OptimizedAddItem(listCustomer);
            else
                return SPContext.Current.Web.Lists[CustomerList].GetItemByIdAllFields(ListItemId);
        }

        private static SPFieldMultiChoiceValue CreateMultiValue(string input)
        {
            SPFieldMultiChoiceValue values = new SPFieldMultiChoiceValue();
            values.Add(input);

            return values;
        }

        private static SPFieldMultiChoiceValue GetSPListItemsFromCheckBoxes(CheckBoxList inputCheckBoxList)
        {
            SPFieldMultiChoiceValue values = new SPFieldMultiChoiceValue();

            foreach (ListItem boxItem in inputCheckBoxList.Items)
                if (boxItem.Selected == true)
                    values.Add(boxItem.Value);

            return values;
        }

        public static SPListItem OptimizedAddItem(SPList list)
        {
            const string EmptyQuery = "0";
            SPQuery q = new SPQuery { Query = EmptyQuery };
            return list.GetItems(q).Add();
        }

        private DateTime CheckForEmpty(DateTime input)
        {
            if (null == input)
                return System.DateTime.Now;
            else
                return input;
        }

        private string GetTextBoxDateString(SPListItem item, string column)
        {
            string textBox = "";

            textBox = Convert.ToDateTime(GetEmptyStringIfNull(item[column]).ToString(), australiaCulture).ToLocalTime().Date.ToString().Replace(" 12:00:00 AM", "");
            textBox = (textBox.Substring(1, 1) != "/") ? textBox : "0" + textBox; //zero pad

            return textBox;
        }

        private string GetTextBoxDateStringVersion(SPListItemVersion item, string column)
        {
            string textBox = "";

            textBox = Convert.ToDateTime(GetEmptyStringIfNull(item[column]).ToString(), australiaCulture).ToLocalTime().Date.ToString().Replace(" 12:00:00 AM", "");
            textBox = (textBox.Substring(1, 1) != "/") ? textBox : "0" + textBox; //zero pad

            return textBox;
        }

    }
}



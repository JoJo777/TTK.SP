using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Web.UI.WebControls;
using System.Globalization;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

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

        static string CustomerList = "Customer2";
        int ListItemId = 0;

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

                if (ListItemId > 0)
                    LoadCustomerData(ListItemId);
                else
                    ddCustomerStatus.SelectedValue = "In progress"; ///default for new.
            }

            EnablePartner(IsPartnerActive());

            lbDateCaptured.Text = DateTime.Now.ToString("dd-MM-yyyy");

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

        private void LoadCustomerData(int ListcustomerListItemId)
        {
            SPListItem customerListItem = SPContext.Current.Web.Lists[CustomerList].GetItemByIdAllFields(ListItemId);

            Logging.WriteToLog(SPContext.Current, "starting load: ID=" + ListItemId);

            //assign to fields
            //top
            lbDateCaptured.Text = ((DateTime)customerListItem["DateCaptured"]).ToString("dd-MM-yyyy");
            txtReferrer.Text = GetEmptyStringIfNull(customerListItem["Referrer"]);

            //Tab1
            txtLastName.Text = customerListItem["Title"].ToString();

            ddCustomerStatus.SelectedValue = GetEmptyStringIfNull(customerListItem["CustomerStatus"]);

            txtFirstName.Text = GetEmptyStringIfNull(customerListItem["FirstName"]);

            calDOB.SelectedDate = (DateTime)customerListItem["DoB"];
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
            if (IsPartnerActive())
            {
                txtSurNameP.Text = GetEmptyStringIfNull(customerListItem["SurNameP"]);
                txtFirstNameP.Text = GetEmptyStringIfNull(customerListItem["FirstNameP"]);

                calDOBP.SelectedDate = (DateTime)customerListItem["DoBP"];
                ddGenderP.SelectedValue = GetEmptyStringIfNull(customerListItem["GenderP"].ToString());

                ddMaritalP.SelectedValue = customerListItem["MaritalStatusP"].ToString();
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
            }

            //tab3
            txtFather.Text = GetEmptyStringIfNull(customerListItem["Father"]);
            calFather.SelectedDate = (DateTime)customerListItem["FatherDOB"];
            SetCheckBoxValues(cbFatherHealth, customerListItem["FatherHealth"]);

            txtMother.Text = GetEmptyStringIfNull(customerListItem["Mother"]);
            calMother.SelectedDate = (DateTime)customerListItem["MotherDOB"];
            SetCheckBoxValues(cbMotherHealth, customerListItem["MotherHealth"]);

            txtBrother.Text = GetEmptyStringIfNull(customerListItem["Brother"]);
            calBrother.SelectedDate = (DateTime)customerListItem["BrotherDOB"];
            SetCheckBoxValues(cbBrotherHealth, customerListItem["BrotherHealth"]);

            txtBrother2.Text = GetEmptyStringIfNull(customerListItem["Brother2"]);
            calBrother2.SelectedDate = (DateTime)customerListItem["Brother2DOB"];
            SetCheckBoxValues(cbBrotherHealth2, customerListItem["BrotherHealth2"]);

            txtSister.Text = GetEmptyStringIfNull(customerListItem["Sister"]);
            calSister.SelectedDate = (DateTime)customerListItem["SisterDOB"];
            SetCheckBoxValues(cbSisterHealth, customerListItem["SisterHealth"]);

            txtSister2.Text = GetEmptyStringIfNull(customerListItem["Sister2"]);
            calSister2.SelectedDate = (DateTime)customerListItem["SisterDOB2"];
            SetCheckBoxValues(cbSisterHealth2, customerListItem["SisterHealth2"]);

            //tab4
            if (IsPartnerActive())
            {
                txtFatherP.Text = GetEmptyStringIfNull(customerListItem["FatherP"]);
                calFatherP.SelectedDate = (DateTime)customerListItem["FatherPDOB"];
                SetCheckBoxValues(cbFatherHealthP, customerListItem["FatherHealthP"]);

                txtMotherP.Text = GetEmptyStringIfNull(customerListItem["MotherP"]);
                calMotherP.SelectedDate = (DateTime)customerListItem["MotherPDOB"];
                SetCheckBoxValues(cbMotherHealthP, customerListItem["MotherHealthP"]);

                txtBrotherP.Text = GetEmptyStringIfNull(customerListItem["BrotherP"]);
                calBrotherP.SelectedDate = (DateTime)customerListItem["BrotherPDOB"];
                SetCheckBoxValues(cbBrotherHealthP, customerListItem["BrotherHealthP"]);

                txtBrother2P.Text = GetEmptyStringIfNull(customerListItem["Brother2P"]);
                calBrother2P.SelectedDate = (DateTime)customerListItem["Brother2PDOB"];
                SetCheckBoxValues(cbBrotherHealth2P, customerListItem["BrotherHealth2P"]);

                txtSisterP.Text = GetEmptyStringIfNull(customerListItem["SisterP"]);
                calSisterP.SelectedDate = (DateTime)customerListItem["SisterPDOB"];
                SetCheckBoxValues(cbSisterHealthP, customerListItem["SisterHealthP"]);

                txtSister2P.Text = GetEmptyStringIfNull(customerListItem["Sister2P"]);
                calSister2P.SelectedDate = (DateTime)customerListItem["Sister2PDOB"];
                SetCheckBoxValues(cbSisterHealth2P, customerListItem["SisterHealth2P"]);
            }

            //tab5 dependant details
            txtDependantsFirst1.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst1"]);
            txtDependantsSurName1.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName1"]);
            calDependants1.SelectedDate = (DateTime)customerListItem["Dependants1"];
            ddRelation1.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation1"]);
            txtDependantsEducationOccupationLevel1.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel1"]);
            txtDependantsSchoolUniversity1.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity1"]);
            SetCheckBoxValues(cbDependantsHealth1, customerListItem["DependantsHealth1"]);

            txtDependantsFirst2.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst2"]);
            txtDependantsSurName2.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName2"]);
            calDependants2.SelectedDate = (DateTime)customerListItem["Dependants2"];
            ddRelation2.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation2"]);
            txtDependantsEducationOccupationLevel2.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel2"]);
            txtDependantsSchoolUniversity2.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity2"]);
            SetCheckBoxValues(cbDependantsHealth2, customerListItem["DependantsHealth2"]);

            txtDependantsFirst3.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst3"]);
            txtDependantsSurName3.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName3"]);
            calDependants3.SelectedDate = (DateTime)customerListItem["Dependants3"];
            ddRelation3.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation3"]);
            txtDependantsEducationOccupationLevel3.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel3"]);
            txtDependantsSchoolUniversity3.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity3"]);
            SetCheckBoxValues(cbDependantsHealth3, customerListItem["DependantsHealth3"]);

            txtDependantsFirst4.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst4"]);
            txtDependantsSurName4.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName4"]);
            calDependants4.SelectedDate = (DateTime)customerListItem["Dependants4"];
            ddRelation4.SelectedValue = GetEmptyStringIfNull(customerListItem["Relation4"]);
            txtDependantsEducationOccupationLevel4.Text = GetEmptyStringIfNull(customerListItem["DependantsEducationOccupationLevel4"]);
            txtDependantsSchoolUniversity4.Text = GetEmptyStringIfNull(customerListItem["DependantsSchoolUniversity4"]);
            SetCheckBoxValues(cbDependantsHealth4, customerListItem["DependantsHealth4"]);

            txtDependantsFirst5.Text = GetEmptyStringIfNull(customerListItem["DependantsFirst5"]);
            txtDependantsSurName5.Text = GetEmptyStringIfNull(customerListItem["DependantsSurName5"]);
            calDependants5.SelectedDate = (DateTime)customerListItem["Dependants5"];
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

        private string GetEmptyStringIfNull(object item)
        {
            if (null == item)
                return "";
            else
                return item.ToString();


            //return string.IsNullOrEmpty(item.ToString()) ? "" :item.ToString(); 
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
            calDOB.SelectedDate = DateTime.Now;
            calDOBP.SelectedDate = DateTime.Now;
            calBrother.SelectedDate = DateTime.Now;
            calBrother2.SelectedDate = DateTime.Now;
            calBrotherP.SelectedDate = DateTime.Now;
            calBrother2P.SelectedDate = DateTime.Now;
            calDependants1.SelectedDate = DateTime.Now;
            calDependants2.SelectedDate = DateTime.Now;
            calDependants2.SelectedDate = DateTime.Now;
            calDependants3.SelectedDate = DateTime.Now;
            calDependants4.SelectedDate = DateTime.Now;
            calDependants5.SelectedDate = DateTime.Now;
            calFather.SelectedDate = DateTime.Now;
            calFatherP.SelectedDate = DateTime.Now;
            calMother.SelectedDate = DateTime.Now;
            calMotherP.SelectedDate = DateTime.Now;
            calSister.SelectedDate = DateTime.Now;
            calSister2.SelectedDate = DateTime.Now;
            calSister2P.SelectedDate = DateTime.Now;
            calSisterP.SelectedDate = DateTime.Now;
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

            calDOBP.Enabled = partnerActive;
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

            RequiredFieldValidator11.Enabled = partnerActive;
            RequiredFieldValidator12.Enabled = partnerActive;
            RequiredFieldValidator21.Enabled = partnerActive;
            RequiredFieldValidator14.Enabled = partnerActive;
            RequiredFieldValidator15.Enabled = partnerActive;

            RequiredFieldValidator19.Enabled = partnerActive;

            RequiredFieldValidator16.Enabled = partnerActive;
            RequiredFieldValidator17.Enabled = partnerActive;
            RequiredFieldValidator18.Enabled = partnerActive;
            RequiredFieldValidator20.Enabled = partnerActive;

            //Partner family details
            txtFatherP.Enabled = partnerActive;
            calFatherP.Enabled = partnerActive;
            cbFatherHealthP.Enabled = partnerActive;

            txtMotherP.Enabled = partnerActive;
            calMotherP.Enabled = partnerActive;
            cbMotherHealthP.Enabled = partnerActive;

            txtBrotherP.Enabled = partnerActive;
            calBrotherP.Enabled = partnerActive;
            cbBrotherHealthP.Enabled = partnerActive;

            txtBrother2P.Enabled = partnerActive;
            calBrother2P.Enabled = partnerActive;
            cbBrotherHealth2P.Enabled = partnerActive;

            txtSisterP.Enabled = partnerActive;
            calSisterP.Enabled = partnerActive;
            cbSisterHealthP.Enabled = partnerActive;

            txtSister2P.Enabled = partnerActive;
            calSister2P.Enabled = partnerActive;
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

            //income - do not do - *might be a partner*
            //txtHouseP.Enabled = partnerActive;
            //txtContentsP.Enabled = partnerActive;
            //txtSuperP.Enabled = partnerActive;
            //txtCashP.Enabled = partnerActive;
            //txtSharesP.Enabled = partnerActive;
            //txtInvestmentPropertiesP.Enabled = partnerActive;
            //txtBusinessValueP.Enabled = partnerActive;
            //txtPotentialInheritanceP.Enabled = partnerActive;
            //txtMortgageP.Enabled = partnerActive;
            //txtPersonalLoansP.Enabled = partnerActive;
            //txtCreditCardDebtP.Enabled = partnerActive;
            //txtInvestmentLoansP.Enabled = partnerActive;
            //txtLeasesP.Enabled = partnerActive;
            //txtBusinessDebtP.Enabled = partnerActive;
            //txtLiabilitesP.Enabled = partnerActive;
            

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
            try
            {
                string NeedsAnalysisList = "Customer2";

                SPContext.Current.Web.AllowUnsafeUpdates = true;
                SPList listCustomer = SPContext.Current.Web.Lists[NeedsAnalysisList];
                ListItemId = GetItemId();

                SPListItem item = GetItemAsNewOrUpdate(listCustomer, ListItemId);

                //top
                item["DateCaptured"] = System.DateTime.Now;
                item["Referrer"] = txtReferrer.Text;

                //Tab1
                item["Title"] = txtLastName.Text;

                item["CustomerStatus"] = ddCustomerStatus.SelectedValue;
                
                item["FirstName"] = txtFirstName.Text;

                item["DoB"] = calDOB.SelectedDate;
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

                    item["DoBP"] = calDOBP.SelectedDate;
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
                item["FatherDOB"] = calFather.SelectedDate;
                item["FatherHealth"] = GetSPListItemsFromCheckBoxes(cbFatherHealth);

                item["Mother"] = txtMother.Text;
                item["MotherDOB"] = calMother.SelectedDate;
                item["MotherHealth"] = GetSPListItemsFromCheckBoxes(cbMotherHealth);

                item["Brother"] = txtBrother.Text;
                item["BrotherDOB"] = calBrother.SelectedDate;
                item["BrotherHealth"] = GetSPListItemsFromCheckBoxes(cbBrotherHealth);

                item["Brother2"] = txtBrother2.Text;
                item["Brother2DOB"] = calBrother2.SelectedDate;
                item["BrotherHealth2"] = GetSPListItemsFromCheckBoxes(cbBrotherHealth2);

                item["Sister"] = txtSister.Text;
                item["SisterDOB"] = calSister.SelectedDate;
                item["SisterHealth"] = GetSPListItemsFromCheckBoxes(cbSisterHealth);

                item["Sister2"] = txtSister2.Text;
                item["SisterDOB2"] = calSister2.SelectedDate;
                item["SisterHealth2"] = GetSPListItemsFromCheckBoxes(cbSisterHealth2);

                //tab4
                if (IsPartnerActive())
                {
                    item["FatherP"] = txtFatherP.Text;
                    item["FatherPDOB"] = calFatherP.SelectedDate;
                    item["FatherHealthP"] = GetSPListItemsFromCheckBoxes(cbFatherHealthP);

                    item["MotherP"] = txtMotherP.Text;
                    item["MotherPDOB"] = calMotherP.SelectedDate;
                    item["MotherHealthP"] = GetSPListItemsFromCheckBoxes(cbMotherHealthP);

                    item["BrotherP"] = txtBrotherP.Text;
                    item["BrotherPDOB"] = calBrotherP.SelectedDate;
                    item["BrotherHealthP"] = GetSPListItemsFromCheckBoxes(cbBrotherHealthP);

                    item["Brother2P"] = txtBrother2P.Text;
                    item["Brother2PDOB"] = calBrother2P.SelectedDate;
                    item["BrotherHealth2P"] = GetSPListItemsFromCheckBoxes(cbBrotherHealth2P);

                    item["SisterP"] = txtSisterP.Text;
                    item["SisterPDOB"] = calSisterP.SelectedDate;
                    item["SisterHealthP"] = GetSPListItemsFromCheckBoxes(cbSisterHealthP);

                    item["Sister2P"] = txtSister2P.Text;
                    item["Sister2PDOB"] = calSister2P.SelectedDate;
                    item["SisterHealth2P"] = GetSPListItemsFromCheckBoxes(cbSisterHealth2P);
                }

                //tab5 dependant details
                item["DependantsFirst1"] = txtDependantsFirst1.Text;
                item["DependantsSurName1"] = txtDependantsSurName1.Text;
                item["Dependants1"] = calDependants1.SelectedDate;
                item["Relation1"] = ddRelation1.SelectedValue;
                item["DependantsEducationOccupationLevel1"] = txtDependantsEducationOccupationLevel1.Text;
                item["DependantsSchoolUniversity1"] = txtDependantsSchoolUniversity1.Text;
                item["DependantsHealth1"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth1);

                item["DependantsFirst2"] = txtDependantsFirst2.Text;
                item["DependantsSurName2"] = txtDependantsSurName2.Text;
                item["Dependants2"] = calDependants2.SelectedDate;
                item["Relation2"] = ddRelation2.SelectedValue;
                item["DependantsEducationOccupationLevel2"] = txtDependantsEducationOccupationLevel2.Text;
                item["DependantsSchoolUniversity2"] = txtDependantsSchoolUniversity2.Text;
                item["DependantsHealth2"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth2);

                item["DependantsFirst3"] = txtDependantsFirst3.Text;
                item["DependantsSurName3"] = txtDependantsSurName3.Text;
                item["Dependants3"] = calDependants3.SelectedDate;
                item["Relation3"] = ddRelation3.SelectedValue;
                item["DependantsEducationOccupationLevel3"] = txtDependantsEducationOccupationLevel3.Text;
                item["DependantsSchoolUniversity3"] = txtDependantsSchoolUniversity3.Text;
                item["DependantsHealth3"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth3);

                item["DependantsFirst4"] = txtDependantsFirst4.Text;
                item["DependantsSurName4"] = txtDependantsSurName4.Text;
                item["Dependants4"] = calDependants4.SelectedDate;
                item["Relation4"] = ddRelation4.SelectedValue;
                item["DependantsEducationOccupationLevel4"] = txtDependantsEducationOccupationLevel4.Text;
                item["DependantsSchoolUniversity4"] = txtDependantsSchoolUniversity4.Text;
                item["DependantsHealth4"] = GetSPListItemsFromCheckBoxes(cbDependantsHealth4);

                item["DependantsFirst5"] = txtDependantsFirst5.Text;
                item["DependantsSurName5"] = txtDependantsSurName5.Text;
                item["Dependants5"] = calDependants5.SelectedDate;
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

                Literal litControl = new Literal();
                phForLiteral.Controls.Add(litControl);

                litControl.Text = "<script type='text/javascript'> window.location ='/Lists/Customer2'; </script>";
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
    }
}


<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control EnableViewState="true" Language="C#" AutoEventWireup="true" CodeBehind="NeedsAnalysis.ascx.cs" Inherits="TTK.SP.NeedsAnalysis.WP.NeedsAnalysis" %>

<style type="text/css">
    .wizard-table {
        width: 800px;
    }

    .auto-style1 {
        width: 285px;
    }

    .auto-style2 {
        width: 297px;
    }

    .auto-style3 {
        width: 306px;
    }

    .auto-style6 {
        width: 118px;
    }

    .auto-style7 {
        width: 137px;
    }

    .auto-style9 {
        width: 298px;
    }

    .auto-style12 {
        width: 124px;
    }

    .auto-style14 {
        width: 101px;
    }

    .auto-style15 {
        width: 96px;
    }

    .auto-style16 {
        width: 223px;
    }

    .auto-style20 {
        width: 737px;
    }

    .auto-style22 {
        width: 292px;
    }

    .auto-style23 {
        width: 265px;
    }

    .auto-style24 {
        font-weight: bold;
    }
</style>

<link rel="stylesheet" type="text/css" href="/Style Library/jquery-ui.css">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.23/jquery-ui.min.js"></script>



<asp:UpdatePanel ID="UpdatePanel1" EnableViewState="true" runat="server">
</asp:UpdatePanel>

<script type="text/javascript" src="/_layouts/datepicker.js"></script>

<asp:PlaceHolder ID="phForLiteral" runat="server"></asp:PlaceHolder>


<table>
    <tr>
        <td colspan="1" class="auto-style24">
            <label>Referrer:</label>
            <asp:TextBox ID="txtReferrer" runat="server"></asp:TextBox>
        </td>
        <td colspan="1">
            <b>
                <label>
                    Date:
                </label>
            </b>
            <asp:Label runat="server" ID="lbDateCaptured"></asp:Label>
        </td>
        <td colspan="1"><b>New Business Register:</b><asp:DropDownList ID="ddNewBusinessRegister" runat="server">
            <asp:ListItem>Inactive</asp:ListItem>
            <asp:ListItem>In Progress</asp:ListItem>
            <asp:ListItem>Complete</asp:ListItem>
        </asp:DropDownList></td>
        <td colspan="1"><b>Underwriting Register:</b><asp:DropDownList ID="ddUnderWritingRegister" runat="server">
            <asp:ListItem>Not Started</asp:ListItem>
            <asp:ListItem>In Progress</asp:ListItem>
            <asp:ListItem>Complete</asp:ListItem>
        </asp:DropDownList></td>
        <td colspan="1">
            <asp:Label Visible="false" runat="server" ID="lbVersion"></asp:Label>
        </td>
    </tr>
</table>

<asp:Wizard ID="wizNeeds" EnableViewState="true" runat="server" ActiveStepIndex="0" OnNextButtonClick="wizNeeds_NextButtonClick" BackColor="White" BorderColor="#CE244A" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" OnFinishButtonClick="wizNeeds_FinishButtonClick">
    <FinishNavigationTemplate>
        <asp:Button ID="FinishPreviousButton" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="MovePrevious" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" Text="Previous" />
        <asp:Button ID="FinishButton" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" CommandName="MoveComplete" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" Text="Finish" />
    </FinishNavigationTemplate>
    <HeaderStyle BackColor="#666666" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
    <NavigationButtonStyle BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" />
    <SideBarButtonStyle ForeColor="White" />
    <SideBarStyle BackColor="#ce244a" Font-Size="1.2em" VerticalAlign="Top" Width="250px" />
    <StepStyle BackColor="White" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="2px" />
    <WizardSteps>
        <asp:WizardStep ID="WizardStepPersonal" EnableViewState="true" runat="server" StepType="Start" Title="Personal">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Client's Personal Data</th>
                </tr>
                <tr>
                    <td>
                        <label>
                            Full Name (first, last)</label></td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtFirstName" ErrorMessage="Please enter First Name" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtLastName" ErrorMessage="Please enter Last Name" />
                    </td>
                    <td>
                        <label>
                            Date of Birth</label></td>
                    <td>
                        <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator72" ControlToValidate="txtDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calDOB" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:52:17" LocaleId="3081" ToolTip="" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Gender</label></td>
                    <td>
                        <asp:DropDownList ID="ddGender" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddGender" ErrorMessage="Please select Gender" />
                    </td>
                    <td>
                        <label>
                            Smoking</label></td>
                    <td>
                        <asp:DropDownList ID="ddSmoker" runat="server">
                            <asp:ListItem>Non-Smoker</asp:ListItem>
                            <asp:ListItem>Smoker</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddSmoker" ErrorMessage="Please select smoking status" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Marital</label></td>
                    <td>
                        <asp:DropDownList ID="ddMarital" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Married</asp:ListItem>
                            <asp:ListItem>Single</asp:ListItem>
                            <asp:ListItem>De Facto</asp:ListItem>
                            <asp:ListItem>Divorced</asp:ListItem>
                            <asp:ListItem>Seperated</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddMarital" ErrorMessage="Please select marital status" />
                    </td>
                    <td>
                        <label>
                            Residential Address</label></td>
                    <td>
                        <asp:TextBox ID="txtResidential" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtResidential" ErrorMessage="Please enter a Residential Address" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Mobile</label></td>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMobile" runat="server" ErrorMessage="Invalid phone format" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtMobile" ErrorMessage="Please enter the Mobile Number" />
                    </td>
                    <td>
                        <label>
                            Business Telephone</label></td>
                    <td>
                        <asp:TextBox ID="txtBusiness" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtBusiness" runat="server" ErrorMessage="Invalid phone format" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtBusiness" ErrorMessage="Please enter the Business Number" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Home Telephone</label></td>
                    <td>
                        <asp:TextBox ID="txtHome" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtHome" runat="server" ErrorMessage="Invalid phone format" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtHome" ErrorMessage="Please enter the Home Number" />
                    </td>
                    <td>
                        <label>
                            Email</label></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEmail" runat="server" ErrorMessage="Invalid email format" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtEmail" ErrorMessage="Please enter the Email address" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Correspondence Preference</label></td>
                    <td>
                        <asp:DropDownList ID="ddCorrespondancePreference" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                            <asp:ListItem>Post</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="ddCorrespondancePreference" ErrorMessage="Please select Correspondance Preference" />
                    </td>
                    <td>
                        <label>
                            Will</label></td>
                    <td>
                        <asp:CheckBox ID="cbWill" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Qantas Frequent Flyer</label></td>
                    <td>
                        <asp:TextBox ID="txtQantas" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <label>
                            RACV Membership</label></td>
                    <td>
                        <asp:TextBox ID="txtRACV" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Solicitor</label></td>
                    <td>
                        <asp:TextBox ID="txtSolicitor" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <label>
                            Accountant</label></td>
                    <td>
                        <asp:TextBox ID="txtAccountant" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStepPartner" EnableViewState="true" runat="server" Title="Partner">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Partner's Personal Data</th>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Full Name<br />
                            (first, last)</label></td>
                    <td class="auto-style20">
                        <asp:TextBox runat="server" ID="txtSurNameP"></asp:TextBox><asp:TextBox runat="server" ID="txtFirstNameP"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtSurNameP" ErrorMessage="Please enter First Name" ID="RequiredFieldValidator11"></asp:RequiredFieldValidator>
						<asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstNameP" ErrorMessage="Please enter Last Name" ID="RequiredFieldValidator12"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="auto-style2">
                        <label>
                            Date of Birth</label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtDOBP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator73" ControlToValidate="txtDOBP" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl runat="server" ID="calDOBP" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" ToolTip=""></SharePoint:DateTimeControl>--%>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Gender</label></td>
                    <td class="auto-style20">
                        <asp:DropDownList ID="ddGenderP" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddGenderP" ErrorMessage="Please select a gender"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        <label>
                            Smoking</label></td>
                    <td>
                        <asp:DropDownList ID="ddSmokerP" runat="server">
                            <asp:ListItem>Non-Smoker</asp:ListItem>
                            <asp:ListItem>Smoker</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Marital</label></td>
                    <td class="auto-style20">
                        <asp:DropDownList ID="ddMaritalP" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Married</asp:ListItem>
                            <asp:ListItem>Single</asp:ListItem>
                            <asp:ListItem>De Facto</asp:ListItem>
                            <asp:ListItem>Divorced</asp:ListItem>
                            <asp:ListItem>Seperated</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="ddMaritalP" ErrorMessage="Please select a Marital Status" />--%>
                    </td>
                    <td class="auto-style1">
                        <label>
                            Residential Address</label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtResidentialP" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtResidentialP" ErrorMessage="Please enter a Residential Address" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Mobile</label></td>
                    <td class="auto-style20">
                        <asp:TextBox ID="txtMobileP" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="txtMobileP" ErrorMessage="Please enter a Mobile Number" />--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtMobileP" Display="Dynamic" ErrorMessage="Invalid phone format" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <label>
                            Business Telephone</label></td>
                    <td>
                        <asp:TextBox ID="txtBusinessP" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="txtBusinessP" ErrorMessage="Please enter a Business Number" />--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtBusinessP" Display="Dynamic" ErrorMessage="Invalid phone format" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Home Telephone</label></td>
                    <td class="auto-style20">
                        <asp:TextBox ID="txtHomeP" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtMobileP" ErrorMessage="Please enter a Home Number"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtHomeP" Display="Dynamic" ErrorMessage="Invalid phone format" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <label>
                            Email</label></td>
                    <td>
                        <asp:TextBox ID="txtEmailP" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtEmailP" ErrorMessage="Please enter a Email"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtEmailP" Display="Dynamic" ErrorMessage="Invalid email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Correspondence Preference</label></td>
                    <td class="auto-style20">
                        <asp:DropDownList ID="ddCorrespondencePreferenceP" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                            <asp:ListItem>Post</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddCorrespondencePreferenceP" ErrorMessage="Please select a Correspondance Preference"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        <label>
                            Will</label></td>
                    <td>
                        <asp:CheckBox ID="cbWillP" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Qantas Frequent Flyer</label></td>
                    <td class="auto-style20">
                        <asp:TextBox ID="txtQantasP" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <label>
                            RACV Membership</label></td>
                    <td>
                        <asp:TextBox ID="txtRACVP" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <label>
                            Solicitor</label></td>
                    <td class="auto-style20">
                        <asp:TextBox ID="txtSolicitorP" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <label>
                            Accountant</label></td>
                    <td>
                        <asp:TextBox ID="txtAccountantP" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <%--<asp:ValidationSummary ID="ValidationSummaryP" runat="server" Visible="False" />--%>
                    </td>
                </tr>
            </table>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStepClientFamilyDetails" EnableViewState="true" runat="server" Title="Client Family Details">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Client's Families Data</th>
                </tr>
                <tr>
                    <th>Relationship</th>
                    <th>Name</th>
                    <th>Date Of Birth</th>
                    <th>Health Conditions</th>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Father</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtFather" runat="server"></asp:TextBox></td>
                    <td valign="top">
                        <asp:TextBox ID="txtFatherDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator71" ControlToValidate="txtFatherDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calFather" runat="server" DateOnly="true" LocaleId="3081" SelectedDate="09/12/2013 23:54:17" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbFatherHealth" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Mother</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtMother" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtMotherDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator100"  ControlToValidate="txtMotherDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calMother" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbMotherHealth" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Brother</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrother" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrotherDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator74"  ControlToValidate="txtBrotherDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calBrother" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbBrotherHealth" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Brother</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrother2" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrother2DOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator75"  ControlToValidate="txtBrother2DOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calBrother2" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbBrotherHealth2" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Sister</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSister" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSisterDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator76"  ControlToValidate="txtSisterDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calSister" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbSisterHealth" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Sister</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSister2" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSister2DOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator77"  ControlToValidate="txtSister2DOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calSister2" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbSisterHealth2" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>

            </table>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStepClientFamilyDetailsP" runat="server" Title="Partner Family Details">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Partner's Personal Data</th>
                </tr>
                <tr>
                    <th>Relationship</th>
                    <th>Name</th>
                    <th>Date Of Birth</th>
                    <th>Health Conditions</th>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Father</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtFatherP" runat="server"></asp:TextBox></td>
                    <td valign="top">
                        <asp:TextBox ID="txtFatherPDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator78"  ControlToValidate="txtFatherPDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calFatherP" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbFatherHealthP" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList></td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Mother</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtMotherP" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtMotherPDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator79"  ControlToValidate="txtMotherPDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calMotherP" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbMotherHealthP" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Brother</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrotherP" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrotherPDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator80"  ControlToValidate="txtBrotherPDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calBrotherP" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbBrotherHealthP" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Brother</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrother2P" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtBrother2PDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator81"  ControlToValidate="txtBrother2PDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calBrother2P" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbBrotherHealth2P" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Sister</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSisterP" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSisterPDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator82"  ControlToValidate="txtSisterPDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calSisterP" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbSisterHealthP" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            Sister</label></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSister2P" runat="server" /></td>
                    <td valign="top">
                        <asp:TextBox ID="txtSister2PDOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator83"  ControlToValidate="txtSister2PDOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calSister2P" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbSisterHealth2P" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList></td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:WizardStep>
        <asp:WizardStep runat="server" Title="Dependant Details">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Client's Dependant's Data</th>
                </tr>
                <tr>
                    <th class="auto-style12">Full Name(first, last)</th>
                    <th>Date of Birth</th>
                    <th>Relation</th>
                    <th class="auto-style15">Occupation/
						Education Year Level</th>
                    <th class="auto-style14">School/
						University Attended</th>
                    <th>Health</th>
                </tr>
                <tr>
                    <td valign="top" class="auto-style12">
                        <asp:TextBox ID="txtDependantsFirst1" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtDependantsSurName1" runat="server"></asp:TextBox>
                    </td>
                    <td valign="top">                        
                        <asp:TextBox ID="txtDependants1DOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator101" ControlToValidate="txtDependants1DOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calDependants1" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddRelation1" runat="server">
                            <asp:ListItem>Daughter</asp:ListItem>
                            <asp:ListItem>Son</asp:ListItem>
                            <asp:ListItem>Step-Daughter</asp:ListItem>
                            <asp:ListItem>Step-Son</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" class="auto-style15">
                        <asp:TextBox ID="txtDependantsEducationOccupationLevel1" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td valign="top" class="auto-style14">
                        <asp:TextBox ID="txtDependantsSchoolUniversity1" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbDependantsHealth1" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="auto-style12">
                        <asp:TextBox ID="txtDependantsFirst2" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtDependantsSurName2" runat="server"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtDependants2DOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator84" ControlToValidate="txtDependants2DOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calDependants2" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddRelation2" runat="server">
                            <asp:ListItem>Daughter</asp:ListItem>
                            <asp:ListItem>Son</asp:ListItem>
                            <asp:ListItem>Step-Daughter</asp:ListItem>
                            <asp:ListItem>Step-Son</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" class="auto-style15">
                        <asp:TextBox ID="txtDependantsEducationOccupationLevel2" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td valign="top" class="auto-style14">
                        <asp:TextBox ID="txtDependantsSchoolUniversity2" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbDependantsHealth2" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="auto-style12">
                        <asp:TextBox ID="txtDependantsFirst3" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtDependantsSurName3" runat="server"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtDependants3DOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator85" ControlToValidate="txtDependants3DOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calDependants3" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddRelation3" runat="server">
                            <asp:ListItem>Daughter</asp:ListItem>
                            <asp:ListItem>Son</asp:ListItem>
                            <asp:ListItem>Step-Daughter</asp:ListItem>
                            <asp:ListItem>Step-Son</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" class="auto-style15">
                        <asp:TextBox ID="txtDependantsEducationOccupationLevel3" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td valign="top" class="auto-style14">
                        <asp:TextBox ID="txtDependantsSchoolUniversity3" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbDependantsHealth3" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>

                <tr>
                    <td valign="top" class="auto-style12">
                        <asp:TextBox ID="txtDependantsFirst4" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtDependantsSurName4" runat="server"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtDependants4DOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator86" ControlToValidate="txtDependants4DOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calDependants4" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddRelation4" runat="server">
                            <asp:ListItem>Daughter</asp:ListItem>
                            <asp:ListItem>Son</asp:ListItem>
                            <asp:ListItem>Step-Daughter</asp:ListItem>
                            <asp:ListItem>Step-Son</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" class="auto-style15">
                        <asp:TextBox ID="txtDependantsEducationOccupationLevel4" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td valign="top" class="auto-style14">
                        <asp:TextBox ID="txtDependantsSchoolUniversity4" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbDependantsHealth4" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>

                <tr>
                    <td valign="top" class="auto-style12">
                        <asp:TextBox ID="txtDependantsFirst5" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtDependantsSurName5" runat="server"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtDependants5DOB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator87" ControlToValidate="txtDependants5DOB" runat="server" ErrorMessage="Invalid Date (dd/mm/yyyy)" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        <%--<SharePoint:DateTimeControl ID="calDependants5" runat="server" DateOnly="true" SelectedDate="08/09/2013 09:55:23" LocaleId="3081" />--%>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddRelation5" runat="server">
                            <asp:ListItem>Daughter</asp:ListItem>
                            <asp:ListItem>Son</asp:ListItem>
                            <asp:ListItem>Step-Daughter</asp:ListItem>
                            <asp:ListItem>Step-Son</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" class="auto-style15">
                        <asp:TextBox ID="txtDependantsEducationOccupationLevel5" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td valign="top" class="auto-style14">
                        <asp:TextBox ID="txtDependantsSchoolUniversity5" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbDependantsHealth5" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>

            </table>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStepOccupation" runat="server" Title="Occupation Details">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Client's and Partner's Occupation Details</th>
                </tr>
                <tr>
                    <td>
                        <label>Job Title</label></td>
                    <td>
                        <asp:TextBox ID="txtJobTitle" runat="server" /></td>
                    <td>
                    <td>
                        <label>Employment Status</label></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddEmploymentStatus">
                            <asp:ListItem>Employee</asp:ListItem>
                            <asp:ListItem>Working Director</asp:ListItem>
                            <asp:ListItem>Sole Trader</asp:ListItem>
                            <asp:ListItem>Home Duties</asp:ListItem>
                        </asp:DropDownList>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <label>Employer</label></td>
                    <td>
                        <asp:TextBox ID="txtEmployer" runat="server" /></td>
                    <td>
                    <td>
                        <label>
                            Hours per week</label></td>
                    <td>
                        <asp:TextBox ID="txtHoursPerWeek" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <label>Qualifications</label></td>
                    <td>
                        <asp:TextBox ID="txtQualifications" runat="server" /></td>
                    <td>
                    <td>
                        <label>
                            Remuneration</label></td>
                    <td>
                        <asp:TextBox ID="txtRemuneration" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <label>Duties</label></td>
                    <td>
                        <asp:TextBox ID="txtDuties" runat="server" TextMode="MultiLine" /></td>
                </tr>
            </table>

            <table class="wizard-table">
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <label>
                            <strong>Partner</strong></label></td>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <label>Job Title</label></td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtJobTitleP" runat="server" /></td>
                    <td>
                    <td>
                        <label>Employment Status</label></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddEmploymentStatusP">
                            <asp:ListItem>Employee</asp:ListItem>
                            <asp:ListItem>Working Director</asp:ListItem>
                            <asp:ListItem>Sole Trader</asp:ListItem>
                            <asp:ListItem>Home Duties</asp:ListItem>
                        </asp:DropDownList>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <label>Employer</label></td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtEmployerP" runat="server" /></td>
                    <td>
                    <td>
                        <label>Hours per week</label></td>
                    <td>
                        <asp:TextBox ID="txtHoursPerWeekP" runat="server" />
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <label>Qualifications</label></td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtQualificationsP" runat="server" /></td>
                    <td>
                    <td>
                        <label>Remuneration</label></td>
                    <td>
                        <asp:TextBox ID="txtRemunerationP" runat="server" />
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <label>Duties</label></td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtDutiesP" runat="server" TextMode="MultiLine" /></td>
                </tr>
            </table>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStepHealth" runat="server" Title="Health History">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Client's Health History</th>
                </tr>
                <tr>
                    <td>
                        <label>
                            Health Condition</label></td>
                    <td>
                        <asp:CheckBoxList ID="cbHealthCondition" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>

                    </td>
                </tr>
            </table>
            <table class="wizard-table">

                <tr>
                    <td>
                        <label>
                            <strong>Partner:</strong></label></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Health Condition</label></td>
                    <td>
                        <asp:CheckBoxList ID="cbHealthConditionP" runat="server" RepeatColumns="2">
                            <asp:ListItem>Back</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Dementia</asp:ListItem>
                            <asp:ListItem>Heart Disease</asp:ListItem>
                            <asp:ListItem>High Blood Pressure</asp:ListItem>
                            <asp:ListItem>High Cholesterol</asp:ListItem>
                            <asp:ListItem>Kidney Disease</asp:ListItem>
                            <asp:ListItem>Mental Illness</asp:ListItem>
                            <asp:ListItem>Stroke</asp:ListItem>
                            <asp:ListItem>Diabetes</asp:ListItem>
                            <asp:ListItem>Cancer</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStepFinance" runat="server" Title="Financial Position">
            <table class="wizard-table">
                <tr>
                    <th colspan="3">Please capture the Client&#39;s and Partner&#39;s (if may be a business partner) Financial Details</th>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style23">&nbsp;</td>
                    <td class="auto-style22">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="font-weight: 700">
                        <label>
                            Assets</label></td>
                    <td class="auto-style23">
                        <label>
                            <strong>Client Value ($)</strong></label></td>
                    <td class="auto-style22"><b>
                        <label>
                            Partner Value ($)</label></b></td>
                    <td><b>
                        <label>
                            Value if Joint ($)</label></b></td>
                </tr>
                <tr>
                    <td>
                        <label>House/Domicile</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtHouse" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="txtHouse" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtHouseP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="txtHouseP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHouseJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ControlToValidate="txtHouseJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Contents</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtContents" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ControlToValidate="txtContents" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtContentsP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server" ControlToValidate="txtContentsP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContentsJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server" ControlToValidate="txtContentsJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Super Annuation Account</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtSuper" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ControlToValidate="txtSuper" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtSuperP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server" ControlToValidate="txtSuperP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSuperJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator32" runat="server" ControlToValidate="txtSuperJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Cash</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtCash" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator33" runat="server" ControlToValidate="txtCash" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtCashP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator34" runat="server" ControlToValidate="txtCashP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCashJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator35" runat="server" ControlToValidate="txtCashJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Shares</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtShares" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator36" runat="server" ControlToValidate="txtShares" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtSharesP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator37" runat="server" ControlToValidate="txtSharesP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSharesJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator38" runat="server" ControlToValidate="txtSharesJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Investment Properties</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtInvestmentProperties" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator39" runat="server" ControlToValidate="txtInvestmentProperties" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtInvestmentPropertiesP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator40" runat="server" ControlToValidate="txtInvestmentPropertiesP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvestmentPropertiesJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator41" runat="server" ControlToValidate="txtInvestmentPropertiesJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Value of Business</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtBusinessValue" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator42" runat="server" ControlToValidate="txtBusinessValue" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtBusinessValueP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator43" runat="server" ControlToValidate="txtBusinessValueP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBusinessValueJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator44" runat="server" ControlToValidate="txtBusinessValueJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Potential Inheritance</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtPotentialInheritance" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator45" runat="server" ControlToValidate="txtPotentialInheritance" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtPotentialInheritanceP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator46" runat="server" ControlToValidate="txtPotentialInheritanceP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPotentialInheritanceJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator47" runat="server" ControlToValidate="txtPotentialInheritanceJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: 700">
                        <label>Liabilities</label></td>
                    <td class="auto-style23">
                        <label><strong>Client Value ($)</strong></label></td>
                    <td class="auto-style22">
                        <b>
                            <label>
                                Partner Value ($)</label></b></td>
                    <td>
                        <b>
                            <label>
                                Value if Joint ($)</label></b></td>
                </tr>
                <tr>
                    <td>
                        <label>Mortgage</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtMortgage" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator48" runat="server" ControlToValidate="txtMortgage" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtMortgageP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator49" runat="server" ControlToValidate="txtMortgageP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMortgageJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator50" runat="server" ControlToValidate="txtMortgageJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Personal Loans</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtPersonalLoans" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator51" runat="server" ControlToValidate="txtPersonalLoans" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtPersonalLoansP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator52" runat="server" ControlToValidate="txtPersonalLoansP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPersonalLoansJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator53" runat="server" ControlToValidate="txtPersonalLoansJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Credit Card Debt</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtCreditCardDebt" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator54" runat="server" ControlToValidate="txtCreditCardDebt" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtCreditCardDebtP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator55" runat="server" ControlToValidate="txtCreditCardDebtP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditCardDebtJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator56" runat="server" ControlToValidate="txtCreditCardDebtJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Investment Loans</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtInvestmentLoans" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator57" runat="server" ControlToValidate="txtInvestmentLoans" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtInvestmentLoansP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator58" runat="server" ControlToValidate="txtInvestmentLoansP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvestmentLoansJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator59" runat="server" ControlToValidate="txtInvestmentLoansJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Leases</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtLeases" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator60" runat="server" ControlToValidate="txtLeases" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtLeasesP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator61" runat="server" ControlToValidate="txtLeasesP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLeasesJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator62" runat="server" ControlToValidate="txtLeasesJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Business Debt</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtBusinessDebt" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator63" runat="server" ControlToValidate="txtBusinessDebt" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtBusinessDebtP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator64" runat="server" ControlToValidate="txtBusinessDebtP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBusinessDebtJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator65" runat="server" ControlToValidate="txtBusinessDebtJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Other Liabilities</label></td>
                    <td class="auto-style23">
                        <asp:TextBox ID="txtLiabilites" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator66" runat="server" ControlToValidate="txtLiabilites" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtLiabilitesP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator67" runat="server" ControlToValidate="txtLiabilitesP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLiabilitesJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator68" runat="server" ControlToValidate="txtLiabilitesJ" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: 700">
                        <label>Investment Income</label>
                    <td class="auto-style23">
                        <label><strong>Other Income Details</strong></label></td>
                    <td class="auto-style22">
                        <b>
                            <label>
                                Client ($ per Annum)</label></b></td>
                    <td>
                        <b>
                            <label>
                                Partner ($ per Annum)</label></b></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style23">
                        <asp:DropDownList runat="server" ID="ddIncome">
                            <asp:ListItem>Rental Income</asp:ListItem>
                            <asp:ListItem>Dividends from Shares</asp:ListItem>
                            <asp:ListItem>Interest Income</asp:ListItem>
                            <asp:ListItem>Other Income</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtClientIncome" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator69" runat="server" ControlToValidate="txtClientIncome" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    <td>
                        <asp:TextBox ID="txtPartnerIncome" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator70" runat="server" ControlToValidate="txtPartnerIncome" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStepInsurance" runat="server" Title="Insurance">
            <table class="wizard-table">
                <tr>
                    <th colspan="2">Please capture the Details of the Client's Existing Insurance</th>
                </tr>
                <tr>
                    <td class="auto-style6">
                        <label>Cover Type</label></td>
                    <td class="auto-style3">
                        <b>
                            <label>
                                Insurer/Company</label>
                        </b>
                    </td>
                    <td class="auto-style3">
                        <b>
                            <label>
                                Client Sum Insured $</label>
                        </b>
                    </td>
                    <td class="auto-style3">
                        <b>
                            <label>
                                Partner Sum Insured $</label>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Income Protection</label></td>
                    <td>
                        <asp:TextBox ID="txtIncomeProtection" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIncomeProtectionC" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" ControlToValidate="txtIncomeProtectionC" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIncomeProtectionP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="txtIncomeProtectionP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Life/Death Cover</label></td>
                    <td>
                        <asp:TextBox ID="txtLifeCover" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLifeCoverC" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="txtLifeCoverC" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLifeCoverP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtLifeCoverP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Total and Permanent Disablement (TPD)</label></td>
                    <td>
                        <asp:TextBox ID="txtDisable" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDisableC" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtDisableC" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDisableP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtDisableP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Trauma/Critical Illness</label></td>
                    <td>
                        <asp:TextBox ID="txtTrauma" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTraumaC" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtTraumaC" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTraumaP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtTraumaP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Other (please specify)</label></td>
                    <td>
                        <asp:TextBox ID="txtOtherInsured" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOtherInsuredC" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtOtherInsuredC" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOtherInsuredP" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtOtherInsuredP" ErrorMessage="Please enter a currency" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
        </asp:WizardStep>
    </WizardSteps>
</asp:Wizard>

<asp:Label runat="server" ID="ErrorLabel" Visible="false"></asp:Label>

<script type="text/javascript" language="javascript">
    $(function () {
        $("#<%= txtDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDOBP.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtFatherDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtMotherDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

        $("#<%= txtBrotherDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtBrother2DOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtSisterDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtSister2DOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtFatherPDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtMotherPDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtBrotherPDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtBrother2PDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtBrother2PDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtSisterPDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtSister2PDOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
	    $("#<%= txtDependants1DOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDependants2DOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDependants3DOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDependants4DOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#<%= txtDependants5DOB.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
	});

</script>







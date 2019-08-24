<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        canvas {
            -moz-user-select: none;
            -webkit-user-select: none;
            -ms-user-select: none;
        }

        html, body {
            margin: 0;
            padding: 0;
        }
    </style>
    <script src="Scripts/Chart.js"></script>
    <script src="Script/LottoResults.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <canvas id="lottoChart"></canvas>
        </div>
        <table style="margin-bottom: 20px; margin-left: 20px;">
            <tr>
                <td style="padding-right: 5px;">
                    <dx:ASPxComboBox runat="server" ID="cboGame" AutoPostBack="True" Caption="Spill" OnValueChanged="cboGame_OnValueChanged" SelectedIndex="0" ValueType="System.Int32">
                        <Items>
                            <dx:ListEditItem Text="Lotto" Value="0" />
                            <dx:ListEditItem Text="Vikinglotto" Value="1" />
                            <dx:ListEditItem Text="EuroJackpot" Value="2" />
                        </Items>
                        <CaptionSettings Position="Left"></CaptionSettings>
                    </dx:ASPxComboBox>
                </td>
                <td style="padding-right: 5px;">
                    <dx:ASPxCheckBox runat="server" ID="chkSorted" AutoPostBack="True" Text="Sortert" TextAlign="Left" Checked="false" OnCheckedChanged="chkSorted_OnCheckedChanged">
                    </dx:ASPxCheckBox>
                </td>
                <td style="padding-right: 5px;">
                    <dx:ASPxComboBox runat="server" ID="cboLastRounds" AutoPostBack="True" Caption="Antall runder" OnValueChanged="cboLastRounds_OnValueChanged" SelectedIndex="1" ValueType="System.Int32">
                        <Items>
                            <dx:ListEditItem Text="10" Value="10" />
                            <dx:ListEditItem Text="30" Value="30" />
                        </Items>
                        <CaptionSettings Position="Left"></CaptionSettings>
                    </dx:ASPxComboBox>
                </td>
                <td>
                    <dx:ASPxComboBox runat="server" ID="cboFilterRounds" AutoPostBack="True" Caption="Spesialfilter" OnValueChanged="cboFilterRounds_OnValueChanged" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="Alle" Value="ALL" />
                            <dx:ListEditItem Text="Oddetall" Value="ODD" />
                            <dx:ListEditItem Text="Partall" Value="EVEN" />
                        </Items>
                        <CaptionSettings Position="Left"></CaptionSettings>
                    </dx:ASPxComboBox>
                </td>
                <td style="padding-right: 5px;">
                    <dx:ASPxLabel runat="server" Text="Tipp:" />
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="txtBall1" ClientInstanceName="txtBall1" Width="30px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="txtBall2" ClientInstanceName="txtBall2" Width="30px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="txtBall3" ClientInstanceName="txtBall3" Width="30px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="txtBall4" ClientInstanceName="txtBall4" Width="30px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="txtBall5" ClientInstanceName="txtBall5" Width="30px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="txtBall6" ClientInstanceName="txtBall6" Width="30px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="txtBall7" ClientInstanceName="txtBall7" Width="30px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxButton runat="server" ID="btnUpdateEstimate" Text="Oppdater" OnClick="btnUpdateEstimate_Click"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

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
    </style>
    <script src="Scripts/Chart.js"></script>
    <script src="Script/LottoResults.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <canvas id="lottoChart"></canvas>
        </div>
        <table>
            <tr>
                <td>
                    <dx:ASPxCheckBox runat="server" ID="chkSorted" AutoPostBack="True" Text="Sortert" TextAlign="Left" Checked="false" OnCheckedChanged="chkSorted_OnCheckedChanged">                                                
                    </dx:ASPxCheckBox>
                </td>
                <td>
                    <dx:ASPxComboBox runat="server" ID="cboLastRounds" AutoPostBack="True" Caption="Antall runder" OnValueChanged="cboLastRounds_OnValueChanged" SelectedIndex="1">
                        <Items>
                            <dx:ListEditItem Text="10" Value="10" />
                            <dx:ListEditItem Text="30" Value="30" />
                            <dx:ListEditItem Text="50" Value="50" />
                            <dx:ListEditItem Text="70" Value="70" />
                            <dx:ListEditItem Text="110" Value="110" />
                        </Items>
                        <CaptionSettings Position="Left"></CaptionSettings>
                    </dx:ASPxComboBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

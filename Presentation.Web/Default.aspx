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
        html, body { margin: 0; padding: 0; }
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
                <td style="padding-right: 5px;">
                    <dx:ASPxComboBox runat="server" ID="cboGame" AutoPostBack="True" Caption="Spill" OnValueChanged="cboGame_OnValueChanged" SelectedIndex="0" ValueType="System.Int32">
                        <Items>
                            <dx:ListEditItem Text="Lotto" Value="0" />
                            <dx:ListEditItem Text="VikingLotto" Value="1" />
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
                            <dx:ListEditItem Text="50" Value="50" />
                            <dx:ListEditItem Text="70" Value="70" />
                            <dx:ListEditItem Text="110" Value="110" />
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
            </tr>
        </table>
    </form>
</body>
</html>

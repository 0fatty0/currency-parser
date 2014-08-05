
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FxParser._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <thead>
                <tr>
                    <td>Currency Pair</td>
                    <td>Time</td>
                    <td>Start Price</td>
                    <td>Middle Price</td>
                    <td>End Price</td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="StampDataRepeater" runat="server">
                    <ItemTemplate>
                            <tr>
                                <td><asp:TextBox ID="txtCurrencyPair" runat="server" Text='<%# Eval("CurrencyPair") %>' /></td>
                                <td><asp:TextBox ID="txtTime" runat="server" Text='<%# Eval("Time") %>' /></td>
                                <td><asp:TextBox ID="txtStartPrice" runat="server" Text='<%# Eval("StartPrice") %>' /></td>
                                <td><asp:TextBox ID="txtMiddlePrice" runat="server" Text='<%# String.Format("{0:d}", Eval("MiddlePrice")) %>' /></td>
                                <td><asp:TextBox ID="txtEndPrice" runat="server" Text='<%# String.Format("{0:d}", Eval("EndPrice")) %>' /></td>
                            </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        
        <asp:Button ID="RefreshButton" runat="server" Text="Refresh" OnClick="RefreshButton_Click" />
    
    </div>
    </form>
</body>
</html>

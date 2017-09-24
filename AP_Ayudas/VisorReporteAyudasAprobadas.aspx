<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisorReporteAyudasAprobadas.aspx.cs" Inherits="AP_Ayudas_VisorReporteAyudasAprobadas" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">

    
.dialogzone
{
	background-color:#bcc8da ;
	color:black;
	font-family:Tahoma,sans-serif;
	font-size:11px;
}

.crtoolbar
{
    background:url('file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/ASP.NETClientFiles/crystalreportviewers13//js/crviewer/images/allInOneBG.gif') repeat-x 0px 0px; /* toolbar_background.gif */
        }

.designTimeToolbarItem
{
    float: left;
}

.designTimeToolbarButton
{
    overflow: hidden;
    float: left;
    width: 21px;
    height: 19px;
    margin-top: 5px;
    margin-left: 5px;
}

.textinputs
{
    font-family: "arial" , "sans-serif";
    font-size: 11px;
    background-color: white;
    border: 1px solid #96A8C3;
    background-image: url('file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/ASP.NETClientFiles/crystalreportviewers13/js/dhtmllib/images/skin_standard/background_field.gif');
    background-repeat: repeat-x;
    padding-left: 5px;
    padding-right: 2px;
}


.designTimeLeftPanel 
{
    height: 100%; 
    float : left;
    vertical-align: top;
    width: 100px; 
}

.designTimePanelNavigator
{  
    vertical-align: top;
    background-color : #e5eaf1;
    border: 1px solid #94abc5;
    float: left;
    width : 33px;
    height: 100%;
    overflow:hidden;
}

.designTimePanelNavigatorItem 
{
    width : 31px;
    height : 33px;
    cursor : pointer;
}

.panelNavigatorItemImage
{
    width : 22px;
    height : 22px;
    position : absolute;
    top : 6px;
    left: 6px;
}

.designTimeToolPanel
{
    height:100%;
    position : relative;
    overflow: hidden;
    float : right;
    border-top: 1px solid #94abc5;
    border-right : 1px solid #94abc5;
    border-bottom: 1px solid #94abc5;
    background-color: #F5F7F9;
}

.panelHeader 
{
    border-top: 1px solid #96a8c3;
    border-right: 1px solid #96a8c3;
    background:url('file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/ASP.NETClientFiles/crystalreportviewers13//js/crviewer/images/allInOneBG.gif') repeat-x 0px -126px; /* panelHeaderBG.gif */
    position : absolute;
    z-index: 1;
    overflow: hidden;
}

.panelHeaderTitle
{
    position : absolute;
    bottom : 3px;
    left : 10px;    
    font-family : "Tahoma";
    font-weight: bold;
    font-size : 8.5pt;
    white-space: nowrap;
    color :#FFFFFF;
    overflow : hidden;
}

.panelHeaderButtonCtn
{
    position : absolute;
    right: 4px;
    bottom: 1px;
}
    
.panelHeaderCloseButton 
{
    padding : 1px;
}

.designTimeReportAlbum 
{
    height: 100%; 
    float:right;
}

.naviHTabMNormal
{
	background-image:url('file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/ASP.NETClientFiles/crystalreportviewers13/js/dhtmllib/images/skin_standard/horiz_tabs.gif');
	background-position:0px -72px;
	height:23px;
	position:relative;
}

.naviHTabMSelected
{
	background-image:url('file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/ASP.NETClientFiles/crystalreportviewers13/js/dhtmllib/images/skin_standard/horiz_tabs.gif');
	background-position:0px -24px;
	height:24px;
}

.naviHTabLSelected
{
	background-image:url('file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/ASP.NETClientFiles/crystalreportviewers13/js/dhtmllib/images/skin_standard/horiz_tabs.gif');
	background-position:0px 0px;
	width:4px;
	height:24px;
}

.naviHTabTextSel
{
	white-space:nowrap;
	font-family:Tahoma,sans-serif;	
	font-size:11px;
	color:black;
	text-align:left;
	font-weight:bold;
}

.naviHTabWithCloseRSel
{
	background-image:url('file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/ASP.NETClientFiles/crystalreportviewers13/js/dhtmllib/images/skin_standard/horiz_tabs.gif');
	background-position:0px -491px;
	width:4px;
	height:24px;
}

.iconText
{
    font-family: "arial" , "sans-serif";
    font-size: 11px;
    color: black;
}

    .designTimeStatusBar 
{
    height: 15px;
    overflow: hidden;
    clear: both;
    text-align: right;
    white-space: nowrap;
    padding: 3px 5px;
    position: relative;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
    <asp:TextBox ID="MensajeR" runat="server" Width="461px" Height="21px" TextMode="MultiLine"></asp:TextBox> 
        <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
        <asp:Label ID="EFechaInicial" runat="server" Text="Fecha Inicial" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="FechaInicial" runat="server" BackColor="#66FF33" Width="88px" Font-Bold="True" ForeColor="Red" TextMode="Date"></asp:TextBox>     
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="EFechaFinal" runat="server" Text="Fecha Final" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
        &nbsp;
        <asp:TextBox ID="FechaFinal" runat="server" BackColor="#66FF33" Width="95px" Font-Bold="True" ForeColor="Red" TextMode="Date"></asp:TextBox>     
        <asp:ImageButton ID="VerificaDatos" runat="server" Height="47px" ImageUrl="~/Iconos/Verificacion.gif" Width="56px" TabIndex="500" OnClick="VerificaDatos_Click" />
        <br />
        <CR:CrystalReportSource ID="FuenteAyudasAprobadas" runat="server">
            <Report FileName="SolicitudAyudasReporteAprob.rpt">
                <DataSources>
                    <CR:DataSourceRef DataSourceID="SoftempBD" TableName="VistaReporteAyudasAprob" />
                </DataSources>
                <Parameters>
                    <CR:ControlParameter ControlID="FechaInicial" ConvertEmptyStringToNull="False" DefaultValue="0" Name="@FechaInicial" PropertyName="Text" ReportName="" />
                    <CR:ControlParameter ControlID="FechaFinal" ConvertEmptyStringToNull="False" DefaultValue="0" Name="FechaFinal" PropertyName="Text" ReportName="" />
                </Parameters>
            </Report>
        </CR:CrystalReportSource>
        <br />
        <CR:CrystalReportViewer ID="AyudasAprobadas" runat="server" ReportSourceID="FuenteAyudasAprobadas" ReuseParameterValuesOnRefresh="True" ToolPanelView="None" />
        <br />
        <p>
            <script type="text/javascript">
                function cerrar() {
                    window.open("", "_parent", "");
                    var ventana = window.self;
                    ventana.opener = window.self;
                    ventana.close();
                }
            </script>
    </p>
    </div>
    </form>
</body>
</html>

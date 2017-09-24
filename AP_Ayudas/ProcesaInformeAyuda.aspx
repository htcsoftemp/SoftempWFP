<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcesaInformeAyuda.aspx.cs" Inherits="AP_Ayudas_ProcesaInformeAyuda" %>

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
        <asp:Label ID="ENumSolicitud" runat="server" Text="Solicitud #" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="NumSolicitud" runat="server" BackColor="#66FF33" Width="69px" Font-Bold="True" ForeColor="Red">103700</asp:TextBox>     
        <asp:ImageButton ID="BuscarInformeSolicitud" runat="server" ImageUrl="~/Iconos/Binoculares.gif" OnClick="BuscarInformeSolicitud_Click"  />
    &nbsp;<asp:TextBox ID="MensajeR" runat="server" Width="352px"></asp:TextBox> 
        <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
    <asp:Label ID="EIdentificacion" runat="server" Text="Identificacion" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="Cedula" runat="server" Enabled="False"></asp:TextBox> 
        <asp:TextBox ID="Grado" runat="server" Width="69px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
    <asp:TextBox ID="Nombres" runat="server" Columns="50" BackColor="#99FF33" Enabled="False" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Width="422px" Height="16px" style="font-size: small"></asp:TextBox>
        <br />
        <asp:Label ID="ETipoAyuda" runat="server" BorderColor="Black" CssClass="active" Text="Ayuda " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="TipoAyuda" runat="server" Width="42px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="RazonSolicitud" runat="server" Width="288px" BackColor="#66FF33" Enabled="False" Font-Names="Arial Narrow" Font-Size="X-Small"></asp:TextBox>
        <asp:Label ID="EAprobado" runat="server" BorderColor="Black" CssClass="active" Text="Aprobado" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="MCalculado" runat="server" Width="75px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="EPorcentaje" runat="server" BorderColor="Black" CssClass="active" Text="% Ayuda" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Porcentaje" runat="server" Width="42px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="Beneficiario" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="TipoBeneficiario" runat="server" Width="75px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="NombreBeneficiario" runat="server" Width="143px" BackColor="#66FF33"></asp:TextBox>
        <br />
        <asp:Label ID="EEspecialidad" runat="server" Text="Especialidad" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Especialidad" runat="server" Width="167px" BackColor="#66FF33"></asp:TextBox>
        <asp:Label ID="ECasaSalud" runat="server" Text="Casa de Salud" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="CasaDeSalud" runat="server" Width="242px" BackColor="#66FF33"></asp:TextBox>
        <asp:Label ID="EMedicoCertifica" runat="server" Text="Medico que Certifica" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="MedicoQueCertifica" runat="server" Width="143px" BackColor="#66FF33"></asp:TextBox>
        <br />
        <asp:ImageButton ID="btn_ArmaInforme" runat="server" Height="31px" ImageUrl="~/Iconos/Modificar.png"  ToolTip="Click para habilitar Modificacion de medios de contacto" Width="37px" BorderColor="#CCFFFF" BorderWidth="1px" OnClick="btn_ArmaInforme_Click" />
        <asp:Label ID="EAsunto" runat="server" Text="Asunto " BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Asunto" runat="server" Width="900px" TabIndex="310" BackColor="#FFFF66" Height="27px" ToolTip="Ingrese el Asunto del Informe Social de la Solicitud" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="EAntecedentes" runat="server" Text="Antecedentes" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;
        <asp:TextBox ID="Antecedentes" runat="server" Width="894px" TabIndex="310" BackColor="#FFFF66" Height="63px" ToolTip="Ingrese el Asunto del Informe Social de la Solicitud" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="EVerificacion" runat="server" Text="Verificacion" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="VerificacionDatos" runat="server" Width="900px" TabIndex="310" BackColor="#FFFF66" Height="61px" ToolTip="Ingrese el Asunto del Informe Social de la Solicitud" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="ECertificacion" runat="server" Text="Certificacion" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;&nbsp;&nbsp; <asp:TextBox ID="Certificacion" runat="server" Width="900px" TabIndex="310" BackColor="#FFFF66" Height="217px" ToolTip="Ingrese el Asunto del Informe Social de la Solicitud" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="ERecomendacion" runat="server" Text="Recomendacion" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Recomendacion" runat="server" Width="900px" TabIndex="310" BackColor="#FFFF66" Height="57px" ToolTip="Ingrese el Asunto del Informe Social de la Solicitud" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
     &nbsp;<asp:ImageButton ID="OtroSocio" runat="server" Height="24px" ImageUrl="~/Iconos/Lupa.gif" Width="24px" BorderStyle="Double" TabIndex="610" OnClick="OtroSocio_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="btn_ActualizarInformeAyudas" runat="server" Text="Actualizar Informe" Font-Bold="True" ForeColor="Red" Width="134px" TabIndex="220" ToolTip="Click para Actualizar los Datos del Informe de Ayudas Economicas Aprobado" OnClick="btn_ActualizarInformeAyudas_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_ImprimeInformeAyudas" runat="server" Text="Imprimir Informe" ToolTip="Click para Imprimir el Informe de Ayudas Econmicas" OnClick="btn_ImprimeInformeAyudas_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <input id="CLOSE1" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
        <br />
        <br />
        <br />
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

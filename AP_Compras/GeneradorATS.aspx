<%@ Page title="Generador ATS" Language="C#" AutoEventWireup="true" CodeFile="GeneradorATS.aspx.cs" Inherits="AP_Contabilidad_GeneradorATS" %>

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
        <asp:TextBox ID="MensajeR" runat="server" Width="923px" Height="44px" TextMode="MultiLine"></asp:TextBox> 
        <br />
        <asp:Panel ID="pnl_Parametros" runat="server" BackColor="#99FF66" Width="990px" Height="72px">
            <asp:ImageButton ID="btn_OtroPeriodo" runat="server" ImageUrl="~/Iconos/Binoculares.gif" style="margin-top: 0px; width: 16px;" OnClick="btn_OtroPeriodo_Click" ToolTip="Click para Procesar Otro Periodo" TabIndex="990"   />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="EYear" runat="server" Text="Año" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddl_Year" runat="server" DataSourceID="TYear" DataTextField="Anio" DataValueField="Anio" Height="16px" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged" Width="284px" AutoPostBack="True" ToolTip="Ingrese o Escoja el Año que desea procesar" TabIndex="10">
            </asp:DropDownList>
            &nbsp;<asp:TextBox ID="tbx_AnioI" runat="server" Width="42px"  TabIndex="20"></asp:TextBox>
            &nbsp;
            <asp:Label ID="EMes" runat="server" Text="Mes" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddl_Mes" runat="server" DataSourceID="TMes" DataTextField="Mes" DataValueField="Codigo" Height="17px" Width="273px" OnSelectedIndexChanged="ddl_Mes_SelectedIndexChanged" AutoPostBack="True" ToolTip="Ingrese o Escoja el Mes que desea procesar" TabIndex="30">
            </asp:DropDownList>
            &nbsp;<asp:TextBox ID="tbx_MesI" runat="server" Width="42px" TabIndex="40"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:ImageButton ID="btn_ValidaParametros" runat="server" ImageUrl="~/Iconos/Verificacion.gif" OnClick="btn_ValidaParametros_Click" style="margin-top: 0px" Height="29px" Width="33px" TabIndex="50"   />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>&nbsp;
            <br />
            <asp:Label ID="EFechaInicial" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="F. Inicial"></asp:Label>
            &nbsp;
            <asp:TextBox ID="tbx_FechaInicial" runat="server" AutoPostBack="True" BackColor="#66FF33" Enabled="False" Font-Bold="True" ForeColor="Red" OnTextChanged="tbx_FechaInicial_TextChanged" TabIndex="70" TextMode="Date" Width="74px"></asp:TextBox>
            &nbsp;
            <asp:Label ID="EFechaFinal" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="F. Final"></asp:Label>
            &nbsp;
            <asp:TextBox ID="tbx_FechaFinal" runat="server" AutoPostBack="True" BackColor="#66FF33" Enabled="False" Font-Bold="True" ForeColor="Red" OnTextChanged="tbx_FechaFinal_TextChanged" TabIndex="80" TextMode="Date" Width="79px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_RevisarInformacion" runat="server" Height="19px" ImageUrl="~/Iconos/Proceso.png" OnClick="btn_RevisarInformacion_Click" style="margin-top: 0px" TabIndex="60" ToolTip="Click para habilitar Procesos de Revisión de Información" Visible="False" Width="31px" />
            &nbsp;&nbsp;<asp:ImageButton ID="btn_ErrorDatosCompras" runat="server" ImageUrl="~/Iconos/Revisar.GIF" OnClick="btn_ErrorDatosCompras_Click" style="margin-top: 0px" TabIndex="60" ToolTip="Click para revisar Compras con Novedades" Visible="False" />
            &nbsp; &nbsp;
            <asp:Button ID="btn_GeneradorATS" runat="server" OnClick="btn_GeneradorATS_Click" TabIndex="100" Text="Genera ATS " ToolTip="Click para Generar el ATS del Periodo en XML" Visible="False" Width="103px" BackColor="#FFFF66" Font-Bold="True" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_DescargaATS" runat="server" OnClick="btn_DescargaATS_Click" TabIndex="110" Text="Descargar ATS" ToolTip="Click para Grabar el ATS Generado" Visible="False" Width="122px" Font-Bold="True" />
            &nbsp;&nbsp;<asp:Button ID="btn_RetencionesAnuladas" runat="server" Font-Bold="True" ForeColor="#CC0000" OnClick="btn_RetencionesAnuladas_Click" TabIndex="320" Text="Retenciones Anuladas" ToolTip="Click para  Verificar Retenciones Anuladas" Visible="False" Width="172px" />
            &nbsp;<asp:Button ID="btn_FacturasAnuladas" runat="server" Font-Bold="True" ForeColor="#CC0000" OnClick="btn_FacturasAnuladas_Click" TabIndex="320" Text="Facturas Anuladas" ToolTip="Click para  Verificar Facturas Anuladas" Visible="False" Width="143px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            &nbsp;</asp:Panel>
        <asp:Panel ID="pnl_Procesos" runat="server" BackColor="#FFFF99" Visible="False" Width="999px">
            <asp:Button ID="btn_VerificaCompras" runat="server"  TabIndex="310" Text="Compras Periodo" ToolTip="Click para  Verificar Compras" Width="120px" OnClick="btn_VerificaCompras_Click" />
            &nbsp;&nbsp;
            <asp:Label ID="EProveedor" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Id.Proveed." Visible="False" Width="72px"></asp:Label>
            <asp:TextBox ID="tbx_IdProveedor" runat="server" AutoPostBack="True"  TabIndex="200" Visible="False" Width="117px"></asp:TextBox>
            <asp:Button ID="btn_VerificaComprasXProveedor" runat="server" OnClick="btn_VerificaComprasXProveedor_Click" style="text-align: center" TabIndex="300" Text="Compras X Proveedor" ToolTip="Click para  Verificar Compras" Visible="False" Width="163px" />
            &nbsp;&nbsp;<asp:Label ID="EDiario" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="N° Diario" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_NDiario" runat="server" AutoPostBack="True" TabIndex="200" Visible="False" Width="69px"></asp:TextBox>
            &nbsp;
            <asp:Button ID="btn_VerificaComprasDiario" runat="server" OnClick="btn_VerificaComprasDiario_Click" style="text-align: center" TabIndex="300" Text="Compras X Diario" ToolTip="Click para  Verificar Compras" Visible="False" Width="134px" />
            &nbsp;&nbsp;<br />
            <asp:Button ID="btn_VerificaVentasPeriodo" runat="server" OnClick="btn_VerificaVentasPeriodo_Click" TabIndex="310" Text="Ventas Periodo" ToolTip="Click para  Verificar Ventas Periodo" Visible="False" Width="120px" />
            &nbsp;&nbsp;&nbsp;<asp:Label ID="ECliente" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Id.Cliente" Visible="False" Width="72px"></asp:Label>
            <asp:TextBox ID="tbx_IdCliente" runat="server" AutoPostBack="True" TabIndex="210" Visible="False" Width="117px"></asp:TextBox>
            <asp:Button ID="btn_VerificaVentas" runat="server" OnClick="btn_VerificaVentas_Click" TabIndex="310" Text="Ventas X Cliente" ToolTip="Click para  Verificar Ventas Cliente" Visible="False" Width="163px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
        <br />
        <asp:Panel ID="pnl_DetalleCompra" runat="server" Height="153px" Visible="False" BackColor="#99CCFF">
            <asp:Label ID="ESustento" runat="server" Text="Sustento" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddl_Sustento" runat="server" Width="94px" DataSourceID="ds_Sustentos" DataTextField="DetalleSustento" DataValueField="CodigoSustento" TabIndex="201" Height="19px" Font-Size="X-Small" ToolTip="Escoja el Tipo de Sustento Tributario de la Transaccion" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="ddl_Sustento_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_codSustento" runat="server" Width="22px" Visible="False" TabIndex="200"></asp:TextBox>
            &nbsp;<asp:Label ID="EDatProveedor" runat="server" Text="Proveedor" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddl_TipoIdContrib" runat="server" Width="121px" DataSourceID="ds_TipoBenef" DataTextField="DetalleBenef" DataValueField="TipoIdCompras" TabIndex="35" Height="16px" OnSelectedIndexChanged="ddl_TipoIdContrib_SelectedIndexChanged" ToolTip="Escoja el Tipo de Identificacion del Beneficiario Relacionado con el Diario" Visible="False" AutoPostBack="True" BackColor="White" Font-Size="X-Small">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_tipIdProv" runat="server" Width="16px" Visible="False" TabIndex="200" ToolTip="Codigo del tipo de Proveedor"></asp:TextBox>
            &nbsp;<asp:TextBox ID="tbx_IdProveedN" runat="server" Width="120px" Visible="False" TabIndex="200" ToolTip="Identificación del Proveedor"></asp:TextBox>
            <asp:Label ID="EFEmision" runat="server" Text="Emision" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_FEmision" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Fecha de Emision del Documento fuente" Visible="False" Width="74px"></asp:TextBox>
            <br />
            <asp:Label ID="EComprobante" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Doc. Fuente" Visible="False"></asp:Label>
            &nbsp;&nbsp;<asp:DropDownList ID="ddl_TipoDocFuente" runat="server" AutoPostBack="True" DataSourceID="ds_TipoFuentes" DataTextField="DetalleDocumento" DataValueField="CodigoDocumento" Font-Size="X-Small" Height="16px" OnSelectedIndexChanged="ddl_TipoDocFuente_SelectedIndexChanged" TabIndex="100" ToolTip="Escoja el Tipo de Documento Fuente" Visible="False" Width="131px">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_tipComprob" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Visible="False" Width="21px" ToolTip="Codigo del Documento fuente"></asp:TextBox>
            <asp:Label ID="EComprobante0" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="N°" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_estab" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Visible="False" Width="27px" ToolTip="Establecimiento del proveedor que emite el documento fuente"></asp:TextBox>
            <asp:TextBox ID="tbx_punto" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Visible="False" Width="25px" ToolTip="Punto de venta del establecimiento que emite el documento fuente"></asp:TextBox>
            <asp:TextBox ID="tbx_secuen" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Visible="False" Width="60px" ToolTip="Numero secuencial de documento fuente"></asp:TextBox>
            <asp:Label ID="EComprobante1" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Autorización Fuente" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_autoriza" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Visible="False" Width="306px" ToolTip="Numero de Autorizacion del Documento fuente"></asp:TextBox>
            &nbsp;&nbsp;
            <br />
            <asp:Label ID="EBases" runat="server" Text="Bas.IVA 0%" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            &nbsp;<asp:TextBox ID="tbx_valorConIva0" runat="server" Width="50px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Valor de base con IVA 0"></asp:TextBox>
            <asp:Label ID="EBasesGravIVA" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Bas.Grav.IVA" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_valorGravIVA" runat="server" Width="50px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor de Base con IVA diferente de 0"></asp:TextBox>
            <asp:Label ID="EIVA" runat="server" Text="IVA" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_valorIVA" runat="server" Width="50px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor del IVA"></asp:TextBox>
            <asp:Label ID="EICE" runat="server" Text="ICE" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_valorICE" runat="server" Width="50px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor del ICE"></asp:TextBox>
            <asp:Label ID="ERetenIVA" runat="server" Text="Ret.IVA" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_RIVA30" runat="server" Width="50px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor de Retencion IVA 30%"></asp:TextBox>
            <asp:TextBox ID="tbx_RIVA70" runat="server" Width="50px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor de Retencion IVA 70%"></asp:TextBox>
            <asp:TextBox ID="tbx_RIVA100" runat="server" Width="50px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor de Retencion IVA 100%"></asp:TextBox>
            <br />
            <asp:Label ID="ERetenIRB" runat="server" Text="Ret.IRB" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddl_TipoBienes" runat="server" Width="92px" DataSourceID="ds_ConceptosB" DataTextField="ConceptoIR" DataValueField="CodConcepto" TabIndex="205" ToolTip="Escoja el Tipo de Bienes de la Transacción" Visible="False" AutoPostBack="True" Font-Size="X-Small" BackColor="White" OnSelectedIndexChanged="ddl_TipoBienes_SelectedIndexChanged" Height="16px">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_codIRB" runat="server" Width="22px" Visible="False" TabIndex="200" style="font-size: x-small" ToolTip="Codigo del Tipo de bienes de la transacción"></asp:TextBox>
            <asp:TextBox ID="tbx_baseIRB" runat="server" Width="50px" Visible="False" Font-Size="Small" TabIndex="200" style="text-align: right; font-size: x-small" ToolTip="Base Imponible para retencion de Impuesto renta en bienes"></asp:TextBox>
            <asp:TextBox ID="tbx_tasaIRB" runat="server" Width="27px" Visible="False" TabIndex="200" style="font-size: x-small" ToolTip="Porcentaje de Retencion de Impuesto a la renta en este tipo de bienes"></asp:TextBox>
            <asp:TextBox ID="tbx_RetenIRB" runat="server" Width="50px" Visible="False" Font-Size="Small" TabIndex="200" style="text-align: right; font-size: x-small" ToolTip="Valor retenido por impuesto a la renta en bienes"></asp:TextBox>
            &nbsp;<asp:Label ID="ERetenIRS" runat="server" Text="Ret.IRS" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False" style="text-align: left"></asp:Label>
            <asp:DropDownList ID="ddl_TipoServicios" runat="server" Width="99px" DataSourceID="ds_ConceptosS" DataTextField="ConceptoIR" DataValueField="CodConcepto" TabIndex="305" Height="20px" ToolTip="Escoja el Tipo de Servicio para retencion Impueto Renta" Visible="False" AutoPostBack="True" Font-Size="X-Small" BackColor="White" OnSelectedIndexChanged="ddl_TipoServicios_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_codIRS" runat="server" Width="22px" Visible="False" TabIndex="200" style="font-size: x-small" ToolTip="Codigo del tipo de servicios "></asp:TextBox>
            <asp:TextBox ID="tbx_baseIRS" runat="server" Width="50px" Visible="False" Font-Size="Small" TabIndex="200" style="text-align: right; font-size: x-small;" ToolTip="Base imponible para retencion de impuesto a la renta por este tipo de servicios"></asp:TextBox>
            <asp:TextBox ID="tbx_tasaIRS" runat="server" Width="27px" Visible="False" TabIndex="200" style="font-size: x-small" ToolTip="Porcentaje de retencion de impuesto a la renta para este tipo de servicio"></asp:TextBox>
            <asp:TextBox ID="tbx_RetenIRS" runat="server" Width="50px" Visible="False" Font-Size="Small" TabIndex="200" style="text-align: right; font-size: x-small;" ToolTip="Valor de retencion en la fuente por este tipo de servicios"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;<br />
            <asp:Label ID="EComprobReten" runat="server" Text="Retencion N°" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_estabR" runat="server" Width="27px" Visible="False"  TabIndex="200" Font-Bold="True" Font-Size="X-Small" ToolTip="Establecimientop de la institucion que emitio el comprobante de retencion"></asp:TextBox>
            <asp:TextBox ID="tbx_ptoEmiR" runat="server" Width="27px" Visible="False"  TabIndex="200" Font-Bold="True" Font-Size="X-Small" ToolTip="Punto de emision de la institucion que emitio el comprobante de retencion"></asp:TextBox>
            <asp:TextBox ID="tbx_secuenR" runat="server" Width="60px" Visible="False" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Numero secuencial del comprobante de retencion emitido"></asp:TextBox>
            <asp:TextBox ID="tbx_autorizaR" runat="server" Width="236px" Visible="False" Font-Size="X-Small" TabIndex="200" Font-Bold="True" ToolTip="Numero de autorizacion del comprobante de retencion emitido"></asp:TextBox>
            &nbsp;<asp:Label ID="EFRegistro" runat="server" Text="Registro" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_FRegistro" runat="server" Width="74px" Visible="False" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Fecha de Registro de la transaccion" ></asp:TextBox>
            <asp:Label ID="EParteRelCompras" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Relacionada" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_ParteRelCompras" runat="server" Font-Size="Small" Height="19px" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="SI/NO dependiendo de si el proveedor es parte relacionada a la Institución" Visible="False" Width="27px"></asp:TextBox>
            <br />
            <asp:Label ID="EClaveCompra" runat="server" Text="KC" BackColor="White" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
            <asp:TextBox ID="tbx_NumDiario" runat="server" Width="56px" Visible="False" AutoPostBack="True" TabIndex="200" Enabled="False" Font-Bold="False" Font-Size="X-Small"></asp:TextBox>
            &nbsp;<asp:TextBox ID="tbx_IdProvA" runat="server" Width="100px" Visible="False" AutoPostBack="True" TabIndex="200" Enabled="False" Font-Bold="False" Font-Size="X-Small"></asp:TextBox>
            <asp:TextBox ID="tbx_SecuenA" runat="server" Width="56px" Visible="False" AutoPostBack="True"  TabIndex="200" Enabled="False" Font-Bold="False" Font-Size="X-Small"></asp:TextBox>
            &nbsp;<asp:ImageButton ID="btn_HabilitaActualCompras" runat="server" ImageUrl="~/Iconos/Chequear.gif" style="margin-top: 0px" Height="20px" Width="20px" TabIndex="50" OnClick="btn_HabilitaActualCompras_Click" Visible="False"   />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_ActualizaCompras" runat="server" Height="18px" ImageUrl="~/Iconos/Grabar.gif" Width="18px" TabIndex="150" ToolTip="Click para Grabar los Datos Ingresados del Comprobante " OnClick="btn_ActualizaCompras_Click" BorderColor="Red" BorderStyle="None" Visible="False" />
            &nbsp;&nbsp;&nbsp;
        </asp:Panel>
        <br/>
        <asp:Panel ID="pnl_Compras" runat="server" Height="180px" ScrollBars="Both" Visible="False" BackColor="#99FFCC" Width="990px">
            &nbsp;
            <asp:Label ID="ECompras" runat="server" Font-Bold="True" Text="Compras Generales"></asp:Label>
            &nbsp;
            <asp:Label ID="EComprasProv" runat="server" Font-Bold="True" Text="Compras por Proveedor"></asp:Label>
            &nbsp;
            <asp:Label ID="EComprasDiario" runat="server" Font-Bold="True" Text="Compras por Diario"></asp:Label>
            <asp:GridView ID="dgv_Compras" runat="server" AutoGenerateColumns="False" DataSourceID="TCompras" PageSize="9" OnSelectedIndexChanged="dgv_Compras_SelectedIndexChanged" TabIndex="400">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                    <asp:BoundField DataField="codSustento" HeaderText="Sto" SortExpression="codSustento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tpIdProv" HeaderText="tId" SortExpression="tpIdProv">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idProv" HeaderText="idProv" SortExpression="idProv">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" BackColor="#CCFFCC" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipoComprobante" HeaderText="tCte" SortExpression="tipoComprobante">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="parteRel" HeaderText="pR" SortExpression="parteRel">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaRegistro" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fReg" SortExpression="fechaRegistro">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="establecimiento" HeaderText="est" SortExpression="establecimiento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="puntoEmision" HeaderText="pEm" SortExpression="puntoEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secuencial" HeaderText="secuen" SortExpression="secuencial">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmis" SortExpression="fechaEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autorizacion" HeaderText="autorizacion" SortExpression="autorizacion">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Bold="True" Font-Size="XX-Small" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Diario" HeaderText="Diario" SortExpression="Diario">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImponible" DataFormatString="{0:0.00}" HeaderText="iva0" SortExpression="baseImponible">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpGrav" DataFormatString="{0:0.00}" HeaderText="GravIVA" SortExpression="baseImpGrav">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIva" DataFormatString="{0:0.00}" HeaderText="Iva" SortExpression="montoIva">
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetBienes" DataFormatString="{0:0.00}" HeaderText="RetB30" SortExpression="valorRetBienes">
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetServicios" DataFormatString="{0:0.00}" HeaderText="RetS70" SortExpression="valorRetServicios">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ100" DataFormatString="{0:0.00}" HeaderText="RetS100" SortExpression="valRetServ100">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estabRetencion1" HeaderText="eRet1" SortExpression="estabRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secRetencion1" HeaderText="secR1" SortExpression="secRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autRetencion1" HeaderText="autRet1" SortExpression="autRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmiRet1" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmiRet1" SortExpression="fechaEmiRet1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoB" HeaderText="CodRB" SortExpression="CodConceptoB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRB" DataFormatString="{0:0.00}" HeaderText="BaseIRB" SortExpression="BaseImponIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRB" DataFormatString="{0:0.00}" HeaderText="TRetIRB" SortExpression="TasaRetIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRB" DataFormatString="{0:0.00}" HeaderText="VRetIRB" SortExpression="ValorRetIRB">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoS" HeaderText="CodRS" SortExpression="CodConceptoS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRS" DataFormatString="{0:0.00}" HeaderText="BaseIRS" SortExpression="BaseImponIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRS" DataFormatString="{0:0.00}" HeaderText="TRetIRS" SortExpression="TasaRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRS" DataFormatString="{0:0.00}" HeaderText="VRetIRS" SortExpression="ValorRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIce" DataFormatString="{0:0.00}" HeaderText="Ice" SortExpression="montoIce">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetBien10" DataFormatString="{0:#.##}" HeaderText="Ret10" SortExpression="valRetBien10">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ20" DataFormatString="{0:0.00}" HeaderText="Ret20" SortExpression="valRetServ20">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ50" DataFormatString="{0:0.00}" HeaderText="Ret50" SortExpression="valRetServ50">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseNoGraIVA" DataFormatString="{0:0.00}" HeaderText="NoIVA" SortExpression="baseNoGraIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpExe" HeaderText="ExeIVA" SortExpression="baseImpExe">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="formaPago" HeaderText="fPg" SortExpression="formaPago">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ptoEmiRetencion1" HeaderText="ptoE1" SortExpression="ptoEmiRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="parteRel" HeaderText="pRel" SortExpression="parteRel">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnl_ComprasProveedor" runat="server" Height="180px" ScrollBars="Both" Visible="False" BackColor="#99FFCC" Width="990px">
            <asp:Label ID="EComprasProveedor" runat="server" Font-Bold="True" Text="Compras por Proveedor"></asp:Label>
            <asp:GridView ID="dgv_ComprasProveedor" runat="server" AutoGenerateColumns="False" DataSourceID="TComprasP" PageSize="9" OnSelectedIndexChanged="dgv_Compras_SelectedIndexChanged" TabIndex="400">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                    <asp:BoundField DataField="codSustento" HeaderText="Sto" SortExpression="codSustento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tpIdProv" HeaderText="tId" SortExpression="tpIdProv">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idProv" HeaderText="idProv" SortExpression="idProv">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" BackColor="#CCFFCC" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipoComprobante" HeaderText="tCte" SortExpression="tipoComprobante">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="parteRel" HeaderText="pR" SortExpression="parteRel">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaRegistro" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fReg" SortExpression="fechaRegistro">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="establecimiento" HeaderText="est" SortExpression="establecimiento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="puntoEmision" HeaderText="pEm" SortExpression="puntoEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secuencial" HeaderText="secuen" SortExpression="secuencial">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmis" SortExpression="fechaEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autorizacion" HeaderText="autorizacion" SortExpression="autorizacion">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Bold="True" Font-Size="XX-Small" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Diario" HeaderText="Diario" SortExpression="Diario">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImponible" DataFormatString="{0:0.00}" HeaderText="iva0" SortExpression="baseImponible">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpGrav" DataFormatString="{0:0.00}" HeaderText="GravIVA" SortExpression="baseImpGrav">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIva" DataFormatString="{0:0.00}" HeaderText="Iva" SortExpression="montoIva">
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetBienes" DataFormatString="{0:0.00}" HeaderText="RetB30" SortExpression="valorRetBienes">
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetServicios" DataFormatString="{0:0.00}" HeaderText="RetS70" SortExpression="valorRetServicios">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ100" DataFormatString="{0:0.00}" HeaderText="RetS100" SortExpression="valRetServ100">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estabRetencion1" HeaderText="eRet1" SortExpression="estabRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secRetencion1" HeaderText="secR1" SortExpression="secRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autRetencion1" HeaderText="autRet1" SortExpression="autRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmiRet1" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmiRet1" SortExpression="fechaEmiRet1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoB" HeaderText="CodRB" SortExpression="CodConceptoB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRB" DataFormatString="{0:0.00}" HeaderText="BaseIRB" SortExpression="BaseImponIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRB" DataFormatString="{0:0.00}" HeaderText="TRetIRB" SortExpression="TasaRetIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRB" DataFormatString="{0:0.00}" HeaderText="VRetIRB" SortExpression="ValorRetIRB">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoS" HeaderText="CodRS" SortExpression="CodConceptoS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRS" DataFormatString="{0:0.00}" HeaderText="BaseIRS" SortExpression="BaseImponIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRS" DataFormatString="{0:0.00}" HeaderText="TRetIRS" SortExpression="TasaRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRS" DataFormatString="{0:0.00}" HeaderText="VRetIRS" SortExpression="ValorRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIce" DataFormatString="{0:0.00}" HeaderText="Ice" SortExpression="montoIce">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetBien10" DataFormatString="{0:#.##}" HeaderText="Ret10" SortExpression="valRetBien10">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ20" DataFormatString="{0:0.00}" HeaderText="Ret20" SortExpression="valRetServ20">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ50" DataFormatString="{0:0.00}" HeaderText="Ret50" SortExpression="valRetServ50">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseNoGraIVA" DataFormatString="{0:0.00}" HeaderText="NoIVA" SortExpression="baseNoGraIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpExe" HeaderText="ExeIVA" SortExpression="baseImpExe">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="formaPago" HeaderText="fPg" SortExpression="formaPago">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ptoEmiRetencion1" HeaderText="ptoE1" SortExpression="ptoEmiRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="parteRel" HeaderText="pRel" SortExpression="parteRel">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnl_ComprasXDiario" runat="server" Height="180px" ScrollBars="Both" Visible="False" BackColor="#99FFCC" Width="990px">
            <asp:Label ID="EComprasXDiario" runat="server" Font-Bold="True" Text="Compras por Diario"></asp:Label>
            <asp:GridView ID="dgv_ComprasDiario" runat="server" AutoGenerateColumns="False" DataSourceID="TComprasD" PageSize="9" Visible="False" OnSelectedIndexChanged="dgv_Compras_SelectedIndexChanged" TabIndex="400">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                    <asp:BoundField DataField="codSustento" HeaderText="Sto" SortExpression="codSustento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tpIdProv" HeaderText="tId" SortExpression="tpIdProv">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idProv" HeaderText="idProv" SortExpression="idProv">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" BackColor="#CCFFCC" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipoComprobante" HeaderText="tCte" SortExpression="tipoComprobante">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="parteRel" HeaderText="pR" SortExpression="parteRel">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaRegistro" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fReg" SortExpression="fechaRegistro">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="establecimiento" HeaderText="est" SortExpression="establecimiento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="puntoEmision" HeaderText="pEm" SortExpression="puntoEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secuencial" HeaderText="secuen" SortExpression="secuencial">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmis" SortExpression="fechaEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autorizacion" HeaderText="autorizacion" SortExpression="autorizacion">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Bold="True" Font-Size="XX-Small" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Diario" HeaderText="Diario" SortExpression="Diario">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImponible" DataFormatString="{0:0.00}" HeaderText="iva0" SortExpression="baseImponible">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpGrav" DataFormatString="{0:0.00}" HeaderText="GravIVA" SortExpression="baseImpGrav">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIva" DataFormatString="{0:0.00}" HeaderText="Iva" SortExpression="montoIva">
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetBienes" DataFormatString="{0:0.00}" HeaderText="RetB30" SortExpression="valorRetBienes">
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetServicios" DataFormatString="{0:0.00}" HeaderText="RetS70" SortExpression="valorRetServicios">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ100" DataFormatString="{0:0.00}" HeaderText="RetS100" SortExpression="valRetServ100">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estabRetencion1" HeaderText="eRet1" SortExpression="estabRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secRetencion1" HeaderText="secR1" SortExpression="secRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autRetencion1" HeaderText="autRet1" SortExpression="autRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmiRet1" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmiRet1" SortExpression="fechaEmiRet1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoB" HeaderText="CodRB" SortExpression="CodConceptoB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRB" DataFormatString="{0:0.00}" HeaderText="BaseIRB" SortExpression="BaseImponIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRB" DataFormatString="{0:0.00}" HeaderText="TRetIRB" SortExpression="TasaRetIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRB" DataFormatString="{0:0.00}" HeaderText="VRetIRB" SortExpression="ValorRetIRB">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoS" HeaderText="CodRS" SortExpression="CodConceptoS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRS" DataFormatString="{0:0.00}" HeaderText="BaseIRS" SortExpression="BaseImponIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRS" DataFormatString="{0:0.00}" HeaderText="TRetIRS" SortExpression="TasaRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRS" DataFormatString="{0:0.00}" HeaderText="VRetIRS" SortExpression="ValorRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIce" DataFormatString="{0:0.00}" HeaderText="Ice" SortExpression="montoIce">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetBien10" DataFormatString="{0:#.##}" HeaderText="Ret10" SortExpression="valRetBien10">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ20" DataFormatString="{0:0.00}" HeaderText="Ret20" SortExpression="valRetServ20">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ50" DataFormatString="{0:0.00}" HeaderText="Ret50" SortExpression="valRetServ50">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseNoGraIVA" DataFormatString="{0:0.00}" HeaderText="NoIVA" SortExpression="baseNoGraIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpExe" HeaderText="ExeIVA" SortExpression="baseImpExe">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="formaPago" HeaderText="fPg" SortExpression="formaPago">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ptoEmiRetencion1" HeaderText="ptoE1" SortExpression="ptoEmiRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="parteRel" HeaderText="pRel" SortExpression="parteRel">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnl_VentasCliente" runat="server" Height="180px" ScrollBars="Vertical" Visible="False" Width="900px" BackColor="#FFFF66">
            <asp:Label ID="EVentasXCliente" runat="server" Font-Bold="True" Text="Ventas por Cliente"></asp:Label>
            <asp:GridView ID="dgv_Ventas" runat="server" AutoGenerateColumns="False" DataSourceID="TVentasCliente" TabIndex="410" Visible="False">
                <Columns>
                    <asp:BoundField DataField="TipoComprobante" HeaderText="tC" SortExpression="TipoComprobante">
                    <HeaderStyle Font-Size="X-Small" Width="10px" />
                    <ItemStyle Font-Size="Small" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumFactura" HeaderText="Factura" SortExpression="NumFactura">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoIdentificacion" HeaderText="tI" SortExpression="TipoIdentificacion">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" SortExpression="Identificacion">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ParteRelVenta" HeaderText="pR" SortExpression="ParteRelVenta">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoEmision" HeaderText="tE" SortExpression="TipoEmision">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="X-Small" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseNoGraIVA" DataFormatString="{0:0.00}" HeaderText="bNoIVA" SortExpression="baseNoGraIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImponible" DataFormatString="{0:0.00}" HeaderText="bIVA0" SortExpression="baseImponible">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpGrav" HeaderText="bconIVA" SortExpression="baseImpGrav">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorIVA" HeaderText="IVA" SortExpression="ValorIVA">
                    <HeaderStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RetencionIVA" HeaderText="RetenIVA" SortExpression="RetencionIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RetencionIR" DataFormatString="{0:0.00}" HeaderText="RetenIR" SortExpression="RetencionIR">
                    <HeaderStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FormaPago" HeaderText="fP" SortExpression="FormaPago">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaFactura" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fFactura" SortExpression="FechaFactura">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:0.00}" HeaderText="Efectivo" SortExpression="Efectivo">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" Width="25px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" SortExpression="NombreCompleto">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="XX-Small" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br/>
        <asp:Panel ID="pnl_DetalleVentas" runat="server" BackColor="#FFCC66" Visible="False" Width="990px">
            <asp:Label ID="EComprobanteVenta" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Comprob. Venta" Width="100px"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddl_TipoDocVenta" runat="server" AutoPostBack="True" DataSourceID="ds_TipoFuentes" DataTextField="DetalleDocumento" DataValueField="CodigoDocumento" Font-Size="X-Small" Height="16px" OnSelectedIndexChanged="ddl_TipoDocFuente_SelectedIndexChanged" TabIndex="100" ToolTip="Escoja el Tipo de Documento Venta" Width="131px">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_tipComprobVenta" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Width="21px"></asp:TextBox>
            <asp:Label ID="EFechaFactura" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Fecha"></asp:Label>
            <asp:TextBox ID="tbx_FechaComprobVenta" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Valor de base con IVA 0" Width="74px"></asp:TextBox>
            <asp:Label ID="ENumComprobVenta" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="N°"></asp:Label>
            <asp:TextBox ID="tbx_estabVenta" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Width="27px"></asp:TextBox>
            <asp:TextBox ID="tbx_puntoVenta" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Width="25px"></asp:TextBox>
            <asp:TextBox ID="tbx_NumComprobVenta" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Width="60px"></asp:TextBox>
            <asp:TextBox ID="tbx_autorizaVenta" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" Width="351px"></asp:TextBox>
            <br />
            <asp:Label ID="EDatCliente" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Tipo Cliente"></asp:Label>
            <asp:DropDownList ID="ddl_TipoIdCliente" runat="server" AutoPostBack="True" BackColor="White" DataSourceID="ds_TipoBenef" DataTextField="DetalleBenef" DataValueField="TipoIdCompras" Font-Size="X-Small" Height="16px" OnSelectedIndexChanged="ddl_TipoIdContrib_SelectedIndexChanged" TabIndex="35" ToolTip="Escoja el Tipo de Identificacion del Beneficiario Relacionado con el Diario" Width="103px">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_tipIdCliente" runat="server" Width="16px" TabIndex="200"></asp:TextBox>
            <asp:TextBox ID="tbx_IdenCliente" runat="server" TabIndex="200" Width="111px"></asp:TextBox>
            <asp:Label ID="EFPago" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Forma de Pago"></asp:Label>
            <asp:DropDownList ID="ddl_FormaPagoV" runat="server" AutoPostBack="True" DataSourceID="ds_FormasPago" DataTextField="DetalleFPago" DataValueField="CodigoFPago" Font-Size="X-Small" Height="19px" OnSelectedIndexChanged="ddl_Sustento_SelectedIndexChanged" TabIndex="201" ToolTip="Escoja el Tipo de Sustento Tributario de la Transaccion" Width="94px">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_FormaPagoV" runat="server" TabIndex="200" Width="22px"></asp:TextBox>
            <asp:Label ID="EPRelacion" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Relacionada"></asp:Label>
            <asp:TextBox ID="tbx_ParteRelacion" runat="server" TabIndex="200" ToolTip="Si/NO" Width="22px"></asp:TextBox>
            <asp:Label ID="EEmision" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Emision" ToolTip="F=Fisica / E=Electronica"></asp:Label>
            <asp:TextBox ID="tbx_TipoEmision" runat="server" TabIndex="200" Width="22px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="EClaveVenta" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="KV"></asp:Label>
            <asp:TextBox ID="tbx_estabVentaA" runat="server" Enabled="False" Font-Bold="True" Font-Size="X-Small" ReadOnly="True" TabIndex="200" Width="27px"></asp:TextBox>
            <asp:TextBox ID="tbx_puntoVentaA" runat="server" Enabled="False" Font-Bold="True" Font-Size="X-Small" ReadOnly="True" TabIndex="200" Width="25px"></asp:TextBox>
            <asp:TextBox ID="tbx_NumComprobVentaA" runat="server" AutoPostBack="True" Enabled="False" Font-Bold="False" Font-Size="X-Small" TabIndex="200" Width="56px"></asp:TextBox>
            <br />
            <asp:Label ID="EBasesNoGraIVA" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Bas. No IVA"></asp:Label>
            &nbsp;<asp:TextBox ID="tbx_VentaExenIVA" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Valor de base con IVA 0" Width="50px"></asp:TextBox>
            <asp:Label ID="EBasesIVA0" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Bas. IVA 0%"></asp:Label>
            <asp:TextBox ID="tbx_VentaConIVA0" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Valor de base con IVA 0" Width="50px"></asp:TextBox>
            <asp:Label ID="EBases1" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Bas.IVA"></asp:Label>
            <asp:TextBox ID="tbx_VentaGraIVA" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor de Base con IVA diferente de 0" Width="50px"></asp:TextBox>
            <asp:Label ID="EIVA0" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="IVA"></asp:Label>
            <asp:TextBox ID="tbx_ValorIVAenVenta" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor del IVA" Width="50px"></asp:TextBox>
            <asp:Label ID="ERetenIVA0" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Ret.IVA"></asp:Label>
            <asp:TextBox ID="tbx_RetIVAVentas" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Valor de Retencion IVA 30%" Width="50px"></asp:TextBox>
            <asp:Label ID="ERetenIRVenta" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Ret.IRB"></asp:Label>
            <asp:TextBox ID="tbx_RetenIRVenta" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small" TabIndex="200" Width="50px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="btn_HabilitaActualVenta" runat="server" Height="20px" ImageUrl="~/Iconos/Chequear.gif" style="margin-top: 0px" TabIndex="50" Width="20px" OnClick="btn_HabilitaActualVenta_Click" />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_ActualizaVenta" runat="server" BorderColor="Red" BorderStyle="None" Height="18px" ImageUrl="~/Iconos/Grabar.gif" TabIndex="150" ToolTip="Click para Grabar los Datos Ingresados del Comprobante " Visible="False" Width="18px" OnClick="btn_ActualizaVenta_Click" />

            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;

        </asp:Panel>
        <asp:Panel ID="pnl_FacturasAnuladas" runat="server" Height="270px" Visible="False" Width="900px" BackColor="#FF6666" ScrollBars="Vertical">
            <asp:Label ID="EFacturasAnuladas" runat="server" Font-Bold="True" ForeColor="Red" Text="Facturas Anuladas"></asp:Label>
            <asp:GridView ID="dgv_FacturasAnuladas" runat="server" AutoGenerateColumns="False" DataKeyNames="NumSucursal,NumCaja,NumFactura" DataSourceID="TFacturasAnuladas" Visible="False" TabIndex="420" BackColor="#FFCC99" PageSize="5" AllowPaging="True" OnSelectedIndexChanged="dgv_FacturasAnuladas_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                    <asp:BoundField DataField="TipoComprobante" HeaderText="tC" SortExpression="TipoComprobante" />
                    <asp:BoundField DataField="NumSucursal" HeaderText="Estab." ReadOnly="True" SortExpression="NumSucursal" />
                    <asp:BoundField DataField="NumFactura" HeaderText="Factura" ReadOnly="True" SortExpression="NumFactura" />
                    <asp:BoundField DataField="NumAutorizacion" HeaderText="Autorizacion" SortExpression="NumAutorizacion" />
                </Columns>
                <PagerSettings FirstPageImageUrl="~/Images/Primero.png" LastPageImageUrl="~/Images/Ultimo.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/Siguiente.png" PageButtonCount="5" PreviousPageImageUrl="~/Images/Anterior.png" />
            </asp:GridView>
            <asp:Label ID="ETComprobante" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Comprobante Venta Anulado "></asp:Label>
            <asp:DropDownList ID="ddl_TipoDocVentaAnul" runat="server" AutoPostBack="True" DataSourceID="ds_TipoFuentes" DataTextField="DetalleDocumento" DataValueField="CodigoDocumento" Font-Size="X-Small" Height="16px" OnSelectedIndexChanged="ddl_TipoDocFuente_SelectedIndexChanged" TabIndex="100" ToolTip="Escoja el Tipo de Documento Venta" Width="131px">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_TipoDocVentaAnul" runat="server" TabIndex="200" Width="22px"></asp:TextBox>
            <asp:Label ID="EComprobante2" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="N°"></asp:Label>
            <asp:TextBox ID="tbx_estabDocVentaAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Establecimiento del proveedor que emite el documento fuente" Width="27px"></asp:TextBox>
            <asp:TextBox ID="tbx_puntoDocVentaAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Punto de venta del establecimiento que emite el documento fuente" Width="25px"></asp:TextBox>
            <asp:TextBox ID="tbx_secuenDocVentaAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Numero secuencial de documento fuente" Width="60px"></asp:TextBox>
            <asp:Label ID="EFechaAnulacion" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Fecha Anulación Comprobante Venta"></asp:Label>
            <asp:TextBox ID="tbx_FechaAnulDocVenta" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Numero secuencial de documento fuente" Width="82px"></asp:TextBox>
            <br />
            <asp:Label ID="EAutorDocVentaAnul" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Autorización Comprobante"></asp:Label>
            <asp:TextBox ID="tbx_AutorDocVentaAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Numero secuencial de documento fuente" Width="341px"></asp:TextBox>
&nbsp;
            <asp:ImageButton ID="btn_GrabaDocVentaAnul" runat="server" BorderColor="Red" BorderStyle="None" Height="18px" ImageUrl="~/Iconos/Grabar.gif" OnClick="btn_ActualizaCompras_Click" TabIndex="150" ToolTip="Click para Grabar los Datos Ingresados del Comprobante " Visible="False" Width="18px" />
&nbsp;
            <asp:ImageButton ID="btn_ModificaDocVentaAnul" runat="server" BorderColor="Red" BorderStyle="None" CausesValidation="False" Height="18px" ImageUrl="~/Iconos/Modificar1.png"  TabIndex="150" ToolTip="Click para grabar cambios en la cabecera del Comprobante" Visible="False" Width="19px" />
            <br />
            <asp:ImageButton ID="btn_NuevoDocVentaAnul" runat="server" Height="20px" ImageUrl="~/Iconos/AddFinances.gif"  TabIndex="297" ToolTip="Click para Ingresar Nueva Transacción" Width="24px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_HabilitaModDocVentaAnul" runat="server" Height="20px" ImageUrl="~/Iconos/Chequear.gif"  style="margin-top: 0px" TabIndex="50" Visible="False" Width="20px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_EliminaDocVentaAnul" runat="server" BorderColor="Red" Height="23px" ImageUrl="~/Iconos/Eliminar.gif" TabIndex="299" ToolTip="Click para Eliminar Transaccion Contable" Visible="False" Width="26px" />
            <br />
        </asp:Panel>
        <br />
        <asp:Panel ID="pnl_VentasPeriodo" runat="server" Height="225px" ScrollBars="Vertical" Visible="False" Width="999px" BackColor="#FFFF66">
            &nbsp;&nbsp;&nbsp;<asp:Label ID="EVentasPeriodo" runat="server" Font-Bold="True" Text="Ventas del Periodo"></asp:Label>
            &nbsp;
            <asp:Label ID="EVentasCliente" runat="server" Font-Bold="True" Text="Ventas por Cliente"></asp:Label>
            <br />
            <asp:Label ID="ENFacturas" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="T.Facturas"></asp:Label>
            <asp:TextBox ID="tbx_NFacturasT" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Numero Total de facturas emitidas en el periodo" Width="50px"></asp:TextBox>
            &nbsp; &nbsp;<asp:Label ID="EBasesNoGraIVAT" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="T.Excen.IVA"></asp:Label>
            <asp:TextBox ID="tbx_VentaExenIVAT" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Valor de ventas excentas de IVA" Width="50px"></asp:TextBox>
            <asp:Label ID="EBasesIVA0T" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="T. con IVA 0%"></asp:Label>
            <asp:TextBox ID="tbx_VentaConIVA0T" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small;" TabIndex="200" ToolTip="Tota Ventas con IVA 0" Width="50px"></asp:TextBox>
            <asp:Label ID="EBaseGraIVAT" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="T. Grav. IVA"></asp:Label>
            <asp:TextBox ID="tbx_VentaGraIVAT" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="VTotal Ventas Gravadas IVA" Width="64px"></asp:TextBox>
            <asp:Label ID="EIVAT" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="T.IVA"></asp:Label>
            <asp:TextBox ID="tbx_IVAVentasT" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Total de lVA garbado en Ventas" Width="50px"></asp:TextBox>
            <asp:Label ID="ERetenIVAT" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="T.Ret.IVA"></asp:Label>
            <asp:TextBox ID="tbx_RetIVAVentasT" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small; font-weight: 700;" TabIndex="200" ToolTip="Total que nos Retuvieron de IVA en Ventas" Width="50px"></asp:TextBox>
            <asp:Label ID="ERetenIRVentaT" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="T.Ret.IR"></asp:Label>
            <asp:TextBox ID="tbx_RetenIRVentaT" runat="server" Font-Size="Small" style="text-align: right; font-size: x-small; height: 19px;" TabIndex="200" ToolTip="Total que nos retuvieron de IR en Ventas" Width="50px"></asp:TextBox>
            &nbsp;<asp:GridView ID="dgv_VentasPeriodo" runat="server" AutoGenerateColumns="False" DataSourceID="TVentasPeriodo" OnSelectedIndexChanged="dgv_VentasPeriodo_SelectedIndexChanged" PageSize="5" TabIndex="410" Visible="False" Width="637px">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                    <asp:BoundField DataField="TipoComprobante" HeaderText="tC" SortExpression="TipoComprobante">
                    <HeaderStyle Font-Size="X-Small" Width="10px" Wrap="False" />
                    <ItemStyle Font-Size="Small" Width="10px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumSucursal" HeaderText="estab" SortExpression="NumSucursal">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumCaja" HeaderText="punto" SortExpression="NumCaja">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumFactura" HeaderText="Factura" SortExpression="NumFactura">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoIdentificacion" HeaderText="tI" SortExpression="TipoIdentificacion">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" SortExpression="Identificacion">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ParteRelVenta" HeaderText="pR" SortExpression="ParteRelVenta">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoEmision" HeaderText="tE" SortExpression="TipoEmision">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="X-Small" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseNoGraIVA" DataFormatString="{0:0.00}" HeaderText="bNoIVA" SortExpression="baseNoGraIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImponible" DataFormatString="{0:0.00}" HeaderText="bIVA0" SortExpression="baseImponible">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpGrav" DataFormatString="{0:0.00}" HeaderText="bconIVA" SortExpression="baseImpGrav">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorIVA" DataFormatString="{0:0.00}" HeaderText="IVA" SortExpression="ValorIVA">
                    <HeaderStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RetencionIVA" DataFormatString="{0:0.00}" HeaderText="RetenIVA" SortExpression="RetencionIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RetencionIR" DataFormatString="{0:0.00}" HeaderText="RetenIR" SortExpression="RetencionIR">
                    <HeaderStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FormaPago" HeaderText="fP" SortExpression="FormaPago">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaFactura" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fFactura" SortExpression="FechaFactura">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:0.00}" HeaderText="Efectivo" SortExpression="Efectivo">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" Width="25px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" SortExpression="NombreCompleto">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="XX-Small" Wrap="False" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings FirstPageImageUrl="~/Images/Primero.png" LastPageImageUrl="~/Images/Ultimo.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/Siguiente.png" PreviousPageImageUrl="~/Images/Anterior.png" />
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnl_RetencionesAnuladas" runat="server" Height="270px" Visible="False" Width="900px" BackColor="#66FF66" ScrollBars="Vertical">
            <asp:Label ID="ERetenAnuladas" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Retenciones Anuladas en el Periodo"></asp:Label>
            <asp:GridView ID="dgv_RetenAnuladas" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="#9999FF" DataKeyNames="SucReten,NumReten,AutorizaReten" DataSourceID="TRetenAnuladas" OnSelectedIndexChanged="dgv_RetenAnuladas_SelectedIndexChanged" PageSize="5" TabIndex="420" Visible="False">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                    <asp:BoundField DataField="TipoComprobante" HeaderText="tC" SortExpression="TipoComprobante" />
                    <asp:BoundField DataField="SucReten" HeaderText="Estab." ReadOnly="True" SortExpression="SucReten" />
                    <asp:BoundField DataField="NumReten" HeaderText="Retención" ReadOnly="True" SortExpression="NumReten" />
                    <asp:BoundField DataField="AutorizaReten" HeaderText="Autorizacion" SortExpression="AutorizaReten" />
                </Columns>
                <PagerSettings FirstPageImageUrl="~/Images/Primero.png" LastPageImageUrl="~/Images/Ultimo.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/Siguiente.png" PageButtonCount="5" PreviousPageImageUrl="~/Images/Anterior.png" />
                <SelectedRowStyle BackColor="#FF6600" />
            </asp:GridView>
            <asp:Label ID="ETComprobRetenAnul" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Comprobante Retención Anulado"></asp:Label>
            <asp:TextBox ID="tbx_tipoRetenAnul" runat="server" TabIndex="200" Width="16px">07</asp:TextBox>
            <asp:Label ID="EComprobante3" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="N°"></asp:Label>
            <asp:DropDownList ID="ddl_estabRetenAnul" runat="server" AutoPostBack="True" DataSourceID="ds_Sucursales" DataTextField="DescripcionCentroCosto" DataValueField="NumSucursalSRI" Font-Size="X-Small" Height="16px" TabIndex="100" ToolTip="Escoja el Tipo de Documento Venta" Width="67px" OnSelectedIndexChanged="ddl_estabRetenAnul_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="tbx_estabRetenAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Establecimiento del proveedor que emite el documento fuente" Width="27px" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="tbx_puntoRetenAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Punto de venta del establecimiento que emite el documento fuente" Width="25px">001</asp:TextBox>
            <asp:TextBox ID="tbx_secuenRetenAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Numero secuencial de documento fuente" Width="60px" Enabled="False"></asp:TextBox>
            <asp:Label ID="EFechaAnulReten" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Fecha Anulación Retención"></asp:Label>
            <asp:TextBox ID="tbx_FechaAnulReten" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Numero secuencial de documento fuente" Width="82px" Enabled="False"></asp:TextBox>
            <br />
            <asp:Label ID="EAutorReten" runat="server" BackColor="White" Font-Bold="True" Font-Size="Small" Text="Autorización Retención"></asp:Label>
            <asp:ImageButton ID="btn_CopiaAutorRetenAnul" runat="server" BorderColor="Red" BorderStyle="None" CausesValidation="False" Height="18px" ImageUrl="~/Iconos/copy1.gif" OnClick="btn_CopiaAutorRetenAnul_Click" TabIndex="150" ToolTip="Click para copiar Autorizacion de Retenciones Vigentes" Width="19px" Enabled="False" />
            <asp:TextBox ID="tbx_AutorRetenAnul" runat="server" Font-Bold="True" Font-Size="X-Small" TabIndex="200" ToolTip="Numero secuencial de documento fuente" Width="341px" Enabled="False"></asp:TextBox>
            &nbsp; &nbsp;
            <br />
            <asp:ImageButton ID="btn_NuevaRetenAnul" runat="server" Height="20px" ImageUrl="~/Iconos/AddFinances.gif" OnClick="btn_NuevaRetenAnul_Click" TabIndex="297" ToolTip="Click para Ingresar Nueva Transacción" Width="24px" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_GrabaRetenAnul" runat="server" BorderColor="Red" BorderStyle="None" Height="18px" ImageUrl="~/Iconos/Grabar.gif" TabIndex="150" ToolTip="Click para Grabar los Datos Ingresados del Comprobante " Visible="False" Width="18px" OnClick="btn_GrabaRetenAnul_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_HabilitaModRetenAnul" runat="server" Height="20px" ImageUrl="~/Iconos/Chequear.gif" TabIndex="50" Visible="False" Width="20px" OnClick="btn_HabilitaModRetenAnul_Click" style="margin-top: 0px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btn_ModificaRetenAnul" runat="server" BorderColor="Red" BorderStyle="None" CausesValidation="False" Height="18px" ImageUrl="~/Iconos/Modificar1.png" OnClick="btn_ModificaRetenAnul_Click" TabIndex="150" ToolTip="Click para grabar cambios en la cabecera del Comprobante" Visible="False" Width="19px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btn_EliminaRetenAnul" runat="server" BorderColor="Red" Height="23px" ImageUrl="~/Iconos/Eliminar.gif" OnClick="btn_EliminaRetenAnul_Click" TabIndex="299" ToolTip="Click para Eliminar Transaccion Contable" Visible="False" Width="26px" />
        </asp:Panel>
        <br />
        <asp:Panel ID="pnl_ErrorCompras" runat="server" Height="180px" ScrollBars="Both" Visible="False" BackColor="#FF9999">
                    <asp:Label ID="ECOMprasConErrores" runat="server" Font-Bold="True" ForeColor="Red" Text="Compras con Novedades"></asp:Label>
                    <br />
            <asp:GridView ID="dgv_ComprasErrorProv" runat="server" AutoGenerateColumns="False" DataSourceID="TComprasError" PageSize="9" Visible="False"  TabIndex="400" OnSelectedIndexChanged="dgv_ComprasErrorProv_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                    <asp:BoundField DataField="codSustento" HeaderText="Sto" SortExpression="codSustento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tpIdProv" HeaderText="tId" SortExpression="tpIdProv">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="idProv" HeaderText="idProv" SortExpression="idProv">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="Small" BackColor="#CCFFCC" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipoComprobante" HeaderText="tCte" SortExpression="tipoComprobante">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="parteRel" HeaderText="pR" SortExpression="parteRel">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaRegistro" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fReg" SortExpression="fechaRegistro">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="establecimiento" HeaderText="est" SortExpression="establecimiento">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="puntoEmision" HeaderText="pEm" SortExpression="puntoEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secuencial" HeaderText="secuen" SortExpression="secuencial">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmis" SortExpression="fechaEmision">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autorizacion" HeaderText="autorizacion" SortExpression="autorizacion">
                    <HeaderStyle Font-Size="Small" />
                    <ItemStyle Font-Bold="True" Font-Size="XX-Small" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Diario" HeaderText="Diario" SortExpression="Diario">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImponible" DataFormatString="{0:0.00}" HeaderText="iva0" SortExpression="baseImponible">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpGrav" DataFormatString="{0:0.00}" HeaderText="GravIVA" SortExpression="baseImpGrav">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIva" DataFormatString="{0:0.00}" HeaderText="Iva" SortExpression="montoIva">
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetBienes" DataFormatString="{0:0.00}" HeaderText="RetB30" SortExpression="valorRetBienes">
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valorRetServicios" DataFormatString="{0:0.00}" HeaderText="RetS70" SortExpression="valorRetServicios">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ100" DataFormatString="{0:0.00}" HeaderText="RetS100" SortExpression="valRetServ100">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estabRetencion1" HeaderText="eRet1" SortExpression="estabRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="secRetencion1" HeaderText="secR1" SortExpression="secRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="autRetencion1" HeaderText="autRet1" SortExpression="autRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaEmiRet1" DataFormatString="{0:dd/MM/yyyy}" HeaderText="fEmiRet1" SortExpression="fechaEmiRet1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoB" HeaderText="CodRB" SortExpression="CodConceptoB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRB" DataFormatString="{0:0.00}" HeaderText="BaseIRB" SortExpression="BaseImponIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRB" DataFormatString="{0:0.00}" HeaderText="TRetIRB" SortExpression="TasaRetIRB">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRB" DataFormatString="{0:0.00}" HeaderText="VRetIRB" SortExpression="ValorRetIRB">
                    <FooterStyle Font-Size="X-Small" />
                    <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodConceptoS" HeaderText="CodRS" SortExpression="CodConceptoS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BaseImponIRS" DataFormatString="{0:0.00}" HeaderText="BaseIRS" SortExpression="BaseImponIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TasaRetIRS" DataFormatString="{0:0.00}" HeaderText="TRetIRS" SortExpression="TasaRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorRetIRS" DataFormatString="{0:0.00}" HeaderText="VRetIRS" SortExpression="ValorRetIRS">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Bold="True" Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="montoIce" DataFormatString="{0:0.00}" HeaderText="Ice" SortExpression="montoIce">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetBien10" DataFormatString="{0:#.##}" HeaderText="Ret10" SortExpression="valRetBien10">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ20" DataFormatString="{0:0.00}" HeaderText="Ret20" SortExpression="valRetServ20">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valRetServ50" DataFormatString="{0:0.00}" HeaderText="Ret50" SortExpression="valRetServ50">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseNoGraIVA" DataFormatString="{0:0.00}" HeaderText="NoIVA" SortExpression="baseNoGraIVA">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="baseImpExe" HeaderText="ExeIVA" SortExpression="baseImpExe">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="formaPago" HeaderText="fPg" SortExpression="formaPago">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ptoEmiRetencion1" HeaderText="ptoE1" SortExpression="ptoEmiRetencion1">
                    <HeaderStyle Font-Size="X-Small" />
                    <ItemStyle Font-Size="X-Small" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
&nbsp;<input id="CLOSE1" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
        <br />
        <asp:SqlDataSource ID="TYear" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Anio] FROM AContabilidad.[APContabAnexT011PerYear] ORDER BY [Anio]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="TMes" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Mes], Codigo FROM AContabilidad.[APContabAnexT010PerMes] ORDER BY [Codigo]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="TComprasP" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="Select Diario, codSustento, tpIdProv, idProv, tipoComprobante, parteRel, fechaRegistro, establecimiento, puntoEmision, 
secuencial, fechaEmision, autorizacion, baseNoGraIVA, baseImponible, baseImpGrav, baseImpExe, montoIce, montoIva, valRetBien10, valRetServ20, valorRetBienes, valRetServ50, valorRetServicios, valRetServ100, totbasesImpReemb, formaPago, CodConceptoB, BaseImponIRB, TasaRetIRB, ValorRetIRB, CodConceptoS, BaseImponIRS, TasaRetIRS, ValorRetIRS, estabRetencion1, ptoEmiRetencion1, secRetencion1, autRetencion1, fechaEmiRet1 From AContabilidad.APContabCompras WHERE fechaRegistro BETWEEN @FechaInicial  AND @FechaFinal  AND tpIdProv  != 'N'  AND idProv = @idProv">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_IdProveedor" Name="idProv" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="TCompras" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="Select Diario, codSustento, tpIdProv, idProv, tipoComprobante, parteRel, fechaRegistro, establecimiento, puntoEmision, 
secuencial, fechaEmision, autorizacion, baseNoGraIVA, baseImponible, baseImpGrav, baseImpExe, montoIce, montoIva, valRetBien10, valRetServ20, valorRetBienes, valRetServ50, valorRetServicios, valRetServ100, totbasesImpReemb, formaPago, CodConceptoB, BaseImponIRB, TasaRetIRB, ValorRetIRB, CodConceptoS, BaseImponIRS, TasaRetIRS, ValorRetIRS, estabRetencion1, ptoEmiRetencion1, secRetencion1, autRetencion1, fechaEmiRet1 From AContabilidad.APContabCompras WHERE fechaRegistro BETWEEN @FechaInicial  AND @FechaFinal  AND tpIdProv  != 'N' ">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="TVentasCliente" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NumFactura, TipoIdentificacion, Identificacion, ParteRelVenta, TipoComprobante, TipoEmision, baseNoGraIVA, baseImponible, baseImpGrav, ValorIVA, ValorICE, RetencionIVA, RetencionIR, FormaPago, NombreCompleto, FechaFactura, Efectivo, NumSucursal, NumCaja FROM AHoteleria.APHotelFacturaCabecera  WHERE ((SituacionFactura = 'F') AND (FechaFactura BETWEEN @FechaInicial AND @FechaFinal)) AND Identificacion=@idCliente
ORDER BY NumFactura ">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" DefaultValue="" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_IdCliente" Name="idCliente" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="TVentasPeriodo" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NumFactura, TipoIdentificacion, Identificacion, ParteRelVenta, TipoComprobante, TipoEmision, baseNoGraIVA, baseImponible, baseImpGrav, ValorIVA, ValorICE, RetencionIVA, RetencionIR, FormaPago, NombreCompleto, FechaFactura, Efectivo, NumSucursal, NumCaja FROM AHoteleria.APHotelFacturaCabecera WHERE (SituacionFactura = 'F') AND (FechaFactura BETWEEN @FechaInicial AND @FechaFinal) ORDER BY NumFactura">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" DefaultValue="" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="TFacturasAnuladas" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="Select TipoComprobante, NumSucursal, NumCaja, NumFactura, NumAutorizacion
FROM AHoteleria.APHotelFacturaCabecera 
WHERE (SituacionFactura='A')
 AND (FechaFactura BETWEEN @FechaInicial   AND @FechaFinal)
 ORDER BY TipoComprobante, NumSucursal, NumFactura ">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="TRetenAnuladas" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="Select '07' As TipoComprobante, SucReten, VenReten, NumReten, AutorizaReten, FechaComprob
FROM AContabilidad.APContabRetenciones 
WHERE (SituacionReten='A')
 AND (FechaComprob BETWEEN @FechaInicial   AND @FechaFinal)
 ORDER BY SucReten, NumReten ">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="TComprasD" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="Select Diario, codSustento, tpIdProv, idProv, tipoComprobante, parteRel, fechaRegistro, establecimiento, puntoEmision, 
secuencial, fechaEmision, autorizacion, baseNoGraIVA, baseImponible, baseImpGrav, baseImpExe, montoIce, montoIva, valRetBien10, valRetServ20, valorRetBienes, valRetServ50, valorRetServicios, valRetServ100, totbasesImpReemb, formaPago, CodConceptoB, BaseImponIRB, TasaRetIRB, ValorRetIRB, CodConceptoS, BaseImponIRS, TasaRetIRS, ValorRetIRS, estabRetencion1, ptoEmiRetencion1, secRetencion1, autRetencion1, fechaEmiRet1 From AContabilidad.APContabCompras WHERE fechaRegistro BETWEEN @FechaInicial  AND @FechaFinal  AND tpIdProv  != 'N'  AND Diario = @NDiario">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_NDiario" Name="NDiario" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_Sustentos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoSustento], [DetalleSustento] FROM AContabilidad.[APContabAnexSustentos] ORDER BY CodigoSustento"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TipoBenef" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoBenef, TipoIdCompras, DetalleBenef, OrdenCodigo FROM AContabilidad.APContabTipoBenef ORDER BY OrdenCodigo"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_ConceptosB" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodConcepto], CodConcepto + '-------(' +CONVERT (nvarchar(2), Porcen) + ')-------'  + [ConceptoIR] AS ConceptoIR, Porcen FROM AContabilidad.[APContabAnexConceptos] WHERE Tipo='B' ORDER BY CodConcepto 
"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_ConceptosS" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodConcepto, CodConcepto + '-------(' +CONVERT (nvarchar(2), Porcen) + ')-------' + ConceptoIR AS ConceptoIR, Porcen FROM AContabilidad.APContabAnexConceptos WHERE (Tipo = 'S') ORDER BY CodConcepto"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TipoDocumentos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoDocumento], [DetalleDocumento] FROM AContabilidad.[APContabTipoDocumentos] ORDER BY [NumDocumento]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TipoFuentes" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoDocumento], [DetalleDocumento] FROM AContabilidad.[APContabAnexTipoDoc] ORDER BY CodigoDocumento"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_FormasPago" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoFPago], [DetalleFPago] FROM AContabilidad.[APContabAnexFormaPago] ORDER BY CodigoFPago"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_Sucursales" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [DescripcionCentroCosto], [NumSucursalSRI] FROM AContabilidad.[APContabCentrosCosto]
WHERE NumSucursalSRI='001' OR NumSucursalSri='003'"></asp:SqlDataSource>
        <p>
        <asp:SqlDataSource ID="TComprasError" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="Select Diario, codSustento, tpIdProv, idProv, tipoComprobante, parteRel, fechaRegistro, establecimiento, puntoEmision, secuencial, fechaEmision, autorizacion, baseNoGraIVA, baseImponible, baseImpGrav, baseImpExe, montoIce, montoIva, valRetBien10, valRetServ20, valorRetBienes, valRetServ50, valorRetServicios, valRetServ100, totbasesImpReemb, formaPago, CodConceptoB, BaseImponIRB, TasaRetIRB, ValorRetIRB, CodConceptoS, BaseImponIRS, TasaRetIRS, ValorRetIRS, estabRetencion1, ptoEmiRetencion1, secRetencion1, autRetencion1, fechaEmiRet1 From AContabilidad.APContabCompras WHERE fechaRegistro BETWEEN @FechaInicial  AND @FechaFinal  AND (((len(idProv) &lt;&gt;10 AND len(idProv) &lt;&gt;13)) OR (autorizacion='' OR autorizacion IS NULL) OR ((secRetencion1&lt;1000) AND (ValorRetIRB&gt;0 OR ValorRetIRS&gt;0)) OR 
((baseNoGraIVA+baseImponible+baseImpGrav+baseImpExe-montoIce)&lt;&gt; (BaseImponIRB+BaseImponIRS)))
">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_FechaInicial" Name="FechaInicial" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_FechaFinal" Name="FechaFinal" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
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

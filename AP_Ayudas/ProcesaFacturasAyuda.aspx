<%@ Page Title="Proceso de Facturas:" Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="ProcesaFacturasAyuda.aspx.cs" Inherits="ProcesaFacturasAyuda" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4 style="height: 20px; width: 916px"><%: Title %> 
    <asp:TextBox ID="MensajeR" runat="server" Width="464px" TextMode="MultiLine"></asp:TextBox> <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </h4>    
    <asp:Label ID="EBuscaSolicitud" runat="server" Text="N° de Solicitud " BackColor="#FFFF66" Font-Bold="True" Font-Size="Small" ></asp:Label>
    <asp:TextBox ID="NSolicitud" runat="server" Enabled="False" Height="14px" TabIndex="5" Width="71px"></asp:TextBox>
    &nbsp;<asp:ImageButton ID="BuscarSolicitud" runat="server" ImageUrl="~/Iconos/VistaPrevia.gif" OnClick="BuscarSolicitud_Click" style="height: 16px; width: 16px" TabIndex="7" />
    &nbsp;<asp:TextBox ID="Cedula" runat="server" Width="106px" BackColor="#66FF33" Enabled="False" Height="15px" ></asp:TextBox>
    <asp:TextBox ID="Nombres" runat="server" Columns="50" BackColor="#99FF33" Enabled="False" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Width="285px" Height="16px"></asp:TextBox>
    <asp:TextBox ID="Servicio" runat="server" Columns="50" Width="41px" TabIndex="65" BackColor="#99FF33" ToolTip="Codigo de Operadora Celular" Enabled="False" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="FechaAfiliacion" runat="server" TextMode="Date" BackColor="#99FF33" Enabled="False" Width="79px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="Situacion" runat="server" Width="131px" AutoPostBack="True" BackColor="#99FF33" Enabled="False" TabIndex="18" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <br />
        <asp:Label ID="EFechaSolicitud" runat="server" BorderColor="Black" CssClass="active" Text="Fecha" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="FechaSolicitud" runat="server" TextMode="Date" Width="75px" BackColor="Yellow" Enabled="False" Height="15px"></asp:TextBox>
        <asp:Label ID="EUnidad" runat="server" BorderColor="Black" CssClass="active" Text="Unidad" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="Unidad" runat="server" Width="70px" BackColor="#66FF33" Enabled="False" Height="15px" ></asp:TextBox>
&nbsp;<asp:Label ID="EGrado" runat="server" BorderColor="Black" CssClass="active" Text="Grado" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="Grado" runat="server" Width="51px" BackColor="#66FF33" Enabled="False" Height="15px"></asp:TextBox>
        <asp:Label ID="EAserv" runat="server" BorderColor="Black" CssClass="active" Text="T.Servicio" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="TiempoServicio" runat="server" Width="42px" BackColor="Yellow" Enabled="False" ToolTip="Tiempo de Servicio"></asp:TextBox>
        <asp:Label ID="ETipoAyuda" runat="server" BorderColor="Black" CssClass="active" Text="Ayuda " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="TipoAyuda" runat="server" Width="42px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="RazonSolicitud" runat="server" Width="301px" BackColor="#66FF33" Enabled="False" Font-Names="Arial Narrow" Font-Size="X-Small"></asp:TextBox>
        <br />
        <asp:Label ID="EBeneficiario" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="TipoBeneficiario" runat="server" Width="32px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="NombreBeneficiario" runat="server" Width="229px" Height="16px" BackColor="Yellow" Enabled="False"></asp:TextBox>
        <asp:Label ID="EDocumentos" runat="server" BorderColor="Black" CssClass="active" Text="Doc." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="DocumentosRecibidos" runat="server" Width="105px" Font-Size="X-Small" ToolTip="Documentacion Recibida" BackColor="Yellow" Enabled="False"></asp:TextBox>
        <asp:Label ID="EObservSolicitud" runat="server" BorderColor="Black" CssClass="active" Text="Observ." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="ObservSolicitud" runat="server" Width="126px" Font-Size="X-Small" BackColor="Yellow" Enabled="False"></asp:TextBox>
        &nbsp;<br />
    <asp:Label ID="EMonto" runat="server" BorderColor="Black" CssClass="active" Text="Gastos" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;<asp:TextBox ID="Monto" runat="server" Width="70px" BackColor="Yellow" Height="16px" TabIndex="50"></asp:TextBox>
        <asp:Label ID="EPorcentaje" runat="server" BorderColor="Black" CssClass="active" Text="% " Font-Bold="True" Font-Size="Medium" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Porcentaje" runat="server" Width="27px" BackColor="#66FF33" Enabled="False" Height="15px"></asp:TextBox>
        <asp:TextBox ID="MCalculado" runat="server" Width="66px" BackColor="#66FF33" Enabled="False" Height="15px"></asp:TextBox>
        <asp:Label ID="EProformas" runat="server" BorderColor="Black" CssClass="active" Text="Proformas" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Proformas" runat="server" Width="70px" BackColor="Yellow" Height="15px" TabIndex="50"></asp:TextBox>
        <asp:Label ID="EPorcentajeP" runat="server" BorderColor="Black" CssClass="active" Text="% " Font-Bold="True" Font-Size="Medium" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="PorcentajeP" runat="server" Width="32px" BackColor="#66FF33" Enabled="False" Height="15px" style="text-align: right"></asp:TextBox>
        <asp:TextBox ID="MCalculadoP" runat="server" Width="66px" BackColor="#66FF33" Enabled="False" Height="15px" style="text-align: right"></asp:TextBox>
     <asp:Label ID="EDonacion" runat="server" BorderColor="Black" CssClass="active" Text="Donación" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Donacion" runat="server" Width="70px" BackColor="Yellow" Height="15px" TabIndex="50"></asp:TextBox>
        <br />
    <asp:Label ID="ETFacturas" runat="server" BorderColor="Black" CssClass="active" Text="T.Gastos" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;<asp:TextBox ID="TGeneral" runat="server" Width="95px" BackColor="Yellow" Height="15px" TabIndex="50" Visible="False"></asp:TextBox>
        &nbsp;<asp:Label ID="ETAyuda" runat="server" BorderColor="Black" CssClass="active" Text="T.Ayuda" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="VSolicitado" runat="server" Width="95px" BackColor="Yellow" Height="15px" TabIndex="50" Visible="False" style="text-align: right"></asp:TextBox>
        <asp:ImageButton ID="btn_TerminarIngresoFacturas0" runat="server" Height="20px" ImageUrl="~/Iconos/Stop sign.png"  ToolTip="Click para Terminar Ingreso de Facturas" Width="23px" BorderColor="White" OnClick="btn_TerminarIngresoFacturas_Click" TabIndex="880" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <br />
       <asp:GridView ID="dg_ValorFacturas" runat="server" AutoGenerateColumns="False" DataKeyNames="NumSolicitud,Secuencial" DataSourceID="TabValorFacturas" PageSize="5" Height="30px" OnSelectedIndexChanged="dg_ValorFacturas_SelectedIndexChanged" Width="458px" Visible="False">
           <AlternatingRowStyle Height="9px" />
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" >
            <ItemStyle Height="5px" />
            </asp:CommandField>
            <asp:BoundField DataField="Secuencial" HeaderText="Secuen" ReadOnly="True" SortExpression="Secuencial">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="ObservFactura" HeaderText="Observacion" SortExpression="ObservFactura">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="Valorfactura" DataFormatString="{0:#####0.00}" HeaderText="Factura" SortExpression="Valorfactura">
            <ControlStyle BackColor="Yellow" />
            <HeaderStyle BackColor="#FFFF99" HorizontalAlign="Right" Width="60px" />
            <ItemStyle BackColor="#FFFF99" HorizontalAlign="Right" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorProforma" DataFormatString="{0:#####0.00}" HeaderText="Proforma" SortExpression="ValorProforma">
            <ControlStyle BackColor="#99CCFF" />
            <HeaderStyle BackColor="#99CCFF" HorizontalAlign="Right" Width="60px" />
            <ItemStyle BackColor="#99CCFF" HorizontalAlign="Right" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorDonacion" DataFormatString="{0:#####0.00}" HeaderText="Donacion" SortExpression="ValorDonacion">
            <ControlStyle BackColor="#66FF33" />
            <HeaderStyle BackColor="#66FF33" HorizontalAlign="Right" Width="60px" />
            <ItemStyle BackColor="#66FF33" HorizontalAlign="Right" Width="150px" />
            </asp:BoundField>
            <asp:CheckBoxField ShowHeader="False">
            <ItemStyle Width="10px" />
            </asp:CheckBoxField>
        </Columns>
    </asp:GridView>
    &nbsp;
     <asp:ImageButton ID="OtroSocio" runat="server" Height="24px" ImageUrl="~/Iconos/Lupa.gif" Width="24px" BorderStyle="Double" TabIndex="610" OnClick="OtroSocio_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="ETFacturas0" runat="server" BorderColor="Black" CssClass="active" Text="Totales" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;
        <asp:TextBox ID="TFacturas" runat="server" Width="72px" BackColor="#FFFF99" Height="15px" TabIndex="50"></asp:TextBox>
        <asp:TextBox ID="TProformas" runat="server" Width="80px" BackColor="#99CCFF" Height="15px" TabIndex="50"></asp:TextBox>
        <asp:TextBox ID="TDonaciones" runat="server" Width="83px" BackColor="#66FF33" Height="15px" TabIndex="50"></asp:TextBox>
        <asp:TextBox ID="TGeneral0" runat="server" Width="87px" BackColor="Yellow" Height="15px" TabIndex="50" Visible="False"></asp:TextBox>
        <asp:ImageButton ID="btn_TerminarIngresoFacturas" runat="server" Height="24px" ImageUrl="~/Iconos/Stop sign.png"  ToolTip="Click para Terminar Ingreso de Facturas" Width="22px" BorderColor="White" BorderWidth="1px" OnClick="btn_TerminarIngresoFacturas_Click" TabIndex="880" />
        &nbsp;<asp:Button ID="ImprimirFacturas" runat="server" Text="Imprimir Facturas" Width="124px" style="margin-left: 0px" OnClick="ImprimirFacturas_Click" Visible="False" />
    &nbsp;<input id="CLOSE1" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
     &nbsp;<asp:Label ID="EFactura" runat="server" BorderColor="Black" CssClass="active" Text="N° Secuencial" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="SecuenFacturas" runat="server" Width="71px" Font-Size="X-Small" ToolTip="Numero de Orden de la Factura ingresar" BackColor="#FFCCFF" TabIndex="800"></asp:TextBox>
        <asp:TextBox ID="VFactura" runat="server" Width="71px" BackColor="#FFFF99" Height="16px" TabIndex="810" ToolTip="Ingrese el Valor de la factura valida presentada por el socio" style="text-align: right" AutoPostBack="True">0</asp:TextBox>
        &nbsp;<asp:TextBox ID="VProforma" runat="server" Width="79px" BackColor="#99CCFF" Height="16px" TabIndex="810" ToolTip="Ingrese el Valor de la Proforma presentada por el socio" style="text-align: right" AutoPostBack="True">0</asp:TextBox>
        <asp:TextBox ID="VDonacion" runat="server" Width="84px" BackColor="#66FF33" Height="16px" TabIndex="810" ToolTip="Ingrese el Valor de la Donacion" style="text-align: right" AutoPostBack="True">0</asp:TextBox>
        <asp:TextBox ID="ObservFactura" runat="server" Width="181px" TabIndex="820" Font-Size="X-Small" ToolTip="Observaciones acerca de Factura" style="margin-bottom: 0px" Height="15px" BackColor="#FFFF66"></asp:TextBox>

        <asp:ImageButton ID="btn_AgregarFactura" runat="server" Height="26px" ImageUrl="~/Iconos/Diskete.ICO"  ToolTip="Click para Agregar Valor de Factura" Width="25px" BorderColor="#CCFFFF" BorderWidth="1px" TabIndex="815" BackColor="#CC0000" ForeColor="Red" OnClick="btn_AgregarFactura_Click" />

        &nbsp;&nbsp;

        &nbsp;

        &nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="EVFactura" runat="server" BorderColor="Black" CssClass="active" Text="Factura" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="EVProforma" runat="server" BorderColor="Black" CssClass="active" Text="Proforma" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="EVDonacion" runat="server" BorderColor="Black" CssClass="active" Text="Donacion" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="EObservFactura" runat="server" BorderColor="Black" CssClass="active" Text="Observacion" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Height="14px"  Width="75px"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="VFacturaAEliminar" runat="server" Width="85px" BackColor="Red" Height="16px" TabIndex="810" ToolTip="Ingrese el Valor de la factura valida presentada por el socio" Visible="False"></asp:TextBox>

        <asp:ImageButton ID="btn_EliminarFactura" runat="server" Height="28px" ImageUrl="~/Iconos/Eliminar.gif"  ToolTip="Click para Eliminar Valor de  Factura" Width="21px" BorderColor="#CCFFFF" BorderWidth="1px" TabIndex="440" BackColor="#CC0000" ForeColor="Red" OnClick="btn_EliminarFactura_Click"  />

        <br />
    &nbsp; 
        &nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:SqlDataSource ID="TabCodTramitesAyudas" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoTramite], [DetalleTramite] FROM ASocios.[APSociosAyudasTramites] ORDER BY [OrdenTramite]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabCodigosGrados" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoGrado, DetalleGrado FROM ASocios.APSociosGrados ORDER BY OrdenGrad"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabZonasSub" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoZona, CodigoSubZona, DetalleZona FROM ASocios.APSociosAyudasZonasSub WHERE (CodigoSubZona &lt;&gt; N'00')">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="TabTipoAyuda" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT OrdenTipo, CodigoAyuda, DetalleAyuda FROM ASocios.APSociosAyudasTipo"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabRazonSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Razones FROM ASocios.APSociosAyudasRazones ORDER BY Razones"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabEspecialidades" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Especialidad FROM ASocios.APSociosAyudasEspecialidades ORDER BY Especialidad"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabCasasSalud" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CasaSalud FROM ASocios.APSociosAyudasCasasSalud ORDER BY CasaSalud"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabTipoBenef" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoBenef, DetalleBenef FROM ASocios.APSociosAyudasBenef ORDER BY OrdenBenef">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="TabValorFacturas" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [NumSolicitud], [Secuencial], [Valorfactura], ValorProforma, ValorDonacion, [UserAlta], [FechaCompAlta], [ObservFactura] FROM ASocios.[APSociosAyudasFacturas] WHERE ([NumSolicitud] = @NSolicitud)">
        <SelectParameters>
            <asp:ControlParameter ControlID="NSolicitud" DefaultValue="0" Name="NSolicitud" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
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
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .active {}
    
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

.iconText
{
    font-family: "arial" , "sans-serif";
    font-size: 11px;
    color: black;
}

    </style>
</asp:Content>



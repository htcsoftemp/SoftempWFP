<%@ Page Title="Consulta Solicitudes de Ayuda:" Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="ConsultaSolicitud.aspx.cs" Inherits="ConsultaSolicitud" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4 style="height: 20px; width: 916px"><%: Title %> 
    <asp:TextBox ID="MensajeR" runat="server" Width="464px"></asp:TextBox> <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>
    </h4>
    <asp:GridView ID="SolicitudesDeAyuda" runat="server" AutoGenerateColumns="False" DataKeyNames="NumSolicitud" DataSourceID="TabSolicitudesAyuda" OnSelectedIndexChanged="SolicitudesDeAyuda_SelectedIndexChanged" AllowPaging="True" PageSize="3" >
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/Chequear.gif" SelectText="" ShowSelectButton="True">
            <ItemStyle Width="50px" />
            </asp:CommandField>
            <asp:BoundField DataField="NumSolicitud" HeaderText="Solicitud " ReadOnly="True" SortExpression="NumSolicitud">
            <HeaderStyle Font-Size="Small" ForeColor="#000099" Font-Names="Arial" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaSolicitud" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" SortExpression="FechaSolicitud">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial Narrow" Font-Size="Small" Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="RazonRechazo" HeaderText="Novedad" SortExpression="RazonRechazo">
            <HeaderStyle BackColor="#E155D7" />
            <ItemStyle BackColor="#CCFF66" Font-Size="X-Small" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="ObservSolicitud" HeaderText="Observ" SortExpression="ObservSolicitud">
            <ItemStyle Font-Size="XX-Small" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoAyuda" HeaderText="TA" SortExpression="TipoAyuda">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial" Font-Size="Small" Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoBeneficiario" HeaderText="TB" SortExpression="TipoBeneficiario">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial" Font-Size="Small" Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="MontoDeGastos" HeaderText="Gastos" SortExpression="MontoDeGastos" DataFormatString="{0:#,###,##0.00}">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle HorizontalAlign="Right" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="PorcentajeAyuda" HeaderText="%" SortExpression="PorcentajeAyuda">
            <HeaderStyle ForeColor="#000099" />
            <ItemStyle HorizontalAlign="Center" Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorSolicitado" DataFormatString="{0:#,###,##0.00}" HeaderText="Solicitado" SortExpression="ValorSolicitado">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorAprobado" HeaderText="Aprobado" SortExpression="ValorAprobado" DataFormatString="{0:#,###,##0.00}">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle HorizontalAlign="Right" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="TotalPagado" DataFormatString="{0:#,##0.00}" HeaderText="Entregado" SortExpression="TotalPagado">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="NombreBeneficiario" HeaderText="Beneficiario" SortExpression="NombreBeneficiario">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="RazonSolicitud" HeaderText="Razon" SortExpression="RazonSolicitud">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial Narrow" Font-Size="X-Small" Width="200px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    &nbsp;<label><asp:Label ID="EBuscaSolicitud" runat="server" Text="Solicitud #" BackColor="#66FFCC" Visible="False" Font-Bold="True" Font-Size="Small" ></asp:Label>

    </label>
       &nbsp;
    <label>
    <asp:TextBox ID="NSolicitud" runat="server" Visible="False" Width="77px" AutoPostBack="True" OnTextChanged="NSolicitud_TextChanged"></asp:TextBox>

    </label>
       &nbsp;<label><asp:ImageButton ID="BuscarSolicitud" runat="server" ImageUrl="~/Iconos/VistaPrevia.gif" Visible="False" OnClick="BuscarSolicitud_Click" />

    </label>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="EIdentificacion" runat="server" Text="Cedula" BackColor="#66FFCC" Font-Bold="True" Font-Size="Small" Visible="False"></asp:Label>
    <asp:TextBox ID="Cedula" runat="server" Enabled="False" Height="20px" Width="122px" Visible="False"></asp:TextBox> 
    &nbsp;<asp:ImageButton ID="BuscarSocios" runat="server" ImageUrl="~/Iconos/Binoculares.gif" OnClick="BuscarSocios_Click" Visible="False" />
    &nbsp;<asp:Label ID="EBuscaNom" runat="server" BorderColor="Black" CssClass="active" Text="Nombres" Font-Bold="True" Font-Size="Small" BackColor="#66FFCC" Visible="False"></asp:Label>
       <asp:TextBox ID="ANombres" runat="server" Columns="50" ViewStateMode="Enabled" Enabled="False" Font-Size="Small" Height="16px" Width="103px" Visible="False"></asp:TextBox>
       <asp:ImageButton ID="ParamNombres" runat="server" Enabled="False" ImageUrl="~/Iconos/Chequear.gif" OnClick="ParamNombres_Click" Visible="False" />
       <asp:DropDownList ID="NombresSimilares" runat="server" Width="271px" DataSourceID="DatosPorNombres" DataTextField="NombresU" DataValueField="Identificacion" TabIndex="60" Height="22px" Font-Size="X-Small" Visible="False">
        </asp:DropDownList>
        <asp:ImageButton ID="BuscaPorNombre" runat="server" ImageUrl="~/Iconos/VistaPrevia.gif" Visible="False" OnClick="BuscaPorNombre_Click" />
       <br />
       <label>
    <asp:Label ID="EAfiliado" runat="server" Text="Afiliado" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small" Visible="True"></asp:Label>
    &nbsp;<asp:TextBox ID="Servicio" runat="server" Columns="50" Width="21px" TabIndex="65" BackColor="#99FF33" ToolTip="Codigo de Operadora Celular" Enabled="False" Font-Bold="True" ReadOnly="True" Height="15px"></asp:TextBox>
    <asp:TextBox ID="FechaAfiliacion" runat="server" TextMode="Date" BackColor="#99FF33" Enabled="False" Width="97px" Font-Bold="True" ReadOnly="True" Height="15px"></asp:TextBox>
    <asp:TextBox ID="Situacion" runat="server" Width="116px" AutoPostBack="True" BackColor="White" Enabled="False" TabIndex="18" Font-Bold="True" ReadOnly="True" Height="15px"></asp:TextBox>
       <asp:TextBox ID="Nombres" runat="server" Columns="50" BackColor="#99FF33" Enabled="False" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Width="422px" Height="15px"></asp:TextBox>
       <br />

        <asp:Label ID="EDocumento" runat="server" BorderColor="Black" CssClass="active" Text="Solicitud Nº" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="NumSolicitud" runat="server" Enabled="False" BackColor="White"></asp:TextBox>     
        <asp:TextBox ID="FechaSolicitud" runat="server" TextMode="Date" Width="108px" BackColor="White" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="Grado" runat="server" Width="69px" BackColor="White" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="Unidad" runat="server" Width="110px" BackColor="White" Enabled="False" ></asp:TextBox>
        <asp:TextBox ID="TiempoServicio" runat="server" Width="42px" BackColor="White" Enabled="False"></asp:TextBox>
    </label>
    <br />

        <asp:Label ID="ETipoAyuda" runat="server" BorderColor="Black" CssClass="active" Text="Tipo Ayuda " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="TipoAyuda" runat="server" Width="42px" BackColor="White" Enabled="False" Font-Names="Arial Narrow" Height="15px"></asp:TextBox>
        <asp:TextBox ID="RazonSolicitud" runat="server" Width="288px" BackColor="White" Enabled="False" Font-Names="Arial Narrow" Font-Size="X-Small" Height="15px"></asp:TextBox>
        <asp:TextBox ID="Especialidad" runat="server" Width="167px" BackColor="White" Height="15px"></asp:TextBox>
        <asp:TextBox ID="CasaDeSalud" runat="server" Width="242px" BackColor="White" Height="15px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;<br />
        <asp:Label ID="EBeneficiario" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="TipoBeneficiario" runat="server" Width="32px" BackColor="White" Enabled="False" Height="15px"></asp:TextBox>
        <asp:TextBox ID="NombreBeneficiario" runat="server" Width="159px" Height="15px" BackColor="White" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="OtroBeneficiario" runat="server" Width="173px" BackColor="White" Enabled="False" Height="15px" ></asp:TextBox>
        &nbsp;<asp:Label ID="EDocumentos" runat="server" BorderColor="Black" CssClass="active" Text="Doc." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="DocumentosRecibidos" runat="server" Width="105px" Font-Size="X-Small" ToolTip="Documentacion Recibida" BackColor="White" Enabled="False" Height="15px"></asp:TextBox>
        <asp:Label ID="EObservSolicitud" runat="server" BorderColor="Black" CssClass="active" Text="Observ." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="ObservSolicitud" runat="server" Width="164px" Font-Size="X-Small" BackColor="White" Enabled="False" Height="15px"></asp:TextBox>
        <br />
     <asp:ImageButton ID="OtroSocio" runat="server" Height="24px" ImageUrl="~/Iconos/Lupa.gif" Width="24px" BorderStyle="Double" TabIndex="610" OnClick="OtroSocio_Click" />
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ImprimirSolicitud" runat="server" OnClick="ImprimirSolicitud_Click" Text="Imprimir Solicitud" Width="124px" style="margin-left: 0px" Visible="False" />
    &nbsp; <asp:Button ID="ImprimirFacturas" runat="server" Text="Imprimir Facturas" Width="124px" style="margin-left: 0px" Visible="False" OnClick="ImprimirFacturas_Click" />
    &nbsp; <asp:Button ID="ImprimirAprobacion" runat="server" Text="Imprimir Aprobacion" Width="140px" style="margin-left: 0px" Visible="False" OnClick="ImprimirAprobacion_Click" />
    &nbsp;&nbsp;<asp:Button ID="btn_ImprimeInformeAyudas" runat="server" Text="Imprimir Informe" ToolTip="Click para Imprimir el Informe de Ayudas Econmicas" OnClick="btn_ImprimeInformeAyudas_Click" Visible="False" Width="116px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input id="CLOSE" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
        <br />
       <br />
    <asp:SqlDataSource ID="TabSolicitudesAyuda" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NumSolicitud, Identificacion, FechaSolicitud, TipoAyuda, TipoBeneficiario, NombreBeneficiario, RazonSolicitud, MontoDeGastos, PorcentajeAyuda, ValorSolicitado, ValorAprobado, TotalPagado, RazonRechazo, ObservAR, ObservSolicitud FROM ASocios.APSociosAyudas WHERE (Identificacion = @Identificacion) ORDER BY FechaSolicitud DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Cedula" DefaultValue="0" Name="Identificacion" PropertyName="Text" Type="String" />
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

    <asp:SqlDataSource ID="DatosPorNombres" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Identificacion, NombresU FROM ASocios.APSocios WHERE (LEFT(NombresU,10) LIKE '%' + @NombresU + '%') ORDER BY NombresU">
            <SelectParameters>
                <asp:ControlParameter ControlID="ANombres" DefaultValue="" Name="NombresU" PropertyName="Text" Type="String" />
            </SelectParameters>
    </asp:SqlDataSource>

    </p>
    <p>
    <code><script src="//fundacionPolinal.org/SoftempWFP/PrintScript.js" type="text/javascript"></script>
        </code>
    </p>
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .active {
            background-color: #FFFF00;
        }
    </style>
</asp:Content>



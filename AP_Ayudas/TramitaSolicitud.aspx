<%@ Page Title="Procesa Solicitud de Ayuda:" Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="TramitaSolicitud.aspx.cs" Inherits="TramitaSolicitud" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4 style="height: 20px; width: 916px"><%: Title %> 
    <asp:TextBox ID="MensajeR" runat="server" Width="464px"></asp:TextBox> <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>
    </h4>    
    <asp:Label ID="EIdentificacion" runat="server" Text="Identificacion" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="Cedula" runat="server" Enabled="False"></asp:TextBox> 
    &nbsp;<asp:ImageButton ID="BuscarSocios" runat="server" ImageUrl="~/Iconos/Binoculares.gif" OnClick="BuscarSocios_Click" />
    &nbsp;<asp:Label ID="EBuscaSolicitud" runat="server" Text="Solicitud #" BackColor="#FFFF66" Visible="False" Font-Bold="True" Font-Size="Small" ></asp:Label>
    <asp:TextBox ID="NSolicitud" runat="server" Enabled="False" Visible="False"></asp:TextBox>
    <asp:ImageButton ID="BuscarSolicitud" runat="server" ImageUrl="~/Iconos/VistaPrevia.gif" Visible="False" />
    <br />
    <asp:Label ID="EAfiliado" runat="server" Text="Afiliado" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small" Visible="True"></asp:Label>
    <asp:TextBox ID="Nombres" runat="server" Columns="50" BackColor="#99FF33" Enabled="False" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Width="422px" Height="16px"></asp:TextBox>
    <asp:TextBox ID="Servicio" runat="server" Columns="50" Width="65px" TabIndex="65" BackColor="#99FF33" ToolTip="Codigo de Operadora Celular" Enabled="False" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="FechaAfiliacion" runat="server" TextMode="Date" BackColor="#99FF33" Enabled="False" Width="97px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="Situacion" runat="server" Width="175px" AutoPostBack="True" BackColor="#99FF33" Enabled="False" TabIndex="18" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <br />
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
            <asp:BoundField DataField="TipoAyuda" HeaderText="TA" SortExpression="TipoAyuda">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial" Font-Size="Small" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoBeneficiario" HeaderText="TB" SortExpression="TipoBeneficiario">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial" Font-Size="Small" Width="50px" />
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
            <ItemStyle Font-Names="Arial Narrow" Font-Size="X-Small" Width="300px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
        <br />
        <asp:Label ID="ENumSolicitud" runat="server" Text="Solicitud #" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="NumSolicitud" runat="server" Enabled="False" BackColor="#66FF33"></asp:TextBox>     
        <asp:Label ID="EFechaSolicitud" runat="server" BorderColor="Black" CssClass="active" Text="Fecha " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="FechaSolicitud" runat="server" TextMode="Date" Width="108px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        &nbsp;&nbsp;<asp:Label ID="EUnidad" runat="server" BorderColor="Black" CssClass="active" Text="Unidad" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp; 
        &nbsp;<asp:TextBox ID="Unidad" runat="server" Width="82px" BackColor="#66FF33" Enabled="False" ></asp:TextBox>
&nbsp;<asp:Label ID="EGrado" runat="server" BorderColor="Black" CssClass="active" Text="Grado" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="Grado" runat="server" Width="69px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="EAserv" runat="server" BorderColor="Black" CssClass="active" Text="Tiempo Servicio (meses)" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="TiempoServicio" runat="server" Width="42px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
            <br />
        <asp:Label ID="ETipoAyuda" runat="server" BorderColor="Black" CssClass="active" Text="Tipo Ayuda " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="TipoAyuda" runat="server" Width="42px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="EDiagnostico" runat="server" BorderColor="Black" CssClass="active" Text="Diagn. / Razón" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="RazonSolicitud" runat="server" Width="288px" BackColor="#66FF33" Enabled="False" Font-Names="Arial Narrow" Font-Size="X-Small"></asp:TextBox>
        <asp:Label ID="EDocumentos" runat="server" BorderColor="Black" CssClass="active" Text="Doc." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="DocumentosRecibidos" runat="server" Width="105px" Font-Size="X-Small" ToolTip="Documentacion Recibida" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="EObservSolicitud" runat="server" BorderColor="Black" CssClass="active" Text="Observ." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="ObservSolicitud" runat="server" Width="164px" Font-Size="X-Small" BackColor="#66FF33" Enabled="False"></asp:TextBox>
    <br />
        <asp:Label ID="EEspecialidad" runat="server" Text="Especialidad" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Especialidad" runat="server" Width="167px" BackColor="#66FF33"></asp:TextBox>
        <asp:Label ID="ECasaSalud" runat="server" Text="Casa de Salud" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="CasaDeSalud" runat="server" Width="242px" BackColor="#66FF33"></asp:TextBox>
        <asp:Label ID="EMedicoCertifica" runat="server" Text="Medico que Certifica" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="MedicoQueCertifica" runat="server" Width="143px" BackColor="#66FF33"></asp:TextBox>
    <br />
        <asp:Label ID="EBeneficiario" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="TipoBeneficiario" runat="server" Width="32px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="NombreBeneficiario" runat="server" Width="159px" Height="16px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="OtroBeneficiario" runat="server" Width="173px" BackColor="#66FF33" Enabled="False" ></asp:TextBox>
        &nbsp;&nbsp;<asp:Label ID="EMonto" runat="server" BorderColor="Black" CssClass="active" Text="Gastos" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Monto" runat="server" Width="90px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="EPorcentaje" runat="server" BorderColor="Black" CssClass="active" Text="% Ayuda" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Porcentaje" runat="server" Width="42px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="EMCalculado" runat="server" BorderColor="Black" CssClass="active" Text="Calculado" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="MCalculado" runat="server" Width="75px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
    <br />
        <asp:Label ID="EFechaTramite" runat="server" Text="Fecha Tramite" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="FechaTramite" runat="server" TextMode="Date" Width="93px" TabIndex="5" ToolTip="Fecha de Tramite" BackColor="#FFFF66" Visible="False"></asp:TextBox>
        <asp:Label ID="ESecuencialM" runat="server" Text="Secuencial" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="Secuencial" runat="server" Width="32px" TabIndex="10" ToolTip="Numero de Tramite en la Fecha " BackColor="#FFFF66" Visible="False"></asp:TextBox>
        <asp:Label ID="ETramite" runat="server" Text="Tramite" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:DropDownList ID="DTramite" runat="server" Width="334px" DataSourceID="TabCodTramitesAyudas" DataTextField="DetalleTramite" DataValueField="CodigoTramite" TabIndex="15" AutoPostBack="True" Height="16px" Font-Names="Arial" Font-Size="Small" OnSelectedIndexChanged="DTramite_SelectedIndexChanged" BackColor="#FFFF66" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="CodTramite" runat="server" Enabled="False" ToolTip="Codigo de Tramite" Width="72px" BackColor="#FFFF66" TabIndex="25" Visible="False"></asp:TextBox>     
        <br />
        <asp:Label ID="EMontoM" runat="server" BorderColor="Black" CssClass="active" Text="Gastos" Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="MontoM" runat="server" Width="74px" TabIndex="105" Visible="False"></asp:TextBox>
        <asp:ImageButton ID="CalculaAyudas" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif"  Width="22px" TabIndex="106" Visible="False" OnClick="CalculaAyudas_Click" />
        <asp:Label ID="EPorcentajeM" runat="server" BorderColor="Black" CssClass="active" Text="% Ayuda" Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="PorcentajeM" runat="server" Width="42px" TabIndex="115" Visible="False"></asp:TextBox>
        <asp:Label ID="EMCalculadoM" runat="server" BorderColor="Black" CssClass="active" Text="Calculado" Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="MCalculadoM" runat="server" Width="75px" TabIndex="125" Visible="False"></asp:TextBox>
    <asp:Label ID="EDocumenM" runat="server" BorderColor="Black" CssClass="active" Text="Doumentacion" Font-Bold="True" Visible="False" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="DocumentosRecibidosM" runat="server" Width="180px" TabIndex="135" Font-Size="X-Small" ToolTip="Documentacion Recibida" Visible="False" style="margin-bottom: 0px"></asp:TextBox>
        <asp:Label ID="EObservM" runat="server" BorderColor="Black" CssClass="active" Text="Observ." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="ObservSolicitudM" runat="server" Width="164px" Font-Size="X-Small" BackColor="White" Visible="False"></asp:TextBox>
        <br />
        <asp:Label ID="EBeneficiarioM" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiarios  " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <asp:DropDownList ID="DTipoBeneficiarioM" runat="server" Width="100px" DataSourceID="TabTipoBenef" DataTextField="DetalleBenef" DataValueField="CodigoBenef" TabIndex="205" Font-Size="Small" Height="22px" Font-Names="Arial" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="TipoBeneficiarioM" runat="server" Width="32px" TabIndex="215" Visible="False"></asp:TextBox>
        <asp:TextBox ID="NombreBeneficiarioM" runat="server" Width="236px" TabIndex="225" Height="16px" Visible="False"></asp:TextBox>
        <asp:Label ID="EOtroBeneficiarioM" runat="server" BorderColor="Black" CssClass="active" Text="Otro Beneficiario  " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <asp:TextBox ID="OtroBeneficiarioM" runat="server" Width="264px" TabIndex="235" Visible="False" ></asp:TextBox>
        <br />
    <asp:Label ID="EDiagnosticoM" runat="server" BorderColor="Black" CssClass="active" Text="Diagnóstico / Razón" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <asp:DropDownList ID="DRazonSolicitudM" runat="server" Width="135px" DataSourceID="TabRazonSolicitud" DataTextField="Razones" TabIndex="305" Font-Size="Small" Height="22px" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="RazonSolicitudM" runat="server" Width="280px" TabIndex="315" Visible="False"></asp:TextBox>
        <asp:Label ID="EObservacionM" runat="server" Text="Observ. Solicitud" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="ObservacionSolicitudM" runat="server" Width="227px" TabIndex="325" Visible="False"></asp:TextBox>
        <br />
        <asp:Label ID="EEspecialidadM" runat="server" Text="Especialidad" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="EspecialidadM" runat="server" Width="167px" TabIndex="405" Visible="False"></asp:TextBox>
        <asp:Label ID="ECasaSaludM" runat="server" Text="Casa de Salud" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="CasaDeSaludM" runat="server" Width="242px" TabIndex="415" Visible="False"></asp:TextBox>
        <asp:Label ID="EMedicoCertificaM" runat="server" Text="Medico que Certifica" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="MedicoQueCertificaM" runat="server" Width="137px" TabIndex="425" Visible="False"></asp:TextBox>
        <br />
    <asp:Label ID="EUnidadM" runat="server" BorderColor="Black" CssClass="active" Text="Unidad" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
    <asp:DropDownList ID="DUnidad" runat="server" Width="196px" DataSourceID="TabZonasSub" DataTextField="DetalleZona" DataValueField="CodigoSubZona" TabIndex="505" Font-Size="X-Small" Height="19px" Font-Bold="True" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="UnidadM" runat="server" Width="82px" TabIndex="515" Visible="False" ></asp:TextBox>
        <asp:Label ID="EGradoM" runat="server" BorderColor="Black" CssClass="active" Text="Grado" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <asp:DropDownList ID="DTipoGrado" runat="server" Width="95px" DataSourceID="TabCodigosGrados" DataTextField="DetalleGrado" DataValueField="CodigoGrado" TabIndex="525" Font-Size="X-Small" Height="22px" Font-Bold="True" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="GradoM" runat="server" Width="69px" TabIndex="535" Visible="False"></asp:TextBox>
        <asp:Label ID="ETiempoServicioM" runat="server" BorderColor="Black" CssClass="active" Text="Tiempo Servicio (meses)" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <asp:TextBox ID="TiempoServicioM" runat="server" Width="42px" TabIndex="545" Visible="False"></asp:TextBox>
        <br />
        <asp:Label ID="EFechaResultado" runat="server" Text="Fecha Resultado" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        &nbsp;<asp:TextBox ID="FechaResultado" runat="server" TextMode="Date" Width="95px" TabIndex="905" BackColor="#FFFF66" Visible="False"></asp:TextBox>
        <asp:Label ID="EResultado" runat="server" Text="Resultado" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        &nbsp;<asp:TextBox ID="ResultadoTramite" runat="server" Width="202px" TabIndex="915" Visible="False" BackColor="#FFFF66"></asp:TextBox>
        &nbsp;
        <asp:Label ID="EObservacion" runat="server" Text="Observacion Tramite" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="ObservacionProceso" runat="server" Width="227px" TabIndex="925" Visible="False" BackColor="#FFFF66"></asp:TextBox>
        <asp:ImageButton ID="VerificaDatos" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" OnClick="VerificaDatos_Click" Width="22px" TabIndex="950" Visible="False" />
    <br />
     <asp:ImageButton ID="OtroSocio" runat="server" Height="24px" ImageUrl="~/Iconos/Lupa.gif" Width="24px" BorderStyle="Double" TabIndex="610" OnClick="OtroSocio_Click" />
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="OtroTramite" runat="server" Height="24px" ImageUrl="~/Iconos/Mas.png" Width="24px" BorderStyle="Double" TabIndex="610" OnClick="OtroTramite_Click" Visible="False" />
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="RegistrarProceso" runat="server" Text="Registrar Proceso" Font-Bold="True" ForeColor="Red" Width="134px" OnClick="RegistrarProceso_Click" Enabled="False" TabIndex="220" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input id="CLOSE" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
        <asp:GridView ID="Procesos" runat="server" AutoGenerateColumns="False" DataKeyNames="NumSolicitud,FechaProceso,Secuencial,Identificacion" DataSourceID="TabTramitesAyudas">
            <Columns>
                <asp:BoundField DataField="FechaProceso" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" ReadOnly="True" SortExpression="FechaProceso">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
                <ItemStyle Font-Names="Arial" Font-Size="Small" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Secuencial" HeaderText="SQ" ReadOnly="True" SortExpression="Secuencial">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
                </asp:BoundField>
                <asp:BoundField DataField="Proceso" HeaderText="Proceso" SortExpression="Proceso">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
                <ItemStyle Font-Size="X-Small" Width="300px" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaProxProb" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Resultado" SortExpression="FechaProxProb">
                <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
                <ItemStyle Font-Size="Small" Width="100px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
       <br />
    <asp:SqlDataSource ID="TabSolicitudesAyuda" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NumSolicitud, Identificacion, FechaSolicitud, TipoAyuda, TipoBeneficiario, NombreBeneficiario, RazonSolicitud, MontoDeGastos, PorcentajeAyuda, ValorSolicitado, ValorAprobado, TotalPagado FROM ASocios.APSociosAyudas WHERE (Identificacion = @Identificacion) ORDER BY FechaSolicitud DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Cedula" DefaultValue="0" Name="Identificacion" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="TabCodTramitesAyudas" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoTramite], [DetalleTramite] FROM ASocios.[APSociosAyudasTramites] ORDER BY [OrdenTramite]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabTramitesAyudas" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Identificacion], [NumSolicitud], [FechaProceso], [Secuencial], [Proceso], [ResultadoProceso], [Observacion], [FechaProxProb] FROM ASocios.[APSociosAyudasProceso] WHERE (([Identificacion] = @Identificacion) AND ([NumSolicitud] = @NumSolicitud)) ORDER BY FechaProceso DESC">
        <SelectParameters>
            <asp:Parameter Name="Identificacion" />
            <asp:ControlParameter ControlID="NumSolicitud" DefaultValue="0" Name="NumSolicitud" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="TabCodigosGrados" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoGrado, DetalleGrado FROM ASocios.APSociosGrados ORDER BY OrdenGrad"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabZonasSub" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoZona, CodigoSubZona, DetalleZona FROM ASocios.APSociosAyudasZonasSub WHERE (CodigoSubZona &lt;&gt; N'00')">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="TabTipoAyuda" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT OrdenTipo, CodigoAyuda, DetalleAyuda FROM ASocios.APSociosAyudasTipo"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabRazonSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Razones FROM ASocios.APSociosAyudasRazones ORDER BY Razones"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabTipoBenef" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoBenef, DetalleBenef FROM ASocios.APSociosAyudasBenef ORDER BY OrdenBenef">
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


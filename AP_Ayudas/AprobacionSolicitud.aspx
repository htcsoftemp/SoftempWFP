<%@ Page Title="Aprobacion de Solicitud de Ayuda N °: " Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="AprobacionSolicitud.aspx.cs" Inherits="AprobacionSolicitud" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="MensajeR" runat="server" Width="661px" Height="42px" TextMode="MultiLine"></asp:TextBox> <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>
    <h4 style="height: 20px">
        <%: Title %>  <asp:TextBox ID="NSolicitud" runat="server" Enabled="False" Height="14px" TabIndex="5" OnTextChanged="NSolicitud_TextChanged" AutoPostBack="True"></asp:TextBox>
    &nbsp;<asp:ImageButton ID="BuscarSolicitud" runat="server" ImageUrl="~/Iconos/VistaPrevia.gif" OnClick="BuscarSolicitud_Click" style="width: 16px; height: 16px;" TabIndex="7" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <asp:Label ID="EIdentificacion" runat="server" Text="Identificacion" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="Cedula" runat="server" Enabled="False" Height="14px" TabIndex="8"></asp:TextBox> 
    &nbsp;<asp:ImageButton ID="BuscarSocios" runat="server" ImageUrl="~/Iconos/Binoculares.gif" OnClick="BuscarSocios_Click" TabIndex="9" />
    </h4>
    <asp:Label ID="EAfiliado" runat="server" Text="Afiliado" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small" Visible="True"></asp:Label>
    <asp:TextBox ID="Nombres" runat="server" Columns="50" BackColor="#99FF33" Enabled="False" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Width="422px" Height="16px"></asp:TextBox>
    <asp:TextBox ID="Servicio" runat="server" Columns="50" Width="65px" TabIndex="65" BackColor="#99FF33" ToolTip="Codigo de Operadora Celular" Enabled="False" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="FechaAfiliacion" runat="server" TextMode="Date" BackColor="#99FF33" Enabled="False" Width="97px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="Situacion" runat="server" Width="175px" AutoPostBack="True" BackColor="#99FF33" Enabled="False" TabIndex="18" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:GridView ID="SolicitudesDeAyuda" runat="server" AutoGenerateColumns="False" DataKeyNames="NumSolicitud" DataSourceID="TabSolicitudesAyuda" OnSelectedIndexChanged="SolicitudesDeAyuda_SelectedIndexChanged" AllowPaging="True" PageSize="3" >
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" SelectText="" ShowSelectButton="True">
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
            <asp:BoundField DataField="RazonRechazo" HeaderText="Resultado" SortExpression="RazonRechazo">
            <ItemStyle BackColor="#FF5050" Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoAyuda" HeaderText="TA" SortExpression="TipoAyuda">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial" Font-Size="Small" Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoBeneficiario" HeaderText="TB" SortExpression="TipoBeneficiario">
            <HeaderStyle Font-Names="Arial" Font-Size="Small" ForeColor="#000099" />
            <ItemStyle Font-Names="Arial" Font-Size="Small" Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="MontoDonaciones" DataFormatString="{0:#,###,##0.00}" HeaderText="Donacion" SortExpression="MontoDonaciones">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="MontoProformas" DataFormatString="{0:#,###,##0.00}" HeaderText="Proformas" SortExpression="MontoProformas">
            <HeaderStyle ForeColor="#000099" />
            <ItemStyle HorizontalAlign="Right" />
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
            <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="#000099" Font-Bold="True" />
            <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width="50px" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="RazonSolicitud" HeaderText="Razon" SortExpression="RazonSolicitud">
            <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="#000099" Font-Bold="True" />
            <ItemStyle Font-Names="Arial Narrow" Font-Size="X-Small" Width="100px" Wrap="False" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
        &nbsp;<asp:Label ID="ENumSolicitud" runat="server" Text="Solicitud #" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
        &nbsp;
        <asp:TextBox ID="NumSolicitud" runat="server" Enabled="False" BackColor="#66FF33" Width="69px" Font-Bold="True" ForeColor="Red"></asp:TextBox>     
        &nbsp;&nbsp;
        <asp:Label ID="EReferAprob" runat="server" BorderColor="Black" CssClass="active" Text="Refer.Aprob." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="FechaAprobRechaz" runat="server" TextMode="Date" Width="60px" TabIndex="300" BackColor="White" ViewStateMode="Enabled" Font-Names="Arial Narrow" Font-Size="X-Small" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="ReferAprobRechaz" runat="server" Width="190px" TabIndex="100" Font-Size="X-Small" ToolTip="Beneficiario" style="margin-bottom: 0px" BackColor="White" Font-Names="Arial Narrow" Height="14px" TextMode="MultiLine" Enabled="False"></asp:TextBox>
        <asp:Label ID="EAprobacion" runat="server" BorderColor="Black" CssClass="active" Text="Aprobado" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Width="50px"></asp:Label>
        <asp:TextBox ID="MontoT" runat="server" Width="42px" TabIndex="30" BackColor="White" Font-Names="Arial Narrow" Font-Size="X-Small" Height="14px" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="BeneficiarioP0" runat="server" Width="76px" TabIndex="60" Font-Size="X-Small" ToolTip="Beneficiario" style="margin-bottom: 0px; margin-left: 12px;" Height="14px" BackColor="White" Font-Names="Arial Narrow" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="MontoP0" runat="server" Width="49px" TabIndex="50" Height="16px" BackColor="White" Font-Names="Arial Narrow" Font-Size="X-Small" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="BeneficiarioP1" runat="server" Width="81px" TabIndex="80" Font-Size="X-Small" ToolTip="Beneficiario" style="margin-bottom: 0px" BackColor="White" Font-Names="Arial Narrow" Height="14px" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="MontoP1" runat="server" Width="44px" TabIndex="70" BackColor="White" Font-Names="Arial Narrow" Font-Size="X-Small" Height="14px" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="BeneficiarioP2" runat="server" Width="93px" TabIndex="100" Font-Size="X-Small" ToolTip="Beneficiario" style="margin-bottom: 0px" BackColor="White" Font-Names="Arial Narrow" Height="14px" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="MontoP2" runat="server" Width="40px" TabIndex="90" BackColor="White" Font-Names="Arial Narrow" Font-Size="X-Small" Height="14px" Enabled="False"></asp:TextBox>
        <br />
    <asp:ImageButton ID="btn_ModificaBasicos" runat="server" Height="20px" ImageUrl="~/Iconos/Modificar1.png"  ToolTip="Click para habilitar Modificar Datos de Solicitud" Width="19px" BorderColor="Red" BorderWidth="1px" OnClick="btn_ModificaBasicos_Click"  />
        &nbsp;&nbsp;
        <asp:Label ID="EFechaSolicitud" runat="server" BorderColor="Black" CssClass="active" Text="Fecha" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:TextBox ID="FechaSolicitud" runat="server" TextMode="Date" Width="75px" BackColor="Yellow" Enabled="False" Height="15px"></asp:TextBox>
        <asp:Label ID="EAserv" runat="server" BorderColor="Black" CssClass="active" Text="T.Servicio" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="TiempoServicio" runat="server" Width="42px" BackColor="Yellow" Enabled="False" ToolTip="Tiempo de Servicio"></asp:TextBox>
        <asp:Label ID="EUnidad" runat="server" BorderColor="Black" CssClass="active" Text="Unidad" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:DropDownList ID="DUnidad" runat="server" Width="122px" DataSourceID="TabZonasSub" DataTextField="DetalleZona" DataValueField="CodigoSubZona" TabIndex="15" Font-Size="X-Small" Height="20px" Font-Bold="True" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="Unidad" runat="server" Width="78px" BackColor="#66FF33" Enabled="False" Height="15px" Font-Size="X-Small" ></asp:TextBox>
&nbsp;<asp:Label ID="EGrado" runat="server" BorderColor="Black" CssClass="active" Text="Grado" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;<asp:DropDownList ID="DTipoGrado" runat="server" Width="88px" DataSourceID="TabCodigosGrados" DataTextField="DetalleGrado" DataValueField="CodigoGrado" TabIndex="25" Font-Size="X-Small" Height="22px" Font-Bold="True" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="Grado" runat="server" Width="51px" BackColor="#66FF33" Enabled="False" Height="15px"></asp:TextBox>
        <br />
        <asp:Label ID="ETipoAyuda" runat="server" BorderColor="Black" CssClass="active" Text="Ayuda " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:DropDownList ID="DTipoAyuda" runat="server" Width="93px" DataSourceID="TabTipoAyuda" DataTextField="DetalleAyuda" DataValueField="CodigoAyuda" TabIndex="34" Font-Size="Small" Height="23px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="DTipoAyuda_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="TipoAyuda" runat="server" Width="42px" BackColor="#66FF33" Enabled="False"></asp:TextBox>
        <asp:Label ID="EBeneficiario" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="DTipoBeneficiario" runat="server" Width="100px" DataSourceID="TabTipoBenef" DataTextField="DetalleBenef" DataValueField="CodigoBenef" TabIndex="52" Font-Size="Small" Height="22px" Font-Names="Arial" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="TipoBeneficiario" runat="server" Width="32px" BackColor="#66FF33" Enabled="False" Height="16px"></asp:TextBox>
        <asp:TextBox ID="NombreBeneficiario" runat="server" Width="114px" Height="16px" BackColor="Yellow" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="OtroBeneficiario" runat="server" Width="91px" BackColor="Yellow" Enabled="False" ></asp:TextBox>
        <br />
        <asp:Label ID="EDiagnostico" runat="server" BorderColor="Black" CssClass="active" Text="Diagn./Razón" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:DropDownList ID="DRazonSolicitud" runat="server" Width="119px" DataSourceID="TabRazonSolicitud" DataTextField="Razones" DataValueField="Razones" TabIndex="38" Font-Size="X-Small" Height="22px" Visible="False" OnSelectedIndexChanged="DRazonSolicitud_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="RazonSolicitud" runat="server" Width="203px" BackColor="#66FF33" Enabled="False" Font-Names="Arial Narrow" Font-Size="X-Small"></asp:TextBox>

        <asp:ImageButton ID="btn_AgregarDiagnosticos" runat="server" Height="18px" ImageUrl="~/Iconos/Diskete.ICO"  ToolTip="Click para Agregar Diagnostico a la Tabla" Width="16px" TabIndex="440" Visible="False" BackColor="White" ForeColor="Red" OnClick="btn_AgregarDiagnosticos_Click" />

        <asp:Label ID="EDocumentos" runat="server" BorderColor="Black" CssClass="active" Text="Documentacion Recibida" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="DocumentosRecibidos" runat="server" Width="105px" Font-Size="X-Small" ToolTip="Documentacion Recibida" BackColor="Yellow" Enabled="False"></asp:TextBox>
        <asp:Label ID="EObservSolicitud" runat="server" BorderColor="Black" CssClass="active" Text="Observ." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="ObservSolicitud" runat="server" Width="159px" Font-Size="X-Small" BackColor="Yellow" Enabled="False"></asp:TextBox>
        <asp:ImageButton ID="btn_ActualizaBasicos" runat="server" Height="22px" ImageUrl="~/Iconos/Grabar.gif"  ToolTip="Click para Actualizar Datos de Solicitud" Width="19px" BorderColor="Red" BorderWidth="1px" TabIndex="440" BackColor="Red" OnClick="btn_ActualizaBasicos_Click" />

        <br />

        <asp:Label ID="EEspecialidad" runat="server" Text="Especialidad" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="DEspecialidad" runat="server" Width="111px" DataSourceID="TabEspecialidades" DataTextField="Especialidad" DataValueField="Especialidad" TabIndex="42" Font-Size="X-Small" Height="16px">
        </asp:DropDownList>
        <asp:TextBox ID="Especialidad" runat="server" Width="166px" BackColor="Yellow" TabIndex="44" ToolTip="Ingrese el nombre de la Especialidad si no consta en la tabla de la izquierda"></asp:TextBox>

        <asp:ImageButton ID="btn_AgregarEspecialidad" runat="server" Height="17px" ImageUrl="~/Iconos/Diskete.ICO"  ToolTip="Click para Agregar Especialidad" Width="17px" BorderColor="White" TabIndex="440" Visible="False" BackColor="White" ForeColor="Red" OnClick="btn_AgregarEspecialidad_Click" />

        &nbsp;<asp:Label ID="EMedicoCertifica" runat="server" Text="Medico " BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="MedicoQueCertifica" runat="server" Width="240px" BackColor="Yellow" TabIndex="46"></asp:TextBox>
        <asp:Label ID="ECasaSalud" runat="server" Text="Casa de Salud" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:DropDownList ID="DCasaSalud" runat="server" Width="135px" DataSourceID="TabCasasSalud" DataTextField="CasaSalud" DataValueField="CasaSalud" TabIndex="48" Font-Size="X-Small" Height="22px">
        </asp:DropDownList>
        <asp:TextBox ID="CasaDeSalud" runat="server" Width="162px" BackColor="Yellow"></asp:TextBox>
        &nbsp;<asp:ImageButton ID="btn_ActualizaSolicitud" runat="server" Height="22px" ImageUrl="~/Iconos/Grabar.gif"  ToolTip="Click para Actualizar Datos de Solicitud" Width="19px" BorderColor="Red" BorderWidth="1px" TabIndex="440" BackColor="Red" OnClick="btn_ActualizaSolicitud_Click" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <br />
    <asp:Button ID="btn_ProcesarFacturas" runat="server" Text="Procesar Facturas" Font-Bold="True" ForeColor="#000066" Width="125px" OnClick="btn_ProcesarFacturas_Click" />
        &nbsp;&nbsp;&nbsp; 
        <asp:ImageButton ID="btn_ProcesaGastos" runat="server" Height="27px" ImageUrl="~/Iconos/Refresh.png" ToolTip="Click para Obtener Valores de Gastos Ingresados" Width="30px" TabIndex="100" BorderColor="White" BackColor="Black" BorderWidth="1px" OnClick="btn_ProcesaGastos_Click" />
        &nbsp;<asp:Label ID="EMonto" runat="server" BorderColor="Black" CssClass="active" Text="Gastos" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Monto" runat="server" Width="59px" BackColor="Yellow" Height="15px" TabIndex="50"></asp:TextBox>
    <asp:Label ID="EPorcentaje" runat="server" BorderColor="Black" CssClass="active" Text="% G." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Porcentaje" runat="server" Width="29px" BackColor="#66FF33" Enabled="False" Height="15px"></asp:TextBox>
        <asp:TextBox ID="MCalculado" runat="server" Width="57px" BackColor="#66FF33" Enabled="False" Height="15px" OnTextChanged="MCalculado_TextChanged"></asp:TextBox>
    <asp:Label ID="EProformas" runat="server" BorderColor="Black" CssClass="active" Text="Proformas" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Proformas" runat="server" Width="59px" BackColor="Yellow" Height="15px" TabIndex="50"></asp:TextBox>
        <asp:Label ID="EPorcentajeP" runat="server" BorderColor="Black" CssClass="active" Text="% P." Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="PorcentajeP" runat="server" Width="29px" BackColor="#66FF33" Enabled="False" Height="15px"></asp:TextBox>
        <asp:TextBox ID="MCalculadoP" runat="server" Width="57px" BackColor="#66FF33" Enabled="False" Height="15px"></asp:TextBox>
    <asp:Label ID="EDonacion" runat="server" BorderColor="Black" CssClass="active" Text="Donación" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="Donacion" runat="server" Width="59px" BackColor="Yellow" Height="15px" TabIndex="50"></asp:TextBox>
        <asp:ImageButton ID="CalculaValores" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" Width="22px" TabIndex="52" OnClick="CalculaValores_Click" />
        &nbsp;<asp:ImageButton ID="btn_HabilitaAprobacion" runat="server" Height="24px" ImageUrl="~/Iconos/Proceso.png" ToolTip="Click para habilitar Aprobacion o Rechazo de Ayuda" Width="30px" TabIndex="100" OnClick="btn_HabilitaAprobacion_Click" BorderColor="White" BackColor="Black" BorderWidth="1px" Visible="False" />
    &nbsp;<br />
    <asp:CheckBox ID="Aprobado" runat="server" Font-Bold="True" Height="20px" Text="Aprobada         " TextAlign="Left" Font-Names="Arial Narrow" Font-Size="Small" TabIndex="110" Width="94px" Visible="False" AutoPostBack="True" OnCheckedChanged="Aprobado_CheckedChanged" />
        <asp:Label ID="EMontoA" runat="server" BorderColor="Black" CssClass="active" Text="Aprobado" Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow" Width="53px"></asp:Label>
        <asp:TextBox ID="PorcentajeAyuda" runat="server" Width="42px" BackColor="#66FF33" Enabled="False" Height="15px" Visible="False"></asp:TextBox>
        <asp:TextBox ID="MontoA" runat="server" Width="100px" TabIndex="120" Visible="False" BackColor="#FFFF66"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="EBeneficiario0" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario - " Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow" Height="14px"  Width="75px"></asp:Label>
        &nbsp;
        <asp:TextBox ID="MontoA0" runat="server" Width="100px" TabIndex="150" Visible="False" Height="15px" BackColor="#FFFF66" style="text-align: right">0</asp:TextBox>
        <asp:TextBox ID="Beneficiario0" runat="server" Width="200px" TabIndex="160" Font-Size="X-Small" ToolTip="Beneficiario " Visible="False" style="margin-bottom: 0px" Height="15px" BackColor="#FFFF66">Socio</asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
        <asp:Label ID="EBeneficiario1" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario 1" Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow" Width="78px"></asp:Label>
        &nbsp;<asp:TextBox ID="MontoA1" runat="server" Width="100px" TabIndex="170" Visible="False" BackColor="#FFFF66" Height="15px" style="text-align: right">0</asp:TextBox>
        <asp:TextBox ID="Beneficiario1" runat="server" Width="200px" TabIndex="180" Font-Size="X-Small" ToolTip="Beneficiario 1" Visible="False" style="margin-bottom: 0px" BackColor="#FFFF66" Height="15px"></asp:TextBox>
        <br />
        <asp:Label ID="EBeneficiario2" runat="server" BorderColor="Black" CssClass="active" Text="Beneficiario 2" Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow" Width="78px"></asp:Label>
        &nbsp;<asp:TextBox ID="MontoA2" runat="server" Width="100px" TabIndex="190" Visible="False" BackColor="#FFFF66" Height="15px" style="text-align: right">0</asp:TextBox>
        <asp:TextBox ID="Beneficiario2" runat="server" Width="200px" TabIndex="200" Font-Size="X-Small" ToolTip=" Beneficiario 2" Visible="False" style="margin-bottom: 0px" BackColor="#FFFF66" Height="15px"></asp:TextBox>
        <br />
        <asp:Label ID="ERecomendacion" runat="server" BorderColor="Black" CssClass="active" Text="Recomendacion" Font-Bold="True" Visible="False" Font-Size="Small" Font-Names="Arial Narrow" Height="16px"></asp:Label>
        <asp:TextBox ID="Recomendacion" runat="server" Width="752px" TabIndex="210" Visible="False" BackColor="#FFFF66" Height="16px" TextMode="MultiLine" Font-Names="Arial Narrow" Font-Size="Small"></asp:TextBox>
        <br />
    <asp:CheckBox ID="Rechazado" runat="server" Font-Bold="True" ForeColor="Red" Text="Rechazada" TextAlign="Left" Font-Names="Arial Narrow" Font-Size="Small" TabIndex="200" Width="96px" Visible="False" AutoPostBack="True" OnCheckedChanged="Rechazado_CheckedChanged" />
        <asp:ImageButton ID="ActivaRechazo" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" OnClick="ActivaRechazo_Click" Width="22px" TabIndex="950" Visible="False" />
    <asp:Label ID="EDRechazo" runat="server" BorderColor="Black" CssClass="active" Text="Razón de Rechazo" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False" ForeColor="Red"></asp:Label>
        <asp:DropDownList ID="DRazonRechazo" runat="server" Width="115px" DataSourceID="TabTramitesAyudas" DataTextField="DetalleTramite" DataValueField="CodigoTramite" TabIndex="210" AutoPostBack="True" Height="19px" Font-Names="Arial" Font-Size="Small" BackColor="#FFFF66" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="RazonRechazo" runat="server" Width="275px" TabIndex="220" Font-Size="X-Small" ToolTip="Documentacion Recibida" Visible="False" style="margin-bottom: 0px" Height="14px" BackColor="#FFFF66"></asp:TextBox>
        <br />
        <asp:Label ID="EFechaResultado" runat="server" Text="Fecha Resultado" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        &nbsp;<asp:TextBox ID="FechaResultado" runat="server" TextMode="Date" Width="95px" TabIndex="300" BackColor="#FFFF66" ViewStateMode="Enabled" Visible="False"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Label ID="EObservacion" runat="server" Text="Observacion " BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="ObservacionProceso" runat="server" Width="227px" TabIndex="310" BackColor="#FFFF66" Visible="False"></asp:TextBox>
        <asp:ImageButton ID="VerificaDatos" runat="server" Height="22px" ImageUrl="~/Iconos/Verificacion.gif" OnClick="VerificaDatos_Click" Width="31px" TabIndex="500" Visible="False" />
    <br />
     <asp:ImageButton ID="OtroSocio" runat="server" Height="24px" ImageUrl="~/Iconos/Lupa.gif" Width="24px" BorderStyle="Double" TabIndex="610" OnClick="OtroSocio_Click" />
     &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ActivaVerifAprob" runat="server" Height="30px" ImageUrl="~/Iconos/mantenimiento.png" TabIndex="1" ToolTip="Click para Activar Aprobacion" Width="30px" BorderColor="Yellow" Visible="False" BorderStyle="Inset" OnClick="ActivaVerifAprob_Click" />
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="RegistrarProceso" runat="server" Text="Registrar Aprobacion" Font-Bold="True" ForeColor="Red" Width="160px" OnClick="RegistrarProceso_Click" TabIndex="220" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_ImprimeAprobacion" runat="server" OnClick="btn_ImprimeAprobacion_Click" Text="Imprimir Aprobacion" Font-Bold="True" Width="152px" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_ProcesarInforme" runat="server" Text="Procesar Informe" Font-Bold="True" ForeColor="#000066" OnClick="btn_ProcesarInforme_Click" Width="138px" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input id="CLOSE" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
    <asp:ImageButton ID="btn_HabilitaProcesoFacturas" runat="server" Height="20px" ImageUrl="~/Iconos/Modificar1.png"  ToolTip="Click para habilitar Proceso de Facturas" Width="19px" BorderColor="Red" BorderWidth="1px" OnClick="btn_HabilitaProcesoFacturas_Click" Visible="False"   />
&nbsp;<asp:Label ID="ETFacturas" runat="server" BorderColor="Black" CssClass="active" Text="Total Facturas" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="TFacturas" runat="server" Width="95px" BackColor="Yellow" Height="15px" TabIndex="50" Visible="False"></asp:TextBox>
        &nbsp;<asp:ImageButton ID="btn_TerminarIngresoFacturas0" runat="server" Height="22px" ImageUrl="~/Iconos/Stop sign.png"  ToolTip="Click para Terminar Ingreso de Facturas" Width="29px" BorderColor="Red" BorderWidth="1px" OnClick="btn_TerminarIngresoFacturas_Click" TabIndex="880" Visible="False" />
        <br />
       <asp:GridView ID="dg_ValorFacturas" runat="server" AutoGenerateColumns="False" DataKeyNames="NumSolicitud,Secuencial" DataSourceID="TabValorFacturas" PageSize="5" Height="54px" OnSelectedIndexChanged="dg_ValorFacturas_SelectedIndexChanged" Width="401px">
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/Revisar.GIF" ShowSelectButton="True" >
            <ItemStyle Height="10px" />
            </asp:CommandField>
            <asp:BoundField DataField="Secuencial" HeaderText="Secuen" ReadOnly="True" SortExpression="Secuencial">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Valorfactura" DataFormatString="{0:#,###,##0.00}" HeaderText="Valor" SortExpression="Valorfactura">
            <ControlStyle BackColor="Yellow" />
            <HeaderStyle BackColor="#FFFF99" HorizontalAlign="Right" />
            <ItemStyle BackColor="#FFFF99" HorizontalAlign="Right" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="ObservFactura" HeaderText="Observacion" SortExpression="ObservFactura">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="200px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="ETFacturas0" runat="server" BorderColor="Black" CssClass="active" Text="Total Facturas" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Visible="False"></asp:Label>
        <asp:TextBox ID="TFacturas0" runat="server" Width="95px" BackColor="Yellow" Height="15px" TabIndex="50" Visible="False"></asp:TextBox>
        &nbsp;<asp:ImageButton ID="btn_TerminarIngresoFacturas" runat="server" Height="23px" ImageUrl="~/Iconos/Stop sign.png"  ToolTip="Click para Terminar Ingreso de Facturas" Width="26px" BorderColor="Red" BorderWidth="1px" OnClick="btn_TerminarIngresoFacturas_Click" TabIndex="880" Visible="False" />
        <br />
    <asp:Label ID="EFactura" runat="server" BorderColor="Black" CssClass="active" Text="Factura" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="SecuenFacturas" runat="server" Width="36px" Font-Size="X-Small" ToolTip="Documentacion Recibida" BackColor="Yellow" TabIndex="800"></asp:TextBox>
    <asp:Label ID="EVFactura" runat="server" BorderColor="Black" CssClass="active" Text="Valor" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow"></asp:Label>
        <asp:TextBox ID="VFactura" runat="server" Width="59px" BackColor="Yellow" Height="16px" TabIndex="810"></asp:TextBox>
        <asp:Label ID="EObservFactura" runat="server" BorderColor="Black" CssClass="active" Text="Observacion" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" Height="14px"  Width="75px"></asp:Label>
        <asp:TextBox ID="ObservFactura" runat="server" Width="181px" TabIndex="820" Font-Size="X-Small" ToolTip="Observaciones acerca de Factura" style="margin-bottom: 0px" Height="15px" BackColor="#FFFF66"></asp:TextBox>

        &nbsp;&nbsp;

        <asp:ImageButton ID="btn_EliminarFactura" runat="server" Height="21px" ImageUrl="~/Iconos/Eliminar.gif"  ToolTip="Click para Eliminar Valor de  Factura" Width="21px" BorderColor="#CCFFFF" BorderWidth="1px" TabIndex="440" BackColor="#CC0000" ForeColor="Red" OnClick="btn_EliminarFactura_Click"  />

        &nbsp;

        <asp:ImageButton ID="btn_AgregarFactura" runat="server" Height="26px" ImageUrl="~/Iconos/Diskete.ICO"  ToolTip="Click para Agregar Valor de Factura" Width="25px" BorderColor="#CCFFFF" BorderWidth="1px" TabIndex="815" BackColor="#CC0000" ForeColor="Red" OnClick="btn_AgregarFactura_Click" />

        &nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp; <asp:ImageButton ID="btn_DeshabilitaProcesoFacturas" runat="server" Height="20px" ImageUrl="~/Iconos/mantenimiento.png"  ToolTip="Click para deshabilitar Proceso de Facturas" Width="19px" BorderColor="Red" BorderWidth="1px" OnClick="btn_DeshabilitaProcesoFacturas_Click" TabIndex="890"   />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:SqlDataSource ID="TabSolicitudesAyuda" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NumSolicitud, Identificacion, FechaSolicitud, TipoAyuda, TipoBeneficiario, NombreBeneficiario, RazonSolicitud, MontoFacturas, MontoProformas, MontoDonaciones, MontoDeGastos, PorcentajeAyuda, ValorSolicitado, ValorAprobado, TotalPagado, RazonRechazo FROM ASocios.APSociosAyudas WHERE (Identificacion = @Identificacion) ORDER BY FechaSolicitud DESC">
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
    <asp:SqlDataSource ID="TabEspecialidades" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Especialidad FROM ASocios.APSociosAyudasEspecialidades ORDER BY Especialidad"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabCasasSalud" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CasaSalud FROM ASocios.APSociosAyudasCasasSalud ORDER BY CasaSalud"></asp:SqlDataSource>
    <asp:SqlDataSource ID="TabTipoBenef" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoBenef, DetalleBenef FROM ASocios.APSociosAyudasBenef ORDER BY OrdenBenef">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="TabValorFacturas" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [NumSolicitud], [Secuencial], [Valorfactura], [UserAlta], [FechaCompAlta], [ObservFactura] FROM ASocios.[APSociosAyudasFacturas] WHERE ([NumSolicitud] = @NumSolicitud)">
        <SelectParameters>
            <asp:ControlParameter ControlID="NSolicitud" DefaultValue="0" Name="NumSolicitud" PropertyName="Text" Type="Int32" />
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



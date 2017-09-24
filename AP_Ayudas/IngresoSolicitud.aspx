<%@ Page Title="Ingreso de Solicitudes de Ayuda:" Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="IngresoSolicitud.aspx.cs" Inherits="IngresoSolicitud" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4 style="height: 20px; width: 916px"><%: Title %>
    <asp:TextBox ID="MensajeR" runat="server" Width="415px" Font-Size="X-Small"></asp:TextBox> <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>
    </h4>
    <asp:Label ID="EIdentificacion" runat="server" Text="Identificacion" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="Cedula" runat="server" Enabled="False" Width="101px"></asp:TextBox>     <asp:ImageButton ID="BuscarSocios" runat="server" ImageUrl="~/Iconos/Binoculares.gif" OnClick="BuscarSocios_Click" />
        <label>&nbsp;<asp:TextBox ID="Nombres" runat="server" Columns="50" BackColor="#99FF33" Enabled="False" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Width="407px"></asp:TextBox>
    &nbsp;<asp:Label ID="EPagAnter" runat="server" BorderColor="#000066" BorderStyle="None" Font-Bold="True" ForeColor="#000099" Text="Pagado " Width="73px"></asp:Label>
    <asp:Label ID="TotalPagado" runat="server" BackColor="White" Font-Bold="True" ForeColor="Black" style="text-align: right" Width="100px" BorderColor="#66CCFF" BorderStyle="Ridge" BorderWidth="1px"></asp:Label>
        </label><br />
    <asp:Label ID="EDatosAfil" runat="server" Text="Afiliado" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="Servicio" runat="server" Columns="50" Width="31px" TabIndex="65" BackColor="#99FF33" ToolTip="Codigo de Operadora Celular" Enabled="False" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="FechaAfiliacion" runat="server" TextMode="Date" BackColor="#99FF33" Enabled="False" Width="97px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="Situacion" runat="server" Width="88px" AutoPostBack="True" BackColor="#99FF33" Enabled="False" TabIndex="18" Font-Bold="True" ReadOnly="True"></asp:TextBox>
    &nbsp;<asp:TextBox ID="Observaciones" runat="server" Width="325px" TabIndex="510" BackColor="#99FF33" Font-Size="X-Small" Height="16px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
          &nbsp;<asp:Label ID="ESaldoPend" runat="server" BorderColor="#000066" BorderStyle="None" Font-Bold="True" ForeColor="#000099" Text=" Pendiente" Width="70px"></asp:Label>
    <asp:Label ID="SPendiente" runat="server" BackColor="Yellow" Font-Bold="True" ForeColor="Red" style="text-align: right" Width="102px" BorderColor="Yellow" BorderStyle="Ridge" BorderWidth="1px"></asp:Label>
         <br />
        <asp:ImageButton ID="btn_ModificaMedios" runat="server" Height="31px" ImageUrl="~/Iconos/Modificar.png"  ToolTip="Click para habilitar Modificacion de medios de contacto" Width="37px" BorderColor="#CCFFFF" BorderWidth="1px" OnClick="btn_ModificaMedios_Click" />
       <asp:Label ID="EEMail" runat="server" Text="Email" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="EMailSocio" runat="server" Columns="50" TextMode="Email" Width="169px" BackColor="#FFCC00" Enabled="False" Font-Size="X-Small" Height="16px" Font-Bold="True" TabIndex="400"></asp:TextBox>
    <asp:Label ID="ETelefono" runat="server" Text="Telef. Conven." BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
<asp:TextBox ID="TelefonoConv" runat="server" Columns="50" Width="92px" TabIndex="410" ToolTip="Telefono Convencional del Socio" BackColor="#FFCC00" Enabled="False" Font-Bold="True"></asp:TextBox>
  <asp:Label ID="ECelular" runat="server" Text="Celular" BackColor="#FFFF66" Font-Bold="True" Font-Size="Small"></asp:Label>
        <asp:DropDownList ID="DOperadora" runat="server" Width="81px" DataSourceID="OperadorasCel" DataTextField="DetalleOperadora" DataValueField="CodigoOperadora" TabIndex="420" BackColor="#FFCC00" Enabled="False">
        </asp:DropDownList>
        <asp:TextBox ID="OperadoraCel" runat="server" Columns="50" Width="29px" TabIndex="65" BackColor="#FFCC00" ToolTip="Codigo de Operadora Celular" Enabled="False" Font-Bold="True" ReadOnly="True"></asp:TextBox>
        <asp:TextBox ID="TelefonoCel" runat="server" Columns="50" Width="100px" TabIndex="430" ToolTip="Telefono Celular del Socio" BackColor="#FFCC00" Enabled="False" Font-Bold="True"></asp:TextBox>
        <asp:ImageButton ID="btn_ActualizaMedios" runat="server" Height="28px" ImageUrl="~/Iconos/Grabar.gif"  ToolTip="Click para Actualizar Medios de Contacto" Width="37px" BorderColor="#CCFFFF" BorderWidth="1px" OnClick="btn_ActualizaMedios_Click" TabIndex="440" />
        &nbsp;&nbsp;<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:ImageButton ID="btn_HabilitaIngresoSol" runat="server" Height="28px" ImageUrl="~/Iconos/Insertar.gif" ToolTip="Click para habilitar Ingreso de Solicitud" Width="37px" OnClick="btn_HabilitaIngresoSol_Click" TabIndex="450" Visible="False" />
        &nbsp;<asp:Label ID="EFechaSolicitud" runat="server" BorderColor="Maroon" CssClass="active" Text="Fecha " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" BackColor="White" ForeColor="#CC0000"></asp:Label>
        &nbsp;<asp:TextBox ID="FechaSolicitud" runat="server" TextMode="Date" Width="108px" TabIndex="10" ToolTip="Ingrese la Fecha de la Solicitud con formato dd/MM/yyyy"></asp:TextBox>
        &nbsp;&nbsp;<asp:Label ID="EUnidad" runat="server" BorderColor="Maroon" CssClass="active" Text="Unidad" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" BackColor="White" ForeColor="#CC0000"></asp:Label>
        &nbsp; <asp:DropDownList ID="DUnidad" runat="server" Width="196px" DataSourceID="TabZonasSub" DataTextField="DetalleZona" DataValueField="CodigoSubZona" TabIndex="15" Font-Size="X-Small" Height="19px" Font-Bold="True" ToolTip="Escoga la Unidad a la que pertenece el Socio">
        </asp:DropDownList>
        &nbsp;<asp:TextBox ID="Unidad" runat="server" Width="98px" TabIndex="20" Font-Size="X-Small" Visible="False" Enabled="False"></asp:TextBox>
&nbsp;<asp:Label ID="EGrado" runat="server" BorderColor="Maroon" CssClass="active" Text="Grado" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" BackColor="White" ForeColor="#CC0000"></asp:Label>
        &nbsp;<asp:DropDownList ID="DTipoGrado" runat="server" Width="95px" DataSourceID="TabCodigosGrados" DataTextField="DetalleGrado" DataValueField="CodigoGrado" TabIndex="25" Font-Size="X-Small" Height="22px" Font-Bold="True" ToolTip="Escoja el Grado del Socio">
        </asp:DropDownList>
        <asp:TextBox ID="Grado" runat="server" Width="42px" TabIndex="30" Visible="False" Enabled="False"></asp:TextBox>
        <asp:Label ID="EAserv" runat="server" BorderColor="Maroon" CssClass="active" Text="Años Serv." Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" BackColor="White" ForeColor="#CC0000"></asp:Label>
        <asp:TextBox ID="TiempoServicio" runat="server" Width="42px" TabIndex="32"></asp:TextBox>
              <br />
        <asp:Label ID="ETipoAyuda" runat="server" BorderColor="Maroon" CssClass="active" Text="Tipo Ayuda " Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" BackColor="White" ForeColor="#CC0000"></asp:Label>
        <asp:DropDownList ID="DTipoAyuda" runat="server" Width="148px" DataSourceID="TabTipoAyuda" DataTextField="DetalleAyuda" DataValueField="CodigoAyuda" TabIndex="34" Font-Size="Small" Height="25px" ToolTip="Escoja el Tipo de Ayuda que Solicita el Socio">
        </asp:DropDownList>
        <asp:TextBox ID="TipoAyuda" runat="server" Width="42px" TabIndex="36" Visible="False" Enabled="False"></asp:TextBox>
        <asp:Label ID="EDiagnostico" runat="server" BorderColor="Maroon" CssClass="active" Text="Diagnóstico / Razón" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" BackColor="White" ForeColor="#CC0000"></asp:Label>
        <asp:DropDownList ID="DRazonSolicitud" runat="server" Width="135px" DataSourceID="TabRazonSolicitud" DataTextField="Razones" DataValueField="Razones" TabIndex="38" Font-Size="X-Small" Height="22px" OnSelectedIndexChanged="DRazonSolicitud_SelectedIndexChanged" ToolTip="Escoja el Diagnostico o Razon de la Solicitud de Ayuda">
        </asp:DropDownList>
        <asp:TextBox ID="RazonSolicitud" runat="server" Width="301px" TabIndex="40" Visible="False" ToolTip="Ingrese el diagnostico que no se encuentre en la tabla de la izquierda"></asp:TextBox>

        <asp:ImageButton ID="btn_AgregarDiagnosticos" runat="server" Height="21px" ImageUrl="~/Iconos/Diskete.ICO"  ToolTip="Click para Agregar Diagnostico a la Tabla" Width="21px" BorderColor="#CCFFFF" BorderWidth="1px" TabIndex="440" Visible="False" BackColor="#CC0000" ForeColor="Red" OnClick="btn_AgregarDiagnosticos_Click" />

        <br />
        <asp:Label ID="EBeneficiario" runat="server" BorderColor="Maroon" CssClass="active" Text="Beneficiario" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" BackColor="White" ForeColor="#CC0000"></asp:Label>
        <asp:DropDownList ID="DTipoBeneficiario" runat="server" Width="100px" DataSourceID="TabTipoBenef" DataTextField="DetalleBenef" DataValueField="CodigoBenef" TabIndex="40" Font-Size="Small" Height="22px" Font-Names="Arial" ToolTip="Escoja el Tipo de Beneficiario de la Ayuda">
        </asp:DropDownList>
        <asp:TextBox ID="TipoBeneficiario" runat="server" Width="32px" TabIndex="54" Visible="False" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="NombreBeneficiario" runat="server" Width="204px" TabIndex="41" Height="16px" ToolTip="Ingrese el Nombre del Beneficiario si el Beneficiario es diferente al Socio"></asp:TextBox>
        <asp:TextBox ID="OtroBeneficiario" runat="server" Width="190px" TabIndex="42" ToolTip="Ingrese el Nombre de Otro Beneficiario" ></asp:TextBox>
        <br />

        <asp:Label ID="EObservSol" runat="server" BorderColor="Black" CssClass="active" Text="Observacion" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" ForeColor="#006600"></asp:Label>
        &nbsp;
        <asp:TextBox ID="ObservSolicitud" runat="server" Width="400px" TabIndex="60" Font-Size="X-Small"></asp:TextBox>
        <asp:Label ID="EDocRecibida" runat="server" BorderColor="Black" CssClass="active" Text="Documentacion Recibida" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" ForeColor="#006600"></asp:Label>
        <asp:TextBox ID="DocumentosRecibidos" runat="server" Width="173px" TabIndex="65" Font-Size="X-Small" ToolTip="Documentacion Recibida"></asp:TextBox>

        <br />

        <asp:Label ID="EEspecialidad" runat="server" Text="Especialidad" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" ForeColor="Blue"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="DEspecialidad" runat="server" Width="135px" DataSourceID="TabEspecialidades" DataTextField="Especialidad" DataValueField="Especialidad" TabIndex="100" Font-Size="X-Small" Height="22px" OnSelectedIndexChanged="DEspecialidad_SelectedIndexChanged" ToolTip="Escoja la Especialidad ">
        </asp:DropDownList>
        <asp:TextBox ID="Especialidad" runat="server" Width="167px" BackColor="White" TabIndex="105" ToolTip="Ingrese la especialidad que no se encuentra en la tabla de la izquierda"></asp:TextBox>

        <asp:ImageButton ID="btn_AgregarEspecialidad" runat="server" Height="21px" ImageUrl="~/Iconos/Diskete.ICO"  ToolTip="Click para Agregar Especialidad a la Tabla" Width="21px" BorderColor="#CCFFFF" BorderWidth="1px" TabIndex="440" Visible="False" BackColor="#CC0000" ForeColor="Red" OnClick="btn_AgregarEspecialidad_Click" />

        <br />
        <asp:Label ID="ECasaSalud" runat="server" Text="Casa de Salud" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" ForeColor="Blue"></asp:Label>
        <asp:DropDownList ID="DCasaSalud" runat="server" Width="135px" DataSourceID="TabCasasSalud" DataTextField="CasaSalud" DataValueField="CasaSalud" TabIndex="110" Font-Size="X-Small" Height="22px" OnSelectedIndexChanged="DEspecialidad_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="CasaDeSalud" runat="server" Width="242px" BackColor="White" TabIndex="115"></asp:TextBox>
        <asp:Label ID="EMedicoCertifica" runat="server" Text="Medico que Certifica" BackColor="White" Font-Bold="True" Font-Size="Small" Font-Names="Arial Narrow" ForeColor="Blue"></asp:Label>
        <asp:TextBox ID="MedicoQueCertifica" runat="server" Width="143px" BackColor="White" TabIndex="120"></asp:TextBox>

            <br />
    <asp:Label ID="EMonto" runat="server" BorderColor="Black" CssClass="active" Text="Gastos" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" ForeColor="Blue"></asp:Label>
        &nbsp;&nbsp;<asp:TextBox ID="Monto" runat="server" Width="102px" TabIndex="130" Height="16px"></asp:TextBox>
        &nbsp;<asp:Label ID="EPorcentaje" runat="server" BorderColor="Black" CssClass="active" Text="% Ayuda" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="Porcentaje" runat="server" Width="42px" TabIndex="132" ReadOnly="True" Enabled="False"></asp:TextBox>
        <asp:Label ID="EMCalculado" runat="server" BorderColor="Black" CssClass="active" Text="Calculado" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="MCalculado" runat="server" Width="104px" TabIndex="134" ReadOnly="True" Enabled="False"></asp:TextBox>
            &nbsp;
            <asp:ImageButton ID="VerificaDatos" runat="server" Height="43px" ImageUrl="~/Iconos/Verificacion.gif" OnClick="VerificaDatos_Click" Width="47px" TabIndex="140" ToolTip="Click para validar Datos Ingresados y habilitar Registro " />
            <br />

    <p>
     &nbsp;
     <asp:ImageButton ID="OtroSocio" runat="server" Height="33px" ImageUrl="~/Iconos/Lupa.gif" Width="33px" TabIndex="610" OnClick="OtroSocio_Click" BorderWidth="1px" BorderColor="#CCFFFF" ToolTip="Click para buscar un Socio" />
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<label><asp:ImageButton ID="ActivaNuevaAyuda" runat="server" Height="30px" ImageUrl="~/Iconos/mantenimiento.png" TabIndex="1" ToolTip="Click para Activar Busqueda  de Otro Socio" Width="30px" BorderColor="#CCFFFF" Visible="False" OnClick="ActivaNuevaAyuda_Click" BorderWidth="1px" />
        &nbsp;&nbsp; <asp:Label ID="EDocumento" runat="server" BorderColor="Black" CssClass="active" Text="Solicitud Nº" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
    </label>
        &nbsp;<label><asp:TextBox ID="NDocumento" runat="server" Width="89px" TabIndex="210" ReadOnly="True" Font-Bold="True" Font-Size="Medium" Visible="False" ></asp:TextBox>
    </label>
        &nbsp;&nbsp;&nbsp;
     <asp:Button ID="RegistrarSolicitud" runat="server" Text="Registrar Solicitud" Font-Bold="True" ForeColor="Red" Width="134px" OnClick="RegistrarSolicitud_Click" TabIndex="220" Enabled="False" Visible="False" ToolTip="Click para Grabar los Datos de la Solicitud Ingresada" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_ImprimeSolicitud" runat="server" OnClick="btn_ImprimeSolicitud_Click" Text="Imprimir Solicitud" Enabled="False" Visible="False" ToolTip="Click para Imprimir la Cabecera de la Solicitud Ingresada" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input id="CLOSE" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><p>
    <asp:GridView ID="SolicitudesDeAyuda" runat="server" AutoGenerateColumns="False" DataKeyNames="NumSolicitud" DataSourceID="TabSolicitudesAyuda" >
        <Columns>
            <asp:BoundField DataField="NumSolicitud" HeaderText="Solicitud" ReadOnly="True" SortExpression="NumSolicitud">
            <ItemStyle Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaSolicitud" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" SortExpression="FechaSolicitud">
            <ItemStyle Font-Names="Arial" Font-Size="Small" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoAyuda" HeaderText="TA" SortExpression="TipoAyuda">
            <ItemStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Center" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoBeneficiario" HeaderText="TB" SortExpression="TipoBeneficiario">
            <ItemStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Center" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="RazonSolicitud" HeaderText="Razon" SortExpression="RazonSolicitud">
            <ItemStyle Font-Size="X-Small" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="NombreBeneficiario" HeaderText="Beneficiario" SortExpression="NombreBeneficiario">
            <ItemStyle Width="150px" Font-Names="Arial" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="MontoDeGastos" HeaderText="Gastos" SortExpression="MontoDeGastos" DataFormatString="{0:#,##0.00}">
            <ItemStyle HorizontalAlign="Right" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="PorcentajeAyuda" HeaderText="%" SortExpression="PorcentajeAyuda">
            <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorSolicitado" HeaderText="Ayuda" SortExpression="ValorSolicitado" DataFormatString="{0:#,##0.00}">
            <HeaderStyle BackColor="#CCFF66" />
            <ItemStyle BackColor="#CCFF66" ForeColor="Red" HorizontalAlign="Right" Width="100px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="EstadoPagosA" runat="server" AutoGenerateColumns="False" DataSourceID="DatosPagosA" PageSize="3" Width="800px" Visible="False">
        <Columns>
            <asp:BoundField DataField="Transaccion" HeaderText="Trans" SortExpression="Transaccion">
            <ItemStyle Width="25px" />
            </asp:BoundField>
            <asp:BoundField DataField="Observacion" HeaderText="Observacion" SortExpression="Observacion">
            <ItemStyle Font-Size="X-Small" Width="250px" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaTran" HeaderText="Fecha" SortExpression="FechaTran" DataFormatString="{0:dd/MM/yyyy}">
            </asp:BoundField>
            <asp:BoundField DataField="Documento" HeaderText="Doc" SortExpression="Documento">
            </asp:BoundField>
            <asp:BoundField DataField="ValorTran" HeaderText="Total" SortExpression="ValorTran" DataFormatString="{0:#,##0.00}">
            <ItemStyle HorizontalAlign="Right" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorAporte" DataFormatString="{0:#,##0.00}" HeaderText="Aporte" SortExpression="ValorAporte">
            <ItemStyle HorizontalAlign="Right" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorInteres" DataFormatString="{0:#,##0.00}" HeaderText="Interes" SortExpression="ValorInteres">
            <ItemStyle HorizontalAlign="Right" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorCostoBan" DataFormatString="{0:#,##0.00}" HeaderText="Costos" SortExpression="ValorCostoBan">
            <ItemStyle HorizontalAlign="Right" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="OtrosValores" DataFormatString="{0:#,##0.00}" HeaderText="Otros" SortExpression="OtrosValores">
            <ItemStyle HorizontalAlign="Right" Width="50px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>

    <p>
    <asp:SqlDataSource ID="TabSolicitudesAyuda" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NumSolicitud, Identificacion, FechaSolicitud, TipoAyuda, TipoBeneficiario, NombreBeneficiario, RazonSolicitud, MontoDeGastos, PorcentajeAyuda, ValorSolicitado FROM ASocios.APSociosAyudas WHERE (Identificacion = @Identificacion) ORDER BY FechaSolicitud DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Cedula" DefaultValue="0" Name="Identificacion" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="DatosPagosA" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Transaccion, FechaTran, Documento, ValorTran, ValorAporte, ValorInteres, ValorCostoBan, OtrosValores, Observacion FROM ASocios.APSociosPagosA WHERE (Identificacion = @Identificacion) ORDER BY FechaTran DESC, Transaccion DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Cedula" Name="Identificacion" PropertyName="Text" Type="String" DefaultValue="0" />
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
<asp:SqlDataSource ID="OperadorasCel" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoOperadora, DetalleOperadora FROM ASocios.APSociosTOperadoras"></asp:SqlDataSource>
            <p>
            <script type="text/javascript">
                function cerrar() {
                    window.open("", "_parent", "");
                    var ventana = window.self;
                    ventana.opener = window.self;
                    ventana.close();
                }
            </script>
    <p>
        &nbsp;</p>
    <p>
    <code><script src="//fundacionPolinal.org/SoftempWFP/PrintScript.js" type="text/javascript"></script>
        </code>
    </p>
</asp:Content>


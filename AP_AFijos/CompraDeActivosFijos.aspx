<%@ Page Title="Pagos a Proveedores: " Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="CompraDeActivosFijos.aspx.cs" Inherits="PagosAProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .active {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
         <asp:TextBox ID="MensajeR" runat="server" Width="846px" BackColor="Yellow" Font-Size="X-Small" ForeColor="Red" TextMode="MultiLine" Height="24px"></asp:TextBox>
         <br />
         <asp:MultiView ID="mv_IngresoActivosFijos" runat="server" ActiveViewIndex="0">
              <asp:View ID="v_DatosCompra" runat="server">
             <asp:Button ID="btn_DatosActivoF" runat="server" 
              CommandArgument="v_DatosActivoF" 
              CommandName="SwitchViewByID"
              Text="Datos Activos" />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <br />
                 <asp:Label ID="EBeneficiario" runat="server" BorderColor="Black" CssClass="active" Text="Proveedor" Visible="False"></asp:Label>
                &nbsp;<asp:TextBox ID="tbx_IdenBenefL" runat="server" Width="112px" BackColor="#FFFF99" ForeColor="Black" TabIndex="10" ToolTip="Ingrese Identificacion del Proveedor" Visible="False" OnTextChanged="tbx_IdenBenefL_TextChanged"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddl_Beneficiarios" runat="server" Width="509px" DataSourceID="ds_Beneficiarios" DataTextField="AN" DataValueField="IdenBenef" TabIndex="25" Height="20px" ToolTip="Escoja el Beneficiario relacionado con el Diario" AutoPostBack="True" OnSelectedIndexChanged="ddl_Beneficiarios_SelectedIndexChanged" Visible="False" BackColor="#FFFF99">
        </asp:DropDownList>
        &nbsp;
        <asp:ImageButton ID="btn_HabilitaIngresoBenef" runat="server" Height="16px" ImageUrl="~/Iconos/Proceso.png" Width="17px" TabIndex="30" ToolTip="Click para para habilitar ingreso de Beneficiarios" Visible="False" OnClick="btn_HabilitaIngresoBenef_Click" />
    &nbsp;
    <asp:ImageButton ID="btn_BuscarBeneficiario" runat="server" ImageUrl="~/Iconos/Binoculares.gif" OnClick="BuscarBeneficiario_Click" TabIndex="32" Visible="False" Height="16px" Width="16px" />
    &nbsp;&nbsp;&nbsp;<br />
    <asp:Label ID="ETipoBenef" runat="server" BorderColor="Black" CssClass="active" Text="Tipo Iden." Visible="False"></asp:Label>
        <asp:DropDownList ID="ddl_TipoIdContrib" runat="server" Width="145px" DataSourceID="ds_TipoBenef" DataTextField="DetalleBenef" DataValueField="CodigoBenef" TabIndex="35" Height="16px" OnSelectedIndexChanged="ddl_TipoBenef_SelectedIndexChanged" ToolTip="Escoja el Tipo de Identificacion del Beneficiario Relacionado con el Diario" Visible="False" AutoPostBack="True" BackColor="#FFFF99">
        </asp:DropDownList>
    <asp:Label ID="ETipoContrib" runat="server" BorderColor="Black" CssClass="active" Text="Tipo Contrib." Visible="False"></asp:Label>
        <asp:DropDownList ID="ddl_TipoContrib" runat="server" Width="145px" DataSourceID="ds_TipoContrib" DataTextField="DetalleContrib" DataValueField="CodigoContrib" TabIndex="37" Height="16px" ToolTip="Escoja el Tipo de Contribuyente" Visible="False" AutoPostBack="True" BackColor="#FFFF99" OnSelectedIndexChanged="ddl_TipoContrib_SelectedIndexChanged">
        </asp:DropDownList>
    <asp:Label ID="EApellidos" runat="server" BorderColor="Black" CssClass="active" Text="Apellidos " Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_Apellido1Benef" runat="server" Width="150px" BackColor="#FFFF99" ForeColor="Black" TabIndex="40" ToolTip="Ingrese el Primer Apellido del Beneficiario" Visible="False"></asp:TextBox>
    <asp:Label ID="ENombres" runat="server" BorderColor="Black" CssClass="active" Text="Nombres " Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_Nombre1Benef" runat="server" Width="150px" BackColor="#FFFF99" ForeColor="Black" TabIndex="50" ToolTip="Ingrese el Primer Nombre del Beneficiario" Visible="False"></asp:TextBox>
        <br />
    <asp:Label ID="EIdentificacion" runat="server" BorderColor="Black" CssClass="active" Text="Contribuyente"></asp:Label>
        <asp:TextBox ID="tbx_TipoIdContrib" runat="server" Width="23px" AutoPostBack="True" BackColor="#CCFF99" Enabled="False" TabIndex="65" OnTextChanged="tbx_TipoIdContrib_TextChanged"></asp:TextBox>
        <asp:TextBox ID="tbx_IdContrib" runat="server" Width="109px" BackColor="#CCFF99" ForeColor="Black" TabIndex="67" ToolTip="Ingrese la Identificacion del Beneficiario relacionado con el Diario" OnTextChanged="tbx_IdBenef_TextChanged"></asp:TextBox>
         <asp:TextBox ID="tbx_TipoContrib" runat="server" Width="23px" BackColor="#CCFF99" ForeColor="Black" TabIndex="69" ToolTip="Tipo de Contribuyente" Enabled="False" Visible="False"></asp:TextBox>
        <asp:TextBox ID="tbx_NombreCompleto" runat="server" Width="275px" BackColor="#CCFF99" ForeColor="Black" TabIndex="70" ToolTip="Razón Social del Contribuyente" Font-Size="X-Small"></asp:TextBox>
        <asp:TextBox ID="tbx_AutorizacionR" runat="server" Width="302px" BackColor="#CCFF99" ForeColor="Black" Height="16px" TabIndex="115" ToolTip="Ingrese el codigo de Autorizacion de los documentos relacionados con el Diario" Font-Size="X-Small" Visible="False"></asp:TextBox>
        <asp:TextBox ID="tbx_FCaducaFuenteR" runat="server" Width="100px" BackColor="#CCFF99" ForeColor="Black" TabIndex="85" ToolTip="Ingrese el N. de Telefono celular del Beneficiario" Font-Size="Small" Visible="False" AutoPostBack="True" OnTextChanged="tbx_FCaducaFuenteR_TextChanged" Height="22px"></asp:TextBox>
        <asp:ImageButton ID="btn_HabilitaModifDatosProv" runat="server" Height="16px" ImageUrl="~/Iconos/Edit1.png" Width="17px" TabIndex="30" ToolTip="Click para habilitar Modificar Datos del Proveedor" Visible="False" OnClick="btn_HabilitaModifDatosProv_Click" />
         &nbsp;<br />
    <asp:Label ID="ETelefonoConv" runat="server" BorderColor="Black" CssClass="active" Text="T.Conv.."></asp:Label>
        <asp:TextBox ID="tbx_TelefConvContrib" runat="server" Width="80px" BackColor="#CCFF99" ForeColor="Black" TabIndex="80" ToolTip="Ingrese el N. de Telefono convencional del Beneficiario" Font-Size="Small"></asp:TextBox>
    <asp:Label ID="ETelefonoCel" runat="server" BorderColor="Black" CssClass="active" Text="T.Cel."></asp:Label>
        <asp:TextBox ID="tbx_TelefCelContrib" runat="server" Width="100px" BackColor="#CCFF99" ForeColor="Black" TabIndex="85" ToolTip="Ingrese el N. de Telefono celular del Beneficiario" Font-Size="Small"></asp:TextBox>
    <asp:Label ID="EDireccion" runat="server" BorderColor="Black" CssClass="active" Text="Dirección"></asp:Label>
        <asp:TextBox ID="tbx_DireccionContrib" runat="server" Width="300px" BackColor="#CCFF99" ForeColor="Black" TabIndex="90" Font-Size="Small"></asp:TextBox>
    <asp:Label ID="EMailBenef" runat="server" BorderColor="Black" CssClass="active" Text="e-Mail"></asp:Label>
        <asp:TextBox ID="tbx_EMailContrib" runat="server" Columns="50" TextMode="Email" Width="206px" Style="text-transform: lowercase" TabIndex="95" BackColor="#CCFF99"></asp:TextBox>
        &nbsp; <asp:ImageButton ID="btn_RegistraBenef" runat="server" Height="18px" ImageUrl="~/Iconos/Diskete.ICO" Width="16px" TabIndex="98" ToolTip="Click para validar y Grabar datos de los Beneficiarios de Operaciones " OnClick="btn_RegistraBenef_Click" Visible="False" />
        &nbsp;<asp:ImageButton ID="btn_ModificaDatosBenef" runat="server" Height="18px" ImageUrl="~/Iconos/Editar.gif" Width="16px" TabIndex="99" ToolTip="Click para actualizar Información de Proveedor" OnClick="btn_ModificaDatosBenef_Click" Visible="False" />
         <br />
        <asp:Label ID="ETipoDiario" runat="server" BorderColor="Black" CssClass="active" Text="Diario" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Width="49px" Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_TipoDiario" runat="server" Width="33px" AutoPostBack="True" BackColor="#CCFF66" Enabled="False" TabIndex="500" Visible="False">E</asp:TextBox>
        <asp:Label ID="EDetalle" runat="server" BorderColor="Black" CssClass="active" Text="Detalle" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_DetalleComprob" runat="server" Width="427px" BackColor="#FFFF66" ForeColor="Black" TextMode="MultiLine" TabIndex="510" ToolTip="Ingrese el detalle de la Razon del Comprobante" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Visible="False" Height="16px"></asp:TextBox>
        <asp:Label ID="EFechaComprob" runat="server" BorderColor="Black" CssClass="active" Text="Fecha: " BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Width="45px" Visible="False"></asp:Label>
        &nbsp;<asp:TextBox ID="tbx_FechaComprob" runat="server" Width="87px" BackColor="#FFFF66" ForeColor="Black" TextMode="Date" TabIndex="520" AutoPostBack="True" Visible="False"></asp:TextBox>
    <asp:Label ID="ENumero" runat="server" BorderColor="Black" CssClass="active" Text="Número" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_NumDiario" runat="server" Width="87px" BackColor="#FFFF66" ForeColor="Black" TextMode="Number" TabIndex="530" Visible="False"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="btn_GrabarComprobante" runat="server" Height="20px" ImageUrl="~/Iconos/Grabar.gif" Width="20px" TabIndex="540" ToolTip="Click para Grabar los Datos Ingresados del Comprobante de Diario" BorderColor="Red" BorderStyle="None" Visible="False" OnClick="btn_GrabarComprobante_Click" />
        &nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="btn_IngresaTransacciones" runat="server" Height="20px" ImageUrl="~/Iconos/AddFinances.gif" TabIndex="550" ToolTip="Click para Ingresar Nueva Transacción" Width="20px" Visible="False" OnClick="btn_IngresaTransacciones_Click" />
         <br />
    <asp:Label ID="ESustento" runat="server" BorderColor="Black" CssClass="active" Text="Cod. Sustento" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="78px" Font-Size="Small"></asp:Label>
        <asp:DropDownList ID="ddl_Sustento" runat="server" Width="94px" DataSourceID="ds_Sustentos" DataTextField="DetalleSustento" DataValueField="CodigoSustento" TabIndex="201" Height="19px" Font-Size="X-Small" ToolTip="Escoja el Tipo de Sustento Tributario de la Transaccion" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="ddl_Sustento_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:TextBox ID="tbx_Sustento" runat="server" Width="19px" BackColor="#FFFF99" ForeColor="Black" TabIndex="203" Visible="False" Enabled="False">02</asp:TextBox>
    <asp:Label ID="EDocFuente" runat="server" BorderColor="Black" CssClass="active" Text="Doc. Fuente " BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="76px" Font-Size="Small" Height="16px"></asp:Label>
        <asp:DropDownList ID="ddl_TipoDocFuente" runat="server" Width="98px" DataSourceID="ds_TipoFuentes" DataTextField="DetalleDocumento" DataValueField="CodigoDocumento" TabIndex="100" Height="16px" ToolTip="Escoja el Tipo de Documento Fuente" Visible="False" AutoPostBack="True" Font-Size="X-Small" OnSelectedIndexChanged="ddl_TipoDocFuente_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="tbx_CodFuente" runat="server" Width="20px" BackColor="White" ForeColor="Black" TabIndex="102" Visible="False" Font-Size="X-Small">01</asp:TextBox>
        <asp:TextBox ID="tbx_DocFuente" runat="server" Width="74px" BackColor="White" ForeColor="Black" TabIndex="104" Visible="False" Font-Size="X-Small">Factura</asp:TextBox>
        <asp:TextBox ID="tbx_NumSucF" runat="server" Width="26px" BackColor="#FFFF99" ForeColor="Black" TabIndex="106" Visible="False">001</asp:TextBox>
        <asp:TextBox ID="tbx_NumCajaF" runat="server" Width="26px" BackColor="#FFFF99" ForeColor="Black" TabIndex="108" Visible="False">001</asp:TextBox>
        <asp:TextBox ID="tbx_NumDocFuente" runat="server" Width="85px" BackColor="#FFFF66" ForeColor="Black" TabIndex="110" Visible="False" style="text-align: right" AutoPostBack="True" OnTextChanged="tbx_NumDocFuente_TextChanged" ></asp:TextBox>
    <asp:Label ID="EFEmision" runat="server" BorderColor="Black" CssClass="active" Text="F.Emisión" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="70px" Font-Size="Small" Height="16px"></asp:Label>
         <asp:TextBox ID="tbx_FEmision" runat="server" Width="87px" BackColor="#FFFF66" ForeColor="Black" TabIndex="117" AutoPostBack="True" Visible="False" ToolTip="Fecha de Emision del Comprobante Fuente" OnTextChanged="tbx_FEmision_TextChanged"></asp:TextBox>
         <br />
    <asp:Label ID="EAutorizacionF" runat="server" BorderColor="#99FF66" CssClass="active" Text="Autorización" Visible="False" TabIndex="900" Font-Bold="True" Font-Size="Small" ForeColor="#669900"></asp:Label>
        &nbsp;<asp:ImageButton ID="btn_CopiaAutorizacion" runat="server" Height="16px" ImageUrl="~/Iconos/copy1.gif" Width="17px" TabIndex="113" ToolTip="Click para copiar Autorizacion almacenada" Visible="False" OnClick="btn_CopiaAutorizacion_Click"  />
        &nbsp;<asp:TextBox ID="tbx_NumAutorFuente" runat="server" Width="352px" BackColor="#FFFF66" ForeColor="Black" Height="16px" TabIndex="115" ToolTip="Ingrese el codigo de Autorizacion de los documentos relacionados con el Diario" Font-Size="X-Small" Visible="False"></asp:TextBox>
         <asp:TextBox ID="tbx_FCaducaFuente" runat="server" Width="87px" BackColor="#FFFF66" ForeColor="Black" TabIndex="117" AutoPostBack="True" Visible="False" OnTextChanged="tbx_FCaducaFuente_TextChanged"></asp:TextBox>
         <br />
    <asp:Label ID="EMonto14" runat="server" BorderColor="Black" CssClass="active" Text="V. con IVA 14%" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="100px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Monto14" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="120" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto14_TextChanged">0</asp:TextBox>
        <asp:TextBox ID="tbx_IVA14" runat="server" Width="60px" BackColor="White" ForeColor="Black" TabIndex="125" Visible="False" Enabled="False" style="text-align: right">0</asp:TextBox>
    <asp:Label ID="EMonto0" runat="server" BorderColor="Black" CssClass="active" Text="V. con IVA 0%" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="90px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Monto0" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="140" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto0_TextChanged">0</asp:TextBox>
    <asp:Label ID="EMonto12" runat="server" BorderColor="Black" CssClass="active" Text="V. con IVA 12%" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="101px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Monto12" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="130" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto12_TextChanged">0</asp:TextBox>
        <asp:TextBox ID="tbx_IVA12" runat="server" Width="60px" BackColor="White" ForeColor="Black" TabIndex="135" Visible="False" style="text-align: right" Enabled="False">0</asp:TextBox>
    <asp:Label ID="EMontoExcento" runat="server" BorderColor="Black" CssClass="active" Text="V. excenta IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="101px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_MontoExcento" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="130" Visible="False" style="text-align: right; height: 22px;" >0</asp:TextBox>
    <asp:Label ID="EICE" runat="server" BorderColor="Black" CssClass="active" Text="ICE" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="30px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_ICE" runat="server" Width="58px" BackColor="#FFFF66" ForeColor="Black" TabIndex="142" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto0_TextChanged">0</asp:TextBox>
         <br />
    <asp:Label ID="EBaseIVA" runat="server" BorderColor="Black" CssClass="active" Text="Base  IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="61px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_BaseIVA" runat="server" Width="70px" BackColor="White" ForeColor="Black" TabIndex="150" Visible="False" Enabled="False"  style="text-align: right">0</asp:TextBox>
    <asp:Label ID="EMontoIVA" runat="server" BorderColor="Black" CssClass="active" Text="IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="29px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_TotalIVA" runat="server" Width="60px" BackColor="White" ForeColor="Black" TabIndex="155" Visible="False" Enabled="False" style="text-align: right">0</asp:TextBox>
    <asp:Label ID="ETotalFactura" runat="server" BorderColor="Black" CssClass="active" Text="Total Factura" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="83px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_TotalFactura" runat="server" Width="70px" BackColor="#FF9900" ForeColor="Black" TabIndex="160" Visible="False" Enabled="False" style="text-align: right">0</asp:TextBox>
    <asp:Label ID="ESubSinICE" runat="server" BorderColor="Black" CssClass="active" Text="Base sin ICE" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="74px" Font-Size="Small"></asp:Label>
         &nbsp;<asp:TextBox ID="tbx_BaseSinICE" runat="server" Width="66px" ForeColor="Black" TabIndex="145" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto0_TextChanged" Enabled="False">0</asp:TextBox>
         <asp:Label ID="EParteRel" runat="server" BorderColor="Black" CssClass="active" Text="Parte Rel." BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="58px" Font-Size="Small" Height="16px"></asp:Label>
         &nbsp;<asp:ListBox ID="lbx_parteRel" runat="server" Height="20px" OnSelectedIndexChanged="lbx_parteRel_SelectedIndexChanged" Visible="False" Width="41px">
             <asp:ListItem>NO</asp:ListItem>
             <asp:ListItem>SI</asp:ListItem>
         </asp:ListBox>
        <asp:TextBox ID="tbx_ParteRel" runat="server" Width="26px" BackColor="#FFFF99" ForeColor="Black" TabIndex="106" Visible="False">NO</asp:TextBox>
         <asp:CheckBox ID="cbx_Bienes" runat="server" Visible="False" Text="Bienes" TabIndex="144" />
         <asp:CheckBox ID="cbx_Servicios" runat="server" Visible="False" Text="Servicios" TabIndex="146" />
        &nbsp;<asp:ImageButton ID="btn_ProcesaFuente" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" Width="22px" TabIndex="150" ToolTip="Click para Validar Fuente" Visible="False" OnClick="btn_ProcesaFuente_Click" />
         &nbsp;<asp:ImageButton ID="btn_GrabaRegCompra" runat="server" Height="20px" ImageUrl="~/Iconos/Grabar.gif" Width="20px" TabIndex="540" ToolTip="Click para Grabar los Datos de Compras" BorderColor="Red" BorderStyle="None" Visible="False" OnClick="btn_GrabaRegCompra_Click" />
         <br />
    <asp:Label ID="EBienes" runat="server" BorderColor="Black" CssClass="active" Text="Bienes" Visible="False" BackColor="#FFCC99" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Medium" Width="68px"></asp:Label>
        <asp:DropDownList ID="ddl_CuentaContableB" runat="server" Width="243px" DataSourceID="ds_CuentasContables" DataTextField="CuenContab" DataValueField="CodigoCuenta" TabIndex="215" Height="16px" AutoPostBack="True" Visible="False" Font-Size="X-Small" BackColor="#FFFF66" OnSelectedIndexChanged="ddl_CuentaContableB_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="tbx_CodigoCuentaB" runat="server" Width="114px" BackColor="#FFFF99" ForeColor="Black" TabIndex="220" Visible="False"></asp:TextBox>
         <asp:ImageButton ID="btn_CopiaValoresB" runat="server" Height="16px" ImageUrl="~/Iconos/copy1.gif" Width="17px" TabIndex="113" ToolTip="Click para valores de Factura a Bienes" Visible="False" OnClick="btn_CopiaValoresB_Click" />
    <asp:Label ID="EMontoB" runat="server" BorderColor="Black" CssClass="active" Text="Monto B." BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="54px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_ValorBienes" runat="server" Width="85px" BackColor="#FFFF66" ForeColor="Black" TabIndex="225" Visible="False" style="text-align: right" OnTextChanged="tbx_ValorBienes_TextChanged">0</asp:TextBox>
    <asp:Label ID="EIVAB" runat="server" BorderColor="Black" CssClass="active" Text="IVA B." BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="54px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_IVABienes" runat="server" Width="85px" BackColor="#FFFF66" ForeColor="Black" TabIndex="230" Visible="False" style="text-align: right" OnTextChanged="tbx_IVABienes_TextChanged" ToolTip="Base imponible del IVA en bienes">0</asp:TextBox>
         <br />
        <asp:TextBox ID="tbx_DetalleCuentaB" runat="server" Width="304px" BackColor="#FFFF99" ForeColor="Black" TabIndex="320" Visible="False"></asp:TextBox>
    <asp:Label ID="ECodTribB" runat="server" BorderColor="Black" CssClass="active" Text="Cod. Trib.Bienes" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="101px" Font-Size="Small"></asp:Label>
        <asp:DropDownList ID="ddl_TipoBienes" runat="server" Width="195px" DataSourceID="ds_ConceptosB" DataTextField="ConceptoIR" DataValueField="CodConcepto" TabIndex="205" ToolTip="Escoja el Tipo de Bienes de la Transacción" Visible="False" AutoPostBack="True" Font-Size="X-Small" BackColor="#FFFF66" OnSelectedIndexChanged="ddl_TipoBienes_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="tbx_TipoConceptoB" runat="server" Width="30px" BackColor="#FFFF99" ForeColor="Black" TabIndex="207" Visible="False" Font-Size="Small" Enabled="False" OnTextChanged="tbx_TipoConceptoB_TextChanged"></asp:TextBox>
        <asp:DropDownList ID="ddl_CodIVAB" runat="server" Width="94px" DataSourceID="ds_CodIVA" DataTextField="DConceptoIVA" DataValueField="CodIVA" TabIndex="231" Height="19px" Font-Size="X-Small" ToolTip="Escoja el Codigo de IVA en Bienes" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="ddl_CodIVAB_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:TextBox ID="tbx_CodIVAB" runat="server" Width="30px" BackColor="White" ForeColor="Black" TabIndex="235" Visible="False" style="text-align: right" OnTextChanged="tbx_PorcenIVAB_TextChanged" ToolTip="Porcentaje de Retencion de IVA bienes"></asp:TextBox>
         <br />
    <asp:Label ID="ERetenIVAB" runat="server" BorderColor="Black" CssClass="active" Text=" Reten. IVA B." BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="80px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_PorcenRetenIVAB" runat="server" Width="33px" BackColor="White" ForeColor="Black" TabIndex="237" Visible="False" style="text-align: right" OnTextChanged="tbx_PorcenIVAB_TextChanged" ToolTip="Porcentaje de Retencion de IVA bienes" Height="16px">0</asp:TextBox>
        <asp:TextBox ID="tbx_IVARetenB" runat="server" Width="70px" BackColor="#FFFF99" ForeColor="Black" TabIndex="240" Visible="False" style="text-align: right" OnTextChanged="tbx_IVARetenB_TextChanged" ToolTip="Valor de Retencion de IVA en Bienes">0</asp:TextBox>
    <asp:Label ID="ERetenIRB" runat="server" BorderColor="Black" CssClass="active" Text="Reten. IR" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="56px" Font-Size="Small"></asp:Label>
        &nbsp;<asp:ImageButton ID="btn_CopiaBaseIRB" runat="server" Height="16px" ImageUrl="~/Iconos/copy1.gif" Width="17px" TabIndex="113" ToolTip="Click para copiar subtotal a Base IR Bienes" Visible="False" OnClick="btn_CopiaBaseIRB_Click" />
    &nbsp;<asp:TextBox ID="tbx_BaseIRB" runat="server" Width="70px" BackColor="#FFFF99" ForeColor="Black" TabIndex="250" Visible="False" OnTextChanged="tbx_IRBienes_TextChanged" style="text-align: right" ToolTip="Base Imponible para Retencion de Impuesto a la Renta en Bienes">0</asp:TextBox>
        <asp:TextBox ID="tbx_PorcenRetenIRB" runat="server" Width="36px" BackColor="White" ForeColor="Black" TabIndex="255" Visible="False" style="text-align: right" OnTextChanged="tbx_PorcenRetenIRB_TextChanged" ToolTip="Porcentaje de retencion de IR en bienes">0</asp:TextBox>
        <asp:TextBox ID="tbx_IRRetenB" runat="server" Width="70px" BackColor="#FFFF99" ForeColor="Black" TabIndex="260" Visible="False" OnTextChanged="tbx_IRBienes_TextChanged" style="text-align: right">0</asp:TextBox>
         &nbsp;&nbsp;<asp:ImageButton ID="btn_ProcesaBienes" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" Width="22px" TabIndex="215" ToolTip="Click para Validar Bienes" Visible="False" OnClick="btn_ProcesaBienes_Click" />
         <br />
                  &nbsp; &nbsp;
         <br />
    <asp:Label ID="ETRetenIVA" runat="server" BorderColor="Black" CssClass="active" Text="IVA Reten." BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="71px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_TotalRetenIVA" runat="server" Width="70px" BackColor="White" ForeColor="Black" TabIndex="430" Visible="False" style="text-align: right">0</asp:TextBox>
         <asp:Label ID="ETRetenIR" runat="server" BorderColor="Black" CssClass="active" Text="IR reten." BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="56px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_TotalRetenIR" runat="server" Width="70px" BackColor="#FFFFCC" ForeColor="Black" TabIndex="440" Visible="False" style="text-align: right">0</asp:TextBox>
         <asp:Label ID="ESaldoXPagar" runat="server" BorderColor="Black" CssClass="active" Text="X Pagar" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="53px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_SaldoXPagar" runat="server" Width="70px" BackColor="#FF9900" ForeColor="Black" TabIndex="160" Visible="False" style="text-align: right">0</asp:TextBox>
         <asp:ImageButton ID="btn_ProcesaRetencion" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" Width="22px" TabIndex="215" ToolTip="Click para preparar proceso retenciones" Visible="False" OnClick="btn_ProcesaRetencion_Click"/>
         <br />
    <asp:Label ID="ENumRetencion" runat="server" BorderColor="Black" CssClass="active" Text="Retencion N° " BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="87px" Font-Size="Small"></asp:Label>
         <asp:TextBox ID="tbx_NumSucC" runat="server" Width="32px" BackColor="#FFFF99" ForeColor="Black" TabIndex="400" Visible="False">001</asp:TextBox>
        <asp:TextBox ID="tbx_NumCajaC" runat="server" Width="26px" BackColor="#FFFF99" ForeColor="Black" TabIndex="410" Visible="False">001</asp:TextBox>
         <asp:TextBox ID="tbx_NumDocReten" runat="server" Width="92px" BackColor="#FFFF66" ForeColor="Black" TabIndex="420" Visible="False" style="text-align: right" ></asp:TextBox>
         &nbsp;<asp:ImageButton ID="btn_CargaDatosAutor" runat="server" Height="16px" ImageUrl="~/Iconos/copy1.gif" Width="17px" TabIndex="113" ToolTip="Click paracargar datos de Autorización de la Institución" Visible="False" OnClick="btn_CargaDatosAutor_Click"  />
        &nbsp;<asp:TextBox ID="tbx_NumAutorReten" runat="server" Width="294px" BackColor="#FFFF66" ForeColor="Black" Height="16px" TabIndex="425" ToolTip="Ingrese el codigo de Autorizacion del comprobante de retencion a emitir" Font-Size="X-Small" Visible="False" ></asp:TextBox>
         <asp:TextBox ID="tbx_FCaducaReten" runat="server" Width="78px" BackColor="#FFFF66" ForeColor="Black" TabIndex="428" Visible="False" style="text-align: right" AutoPostBack="True" OnTextChanged="tbx_FCaducaReten_TextChanged"></asp:TextBox>
         &nbsp; <asp:ImageButton ID="btn_RegistraDocumento" runat="server" Height="20px" ImageUrl="~/Iconos/Grabar.gif" Width="30px" TabIndex="430" ToolTip="Click para Registrar Transacciones" Visible="False" OnClick="btn_RegistraDocumento_Click"/>
        &nbsp;&nbsp; 
         &nbsp;&nbsp;<asp:ImageButton ID="btn_ImprimeRetencion" runat="server" Height="20px" ImageUrl="~/Images/Reporte2.png" TabIndex="440" ToolTip="Click para Imprimir Comprobante" Width="30px" Visible="False" OnClick="btn_ImprimeRetencion_Click" />
         &nbsp;&nbsp;<asp:ImageButton ID="btn_IngresaPago" runat="server" Height="20px" ImageUrl="~/Iconos/Nuevo.gif" TabIndex="450" ToolTip="Click para Ingresar detalle de Pago" Width="30px" Visible="False" OnClick="btn_IngresaPago_Click"  />
         <br />
         <br />
    <asp:Label ID="EFormaPago" runat="server" BorderColor="Black" CssClass="active" Text="Forma de Pago" Visible="False" BackColor="#FFCC99" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Small" Width="101px"></asp:Label>
        <asp:DropDownList ID="ddl_FPago" runat="server" Width="94px" DataSourceID="ds_FPago" DataTextField="DetalleFPago" DataValueField="CodigoFPago" TabIndex="505" Height="19px" Font-Size="X-Small" ToolTip="Escoja la Forma de Pago" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="ddl_FPago_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:TextBox ID="tbx_FPago" runat="server" Width="28px" BackColor="#FFFF99" ForeColor="Black" TabIndex="510" Visible="False">02</asp:TextBox>
         <asp:Label ID="ETotalCheques0" runat="server" BorderColor="Black" CssClass="active" Text="Documento" Width="72px" BorderStyle="Solid" BorderWidth="1px" ForeColor="#669900" Font-Bold="True" Height="19px" Font-Size="Small" Visible="False"></asp:Label>
        <asp:DropDownList ID="ddl_TipoDoc1" runat="server" Width="94px" DataSourceID="ds_TipoDocumentos" DataTextField="DetalleDocumento" DataValueField="CodigoDocumento" TabIndex="550" Height="19px" Font-Size="X-Small" ToolTip="Escoja el Tipo de Instrumento con el que Gira la Ayuda" AutoPostBack="True" OnSelectedIndexChanged="ddl_TipoDoc1_SelectedIndexChanged" Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="tbx_TipoDoc1" runat="server" Width="52px" AutoPostBack="True" BackColor="#CCFF66" TabIndex="430" ForeColor="Black" style="text-align: left" Visible="False">CHEQ</asp:TextBox>
        <asp:TextBox ID="tbx_NumDoc1" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="560" ToolTip="Ingrese el Numero de Documento del Instrumento de la Ayuda" style="text-align: right" Visible="False">0</asp:TextBox>
        <asp:TextBox ID="tbx_SecuenTran" runat="server" Width="18px" BackColor="#FFFF66" ForeColor="Black" TabIndex="560" ToolTip="Ingrese el Numero de Transacion con esa Referencia" style="text-align: right" Visible="False">0</asp:TextBox>
        <asp:ImageButton ID="btn_ProcesaValPendientes" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" Width="22px" TabIndex="215" ToolTip="Click para Validar Forma de Pago" Visible="False" OnClick="btn_ProcesaValPendientes_Click" />
         <br />
    <asp:Label ID="ECuentaPagadora" runat="server" BorderColor="Black" CssClass="active" Text="Cuenta Giradora" Visible="False" BackColor="#FFCC99" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Small" Width="101px"></asp:Label>
        <asp:DropDownList ID="ddl_CuentaPagadora" runat="server" Width="338px" DataSourceID="ds_CuentasContables" DataTextField="CuenContab" DataValueField="CodigoCuenta" TabIndex="520" Height="17px" OnSelectedIndexChanged="ddl_CuentaContable_SelectedIndexChanged" AutoPostBack="True" Visible="False" Font-Size="X-Small">
        </asp:DropDownList>
        <asp:TextBox ID="tbx_CuentaPagadora" runat="server" Width="124px" BackColor="#FFFF66" ForeColor="Black" TabIndex="530" ToolTip="Ingrese el Numero de Documento del Instrumento de la Ayuda" Visible="False" OnTextChanged="tbx_CuentaPagadora_TextChanged">1103101001</asp:TextBox>
    <asp:Label ID="EMontoPagado" runat="server" BorderColor="Black" CssClass="active" Text="Valor" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="39px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_ValorPagado" runat="server" Width="85px" BackColor="#FFFF66" ForeColor="Black" TabIndex="540" Visible="False" style="text-align: right">0</asp:TextBox>
         <asp:ImageButton ID="btn_RegistraPago" runat="server" Height="20px" ImageUrl="~/Iconos/Grabar.gif" Width="30px" TabIndex="570" ToolTip="Click para Registrar Pago o CXP" Visible="False" OnClick="btn_RegistraPago_Click"/>
         <br />
         <asp:Label ID="ETotalCheques" runat="server" BorderColor="Black" CssClass="active" Text="T. pendiente Doc. Financiero" Width="173px" BorderStyle="Solid" BorderWidth="1px" ForeColor="#669900" Font-Bold="True" Height="19px" Font-Size="Small" Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_TotalCheque" runat="server" Width="75px" BackColor="#FFCC99" ForeColor="Red" TabIndex="192" Height="20px" Visible="False" style="text-align: right"></asp:TextBox>
         <asp:ImageButton ID="btn_CopiaValPagACheque" runat="server" Height="16px" ImageUrl="~/Iconos/copy1.gif" Width="17px" TabIndex="113" ToolTip="Click valor Pagado a Valor cheque" Visible="False" OnClick="btn_CopiaValPagACheque_Click" />
         &nbsp;
         <asp:Label ID="EVCheque" runat="server" BorderColor="Black" CssClass="active" Text="V. Cheque/Transfer" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="116px" Font-Size="Small"></asp:Label>
         <asp:TextBox ID="tbx_ValorCheque" runat="server" Width="75px" BackColor="#FFFF66" ForeColor="Black" TabIndex="540" Visible="False" style="text-align: right">0</asp:TextBox>
         &nbsp;
         <asp:ImageButton ID="btn_GrabaCheque" runat="server" Height="22px" ImageUrl="~/Images/Guardar.png" Width="34px" TabIndex="150" ToolTip="Click para generar Cheque" BorderColor="Red" BorderStyle="None" Visible="False" OnClick="btn_GrabaCheque_Click" />
        &nbsp;&nbsp; <asp:ImageButton ID="btn_ImprimeCheque1" runat="server" Height="20px" ImageUrl="~/Iconos/Cheque.ICO" TabIndex="580" ToolTip="Click para Imprimir Cheque Ayuda a Beneficiario 1" Width="30px" Visible="False" style="background-color: #3366FF" OnClick="btn_ImprimeCheque1_Click" />
                  &nbsp;&nbsp;<br />
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;
         <br />
         <br />
         </asp:View>
                         <asp:View ID="v_DatosActivoF" runat="server">
                             <asp:Panel ID="pnl_DatosBasicosActivo" runat="server" Height="210px" Width="900px">
                                 <asp:Button ID="btn_DatosFactura" runat="server" CommandArgument="v_DatosCompra" CommandName="SwitchViewByID" Text="Datos Compra" />
                                 <br />
                                 <asp:Label ID="ECategoria" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Categoria" Width="63px"></asp:Label>
                                 <asp:DropDownList ID="ddl_Categoria" runat="server" AutoPostBack="True" DataSourceID="ds_CatActivos" DataTextField="Descripcion" DataValueField="Categoria" Font-Size="X-Small" Height="25px" TabIndex="505" ToolTip="Escoja Categoria del Activo" Width="72px" OnSelectedIndexChanged="ddl_Categoria_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_Categoria" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="510" ToolTip="Codigo de Categoria" Width="18px" style="font-weight: 700">10</asp:TextBox>
                                 <asp:Label ID="ECentroCostos" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="18px" Text="Centro Costos" Width="90px"></asp:Label>
                                 <asp:DropDownList ID="ddl_CentroCostos" runat="server" AutoPostBack="True" DataSourceID="ds_CentroCostos" DataTextField="DescripcionCentroCosto" DataValueField="CodCentroCosto" Font-Size="X-Small" Height="19px" TabIndex="515" ToolTip="Escoja Centro de Costos que afecta el activo" Width="70px" OnSelectedIndexChanged="ddl_CentroCostos_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_CentroCostos" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="520" ToolTip="Centro de Costos" Width="24px" style="font-weight: 700">000</asp:TextBox>
                                 <asp:Label ID="EGrupoActivos" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Grupo" Width="50px"></asp:Label>
                                 <asp:DropDownList ID="ddl_GrupoActivos" runat="server" AutoPostBack="True" DataSourceID="ds_GrupoActivos" DataTextField="Descripcion" DataValueField="CodGrupoActivo" Font-Size="X-Small" Height="25px" TabIndex="525" ToolTip="Escoja el Grupo al que pertenece el activo" Width="74px" OnSelectedIndexChanged="ddl_GrupoActivos_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_GrupoActivos" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="530" ToolTip="Codigo de Grupo de Activos" Width="43px" style="font-weight: 700"></asp:TextBox>
                                 <asp:Label ID="ECodigoActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Codigo Activo" Width="85px"></asp:Label>
                                 <asp:TextBox ID="tbx_CodigoActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="540" ToolTip="Ingrese el Codigo del Activo" Width="126px"></asp:TextBox>
                                 &nbsp;&nbsp;<asp:ImageButton ID="btn_GeneraCodigo" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" TabIndex="550" ToolTip="Click para Escojer Cuenta Contable que afecta la Transaccion" Visible="False" Width="22px" OnClick="btn_GeneraCodigo_Click" />
                                 &nbsp;<br />
                                 <asp:Label ID="ENombreActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Nombre Activo" Width="120px"></asp:Label>
                                 <asp:TextBox ID="tbx_NombreActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="560" ToolTip="Ingrese un Nombre corto del Activo" Width="300px"></asp:TextBox>
                                 <asp:Label ID="EMarcaActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Marca" Width="50px"></asp:Label>
                                 <asp:TextBox ID="tbx_MarcaActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="570" ToolTip="Ingrese la Marca del Activo" Width="300px"></asp:TextBox>
                                 <br />
                                 <asp:Label ID="EDescripcionActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Descripcion" Width="120px"></asp:Label>
                                 <asp:TextBox ID="tbx_DescripcionActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="580" ToolTip="Ingrese una descripción detallada del activo" Width="300px"></asp:TextBox>
                                 <asp:Label ID="ESerieActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Serie" Width="50px"></asp:Label>
                                 <asp:TextBox ID="tbx_SerieActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="590" ToolTip="número de serie del Activo" Width="300px"></asp:TextBox>
                                 <br />
                                 <asp:Label ID="EFechaIngreso" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="16px" Text="Fecha Ingreso" Width="120px"></asp:Label>
                                 <asp:TextBox ID="tbx_FechaIngreso" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="600" ToolTip="Fecha de Adquisicion del Activo" Width="90px"></asp:TextBox>
                                 <asp:Label ID="ERazonIngreso" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Codigo Ingreso" Width="90px"></asp:Label>
                                 <asp:DropDownList ID="ddl_RazonIngreso" runat="server" AutoPostBack="True" DataSourceID="ds_CodTranActivos" DataTextField="Descripcion" DataValueField="CodTranActivo" Font-Size="X-Small" Height="19px" TabIndex="610" ToolTip="Escoja la Razon del Ingreso" Width="94px" OnSelectedIndexChanged="ddl_RazonIngreso_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_RazonIngreso" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="620" ToolTip="Codigo de Razon del Ingreso" Width="40px"></asp:TextBox>
                                 <asp:Label ID="EVidaUtil" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Vida Util " Width="54px"></asp:Label>
                                 <asp:TextBox ID="tbx_VidaUtil" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="630" ToolTip="Ingrese # de meses de Vida util del activo" Width="48px"></asp:TextBox>
                                 <br />
                                 <asp:Label ID="EValorInicial" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Valor Inicial" Width="120px"></asp:Label>
                                 <asp:TextBox ID="tbx_ValorInicial" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="640" ToolTip="Ingrese el Valor Inicial del Activo" Width="90px"></asp:TextBox>
                                 <asp:Label ID="EMetodoDeprecia" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Metod.Deprec." Width="90px"></asp:Label>
                                 <asp:DropDownList ID="ddl_MetodoDeprecia" runat="server" AutoPostBack="True" DataSourceID="ds_MetodoDeprecia" DataTextField="DescripcionDeprecia" DataValueField="CodTipoDeprecia" Font-Size="X-Small" Height="19px" TabIndex="650" ToolTip="Escoja el Metodo de Depreciacion" Width="94px">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_MetodoDeprecia" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="660" ToolTip="Codigo del Metodo de depreciacion" Width="20px" style="font-weight: 700">10</asp:TextBox>
                                 <asp:Label ID="EValorResidual" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Valor Residual" Width="101px"></asp:Label>
                                 <asp:TextBox ID="tbx_PorcenValResid" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="670" ToolTip="Ingrese el porcentaje del valor residual del activo" Width="42px"></asp:TextBox>
                                 <asp:TextBox ID="tbx_ValorResidual" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="680" ToolTip="Ingrese el Valor Residual del Activo" Width="94px"></asp:TextBox>
                                 <br />
                                 <asp:Label ID="EUbicacion" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Ubicación" Width="70px"></asp:Label>
                                 <asp:DropDownList ID="ddl_UbicaHabita" runat="server" AutoPostBack="True" DataSourceID="ds_Habitaciones" DataTextField="NombreProducto" DataValueField="CodigoProducto" Font-Size="X-Small" Height="25px" TabIndex="690" ToolTip="Escoja la Habitación en el que esta ubicado el activo" Width="74px" OnSelectedIndexChanged="ddl_UbicaHabita_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_NumHabitacion" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="700" ToolTip="Codigo de la habitacion en donde esta ubicado el activo" Width="40px"></asp:TextBox>
                                 <asp:TextBox ID="tbx_Ubicacion" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="710" ToolTip="Ingrese el detalle de la ubicacion del activo" Width="250px"></asp:TextBox>
                                 <asp:Label ID="EResponsable" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Responsable" Width="70px"></asp:Label>
                                 <asp:TextBox ID="tbx_Responsable" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="720" ToolTip="Ingrese Responsable de custodia del activo" Width="246px"></asp:TextBox>
                                 <br />
                                 <asp:ImageButton ID="btn_NuevoComprobante1" runat="server" Height="30px" ImageUrl="~/Iconos/Mas.png" OnClick="btn_NuevoComprobante1_Click" TabIndex="900" ToolTip="Click para Ingresar Nuevo Comprobante" Width="30px" />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:ImageButton ID="btn_RegistraDocumento0" runat="server" Height="30px" ImageUrl="~/Iconos/Grabar.gif" OnClick="btn_RegistraDocumento_Click" TabIndex="430" ToolTip="Click para Registrar Transacciones" Visible="False" Width="30px" />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:ImageButton ID="btn_HabilitaModifDatosProv0" runat="server" BackColor="Red" Height="22px" ImageUrl="~/Iconos/Edit1.png" OnClick="btn_HabilitaModifDatosProv_Click" TabIndex="30" ToolTip="Click para habilitar Modificar Datos del Proveedor" Visible="False" Width="26px" />
                                 &nbsp;&nbsp;&nbsp;
                                 <asp:ImageButton ID="btn_EliminaTransaccion" runat="server" BorderColor="Red" Height="23px" ImageUrl="~/Iconos/Eliminar.gif" TabIndex="299" ToolTip="Click para Eliminar Transaccion Contable" Visible="False" Width="26px" />
                                 &nbsp;&nbsp;&nbsp;
                                 <asp:ImageButton ID="btn_ModificaTransaccion" runat="server" BorderColor="Red" BorderStyle="None" CausesValidation="False" Height="18px" ImageUrl="~/Iconos/Modificar1.png" TabIndex="150" ToolTip="Click para grabar cambios en la cabecera del Comprobante" Visible="False" Width="19px" />
                             </asp:Panel>
                             <asp:Panel ID="pnl_ActivosRelFactura" runat="server" Height="250px" ScrollBars="Vertical" Width="981px">
                                 <asp:GridView ID="gv_DatosActivos" runat="server" AutoGenerateColumns="False" DataSourceID="ds_ActivosFijos" Height="150px" OnSelectedIndexChanged="gv_DatosActivos_SelectedIndexChanged" style="margin-top: 2px; margin-right: 2px;" Width="900px">
                                     <Columns>
                                         <asp:CommandField ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" ShowSelectButton="True" />
                                         <asp:BoundField DataField="CodigoActivo" HeaderText="Codigo" SortExpression="CodigoActivo">
                                         <ItemStyle Font-Bold="True" Font-Size="Small" Width="100px" Wrap="False" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="Grupo" HeaderText="Grp" SortExpression="Grupo">
                                         <ItemStyle Width="15px" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="NombreDelActivo" HeaderText="Nombre" SortExpression="NombreDelActivo">
                                         <ItemStyle Font-Size="Small" Width="200px" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="Descripción" HeaderText="Descripción" SortExpression="Descripción">
                                         <ItemStyle Font-Size="XX-Small" Width="100px" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                                         <asp:BoundField DataField="NúmDeSerie" HeaderText="Serie" SortExpression="NúmDeSerie" />
                                         <asp:BoundField DataField="FechaIngreso" DataFormatString="{0:dd/MM/yyyy}" HeaderText="F.Ingreso" SortExpression="FechaIngreso" />
                                         <asp:BoundField DataField="VidaUtil" HeaderText="V.Util" SortExpression="VidaUtil">
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="TasaDepreciacionAnual" HeaderText="T.Dep." SortExpression="TasaDepreciacionAnual">
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="FechaUltimaDepreciacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Ult.Dep." SortExpression="FechaUltimaDepreciacion" />
                                         <asp:BoundField DataField="ValorInicial" DataFormatString="{0:0.00}" HeaderText="V.Inicial" SortExpression="ValorInicial">
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="DepreciacionAcumulada" DataFormatString="{0:0.00}" HeaderText="Dep.Acum" SortExpression="DepreciacionAcumulada">
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="ValorActual" DataFormatString="{0:0.00}" HeaderText="V.Actual" SortExpression="ValorActual">
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                     </Columns>
                                 </asp:GridView>
                                 <br />
                                 <asp:Panel ID="pnl_DatosActivosAnter" runat="server" Height="80px" Visible="False" Width="850px">
                                     <asp:Label ID="EFUltimaDeprecia" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="16px" Text="Fecha Ult. Deprecia." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimaDeprecia" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_FEmision_TextChanged" TabIndex="117" ToolTip="Fecha de Emision del Comprobante Fuente" Visible="False" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EDepreciaAcumul" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Dep. Norm. Acum." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_DepreciaNormAcum0" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <br />
                                     <asp:Label ID="EFechaUltimReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Fecha Ult. Revaloriz." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimReval" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValorReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Valor Revaloriz." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorReval" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EFUltimaDepReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="F. Ult. Deprec. Reval." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimaDepReval" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValorDepReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="V. Deprec. Reval." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorDepreReval" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <br />
                                     <asp:Label ID="EFUltimaMejora" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Fecha Ult. Mejora" Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimaMejora" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValorMejoras" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Valor Mejoras" Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorMejoras" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EFUltimDepMejoras" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="F. Ult. Deprec. Mejor." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimDepMejoras" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValDepMejoras" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="V. Deprec. Mejor." Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorDepMejoras" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_CuentaPagadora_TextChanged" TabIndex="530" ToolTip="Ingrese el Codigo del Activo" Visible="False" Width="80px"></asp:TextBox>
                                 </asp:Panel>
                             </asp:Panel>
                 <br />
                 <br />
                 <br />
                 <br />
             </asp:View>

         </asp:MultiView>
         <br />
         <asp:ImageButton ID="btn_NuevoComprobante" runat="server" Height="30px" ImageUrl="~/Iconos/AddRegistro.gif" TabIndex="900" ToolTip="Click para Ingresar Nuevo Comprobante" Width="30px" OnClick="btn_NuevoComprobante_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="btn_BuscaOtroDiario" runat="server" Height="26px" ImageUrl="~/Iconos/Lupa.gif" Width="29px" TabIndex="800" OnClick="btn_BuscaOtroDiario_Click" ToolTip="Click para Buscar Comprobante" Visible="False"  />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="btn_VistaPreComprobante" runat="server" Height="30px" ImageUrl="~/Iconos/VistaPrevia.gif" TabIndex="7" ToolTip="Click para Vista previa de Comprobante" Width="30px" OnClick="btn_VistaPreComprobante_Click" Visible="False" />
         &nbsp;&nbsp; <asp:ImageButton ID="btn_ImprimeComprobante" runat="server" Height="30px" ImageUrl="~/Iconos/Impresora.gif" TabIndex="9" ToolTip="Click para Imprimir Comprobante" Width="30px" OnClick="btn_ImprimeComprobante_Click" Visible="False" />

         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input id="CLOSE" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 80px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
         <asp:Label ID="EDiferencia" runat="server" BorderColor="Black" CssClass="active" Text="Totales de Transacciones:" Width="250px" BorderStyle="Solid" BorderWidth="1px" ForeColor="#669900" Font-Bold="True" Height="19px" Font-Size="Small" Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbx_Diferencia" runat="server" Width="90px" BackColor="#FFCC99" ForeColor="Red" TabIndex="192" Height="20px" Visible="False"></asp:TextBox>
        &nbsp;<asp:TextBox ID="tbx_TotalDebe" runat="server" Width="90px" BackColor="#FFFF66" ForeColor="Black" TabIndex="194" Font-Bold="True" Font-Size="Small" Height="20px" Visible="False"></asp:TextBox>
        &nbsp;<asp:TextBox ID="tbx_TotalHaber" runat="server" Width="90px" BackColor="#FFFF66" ForeColor="Black" TabIndex="196" Font-Bold="True" Font-Size="Small" Height="20px" Visible="False"></asp:TextBox>
         <asp:GridView ID="dgv_TransaccionesContables" runat="server" AutoGenerateColumns="False" DataKeyNames="NumDiario,TipoDiario,FechaDiario,CodigoCuenta,SecuenTran" DataSourceID="ds_TransaccionesContables" OnSelectedIndexChanged="dgv_TransaccionesContables_SelectedIndexChanged" BackColor="#CCFFFF" TabIndex="190">
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Iconos/SenalDer.ICO" />
            <asp:BoundField DataField="CodigoCuenta" HeaderText="Codigo" ReadOnly="True" SortExpression="CodigoCuenta">
            <HeaderStyle Font-Size="Small" />
            <ItemStyle Width="125px" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="DetalleCuenta" HeaderText="Cuenta" SortExpression="DetalleCuenta">
            <HeaderStyle Font-Size="Small" />
            <ItemStyle Font-Size="X-Small" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="SecuenTran" HeaderText="Sq" SortExpression="SecuenTran" ReadOnly="True">
            <HeaderStyle Font-Size="Small" />
            <ItemStyle Width="10px" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorDebe" HeaderText="Debe" SortExpression="ValorDebe" DataFormatString="{0:#,##0.00}">
            <HeaderStyle HorizontalAlign="Right" Font-Size="Small" />
            <ItemStyle HorizontalAlign="Right" Width="100px" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="ValorHaber" HeaderText="Haber" SortExpression="ValorHaber" DataFormatString="{0:#,##0.00}">
            <HeaderStyle HorizontalAlign="Right" Font-Size="Small" />
            <ItemStyle HorizontalAlign="Right" Width="100px" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoDocTran" HeaderText="Doc" SortExpression="TipoDocTran" >
            <HeaderStyle Font-Size="Small" />
            <ItemStyle Font-Size="X-Small" Width="25px" />
            </asp:BoundField>
            <asp:BoundField DataField="NumDocTran" HeaderText="NDoc" SortExpression="NumDocTran" >
            <HeaderStyle Font-Size="Small" />
            <ItemStyle Font-Size="X-Small" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="ReferDocTran" HeaderText="Referencia" SortExpression="ReferDocTran" >
            <HeaderStyle Font-Size="Small" />
            <ItemStyle Font-Size="X-Small" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="AutorizaDocTran" HeaderText="Autorizacion" SortExpression="AutorizaDocTran" >
            <HeaderStyle Font-Size="Small" />
            <ItemStyle Font-Size="XX-Small" Width="250px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
         <asp:GridView ID="dgv_Cheques" runat="server" AutoGenerateColumns="False" DataSourceID="ds_Cheques" OnSelectedIndexChanged="dgv_TransaccionesContables_SelectedIndexChanged" BackColor="#CCFFFF" TabIndex="190">
        <Columns>
            <asp:BoundField DataField="ValorHaber" HeaderText="Det. Rel. Cheq." SortExpression="ValorHaber" DataFormatString="{0:0.00}">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
    <asp:SqlDataSource ID="ds_TipoDocumentos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoDocumento], [DetalleDocumento] FROM AContabilidad.[APContabTipoDocumentos] ORDER BY [NumDocumento]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TipoFuentes" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoDocumento], [DetalleDocumento] FROM AContabilidad.[APContabAnexTipoDoc] ORDER BY CodigoDocumento"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_ConceptosB" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodConcepto], CodConcepto + '-------(' +CONVERT (nvarchar(2), Porcen) + ')-------'  + [ConceptoIR] AS ConceptoIR, Porcen FROM AContabilidad.[APContabAnexConceptos] WHERE Tipo='B' ORDER BY CodConcepto 
"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_Sustentos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoSustento], [DetalleSustento] FROM AContabilidad.[APContabAnexSustentos] ORDER BY CodigoSustento"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_CodIVA" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodIVA], CodIVA +  '   ' +  [ConceptoIVA]  AS DConceptoIVA  FROM AContabilidad.[APContabAnexCodIVA] ORDER BY CodIVA"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_FPago" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoFPago], [DetalleFPago] FROM AContabilidad.[APContabAnexFormaPago] ORDER BY CodigoFPago"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_ConceptosS" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodConcepto, CodConcepto + '-------(' +CONVERT (nvarchar(2), Porcen) + ')-------' + ConceptoIR AS ConceptoIR, Porcen FROM AContabilidad.APContabAnexConceptos WHERE (Tipo = 'S') ORDER BY CodConcepto"></asp:SqlDataSource>
        <asp:SqlDataSource ID="ds_CuentasContables" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoCuenta] + '   ' + DetalleCuenta AS CuenContab, TipoCuenta, [CodigoCuenta] FROM AContabilidad.[APContabPlan] WHERE ([TipoCuenta] = @TipoCuenta)" OnSelecting="ds_CuentasContables_Selecting">
            <SelectParameters>
                <asp:Parameter DefaultValue="D" Name="TipoCuenta" Type="String" />
            </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TransaccionesContables" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT AContabilidad.APContabTransacciones.NumDiario, AContabilidad.APContabTransacciones.TipoDiario, AContabilidad.APContabTransacciones.FechaDiario, AContabilidad.APContabTransacciones.CodigoCuenta, AContabilidad.APContabTransacciones.SecuenTran, AContabilidad.APContabTransacciones.TipoTran, AContabilidad.APContabTransacciones.ValorDebe, AContabilidad.APContabTransacciones.ValorHaber, AContabilidad.APContabTransacciones.TipoDocTran, AContabilidad.APContabTransacciones.ReferDocTran, AContabilidad.APContabTransacciones.AutorizaDocTran, AContabilidad.APContabTransacciones.NumDocTran, AContabilidad.APContabTransacciones.ObservTran AS Nota, AContabilidad.APContabPlan.DetalleCuenta FROM AContabilidad.APContabTransacciones INNER JOIN AContabilidad.APContabPlan ON AContabilidad.APContabTransacciones.CodigoCuenta = AContabilidad.APContabPlan.CodigoCuenta WHERE (AContabilidad.APContabTransacciones.NumDiario = @NumDiario)" DeleteCommand="DELETE FROM AContabilidad.APContabTransacciones WHERE (TipoDiario = @TipoDiario) AND (NumDiario = @NumDiario) AND (CodigoCuenta = @CodigoCuenta) AND (SecuenTran = @SecuenTran)">
        <DeleteParameters>
            <asp:Parameter Name="TipoDiario" />
            <asp:Parameter Name="NumDiario" />
            <asp:Parameter Name="CodigoCuenta" />
            <asp:Parameter Name="SecuenTran" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="tbx_NumDiario" DefaultValue="0" Name="NumDiario" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TipoDiario" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoDiario], [DetalleDiario] FROM AContabilidad.[APContabTipoDiarios] ORDER BY SecuenCodDiario ASC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TipoBenef" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoBenef], [DetalleBenef], [OrdenCodigo] FROM AContabilidad.[APContabTipoBenef] ORDER BY [OrdenCodigo]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_TipoContrib" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoContrib], [DetalleContrib], [OrdenCodigo] FROM AContabilidad.[APContabTipoContrib] ORDER BY [OrdenCodigo]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_Beneficiarios" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT ApellidosNombresU AS AN , IdenBenef FROM AContabilidad.APContabBenef ORDER BY ApellidosNombresU"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_CentroCostos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodCentroCosto], [DescripcionCentroCosto] FROM AContabilidad.[APContabCentrosCosto]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_ComprobantesContables" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT TipoDiario + ' ' + CONVERT(nvarchar, FechaDiario) + '     ' + CONVERT(nvarchar,NumDiario) +  '     ' + SUBSTRING(NombreBenef,0,30) + ' ' + SUBSTRING(DetalleDiario,0,100) AS DC, NumDiario FROM AContabilidad.APContabDiario ORDER BY NumDiario DESC" DeleteCommand="DELETE FROM AContabilidad.APContabTransacciones WHERE (TipoDiario = @TipoDiario) AND (NumDiario = @NumDiario) AND (CodigoCuenta = @CodigoCuenta) AND (SecuenTran = @SecuenTran)">
        <DeleteParameters>
            <asp:Parameter Name="TipoDiario" />
            <asp:Parameter Name="NumDiario" />
            <asp:Parameter Name="CodigoCuenta" />
            <asp:Parameter Name="SecuenTran" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_IdenBeneficiarios" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT ApellidosNombresU AS AN , IdenBenef FROM AContabilidad.APContabBenef ORDER BY ApellidosNombresU"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_Cheques" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoCuenta, NumDocTran, ValorHaber FROM AContabilidad.APContabTransacciones
WHERE CodigoCuenta = @CuentaPagadora AND NumDocTran=@NumCheque" DeleteCommand="DELETE FROM AContabilidad.APContabTransacciones WHERE (TipoDiario = @TipoDiario) AND (NumDiario = @NumDiario) AND (CodigoCuenta = @CodigoCuenta) AND (SecuenTran = @SecuenTran)">
        <DeleteParameters>
            <asp:Parameter Name="TipoDiario" />
            <asp:Parameter Name="NumDiario" />
            <asp:Parameter Name="CodigoCuenta" />
            <asp:Parameter Name="SecuenTran" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="tbx_CuentaPagadora" Name="CuentaPagadora" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbx_NumDoc1" Name="NumCheque" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    &nbsp;<asp:SqlDataSource ID="ds_ActivosFijos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoActivo], [Categoría], [Grupo], [CodigoIngreso], [Departamento], [Ubicacion], [NombreDelActivo], [Unidades], [Medida], [Marca], [Modelo], [NúmDeModelo], [NúmDeSerie], [FechaIngreso], [FechaDeBaja], [VidaUtil], [TasaDepreciacionAnual], [FechaUltimaDepreciacion], [VidaUtilRev], [TasaDeprecRevAnual], [FechaUltimaRevalorizacion], [FechaUltimaDepreciacionRev], [VidaUtilMejoras], [TasaDeprecMejAnual], [FechaUltimaDeprecMejoras], [MétodoDeDepreciación], [ValorInicial], [RevalorizacionAcumulada], [DepreciacionAcumulada], [MejorasAcumuladas], [DepreciacionAceleradaAcumulada], [DepreciacionEspecialAcumulada], [DepreciacionMejoras], [DepreciacionRevAcumulada], [ValorActual], [ValorResidual], [DepreciacionMensual], [DepreciacionRevMensual], [DepreciacionAnual], [DepreciacionRevAnual], [RevalorizacionMensual], [RevalorizacionAnual], [Comentarios], [Descripción] FROM AFijos.[APActivosFijos]">
                 </asp:SqlDataSource>
                             <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Ubicacion] FROM [APActivosFijos]"></asp:SqlDataSource>
                             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" ProviderName="<%$ ConnectionStrings:SoftempBDFPD.ProviderName %>" SelectCommand="SELECT [Departamento] FROM [APActivosFijos]" ></asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_CatActivos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Descripcion], [Categoria] FROM AFijos.[APActivosFijosTCategoria]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="ds_GrupoActivos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Descripcion, CodGrupoActivo FROM AFijos.APActivosFijosTGrupos WHERE (TipoCodigo = 'D')"></asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_CodTranActivos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodTranActivo], [Descripcion] FROM AFijos.[APActivosFijosTCodTrans]"></asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_MetodoDeprecia" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [DescripcionDeprecia], [CodTipoDeprecia] FROM AFijos.[APActivosFijosTTipoDeprecia]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="ds_Habitaciones" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoProducto], [NombreProducto], [CodCentroCosto] FROM AHoteleria.[APHotelCatalogoHospedaje]
WHERE ([CodCentroCosto] = @CodCentroCosto)">
             <SelectParameters>
                 <asp:ControlParameter ControlID="tbx_CentroCostos" Name="CodCentroCosto" PropertyName="Text" />
             </SelectParameters>
         </asp:SqlDataSource>
             <br />
    &nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <br />

        <br />

<script type="text/javascript">
           function cerrar() {
               window.open("", "_parent", "");
               var ventana = window.self;
               ventana.opener = window.self;
               ventana.close();
           }
</script>

    </asp:Content>


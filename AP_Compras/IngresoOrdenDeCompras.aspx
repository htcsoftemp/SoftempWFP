<%@ Page Title="Ingreso Orden de Compra: " Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="IngresoOrdenDeCompras.aspx.cs" Inherits="IngresoOrdenDeCompras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .active {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
         <asp:TextBox ID="MensajeR" runat="server" Width="658px" BackColor="Yellow" Font-Size="X-Small" ForeColor="Red" TextMode="MultiLine" Height="52px"></asp:TextBox>
         <input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
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
        <asp:TextBox ID="tbx_TipoIdContrib" runat="server" Width="23px" AutoPostBack="True" BackColor="#CCFF99" Enabled="False" TabIndex="65"></asp:TextBox>
        <asp:TextBox ID="tbx_IdContrib" runat="server" Width="109px" BackColor="#CCFF99" ForeColor="Black" TabIndex="67" ToolTip="Ingrese la Identificacion del Beneficiario relacionado con el Diario" OnTextChanged="tbx_IdBenef_TextChanged"></asp:TextBox>
         <asp:TextBox ID="tbx_TipoContrib" runat="server" Width="23px" BackColor="#CCFF99" ForeColor="Black" TabIndex="69" ToolTip="Ingrese el N. de Telefono celular del Beneficiario" Enabled="False" Visible="False"></asp:TextBox>
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
        &nbsp;&nbsp;&nbsp; <asp:ImageButton ID="btn_RegistraBenef" runat="server" Height="18px" ImageUrl="~/Iconos/Diskete.ICO" Width="16px" TabIndex="98" ToolTip="Click para validar y Grabar datos de los Beneficiarios de Operaciones " OnClick="btn_RegistraBenef_Click" Visible="False" />
        &nbsp;&nbsp; <asp:ImageButton ID="btn_ModificaDatosBenef" runat="server" Height="18px" ImageUrl="~/Iconos/Editar.gif" Width="16px" TabIndex="99" ToolTip="Click para actualizar Información de Proveedor" OnClick="btn_ModificaDatosBenef_Click" Visible="False" />
         &nbsp;&nbsp;
         <br />
    <asp:Label ID="EDocFuente" runat="server" BorderColor="Black" CssClass="active" Text="Doc. Fuente " BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="76px" Font-Size="Small" Height="16px"></asp:Label>
        <asp:DropDownList ID="ddl_TipoDocFuente" runat="server" Width="131px" DataSourceID="ds_TipoFuentes" DataTextField="DetalleDocumento" DataValueField="CodigoDocumento" TabIndex="100" Height="16px" ToolTip="Escoja el Tipo de Documento Fuente" Visible="False" AutoPostBack="True" Font-Size="X-Small" OnSelectedIndexChanged="ddl_TipoDocFuente_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="tbx_CodFuente" runat="server" Width="20px" BackColor="White" ForeColor="Black" TabIndex="102" Visible="False" Font-Size="X-Small">01</asp:TextBox>
        <asp:TextBox ID="tbx_DocFuente" runat="server" Width="74px" BackColor="White" ForeColor="Black" TabIndex="104" Visible="False" Font-Size="X-Small">Factura</asp:TextBox>
        <asp:TextBox ID="tbx_NumSucF" runat="server" Width="26px" BackColor="#FFFF99" ForeColor="Black" TabIndex="106" Visible="False">001</asp:TextBox>
        <asp:TextBox ID="tbx_NumCajaF" runat="server" Width="26px" BackColor="#FFFF99" ForeColor="Black" TabIndex="108" Visible="False">001</asp:TextBox>
        <asp:TextBox ID="tbx_NumDocFuente" runat="server" Width="85px" BackColor="#FFFF66" ForeColor="Black" TabIndex="110" Visible="False" style="text-align: right" AutoPostBack="True" OnTextChanged="tbx_NumDocFuente_TextChanged" ></asp:TextBox>
        <asp:Label ID="EFechaComprob" runat="server" BorderColor="Black" CssClass="active" Text="Fecha: " BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Width="45px" Visible="False"></asp:Label>
         <asp:TextBox ID="tbx_FechaComprob" runat="server" Width="87px" BackColor="#FFFF66" ForeColor="Black" TextMode="Date" TabIndex="520" AutoPostBack="True" Visible="False"></asp:TextBox>
         <br />
    <asp:Label ID="EAutorizacionF" runat="server" BorderColor="Black" CssClass="active" Text="Autorización" Visible="False" TabIndex="900"></asp:Label>
        &nbsp;<asp:ImageButton ID="btn_CopiaAutorizacion" runat="server" Height="16px" ImageUrl="~/Iconos/copy1.gif" Width="17px" TabIndex="113" ToolTip="Click para copiar Autorizacion almacenada" Visible="False" OnClick="btn_CopiaAutorizacion_Click"  />
        &nbsp;<asp:TextBox ID="tbx_NumAutorFuente" runat="server" Width="294px" BackColor="#FFFF66" ForeColor="Black" Height="16px" TabIndex="115" ToolTip="Ingrese el codigo de Autorizacion de los documentos relacionados con el Diario" Font-Size="X-Small" Visible="False"></asp:TextBox>
         <asp:TextBox ID="tbx_FCaducaFuente" runat="server" Width="87px" BackColor="#FFFF66" ForeColor="Black" TabIndex="117" AutoPostBack="True" Visible="False" OnTextChanged="tbx_FCaducaFuente_TextChanged"></asp:TextBox>
         <br />
    <asp:Label ID="EMonto14" runat="server" BorderColor="Black" CssClass="active" Text="V. con IVA 14%" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="100px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Monto14" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="120" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto14_TextChanged">0</asp:TextBox>
        <asp:TextBox ID="tbx_IVA14" runat="server" Width="60px" BackColor="White" ForeColor="Black" TabIndex="125" Visible="False" Enabled="False" style="text-align: right">0</asp:TextBox>
    <asp:Label ID="EMonto12" runat="server" BorderColor="Black" CssClass="active" Text="V. con IVA 12%" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="101px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Monto12" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="130" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto12_TextChanged">0</asp:TextBox>
        <asp:TextBox ID="tbx_IVA12" runat="server" Width="60px" BackColor="White" ForeColor="Black" TabIndex="135" Visible="False" style="text-align: right" Enabled="False">0</asp:TextBox>
    <asp:Label ID="EMonto0" runat="server" BorderColor="Black" CssClass="active" Text="V. con IVA 0%" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="90px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Monto0" runat="server" Width="80px" BackColor="#FFFF66" ForeColor="Black" TabIndex="140" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto0_TextChanged">0</asp:TextBox>
    <asp:Label ID="EICE" runat="server" BorderColor="Black" CssClass="active" Text="ICE" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="30px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_ICE" runat="server" Width="58px" BackColor="#FFFF66" ForeColor="Black" TabIndex="142" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto0_TextChanged">0</asp:TextBox>
    <asp:Label ID="ESubtotal" runat="server" BorderColor="Black" CssClass="active" Text="Subtotal" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="52px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Subtotal" runat="server" Width="66px" BackColor="#FFFF66" ForeColor="Black" TabIndex="145" Visible="False" style="text-align: right" OnTextChanged="tbx_Monto0_TextChanged" Enabled="False">0</asp:TextBox>
         <asp:CheckBox ID="cbx_Bienes" runat="server" Visible="False" Text="Bienes" TabIndex="144" />
         <asp:CheckBox ID="cbx_Servicios" runat="server" Visible="False" Text="Servicios" TabIndex="146" />
        <asp:ImageButton ID="btn_ProcesaFuente" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" Width="22px" TabIndex="150" ToolTip="Click para Validar Fuente" Visible="False" OnClick="btn_ProcesaFuente_Click" />
         <br />
    <asp:Label ID="EMontoP" runat="server" BorderColor="Black" CssClass="active" Text="Sub. sin IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="78px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_TotalParcial" runat="server" Width="70px" BackColor="White" ForeColor="Black" TabIndex="150" Visible="False" Enabled="False"  style="text-align: right">0</asp:TextBox>
    <asp:Label ID="EMontoIVA" runat="server" BorderColor="Black" CssClass="active" Text="IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="29px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_TotalIVA" runat="server" Width="60px" BackColor="White" ForeColor="Black" TabIndex="155" Visible="False" Enabled="False" style="text-align: right">0</asp:TextBox>
    <asp:Label ID="ETotalFactura" runat="server" BorderColor="Black" CssClass="active" Text="Total Factura" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="83px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_TotalFactura" runat="server" Width="70px" BackColor="#FF9900" ForeColor="Black" TabIndex="160" Visible="False" Enabled="False" style="text-align: right">0</asp:TextBox>
         <br />
        <asp:Label ID="EDetalle" runat="server" BorderColor="Black" CssClass="active" Text="Detalle" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_DetalleComprob" runat="server" Width="628px" BackColor="#FFFF66" ForeColor="Black" TextMode="MultiLine" TabIndex="510" ToolTip="Ingrese el detalle de la Razon del Comprobante" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Visible="False" Height="37px" OnTextChanged="tbx_DetalleComprob_TextChanged"></asp:TextBox>
    <asp:Label ID="ENumero" runat="server" BorderColor="Black" CssClass="active" Text="Número" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False"></asp:Label>
        <asp:TextBox ID="tbx_NumDiario" runat="server" Width="87px" BackColor="#FFFF66" ForeColor="Black" TextMode="Number" TabIndex="530" Visible="False"></asp:TextBox>
         <asp:ImageButton ID="btn_GrabarOrden" runat="server" Height="20px" ImageUrl="~/Iconos/Grabar.gif" Width="37px" TabIndex="540" ToolTip="Click para Grabar Cabecera de Orden de Compra"  BorderColor="Red" BorderStyle="None" Visible="False" OnClick="btn_GrabarOrden_Click" />
         <br />
         <br />
    <asp:Label ID="ENLineaOrden" runat="server" BorderColor="Black" CssClass="active" Text="Linea N°" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="56px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_NLineaOrden" runat="server" Width="27px" BackColor="White" ForeColor="Black" TabIndex="150" Visible="False" Enabled="False"  style="text-align: right">0</asp:TextBox>
    <asp:Label ID="ECodProducto" runat="server" BorderColor="Black" CssClass="active" Text="Producto" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="59px" Font-Size="Small" Height="16px"></asp:Label>
        <asp:DropDownList ID="ddl_CodProducto" runat="server" Width="106px" DataSourceID="ds_TipoFuentes" DataTextField="DetalleDocumento" DataValueField="CodigoDocumento" TabIndex="100" Height="16px" ToolTip="Escoja el Tipo de Documento Fuente" Visible="False" AutoPostBack="True" Font-Size="X-Small" OnSelectedIndexChanged="ddl_TipoDocFuente_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="tbx_CodProducto" runat="server" Width="74px" BackColor="White" ForeColor="Black" TabIndex="104" Visible="False" Font-Size="X-Small"></asp:TextBox>
        <asp:TextBox ID="tbx_NombreProducto" runat="server" Width="316px" BackColor="#FFFF66" ForeColor="Black" TextMode="MultiLine" TabIndex="510" ToolTip="Ingrese el detalle de la Razon del Comprobante" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Visible="False" Height="16px" OnTextChanged="tbx_DetalleComprob_TextChanged"></asp:TextBox>
    <asp:Label ID="EUnidadM" runat="server" BorderColor="Black" CssClass="active" Text="U. Medida" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="59px" Font-Size="Small" Height="16px"></asp:Label>
        <asp:TextBox ID="tbx_UnidadM" runat="server" Width="45px" BackColor="White" ForeColor="Black" TabIndex="104" Visible="False" Font-Size="X-Small" OnTextChanged="tbxCodProducto0_TextChanged"></asp:TextBox>
         <br />
    <asp:Label ID="ECantidad" runat="server" BorderColor="Black" CssClass="active" Text="Cantidad" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="56px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Cantidad" runat="server" Width="45px" BackColor="White" ForeColor="Black" TabIndex="150" Visible="False" Enabled="False"  style="text-align: right">0</asp:TextBox>
    <asp:Label ID="ECostoU" runat="server" BorderColor="Black" CssClass="active" Text="Costo Unitario sin IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="134px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Cantidad0" runat="server" Width="74px" BackColor="White" ForeColor="Black" TabIndex="150" Visible="False" Enabled="False"  style="text-align: right" OnTextChanged="tbx_Cantidad0_TextChanged">0</asp:TextBox>
    <asp:Label ID="EParcial" runat="server" BorderColor="Black" CssClass="active" Text="Parcial sin IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="96px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_Parcial" runat="server" Width="74px" BackColor="White" ForeColor="Black" TabIndex="150" Visible="False" Enabled="False"  style="text-align: right" OnTextChanged="tbx_Cantidad0_TextChanged">0</asp:TextBox>
    <asp:Label ID="EIVAParcial" runat="server" BorderColor="Black" CssClass="active" Text="IVA" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#669900" Visible="False" Width="33px" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="tbx_IVAParcial" runat="server" Width="74px" BackColor="White" ForeColor="Black" TabIndex="150" Visible="False" Enabled="False"  style="text-align: right" OnTextChanged="tbx_Cantidad0_TextChanged">0</asp:TextBox>
         &nbsp;<asp:ImageButton ID="btn_GrabarLineaOrden" runat="server" Height="17px" ImageUrl="~/Images/Guardar.png" Width="24px" TabIndex="540" ToolTip="Click para Grabar los Datos Ingresados del Comprobante de Diario" BorderColor="Red" BorderStyle="None" Visible="False" OnClick="btn_GrabarLineaOrden_Click" />
         &nbsp;<asp:ImageButton ID="btn_IngresaTransacciones" runat="server" Height="20px" ImageUrl="~/Iconos/AddFinances.gif" TabIndex="550" ToolTip="Click para Ingresar Nueva Transacción" Width="33px" Visible="False" OnClick="btn_IngresaTransacciones_Click" />
         <br />
         <br />
         <br />
         <asp:ImageButton ID="btn_NuevoComprobante" runat="server" Height="30px" ImageUrl="~/Iconos/AddRegistro.gif" TabIndex="900" ToolTip="Click para Ingresar Nuevo Comprobante" Width="30px" OnClick="btn_NuevoComprobante_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="btn_BuscaOtroDiario" runat="server" Height="26px" ImageUrl="~/Iconos/Lupa.gif" Width="29px" TabIndex="800" OnClick="btn_BuscaOtroDiario_Click" ToolTip="Click para Buscar Comprobante" Visible="False"  />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="btn_VistaPreComprobante" runat="server" Height="30px" ImageUrl="~/Iconos/VistaPrevia.gif" TabIndex="7" ToolTip="Click para Vista previa de Comprobante" Width="30px" OnClick="btn_VistaPreComprobante_Click" Visible="False" />
         &nbsp;&nbsp; <asp:ImageButton ID="btn_ImprimeComprobante" runat="server" Height="30px" ImageUrl="~/Iconos/Impresora.gif" TabIndex="9" ToolTip="Click para Imprimir Comprobante" Width="30px" OnClick="btn_ImprimeComprobante_Click" Visible="False" />
        &nbsp;&nbsp;&nbsp;
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
    <asp:SqlDataSource ID="ds_IdenBeneficiarios" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT ApellidosNombresU AS AN , IdenBenef FROM AContabilidad.APContabBenef ORDER BY ApellidosNombresU"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_LineasOrdenCompra" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NumLineaOrdenCompra, CodigoProducto, NombreProducto, UnidadMedida, Cantidad, CostoUnitarioProm, ValorTotal, ValorIVA FROM ACompras.APComprasOrdenLineas " DeleteCommand="DELETE FROM AContabilidad.APContabTransacciones WHERE (TipoDiario = @TipoDiario) AND (NumDiario = @NumDiario) AND (CodigoCuenta = @CodigoCuenta) AND (SecuenTran = @SecuenTran)">
        <DeleteParameters>
            <asp:Parameter Name="TipoDiario" />
            <asp:Parameter Name="NumDiario" />
            <asp:Parameter Name="CodigoCuenta" />
            <asp:Parameter Name="SecuenTran" />
        </DeleteParameters>
    </asp:SqlDataSource>
    &nbsp;<br />
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


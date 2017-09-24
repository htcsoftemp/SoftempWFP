<%@ Page Title="Información Activos" Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="DatosGenActivos.aspx.cs" Inherits="DatosActivosFijos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .active {
            text-align: left;
            margin-left: 0px;
            margin-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
         <asp:TextBox ID="MensajeR" runat="server" Width="925px" BackColor="Yellow" Font-Size="X-Small" ForeColor="Red" TextMode="MultiLine" Height="58px"></asp:TextBox>
         <br />
         <asp:MultiView ID="mv_IngresoActivosFijos" runat="server" ActiveViewIndex="1">
             <asp:View ID="v_CuadroXCentroCostos" runat="server">
                 <asp:Button ID="btn_DatosIndividualesC" runat="server" CommandArgument="v_DatosActivoF" CommandName="SwitchViewByID" OnClick="btn_DatosIndividualesC_Click" Text="Datos Individuales de Activos" Width="202px" />
                 <asp:Button ID="btn_CuadroGenActivosC" runat="server" CommandArgument="v_CuadroGenActivos" CommandName="SwitchViewByID" Text="Cuadro General de Activos" Width="174px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="CLOSE2" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                 <asp:Panel ID="pnl_Busqueda0" runat="server" Width="720px" Wrap="False">
                     <asp:Label ID="ECuadroAFXCC" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Text="Cuadro de Activos x Centro de Costos" Width="289px"></asp:Label>
                     <asp:DropDownList ID="ddl_CodCentroCostosParaCuadro" runat="server" AutoPostBack="True" DataSourceID="ds_CentroCostos" DataTextField="DescripcionCentroCosto" DataValueField="CodCentroCosto" Font-Size="X-Small" Height="24px" OnSelectedIndexChanged="ddl_CodCentroCostosParaCuadro_SelectedIndexChanged" TabIndex="20" ToolTip="Escoja Centro de Costos que afecta el activo" Width="309px">
                     </asp:DropDownList>
                     <asp:TextBox ID="tbx_CodCentroCostosParaCuadro" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" style="font-weight: 700" TabIndex="30" ToolTip="Centro de Costos" Width="24px">000</asp:TextBox>
                 </asp:Panel>
                 <asp:Panel ID="pnl_CuadroGeneralActivos0" runat="server" Height="250px" ScrollBars="Vertical" Width="981px">
                     <asp:GridView ID="gv_CuadroXCentroCostos" runat="server" AutoGenerateColumns="False" BackColor="#FFCC99" DataSourceID="ds_ActivosXCentro" Height="115px" OnSelectedIndexChanged="gv_CuadroXCentroCostos_SelectedIndexChanged" style="margin-top: 2px; margin-right: 2px;" Width="900px">
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
                             <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion">
                             <ItemStyle Font-Size="XX-Small" Width="100px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                             <asp:BoundField DataField="NumDeSerie" HeaderText="Serie" SortExpression="NumDeSerie" />
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
                 </asp:Panel>
                 <br />
             </asp:View>
            <asp:View ID="v_DatosActivoF" runat="server">
                 <asp:Button ID="btn_CuadroGenActivosI" runat="server" 
                      CommandArgument="v_CuadroGenActivos" 
                      CommandName="SwitchViewByID"
                      Text="Cuadro General de Activos" Width="183px" />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_CuadroXCentrosCostoI" runat="server" CommandArgument="v_CuadroXCentroCostos" CommandName="SwitchViewByID" Text="Cuadro Activos X Centro de Costos" Width="236px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> 
                 <asp:Label ID="EDatosIndivAF" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Text="Datos Individuales de Activos" Width="238px"></asp:Label>
                 <asp:Panel ID="pnl_Busqueda" runat="server" Visible="False" Width="900px" Wrap="False">
                     &nbsp;<asp:Label ID="EBuscaActivo" runat="server" BackColor="#33CC33" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Buscar Activo" Width="87px" TabIndex="10"></asp:Label>
                     <asp:DropDownList ID="ddl_CentroCostosL" runat="server" AutoPostBack="True" DataSourceID="ds_CentroCostos" DataTextField="DescripcionCentroCosto" DataValueField="CodCentroCosto" Font-Size="X-Small" Height="19px" OnSelectedIndexChanged="ddl_CentroCostosL_SelectedIndexChanged" TabIndex="20" ToolTip="Escoja Centro de Costos que afecta el activo" Width="150px">
                     </asp:DropDownList>
                     <asp:TextBox ID="tbx_CentroCostosL" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" style="font-weight: 700" TabIndex="30" ToolTip="Centro de Costos" Width="24px">000</asp:TextBox>
                     <asp:DropDownList ID="ddl_CodigoActivoL" runat="server" AutoPostBack="True" DataSourceID="ds_CodigoActivos" DataTextField="NombreActivo" DataValueField="CodigoActivo" Font-Size="X-Small" Height="16px" OnSelectedIndexChanged="ddl_CodigoActivoL_SelectedIndexChanged" TabIndex="40" ToolTip="Escoja Centro de Costos que afecta el activo" Width="230px">
                     </asp:DropDownList>
                     <asp:TextBox ID="tbx_CodigoActivoL" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="50" ToolTip="Ingrese el Codigo del Activo" Width="126px"></asp:TextBox>
                     <asp:ImageButton ID="btn_BuscarActivoIndiv" runat="server" Height="20px" ImageUrl="~/Iconos/Binoculares.gif" OnClick="btn_BuscarActivoIndiv_Click" TabIndex="60" ToolTip="Click para Buscar Comprobante" Width="20px" />
                 </asp:Panel>
                 <asp:Panel ID="pnl_DatosBasicosActivo" runat="server" Height="135px" Width="900px" Visible="False">
                                 <asp:Label ID="ECategoria" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Categoria" Width="63px"></asp:Label>
                                 <asp:DropDownList ID="ddl_Categoria" runat="server" AutoPostBack="True" DataSourceID="ds_CatActivos" DataTextField="Descripcion" DataValueField="Categoria" Font-Size="X-Small" Height="25px" TabIndex="505" ToolTip="Escoja Categoria del Activo" Width="72px" OnSelectedIndexChanged="ddl_Categoria_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_Categoria" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" TabIndex="510" ToolTip="Codigo de Categoria" Width="18px" style="font-weight: 700">10</asp:TextBox>
                                 <asp:Label ID="ECentroCostos" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="18px" Text="Centro Costos" Width="90px"></asp:Label>
                                 <asp:DropDownList ID="ddl_CentroCostos" runat="server" AutoPostBack="True" DataSourceID="ds_CentroCostos" DataTextField="DescripcionCentroCosto" DataValueField="CodCentroCosto" Font-Size="X-Small" Height="19px" TabIndex="515" ToolTip="Escoja Centro de Costos que afecta el activo" Width="70px" OnSelectedIndexChanged="ddl_CentroCostos_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_CentroCostos" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" TabIndex="520" ToolTip="Centro de Costos" Width="24px" style="font-weight: 700">000</asp:TextBox>
                                 <asp:Label ID="EGrupoActivos" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Grupo" Width="50px"></asp:Label>
                                 <asp:DropDownList ID="ddl_GrupoActivos" runat="server" AutoPostBack="True" DataSourceID="ds_GrupoActivos" DataTextField="Descripcion" DataValueField="CodGrupoActivo" Font-Size="X-Small" Height="25px" TabIndex="525" ToolTip="Escoja el Grupo al que pertenece el activo" Width="74px" OnSelectedIndexChanged="ddl_GrupoActivos_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_GrupoActivos" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black"  TabIndex="530" ToolTip="Codigo de Grupo de Activos" Width="43px" style="font-weight: 700"></asp:TextBox>
                                 <asp:Label ID="ECodigoActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Codigo Activo" Width="85px"></asp:Label>
                                 <asp:ImageButton ID="btn_GeneraCodigo" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" TabIndex="550" ToolTip="Click para Generar un Codigo para el Activo" Visible="False" Width="22px" OnClick="btn_GeneraCodigo_Click" />
                                 <asp:TextBox ID="tbx_CodigoActivo" runat="server" BackColor="#FFFF66" ForeColor="Black"  TabIndex="540" ToolTip="Ingrese el Codigo del Activo" Width="126px"></asp:TextBox>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:ImageButton ID="btn_HabilitaModifDatosActivo" runat="server" Height="22px" ImageUrl="~/Iconos/Edit1.png" ndex="30" ToolTip="Click para habilitar Modificar Datos del Proveedor" Visible="False" Width="26px" />
                                 <br />
                                 <asp:Label ID="ENombreActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Nombre Activo" Width="90px"></asp:Label>
                                 <asp:TextBox ID="tbx_NombreActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="560" ToolTip="Ingrese un Nombre corto del Activo" Width="300px"></asp:TextBox>
                                 <asp:Label ID="EDescripcionActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Descripcion" Width="90px"></asp:Label>
                                 <asp:TextBox ID="tbx_DescripcionActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="580" ToolTip="Ingrese una descripción detallada del activo" Width="359px"></asp:TextBox>
                                 &nbsp;<br />
                                 <asp:Label ID="EMarcaActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Marca" Width="50px"></asp:Label>
                                 <asp:TextBox ID="tbx_MarcaActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="570" ToolTip="Ingrese la Marca del Activo" Width="102px"></asp:TextBox>
                                 <asp:Label ID="ESerieActivo" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Serie" Width="34px"></asp:Label>
                                 <asp:TextBox ID="tbx_SerieActivo" runat="server" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_SerieActivo_TextChanged" TabIndex="590" ToolTip="número de serie del Activo" Width="115px"></asp:TextBox>
                                 <asp:Label ID="EUbicacion" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Ubicación" Width="70px"></asp:Label>
                                 <asp:DropDownList ID="ddl_UbicaHabita" runat="server" AutoPostBack="True" DataSourceID="ds_Habitaciones" DataTextField="NombreProducto" DataValueField="CodigoProducto" Font-Size="X-Small" Height="25px" OnSelectedIndexChanged="ddl_UbicaHabita_SelectedIndexChanged" TabIndex="660" ToolTip="Escoja la Habitación en el que esta ubicado el activo" Width="74px">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_NumHabitacion" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" TabIndex="670" ToolTip="Codigo de la habitacion en donde esta ubicado el activo" Width="27px"></asp:TextBox>
                                 <asp:TextBox ID="tbx_Ubicacion" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="680" ToolTip="Ingrese el detalle de la ubicacion del activo" Width="120px"></asp:TextBox>
                                 <asp:Label ID="EResponsable" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Responsable" Width="70px"></asp:Label>
                                 <asp:TextBox ID="tbx_Responsable" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="690" ToolTip="Ingrese Responsable de custodia del activo" Width="149px"></asp:TextBox>
                                 <br />
                                 <asp:Label ID="EFechaIngreso" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="16px" Text="Fecha Ingreso" Width="82px"></asp:Label>
                                 <asp:TextBox ID="tbx_FechaIngreso" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" TabIndex="600" ToolTip="Fecha de Adquisicion del Activo" Width="90px" OnTextChanged="tbx_FechaIngreso_TextChanged"></asp:TextBox>
                                 <asp:Label ID="ERazonIngreso" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Cod.Ingreso" Width="72px"></asp:Label>
                                 <asp:DropDownList ID="ddl_RazonIngreso" runat="server" AutoPostBack="True" DataSourceID="ds_CodTranActivos" DataTextField="Descripcion" DataValueField="CodTranActivo" Font-Size="X-Small" Height="19px" OnSelectedIndexChanged="ddl_RazonIngreso_SelectedIndexChanged" TabIndex="610" ToolTip="Escoja la Razon del Ingreso" Width="94px">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_RazonIngreso" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" TabIndex="620" ToolTip="Codigo de Razon del Ingreso" Width="40px"></asp:TextBox>
                                 <asp:Label ID="ENDoc" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Doc.N°" Width="44px"></asp:Label>
                                 <asp:TextBox ID="tbx_NumDocumento" runat="server" BackColor="#FFFF66" ForeColor="Black" style="text-align: right" TabIndex="630" ToolTip="Ingrese # de Documento de Ingreso" Width="57px">0</asp:TextBox>
                                 <asp:Label ID="EVidaUtil" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Vida Util " Width="54px"></asp:Label>
                                 <asp:TextBox ID="tbx_VidaUtil" runat="server" BackColor="#FFFF66" ForeColor="Black" style="text-align: right" TabIndex="630" ToolTip="Ingrese # de meses de Vida util del activo" Width="48px">0</asp:TextBox>
                                 <asp:Label ID="EMetodoDeprecia" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="19px" Text="Metod.Deprec." Width="90px"></asp:Label>
                                 <asp:DropDownList ID="ddl_MetodoDeprecia" runat="server" AutoPostBack="True" DataSourceID="ds_MetodoDeprecia" DataTextField="DD" DataValueField="CodTipoDeprecia" Font-Size="X-Small" Height="19px" TabIndex="640" ToolTip="Escoja el Metodo de Depreciacion" Width="94px">
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbx_MetodoDeprecia" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" style="font-weight: 700" TabIndex="650" ToolTip="Codigo del Metodo de depreciacion" Width="20px">10</asp:TextBox>
                                 <br />
                                 <asp:Label ID="EValorInicial" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Valor Inicial" Width="81px"></asp:Label>
                                 <asp:TextBox ID="tbx_ValorInicial" runat="server" BackColor="#99FF66" ForeColor="Black" style="text-align: right" TabIndex="700" ToolTip="Ingrese el Valor Inicial del Activo" Width="79px">0</asp:TextBox>
                                 &nbsp;<asp:Label ID="EDepreciaAcumul" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Dep.Norm.Acum." Width="99px"></asp:Label>
                                 <asp:TextBox ID="tbx_DepreciaNormAcum" runat="server" BackColor="#99FF66" ForeColor="Black" style="text-align: right" TabIndex="740" ToolTip="Valor acumulado de la depreciación Normal" Width="88px">0</asp:TextBox>
                                 <asp:Label ID="EFUltimaDeprecia" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="16px" Text="Fech.Ult.Deprec.N" Width="108px"></asp:Label>
                                 <asp:TextBox ID="tbx_FUltimaDeprecia" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" TabIndex="710" ToolTip="Fecha de Ultima depreciación" Width="90px"></asp:TextBox>
                                 <asp:Label ID="EValorResidual" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Height="18px" Text="Valor Residual" Width="90px"></asp:Label>
                                 <asp:TextBox ID="tbx_PorcenValResid" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" OnTextChanged="tbx_PorcenValResid_TextChanged" style="text-align: right" TabIndex="720" ToolTip="Ingrese el porcentaje del valor residual del activo" Width="42px">0</asp:TextBox>
                                 <asp:TextBox ID="tbx_ValorResidual" runat="server" BackColor="#FFFF66" ForeColor="Black" style="text-align: right" TabIndex="730" ToolTip="Ingrese el Valor Residual del Activo" Width="90px">0</asp:TextBox>
                                 &nbsp;
                                 <asp:ImageButton ID="btn_VerificaDatosIngres" runat="server" Height="19px" ImageUrl="~/Iconos/Stop sign.png" OnClick="btn_VerificaDatosIngres_Click" TabIndex="890" ToolTip="Click para Validar Datos adicionales" Width="22px" />
&nbsp;<asp:ImageButton ID="btn_HabilitaIngresoComplementos" runat="server" Height="16px" ImageUrl="~/Iconos/AddForm.gif" OnClick="btn_HabilitaIngresoComplementos_Click" TabIndex="770" ToolTip="Click para ingresar Informacion adiconal" Visible="False" Width="17px" />
                                 &nbsp;&nbsp;&nbsp;
                                 <br />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <br />
                             </asp:Panel>
                <asp:Panel ID="pnl_DatosActivosAnter" runat="server" Height="81px" Width="900px" Visible="False">
                                     <asp:Label ID="EFechaUltimReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Fecha Ult. Revaloriz." Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimReval" runat="server" BackColor="#FFFF66" ForeColor="Black"  TabIndex="810" ToolTip="Fecha de ultima Revalorización" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValorReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Valor Revaloriz." Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorReval" runat="server" BackColor="#99FF66" ForeColor="Black"  TabIndex="820" ToolTip="Valor de revalorización" Width="80px" style="text-align: right">0</asp:TextBox>
                                     <asp:Label ID="EFUltimaDepReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="F. Ult. Deprec. Reval." Width="150px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimaDepReval" runat="server" BackColor="#FFFF66" ForeColor="Black"  TabIndex="830" ToolTip="Fecha de ultima depreciación de la rfevalorización" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValorDepReval" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="V. Deprec. Reval." Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorDepreReval" runat="server" BackColor="#99FF66" ForeColor="Black"  TabIndex="840" ToolTip="Valor de depreciación de la Revalorización Acumulada" Width="80px" style="text-align: right">0</asp:TextBox>
                                     <br />
                                     <asp:Label ID="EFUltimaMejora" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Fecha Ult. Mejora" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimaMejora" runat="server" BackColor="#FFFF66" ForeColor="Black"  TabIndex="840" ToolTip="Fecha de ultima Mejora" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValorMejoras" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Valor Mejoras" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorMejoras" runat="server" BackColor="#99FF66" ForeColor="Black"  TabIndex="850" ToolTip="Valor Acumulado de las mejoras" Width="80px" style="text-align: right">0</asp:TextBox>
                                     <asp:Label ID="EFUltimDepMejoras" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="F. Ult. Deprec. Mejor." Width="150px"></asp:Label>
                                     <asp:TextBox ID="tbx_FUltimDepMejoras" runat="server" BackColor="#FFFF66" ForeColor="Black"  TabIndex="860" ToolTip="Fecha de la ultima depreciación de las Mejoras" Width="80px"></asp:TextBox>
                                     <asp:Label ID="EValDepMejoras" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="V. Deprec. Mejor." Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorDepMejoras" runat="server" BackColor="#99FF66" ForeColor="Black"  TabIndex="870" ToolTip="Valor de depreciación de las Mejoras acumulado" Width="80px" style="text-align: right" >0</asp:TextBox>
                                     <br />
                                     <asp:Label ID="EValorDepAcelerada" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="V. Depr.Acelerada" Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorDepAcelerada" runat="server" BackColor="#99FF66" ForeColor="Black" style="text-align: right" TabIndex="750" ToolTip="Valor de depreciación Acelerada" Visible="False" Width="81px">0</asp:TextBox>
                                     <asp:Label ID="EValDepEspecial" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="V.Deprec.Especial" Visible="False" Width="120px"></asp:Label>
                                     <asp:TextBox ID="tbx_ValorDepEspecial" runat="server" BackColor="#99FF66" ForeColor="Black" style="text-align: right" TabIndex="760" ToolTip="Valor de depreciacion Especial" Visible="False" Width="79px">0</asp:TextBox>
                                     <asp:ImageButton ID="btn_VerificaDatosAd" runat="server" Height="19px" ImageUrl="~/Iconos/Verificacion.gif" TabIndex="890" ToolTip="Click para Validar Datos adicionales" Width="22px" OnClick="btn_VerificaDatosAd_Click" />
                                     <br />
                                     <br />
                                     <br />
                             </asp:Panel>
                 <asp:Panel ID="pnl_GrabaActivo" runat="server" Height="27px" Visible="False" Width="900px">
                     <asp:Label ID="EValorActual" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Valor Actual" Width="94px"></asp:Label>
                     <asp:TextBox ID="tbx_ValorActual" runat="server" BackColor="#66FF33" ForeColor="Black" style="text-align: right" TabIndex="790" ToolTip="Valor Actual" Width="90px">0</asp:TextBox>
                     &nbsp;&nbsp;&nbsp;
                     <asp:ImageButton ID="btn_GrabarActivo" runat="server" BorderColor="Red" BorderStyle="None" Height="24px" ImageUrl="~/Iconos/Grabar.gif" OnClick="btn_GrabarActivo_Click" TabIndex="150" ToolTip="Click para Grabar los Datos Datos de Nuevo Activo" Visible="False" Width="31px" />
                     &nbsp;&nbsp;
                     <asp:ImageButton ID="btn_GeneraHistorico" runat="server" Height="19px" ImageUrl="~/Iconos/Proceso.png" OnClick="btn_VerificaDatosAd_Click" TabIndex="890" ToolTip="Click para Grabar datos  iniciales de historico del activo" Visible="False" Width="22px" />
                     <br />
                     <br />
                     <br />
                     <br />
                 </asp:Panel>
                 <asp:Panel ID="pnl_Controles" runat="server" Width="900px">
                     <asp:ImageButton ID="btn_NuevoActivo" runat="server" Height="30px" ImageUrl="~/Iconos/Mas.png" OnClick="btn_NuevoActivo_Click" TabIndex="900" ToolTip="Click para Ingresar Nuevo Activo" Width="30px" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:ImageButton ID="btn_BuscarActivo" runat="server" Height="26px" ImageUrl="~/Iconos/Lupa.gif" OnClick="btn_BuscarActivo_Click" TabIndex="800" ToolTip="Click para Buscar Activo" Width="29px" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:ImageButton ID="btn_HabilitaModifActivo" runat="server" BackColor="Red" Height="22px" ImageUrl="~/Iconos/Edit1.png" OnClick="btn_HabilitaModifActivo_Click" TabIndex="30" ToolTip="Click para habilitar Modificación de Activo" Visible="False" Width="26px" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:ImageButton ID="btn_GrabaCambios" runat="server" BorderColor="Red" BorderStyle="None" CausesValidation="False" Height="18px" ImageUrl="~/Iconos/Modificar1.png" OnClick="btn_GrabaCambios_Click" TabIndex="150" ToolTip="Click para grabar cambios en Datos de Activos" Visible="False" Width="19px" />
                     &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btn_EliminaActivoFijo" runat="server" BorderColor="Red" Height="23px" ImageUrl="~/Iconos/Eliminar.gif" OnClick="btn_EliminaActivoFijo_Click" TabIndex="299" ToolTip="Click para Eliminar Datos de Activo Fijo" Visible="False" Width="26px" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <input id="CLOSE" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 80px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>
                 </asp:Panel>
                 <asp:Panel ID="pnl_Historico" runat="server" Height="162px" Width="603px" HorizontalAlign="Right" ScrollBars="Both" Wrap="False">
                     <asp:GridView ID="gv_HistoricoActivo" runat="server" AutoGenerateColumns="False" DataSourceID="ds_HistoricoActivo" Height="108px" OnSelectedIndexChanged="gv_DatosActivos_SelectedIndexChanged" style="margin-top: 2px; margin-right: 2px;" Width="570px">
                         <AlternatingRowStyle Wrap="False" />
                         <Columns>
                             <asp:BoundField DataField="CodigoTransaccion" HeaderText="Trans." ReadOnly="True" SortExpression="CodigoTransaccion">
                             <ItemStyle Width="20px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="FechaTransaccion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" ReadOnly="True" SortExpression="FechaTransaccion">
                             <ItemStyle Width="75px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Documento" HeaderText="Doc.N°" ReadOnly="True" SortExpression="Documento">
                             <ItemStyle HorizontalAlign="Right" Width="75px" Wrap="False" />
                             </asp:BoundField>
                             <asp:BoundField DataField="ValorTran" DataFormatString="{0:0.00}" HeaderText="Valor" SortExpression="ValorTran">
                             <HeaderStyle HorizontalAlign="Right" Wrap="False" />
                             <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="75px" Wrap="False" />
                             </asp:BoundField>
                             <asp:BoundField DataField="ValorCompras" DataFormatString="{0:0.00}" HeaderText="Compra/SI" SortExpression="ValorCompras">
                             <ItemStyle HorizontalAlign="Right" Width="75px" Wrap="False" />
                             </asp:BoundField>
                             <asp:BoundField DataField="ValorDepreciacionNormal" DataFormatString="{0:0.00}" HeaderText="Depr.Acum." SortExpression="ValorDepreciacionNormal">
                             <HeaderStyle Width="75px" />
                             <ItemStyle HorizontalAlign="Right" Width="75px" Wrap="False" />
                             </asp:BoundField>
                             <asp:BoundField DataField="ValorBaja" DataFormatString="{0:0.00}" HeaderText="V.Baja" SortExpression="ValorBaja">
                             <HeaderStyle HorizontalAlign="Right" />
                             <ItemStyle HorizontalAlign="Right" Width="75px" Wrap="True" />
                             </asp:BoundField>
                         </Columns>
                     </asp:GridView>
                     <br />
                 </asp:Panel>
            </asp:View>
            <asp:View ID="v_CuadroGenActivos" runat="server">  
                <asp:Button ID="btn_DatosIndivActivosG" runat="server" 
                      CommandArgument="v_DatosActivoF" 
                      CommandName="SwitchViewByID"
                      Text="Datos Individuales de Activos" Width="216px" OnClick="btn_DatosIndivActivosG_Click" />
                  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_CuadroXCentrosCostoG" runat="server" CommandArgument="v_CuadroXCentroCostos" CommandName="SwitchViewByID" Text="Cuadro Activos X Centro de Costos" Width="234px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="CLOSE1" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/><br />
                <asp:Label ID="ECuadroGenAF" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Text="Cuadro General de Activos" Width="261px"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;      
                 <asp:Panel ID="pnl_CuadroGeneralActivos" runat="server" Height="250px" ScrollBars="Vertical" Width="981px">
                     <asp:GridView ID="gv_DatosActivos" runat="server" AutoGenerateColumns="False" DataSourceID="ds_ActivosFijos" Height="150px" OnSelectedIndexChanged="gv_DatosActivos_SelectedIndexChanged" style="margin-top: 2px; margin-right: 2px;" Width="900px" BackColor="#99FFCC">
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
                                         <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion">
                                         <ItemStyle Font-Size="XX-Small" Width="100px" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                                         <asp:BoundField DataField="NumDeSerie" HeaderText="Serie" SortExpression="NumDeSerie" />
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
                 </asp:Panel>
            </asp:View>
         </asp:MultiView>
         <br />
         <br />
        &nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
    <asp:SqlDataSource ID="ds_CentroCostos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodCentroCosto], [DescripcionCentroCosto] FROM AContabilidad.[APContabCentrosCosto] WHERE CodCentroCosto&lt;'900'"></asp:SqlDataSource>
         <asp:SqlDataSource ID="ds_ActivosFijos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoActivo], [Categoria], [Grupo], [CodigoIngreso], [Departamento], [Ubicacion], [NombreDelActivo], [Unidades], [Medida], [Marca], [Modelo], [NumDeModelo], [NumDeSerie], [FechaIngreso], [FechaDeBaja], [VidaUtil], [TasaDepreciacionAnual], [FechaUltimaDepreciacion], [VidaUtilRev], [TasaDeprecRevAnual], [FechaUltimaRevalorizacion], [FechaUltimaDepreciacionRev], [VidaUtilMejoras], [TasaDeprecMejAnual], [FechaUltimaDeprecMejoras], [MetodoDeDepreciacion], [ValorInicial], [RevalorizacionAcumulada], [DepreciacionAcumulada], [MejorasAcumuladas], [DepreciacionAceleradaAcumulada], [DepreciacionEspecialAcumulada], [DepreciacionMejoras], [DepreciacionRevAcumulada], [ValorActual], [ValorResidual], [DepreciacionMensual], [DepreciacionRevMensual], [DepreciacionAnual], [DepreciacionRevAnual], [RevalorizacionMensual], [RevalorizacionAnual], [Comentarios], [Descripcion] FROM AFijos.[APActivosFijos]">
                 </asp:SqlDataSource>
                             <asp:SqlDataSource ID="ds_Ubicacion" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT NombreProducto, [CodigoProducto] FROM AHoteleria.[APHotelCatalogoHospedaje]
WHERE CodigoProducto&lt;'999'
AND ([CodCentroCosto] = @CodCentroCosto)">
                                 <SelectParameters>
                                     <asp:Parameter Name="CodCentroCosto" />
                                 </SelectParameters>
         </asp:SqlDataSource>
                             <asp:SqlDataSource ID="ds_Responsables" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Departamento] FROM [APActivosFijos]" ></asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_CatActivos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Descripcion], [Categoria] FROM AFijos.[APActivosFijosTCategoria]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="ds_GrupoActivos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT Descripcion, CodGrupoActivo FROM AFijos.APActivosFijosTGrupos WHERE (TipoCodigo = 'D')"></asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_CodTranActivos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodTranActivo], [Descripcion] + ' - ' + CodTranActivo As Descripcion FROM AFijos.[APActivosFijosTCodTrans]"></asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_MetodoDeprecia" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [DescripcionDeprecia] + ' ' + CodTipoDeprecia AS DD, [CodTipoDeprecia] FROM AFijos.[APActivosFijosTTipoDeprecia]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="ds_Habitaciones" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoProducto], [NombreProducto], [CodCentroCosto] FROM AHoteleria.[APHotelCatalogoHospedaje]
WHERE ([CodCentroCosto] = @CodCentroCosto)">
             <SelectParameters>
                 <asp:ControlParameter ControlID="tbx_CentroCostos" Name="CodCentroCosto" PropertyName="Text" />
             </SelectParameters>
         </asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_CodigoActivos" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [NombreDelActivo] + '         ' + CodigoActivo AS NombreActivo, [CodigoActivo], [CentroCostos], [Marca], [NumDeSerie] FROM AFijos.[APActivosFijos] WHERE ([CentroCostos] = @CentroCostos)">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="tbx_CentroCostosL" Name="CentroCostos" PropertyName="Text" />
                 </SelectParameters>
         </asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_ActivosXCentro" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT CodigoActivo, Categoria, Grupo, CodigoIngreso, Departamento, Ubicacion, NombreDelActivo, Unidades, Medida, Marca, Modelo, NumDeModelo, NumDeSerie, FechaIngreso, FechaDeBaja, VidaUtil, TasaDepreciacionAnual, FechaUltimaDepreciacion, VidaUtilRev, TasaDeprecRevAnual, FechaUltimaRevalorizacion, FechaUltimaDepreciacionRev, VidaUtilMejoras, TasaDeprecMejAnual, FechaUltimaDeprecMejoras, MetodoDeDepreciacion, ValorInicial, RevalorizacionAcumulada, DepreciacionAcumulada, MejorasAcumuladas, DepreciacionAceleradaAcumulada, DepreciacionEspecialAcumulada, DepreciacionMejoras, DepreciacionRevAcumulada, ValorActual, ValorResidual, DepreciacionMensual, DepreciacionRevMensual, DepreciacionAnual, DepreciacionRevAnual, RevalorizacionMensual, RevalorizacionAnual, Comentarios, Descripcion FROM AFijos.APActivosFijos
WHERE CentroCostos=@CentroCostos">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="tbx_CodCentroCostosParaCuadro" Name="CentroCostos" PropertyName="Text" />
                 </SelectParameters>
                 </asp:SqlDataSource>
             <asp:SqlDataSource ID="ds_HistoricoActivo" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [CodigoActivo], [CodigoTransaccion], [FechaTransaccion], [Documento], [Secuencial], [CentroCostos], [ValorTran], [ValorCompras], [ValorMejoras], [ValorRevalorizacion], [ValorDepreciacionNormal], [ValorDepreciacionAcelerada], [ValorDepreciacionEspecial], [ValorDepreciacionRevalorizacion], [ValorDepreciacionMejoras], [ValorBaja], [SucDestino], [UbicacionDestino], [DepartamentoDestino], [ResponsableDestino], [SucOrigen], [UbicacionOrigen], [DepartamentoOrigen], [ResponsableOrigen], [UserAlta], [FechaCompAlta], [Comentario] FROM AFijos.[APActivosFijosHistor] WHERE ([CodigoActivo] = @CodigoActivo)">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="tbx_CodigoActivo" Name="CodigoActivo" PropertyName="Text" Type="String" />
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


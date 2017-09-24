<%@ Page Title="Depreciación Activos " Language="C#" MasterPageFile="~/MenuPrincipal.master" AutoEventWireup="true" CodeFile="ProcesoDepreciacionActivos.aspx.cs" Inherits="ProcesoDepreciacionActivos" %>
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
         <asp:Panel ID="pnl_Comados" runat="server" Width="900px">
             <asp:Label ID="EFechaDepreciacion" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" ForeColor="Black" Height="16px" Text="Fecha Depreciación" Width="120px"></asp:Label>
             <asp:TextBox ID="tbx_FechaDepreciacion" runat="server" AutoPostBack="True" BackColor="#FFFF66" ForeColor="Black" TabIndex="600" ToolTip="Fecha Proceso de depreciación" Width="90px"></asp:TextBox>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btn_PreparaDepreciacion" runat="server" BorderColor="Red" BorderStyle="None" Height="24px" ImageUrl="~/Iconos/Proceso.png" OnClick="btn_PreparaDepreciacion_Click" TabIndex="150" ToolTip="Click para Grabar los Datos Ingresados del Comprobante de Diario" Width="31px" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btn_ValidaDepreciacion" runat="server" BorderColor="Red" BorderStyle="None" Height="24px" ImageUrl="~/Iconos/Verificacion.gif" OnClick="btn_ValidaDepreciacion_Click" TabIndex="150" ToolTip="Click para habilitar registro de depreciación" Visible="False" Width="31px" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="CLOSE0" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 100px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>
         </asp:Panel>
         <asp:Panel ID="pnl_MovDepreciacion" runat="server" Height="180px" Width="900px" ScrollBars="Vertical">
                         <asp:GridView ID="gv_DepreciacionPeriodo" runat="server" AutoGenerateColumns="False" DataSourceID="ds_DepreciacionPerido" Visible="False">
                             <Columns>
                                 <asp:BoundField DataField="CodigoActivo" HeaderText="Codigo" SortExpression="CodigoActivo" >
                                 <ItemStyle Font-Bold="True" Font-Size="Small" Width="150px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ValorInicial" DataFormatString="{0:#,##0.00}" HeaderText="V.Inicial" SortExpression="ValorInicial">
                                 <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="100px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="VidaUtil" HeaderText="VU" SortExpression="VidaUtil">
                                 <ItemStyle HorizontalAlign="Right" Width="50px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="DepreciacionAcumul" DataFormatString="{0:#,##0.00}" HeaderText="D.Acumul" SortExpression="DepreciacionAcumul">
                                 <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="100px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ValorResidual" DataFormatString="{0:0.00}" HeaderText="V.Residual" SortExpression="ValorResidual">
                                 <ItemStyle HorizontalAlign="Right" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ValorDepreciacionNormal" HeaderText="V.Deprec.N" SortExpression="ValorDepreciacionNormal" DataFormatString="{0:#,##0.00}" >
                                 <ItemStyle HorizontalAlign="Right" Font-Size="Small" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ValorActual" DataFormatString="{0:#,##0.00}" HeaderText="V.Actual" SortExpression="ValorActual">
                                 <ItemStyle HorizontalAlign="Right" Width="100px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="FechaUltimDeprecia" DataFormatString="{0:yyyy/MM/dd}" HeaderText="FUD" SortExpression="FechaUltimDeprecia" >
                                 <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="100px" />
                                 </asp:BoundField>
                             </Columns>
                         </asp:GridView>
         </asp:Panel>
         <asp:Panel ID="pnl_Controles" runat="server" Visible="False">
             <asp:Label ID="ENumDiario" runat="server" BackColor="#FFCC99" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="active" Font-Bold="True" Font-Size="Small" Text="Diario N°" Width="71px"></asp:Label>
             <asp:TextBox ID="tbx_TipoDiario" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="540" ToolTip="Ingrese el Codigo del Activo" Width="23px">G</asp:TextBox>
             <asp:TextBox ID="tbx_NumDiario" runat="server" BackColor="#FFFF66" ForeColor="Black" TabIndex="540" ToolTip="Ingrese el Codigo del Activo" Width="95px"></asp:TextBox>
             <asp:ImageButton ID="btn_GrabarDiarioDepreciacion" runat="server" BorderColor="Red" BorderStyle="None" Height="24px" ImageUrl="~/Iconos/Grabar.gif" OnClick="btn_GrabarDiarioDepreciacion_Click" TabIndex="150" ToolTip="Click para GrabarDepreciacion" Width="31px" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <input id="CLOSE" type="button" value="       Cerrar" onclick="cerrar();" style="background-position: left; background-image: url('../Iconos/Salir.gif'); font-size: small; color: #FF0000; height: 21px; width: 80px; background-color: #FFFF00; background-repeat: no-repeat; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; text-align: right;"/>
         </asp:Panel>
         <br />
         <asp:SqlDataSource ID="ds_DepreciacionPerido" runat="server" ConnectionString="<%$ ConnectionStrings:SoftempBDFPD %>" SelectCommand="SELECT [Secuen], [CentroCostos], [CodigoActivo], [Grupo], [CuentaContabDeprecia], [ValorDepreciacionNormal], [FechaDepreciacion], [UsuarioProceso], [FechaCompProceso], [DepreciacionAcumul], [ValorInicial], [FechaUltimDeprecia], [ValorResidual], [VidaUtil], [ValorActual]  FROM  AFijos.[APActivosFijosMovDepreciaPer]"></asp:SqlDataSource>
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


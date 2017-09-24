
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Globalization;

public partial class PagosAProveedores : System.Web.UI.Page
{
    static DateTime FechaConvertida;
    static string tipIdProv;
    static string CUsuario;
    static string CodServicio = "";
    static string AutorizaDoc = "";
    static string CentroCostos="";
    static string Refer="";
    static string sql;

    static SqlCommand cmd;
    static bool CodImp, Situacion;
    static decimal ValorBienes, IVABienes, PorcenIVAB, IVARetenB, PorcenIRB, IRRetenB, BaseIRB;

    static string CuentaIVACompras = "550405";
    static decimal sumaCheques;
    static string CuentaRetenIVA, CuentaRetenIR;

    static string CuentaRetenIVA30 = "25040531";
    static string CuentaRetenIVA70 = "25040532";
    static string CuentaRetenIVA100 = "25040533";


    static string CuentaRetenIR1 = "25040521";
    static string CuentaRetenIR2 = "25040522";
    static string CuentaRetenIR8 = "25040523";
    static string CuentaRetenIR10 = "25040524";



    static decimal Monto14, Monto12, Monto0, IVA14, IVA12, TotalParcial, TotalIVA, TotalFactura;
    static decimal RetenIVAT, RetenIRT, SaldoXPagar, ValorPagado, ValorCheque, Subtotal, TotalICE;
    static string IVAReten70, IVAReten100;

    //    static string Debe, Haber, NumDoc, Refer, Codigo,Cuenta,;
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    //    decimal Valor, ValorE, ValorD, 
    //    decimal Valor, ValorC;
    //    int NPagos;
    //    void RegAccesos As New RegistraAccesos;
    SqlTransaction transaction;
    SqlCommand cmdCR; //, cmd;
    //   string UsuarioR, IPClienteR, EQClienteR, ProcesoR, ReferenciaR, ObservacionR;
    //   DateTime FechaSR, FechaCR;
    RegistraAccesos RA;
    ValidaPeriodoContable VPC;
    NumComprobDiario NCD;
    protected void Page_Load(object sender, EventArgs e)
    {
        CUsuario = (string)(Session["CodUsuarioM"]);
        if (CUsuario == ""
            || String.IsNullOrEmpty(CUsuario)
            || String.IsNullOrEmpty((string)(Session["GrupoU"])))
        {
 //           MensajeR.Text = "Codigo de Usuario Invalido, Reinicie el Sistema para continuar";
        /*    
            Response.Write("<script>window.alert('Codigo de Usuario Invalido, Reinicie el Sistema para continuar');</script>");
            Response.Write("<script>window.open('','_self').close();</script>");
            Response.Write("<script>window.open('/SoftempWFP/IngresoSistema.aspx')</script>");
        */
            //  btn_NuevoComprobante.Enabled = false; // Quitar Comentario despues
            //           OcultaDiario();
  //          InicializaDatos(); // Comentariar despues
            OcultaDatosBenef();
 //           MensajeR.Visible = true;
        }
        else
        {
            if (!IsPostBack)
            {
                if (CUsuario == "")
                {
                    Response.Write("<script>window.close();</script>");
                }
                else
                {
                    MensajeR.Text = "";
                    InicializaDatos();
                    OcultaDatosBenef();
                    RA = new RegistraAccesos((string)(Session["CodUsuarioM"]),
                        (string)(Session["FechaProceso"]),
                        DateTime.Now,
                        (string)(Session["IPCliente"]),
                        (string)(Session["IPCliente"]),
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                        "Ingreso a Pago de Proveedores",
                        new System.IO.FileInfo(Page.Request.Url.AbsolutePath).Name);
                    btn_NuevoComprobante.Focus();
                }
            }
        }
        
    }

    private void InicializaDatosBenef()
    {
        tbx_TipoIdContrib.Text = "";
        tbx_IdenBenefL.Text = "";
        tbx_IdContrib.Text = "";
        tbx_Apellido1Benef.Text = "";
        tbx_Nombre1Benef.Text = "";
        tbx_NombreCompleto.Text = "";
        tbx_DireccionContrib.Text = "";
        tbx_TelefConvContrib.Text = "";
        tbx_TelefCelContrib.Text = "";
        tbx_AutorizacionR.Text = "";
        tbx_EMailContrib.Text = "";
    }

    private void InicializaComprobantesDiario()
    {
        ddl_Beneficiarios.Items.Clear();
        ddl_Beneficiarios.DataSourceID = "";
        ddl_Beneficiarios.DataSourceID = (string)(ds_Beneficiarios.UniqueID);
        ddl_Beneficiarios.DataBind();
        ddl_Beneficiarios.Items.Insert(0, "  Seleccione Beneficiario....");
        ddl_Beneficiarios.SelectedIndex = 0;
    }

    protected void InicializaDatos()
    {
        InicializaComprobantesDiario();
        InicializaDatosBenef();
        InicializaTransaccion();
        OcultaIngresoTransaccion();
        tbx_FechaComprob.Text = "";
        tbx_TipoDiario.Text = "E";
        tbx_DetalleComprob.Text = "";
        tbx_NumDiario.Text = "";
        tbx_TotalDebe.Text = "";
        tbx_TotalHaber.Text = "";
        tbx_Diferencia.Text = "";

        //       ddl_TipoDiario.SelectedIndex = 0;
        EDiferencia.Visible = false;
        tbx_Diferencia.Visible = false;
        tbx_TotalDebe.Visible = false;
        tbx_TotalHaber.Visible = false;
        EDetalle.Visible = false;
        MensajeR.Text = "";
        //       ddl_TipoDiario.SelectedIndex = 0;
        tbx_TipoDiario.Text = "E";

        btn_ImprimeComprobante.Visible = false;
        btn_VistaPreComprobante.Visible = false;
        MensajeR.Visible = false;
    }

    private void DesplegaDatosDiario()
    {
        ETipoDiario.Visible = true;
        tbx_TipoDiario.Visible = true;
        EDetalle.Visible = true;
        tbx_DetalleComprob.Visible = true;
        EFechaComprob.Visible = true;
        tbx_FechaComprob.Visible = true;
        ENumero.Visible = true;
        tbx_NumDiario.Visible = true;
        btn_GrabarComprobante.Visible = true;
    }
    private void OcultaDatosDiario()
    {
        ETipoDiario.Visible = false;
        tbx_TipoDiario.Visible = false;
        EDetalle.Visible = false;
        tbx_DetalleComprob.Visible = false;
        EFechaComprob.Visible = false;
        tbx_FechaComprob.Visible = false;
        ENumero.Visible = false;
        tbx_NumDiario.Visible = false;
        btn_GrabarComprobante.Visible = false;
        btn_IngresaTransacciones.Visible = false;

    }
    protected void InicializaTransaccion()
    {
        //       ddl_CentroCostos.SelectedIndex = 0;
        HabitaIngresoTransaccion();


        tbx_NumAutorFuente.Text = "";
        InicializaCentroCostos();

    }
    private void InicializaCentroCostos()
    {

    }
    private void DesplegaIngresoTransCC()
    {

    }
    private void OcultaIngresoTransCC()
    {

    }
    private void DesplegaIngresoTransaccion()
    {
    }
    private void OcultaIngresoTransaccion()
    {


    }
    private void HabitaIngresoTransaccion()
    {
        DesplegaIngresoTransaccion();

    }
    protected int ValidaCedula(string Cedula)
    {

        int s, Digito;
        int D1, D2, D3, D4, D5, D6, D7, D8, D9, D10;
        int r1, r3, r5, r7, r9;

        if (Cedula.Length != 10)
        {
            MensajeR.Text = "Cedula no tiene 10 Digitos";
            return 0;
        }

        D1 = Int32.Parse(Cedula.Substring(0, 1));
        D2 = Int32.Parse(Cedula.Substring(1, 1));
        D3 = Int32.Parse(Cedula.Substring(2, 1));
        D4 = Int32.Parse(Cedula.Substring(3, 1));
        D5 = Int32.Parse(Cedula.Substring(4, 1));
        D6 = Int32.Parse(Cedula.Substring(5, 1));
        D7 = Int32.Parse(Cedula.Substring(6, 1));
        D8 = Int32.Parse(Cedula.Substring(7, 1));
        D9 = Int32.Parse(Cedula.Substring(8, 1));
        D10 = Int32.Parse(Cedula.Substring(9, 1));
        r1 = D1 * 2;
        r3 = D3 * 2;
        r5 = D5 * 2;
        r7 = D7 * 2;
        r9 = D9 * 2;
        if (r1 >= 10)
        {
            r1 = r1 - 10 + 1;
        }

        if (r3 >= 10)
        {
            r3 = r3 - 10 + 1;
        }

        if (r5 >= 10)
        {
            r5 = r5 - 10 + 1;
        }

        if (r7 >= 10)
        {
            r7 = r7 - 10 + 1;
        }

        if (r9 >= 10)
        {
            r9 = r9 - 10 + 1;
        }

        s = r1 + D2 + r3 + D4 + r5 + D6 + r7 + D8 + r9;

        Digito = 10 - (s % 10);

        if (Digito == 10)
        {
            Digito = 0;
        }

        if (Digito == D10)
        {
        }
        else
        {
            MensajeR.Text = "Digito Verificador ingresado " + D10 + " esta errado, el correcto es  " + Digito;
            return 0;
        }
        return 1;
    }
    protected int LeeDatosBeneficiario(string ParamCedula)
    {
        MensajeR.Text = ParamCedula;
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select IdenBenef, TipoBenef, "
                                + "Apellido1Benef, Apellido2Benef, "
                                + "Nombre1Benef, Nombre2Benef, "
                                + "DireccionBenef, TelefConvBenef, "
                                + "TelefCelBenef, EmailBenef, "
                                + "Autorizacion, ApellidosNombresU, TipoContrib, FechaCaducidad "
                                + "From AContabilidad.APContabBenef  Where IdenBenef = '"
                                + ParamCedula + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = sql + " BENEFICIARIO NO REGISTRADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return 0;
            }
            else
            {

                if ((string)reader.Rows[0][0] == "0000000000")
                {
                    cmd.Connection.Close();
                    return 0;
                }
                else
                {
  //                  MensajeR.Text = "BENEFICIARIO YA REGISTRADO";
                    MensajeR.Visible = true;
                    if (!DBNull.Value.Equals(reader.Rows[0][0]))
                    {
                        tbx_IdContrib.Text = (string)reader.Rows[0][0];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][1]))
                    {
                        tbx_TipoIdContrib.Text = (string)reader.Rows[0][1];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }

                    tbx_TipoIdContrib.Visible = true;

                    tbx_NombreCompleto.Text = "";
                    if (!DBNull.Value.Equals(reader.Rows[0][2]))
                    {
                        tbx_Apellido1Benef.Text = (string)reader.Rows[0][2];
                        tbx_NombreCompleto.Text += " " + (string)reader.Rows[0][2];
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][4]))
                    {
                        tbx_Nombre1Benef.Text = (string)reader.Rows[0][4];
                        tbx_NombreCompleto.Text += " " + (string)reader.Rows[0][4];
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][6]))
                    {
                        tbx_DireccionContrib.Text = (string)reader.Rows[0][6];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][7]))
                    {
                        tbx_TelefConvContrib.Text = (string)reader.Rows[0][7];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][8]))
                    {
                        tbx_TelefCelContrib.Text = (string)reader.Rows[0][8];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][9]))
                    {
                        tbx_EMailContrib.Text = (string)reader.Rows[0][9];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][10]))
                    {
                        tbx_AutorizacionR.Text = (string)reader.Rows[0][10];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][11]))
                    {
                        tbx_NombreCompleto.Text = (string)reader.Rows[0][11];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][12]))
                    {
                        tbx_TipoContrib.Text = (string)reader.Rows[0][12];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][13]))
                    {
                        tbx_FCaducaFuenteR.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][13]));
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    cmd.Connection.Close();
                    return 1;
                }
            }
        }
    }
    protected void BuscarBeneficiario_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Visible = true;
        if (LeeDatosBeneficiario(ddl_Beneficiarios.SelectedItem.Value) == 1)
        {
            OcultaIngresoBenef();
        }
        else
        {
            MensajeR.Text = "INGRESE LOS DATOS DEL BENEFICIARIO";
            btn_HabilitaIngresoBenef.Visible = true;
        }
        tbx_DetalleComprob.Focus();
    }

    private void OcultaNomBenef()
    {
        ETipoContrib.Visible = false;
        ddl_TipoContrib.Visible = false;
        ETipoBenef.Visible = false;
        ddl_TipoIdContrib.Visible = false;
        ENombres.Visible = false;
        tbx_Nombre1Benef.Visible = false;
        EApellidos.Visible = false;
        tbx_Apellido1Benef.Visible = false;
//        btn_HabilitaModifDatosProv.Visible = false;
        btn_ModificaDatosBenef.Visible = false;
    }
    private void DesplegaNomBenef()
    {
        ETipoBenef.Visible = true;
        ddl_TipoIdContrib.Items.Clear();
        ddl_TipoIdContrib.DataSourceID = "";
        ddl_TipoIdContrib.DataSourceID = (string)(ds_TipoBenef.UniqueID);
        ddl_TipoIdContrib.DataBind();
        ddl_TipoIdContrib.Items.Insert(0, "  Seleccione Tipo de Identificacion....");
        ddl_TipoIdContrib.SelectedIndex = 0;
        ddl_TipoIdContrib.Visible = true;
        ETipoContrib.Visible = true;
        ddl_TipoContrib.Items.Clear();
        ddl_TipoContrib.DataSourceID = "";
        ddl_TipoContrib.DataSourceID = (string)(ds_TipoContrib.UniqueID);
        ddl_TipoContrib.DataBind();
        ddl_TipoContrib.Items.Insert(0, "  Seleccione Tipo de Contribuyente....");
        ddl_TipoContrib.SelectedIndex = 0;
        ddl_TipoContrib.Visible = true;
        ENombres.Visible = true;
        tbx_Nombre1Benef.Visible = true;
        EApellidos.Visible = true;
        tbx_Apellido1Benef.Visible = true;
    }

    private void OcultaDatosBenef()
    {
        EIdentificacion.Visible = false;
        tbx_IdContrib.Visible = false;
        tbx_TipoIdContrib.Visible = false;
        tbx_NombreCompleto.Visible = false;

        tbx_AutorizacionR.Visible = false;
        tbx_FCaducaFuenteR.Visible = false;

        ETelefonoConv.Visible = false;
        tbx_TelefConvContrib.Visible = false;
        ETelefonoCel.Visible = false;
        tbx_TelefCelContrib.Visible = false;
        EMailBenef.Visible = false;
        tbx_EMailContrib.Visible = false;
        EDireccion.Visible = false;
        tbx_DireccionContrib.Visible = false;
        btn_RegistraBenef.Visible = false;
        tbx_TipoContrib.Visible = false;
        btn_HabilitaModifDatosProv.Visible = false;
    }
    private void DesplegaDatosBenef()
    {
        EIdentificacion.Visible = true;
        tbx_IdContrib.Visible = true;
        tbx_TipoIdContrib.Visible = true;
        tbx_NombreCompleto.Visible = true;

        tbx_AutorizacionR.Visible = true;
        tbx_FCaducaFuenteR.Visible = true;

        btn_HabilitaModifDatosProv.Visible = true;
 //       tbx_NumAutorFuente.Visible = true;
        ETelefonoConv.Visible = true;
        tbx_TelefConvContrib.Visible = true;
        ETelefonoCel.Visible = true;
        tbx_TelefCelContrib.Visible = true;
        EMailBenef.Visible = true;
        tbx_EMailContrib.Visible = true;
        EDireccion.Visible = true;
        tbx_DireccionContrib.Visible = true;
        tbx_TipoContrib.Visible = true;
        //       btn_RegistraBenef.Visible = true;
    }
    private void DesplegaDatosFuente()
    {
        EDocFuente.Visible = true;
        EAutorizacionF.Visible = true;
        cbx_Bienes.Visible = true;
        cbx_Servicios.Visible = true;
        ddl_TipoDocFuente.Items.Clear();
        ddl_TipoDocFuente.DataSourceID = "";
        ddl_TipoDocFuente.DataSourceID = (string)(ds_TipoFuentes.UniqueID);
        ddl_TipoDocFuente.DataBind();
        ddl_TipoDocFuente.Items.Insert(0, "  Seleccione Tipo de Docuemnto Fuente....");
        ddl_TipoDocFuente.SelectedIndex = 0;

        btn_CopiaAutorizacion.Visible = true;
        ddl_TipoDocFuente.Visible = true;
        tbx_CodFuente.Visible = true;
        tbx_DocFuente.Visible = true;
        tbx_NumSucF.Visible = true;
        tbx_NumCajaF.Visible = true;
        tbx_NumDocFuente.Visible = true;
        EAutorizacionF.Visible = true;
        tbx_NumAutorFuente.Visible = true;
        tbx_FCaducaFuente.Visible = true;
        EMonto14.Visible = true;
        tbx_Monto14.Visible = true;
        tbx_IVA14.Visible = true;
        EMonto12.Visible = true;
        tbx_Monto12.Visible = true;
        tbx_IVA12.Visible = true;
        EMonto0.Visible = true;
        tbx_Monto0.Visible = true;
        EMontoExcento.Visible = true;
        tbx_MontoExcento.Visible = true;

        EICE.Visible = true;
        tbx_ICE.Visible = true;
        ESubSinICE.Visible = true;
        tbx_BaseSinICE.Visible = true;

        EBaseIVA.Visible = true;
        tbx_BaseIVA.Visible = true;
        EMontoIVA.Visible = true;
        tbx_TotalIVA.Visible = true;
        ETotalFactura.Visible = true;
        tbx_TotalFactura.Visible = true;

        EParteRel.Visible = true;
        lbx_parteRel.Visible = true;
        tbx_ParteRel.Visible = true;
        tbx_FEmision.Visible = true;
        EFEmision.Visible = true;

        btn_ProcesaFuente.Visible = true;
    }
    private void OcultaDatosFuente()
    {
        EDocFuente.Visible = false;
        EAutorizacionF.Visible = false;
        ddl_TipoDocFuente.Visible = false;
        tbx_CodFuente.Visible = false;
        tbx_DocFuente.Visible = false;
        tbx_NumSucF.Visible = false;
        tbx_NumCajaF.Visible = false;
        tbx_NumDocFuente.Visible = false;
        EAutorizacionF.Visible = false;
        tbx_NumAutorFuente.Visible = false;
        tbx_FCaducaFuente.Visible = false;

        EMonto14.Visible = false;
        tbx_Monto14.Visible = false;
        tbx_IVA14.Visible = false;
        EMonto12.Visible = false;
        tbx_Monto12.Visible = false;
        tbx_IVA12.Visible = false;
        EMonto0.Visible = false;
        tbx_Monto0.Visible = false;
        EMontoExcento.Visible = false;
        tbx_MontoExcento.Visible = false;

        EBaseIVA.Visible = false;
        tbx_BaseIVA.Visible = false;
        EMontoIVA.Visible = false;
        tbx_TotalIVA.Visible = false;
        ETotalFactura.Visible = false;
        tbx_TotalFactura.Visible = false;

        EICE.Visible = false;
        tbx_ICE.Visible = false;
        ESubSinICE.Visible = false;
        tbx_BaseSinICE.Visible = false;

        btn_CopiaAutorizacion.Visible = false;

        EBaseIVA.Visible = false;
        tbx_BaseIVA.Visible = false;
        EMontoIVA.Visible = false;
        tbx_TotalIVA.Visible = false;
        ETotalFactura.Visible = false;
        tbx_TotalFactura.Visible = false;

        EParteRel.Visible = false;
        lbx_parteRel.Visible = false;
        btn_CargaDatosAutor.Visible = false;
        cbx_Bienes.Visible = false;
        cbx_Servicios.Visible = false;
        btn_ProcesaFuente.Visible = false;
        btn_GrabaRegCompra.Visible = false;
        tbx_ParteRel.Visible = false;
        EFEmision.Visible = false;
        tbx_FEmision.Visible = false;

    }
    private void DesplegaRetenServicios()
    {

    }

    private void DesplegaDocumentoReten()
    {
        btn_ProcesaRetencion.Visible = true;
        ENumRetencion.Visible = true;
        tbx_NumSucC.Visible = true;
        tbx_NumCajaC.Visible = true;
        tbx_NumDocReten.Visible = true;
        tbx_NumAutorReten.Visible = true;
        tbx_FCaducaReten.Visible = true;

        ETRetenIVA.Visible = true;
        tbx_TotalRetenIVA.Visible = true;
        ETRetenIR.Visible = true;
        tbx_TotalRetenIR.Visible = true;
    }
    private void DesplegaRetenBienes()
    {
 /*       ddl_SustentoB.Items.Clear();
        ddl_SustentoB.DataSourceID = "";
        ddl_SustentoB.DataSourceID = (string)(ds_Sustentos.UniqueID);
        ddl_SustentoB.DataBind();
        ddl_SustentoB.Items.Insert(0, "  Seleccione Tipo de Sustento Tributario....");
        ddl_SustentoB.SelectedIndex = 0;
*/
        ddl_TipoBienes.Items.Clear();
        ddl_TipoBienes.DataSourceID = "";
        ddl_TipoBienes.DataSourceID = (string)(ds_ConceptosB.UniqueID);
        ddl_TipoBienes.DataBind();
        ddl_TipoBienes.Items.Insert(0, "  Seleccione Tipo de Bien....");
        ddl_TipoBienes.SelectedIndex = 0;

        ddl_CuentaContableB.Items.Clear();
        ddl_CuentaContableB.DataSourceID = "";
        ddl_CuentaContableB.DataSourceID = (string)(ds_CuentasContables.UniqueID);
        ddl_CuentaContableB.DataBind();
        ddl_CuentaContableB.Items.Insert(0, "  Seleccione Cuenta de Bienes....");
        ddl_CuentaContableB.SelectedIndex = 0;

        EBienes.Visible = true;

        ddl_TipoBienes.Visible = true;
        tbx_TipoConceptoB.Visible = true;
        ddl_CuentaContableB.Visible = true;
        tbx_CodigoCuentaB.Visible = true;
        tbx_DetalleCuentaB.Visible = true;

        EMontoB.Visible = true;
        tbx_ValorBienes.Visible = true;
        EIVAB.Visible = true;
        tbx_IVABienes.Visible = true;
        ERetenIVAB.Visible = true;
        tbx_CodIVAB.Visible = true;
        tbx_PorcenRetenIVAB.Visible = true;
        tbx_IVARetenB.Visible = true;

        ERetenIRB.Visible = true;
        tbx_PorcenRetenIRB.Visible = true;
        tbx_IRRetenB.Visible = true;

        ECodTribB.Visible = true;

        ETRetenIVA.Visible = true;
        tbx_TotalRetenIVA.Visible = true;
        ETRetenIR.Visible = true;
        tbx_BaseIRB.Visible = true;
        tbx_TotalRetenIR.Visible = true;
        ddl_CodIVAB.Visible = true;
        btn_ProcesaBienes.Visible = true;
        btn_ProcesaRetencion.Visible = true;

        btn_CopiaValoresB.Visible = true;
        btn_CopiaBaseIRB.Visible = true;


    }
    private void OcultaDatosRetenciones()
    {
        EBienes.Visible = false;
        ddl_TipoBienes.Visible = false;
        tbx_TipoConceptoB.Visible = false;
        ddl_CuentaContableB.Visible = false;
        tbx_CodigoCuentaB.Visible = false;
        tbx_DetalleCuentaB.Visible = false;

        EMontoB.Visible = false;
        tbx_ValorBienes.Visible = false;
        EIVAB.Visible = false;
        tbx_IVABienes.Visible = false;
        ERetenIVAB.Visible = false;
        tbx_PorcenRetenIVAB.Visible = false;
        tbx_IVARetenB.Visible = false;
        ECodTribB.Visible = false;
        ERetenIRB.Visible = false;
        tbx_PorcenRetenIRB.Visible = false;
        tbx_IRRetenB.Visible = false;




        ETRetenIVA.Visible = false;
        tbx_TotalRetenIVA.Visible = false;
        ETRetenIR.Visible = false;
        tbx_TotalRetenIR.Visible = false;
        btn_ProcesaRetencion.Visible = false;

        ENumRetencion.Visible = false;
        tbx_NumSucC.Visible = false;
        tbx_NumCajaC.Visible = false;
        tbx_NumDocReten.Visible = false;
        tbx_NumAutorReten.Visible = false;
        tbx_FCaducaReten.Visible = false;
        btn_RegistraDocumento.Visible = false;
        btn_ImprimeRetencion.Visible = false;
        btn_IngresaPago.Visible = false;


 //       btn_ProcesaFuente.Visible = false;
        tbx_CodIVAB.Visible = false;

        btn_ProcesaBienes.Visible = false;

        ESaldoXPagar.Visible = false;
        tbx_SaldoXPagar.Visible = false;

        btn_CopiaValoresB.Visible = false;
        ddl_CodIVAB.Visible = false;
        btn_CopiaBaseIRB.Visible = false;
        tbx_BaseIRB.Visible = false;


    }

    protected void OcultaDatosPago()
    {
        EFormaPago.Visible = false;
        ddl_FPago.Visible = false;
        tbx_FPago.Visible = false;
        ECuentaPagadora.Visible = false;
        ddl_CuentaPagadora.Visible = false;
        tbx_CuentaPagadora.Visible = false;
        EMontoPagado.Visible = false;
        tbx_ValorPagado.Visible = false;
        ddl_TipoDoc1.Visible = false;
        tbx_TipoDoc1.Visible = false;
        tbx_NumDoc1.Visible = false;
        tbx_SecuenTran.Visible = false;
        tbx_ValorCheque.Visible = false;
        EVCheque.Visible = false;
        btn_RegistraPago.Visible = false;
        btn_ImprimeCheque1.Visible = false;
        btn_ProcesaValPendientes.Visible = false;
        ETotalCheques.Visible = false;
        tbx_TotalCheque.Visible = false;
        btn_CopiaValPagACheque.Visible = false;

    }
    private void DesplegaTransacciones()
    {
        tbx_TotalDebe.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        tbx_TotalHaber.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        tbx_Diferencia.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        dgv_TransaccionesContables.Dispose();
        dgv_TransaccionesContables.DataSourceID = "";
        dgv_TransaccionesContables.DataSourceID = (string)(ds_TransaccionesContables.UniqueID);
        dgv_TransaccionesContables.DataBind();
        decimal sumaD = 0, sumaH = 0, Diferencia = 0;
        for (int X = 0; X < dgv_TransaccionesContables.Rows.Count; X++)
        {
            sumaD += decimal.Parse(dgv_TransaccionesContables.Rows[X].Cells[4].Text);
            sumaH += decimal.Parse(dgv_TransaccionesContables.Rows[X].Cells[5].Text);
            EDiferencia.Visible = true;
        }
        Diferencia = sumaD - sumaH;
        if (sumaD > 0 || sumaH > 0)
        {
            tbx_Diferencia.Visible = true;
            tbx_TotalDebe.Visible = true;
            tbx_TotalHaber.Visible = true;
        }
        tbx_TotalDebe.Text = (string.Format("{0:#,##0.00}", sumaD));
        tbx_TotalHaber.Text = (string.Format("{0:#,##0.00}", sumaH));
        tbx_Diferencia.Text = (string.Format("{0:#,##0.00}", Diferencia));

    }
    protected void btn_NuevoComprobante_Click(object sender, ImageClickEventArgs e)
    {
        DesplegaBuscaBenef();

        InicializaDatos();

        OcultaDatosDiario();
        OcultaDatosBenef();
        OcultaNomBenef();
        OcultaDatosFuente();
        OcultaDatosRetenciones();
        OcultaDatosPago();
        EBeneficiario.Visible = true;
        ddl_Beneficiarios.Visible = true;
        tbx_IdenBenefL.Visible=true;
        btn_HabilitaIngresoBenef.Visible = true;
        btn_GrabarComprobante.Visible = false;
        //       (Session["CodUsuarioM"]) = "";
        if ((string)(Session["CodUsuarioM"]) == "")
        {
            MensajeR.Text = "Codigo de Usuario Invalido, Reinicie el Sistema para continuar";

            Response.Write("<script>window.close();</script>");
            MensajeR.Visible = true;
            btn_NuevoComprobante.Focus();
        }
        else
        {
            tbx_IdenBenefL.Focus();
        }
    }

    protected void ActivaNuevoRegistro_Click(object sender, ImageClickEventArgs e)
    {
        btn_NuevoComprobante.Visible = true;
        btn_NuevoComprobante.Focus();
    }
    private void OcultaIngresoBenef()
    {
        ETipoBenef.Visible = false;
        ddl_TipoIdContrib.Visible = false;
        EApellidos.Visible = false;
        tbx_Apellido1Benef.Visible = false;
        ENombres.Visible = false;
        tbx_Nombre1Benef.Visible = false;
    }
    private void DesplegaIngresoBenef()
    {
        ETipoBenef.Visible = false;
        ddl_TipoIdContrib.Visible = false;
        EApellidos.Visible = false;
        tbx_Apellido1Benef.Visible = false;
        ENombres.Visible = false;
        tbx_Nombre1Benef.Visible = false;
    }
    private void OcultaBuscaBenef()
    {
        EBeneficiario.Visible = false;
        ddl_Beneficiarios.Visible = false;
        tbx_IdenBenefL.Visible = false;
        btn_HabilitaIngresoBenef.Visible = false;
        btn_BuscarBeneficiario.Visible = false;

    }
    private void DesplegaBuscaBenef()
    {
        EBeneficiario.Visible = true;
        ddl_Beneficiarios.Visible = true;
        btn_HabilitaIngresoBenef.Visible = true;
        btn_BuscarBeneficiario.Visible = true;


    }
    protected void btn_BuscaOtroDiario_Click(object sender, ImageClickEventArgs e)
    {

        InicializaDatos();
        OcultaBuscaBenef();
        OcultaIngresoBenef();
        OcultaDatosBenef();
        OcultaIngresoTransaccion();
        OcultaDatosDiario();
        OcultaIngresoTransCC();


    }
    public void BloqueaComprobante()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int UNumComprobante = Convert.ToInt32(tbx_NumDiario.Text) + 1;
            string sql = "Update AContabilidad.APContabDiario "
                + "Set CodImp=1 WHERE NumDiario='" + tbx_NumDiario.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            CodImp = true;
            btn_GrabarComprobante.Visible = false;
            btn_IngresaTransacciones.Visible = false;
        }
    }
    private void LeeComprobante()
    {
        string sql = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "SELECT  "
                          + "TipoDiario, NumDiario, "
                          + "FechaDiario, NombreBenef, "
                          + "DetalleDiario, "
                          + "TipoBenef, IdBenef, "
                          + "DireccionBenef, TelefConvBenef, TelefCelBenef, "
                          + "EMailBenef, Autorizacion, "
                          + "UserAlta, FechaCompAlta, OfiRecep, CodImp, Situacion "
                          + "FROM AContabilidad.APContabDiario "
                          + "WHERE NumDiario=" + 0;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " DIARIO NO REGISTRADO";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                    //                   return 0;
                }
                else
                {
                    if (!DBNull.Value.Equals(reader.Rows[0][0]))
                    {
                        tbx_TipoDiario.Text = (string)reader.Rows[0][0];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][1]))
                    {
                        tbx_NumDiario.Text = (string.Format("{0:0}", reader.Rows[0][1]));
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][2]))
                    {
                        tbx_FechaComprob.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][2]));
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][3]))
                    {
                        tbx_NombreCompleto.Text = (string)reader.Rows[0][3];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][4]))
                    {
                        tbx_DetalleComprob.Text = (string)reader.Rows[0][4];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][5]))
                    {
                        tbx_TipoIdContrib.Text = (string)reader.Rows[0][5];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][6]))
                    {
                        tbx_IdContrib.Text = (string)reader.Rows[0][6];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][7]))
                    {
                        tbx_DireccionContrib.Text = (string)reader.Rows[0][7];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][8]))
                    {
                        tbx_TelefConvContrib.Text = (string)reader.Rows[0][8];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][9]))
                    {
                        tbx_TelefCelContrib.Text = (string)reader.Rows[0][9];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][10]))
                    {
                        tbx_EMailContrib.Text = (string)reader.Rows[0][10];
                        //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][15]))
                    {
                        CodImp = (bool)reader.Rows[0][15];
                    }
                    else
                    {
                        CodImp = false;
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][16]))
                    {
                        Situacion = (bool)reader.Rows[0][16];
                    }
                    else
                    {
                        Situacion = false;
                    }
                    if (CodImp || Situacion)
                    {
                        CodImp = true;
                        btn_GrabarComprobante.Visible = false;
                        btn_IngresaTransacciones.Visible = false;
                        btn_GrabarComprobante.Visible = false;
                    }
                    else
                    {
                        CodImp = false;
                        btn_GrabarComprobante.Visible = true;
                        btn_IngresaTransacciones.Visible = true;
                        btn_GrabarComprobante.Visible = true;
                    }
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + ex.Message;
            }
            DesplegaDatosBenef();
            DesplegaTransacciones();
            btn_ImprimeComprobante.Visible = true;
            btn_VistaPreComprobante.Visible = true;

            RA = new RegistraAccesos((string)(Session["CodUsuarioM"]), (string)(Session["FechaProceso"]), DateTime.Now,
                (string)(Session["IPCliente"]), (string)(Session["IPCliente"]),
                System.Reflection.MethodBase.GetCurrentMethod().Name,
                tbx_NumDiario.Text, new System.IO.FileInfo(Page.Request.Url.AbsolutePath).Name);
        }
    }

    protected void btn_BuscarComprobante_Click(object sender, ImageClickEventArgs e)
    {
        LeeComprobante();
        DesplegaDatosDiario();
        OcultaIngresoTransaccion();
    }

    protected void ddl_TipoBenef_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_TipoIdContrib.Text = ddl_TipoIdContrib.SelectedItem.Value.ToString();
        tbx_Apellido1Benef.Focus();

    }


    protected void btn_RegistraBenef_Click(object sender, ImageClickEventArgs e)
    {
        tbx_Apellido1Benef.Text = tbx_Apellido1Benef.Text.ToUpper();

        tbx_Nombre1Benef.Text = tbx_Nombre1Benef.Text.ToUpper();

        tbx_TipoIdContrib.Text = ddl_TipoIdContrib.SelectedItem.Value.ToString();
        if (tbx_NombreCompleto.Text=="" || tbx_IdContrib.Text=="")
        {
            MensajeR.Text = "Identificacion o Nombre Completo no puede estar en blanco ";
            tbx_IdContrib.Focus();
        }
        else
        {

    //        tbx_NombreCompleto.Text = tbx_Apellido1Benef.Text 
    //                        + " " + tbx_Nombre1Benef.Text;

            string sql = "";
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                try
                {
                    sql = "Insert INTO AContabilidad.APContabBenef "
                              + "(IdenBenef, TipoBenef, "
                              + "Apellido1Benef, "
                              + "Nombre1Benef, "
                              + "DireccionBenef, TelefConvBenef, "
                              + "TelefCelBenef, EmailBenef, "
                              + "Autorizacion, ApellidosNombresU, "
                              + "TipoContrib, "
                              + "UserAlta, FechaCompAlta )"
                              + " VALUES ('" + tbx_IdContrib.Text + "', '" + tbx_TipoIdContrib.Text + "', "
                              + "'" + tbx_Apellido1Benef.Text + "', "
                              + "'" + tbx_Nombre1Benef.Text + "', "
                              + "'" + tbx_DireccionContrib.Text + "', '" + tbx_TelefConvContrib.Text + "', "
                              + "'" + tbx_TelefCelContrib.Text + "', '" + tbx_EMailContrib.Text + "', "
                              + "'" + tbx_NumAutorFuente.Text + "', '" + tbx_NombreCompleto.Text + "', "
                              + "'" + tbx_TipoContrib.Text + "', "
                              + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //               MensajeR.Text = sql;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    ddl_Beneficiarios.Dispose();
                    ddl_Beneficiarios.DataSourceID = "";
                    ddl_Beneficiarios.DataSourceID = (string)(ds_Beneficiarios.UniqueID);
                    ddl_Beneficiarios.DataBind();
                    DesplegaDatosDiario();
                    tbx_DetalleComprob.Focus();
                    btn_GrabarComprobante.Visible = true;
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.Message;
                    MensajeR.Visible = true;
                }
            }
        }

    }

    public string LeeNumComprobante()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select UNumComprobanteE From ASeguridad.DatosEmpresa ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "EMPRESA NO IDENTIFICADA";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Epresa";
            }
            else
            {
                cmd.Connection.Close();
                return Convert.ToString(reader.Rows[0][0]);
            }
        }
    }
    public void ActualNumComprobante()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int UNumComprobante = Convert.ToInt32(tbx_NumDiario.Text) + 1;
            string sql = "Update ASeguridad.DatosEmpresa "
                + "Set UNumComprobanteE='" + UNumComprobante + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
    public void ActualAutorizacionesComprobante()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Update AContabilidad.APContabDiario "
                + "Set "
                + "AutorizacionFuente='" + tbx_NumAutorFuente.Text + "'"
                + ", FechaCaducaFuente='" + tbx_FCaducaFuente.Text + "'"
                + ", SucFuente='" + tbx_NumSucF.Text + "'"
                + ", VenFuente='" + tbx_NumCajaF.Text + "'"
                + ", NumDocFuente='" + tbx_NumDocFuente.Text + "'"
                + ", SucReten='" + tbx_NumSucC.Text + "'"
                + ", VenReten='" + tbx_NumCajaC.Text + "'"
                + ", RetencionNum='" + tbx_NumDocReten.Text + "'"
                + ", AutorizacionReten='" + tbx_NumAutorReten.Text + "'"
                + ", FechaCaducaReten='" + tbx_FCaducaReten.Text + "'"
                + ", FormaDePago='" + tbx_FPago.Text + "'"
                + ", TipoDocFinanciero='" + tbx_TipoDoc1.Text + "'"
                + ", NumDocFinanciero='" + tbx_NumDoc1.Text + "'"
                + ", ValorDocFinanciero=" + ValorPagado.ToString("0.00", CultureInfo.InvariantCulture)
                + " WHERE TipoDiario='E' " + " AND NumDiario=" + tbx_NumDiario.Text;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = "Error en actualizacion de datos de Autorizaciones" + sql + " " + ex.ToString();
                MensajeR.Visible = true;
            }
            cmd.Connection.Close();
        }
    }

    public void ActualDocFinancieroEmitido()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Update AContabilidad.APContabDiario "
                + "Set "
                + " ValorFinancieroEmitido=" + ValorCheque.ToString("0.00", CultureInfo.InvariantCulture)
                + " WHERE TipoDiario='E' " + " AND NumDiario=" + tbx_NumDiario.Text;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            { 
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = "Error en actualizacion de datos de Autorizaciones" + sql + " " + ex.ToString();
                MensajeR.Visible = true;
            }
            cmd.Connection.Close();
        }
    }
    protected void GeneraComprobante()
    {
        string sql = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "Insert INTO AContabilidad.APContabDiario "
                          + "(TipoDiario, NumDiario, "
                          + "FechaDiario, NombreBenef, "
                          + "DetalleDiario, OfiRecep, "
                          + "TipoBenef, IdBenef, DireccionBenef, "
                          + "TelefConvBenef, TelefCelBenef, EMailBenef, "
                          + "AutorizacionFuente, FechaCaducaFuente, "
                          + "SucFuente, VenFuente, NumDocFuente, "
                          + "AutorizacionReten, FechaCaducaReten, "
                          + "SucReten, VenReten, RetencionNum, "
                          + "UserAlta, FechaCompAlta "
                          + ") "
                          + " VALUES ("
                          + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                          + "'" + tbx_FechaComprob.Text + "', '" + tbx_NombreCompleto.Text + "', "
                          + "'" + tbx_DetalleComprob.Text + "', '" + (string)(Session["CCostos"]) + "', "
                          + "'" + tbx_TipoIdContrib.Text + "', '" + tbx_IdContrib.Text + "', " + "'" + tbx_DireccionContrib.Text + "', "
                          + "' " + tbx_TelefConvContrib.Text + "', '" + tbx_TelefConvContrib.Text + "', " + "'" + tbx_EMailContrib.Text + "', "
                          + "'" + tbx_NumAutorFuente.Text + "', '" + tbx_FCaducaFuente.Text + "', "
                          + "'" + tbx_NumSucF.Text + "', '" + tbx_NumCajaF.Text + "', '" + tbx_NumDocFuente.Text + "', "
                          + "'" + tbx_NumAutorReten.Text + "', '" + tbx_FCaducaReten.Text + "', "
                          + "'" + tbx_NumSucC.Text + "', '" + tbx_NumCajaC.Text + "', '" + tbx_NumDocReten.Text + "', "
                          + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                          + ")";
                cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                MensajeR.Text = "Error al Crear Diario " + sql + " " + ex.Message;
            }
            finally
            {
                cmd.Connection.Close();
            }
         }
    }
    protected void btn_GrabarComprobante_Click(object sender, ImageClickEventArgs e)
    {
        RA = new RegistraAccesos((string)(Session["CodUsuarioM"]), (string)(Session["FechaProceso"]), DateTime.Now,
            (string)(Session["IPCliente"]), (string)(Session["IPCliente"]),
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            tbx_NumDiario.Text, new System.IO.FileInfo(Page.Request.Url.AbsolutePath).Name);
        if (tbx_IdContrib.Text == "")
        {
            MensajeR.Text = "Id. de Proveedor errado";
            MensajeR.Visible = true;
            tbx_IdContrib.Focus();
        }
        else
        { 
            if (tbx_FechaComprob.Text == "")
            {
                tbx_FechaComprob.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            FechaConvertida = Convert.ToDateTime(tbx_FechaComprob.Text);
            tbx_FechaComprob.Text = FechaConvertida.ToString("dd/MM/yyyy");

            VPC = new ValidaPeriodoContable(tbx_FechaComprob.Text);
            if (VPC.SituacionPeriodo != "A")
            {
                Response.Write("<script>window.alert('Periodo Contable Errado ');</script>");
                MensajeR.Text = "Periodo Contable Errado " + VPC.SituacionPeriodo;
            }
            else
            {
                if (tbx_NumDiario.Text == "" || tbx_NumDiario.Text == "0")
                {
                    NCD = new NumComprobDiario(tbx_TipoDiario.Text);
                    tbx_NumDiario.Text = Convert.ToString(NCD.NumComprobDiarioE);
                    GeneraComprobante();
                    NCD = new NumComprobDiario(tbx_TipoDiario.Text, Convert.ToInt32(tbx_NumDiario.Text));
                }
                else
                {
                    GeneraComprobante();
                }
                CodImp = false;
                Situacion = false;
                btn_IngresaTransacciones.Visible = true;
                btn_VistaPreComprobante.Visible = true;
                btn_GrabarComprobante.Visible = false;
                btn_IngresaTransacciones.Visible = true;
                DesplegaDatosFuente();
                tbx_NumDocFuente.Focus();
            }
        }
    }

    protected void ddl_CuentaContable_SelectedIndexChanged(object sender, EventArgs e)
    {
        InicializaTransaccion();

    }

    protected void btn_EscojeCuenta_Click(object sender, ImageClickEventArgs e)
    {
        InicializaTransaccion();

    }

    protected void dgv_TransaccionesContables_SelectedIndexChanged(object sender, EventArgs e)
    {

        int NF = dgv_TransaccionesContables.SelectedIndex;
        if ((string.IsNullOrEmpty(dgv_TransaccionesContables.Rows[NF].Cells[1].Text))
            || (dgv_TransaccionesContables.Rows[NF].Cells[1].Text == "&nbsp;"))
        {
            MensajeR.Text = NF + "Error en Transaccion ";
        }
        else
        {
            MensajeR.Text = NF + "Transaccion Procesada correctamente ";

            DesplegaIngresoTransaccion();
            DesplegaTransaccionesXCC();
            if (CodImp)
            {
                btn_GrabarComprobante.Visible = false;
            }
            else
            {

                //             btn_GrabarComprob.Visible = true;
            }
        }
    }
    protected void GrabaTransacciones(decimal ValorDebe,
                                      decimal ValorHaber,
                                      string CodigoCuenta,
                                      string DetalleCuenta,
                                      string TipoDoc,
                                      string NumDoc)
    {
        string sql = "";
        string TipoTran;
        if (ValorDebe > 0)
        {
            TipoTran = "D";
        }
        else
        {
            TipoTran = "H";
        }

        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            conn.Open();
            try
            {
                sql = "Insert INTO AContabilidad.APContabTransacciones "
                          + "(TipoDiario, NumDiario, "
                          + "FechaDiario, "
                          + "CodigoCuenta, SecuenTran, "
                          + "ValorDebe, Valorhaber, "
                          + "TipoTran, SituacionTran, CentroCostos, "
                          + "TipoDocTran, NumDocTran, "
                          + "AutorizaDocTran, ReferDocTran, "
                          + "Observtran, NotaTran, "
                          + "UserAlta, FechaCompAlta "
                          + ") "
                          + " VALUES ("
                          + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                          + "'" + tbx_FechaComprob.Text + "', "
                          + "'" + CodigoCuenta + "', '0', "
                          + ValorDebe.ToString("0.00", CultureInfo.InvariantCulture) + ", " + ValorHaber.ToString("0.00", CultureInfo.InvariantCulture) + ", "
                          + "'" + TipoTran + "', '0', '" + 000 + "', "
                          + "'" + TipoDoc + "', '" + NumDoc + "', "
                          + "'" + AutorizaDoc + "', '" + Refer + "', "
                          + "'" + DetalleCuenta + "', '" + Refer + "', "
                          + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                          + ")";
                cmdCR = conn.CreateCommand();
                // cmd=new SqlCommand(sql, conn);
                transaction = conn.BeginTransaction();
                cmdCR.Connection = conn;
                cmdCR.Transaction = transaction;
//                MensajeR.Text += sql;
                cmdCR.CommandText = sql;
                cmdCR.ExecuteNonQuery();

                sql = "Insert INTO AContabilidad.APContabTransaccionesXCC "
                          + "(TipoDiario, NumDiario, "
                          + "FechaDiario, "
                          + "CodigoCuenta, SecuenTran, "
                          + "CentroDeCostos, "
                          + "ValorDebe, Valorhaber, "
                          + "TipoTran, TipoDocTranCC, "
                          + "NumDocTranCC, "
                          + "ObservTranCC, NotaCC, "
                          + "UserAlta, FechaCompAlta "
                          + ") "
                          + " VALUES ("
                          + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                          + "'" + tbx_FechaComprob.Text + "', "
                          + "'" + CodigoCuenta + "', '" + 0 + "', "
                          + "'" + CentroCostos + "', "
                          + ValorDebe.ToString("0.00", CultureInfo.InvariantCulture) + ", " + ValorHaber.ToString("0.00", CultureInfo.InvariantCulture) + ", "
                          + "'" + TipoTran + "', '', '', "
                          + "'', '', "
                          + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                          + ")";
                cmdCR.CommandText = sql;
                cmdCR.ExecuteNonQuery();
                transaction.Commit();
                DesplegaTransacciones();
            }
            catch (Exception ex)
            {
                MensajeR.Text += ex.Message;
                MensajeR.Visible = true;
                tbx_DetalleComprob.Focus();
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

    }
    protected void DesplegaTransaccionesXCC()
    {

    }
    protected void btn_RegistraValoresXCC_Click(object sender, ImageClickEventArgs e)
    {
        //    string sql = "";
        decimal ValorDebe = 0, ValorHaber = 0;
        //    string TipoTran;

        MensajeR.Visible = true;
        MensajeR.Text += "Paso Valores";
        if (ValorDebe > 0 || ValorHaber > 0)
        {
        }
    }

    protected void btn_ImprimeComprobante_Click(object sender, ImageClickEventArgs e)
    {
        if (tbx_TotalHaber.Text == ""
    || tbx_TotalDebe.Text == ""
    || tbx_TotalDebe.Text == "0,00"
    || tbx_TotalDebe.Text == "0,00"
    || (string.IsNullOrEmpty(tbx_TotalDebe.Text))
    || (string.IsNullOrEmpty(tbx_TotalHaber.Text)))
        {
            MensajeR.Text = "Comprobante con error, no se puede imprimir";
            MensajeR.Visible = true;
        }
        else
        {
            if (tbx_TotalDebe.Text == tbx_TotalHaber.Text)
            {
                Session["NumDiario"] = tbx_NumDiario.Text;
                MensajeR.Text = (string)(Session["NumDiario"]) + "D " + tbx_TotalDebe.Text + "H " + tbx_TotalHaber.Text;
                MensajeR.Visible = true;
                Response.Write("<script>window.open('VisorComprobanteEgreso.aspx');</script>");
                BloqueaComprobante();
            }
            else
            {
                MensajeR.Text = "Comprobante Descuadrado, no se puede imprimir";
                MensajeR.Visible = true;
            }
        }
    }

    protected void btn_VistaPreComprobante_Click(object sender, ImageClickEventArgs e)
    {

    }




    private void OcultaComplementarios()
    {

        tbx_NumDocFuente.Visible=false;

        tbx_NumAutorFuente.Visible=false;
    }
    protected void dgv_TransaccionesXCC_SelectedIndexChanged(object sender, EventArgs e)
    {

        DesplegaTransaccionesXCC();

        if (CodImp)
        {

        }
        else
        {
            DesplegaIngresoTransCC();

        }
    }
    protected void btn_ImprimeCheque1_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Visible = true;
        Session["NumDocumento"] = tbx_NumDoc1.Text;
        Session["CodBanco"] = tbx_CuentaPagadora.Text;
        Response.Write("<script>window.open('VisorImpresionCheque.aspx');</script>");
    }


    protected void btn_IngresaDiarioSinBenef_Click(object sender, ImageClickEventArgs e)
    {
        DesplegaDatosDiario();
        tbx_DetalleComprob.Focus();
        btn_GrabarComprobante.Visible = true;
        OcultaBuscaBenef();
        btn_IngresaTransacciones.Visible = false;
    }

    protected void btn_ModificaDatosBenef_Click(object sender, ImageClickEventArgs e)
    {
        tbx_NombreCompleto.Text = tbx_NombreCompleto.Text.ToUpper(); 
        string sql = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "UPDATE AContabilidad.APContabBenef "
                          + "SET "
                          + "DireccionBenef='" + tbx_DireccionContrib.Text + "'"
                          + ", TelefConvBenef='" + tbx_TelefConvContrib.Text + "'"
                          + ", TelefCelBenef='" + tbx_TelefCelContrib.Text + "'"
                          + ", EmailBenef='" + tbx_EMailContrib.Text + "'"
                          + ", Autorizacion='" + tbx_AutorizacionR.Text + "'"
                          + ", FechaCaducidad='" + tbx_FCaducaFuenteR.Text + "'"
                          + ", ApellidosNombresU='" + tbx_NombreCompleto.Text + "'"
                          + ", TipoContrib='" + tbx_TipoContrib.Text + "'"
                          + ", UserMod='" + (string)(Session["CodUsuarioM"]) + "'"
                          + ", FechaCompMod='" + DateTime.Now + "'"
                          + " WHERE IdenBenef='" + tbx_IdContrib.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //               MensajeR.Text = sql;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                OcultaNomBenef();
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + ex.Message;
                MensajeR.Visible = true;
            }
        }
    }

    protected void btn_HabilitaIngresoBenef_Click(object sender, ImageClickEventArgs e)
    {
        btn_RegistraBenef.Visible = true;
        InicializaDatosBenef();
        DesplegaNomBenef();
        DesplegaDatosBenef();
        tbx_IdContrib.Text = tbx_IdenBenefL.Text;
        ddl_TipoIdContrib.Focus();
    }

    protected void btn_IngresaTransacciones_Click(object sender, ImageClickEventArgs e)
    {
        DesplegaDatosFuente();
    }

    protected void ddl_Beneficiarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        MensajeR.Visible = true;
        if (LeeDatosBeneficiario(ddl_Beneficiarios.SelectedItem.Value) == 1)
        {
            OcultaBuscaBenef();
            OcultaIngresoBenef();
            DesplegaDatosBenef();
            //            DesplegaDatosFuente();
            //           DesplegaDatosRetenciones();
            //           DesplegaDatosDiario();
            // btn_HabilitaModifDatosProv.Visible = true;

            DesplegaDatosDiario();
            btn_GrabarComprobante.Visible = true;
 //           btn_IngresaTransacciones.Visible = true;
 //           btn_RegistraBenef.Visible = true;
            tbx_DetalleComprob.Focus();
        }
        else
        {
            MensajeR.Text = ddl_Beneficiarios.SelectedItem.Value + "BENEFICIARIO NO REGISTRADO, INGRESE DATOS DEL BENEFICIARIO";
            btn_HabilitaIngresoBenef.Visible = true;
            btn_RegistraBenef.Visible = true;
        }
    }

    protected void ddl_TipoDoc1_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_TipoDoc1.Text = ddl_TipoDoc1.SelectedItem.Value.ToString();

    }

    protected void ds_CuentasContables_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }

    protected void tbx_IdenBenefL_TextChanged(object sender, EventArgs e)
    {
        MensajeR.Visible = true;
        if (LeeDatosBeneficiario(tbx_IdenBenefL.Text) == 1)
        {
 //           MensajeR.Text = ddl_Beneficiarios.SelectedItem.Text;
            OcultaBuscaBenef();
            OcultaIngresoBenef();
            DesplegaDatosBenef();
 //           DesplegaDatosFuente();
 //           btn_HabilitaModifDatosProv.Visible = true;
 //           DesplegaDatosRetenciones();
            DesplegaDatosDiario();
            btn_GrabarComprobante.Visible = true;
 //           btn_IngresaTransacciones.Visible = true;
 //           btn_RegistraBenef.Visible = true;
            tbx_DetalleComprob.Focus();
        }
        else
        {
            MensajeR.Text = "INGRESE LOS DATOS DEL BENEFICIARIO" + tbx_IdenBenefL.Text;
            tbx_IdContrib.Text = tbx_IdenBenefL.Text;
            DesplegaIngresoBenef();
            btn_HabilitaIngresoBenef.Visible = true;
            btn_RegistraBenef.Visible = true;
        }
    }


    public void CalculaFactura()
    {
        tbx_Monto14.Text = tbx_Monto14.Text.Replace(".", ",");
        Monto14 = System.Convert.ToDecimal(tbx_Monto14.Text);
        tbx_Monto14.Text = string.Format("{0:0.00}", Monto14);
        IVA14 = Monto14 * 14 / 100;
        tbx_IVA14.Text = string.Format("{0:0.00}", IVA14);
        IVA14= System.Convert.ToDecimal(tbx_IVA14.Text);
        tbx_Monto12.Text = tbx_Monto12.Text.Replace(".", ",");
        Monto12 = System.Convert.ToDecimal(tbx_Monto12.Text);
        tbx_Monto12.Text = string.Format("{0:0.00}", Monto12);
        IVA12 = Monto12 * 12 / 100;
        tbx_IVA12.Text = string.Format("{0:0.00}", IVA12);
        IVA12 = System.Convert.ToDecimal(tbx_IVA12.Text);

        tbx_Monto0.Text = tbx_Monto0.Text.Replace(".", ",");
        Monto0 = System.Convert.ToDecimal(tbx_Monto0.Text);
        tbx_Monto0.Text = string.Format("{0:0.00}", Monto0);

        tbx_ICE.Text = tbx_ICE.Text.Replace(".", ",");
        TotalICE = System.Convert.ToDecimal(tbx_ICE.Text);
        tbx_ICE.Text = string.Format("{0:0.00}", TotalICE);

        TotalParcial = Monto14 + Monto12 + Monto0;
        Subtotal = TotalParcial - TotalICE;
        tbx_BaseSinICE.Text = string.Format("{0:0.00}", Subtotal);
        TotalIVA = IVA14 + IVA12;
        TotalFactura = TotalParcial + TotalIVA;
        tbx_BaseIVA.Text = string.Format("{0:0.00}", TotalParcial);
        tbx_TotalIVA.Text = string.Format("{0:0.00}", TotalIVA);
        tbx_TotalFactura.Text = string.Format("{0:0.00}", TotalFactura);
        
        EBaseIVA.Visible = true;
        tbx_BaseIVA.Visible = true;
        EMontoIVA.Visible = true;
        tbx_TotalIVA.Visible = true;
        ETotalFactura.Visible = true;
        tbx_TotalFactura.Visible = true;

        tbx_ValorBienes.Text = "0";
        tbx_IVABienes.Text = "0";
        tbx_BaseIRB.Text = "0";
        tbx_IRRetenB.Text = "0";


        ValorBienes = 0;
        IVABienes = 0;


 //       tbx_ValorBienes.Text = string.Format("{0:0.00}", ValorBienes);
 //       tbx_BaseIRB.Text = string.Format("{0:0.00}", Subtotal);
 //       tbx_IVABienes.Text = string.Format("{0:0.00}", IVABienes);

        ddl_TipoBienes.Focus();
    }
    protected string ObtieneSecuenCFinal()
    {
        int NumSecuenCFinal = 0;
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select UNumSecuenCFinal From ASeguridad.DatosEmpresa ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "EMPRESA NO IDENTIFICADA";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en lectura de Secuencial de consumidor final";
            }
            else
            {
                NumSecuenCFinal = Convert.ToInt16(reader.Rows[0][0]) + 1;
                sql = "Update ASeguridad.DatosEmpresa "
                    + "Set UNumSecuenCFinal='" + NumSecuenCFinal + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return Convert.ToString(NumSecuenCFinal);
            }
        }
    }
    protected void btn_ProcesaFuente_Click(object sender, ImageClickEventArgs e)
    {
        if (tbx_IdContrib.Text=="9999999999999")
        {
            if (tbx_NumDocFuente.Text=="0" || tbx_NumDocFuente.Text=="")
            {
                tbx_NumDocFuente.Text=ObtieneSecuenCFinal();
            }
        }
        IVARetenB = 0;

        IRRetenB = 0;

 //       DesplegaRetenBienes();
 //       DesplegaRetenServicios();
 //       OcultaDatosRetenciones();
        CalculaFactura();
        //       DesplegaRetenServicios();
        //       DesplegaRetenBienes();


        if (tbx_NumAutorFuente.Text == "" || tbx_FCaducaFuente.Text == "" || tbx_NumDocFuente.Text == "")
        {
            MensajeR.Text = "Error en Datos de Documento Fuente";
            tbx_NumDocFuente.Focus();
        }
        else
        {
            btn_GrabaRegCompra.Visible = true;
            btn_GrabaRegCompra.Focus();

            //            CalculaFactura();
 /*           if (cbx_Bienes.Checked)
            {
                DesplegaRetenBienes();
                ddl_CuentaContableB.Focus();
                if (cbx_Servicios.Checked)
                {
                    DesplegaRetenServicios();
                }
            }
            else
            {
                DesplegaRetenServicios();
                ddl_CuentaContableS.Focus();
            }
*/
        }
    }

    protected void tbx_IdBenef_TextChanged(object sender, EventArgs e)
    {

    }

    protected void tbx_IRBienes_TextChanged(object sender, EventArgs e)
    {

    }

    protected void tbx_IVARetenS_TextChanged(object sender, EventArgs e)
    {

    }

    protected void tbx_Monto14_TextChanged(object sender, EventArgs e)
    {
        CalculaFactura();
    }

    protected void tbx_Monto12_TextChanged(object sender, EventArgs e)
    {
        CalculaFactura();
    }

    protected void tbx_Monto0_TextChanged(object sender, EventArgs e)
    {
        CalculaFactura();
    }

    public void CalculaRetenBienes()
    {
        tbx_ValorBienes.Text = tbx_ValorBienes.Text.Replace(".", ",");
        ValorBienes = System.Convert.ToDecimal(tbx_ValorBienes.Text);
        tbx_ValorBienes.Text = string.Format("{0:0.00}", ValorBienes);
        try
        { 
            tbx_IVABienes.Text = tbx_IVABienes.Text.Replace(".", ",");
            IVABienes = System.Convert.ToDecimal(tbx_IVABienes.Text);
        }
        catch (Exception ex)
        {
            MensajeR.Text = tbx_IVABienes.Text + ex.ToString();
        }

        tbx_IVABienes.Text = string.Format("{0:0.00}", IVABienes);

        tbx_PorcenRetenIVAB.Text = tbx_PorcenRetenIVAB.Text.Replace(".", ",");
        PorcenIVAB = System.Convert.ToDecimal(tbx_PorcenRetenIVAB.Text);
        tbx_PorcenRetenIVAB.Text = string.Format("{0:0}", PorcenIVAB);

        IVARetenB = IVABienes*PorcenIVAB/100;
        tbx_IVARetenB.Text= string.Format("{0:0.00}", IVARetenB);

        tbx_BaseIRB.Text = tbx_BaseIRB.Text.Replace(".", ",");
        BaseIRB = System.Convert.ToDecimal(tbx_BaseIRB.Text);
        tbx_BaseIRB.Text = string.Format("{0:0.00}", BaseIRB);

        tbx_PorcenRetenIRB.Text = tbx_PorcenRetenIRB.Text.Replace(".", ",");
        PorcenIRB = System.Convert.ToDecimal(tbx_PorcenRetenIRB.Text);
        tbx_PorcenRetenIRB.Text = string.Format("{0:0}", PorcenIRB);

        IRRetenB = BaseIRB * PorcenIRB / 100;
        tbx_IRRetenB.Text = string.Format("{0:0.00}", IRRetenB);

        CalculaRetenTotales();

    }
   
    private void CalculaRetenTotales()
    {
  //      CalculaRetenBienes();
  //      CalculaRetenServicios();
        RetenIVAT = IVARetenB;
        tbx_TotalRetenIVA.Text = string.Format("{0:0.00}", RetenIVAT);
        RetenIRT = IRRetenB;
        tbx_TotalRetenIR.Text = string.Format("{0:0.00}", RetenIRT);

    }
    protected void btn_ProcesaRetencion_Click(object sender, ImageClickEventArgs e)
    {
//        CalculaFactura();
        if ((ValorBienes ) != TotalParcial)
        {
            MensajeR.Text = "Valores no Cuadran con Total Factura " 
                + ValorBienes.ToString("0.00", CultureInfo.InvariantCulture) + " "
                + TotalParcial.ToString("0.00", CultureInfo.InvariantCulture);
        }
        else
        {
            if ((IVABienes ) != TotalIVA)
            {
                MensajeR.Text = "Valores IVA no Cuadran con Total Factura "
                + IVABienes.ToString("0.00", CultureInfo.InvariantCulture) + " "
                + TotalIVA.ToString("0.00", CultureInfo.InvariantCulture); 
            }
            else
            {
                SaldoXPagar = TotalFactura - RetenIVAT - RetenIRT;
                tbx_SaldoXPagar.Text = (string.Format("{0:0.00}", SaldoXPagar));
                ESaldoXPagar.Visible = true;
                tbx_SaldoXPagar.Visible = true;
                DesplegaDocumentoReten();
                //                ENumRetencion.Visible = true;
                //                tbx_NumSucC.Visible = true;
                //               tbx_NumCajaC.Visible = true;
                //               tbx_NumAutorReten.Visible = true;
                //               tbx_FCaducaReten.Visible = true;
                //               tbx_NumDocReten.Visible = true;
                btn_CargaDatosAutor.Visible = true;
                btn_RegistraDocumento.Visible = true;
            }
        }
    }

    protected void btn_HabilitaModifDatosProv_Click(object sender, ImageClickEventArgs e)
    {
  //      btn_RegistraBenef.Visible = true;
  //      InicializaDatosBenef();
        DesplegaNomBenef();
        DesplegaDatosBenef();
        ddl_TipoIdContrib.Focus();
        btn_ModificaDatosBenef.Visible = true;
    }

    protected void ddl_TipoContrib_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_TipoContrib.Text = ddl_TipoContrib.SelectedItem.Value.ToString(); 
    }

    protected void btn_CopiaAutorizacion_Click(object sender, ImageClickEventArgs e)
    {
        tbx_NumAutorFuente.Text = tbx_AutorizacionR.Text;
        tbx_FCaducaFuente.Text = tbx_FCaducaFuenteR.Text;
        tbx_Monto14.Focus();
    }

    protected void ddl_TipoBienes_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_TipoConceptoB.Text = ddl_TipoBienes.SelectedItem.Value.ToString();
        tbx_PorcenRetenIRB.Text=ObtienePorcenIR(tbx_TipoConceptoB.Text);
    }
    protected string ObtienePorcenIR(string CodigoIR)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                string sql = "Select Porcen From AContabilidad.APContabAnexConceptos "
                             + " Where CodConcepto = '" + CodigoIR + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    // do something if the query return no rows 
                    // you may insert the relevant redirection you asked for
                    MensajeR.Text = "Concepto NO IDENTIFICADO";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                    return "0";
                }
                else
                {
                    // Get the value of Col1 in the 3rd row (0 is the first row)
                    // Col1Value = (int)dt.Rows[2]["Col1"];
                    cmd.Connection.Close();
                    return (string.Format("{0:0.00}",  reader.Rows[0][0]));
                    // or just make the other redirection from your question
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = "Error en obtencion de Porcentaje de Retencion de IR " + ex.Message;
                MensajeR.Visible = true;
                return "0";
            }
        }
    }


    protected void ddl_CuentaContableB_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CodigoCuentaB.Text = ddl_CuentaContableB.SelectedItem.Value.ToString();
        tbx_ValorBienes.Focus();
    }



    protected void ddl_TipoDocFuente_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CodFuente.Text=ddl_TipoDocFuente.SelectedItem.Value.ToString();
        tbx_DocFuente.Text = ddl_TipoDocFuente.SelectedItem.Text;

    }

    protected void ProcesaContabDocumento()
    {
        CalculaRetenBienes();

        CalculaRetenTotales();
 /*       if (TotalIVA > 0)
        {
            GrabaTransacciones(TotalIVA, 0, CuentaIVACompras, "IVA en Compras", "", "");
        }
 */

        if (ValorBienes > 0)
        {
            GrabaTransacciones(ValorBienes, 0, tbx_CodigoCuentaB.Text, "Compra de Bienes", "", "");
            if (IVABienes > 0)
            {
                if (IVARetenB > 0)
                {
                    switch (tbx_PorcenRetenIVAB.Text)
                        {
                            case "30":
                                CuentaRetenIVA = CuentaRetenIVA30;
                                break;
                            case "70":
                                CuentaRetenIVA = CuentaRetenIVA70;
                                break;
                            case "100":
                                CuentaRetenIVA = CuentaRetenIVA100;
                                break;
                            default:
                                MensajeR.Text = "Error en Porcentaje IVA " + tbx_PorcenRetenIVAB.Text;
                                break;                        
                        }
                    GrabaTransacciones(0, IVARetenB, CuentaRetenIVA, "Retencion IVA en Bienes", "", "");
                }
            }
            if (IRRetenB > 0)
            {
                    switch (tbx_PorcenRetenIRB.Text)
                    {
                        case "1":
                            CuentaRetenIR = CuentaRetenIR1;
                            break;
                        case "2":
                            CuentaRetenIR = CuentaRetenIR2;
                            break;
                        case "8":
                            CuentaRetenIR = CuentaRetenIR8;
                            break;
                        case "10":
                            CuentaRetenIR = CuentaRetenIR10;
                            break;
                        default:
                            MensajeR.Text += " Error en Porcentaje IR Bienes" + tbx_PorcenRetenIRB.Text;
                            break;
                }
                GrabaTransacciones(0, IRRetenB, CuentaRetenIR, "Retencion IR en Bienes", "", "");
            }
        }

    }

    protected void ProcesaComprobanteReten()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            conn.Open();
            try
            {
               sql = "INSERT INTO AContabilidad.APContabRetenciones "
                + "(SucReten, VenReten, NumReten"
                + ", AutorizaReten, FechaCaducaReten"
                + ", CentroCosto, TipoComprob, NumComprob"
                + ", FechaComprob, DetalleComprob"
                + ", TipoContrib, TipoIdContrib"
                + ", IdContrib, NombreContrib, MailContrib"
                + ", DireccionContrib, TelefonoContrib"
                + ", CodTipoFuente, TipoFuente "
                + ", SucFuente, VenFuente, NumFuente"
                + ", AutorizaFuente, FechaCaducaFuente"
                + ", CodConceptoB, CodRetIVAB"
                + ", BaseImponIRB, TasaRetenIRB "
                + ", RetencionIRB, BaseImponIVAB"
                + ", TasaRetenIVAB, RetencionIVAB"
                + ", CodConceptoS, CodRetIVAS"
                + ", BaseImponIRS, TasaRetenIRS"
                + ", RetencionIRS, BaseImponIVAS"
                + ", TasaRetenIVAS, RetencionIVAS"
                + ", ValorBienes, ValorServicios"
                + ", UserAlta, FechaCompAlta"
                + ") "
                + " VALUES ("
                + "'" + tbx_NumSucC.Text + "', '" + tbx_NumCajaC.Text + "', '" + tbx_NumDocReten.Text + "', "
                + "'" + tbx_NumAutorReten.Text + "', '" + tbx_FCaducaReten.Text + "', "
                + "'000', 'E', " + tbx_NumDiario.Text + ", "
                + "'" + tbx_FechaComprob.Text + "', '" + tbx_DetalleComprob.Text + "', "
                + "'" + tbx_TipoContrib.Text + "', '" + tbx_TipoIdContrib.Text + "', "
                + "'" + tbx_IdContrib.Text + "', '" + tbx_NombreCompleto.Text + "', '" + tbx_EMailContrib.Text + "', "
                + "'" + tbx_DireccionContrib.Text + "', '" + tbx_TelefConvContrib.Text + " / " + tbx_TelefCelContrib.Text + "', "
                + "'" + tbx_CodFuente.Text + "', '" + tbx_DocFuente.Text + "', "
                + "'" + tbx_NumSucF.Text + "', '" + tbx_NumCajaF.Text + "', '" + tbx_NumDocFuente.Text + "', "
                + "'" + tbx_NumAutorFuente.Text + "', '" + tbx_FCaducaFuente.Text + "', "
                + "'" + tbx_TipoConceptoB.Text + "', '" + tbx_CodIVAB.Text + "', "
                + BaseIRB.ToString("0.00", CultureInfo.InvariantCulture) + ", " + PorcenIRB.ToString("0.00", CultureInfo.InvariantCulture) + ", "               
                + IRRetenB.ToString("0.00", CultureInfo.InvariantCulture) + ", " + IVABienes.ToString("0.00", CultureInfo.InvariantCulture) + ", "
                + PorcenIVAB.ToString("0.00", CultureInfo.InvariantCulture) + ", " + IVARetenB.ToString("0.00", CultureInfo.InvariantCulture) + ", "
                + "'', '" + tbx_CodIVAB.Text + "', 0, 0, 0, 0, 0, 0, 0, 0,"
                + ValorBienes.ToString("0.00", CultureInfo.InvariantCulture) + ", 0, " 
                + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                + ")";
                cmdCR = conn.CreateCommand();
                cmdCR.Connection = conn;
                transaction = conn.BeginTransaction();
                //               MensajeR.Text = sql;
                cmdCR.Transaction = transaction;
                cmdCR.CommandText = sql;
                cmdCR.ExecuteNonQuery();

                IVAReten70 = "0.00";
                IVAReten100 = "0.00";

                sql = "UPDATE AContabilidad.APContabCompras SET "
                    + "valRetBien10=" + "0.00, "
                    + "valRetServ20=" + "0.00, " 
                    + "valorRetBienes=" + IVARetenB.ToString("0.00", CultureInfo.InvariantCulture) + ", "
                    + "valRetServ50=" + "0.00, " 
                    + "valorRetServicios=" + IVAReten70.Replace(",",".") + ", "
                    + "valRetServ100=" + IVAReten100.Replace(",", ".") + ", "
                    + "totbasesImpReemb=" + "0.00, "
                    + "CodConceptoB='" + tbx_TipoConceptoB.Text + "', "
                    + "BaseImponIRB=" + tbx_BaseIRB.Text.Replace(",",".") + ", "
                    + "TasaRetIRB=" + tbx_PorcenRetenIRB.Text.Replace(",", ".") + ", "
                    + "ValorRetIRB=" + tbx_IRRetenB.Text.Replace(",", ".") + ", "
                    + "CodConceptoS='0', "
                    + "BaseImponIRS=0, "
                    + "TasaRetIRS=0, "
                    + "ValorRetIRS=0, "
                    + "estabRetencion1='" + tbx_NumSucC.Text + "', "
                    + "ptoEmiRetencion1='" + tbx_NumCajaC.Text + "', "
                    + "secRetencion1='" + tbx_NumDocReten.Text + "', "
                    + "fechaEmiRet1='" + tbx_FechaComprob.Text + "', "
                    + "autRetencion1='" + tbx_NumAutorReten.Text + "', "
                    + "UsuarioMod='" + (string)(Session["CodUsuarioM"]) + "', " 
                    + "FechaCompMod='" + DateTime.Now + "' "
                    + " WHERE TipoDiario='E' " + " AND Diario=" + tbx_NumDiario.Text;
                cmdCR = conn.CreateCommand();
                cmdCR.Connection = conn;
   //             MensajeR.Text = sql;
                cmdCR.CommandText = sql;
                cmdCR.Transaction = transaction;
                cmdCR.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + "Error en Grabacion de Retencion "  + ex.ToString();
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }
    }


    protected void btn_RegistraDocumento_Click(object sender, ImageClickEventArgs e)
    {
        ProcesaContabDocumento();
        ProcesaComprobanteReten();
        ActualAutorizacionesComprobante();
        btn_RegistraDocumento.Visible = false;
        btn_ImprimeRetencion.Visible = true;
        btn_IngresaPago.Visible = true;
    }

    protected void btn_IngresaNuevoDocFuente_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btn_IngresaPago_Click(object sender, ImageClickEventArgs e)
    {
        EFormaPago.Visible = true;

        ddl_FPago.Items.Clear();
        ddl_FPago.DataSourceID = "";
        ddl_FPago.DataSourceID = (string)(ds_FPago.UniqueID);
        ddl_FPago.DataBind();
        ddl_FPago.Items.Insert(0, "  Seleccione Forma de Pago....");
        ddl_FPago.Visible = true;
        tbx_FPago.Visible = true;

        ECuentaPagadora.Visible = true;

        ddl_CuentaPagadora.Items.Clear();
        ddl_CuentaPagadora.DataSourceID = "";
        ddl_CuentaPagadora.DataSourceID = (string)(ds_CuentasContables.UniqueID);
        ddl_CuentaPagadora.DataBind();
        ddl_CuentaPagadora.Items.Insert(0, "  Seleccione Cuenta Pagadora....");

        ddl_CuentaPagadora.Visible = true;
        tbx_CuentaPagadora.Visible = true;
        EMontoPagado.Visible = true;
        tbx_ValorPagado.Visible = true;

        ddl_TipoDoc1.Items.Clear();
        ddl_TipoDoc1.DataSourceID = "";
        ddl_TipoDoc1.DataSourceID = (string)(ds_TipoDocumentos.UniqueID);
        ddl_TipoDoc1.DataBind();
        ddl_TipoDoc1.Items.Insert(0, "  Seleccione Tipo de Documento de Pago....");

        ddl_TipoDoc1.Visible = true;
        tbx_TipoDoc1.Visible = true;
        tbx_NumDoc1.Visible = true;
        btn_RegistraPago.Visible = true;
  //      btn_ImprimeCheque1.Visible = true;
        tbx_CuentaPagadora.Text = "1103101001";
        tbx_NumDoc1.Text = "";
        tbx_TipoDoc1.Text = "CHEQ";
        tbx_ValorPagado.Text = tbx_SaldoXPagar.Text;
        EVCheque.Visible = true;
        tbx_ValorCheque.Visible = true;
        btn_ProcesaValPendientes.Visible = true;

    }

    protected void btn_ImprimeRetencion_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Visible = true;
        Session["NumSuc"] = tbx_NumSucC.Text;
        Session["NumVen"] = tbx_NumCajaC.Text;
        Session["NumReten"] = tbx_NumDocReten.Text;
        Response.Write("<script>window.open('VisorDeComprobanteRetencion.aspx');</script>");

    }

    protected void tbx_CuentaPagadora_TextChanged(object sender, EventArgs e)
    {

    }

    protected void tbx_IVARetenB_TextChanged(object sender, EventArgs e)
    {
        CalculaRetenBienes();
    }

    protected void tbx_PorcenIVAB_TextChanged(object sender, EventArgs e)
    {
        CalculaRetenBienes();
    }

    protected void tbx_PorcenRetenIRB_TextChanged(object sender, EventArgs e)
    {
        CalculaRetenBienes();

    }

    protected void btn_ProcesaBienes_Click(object sender, ImageClickEventArgs e)
    {
        
        tbx_ValorBienes.Text = tbx_ValorBienes.Text.Replace(".", ",");
        ValorBienes = System.Convert.ToDecimal(tbx_ValorBienes.Text);
        if (ValorBienes > 0)
        {   
            MensajeR.Text = "Valor bienes = " + tbx_ValorBienes.Text + " " + (string.Format("{0:0.00}", TotalParcial));
            if (ValorBienes == TotalParcial)
            {
                tbx_IVABienes.Text = (string.Format("{0:0.00}", TotalIVA));
                tbx_IVABienes.Text = tbx_IVABienes.Text.Replace(".", ",");
                IVABienes = TotalIVA;
                tbx_IVABienes.Text = (string.Format("{0:0.00}", IVABienes));
            }
            else
            {
                if (TotalParcial <= 0)
                {
                    MensajeR.Text = "Error en valor parcial de Factura " + (string.Format("{0:#,##0.00}", TotalParcial)); ;

                }
                else
                {
                    if (ValorBienes > TotalParcial)
                    {
                        ValorBienes = TotalParcial;
                        tbx_ValorBienes.Text = (string.Format("{0:0.00}", ValorBienes));
                    }
                    tbx_IVABienes.Text = tbx_IVABienes.Text.Replace(".", ",");
                    IVABienes = System.Convert.ToDecimal(tbx_IVABienes.Text);
                    if (IVABienes > TotalIVA)
                    {
                        IVABienes = TotalIVA;
                    }
                    tbx_IVABienes.Text = (string.Format("{0:0.00}", IVABienes));
                }
            }
            if (ValorBienes > 0)
            {
                if (tbx_TipoConceptoB.Text == "")
                {
                    MensajeR.Text = "Error en Codigo de Tipo de Bienes";
                    ddl_TipoBienes.Focus();
                }
                else
                {
                    if (tbx_CodigoCuentaB.Text == "")
                    {
                        MensajeR.Text = "Error en Codigo de Cuenta de Bienes";
                        ddl_CuentaContableB.Focus();
                    }
                    else
                    {
                        CalculaRetenBienes();
                        CalculaRetenTotales();
                        if (ValorBienes < TotalParcial)
                        {
                            DesplegaRetenServicios();
                        }
                        else
                        {
                            DesplegaDocumentoReten();
                        }
                    }
                }
            }
            else
            {
                DesplegaRetenServicios();
            }
        }
        else
        {
           // ddl_SustentoS.Focus();
        }
    }



    protected void tbx_ValorBienes_TextChanged(object sender, EventArgs e)
    {
        tbx_ValorBienes.Text = tbx_ValorBienes.Text.Replace(".", ",");
        ValorBienes = System.Convert.ToDecimal(tbx_ValorBienes.Text);

        if (ValorBienes == 0)
        {
            IVABienes = 0;
            tbx_IVABienes.Text = "0";
            btn_ProcesaBienes.Focus();
        }
        else
        {
            if (ValorBienes==TotalParcial)
            {
                tbx_IVABienes.Text = tbx_TotalIVA.Text;
                IVABienes = System.Convert.ToDecimal(tbx_IVABienes.Text);
            }
            else
            {
                if (ValorBienes > TotalParcial)
                {
                    MensajeR.Text = "Error en Valor de Bienes ";
                    tbx_ValorBienes.Focus();
                }

            }
        }
    }

    protected void tbx_IVABienes_TextChanged(object sender, EventArgs e)
    {
        tbx_IVABienes.Text = tbx_IVABienes.Text.Replace(".", ",");
        IVABienes = System.Convert.ToDecimal(tbx_IVABienes.Text);
        if (IVABienes>TotalIVA)
        {
            MensajeR.Text = "Error en Valor de IVA Bienes";
            tbx_IVABienes.Focus();
        }
    }

    protected void btn_RegistraPago_Click(object sender, ImageClickEventArgs e)
    {
        tbx_ValorPagado.Text = tbx_ValorPagado.Text.Replace(".", ",");
        ValorPagado = System.Convert.ToDecimal(tbx_ValorPagado.Text);
        tbx_SaldoXPagar.Text = tbx_SaldoXPagar.Text.Replace(".", ",");
        SaldoXPagar = System.Convert.ToDecimal(tbx_SaldoXPagar.Text);
        tbx_ValorCheque.Text = tbx_ValorCheque.Text.Replace(".", ",");
        ValorCheque = System.Convert.ToDecimal(tbx_ValorCheque.Text);
        ProcesaValoresPendientesDocFinanciero();
        if (ValorPagado <= 0 || ValorPagado > SaldoXPagar)
        {
            MensajeR.Text = "Valor Girado Errado " + ValorPagado.ToString("0.00", CultureInfo.InvariantCulture)+ " " + SaldoXPagar.ToString("0.00", CultureInfo.InvariantCulture);
            tbx_ValorPagado.Focus();
        }
        else
        {
            GrabaTransaccionPago(0, ValorPagado, tbx_CuentaPagadora.Text, "Banco ", tbx_TipoDoc1.Text, tbx_NumDoc1.Text);
            ActualAutorizacionesComprobante();
        }
    }
    protected void GrabaTransaccionPago(decimal ValorDebe,
                                  decimal ValorHaber,
                                  string CodigoCuenta,
                                  string DetalleCuenta,
                                  string TipoDoc,
                                  string NumDoc)
    {
        sql = "";
        string TipoTran;
        if (ValorDebe > 0)
        {
            TipoTran = "D";
        }
        else
        {
            TipoTran = "H";
        }

        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            conn.Open();
            try
            {
                sql = "Insert INTO AContabilidad.APContabTransacciones "
                          + "(TipoDiario, NumDiario, "
                          + "FechaDiario, "
                          + "CodigoCuenta, SecuenTran, "
                          + "ValorDebe, Valorhaber, "
                          + "TipoTran, SituacionTran, CentroCostos, "
                          + "TipoDocTran, NumDocTran, "
                          + "AutorizaDocTran, ReferDocTran, "
                          + "Observtran, NotaTran, "
                          + "UserAlta, FechaCompAlta "
                          + ") "
                          + " VALUES ("
                          + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                          + "'" + tbx_FechaComprob.Text + "', "
                          + "'" + CodigoCuenta + "', " + tbx_SecuenTran.Text + ", "
                          + ValorDebe.ToString("0.00", CultureInfo.InvariantCulture) + ", " + ValorHaber.ToString("0.00", CultureInfo.InvariantCulture) + ", "
                          + "'" + TipoTran + "', '0', '" + 000 + "', "
                          + "'" + TipoDoc + "', '" + NumDoc + "', "
                          + "'" + AutorizaDoc + "', '" + Refer + "', "
                          + "'" + DetalleCuenta + "', '" + Refer + "', "
                          + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                          + ")";
                cmdCR = conn.CreateCommand();
                // cmd=new SqlCommand(sql, conn);
                transaction = conn.BeginTransaction();
                cmdCR.Connection = conn;
                cmdCR.Transaction = transaction;
                MensajeR.Text += sql;
                cmdCR.CommandText = sql;
                cmdCR.ExecuteNonQuery();

                sql = "Insert INTO AContabilidad.APContabTransaccionesXCC "
                          + "(TipoDiario, NumDiario, "
                          + "FechaDiario, "
                          + "CodigoCuenta, SecuenTran, "
                          + "CentroDeCostos, "
                          + "ValorDebe, Valorhaber, "
                          + "TipoTran, TipoDocTranCC, "
                          + "NumDocTranCC, "
                          + "ObservTranCC, NotaCC, "
                          + "UserAlta, FechaCompAlta "
                          + ") "
                          + " VALUES ("
                          + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                          + "'" + tbx_FechaComprob.Text + "', "
                          + "'" + CodigoCuenta + "', '" + tbx_SecuenTran.Text + "', "
                          + "'" + CentroCostos + "', "
                          + ValorDebe.ToString("0.00", CultureInfo.InvariantCulture) + ", " + ValorHaber.ToString("0.00", CultureInfo.InvariantCulture) + ", "
                          + "'" + TipoTran + "', '', '', "
                          + "'', '', "
                          + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                          + ")";
                MensajeR.Text = sql;
                cmdCR.CommandText = sql;
                cmdCR.Transaction = transaction;           
                cmdCR.ExecuteNonQuery();
                ActualDocFinancieroEmitido();
                MensajeR.Text = "Centros de Costo OK";
                btn_ImprimeComprobante.Visible = true;


                sql = "UPDATE AContabilidad.APContabCompras SET "
                    + "formaPago='" + tbx_FPago.Text + "', "
                    + "UsuarioMod='" + (string)(Session["CodUsuarioM"]) + "', "
                    + "FechaCompMod='" + DateTime.Now + "' "
                    + " WHERE TipoDiario='E' " + " AND Diario=" + tbx_NumDiario.Text;
                cmdCR = conn.CreateCommand();
                cmdCR.Connection = conn;
                cmdCR.Transaction = transaction;
                MensajeR.Text = sql;
                cmdCR.CommandText = sql;
                cmdCR.ExecuteNonQuery();
                transaction.Commit();
                DesplegaTransacciones();                
            }
            catch (Exception ex)
            {
                MensajeR.Text += ex.Message;
                MensajeR.Visible = true;
                tbx_DetalleComprob.Focus();
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

    }



    protected void tbx_FCaducaFuenteR_TextChanged(object sender, EventArgs e)
    {
        FechaConvertida = Convert.ToDateTime(tbx_FCaducaFuenteR.Text);
        tbx_FCaducaFuenteR.Text = FechaConvertida.ToString("dd/MM/yyyy");
    }

    protected void tbx_FCaducaFuente_TextChanged(object sender, EventArgs e)
    {
        FechaConvertida = Convert.ToDateTime(tbx_FCaducaFuente.Text);
        tbx_FCaducaFuente.Text = FechaConvertida.ToString("dd/MM/yyyy");
    }

    protected void tbx_FCaducaReten_TextChanged(object sender, EventArgs e)
    {
        FechaConvertida = Convert.ToDateTime(tbx_FCaducaReten.Text);
        tbx_FCaducaReten.Text = FechaConvertida.ToString("dd/MM/yyyy");
    }



    protected void tbx_NumDocFuente_TextChanged(object sender, EventArgs e)
    {
        tbx_NumAutorFuente.Focus();
    }

    protected void btn_CargaDatosAutor_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select AutorizacionRetenR, FechaCaducaRetenR From ASeguridad.DatosEmpresa ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "EMPRESA NO IDENTIFICADA";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                tbx_NumAutorReten.Text= "Error en Lectura de Epresa";
            }
            else
            {
                if (!DBNull.Value.Equals(reader.Rows[0][1]))
                {
                    tbx_NumAutorReten.Text = Convert.ToString(reader.Rows[0][0]);
                }
                if (!DBNull.Value.Equals(reader.Rows[0][1]))
                {
                    tbx_FCaducaReten.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][1]));
                    //               DServicio.Text = LeeServicio((string)reader.Rows[0][1]);
                }

                cmd.Connection.Close();

            }
        }
    }

    protected void ddl_CodIVAB_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CodIVAB.Text = ddl_CodIVAB.SelectedItem.Value.ToString();
        tbx_PorcenRetenIVAB.Text = LeePorcenCodIVA(tbx_CodIVAB.Text);

    }

    protected string LeePorcenCodIVA(string CodIVA)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select PorcenIVA From AContabilidad.APContabAnexCodIVA "
                         + " WHERE CodIVA = '" + CodIVA + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "Codigo IVA NO IDENTIFICADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "0";
             }
            else
            {
                cmd.Connection.Close();
                return (string.Format("{0:0}", reader.Rows[0][0])); 
            }
    }
}





    protected void btn_CopiaValoresB_Click(object sender, ImageClickEventArgs e)
    {
        tbx_ValorBienes.Text = tbx_BaseIVA.Text;
        tbx_IVABienes.Text = tbx_TotalIVA.Text;
    }



    protected void btn_CopiaBaseIRB_Click(object sender, ImageClickEventArgs e)
    {
        tbx_BaseIRB.Text = tbx_BaseSinICE.Text;
    }

    protected void ProcesaValoresPendientesDocFinanciero()
    {
        sumaCheques = 0;
        for (int X = 0; X < dgv_Cheques.Rows.Count; X++)
        {
            sumaCheques += decimal.Parse(dgv_Cheques.Rows[X].Cells[0].Text);
        }
        ETotalCheques.Visible = true;
        tbx_TotalCheque.Text = sumaCheques.ToString("0.00", CultureInfo.InvariantCulture);
        tbx_TotalCheque.Visible = true;
    }
    protected void btn_ProcesaValPendientes_Click(object sender, ImageClickEventArgs e)
    {
        ProcesaValoresPendientesDocFinanciero();
        btn_CopiaValPagACheque.Visible = true;
    }

    protected void btn_CopiaValPagACheque_Click(object sender, ImageClickEventArgs e)
    {
        ProcesaValoresPendientesDocFinanciero();
        tbx_ValorPagado.Text = tbx_ValorPagado.Text.Replace(".", ",");
        ValorPagado = System.Convert.ToDecimal(tbx_ValorPagado.Text);

        ValorCheque = sumaCheques + ValorPagado;
        tbx_ValorCheque.Text = ValorCheque.ToString("0.00", CultureInfo.InvariantCulture);

        btn_GrabaCheque.Visible = true;
        btn_GrabaCheque.Focus();

    }

    protected void btn_GrabaCheque_Click(object sender, ImageClickEventArgs e)
    {
        sql = "";
        MensajeR.Text = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            conn.Open();
            try
            {
                MensajeR.Text = "";
                cmdCR = conn.CreateCommand();
                // cmd=new SqlCommand(sql, conn);
                transaction = conn.BeginTransaction();
                cmdCR.Connection = conn;
                cmdCR.Transaction = transaction;
                           
                if ((tbx_TipoDoc1.Text == "CHEQ "
                    || tbx_TipoDoc1.Text == "CHEQ") && ValorCheque > 0)
                {
                    sql = "Insert INTO AContabilidad.APContabBancos "
                         + "(CuentaBanco, FechaTran, CodTran, NumDocumento, SecuenTran, "
                         + "ValorTran, SituacionTran, IdBenef , NombreBenef, "
                         + "ComprobanteContab, OficinaRecep, "
                         + "Observacion, UsuarioTran, FechaComp "
                         + ") "
                         + " VALUES ("
                         + "'" + tbx_CuentaPagadora.Text + "', '" + tbx_FechaComprob.Text + "', "
                         + "'" + tbx_TipoDoc1.Text + "', '" + tbx_NumDoc1.Text + "', "
                         + "'0', " + ValorCheque.ToString("0.00", CultureInfo.InvariantCulture) + ", '" + 0 + "', "
                         + "'" + tbx_IdContrib.Text + "', '" + tbx_NombreCompleto.Text + "', '" + tbx_NumDiario.Text + "', "
                         + "'000', '" + tbx_DetalleComprob.Text + "', "
                         + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                         + ")";
                    cmdCR.CommandText = sql;
                    try
                    {
                        cmdCR.ExecuteNonQuery();
                        transaction.Commit();
                        ActualDocFinancieroEmitido();
                        MensajeR.Text = "Bancos OK";
                        btn_ImprimeCheque1.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MensajeR.Text = "Error en Grabar cheque/Transferencia "  + sql + " " + ex.Message;
                    }
                }
                else 
                {
                    MensajeR.Text = "No Proceso " + tbx_TipoDoc1.Text + " N° " + tbx_NumDoc1.Text;
                }

            }
            catch (Exception ex)
            {
                MensajeR.Text += ex.Message;
                MensajeR.Visible = true;
                tbx_DetalleComprob.Focus();
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }
    }

    protected void ddl_Sustento_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_Sustento.Text = ddl_Sustento.SelectedItem.Value.ToString();

    }

    protected void btn_GrabaRegCompra_Click(object sender, ImageClickEventArgs e)
    {
        sql = "";
        switch (tbx_TipoIdContrib.Text)
        {
            case "R":
                tipIdProv = "01";
                break;
            case "C":
                tipIdProv = "02";
                break;
            case "P":
                tipIdProv = "03";
                break;
            case "N":
                tipIdProv = "07";
                break;
            default:
                tipIdProv = "";
                MensajeR.Text = "Error en Tipo de Identificacion del Proceedor";
                break;
        }

        if (tipIdProv == "")
        {

        }
        else
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                conn.Open();
                try
                {
                    sql = "INSERT INTO AContabilidad.APContabCompras "
                     + "(TipoDiario, Diario, CodSustento, "
                     + "tpIdProv, idProv, tipoComprobante, "
                     + "parteRel, fechaRegistro, "
                     + "establecimiento, puntoEmision, secuencial, "
                     + "fechaEmision, autorizacion, "
                     + "baseNoGraIva, baseImponible, "
                     + "baseImpGrav, baseImpExe, "
                     + "montoIce, montoIva, "
                     + "valRetBien10, valRetServ20, valorRetBienes, "
                     + "valRetServ50, valorRetServicios, valRetServ100, "
                     + "totbasesImpReemb, formaPago, "
                     + "CodRetIVA, ValorRetIVAB, TasaRetenIVAB, "
                     + "ValorRetIVAS, TasaRetenIVAS, "
                     + "CodConceptoB, BaseImponIRB, TasaRetIRB, ValorRetIRB, "
                     + "CodConceptoS, BaseImponIRS, TasaRetIRS, ValorRetIRS, "
                     + "estabRetencion1, ptoEmiRetencion1, secRetencion1, fechaEmiRet1, "
                     + "NombreContrib, DetalleComprob, "
                     + "UsuarioAlta, FechaCompAlta"
                     + ") "
                     + " VALUES ("
                     + "'E', " + tbx_NumDiario.Text + ", '" + tbx_Sustento.Text + "', "
                     + "'" + tipIdProv + "', '" + tbx_IdContrib.Text + "', '" + tbx_CodFuente.Text + "', "
                     + "'" + tbx_ParteRel.Text + "', '" + tbx_FechaComprob.Text + "', "
                     + "'" + tbx_NumSucF.Text + "', '" + tbx_NumCajaF.Text + "', '" + tbx_NumDocFuente.Text + "', "
                     + "'" + tbx_FEmision.Text + "', '" + tbx_NumAutorReten.Text + "', "
                     + "0.00, " + tbx_Monto0.Text.Replace(",", ".") + ", "
                     + tbx_BaseIVA.Text.Replace(",", ".") + ", 0.00, "
                     + "" + tbx_ICE.Text.Replace(",", ".") + ", " + tbx_TotalIVA.Text.Replace(",", ".") + ", "
                     + "0.00, 0.00, 0.00, "
                     + "0.00, 0.00, 0.00, "
                     + "0.00, '20', "
                     + "'', 0.00, 0.00, "
                     + "0.00, 0.00, "
                     + "'', 0.00, 0.00, 0.00, "
                     + "'', 0.00, 0.00, 0.00, "
                     + "'', '', '', '', "
                     + "'" + tbx_NombreCompleto.Text + "', '" + tbx_DetalleComprob.Text + "', "
                     + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "'"
                     + ")";
                    //               MensajeR.Text = sql;
                    cmdCR = conn.CreateCommand();
                    cmdCR.Connection = conn;

                    cmdCR.CommandText = sql;
                    cmdCR.ExecuteNonQuery();
                    if (TotalIVA > 0)
                    {
                        GrabaTransacciones(TotalIVA, 0, CuentaIVACompras, "IVA en Compras", "", "");
                    }
                    if (cbx_Bienes.Checked)
                    {
                        DesplegaRetenBienes();
                        ddl_CuentaContableB.Focus();
                        if (cbx_Servicios.Checked)
                        {
                            DesplegaRetenServicios();
                        }
                    }
                    else
                    {
                        DesplegaRetenServicios();

                    }

                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + "Error en Grabacion de Compra " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }

    protected void lbx_parteRel_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_ParteRel.Text = lbx_parteRel.SelectedItem.Value.ToString();
    }



    protected void tbx_FEmision_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FechaConvertida = Convert.ToDateTime(tbx_FEmision.Text);
            tbx_FEmision.Text = FechaConvertida.ToString("dd/MM/yyyy");
        }
        catch(Exception ex)
        {
            MensajeR.Text = "Fecha de emision Errada " + ex.Message;
        }

    }




    protected void tbx_TipoConceptoB_TextChanged(object sender, EventArgs e)
    {

    }

    protected void tbx_TipoIdContrib_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_FPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_FPago.Text = ddl_FPago.SelectedItem.Value.ToString();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gv_DatosActivos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int NF = gv_DatosActivos.SelectedIndex;
        if ((string.IsNullOrEmpty(gv_DatosActivos.Rows[NF].Cells[1].Text))
            || (gv_DatosActivos.Rows[NF].Cells[1].Text == "&nbsp;"))
        {
            MensajeR.Text = NF + "Error en Transaccion ";
        }
        else
        {
            MensajeR.Text = NF + "Transaccion Procesada correctamente ";
            tbx_CodigoActivo.Text = gv_DatosActivos.Rows[NF].Cells[1].Text;
            ECodigoActivo.Visible = true;
            tbx_CodigoActivo.Visible = true;
        }
    }

    protected void btn_NuevoComprobante1_Click(object sender, ImageClickEventArgs e)
    {
        tbx_CodigoActivo.Focus();
    }



    protected void btn_GeneraCodigo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ddl_Categoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_Categoria.Text = ddl_Categoria.SelectedItem.Value.ToString();

    }

    protected void ddl_CentroCostos_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CentroCostos.Text = ddl_CentroCostos.SelectedItem.Value.ToString();
    }

    protected void ddl_GrupoActivos_SelectedIndexChanged(object sender, EventArgs e)
    {

        tbx_GrupoActivos.Text = ddl_GrupoActivos.SelectedItem.Value.ToString();
    }

    protected void ddl_RazonIngreso_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_RazonIngreso.Text = ddl_RazonIngreso.SelectedItem.Value.ToString();
    }

    protected void ddl_UbicaHabita_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_NumHabitacion.Text = ddl_UbicaHabita.SelectedItem.Value.ToString();
        tbx_Ubicacion.Text = ddl_UbicaHabita.SelectedItem.Text;
    }
}
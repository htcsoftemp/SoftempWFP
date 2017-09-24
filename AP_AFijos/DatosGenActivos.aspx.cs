
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

public partial class DatosActivosFijos : System.Web.UI.Page
{
    static DateTime FechaConvertida;
    static string CUsuario;
    static string CodServicio = "";

    static string sql;

    static SqlCommand cmd;
    static int UNumSecuenAFijos;

    /*  static string CuentaIVACompras = "550405";
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
      */
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
    //   ValidaPeriodoContable VPC;
    //    NumComprobDiario NCD;
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

                    RA = new RegistraAccesos((string)(Session["CodUsuarioM"]),
                        (string)(Session["FechaProceso"]),
                        DateTime.Now,
                        (string)(Session["IPCliente"]),
                        (string)(Session["IPCliente"]),
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                        "Ingreso a Pago de Proveedores",
                        new System.IO.FileInfo(Page.Request.Url.AbsolutePath).Name);

                }
            }
        }

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

    private void InicializaParamActivos()
    {
        ddl_Categoria.Items.Clear();
        ddl_Categoria.DataSourceID = "";
        ddl_Categoria.DataSourceID = (string)(ds_CatActivos.UniqueID);
        ddl_Categoria.DataBind();
        ddl_Categoria.Items.Insert(0, "  Seleccione la categoria del Activo....");
        ddl_Categoria.SelectedIndex = 0;

        ddl_GrupoActivos.Items.Clear();
        ddl_GrupoActivos.DataSourceID = "";
        ddl_GrupoActivos.DataSourceID = (string)(ds_GrupoActivos.UniqueID);
        ddl_GrupoActivos.DataBind();
        ddl_GrupoActivos.Items.Insert(0, "  Seleccione Grupo de Activos....");
        ddl_GrupoActivos.SelectedIndex = 0;

        ddl_CentroCostos.Items.Clear();
        ddl_CentroCostos.DataSourceID = "";
        ddl_CentroCostos.DataSourceID = (string)(ds_CentroCostos.UniqueID);
        ddl_CentroCostos.DataBind();
        ddl_CentroCostos.Items.Insert(0, "  Seleccione el Centro de Costos....");
        ddl_CentroCostos.SelectedIndex = 0;

        ddl_CentroCostosL.Items.Clear();
        ddl_CentroCostosL.DataSourceID = "";
        ddl_CentroCostosL.DataSourceID = (string)(ds_CentroCostos.UniqueID);
        ddl_CentroCostosL.DataBind();
        ddl_CentroCostosL.Items.Insert(0, "  Seleccione el Centro de Costos....");
        ddl_CentroCostosL.SelectedIndex = 0;

        ddl_CodigoActivoL.Items.Clear();
        ddl_CodigoActivoL.DataSourceID = "";
        ddl_CodigoActivoL.DataSourceID = (string)(ds_CodigoActivos.UniqueID);
        ddl_CodigoActivoL.DataBind();
        ddl_CodigoActivoL.Items.Insert(0, "  Seleccione el Activo que desea Procesar...");
        ddl_CodigoActivoL.SelectedIndex = 0;


        ddl_RazonIngreso.Items.Clear();
        ddl_RazonIngreso.DataSourceID = "";
        ddl_RazonIngreso.DataSourceID = (string)(ds_CodTranActivos.UniqueID);
        ddl_RazonIngreso.DataBind();
        ddl_RazonIngreso.Items.Insert(0, "  Seleccione Razon de Ingreso de Activo....");
        ddl_RazonIngreso.SelectedIndex = 0;
        tbx_RazonIngreso.Text = "";

        ddl_MetodoDeprecia.Items.Clear();
        ddl_MetodoDeprecia.DataSourceID = "";
        ddl_MetodoDeprecia.DataSourceID = (string)(ds_MetodoDeprecia.UniqueID);
        ddl_MetodoDeprecia.DataBind();
        ddl_MetodoDeprecia.Items.Insert(0, "  Seleccione Metodo de depreciacion...");
        ddl_MetodoDeprecia.SelectedIndex = 0;

        ddl_UbicaHabita.Items.Clear();
        ddl_UbicaHabita.DataSourceID = "";
        ddl_UbicaHabita.DataSourceID = (string)(ds_Habitaciones.UniqueID);
        ddl_UbicaHabita.DataBind();
        ddl_UbicaHabita.Items.Insert(0, "  Seleccione habitación en donde esta ubicado el activo...");
        ddl_UbicaHabita.SelectedIndex = 0;

        tbx_CodigoActivoL.Text = "";
        tbx_CentroCostosL.Text = "";
        tbx_GrupoActivos.Text = "";


        tbx_CodigoActivo.Text = "";
        tbx_NombreActivo.Text = "";
        tbx_MarcaActivo.Text = "";
        tbx_SerieActivo.Text = "";
        tbx_ValorInicial.Text = "0.00";
        tbx_VidaUtil.Text = "0";
        tbx_FechaIngreso.Text = "";
        tbx_DescripcionActivo.Text = "";
        tbx_FUltimaDeprecia.Text = "";
        tbx_Ubicacion.Text = "";
        tbx_Responsable.Text = "";
        tbx_FUltimaDeprecia.Text = "";
        tbx_FUltimaDepReval.Text = "";
        tbx_FUltimaMejora.Text = "";
        tbx_FUltimDepMejoras.Text = "";
        tbx_FUltimReval.Text = "";
        tbx_ValorDepAcelerada.Text = "0.00";
        tbx_ValorActual.Text = "0.00";
        tbx_ValorDepEspecial.Text = "0.00";
        tbx_ValorDepMejoras.Text = "0.00";
        tbx_ValorDepreReval.Text = "0.00";
        tbx_ValorMejoras.Text = "0.00";
        tbx_PorcenValResid.Text = "0";
        tbx_ValorResidual.Text = "0.00";
        tbx_ValorReval.Text = "0.00";

        tbx_DepreciaNormAcum.Text = "0.00";
        tbx_ValorActual.Text = "0.00";
        btn_EliminaActivoFijo.Visible = false;
        btn_GrabaCambios.Visible = false;
        btn_GrabarActivo.Visible = false;

        pnl_DatosBasicosActivo.Visible = true;

        btn_HabilitaIngresoComplementos.Visible = true;
        EValorDepAcelerada.Visible = false;
        tbx_ValorDepAcelerada.Visible = false;
        EValDepEspecial.Visible = false;
        tbx_ValorDepEspecial.Visible = false;
        pnl_DatosActivosAnter.Visible = false;



    }
    private void InicializaParamActivosa()
    {
        ddl_Categoria.Items.Clear();
        ddl_Categoria.DataSourceID = "";
        ddl_Categoria.DataSourceID = (string)(ds_CatActivos.UniqueID);
        ddl_Categoria.DataBind();
        ddl_Categoria.Items.Insert(0, "  Seleccione la categoria del Activo....");
        ddl_Categoria.SelectedIndex = 0;

        ddl_GrupoActivos.Items.Clear();
        ddl_GrupoActivos.DataSourceID = "";
        ddl_GrupoActivos.DataSourceID = (string)(ds_GrupoActivos.UniqueID);
        ddl_GrupoActivos.DataBind();
        ddl_GrupoActivos.Items.Insert(0, "  Seleccione Grupo de Activos....");
        ddl_GrupoActivos.SelectedIndex = 0;


        ddl_RazonIngreso.Items.Clear();
        ddl_RazonIngreso.DataSourceID = "";
        ddl_RazonIngreso.DataSourceID = (string)(ds_CodTranActivos.UniqueID);
        ddl_RazonIngreso.DataBind();
        ddl_RazonIngreso.Items.Insert(0, "  Seleccione Razon de Ingreso de Activo....");
        ddl_RazonIngreso.SelectedIndex = 0;


        ddl_MetodoDeprecia.Items.Clear();
        ddl_MetodoDeprecia.DataSourceID = "";
        ddl_MetodoDeprecia.DataSourceID = (string)(ds_MetodoDeprecia.UniqueID);
        ddl_MetodoDeprecia.DataBind();
        ddl_MetodoDeprecia.Items.Insert(0, "  Seleccione Metodo de depreciacion...");
        ddl_MetodoDeprecia.SelectedIndex = 0;

        ddl_UbicaHabita.Items.Clear();
        ddl_UbicaHabita.DataSourceID = "";
        ddl_UbicaHabita.DataSourceID = (string)(ds_Habitaciones.UniqueID);
        ddl_UbicaHabita.DataBind();
        ddl_UbicaHabita.Items.Insert(0, "  Seleccione habitación en donde esta ubicado el activo...");
        ddl_UbicaHabita.SelectedIndex = 0;


    }





    protected void GrabaActivo()
    {
        sql = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "Insert INTO AFijos.APActivosFijos ("
                    + "CentroCostos, CodigoActivo, "
                    + "Categoria, Grupo, "
                    + "NombreDelActivo, Descripcion, "
                    + "Marca, NumDeSerie, "
                    + "CodHabitacion, Ubicacion, Responsable, "
                    + "CodigoIngreso, FechaIngreso, VidaUtil, "
                    + "PorcenResidual, ValorResidual, "
                    + "ValorInicial, FechaUltimaDepreciacion, "
                    + "DepreciacionAcumulada, ValorActual, "
                    + "FechaUltimaRevalorizacion, RevalorizacionAcumulada, "
                    + "FechaUltimaDepreciacionRev, DepreciacionRevAcumulada, "
                    + "FechaUltimaMejora, MejorasAcumuladas, "
                    + "FechaUltimaDeprecMejoras, DepreciacionMejoras, "
                    + "DepreciacionAceleradaAcumulada, DepreciacionEspecialAcumulada, "
                    + "UserAlta, FechaCompAlta "
                    + ") "
                    + " VALUES ("
                    + "'" + tbx_CentroCostos.Text + "', '" + tbx_CodigoActivo.Text + "', "
                    + "'" + tbx_Categoria.Text + "', '" + tbx_GrupoActivos.Text + "', "
                    + "'" + tbx_NombreActivo.Text + "', '" + tbx_DescripcionActivo.Text + "', "
                    + "'" + tbx_MarcaActivo.Text + "', '" + tbx_SerieActivo.Text + "', "
                    + "'" + tbx_NumHabitacion.Text + "', '" + tbx_Ubicacion.Text + "', '" + tbx_Responsable.Text + "', "
                    + "'" + tbx_RazonIngreso.Text + "', '" + tbx_FechaIngreso.Text + "', '" + tbx_VidaUtil.Text + "', "
                    + "'" + tbx_PorcenValResid.Text + "', " + tbx_ValorResidual.Text.Replace(",", ".") + ", "
                    + "" + tbx_ValorInicial.Text.Replace(",", ".") + ", '" + tbx_FUltimaDeprecia.Text + "', "
                    + "" + tbx_DepreciaNormAcum.Text.Replace(",", ".") + ", " + tbx_ValorActual.Text.Replace(",", ".") + ", "
                    + "'" + tbx_FUltimReval.Text + "', " + tbx_ValorReval.Text.Replace(",", ".") + ", "
                    + "'" + tbx_FUltimaDepReval.Text + "', " + tbx_ValorDepreReval.Text.Replace(",", ".") + ", "
                    + "'" + tbx_FUltimaMejora.Text + "', " + tbx_ValorMejoras.Text.Replace(",", ".") + ", "
                    + "'" + tbx_FUltimDepMejoras.Text + "', " + tbx_ValorDepMejoras.Text.Replace(",", ".") + ", "
                    + "" + tbx_ValorDepAcelerada.Text.Replace(",", ".") + ", " + tbx_ValorDepEspecial.Text.Replace(",", ".") + ", "
                    + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "'"
                    + ")";
                cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                sql = "Insert INTO AFijos.APActivosFijosHistor ("
                    + "CodigoActivo, CentroCostos, "
                    + "CodigoTransaccion, FechaTransaccion, Documento, Secuencial, "
                    + "ValorTran, ValorCompras, "
                    + "ValorMejoras, ValorRevalorizacion, "
                    + "ValorDepreciacionNormal, "
                    + "ValorDepreciacionAcelerada, "
                    + "ValorDepreciacionEspecial, "
                    + "ValorDepreciacionRevalorizacion, "
                    + "ValorDepreciacionMejoras, "
                    + "ValorBaja, "
                    + "SucDestino, UbicacionDestino, DepartamentoDestino, ResponsableDestino, "
                    + "SucOrigen, UbicacionOrigen, DepartamentoOrigen, ResponsableOrigen, "
                    + "UserAlta, FechaCompAlta "
                    + ") "
                    + " VALUES ("
                    + "'" + tbx_CodigoActivo.Text + "', '" + tbx_CentroCostos.Text + "', "
                    + "'" + tbx_RazonIngreso.Text + "', '" + tbx_FechaIngreso.Text + "', 0, 0, "
                    + tbx_ValorInicial.Text.Replace(",", ".") + ", " + tbx_ValorInicial.Text.Replace(",", ".") + ", "
                    + tbx_ValorMejoras.Text.Replace(",", ".") + ", " + tbx_ValorReval.Text.Replace(",", ".") + ", "
                    + tbx_DepreciaNormAcum.Text.Replace(",", ".") + ", "
                    + tbx_ValorDepAcelerada.Text.Replace(",", ".") + ", "
                    + tbx_ValorDepEspecial.Text.Replace(",", ".") + ", "
                    + tbx_ValorDepreReval.Text.Replace(",", ".") + ", "
                    + tbx_ValorDepMejoras.Text.Replace(",", ".") + ", "
                    + "0, "
                    + "'', '', '', '', "
                    + "'', '', '', '', "
                    + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "'"
                    + ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                btn_GeneraHistorico.Visible = true;

            }
            catch (Exception ex)
            {
                MensajeR.Text = "Error al Crear Activo Fijo " + sql + " " + ex.Message;
                Response.Write("<script>window.alert('Error en creacion de Activo ');</script>");

            }
            finally
            {
                cmd.Connection.Close();
            }
        }

    }
    protected void btn_GrabarComprobante_Click(object sender, ImageClickEventArgs e)
    {
        /*
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
        */
    }

    protected string ObtieneSecuenGrupo(string GrupoActivos)
    {
        int NumSecuenGrupo = 0;
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select SecuenGrupo FROM AFijoS.APActivosFijosTGrupos "
                       + "WHERE CodGrupoActivo='" + GrupoActivos + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "GRUPO NO IDENTIFICADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en lectura de Secuencial de Grupo";
            }
            else
            {
                NumSecuenGrupo = Convert.ToInt32(reader.Rows[0][0]) + 1;
                sql = "UPDATE AFijoS.APActivosFijosTGrupos "
                    + "SET SecuenGrupo='" + NumSecuenGrupo + "'"
                    + "WHERE CodGrupoActivo='" + GrupoActivos + "' ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return Convert.ToString(NumSecuenGrupo);
            }
        }
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







    public void CalculaRetenBienes()
    {
        /*   tbx_ValorBienes.Text = tbx_ValorBienes.Text.Replace(".", ",");
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
           */
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
                    return (string.Format("{0:0.00}", reader.Rows[0][0]));
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
            tbx_CodigoActivoL.Text = gv_DatosActivos.Rows[NF].Cells[1].Text;
            ObtieneDatosActivo();
        }
    }

    protected void btn_NuevoComprobante1_Click(object sender, ImageClickEventArgs e)
    {
        tbx_CodigoActivo.Focus();
    }



    protected void btn_GeneraCodigo_Click(object sender, ImageClickEventArgs e)
    {
        if (tbx_CodigoActivo.Text == "" || (tbx_CodigoActivo.Text).Length < 5)
        {
            tbx_CodigoActivo.Text = tbx_GrupoActivos.Text + "-"
                                  + string.Format("{0:00000}", Convert.ToInt32(ObtieneSecuenGrupo(tbx_GrupoActivos.Text)));
        }

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

    protected void btn_NuevoActivo_Click(object sender, ImageClickEventArgs e)
    {
        InicializaParamActivos();
        pnl_DatosBasicosActivo.Enabled = true;
        //       btn_GrabarActivo.Visible = true;
        btn_GeneraCodigo.Visible = true;
        btn_HabilitaModifActivo.Visible = false;
        pnl_GrabaActivo.Visible = true;
        btn_GrabaCambios.Visible = false;
        btn_EliminaActivoFijo.Visible = false;




    }

    protected void btn_DatosActivoF_Click(object sender, EventArgs e)
    {

    }


    protected string LeeActivoFijo(string CodigoActivoL)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                string sql = "Select "
                    + "CentroCostos, CodigoActivo, Categoria, Grupo, "
                    + "NombreDelActivo, Descripcion, Marca, NumDeSerie, "
                    + "CodHabitacion, Ubicacion, Responsable, "
                    + "CodigoIngreso, FechaIngreso, VidaUtil, "
                    + "PorcenResidual, ValorResidual, "
                    + "ValorInicial, FechaUltimaDepreciacion, "
                    + "DepreciacionAcumulada, ValorActual, "
                    + "FechaUltimaRevalorizacion, RevalorizacionAcumulada, "
                    + "FechaUltimaDepreciacionRev, DepreciacionRevAcumulada, "
                    + "FechaUltimaMejora, MejorasAcumuladas, "
                    + "FechaUltimaDeprecMejoras, DepreciacionMejoras, "
                    + "DepreciacionAceleradaAcumulada, DepreciacionEspecialAcumulada "
                    + "From AFijos.APActivosFijos "
                    + " Where CodigoActivo = '" + CodigoActivoL + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    // do something if the query return no rows 
                    // you may insert the relevant redirection you asked for
                    MensajeR.Text = "Activo NO IDENTIFICADO";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                    return "0";
                }
                else
                {
                    // Get the value of Col1 in the 3rd row (0 is the first row)
                    // Col1Value = (int)dt.Rows[2]["Col1"];
                    cmd.Connection.Close();
                    if (!DBNull.Value.Equals(reader.Rows[0][0]))
                    {
                        tbx_CentroCostos.Text = (string)reader.Rows[0][0];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][1]))
                    {
                        tbx_CodigoActivo.Text = (string)reader.Rows[0][1];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][2]))
                    {
                        tbx_Categoria.Text = (string)reader.Rows[0][2];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][3]))
                    {
                        tbx_GrupoActivos.Text = (string)reader.Rows[0][3];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][4]))
                    {
                        tbx_NombreActivo.Text = (string)reader.Rows[0][4];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][5]))
                    {
                        tbx_DescripcionActivo.Text = (string)reader.Rows[0][5];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][6]))
                    {
                        tbx_MarcaActivo.Text = (string)reader.Rows[0][6];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][7]))
                    {
                        tbx_SerieActivo.Text = (string)reader.Rows[0][7];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][8]))
                    {
                        tbx_NumHabitacion.Text = (string)reader.Rows[0][8];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][9]))
                    {
                        tbx_Ubicacion.Text = (string)reader.Rows[0][9];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][10]))
                    {
                        tbx_Responsable.Text = (string)reader.Rows[0][10];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][11]))
                    {
                        tbx_RazonIngreso.Text = (string)reader.Rows[0][11];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][12]))
                    {
                        tbx_FechaIngreso.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][12]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][13]))
                    {
                        tbx_VidaUtil.Text = (string.Format("{0:0}", reader.Rows[0][13]));
                    }


                    if (!DBNull.Value.Equals(reader.Rows[0][14]))
                    {
                        tbx_PorcenValResid.Text = (string.Format("{0:0.00}", reader.Rows[0][14]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][15]))
                    {
                        tbx_ValorResidual.Text = (string.Format("{0:0.00}", reader.Rows[0][15]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][16]))
                    {
                        tbx_ValorInicial.Text = (string.Format("{0:0.00}", reader.Rows[0][16]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][17]))
                    {
                        tbx_FUltimaDeprecia.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][17]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][18]))
                    {
                        tbx_DepreciaNormAcum.Text = (string.Format("{0:0.00}", reader.Rows[0][18]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][19]))
                    {
                        tbx_ValorActual.Text = (string.Format("{0:0.00}", reader.Rows[0][19]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][20]))
                    {
                        tbx_FUltimReval.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][20]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][21]))
                    {
                        tbx_ValorReval.Text = (string.Format("{0:0.00}", reader.Rows[0][21]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][22]))
                    {
                        tbx_FUltimaDepReval.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][22]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][23]))
                    {
                        tbx_ValorDepreReval.Text = (string.Format("{0:0.00}", reader.Rows[0][23]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][24]))
                    {
                        tbx_FUltimaMejora.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][24]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][25]))
                    {
                        tbx_ValorMejoras.Text = (string.Format("{0:0.00}", reader.Rows[0][25]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][26]))
                    {
                        tbx_FUltimDepMejoras.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][26]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][27]))
                    {
                        tbx_ValorDepMejoras.Text = (string.Format("{0:0.00}", reader.Rows[0][27]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][28]))
                    {
                        tbx_ValorDepAcelerada.Text = (string.Format("{0:0.00}", reader.Rows[0][28]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][29]))
                    {
                        tbx_ValorDepEspecial.Text = (string.Format("{0:0.00}", reader.Rows[0][29]));
                    }
                    /*                   

                    + "FechaUltimaMejora, MejorasAcumuladas, "
                    + "FechaUltimaDeprecMejoras, DepreciacionMejoras, "
                    + "DepreciacionAceleradaAcumulada, DepreciacionEspecialAcumulada "
*/
                    return (string.Format("{0:0.00}", reader.Rows[0][0]));


                    // or just make the other redirection from your question
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = "Error en Lectura de Activo Fijo " + ex.Message;
                MensajeR.Visible = true;
                return "0";
            }
        }
    }

    protected void ddl_CentroCostosL_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CentroCostosL.Text = ddl_CentroCostosL.SelectedItem.Value.ToString();

        ddl_CodigoActivoL.Items.Clear();
        ddl_CodigoActivoL.DataSourceID = "";
        ddl_CodigoActivoL.DataSourceID = (string)(ds_CodigoActivos.UniqueID);
        ddl_CodigoActivoL.DataBind();
        ddl_CodigoActivoL.Items.Insert(0, "  Seleccione el Activo que desea procesar....");
        ddl_CodigoActivoL.SelectedIndex = 0;

        /*       
               gv_CuadroXCentroCostos.DataSourceID = "";
               gv_CuadroXCentroCostos.DataSourceID = (string)(ds_ActivosXCentro.UniqueID);
               gv_CuadroXCentroCostos.DataBind();
       */

    }
    protected void ddl_CentroCostosIndiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CodCentroCostosParaCuadro.Text = ddl_CodCentroCostosParaCuadro.SelectedItem.Value.ToString();


        gv_CuadroXCentroCostos.DataSourceID = "";
        gv_CuadroXCentroCostos.DataSourceID = (string)(ds_ActivosXCentro.UniqueID);
        gv_CuadroXCentroCostos.DataBind();

    }

    protected void ProcesaActivoIndiv()
    {
        if (LeeActivoFijo(tbx_CodigoActivoL.Text) != "0")
        {
            pnl_Busqueda.Visible = false;
            pnl_DatosBasicosActivo.Visible = true;
            pnl_DatosBasicosActivo.Enabled = false;
            //            pnl_ControlesMod.Visible = true;

            tbx_CentroCostosL.Text = "";
            //            tbx_CodigoActivoL.Text = "";

            btn_GrabaCambios.Visible = false;
            btn_EliminaActivoFijo.Visible = false;
            btn_HabilitaModifActivo.Visible = true;
            pnl_GrabaActivo.Visible = true;
        }
    }
    protected void ddl_CodigoActivoL_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CodigoActivoL.Text = ddl_CodigoActivoL.SelectedItem.Value.ToString();
        ProcesaActivoIndiv();
    }

    protected void btn_BuscarActivo_Click(object sender, ImageClickEventArgs e)
    {
        PreparaBusquedaActivos();
    }

    protected void PreparaBusquedaActivos()
    {
        InicializaParamActivos();
        pnl_Busqueda.Visible = true;
        pnl_DatosBasicosActivo.Visible = false;
        btn_HabilitaModifActivo.Visible = false;
        btn_GrabaCambios.Visible = false;
        btn_GrabarActivo.Visible = false;
        pnl_GrabaActivo.Visible = false;
        ddl_CentroCostosL.Focus();
    }


    protected void btn_GrabarActivo_Click(object sender, ImageClickEventArgs e)
    {
        GrabaActivo();
    }

    protected void btn_HabilitaIngresoComplementos_Click(object sender, ImageClickEventArgs e)
    {
        EValorDepAcelerada.Visible = true;
        tbx_ValorDepAcelerada.Visible = true;
        EValDepEspecial.Visible = true;
        tbx_ValorDepEspecial.Visible = true;
        pnl_DatosActivosAnter.Visible = true;
    }

    protected void btn_RegistraActivoFijo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btn_HabilitaModifActivo_Click(object sender, ImageClickEventArgs e)
    {
        btn_HabilitaIngresoComplementos.Visible = true;
        pnl_DatosBasicosActivo.Enabled = true;
        btn_GrabaCambios.Visible = true;
        btn_EliminaActivoFijo.Visible = true;
    }

    protected void btn_EliminaActivo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void tbx_PorcenValResid_TextChanged(object sender, EventArgs e)
    {
        tbx_ValorResidual.Text = String.Format("{0:0.00}",
                                         (Convert.ToDecimal(tbx_ValorInicial.Text.Replace(".", ","))
                                             * Convert.ToDecimal(tbx_PorcenValResid.Text.Replace(".", ",")) / 100)
                               );

    }

    protected void btn_GrabaCambios_Click(object sender, ImageClickEventArgs e)
    {
        sql = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {

            sql = "UPDATE AFijos.APActivosFijos SET "
                + "CentroCostos='" + tbx_CentroCostos.Text + "', "
                + "CodigoActivo='" + tbx_CodigoActivo.Text + "', "
                + "Categoria='" + tbx_Categoria.Text + "', "
                + "Grupo='" + tbx_GrupoActivos.Text + "', "
                + "NombreDelActivo='" + tbx_NombreActivo.Text + "', "
                + "Descripcion='" + tbx_DescripcionActivo.Text + "', "
                + "Marca='" + tbx_MarcaActivo.Text + "', "
                + "NumDeSerie='" + tbx_SerieActivo.Text + "', "
                + "CodHabitacion='" + tbx_NumHabitacion.Text + "', "
                + "Ubicacion='" + tbx_Ubicacion.Text + "', "
                + "Responsable='" + tbx_Responsable.Text + "', "
                + "CodigoIngreso='" + tbx_RazonIngreso.Text + "', "
                + "FechaIngreso='" + tbx_FechaIngreso.Text + "', "
                + "VidaUtil='" + tbx_VidaUtil.Text + "', "
                + "PorcenResidual='" + tbx_PorcenValResid.Text.Replace(",", ".") + "', "
                + "ValorResidual='" + tbx_ValorResidual.Text.Replace(",", ".") + "', "
                + "ValorInicial='" + tbx_ValorInicial.Text.Replace(",", ".") + "', "
                + "FechaUltimaDepreciacion='" + tbx_FUltimaDeprecia.Text + "', "
                + "DepreciacionAcumulada='" + tbx_DepreciaNormAcum.Text.Replace(",", ".") + "', "
                + "ValorActual='" + tbx_ValorActual.Text.Replace(",", ".") + "', "
                + "FechaUltimaRevalorizacion='" + tbx_FUltimReval.Text + "', "
                + "RevalorizacionAcumulada='" + tbx_ValorReval.Text.Replace(",", ".") + "', "
                + "FechaUltimaDepreciacionRev='" + tbx_FUltimaDepReval.Text + "', "
                + "DepreciacionRevAcumulada='" + tbx_ValorDepreReval.Text.Replace(",", ".") + "', "
                + "FechaUltimaMejora='" + tbx_FUltimaMejora.Text + "', "
                + "MejorasAcumuladas='" + tbx_ValorMejoras.Text.Replace(",", ".") + "', "
                + "FechaUltimaDeprecMejoras='" + tbx_FUltimDepMejoras.Text + "', "
                + "DepreciacionMejoras='" + tbx_ValorDepMejoras.Text.Replace(",", ".") + "', "
                + "DepreciacionAceleradaAcumulada='" + tbx_ValorDepAcelerada.Text.Replace(",", ".") + "', "
                + "DepreciacionEspecialAcumulada='" + tbx_ValorDepEspecial.Text.Replace(",", ".") + "', "
                + "UserMod='" + (string)(Session["CodUsuarioM"]) + "', "
                + "FechaCompMod='" + DateTime.Now + "' "
                + "WHERE CodigoActivo='" + tbx_CodigoActivoL.Text + "' ";
            cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            //           MensajeR.Text = sql;
            try
            {
                cmd.ExecuteNonQuery();
                MensajeR.Text = "Actulizacion OK";
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.Message + sql;
            }
            finally
            {
                cmd.Connection.Close();
            }

        }
    }

    protected void tbx_CodigoActivo_TextChanged(object sender, EventArgs e)
    {

    }



    protected void btn_EliminaActivoFijo_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Text = "";
        string sql = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "DELETE FROM AFijos.APActivosFijos "
                          + "WHERE ("
                          + "CodigoActivo='" + tbx_CodigoActivoL.Text + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                MensajeR.Text = "PROCESO ELIMINACION OK / " + sql;
                btn_EliminaActivoFijo.Visible = false;
                InicializaParamActivos();
                pnl_Busqueda.Visible = true;
                ddl_CentroCostosL.Focus();
            }
            catch (Exception ex)
            {
                MensajeR.Visible = true;
                MensajeR.Text = "PROCESO DE ELIMINACION ERRADO / " + ex.Message;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }


    protected void ddl_CodCentroCostosParaCuadro_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_CodCentroCostosParaCuadro.Text = ddl_CodCentroCostosParaCuadro.SelectedItem.Value.ToString();
        gv_CuadroXCentroCostos.DataSourceID = "";
        gv_CuadroXCentroCostos.DataSourceID = (string)(ds_ActivosXCentro.UniqueID);
        gv_CuadroXCentroCostos.DataBind();
    }

    protected void btn_VerificaDatosAd_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void tbx_SerieActivo_TextChanged(object sender, EventArgs e)
    {

    }



    protected void btn_VerificaDatosIngres_Click(object sender, ImageClickEventArgs e)
    {
        btn_GrabarActivo.Visible = true;
    }



    protected void gv_CuadroXCentroCostos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int NF = gv_CuadroXCentroCostos.SelectedIndex;
        if ((string.IsNullOrEmpty(gv_CuadroXCentroCostos.Rows[NF].Cells[1].Text))
            || (gv_CuadroXCentroCostos.Rows[NF].Cells[1].Text == "&nbsp;"))
        {
            MensajeR.Text = NF + "Error en Seleccion de Activo.... ";
        }
        else
        {
            MensajeR.Text = NF + "Transaccion Procesada correctamente ";
            tbx_CodigoActivoL.Text = gv_CuadroXCentroCostos.Rows[NF].Cells[1].Text;
            ObtieneDatosActivo();
        }
    }
    protected void ObtieneDatosActivo()
    {

        LeeActivoFijo(tbx_CodigoActivoL.Text);
        pnl_DatosBasicosActivo.Visible = true;
        pnl_DatosBasicosActivo.Enabled = false;
        //            pnl_ControlesMod.Visible = true;

        tbx_CentroCostosL.Text = "";
        //            tbx_CodigoActivoL.Text = "";

        btn_GrabaCambios.Visible = false;
        btn_EliminaActivoFijo.Visible = false;
        btn_HabilitaModifActivo.Visible = true;
        pnl_GrabaActivo.Visible = true;

        ECodigoActivo.Visible = true;
        tbx_CodigoActivo.Visible = true;
        mv_IngresoActivosFijos.SetActiveView(v_DatosActivoF);
    }

    protected void btn_DatosIndivActivosG_Click(object sender, EventArgs e)
    {
        PreparaBusquedaActivos();
    }

    protected void btn_DatosIndividualesC_Click(object sender, EventArgs e)
    {
        PreparaBusquedaActivos();
    }

    protected void tbx_FechaIngreso_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btn_BuscarActivoIndiv_Click(object sender, ImageClickEventArgs e)
    {
        ProcesaActivoIndiv();
    }
}






























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































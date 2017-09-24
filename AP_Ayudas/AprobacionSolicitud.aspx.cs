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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
public partial class AprobacionSolicitud : System.Web.UI.Page
{
    static string CodServicio, sql;
    static decimal MontoSupGastosP, MontoMaxAyudaP, PorcenInferP, PorcenSuperP, MontoExterAyudaP;
    // static decimal MontoAprobadoD;
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    static DateTime FechaConvertida;
    static decimal MontoD, PorcentajeD, MCalculadoD;
    static decimal ProformasD, PorcentajeProformasD, MCalculadoProformasD;
    static decimal DonacionD, MontoAyuda;
    //   static decimal PorcentajeDonacionD;
    static decimal PorcentajeAyudaD, TotalCalculadoD;
    static int SecuenProceso = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        OtroSocio.Focus();
        if (!IsPostBack)
        {
            LeeParamAyudas();
            OcultaSolicitud();
        }
    }
    protected void InicializaDatos()
    {
        MensajeR.Text = "";
        NSolicitud.Text = "";
        Cedula.Text = "";
        Nombres.Text = "";
        Servicio.Text = "";
        Situacion.Text = "";
        FechaAfiliacion.Text = "";
        FechaSolicitud.Text = "";
        MontoA.Text = "0";
        MontoA0.Text = "0";
        MontoA1.Text = "0";
        MontoA2.Text = "0";
        DEspecialidad.Dispose();
        DEspecialidad.DataBind();
        Especialidad.Text = "";
        DCasaSalud.Dispose();
        DCasaSalud.DataBind();
        CasaDeSalud.Text = "";
        MedicoQueCertifica.Text = "";
        btn_AgregarDiagnosticos.Visible = false;

        OcultaSolicitud();
        RegistrarProceso.Visible = false;
        btn_ImprimeAprobacion.Visible = false;
        btn_ProcesarInforme.Visible = false;
    }
    protected void InicializaModificaciones()
    {
        EMontoA.Visible = false;
        MontoA.Visible = false;
        //       VerificaDatos.Visible = false;
    }
    protected void OtroSocio_Click(object sender, ImageClickEventArgs e)
    {
        Cedula.Enabled = true;
        InicializaDatos();
        OcultaSolicitud();
        NSolicitud.Enabled = true;
        NSolicitud.Focus();
        ActivaVerifAprob.Visible = false;
    }
    public string LeeServicio(String CodServicio)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select DetalleServicio From ASocios.APSociosTServicio "
                         + " Where CodigoServicio = '" + CodServicio + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "SERVICIO NO IDENTIFICADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Servicio";
            }
            else
            {
                cmd.Connection.Close();
                return (string)reader.Rows[0][0];
            }
        }
    }
    public string LeeDetalleTipoCuentaIF(string TipoCuenta)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select DetalleTipoCuenta From ASocios.APSociosTIFISTipoCuenta "
                         + " Where CodigoTipoCuenta = '" + TipoCuenta + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "SERVICIO NO IDENTIFICADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Servicio";
            }
            else
            {
                cmd.Connection.Close();
                return (string)reader.Rows[0][0];
            }
        }
    }
    public string LeeSituacion(String CodSituacion)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select DetalleSituacion From ASocios.APSociosTSituacion "
                         + " Where CodigoSituacion = '" + CodSituacion + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodSituacion + "SITUACION NO IDENTIFICADA";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Situacion";
            }
            else
            {
                cmd.Connection.Close();
                return (string)reader.Rows[0][0];
            }
        }
    }

    protected void BuscarSocios_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select Identificacion, TipoSocio, "
                                + "Apellidos, Nombres, "
                                + "FechaAfil, Email, "
                                + "SituacionSocio, "
                                + "TipoIF, NombreIF, Codigo_BCE, "
                                + "TipoCuentaIF, NumCuenta, "
                                + "TelefonoConv, TelefonoCel, FechaDesafil "
                                + "From ASocios.APSocios  Where Identificacion = '"
                                + Cedula.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                // do something if the query return no rows
                // you may insert the relevant redirection you asked for
                MensajeR.Text = "CEDULA  NO REGISTRADA, BUSCAR POR NUMERO DE SOLICITUD";
                MensajeR.Visible = true;
                NSolicitud.Visible = true;
                //               EBuscaSolicitud.Visible = true;
                BuscarSolicitud.Visible = true;
                NSolicitud.Enabled = true;
                NSolicitud.Focus();

            }
            else
            {
                // Get the value of Col1 in the 3rd row (0 is the first row)
                // Col1Value = (int)dt.Rows[2]["Col1"];
                CodServicio = (string)reader.Rows[0][1];
                Servicio.Text = (string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                Servicio.Visible = true;
                Nombres.Text = (string)reader.Rows[0][2] + " " + (string)reader.Rows[0][3];
                FechaAfiliacion.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][4]));
                Situacion.Text = LeeSituacion((string)reader.Rows[0][6]);

                if ((string)reader.Rows[0][6] == "D")
                {
                    Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][14]));
                    RegistrarProceso.Enabled = false;
                    OtroSocio.Focus();
                }
                else
                {
                    FechaSolicitud.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
                    RegistrarProceso.Enabled = true;
                    FechaSolicitud.Focus();
                }
            }
            cmd.Connection.Close();
        }


    }
    protected void LeeSolAyuda()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int SolicitudNum;
            SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
            string sql = "Select Grado, Unidad, "
                    + "TelefonosRef, DocRecibidos, "
                    + "MontoDeGastos, PorcentajeAyuda, ValorSolicitado, "
                    + "Especialidad, CasaSalud, MedicoCertifica, "
                    + "AServ, ObservSolicitud, OtroBeneficiario, "
                    + "FechaSolicitud, TipoAyuda, RazonSolicitud, "
                    + "TipoBeneficiario, NombreBeneficiario, "
                    + "ValorAprobado, TotalPagado, "
                    + "ValorAprobado0, Beneficiario0, "
                    + "ValorAprobado1, Beneficiario1, "
                    + "ValorAprobado2, Beneficiario2, "
                    + "FechaAprobRechaz, ReferAprobRechaz, Identificacion, "
                    + "MontoFacturas, MontoProformas, MontoDonaciones, PorcentajeProformas, PorcentajeFacturas "
                    + "From ASocios.APSociosAyudas  Where NumSolicitud = "
                    + SolicitudNum;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    // do something if the query return no rows
                    // you may insert the relevant redirection you asked for
                    MensajeR.Text = "SOLICITUD NO REGISTRADA";
                    MensajeR.Visible = true;
                }
                else
                {
                    // Get the value of Col1 in the 3rd row (0 is the first row)
                    // Col1Value = (int)dt.Rows[2]["Col1"];

                    Monto.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    Porcentaje.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    MCalculado.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    MontoT.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    MontoP0.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    MontoP1.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    MontoP2.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    if (!DBNull.Value.Equals(reader.Rows[0][0]))
                    {
                        Grado.Text = (string)reader.Rows[0][0];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][1]))
                    {
                        Unidad.Text = (string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][10]))
                    {
                        TiempoServicio.Text = (string.Format("{0:0}", reader.Rows[0][10]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][3]))
                    {
                        DocumentosRecibidos.Text = (string)reader.Rows[0][3];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][29]))
                    {
                        Monto.Text = (string.Format("{0:0.00}", reader.Rows[0][29]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][30]))
                    {
                        Proformas.Text = (string.Format("{0:0.00}", reader.Rows[0][30]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][31]))
                    {
                        Donacion.Text = (string.Format("{0:0.00}", reader.Rows[0][31]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][5]))
                    {
                        PorcentajeAyuda.Text = (string.Format("{0:0}", reader.Rows[0][5]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][6]))
                    {
                        MCalculado.Text = (string.Format("{0:0.00}", reader.Rows[0][6]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][7]))
                    {
                        Especialidad.Text = (string)reader.Rows[0][7];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][8]))
                    {
                        CasaDeSalud.Text = (string)reader.Rows[0][8];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][9]))
                    {
                        MedicoQueCertifica.Text = (string)reader.Rows[0][9];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][10]))
                    {
                        TiempoServicio.Text = (string.Format("{0:0}", reader.Rows[0][10]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][11]))
                    {
                        ObservSolicitud.Text = (string)reader.Rows[0][11];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][12]))
                    {
                        OtroBeneficiario.Text = (string)reader.Rows[0][12];
                    }
                    //              +"FechaSolicitud, TipoAyuda, RazonSolicitud, "
                    //              + "TipoBeneficiario, NombreBeneficiario, "
                    //              + "ValorAprobado, TotalPagado "
                    if (!DBNull.Value.Equals(reader.Rows[0][13]))
                    {
                        FechaSolicitud.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][13]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][14]))
                    {
                        TipoAyuda.Text = (string)reader.Rows[0][14];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][15]))
                    {
                        RazonSolicitud.Text = (string)reader.Rows[0][15];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][16]))
                    {
                        TipoBeneficiario.Text = (string)reader.Rows[0][16];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][17]))
                    {
                        NombreBeneficiario.Text = (string)reader.Rows[0][17];
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][18]))
                    {
                        MontoT.Text = (string.Format("{0:0.00}", reader.Rows[0][18]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][20]))
                    {
                        MontoP0.Text = (string.Format("{0:0.00}", reader.Rows[0][20]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][21]))
                    {
                        BeneficiarioP0.Text = (string)reader.Rows[0][21];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][22]))
                    {
                        MontoP1.Text = (string.Format("{0:0.00}", reader.Rows[0][22]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][23]))
                    {
                        BeneficiarioP1.Text = (string)reader.Rows[0][23];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][24]))
                    {
                        MontoP2.Text = (string.Format("{0:0.00}", reader.Rows[0][24]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][25]))
                    {
                        BeneficiarioP2.Text = (string)reader.Rows[0][25];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][26]))
                    {
                        FechaAprobRechaz.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][26]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][27]))
                    {
                        ReferAprobRechaz.Text = (string)reader.Rows[0][27];
                    }

                    Cedula.Text = (string)reader.Rows[0][28];

                    if (!DBNull.Value.Equals(reader.Rows[0][32]))
                    {
                        PorcentajeP.Text = (string.Format("{0:0.00}", reader.Rows[0][32]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][32]))
                    {
                        Porcentaje.Text = (string.Format("{0:0.00}", reader.Rows[0][33]));
                    }

                    //                 if (!DBNull.Value.Equals(reader.Rows[0][30]))
                    //                 {
                    //                     Proformas.Text = (string)reader.Rows[0][30];
                    //                 }

                    DesplegaAR();
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();
            }
            cmd.Connection.Close();
        }
    }
    private void OcultaDatGenMod()
    {
        DRazonRechazo.Visible = false;
        EFechaResultado.Visible = false;
        FechaResultado.Visible = false;
        Recomendacion.Visible = false;
        EObservacion.Visible = false;
        VerificaDatos.Visible = false;
        ObservacionProceso.Visible = false;
    }
    private void DesplegaDatGenMod()
    {
        if (Aprobado.Checked)
        {
            Recomendacion.Visible = true;
            Recomendacion.Text = "";
        }
        else
        {
            if (Rechazado.Checked)
            {
                DRazonRechazo.Visible = true;
                DRazonRechazo.ClearSelection();
                RazonRechazo.Visible = true;
                EDRechazo.Visible = true;
            }
        }

        Aprobado.Visible = true;
        Rechazado.Visible = true;
        //      ActivaAprobacion.Visible = true;
        //      ActivaRechazo.Visible = true;
        EFechaResultado.Visible = true;
        FechaResultado.Visible = true;
        ObservacionProceso.Text = "";
        EObservacion.Visible = true;
        ObservacionProceso.Visible = true;
        VerificaDatos.Visible = true;
    }
    private void GrabaTramite()
    {
        SecuenProceso += 1;
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Insert INTO ASocios.APSociosAyudasProceso "
                        + "(Identificacion, NumSolicitud, "
                        + "CodProceso, FechaProceso, "
                        + "Secuencial, Proceso,"
                        + "FechaProxProb, ResultadoProceso, "
                        + "Observacion, "
                        + "UserProceso, FechaCompProceso )"
                        + " VALUES ('" + Cedula.Text + "', '" + NumSolicitud.Text + "', "
                        + "'" + 390 + "', '" + (string.Format("{0:dd/MM/yyyy}", DateTime.Now)) + "', "
                        + "'" + SecuenProceso + "', '" + "Aprobacion" + "', "
                        + "'" + FechaResultado.Text + "', '" + Recomendacion.Text + "', "
                        + "'" + ObservacionProceso.Text + "', "
                        + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();

                MontoD = Convert.ToDecimal(MCalculado.Text);
                MCalculado.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);

                PorcentajeAyudaD = Convert.ToDecimal(Porcentaje.Text);
                Porcentaje.Text = PorcentajeAyudaD.ToString("0", CultureInfo.InvariantCulture);

                MontoD = Convert.ToDecimal(MontoA.Text);
                MontoA.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);
                MontoD = Convert.ToDecimal(MontoA0.Text);
                MontoA0.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);
                MontoD = Convert.ToDecimal(MontoA1.Text);
                MontoA1.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);
                MontoD = Convert.ToDecimal(MontoA2.Text);
                MontoA2.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);

                sql = "UPDATE ASocios.APSociosAyudas "
                        + "SET MontoDeGastos=" + Monto.Text
                        + ", PorcentajeAyuda=" + PorcentajeAyuda.Text
                        + ", PorcentajeFacturas=" + Porcentaje.Text
                        + ", PorcentajeProformas=" + PorcentajeP.Text
                        + ", ValorSolicitado=" + MontoA.Text
                        + ", Especialidad='" + Especialidad.Text + "' "
                        + ", CasaSalud='" + CasaDeSalud.Text + "' "
                        + ", MedicoCertifica='" + MedicoQueCertifica.Text + "' "
                        + ", ValorAprobado =" + MontoA.Text
                        + ", ValorAprobado0=" + MontoA0.Text
                        + ", Beneficiario0='" + Beneficiario0.Text + "' "
                        + ", ValorAprobado1=" + MontoA1.Text
                        + ", Beneficiario1='" + Beneficiario1.Text + "' "
                        + ", ValorAprobado2=" + MontoA2.Text
                        + ", Beneficiario2='" + Beneficiario2.Text + "' "
                        + ", FechaAprobRechaz='" + FechaResultado.Text + "' "
                        + ", ReferAprobRechaz='" + Recomendacion.Text + "' "
                        + ", ObservAR='" + ObservacionProceso.Text + "' "
                        + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
                cmd = new SqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.ToString();
                }
                btn_ImprimeAprobacion.Visible = true;
                RegistrarProceso.Visible = false;
                DesplegaDatGenMod();
                MensajeR.Visible = true;
                SolicitudesDeAyuda.DataSourceID = "";
                SolicitudesDeAyuda.DataSourceID = (string)(TabSolicitudesAyuda.UniqueID);
                LeeSolAyuda();
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();
            }
            cmd.Connection.Close();
        }
    }
    private void DesplegaAR()
    {
        Aprobado.Visible = true;
        //        ActivaAprobacion.Visible = true;
        Rechazado.Visible = true;
        //        ActivaRechazo.Visible = true;
        EFechaResultado.Visible = true;
        FechaResultado.Visible = true;
        EObservacion.Visible = true;
        ObservacionProceso.Visible = true;
        VerificaDatos.Visible = true;
    }
    private void OcultaAR()
    {
        Aprobado.Visible = false;
        //        ActivaAprobacion.Visible = false;
        Rechazado.Visible = false;
        //        ActivaRechazo.Visible = false;
        EFechaResultado.Visible = false;
        FechaResultado.Visible = false;
        EObservacion.Visible = false;
        ObservacionProceso.Visible = false;
        VerificaDatos.Visible = false;
    }
    protected void RegistrarProceso_Click(object sender, EventArgs e)
    {
        if (FechaResultado.Text == "")
        {
            MensajeR.Text = "Error en Fecha de Aprobación";
            FechaResultado.Focus();
        }
        else
        {
            GrabaTramite();
            //            OcultaDatGenMod();
            btn_ImprimeAprobacion.Visible = true;
            btn_ProcesarInforme.Visible = true;
        }


        //       OcultaCamposAModificar();
        //       MensajeR.Text = "Proceso Aprobacion Registrado CORRECTAMENTE";

    }
    protected void ProcesaValores()
    {
        MontoA.Text = MontoA.Text.Replace(".", ",");
        MontoA0.Text = MontoA0.Text.Replace(".", ",");
        MontoA1.Text = MontoA1.Text.Replace(".", ",");
        MontoA2.Text = MontoA2.Text.Replace(".", ",");
        MCalculado.Text = MCalculado.Text.Replace(".", ",");

        MensajeR.Text = "Error en ";
        //      MensajeR.Visible = false;
        if (Aprobado.Checked)
        {
            if (DEspecialidad.SelectedItem.Text != "-----")
            {
                Especialidad.Text = DEspecialidad.SelectedItem.Text;
            }
            if (DCasaSalud.SelectedItem.Text != "-----")
            {
                CasaDeSalud.Text = DCasaSalud.SelectedItem.Text;
            }
            if (MontoA.Text == "")
            {
                MontoA.Focus();
                MensajeR.Text = MensajeR.Text + "Error en Monto Aprobado";
                MensajeR.Visible = true;
            }
            else
            {

                if (Beneficiario0.Text == "")
                {
                    Beneficiario0.Focus();
                    MensajeR.Text = MensajeR.Text + "Error en Beneficiario";
                    MensajeR.Visible = true;
                }
                else
                {
                    if (MontoA0.Text == "")
                    {
                        MontoA.Focus();
                        MensajeR.Text = MensajeR.Text + "Error en Monto a Pagar";
                        MensajeR.Visible = true;
                    }
                    else
                    {

                        decimal MontoT = Convert.ToDecimal(MontoA0.Text)
                                       + Convert.ToDecimal(MontoA1.Text)
                                       + Convert.ToDecimal(MontoA2.Text);

                        if (MontoT != Convert.ToDecimal(MontoA.Text))
                        {
                            MontoA.Focus();
                            MensajeR.Text = MensajeR.Text + "Valores no cuadran";
                            MensajeR.Visible = true;
                        }
                        else
                        {
                            MontoA.Text = MontoA.Text.Replace(".", ",");
                            MontoA0.Text = MontoA0.Text.Replace(".", ",");
                            MontoA1.Text = MontoA1.Text.Replace(".", ",");
                            MontoA2.Text = MontoA2.Text.Replace(".", ",");

                            MontoA.Text = (string.Format("{0:#,##0.00}", Convert.ToDecimal(MontoA.Text)));
                            MontoA0.Text = (string.Format("{0:#,##0.00}", Convert.ToDecimal(MontoA0.Text)));
                            MontoA1.Text = (string.Format("{0:#,##0.00}", Convert.ToDecimal(MontoA1.Text)));
                            MontoA2.Text = (string.Format("{0:#,##0.00}", Convert.ToDecimal(MontoA2.Text)));
                            RegistrarProceso.Visible = true;
                        }
                    }
                }
            }
        }
    }
    protected void VerificaDatos_Click(object sender, ImageClickEventArgs e)
    {
        if (FechaResultado.Text != "")
        {
            try
            {
                FechaConvertida = Convert.ToDateTime(FechaResultado.Text); ;
                FechaResultado.Text = FechaConvertida.ToString("dd/MM/yyyy");
                ProcesaValores();
            }
            catch (Exception ex)
            {
                MensajeR.Text = "Error en Fecha de registro " + ex.Message;
                FechaResultado.Focus();
            }
        }
        else
        {
            MensajeR.Text = "Error en Fecha de registro ";
            FechaResultado.Focus();
        }
    }

    protected void ActivaRechazo_Click(object sender, ImageClickEventArgs e)
    {
        if (Rechazado.Checked)
        {
            EDRechazo.Visible = true;
            DRazonRechazo.Visible = true;
            Rechazado.Visible = true;
            RazonRechazo.Visible = true;
//            ActivaRechazo.Visible = true;

//            ActivaAprobacion.Visible = false;
            Aprobado.Visible = false;
            EMontoA.Visible = false;
            MontoA.Visible = false;
            EBeneficiario0.Visible = false;
            Beneficiario0.Visible = false;
            EBeneficiario1.Visible = false;
            Beneficiario1.Visible = false;
            EBeneficiario2.Visible = false;
            Beneficiario2.Visible = false;
            ERecomendacion.Visible = false;
            Recomendacion.Visible = false;
            FechaResultado.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
            ObservacionProceso.Text = "NEGADA ";

        }
        else
        {

            EDRechazo.Visible = false;
            DRazonRechazo.Visible = false;
            Rechazado.Visible = false;
            RazonRechazo.Visible = false;
 //           ActivaRechazo.Visible = false;
            Aprobado.Visible = true;
 //           ActivaAprobacion.Visible = true;
        }
    }

    protected void btn_ImprimeAprobacion_Click(object sender, EventArgs e)
    {
        Session["NumSolicitud"] = NumSolicitud.Text;
        Response.Write("<script>window.open('VisorSolicitudAprobada.aspx');</script>");
 //       ImpresionAprobacion();
 //       CierraReporte.Visible = true;
    }

    protected void ActivaVerifAprob_Click(object sender, ImageClickEventArgs e)
    {
        DesplegaSolicitud();
 //       DesplegaAR();
    }
    protected void OcultaSolicitud()
    {
        ENumSolicitud.Visible = false;
        NumSolicitud.Visible = false;
        EFechaSolicitud.Visible = false;
        FechaSolicitud.Visible = false;
        EUnidad.Visible = false;
        DUnidad.Visible = false;
        Unidad.Visible = false;
        EGrado.Visible = false;
        DTipoGrado.Visible = false;
        Grado.Visible = false;
        EAserv.Visible = false;
        TiempoServicio.Visible = false;
        ETipoAyuda.Visible = false;
        DTipoAyuda.Visible = false;
        TipoAyuda.Visible = false;
        EDiagnostico.Visible = false;
        DRazonSolicitud.Visible = false;
        RazonSolicitud.Visible = false;
        EEspecialidad.Visible = false;
        DEspecialidad.Visible = false;
        Especialidad.Visible = false;
        ECasaSalud.Visible = false;
        DCasaSalud.Visible = false;
        CasaDeSalud.Visible = false;
        EMedicoCertifica.Visible = false;
        MedicoQueCertifica.Visible = false;
        EBeneficiario.Visible = false;
        DTipoBeneficiario.Visible = false;
        TipoBeneficiario.Visible = false;
        NombreBeneficiario.Visible = false;
        OtroBeneficiario.Visible = false;
        EMonto.Visible = false;
        Monto.Visible = false;
        EPorcentaje.Visible = false;
        Porcentaje.Visible = false;
        MCalculado.Visible = false;
        EDocumentos.Visible = false;
        DocumentosRecibidos.Visible = false;
        EObservSolicitud.Visible = false;
        ObservSolicitud.Visible = false;
        EAprobacion.Visible = false;
        MontoT.Visible = false;
        MontoP0.Visible = false;
        BeneficiarioP0.Visible = false;
        MontoP1.Visible = false;
        BeneficiarioP1.Visible = false;
        MontoP2.Visible = false;
        BeneficiarioP2.Visible = false;
        FechaAprobRechaz.Visible = false;
        ReferAprobRechaz.Visible = false;
        btn_ModificaBasicos.Visible = false;
        FechaAprobRechaz.Visible = false;
        ReferAprobRechaz.Visible = false;
        CalculaValores.Visible = false;
        EReferAprob.Visible = false;
  //      btn_ModificaSolicitud.Visible = false;
        btn_ActualizaBasicos.Visible = false;
        btn_HabilitaAprobacion.Visible = false;
        btn_AgregarEspecialidad.Visible = false;
        MontoA.Visible = false;
        MontoA0.Visible = false;
        MontoA1.Visible = false;
        MontoA2.Visible = false;
        Beneficiario0.Visible = false;
        Beneficiario1.Visible = false;
        Beneficiario2.Visible = false;
        EObservacion.Visible = false;
        EMontoA.Visible = false;
        EBeneficiario0.Visible = false;
        EBeneficiario2.Visible = false;
        EBeneficiario1.Visible = false;
        ERecomendacion.Visible = false;
        Recomendacion.Visible = false;
        btn_ImprimeAprobacion.Visible = false;
        btn_ProcesaGastos.Visible = false;
        btn_ProcesarFacturas.Visible = false;
        btn_ActualizaSolicitud.Visible = false;
        EProformas.Visible = false;
        Proformas.Visible = false;
        EDonacion.Visible = false;
        Donacion.Visible = false;
        dg_ValorFacturas.Visible = false;
        btn_HabilitaProcesoFacturas.Visible = false;
        btn_DeshabilitaProcesoFacturas.Visible = false;
        EFactura.Visible = false;
        SecuenFacturas.Visible = false;
        EVFactura.Visible = false;
        VFactura.Visible = false;
        EObservFactura.Visible = false;
        ObservFactura.Visible = false;
        btn_AgregarFactura.Visible = false;
        btn_EliminarFactura.Visible = false;
        btn_TerminarIngresoFacturas.Visible = false;
        btn_TerminarIngresoFacturas0.Visible = false;

        ETFacturas.Visible = false;
        TFacturas.Visible = false;
        ETFacturas0.Visible = false;
        TFacturas0.Visible = false;
        EPorcentajeP.Visible = false;
        PorcentajeP.Visible = false;
        MCalculadoP.Visible = false;
        PorcentajeAyuda.Visible = false;

        OcultaAR();
    }
    protected void DesplegaSolicitud()
    {
        ENumSolicitud.Visible = true;
        NumSolicitud.Visible = true;
        EFechaSolicitud.Visible = true;
        FechaSolicitud.Visible = true;
        EUnidad.Visible = true;
        Unidad.Visible = true;
        EGrado.Visible = true;
        Grado.Visible = true;
        EAserv.Visible = true;
        TiempoServicio.Visible = true;
        ETipoAyuda.Visible = true;
        TipoAyuda.Visible = true;
        EDiagnostico.Visible = true;
        RazonSolicitud.Visible = true;
        DEspecialidad.Visible = true;
        EEspecialidad.Visible = true;
        Especialidad.Visible = true;
        ECasaSalud.Visible = true;
        DCasaSalud.Visible = true;
        CasaDeSalud.Visible = true;
        EMedicoCertifica.Visible = true;
        MedicoQueCertifica.Visible = true;
        EBeneficiario.Visible = true;
        TipoBeneficiario.Visible = true;
        NombreBeneficiario.Visible = true;
        OtroBeneficiario.Visible = true;
        EMonto.Visible = true;
        Monto.Visible = true;
        EProformas.Visible = true;
        Proformas.Visible = true;
        EDonacion.Visible = true;
        Donacion.Visible = true;
        //        EPorcentaje.Visible = true;
        //        Porcentaje.Visible = true;
        //        EMCalculado.Visible = true;
        //        MCalculado.Visible = true;
        EDocumentos.Visible = true;
        DocumentosRecibidos.Visible = true;
        ReferAprobRechaz.Visible = true;
        CalculaValores.Visible = true;
        EReferAprob.Visible = true;
        FechaAprobRechaz.Visible = true;
        //       EObservacion.Visible = true;
        EObservSolicitud.Visible = true;
        ObservSolicitud.Visible = true;
 //       ObservProceso.Visible = true;
        EAprobacion.Visible = true;
        MontoT.Visible = true;
        MontoP0.Visible = true;
        BeneficiarioP0.Visible = true;
        MontoP1.Visible = true;
        BeneficiarioP1.Visible = true;
        MontoP2.Visible = true;
        BeneficiarioP2.Visible = true;
//        btn_ModificaSolicitud.Visible = true;
        btn_ModificaBasicos.Visible = true;
//        btn_HabilitaAprobacion.Visible = true;
        btn_AgregarEspecialidad.Visible = true;
        btn_ProcesarFacturas.Visible = true;
        btn_ActualizaSolicitud.Visible = true;
        //        btn_HabilitaProcesoFacturas.Visible = true;
        btn_ProcesaGastos.Visible = true;

    }
    protected void btn_ModificaSolicitud_Click(object sender, ImageClickEventArgs e)
    {

        DEspecialidad.Visible = true;
        DCasaSalud.Visible = true;
        MedicoQueCertifica.Enabled = true;
        Monto.Enabled = true;
        MedicoQueCertifica.BackColor = System.Drawing.Color.White;
        NombreBeneficiario.BackColor = System.Drawing.Color.White;
        Monto.BackColor = System.Drawing.Color.White;
        btn_ActualizaBasicos.Visible = true;
    }

    protected void btn_ActualizaSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE ASocios.APSociosAyudas "
                    + "SET Especialidad='" + Especialidad.Text + "', "
                    + "CasaSalud='" + CasaDeSalud.Text + "', "
                    + "MedicoCertifica='" + MedicoQueCertifica.Text + "' "
                    + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
    protected void btn_HabilitaAprobacion_Click(object sender, ImageClickEventArgs e)
    {
        DesplegaAR();
        InicializaModificaciones();
        DesplegaDatGenMod();
        Aprobado.Focus();
 //       btn_ImprimeAprobacion.Visible = true;

    }

    protected void btn_ModificaBasicos_Click(object sender, ImageClickEventArgs e)
    {
        RazonSolicitud.Enabled = true;
        DTipoBeneficiario.Visible = true;
        DUnidad.Visible = true;
        DTipoGrado.Visible = true;
        DTipoAyuda.Visible = true;
        DRazonSolicitud.Visible = true;
        FechaSolicitud.Enabled = true;
        TiempoServicio.Enabled = true;
        FechaSolicitud.BackColor = System.Drawing.Color.White;
        TiempoServicio.BackColor = System.Drawing.Color.White;
        NombreBeneficiario.Enabled = true;
        OtroBeneficiario.Enabled = true;
        DocumentosRecibidos.Enabled = true;
        EObservSolicitud.Visible = true;
        ObservSolicitud.Enabled = true;
        DocumentosRecibidos.BackColor = System.Drawing.Color.White;
        ObservSolicitud.BackColor = System.Drawing.Color.White;
        OtroBeneficiario.BackColor = System.Drawing.Color.White;
        btn_ActualizaBasicos.Visible = true;
        btn_AgregarDiagnosticos.Visible = true;
    }

    public void LeeParamAyudas()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select MontoSupGastos, MontoMaximoAyuda, PorcenSuper, PorcenInfer, MontoExterAyuda From ASeguridad.DatosEmpresa ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "EMPRESA NO IDENTIFICADA";
                MensajeR.Visible = true;
                cmd.Connection.Close();
            }
            else
            {
                cmd.Connection.Close();
                MontoSupGastosP = Convert.ToDecimal(reader.Rows[0][0]);
                MontoMaxAyudaP = Convert.ToDecimal(reader.Rows[0][1]);
                PorcenSuperP = Convert.ToDecimal(reader.Rows[0][2]);
                PorcenInferP = Convert.ToDecimal(reader.Rows[0][3]);
                MontoExterAyudaP = Convert.ToDecimal(reader.Rows[0][4]);
            }
            return;
        }
    }

    protected void CalculaValores_Click(object sender, ImageClickEventArgs e)
    {
 //       btn_HabilitaAprobacion.Visible = true;
        Monto.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        Porcentaje.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MCalculado.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        Proformas.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        PorcentajeP.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MCalculadoP.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        Donacion.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        //       TGeneral.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        PorcentajeD = 0;
        PorcentajeProformasD = 0;
        MCalculadoProformasD = 0;
        MCalculadoD = 0;
         
        Monto.Text = Monto.Text.Replace(".", ",");
        Proformas.Text = Proformas.Text.Replace(".", ",");
        Donacion.Text = Donacion.Text.Replace(".", ",");

        MontoD = System.Convert.ToDecimal(Monto.Text);
        ProformasD = System.Convert.ToDecimal(Proformas.Text);
        DonacionD = System.Convert.ToDecimal(Donacion.Text);

        if (MontoD <= 0 && ProformasD <= 0 && DonacionD <= 0)
        {
            MensajeR.Text = "Monto de Gastos no puede ser 0";
            Monto.Focus();
        }
        else
        {
            if (MontoD<=0)
            {
                PorcentajeD = 0;
            }
            else
            { 
                if ((MontoD) <= MontoSupGastosP)
                {
                    PorcentajeD = PorcenSuperP;
                }
                else
                {
                    PorcentajeD = PorcenInferP;
                }
                MCalculadoD = MontoD * PorcentajeD / 100;
            }

            if (ProformasD <= 0)
            {
                PorcentajeProformasD = 0;
            }
            else
            {
                if ((ProformasD) <= MontoSupGastosP)
                {
                    PorcentajeProformasD = PorcenSuperP;
                }
                else
                {
                    PorcentajeProformasD = PorcenInferP;
                }
                MCalculadoProformasD = ProformasD * PorcentajeProformasD / 100;
            }

            if (MCalculadoD > MontoMaxAyudaP)
            {
                MCalculadoD = MontoMaxAyudaP;
                MCalculadoProformasD = 0;
                DonacionD = 0M;
            }
            else
            {
                if((MCalculadoD+MCalculadoProformasD) > MontoMaxAyudaP)
                {
                    MCalculadoProformasD = MontoMaxAyudaP - MCalculadoD;
                    DonacionD = 0;
                }
                else
                {
                    if ((MCalculadoD + MCalculadoProformasD+DonacionD) > MontoMaxAyudaP)
                    {
                        DonacionD = MontoMaxAyudaP -MCalculadoProformasD- MCalculadoD;
                    }
                }
            }

            TotalCalculadoD = MCalculadoD + MCalculadoProformasD + DonacionD;

            if (PorcentajeProformasD > PorcentajeD)
            {
                PorcentajeAyudaD = PorcentajeProformasD;
            }
            else
            {
                PorcentajeAyudaD = PorcentajeD;
            }

            if ((MCalculadoD + MCalculadoProformasD) >= MontoMaxAyudaP)
            {
                if (TipoAyuda.Text == "AX")
                {
                    if (TotalCalculadoD > MontoMaxAyudaP * 2)
                    {
                        TotalCalculadoD = MontoMaxAyudaP * 2;
                    }
                }
                else
                { 
                    TotalCalculadoD = MontoMaxAyudaP;
                }
                PorcentajeAyudaD = 100;
            }

 //           if (DonacionD > 0)
 //           {
 //               PorcentajeDonacionD = 100;
 //           }

            Monto.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);
            Porcentaje.Text = PorcentajeD.ToString("0", CultureInfo.InvariantCulture);
            MCalculado.Text = MCalculadoD.ToString("0.00", CultureInfo.InvariantCulture);

            Proformas.Text = ProformasD.ToString("0.00", CultureInfo.InvariantCulture);
            PorcentajeP.Text = PorcentajeProformasD.ToString("0", CultureInfo.InvariantCulture);
            MCalculadoP.Text = MCalculadoProformasD.ToString("0.00", CultureInfo.InvariantCulture);

            Donacion.Text = DonacionD.ToString("0.00", CultureInfo.InvariantCulture);

            PorcentajeAyuda.Text = PorcentajeAyudaD.ToString("0", CultureInfo.InvariantCulture); ;
            MontoA.Text = TotalCalculadoD.ToString("0.00", CultureInfo.InvariantCulture);

            EPorcentaje.Visible = true;
            Porcentaje.Visible = true;
            MCalculado.Visible = true;
            EPorcentajeP.Visible = true;
            PorcentajeP.Visible = true;
            MCalculadoP.Visible = true;
 //           PorcentajeAyuda.Visible = true;
           
            DesplegaAR();
            InicializaModificaciones();
            DesplegaDatGenMod();
            Aprobado.Focus();
        }

    }

    protected void btn_ProcesarInforme_Click(object sender, EventArgs e)
    {
        Session["NumSolicitud"] = NumSolicitud.Text;
        Response.Write("<script>window.open('ProcesaInformeAyuda.aspx');</script>");

    }


    protected void btn_AgregarEspecialidad_Click(object sender, ImageClickEventArgs e)
    {
        if (Especialidad.Text == "" || String.IsNullOrEmpty(Especialidad.Text))
        {
        }
        else
        {
            Especialidad.Text = Especialidad.Text.ToUpper();
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                string sql = "Insert INTO ASocios.APSociosAyudasEspecialidades "
                        + "(Especialidad, UserAlta, FechaCompAlta)"
                        + " VALUES ('" + Especialidad.Text + "', "
                        + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "')";
                //             MensajeR.Text = NDocumento.Text + " " + sql;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = ex.ToString();
                }
                cmd.Connection.Close();
            }
        }
    }
    protected void SolicitudesDeAyuda_SelectedIndexChanged(object sender, EventArgs e)
    {
        MontoA.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MontoA0.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MontoA1.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MontoA2.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        int NF;
        MensajeR.Text = "Error en ";
        VerificaDatos.Visible = true;
        decimal ValorEntregado;
        NF = SolicitudesDeAyuda.SelectedIndex;
        if ((string.IsNullOrEmpty(SolicitudesDeAyuda.Rows[NF].Cells[7].Text))
            || (SolicitudesDeAyuda.Rows[NF].Cells[6].Text == "&nbsp;")
            )
        {
            MensajeR.Text = "Valor Aprobado,";
        }
        else
        {
            //    ValorAprobado = Convert.ToDecimal(SolicitudesDeAyuda.Rows[NF].Cells[7].Text);
        }

        if ((string.IsNullOrEmpty(SolicitudesDeAyuda.Rows[NF].Cells[10].Text))
        || (SolicitudesDeAyuda.Rows[NF].Cells[10].Text == "&nbsp;")
       )
        {
            MensajeR.Text = MensajeR.Text + " Beneficiario, ";
            NombreBeneficiario.Text = "";
        }
        else
        {
            //    NombreBeneficiario.Text = SolicitudesDeAyuda.Rows[NF].Cells[10].Text;
        }

        if ((string.IsNullOrEmpty(SolicitudesDeAyuda.Rows[NF].Cells[8].Text))
                || (SolicitudesDeAyuda.Rows[NF].Cells[8].Text == "&nbsp;")
               )
        {
            ValorEntregado = 0;
        }
        else
        {
            ValorEntregado = Convert.ToDecimal(SolicitudesDeAyuda.Rows[NF].Cells[8].Text);
        }
        NumSolicitud.Text = SolicitudesDeAyuda.Rows[NF].Cells[1].Text;
        LeeSolAyuda();
        if (ValorEntregado <= 0)
        {
            DesplegaSolicitud();
            DEspecialidad.Focus();
            OcultaAR();
        }
        else
        {
            MensajeR.Text = "Ayuda ya fue Entregada, no se puede modificar";
            //           NumSolicitud.Text = SolicitudesDeAyuda.Rows[NF].Cells[1].Text;
            ActivaVerifAprob.Visible = true;
            OcultaAR();
        }
    }
    protected void BuscarSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Text = "";
        if (NSolicitud.Text == "")
        {
            Cedula.Focus();
        }
        else
        { 
            NumSolicitud.Text = NSolicitud.Text;
            LeeSolAyuda();
            DesplegaSolicitud();
            OcultaAR();
            LeeDatosSocio();
            DEspecialidad.Focus();
        }
    }

    protected void LeeDatosSocio()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select Identificacion, TipoSocio, "
                                + "Apellidos, Nombres, "
                                + "FechaAfil, Email, "
                                + "SituacionSocio, "
                                + "TipoIF, NombreIF, Codigo_BCE, "
                                + "TipoCuentaIF, NumCuenta, "
                                + "TelefonoConv, TelefonoCel, FechaDesafil "
                                + "From ASocios.APSocios  Where Identificacion = '"
                                + Cedula.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                // do something if the query return no rows
                // you may insert the relevant redirection you asked for
                MensajeR.Text = "SOCIO NO REGISTRADO";
                MensajeR.Visible = true;
            }
            else
            {
                // Get the value of Col1 in the 3rd row (0 is the first row)
                // Col1Value = (int)dt.Rows[2]["Col1"];
                CodServicio = (string)reader.Rows[0][1];
                Servicio.Text = (string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                Servicio.Visible = true;
                Nombres.Text = (string)reader.Rows[0][2] + " " + (string)reader.Rows[0][3];
                FechaAfiliacion.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][4]));
                Situacion.Text = LeeSituacion((string)reader.Rows[0][6]);

                if ((string)reader.Rows[0][6] == "D")
                {
                    Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][14]));

                    OtroSocio.Focus();
                }
                else
                {
                    FechaSolicitud.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));

                    FechaSolicitud.Focus();
                }
            }
            cmd.Connection.Close();
        }
    }


    protected void btn_HabilitaProcesoFacturas_Click(object sender, ImageClickEventArgs e)
    {

        dg_ValorFacturas.Visible = true;
        btn_DeshabilitaProcesoFacturas.Visible = true;
        EFactura.Visible = true;
        SecuenFacturas.Visible = true;
        EVFactura.Visible = true;
        VFactura.Visible = true;
        EObservFactura.Visible = true;
        ObservFactura.Visible = true;
        btn_AgregarFactura.Visible = true;
        btn_TerminarIngresoFacturas.Visible = true;
        btn_TerminarIngresoFacturas0.Visible = true;

        SecuenFacturas.Focus();
        RecalculaFacturas();
        ETFacturas.Visible = true;
        TFacturas.Visible = true;
        ETFacturas0.Visible = true;
        TFacturas0.Visible = true;
    }

    protected void btn_DeshabilitaProcesoFacturas_Click(object sender, ImageClickEventArgs e)
    {
        dg_ValorFacturas.Visible = false;
//        btn_HabilitaProcesoFacturas.Visible = false;
        btn_DeshabilitaProcesoFacturas.Visible = false;
        EFactura.Visible = false;
        SecuenFacturas.Visible = false;
        EVFactura.Visible = false;
        VFactura.Visible = false;
        EObservFactura.Visible = false;
        ObservFactura.Visible = false;
        btn_AgregarFactura.Visible = false;
        btn_EliminarFactura.Visible = false;
    }

    protected void dg_ValorFacturas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int NF = dg_ValorFacturas.SelectedIndex;
        if ((string.IsNullOrEmpty(dg_ValorFacturas.Rows[NF].Cells[1].Text))
            || (dg_ValorFacturas.Rows[NF].Cells[1].Text == "&nbsp;"))
        {
            MensajeR.Text = NF + "Error Factura ";
        }
        else
        {
            MensajeR.Text = NF + "Factura Procesada correctamente ";
            SecuenFacturas.Text = dg_ValorFacturas.Rows[NF].Cells[1].Text;
            VFactura.Text = dg_ValorFacturas.Rows[NF].Cells[2].Text;
            ObservFactura.Text = dg_ValorFacturas.Rows[NF].Cells[3].Text;
        }
        btn_EliminarFactura.Visible = true;
        SecuenFacturas.Focus();
    }

    protected void btn_AgregarFactura_Click(object sender, ImageClickEventArgs e)
    {
        VFactura.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        decimal VFacturaD;
        VFactura.Text = VFactura.Text.Replace(".", ",");

        VFacturaD = System.Convert.ToDecimal(VFactura.Text);


        VFactura.Text = (string.Format("{0:###0.00}", Convert.ToDecimal(VFactura.Text)));
        VFactura.Text = VFactura.Text.Replace(",", ".");

        if (VFactura.Text == "" || String.IsNullOrEmpty(VFactura.Text) || (VFacturaD <=0))
        {
        }
        else
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                string sql = "Insert INTO ASocios.APSociosAyudasFacturas "
                        + "(NumSolicitud, Secuencial, "
                        + "Valorfactura, ObservFactura, "
                        + "UserAlta, FechaCompAlta) "
                        + "VALUES (" + NumSolicitud.Text + ", " + SecuenFacturas.Text + ", "
                        +  VFactura.Text + ", '" + ObservFactura.Text + "', "
                        + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "')";
                //             MensajeR.Text = NDocumento.Text + " " + sql;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.ToString();
                }
                cmd.Connection.Close();

                MensajeR.Text = "Factura Registrada Correctamente";
                RecalculaFacturas();
            }
        }
    }

    protected void btn_EliminarFactura_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Delete FROM ASocios.APSociosAyudasFacturas "
                    + "WHERE NumSolicitud=" + NumSolicitud.Text
                    + " AND Secuencial=" + SecuenFacturas.Text;

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + ex.ToString();
            }
            cmd.Connection.Close();
            RecalculaFacturas();
            SecuenFacturas.Focus();
            btn_EliminarFactura.Visible = false;
        }
    }
    protected void btn_TerminarIngresoFacturas_Click(object sender, ImageClickEventArgs e)
    {
        Monto.Text = TFacturas.Text;
        Monto.Focus();
    }
    private void RecalculaFacturas()
    {
        TFacturas.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        TFacturas0.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        dg_ValorFacturas.Dispose();
        dg_ValorFacturas.DataSourceID = "";
        dg_ValorFacturas.DataSourceID = (string)(TabValorFacturas.UniqueID);
        dg_ValorFacturas.DataBind();
        SecuenFacturas.Text = "";
        VFactura.Text = "0";
        ObservFactura.Text = "OK";
        SecuenFacturas.Focus();
        decimal suma = 0;
        for (int X = 0; X < dg_ValorFacturas.Rows.Count; X++)
        {
            suma = suma + decimal.Parse(dg_ValorFacturas.Rows[X].Cells[2].Text);
        }
 //       TFacturas.Text = suma.ToString("N2");
        TFacturas.Text= (string.Format("{0:##0.00}", suma));
        TFacturas0.Text = TFacturas.Text;
    }

    protected void btn_ProcesarFacturas_Click(object sender, EventArgs e)
    {
        Session["NumSolicitud"] = NumSolicitud.Text;
        Response.Write("<script>window.open('ProcesaFacturasAyuda.aspx');</script>");
    }

    protected void btn_ProcesaGastos_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int SolicitudNum;
            SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
            string sql = "Select "
                    + "MontoDeGastos, PorcentajeAyuda, ValorSolicitado, "
                    + "MontoFacturas, MontoProformas, MontoDonaciones "
                    + "From ASocios.APSociosAyudas  Where NumSolicitud = "
                    + SolicitudNum;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    // do something if the query return no rows
                    // you may insert the relevant redirection you asked for
                    MensajeR.Text = "GASTOS NO LEIDOS";
                    MensajeR.Visible = true;
                }
                else
                {
                    // Get the value of Col1 in the 3rd row (0 is the first row)
                    // Col1Value = (int)dt.Rows[2]["Col1"];

                    Monto.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    Porcentaje.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    MCalculado.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    Proformas.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    Donacion.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

                    if (!DBNull.Value.Equals(reader.Rows[0][3]))
                    {
                        Monto.Text = (string.Format("{0:0.00}", reader.Rows[0][3]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][1]))
                    {
                        Porcentaje.Text = (string.Format("{0:0}", reader.Rows[0][1]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][2]))
                    {
                        MCalculado.Text = (string.Format("{0:0.00}", reader.Rows[0][2]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][4]))
                    {
                        Proformas.Text = (string.Format("{0:0.00}", reader.Rows[0][4]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][5]))
                    {
                        Donacion.Text = (string.Format("{0:0.00}", reader.Rows[0][5]));
                    }



                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();
            }
            cmd.Connection.Close();
        }
    }

    protected void btn_ActualizaBasicos_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE ASocios.APSociosAyudas "
                        + "SET "
                        + "Unidad='" + Unidad.Text + "', "
                        + "Grado='" + Grado.Text + "', "
                        + "AServ='" + Convert.ToString(Convert.ToInt16(TiempoServicio.Text)) + "', "
                        + "RazonSolicitud='" + RazonSolicitud.Text + "', "
                        + "ObservSolicitud='" + ObservSolicitud.Text + "', "
                        + "TipoBeneficiario='" + TipoBeneficiario.Text + "', "
                        + "NombreBeneficiario='" + NombreBeneficiario.Text + "', "
                        + "OtroBeneficiario='" + OtroBeneficiario.Text + "' "
                        + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }


    protected void Aprobado_CheckedChanged(object sender, EventArgs e)
    {
        if (Aprobado.Checked)
        {
            EMontoA.Visible = true;
            MontoA.Visible = true;
            EBeneficiario0.Visible = true;


            if (MCalculadoD > 0)
            {
                MontoA0.Text = MCalculado.Text;
                Beneficiario0.Text = "SOCIO";
            }

            MontoA1.Visible = true;
            Beneficiario1.Visible = true;
            if (MCalculadoProformasD > 0)
            {
                MontoA1.Text = MCalculadoP.Text;
                Beneficiario1.Text = "PROFORMA";
            }

            if (DonacionD > 0)
            {
                MontoA2.Text = Donacion.Text;
                Beneficiario2.Text = "DONACION";
            }

            PorcentajeAyuda.Visible = true;
            MontoA0.Visible = true;
            Beneficiario0.Visible = true;

            EBeneficiario1.Visible = true;
            MontoA1.Visible = true;
            Beneficiario1.Visible = true;

            EBeneficiario2.Visible = true;
            MontoA2.Visible = true;
            Beneficiario2.Visible = true;

            ERecomendacion.Visible = true;
            Recomendacion.Visible = true;
            FechaResultado.Text = ""; // (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
            ObservacionProceso.Text = "OK";

            PorcentajeAyuda.Text = PorcentajeAyuda.Text.Replace(".", ",");
            MontoAyuda = TotalCalculadoD;
            if (Convert.ToDecimal(PorcentajeAyuda.Text) > 50 || Convert.ToDecimal(PorcentajeAyuda.Text) == 0)
            {
                if ((MontoAyuda >= MontoMaxAyudaP) && (Convert.ToDecimal(PorcentajeAyuda.Text) == 0))
                {
                    MensajeR.Text = (string.Format("{0:0.00}", MontoMaxAyudaP)) + " " + (string.Format("{0:0.00}", MontoAyuda)) + " " + PorcentajeAyuda.Text;
                    Recomendacion.Text = "La Comisión de Ayudas recomienda conceder el MONTO MAXIMO DE AYUDA, "
                           + "segun establece el Reglamento de Ayudas por Emfermedad Grave y/o Calamidad Doméstica Vigente.";
                }
                else
                {
                    Recomendacion.Text = "La Comisión de Ayudas recomienda conceder una ayuda por "
                                 + "los gastos efectuados por el Socio/a segun establece "
                                 + "el Reglamento de Ayudas por Emfermedad Grave y/o Calamidad Doméstica Vigente.";
                }
            }
            else
            {
                Recomendacion.Text = "La Comisión de Ayudas recomienda conceder una ayuda por "
                 + "los gastos efectuados por el Socio/a segun establece el Reglamento de Ayudas por Emfermedad Grave y/o Calamidad Doméstica Vigente.";
            }
            MontoA0.Focus();

//            ActivaRechazo.Visible = false;
            EDRechazo.Visible = false;
            DRazonRechazo.Visible = false;
            Rechazado.Visible = false;
            RazonRechazo.Visible = false;
        }
        else
        {

            Rechazado.Visible = true;
//            ActivaRechazo.Visible = true;

            Aprobado.Visible = false;
            EMontoA.Visible = false;
            MontoA.Visible = false;
            EBeneficiario0.Visible = false;
            Beneficiario0.Visible = false;
            MontoA0.Visible = false;
            EBeneficiario1.Visible = false;
            Beneficiario1.Visible = false;
            MontoA1.Visible = false;
            EBeneficiario2.Visible = false;
            Beneficiario2.Visible = false;
            MontoA2.Visible = false;
            ERecomendacion.Visible = false;
            Recomendacion.Visible = false;
  //          ActivaAprobacion.Visible = false;
        }
    }

    protected void Rechazado_CheckedChanged(object sender, EventArgs e)
    {
        if (Rechazado.Checked)
        {
            EDRechazo.Visible = true;
            DRazonRechazo.Visible = true;
            Rechazado.Visible = true;
            RazonRechazo.Visible = true;
            //          ActivaRechazo.Visible = true;

            //          ActivaAprobacion.Visible = false;
            Aprobado.Visible = false;
            EMontoA.Visible = false;
            MontoA.Visible = false;
            EBeneficiario0.Visible = false;
            Beneficiario0.Visible = false;
            EBeneficiario1.Visible = false;
            Beneficiario1.Visible = false;
            EBeneficiario2.Visible = false;
            Beneficiario2.Visible = false;
            ERecomendacion.Visible = false;
            Recomendacion.Visible = false;
            FechaResultado.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
            ObservacionProceso.Text = "NEGADA ";

        }
        else
        {

            EDRechazo.Visible = false;
            DRazonRechazo.Visible = false;
            Rechazado.Visible = false;
            RazonRechazo.Visible = false;
  //          ActivaRechazo.Visible = false;
            Aprobado.Visible = true;
  //          ActivaAprobacion.Visible = true;
        }
    }

    protected void NSolicitud_TextChanged(object sender, EventArgs e)
    {
        MensajeR.Text = "";
        if (NSolicitud.Text == "")
        {
            Cedula.Focus();
        }
        else
        {
            NumSolicitud.Text = NSolicitud.Text;
            LeeSolAyuda();
            DesplegaSolicitud();
            OcultaAR();
            LeeDatosSocio();
            DEspecialidad.Focus();
        }

    }
    protected void btn_AgregarDiagnosticos_Click(object sender, ImageClickEventArgs e)
    {
        if ((RazonSolicitud.Text == "") 
                || String.IsNullOrEmpty(RazonSolicitud.Text)
                || (DRazonSolicitud.SelectedItem.Value.ToString() != "-----")
            )
        {
            MensajeR.Text = "No se incluyo Diagnostico a la Tabla";
        }
        else
        {
            RazonSolicitud.Text = RazonSolicitud.Text.ToUpper();
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                string sql = "Insert INTO ASocios.APSociosAyudasRazones "
                        + "(Razones, UserAlta, FechaCompAlta)"
                        + " VALUES ('" + RazonSolicitud.Text + "', "
                        + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "')";
                //             MensajeR.Text = NDocumento.Text + " " + sql;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = ex.ToString();
                }
                cmd.Connection.Close();
            }
            DRazonSolicitud.Dispose();
            DRazonSolicitud.DataSourceID = "";
            DRazonSolicitud.DataSourceID = (string)(TabRazonSolicitud.UniqueID);
            DRazonSolicitud.DataBind();
            DRazonSolicitud.SelectedIndex = -1;
        }
    }

    protected void DTipoAyuda_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoAyuda.Text = DTipoAyuda.SelectedItem.Value.ToString();
        DocumentosRecibidos.Focus();
    }

    protected void DRazonSolicitud_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DRazonSolicitud.SelectedItem.Value.ToString()!="-----")
        {
            RazonSolicitud.Text = DRazonSolicitud.SelectedItem.Value.ToString();
        }
    }

    protected void MCalculado_TextChanged(object sender, EventArgs e)
    {

    }
}
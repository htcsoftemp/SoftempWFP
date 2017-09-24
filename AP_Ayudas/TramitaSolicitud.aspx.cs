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

public partial class TramitaSolicitud : System.Web.UI.Page
{
    // FechaSolicitud  date    Checked
    // TipoAyuda   nvarchar(3) Checked
    // TipoBeneficiario    varchar(10) Checked
    // NombreBeneficiario  varchar(50) Checked
    // OtroBeneficiario    varchar(50) Checked
    // RazonSolicitud  nvarchar(MAX)   Checked
    // Especialidad    nvarchar(30)    Checked
    // CasaSalud   nvarchar(50)    Checked
    // MedicoCertifica nvarchar(50)    Checked
    // ObservSolicitud nvarchar(150)   Checked
    // Grado   nvarchar(15)    Checked
    // Unidad  nvarchar(30)    Checked
    // AServ   int Checked
    // NombresU    nvarchar(100)   Checked
    // TelefonosRef    nvarchar(30)    Checked
    // DocRecibidos    nvarchar(MAX)   Checked
    // MontoDeGastos   decimal(18, 2)  Checked
    // PorcentajeAyuda real    Checked
    // ValorSolicitado decimal(18, 2)  Checked
    // FechaAprobRechaz    date    Checked
    // ReferAprobRechaz    nvarchar(50)    Checked
    // ResultadoComiteAR   char(1) Checked
    // ObservAR    nvarchar(200)   Checked
    // ValorAprobado   decimal(18, 2)  Checked
    // ComprobanteP    bigint  Checked
    // ValorAprobado1  decimal(18, 2)  Checked
    // Beneficiario1   nvarchar(50)    Checked
    // ComprobanteP1   bigint  Checked
    // ValorAprobado2  decimal(18, 2)  Checked
    // Beneficiario2   nvarchar(50)    Checked
    // ComprobanteP2   bigint  Checked
    // ReferEntrega    nvarchar(50)    Checked
    // FechaEntrega    date    Checked
    // ObservEntrega   nvarchar(50)    Checked
    // Doc1    nvarchar(100)   Checked
    // Doc2    nvarchar(100)   Checked
    // Doc3    nvarchar(150)   Checked
    // Doc4    nvarchar(100)   Checked
    // CertificacionInfor  nvarchar(MAX)   Checked
    // UserAlta    nchar(20)   Checked
    // FechaCompAlta   datetime    Checked
    // CentroCostos    nchar(5)    Checked
    // TotalPagado decimal(18, 2)  Checked
    // UserAR  nvarchar(15)    Checked
    // FechaCompAR datetime    Checked
    // UserEntrega nvarchar(15)    Checked
    // FechaCompEntrega    datetime    Checked
    static string CodServicio;
    static decimal MontoSupGastosP, MontoMaxAyudaP, PorcenInferP, PorcenSuperP, MontoExterAyudaP;

    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        LeeParamAyudas();
        OtroSocio.Focus();
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
    protected void InicializaDatos()
    {
        Cedula.Text = "";
        Nombres.Text = "";
        Servicio.Text = "";
        Situacion.Text = "";
        FechaAfiliacion.Text = "";
        FechaSolicitud.Text = "";
        RegistrarProceso.Visible = false;
        EFechaSolicitud.Text = "";
        FechaTramite.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
        Secuencial.Text = "0";
    }
    protected void InicializaModificaciones()
    {
        Secuencial.Text = Convert.ToString(Convert.ToInt16(Secuencial.Text) + 1);
        EMontoM.Visible = false;
        MontoM.Visible = false;
        EPorcentajeM.Visible = false;
        PorcentajeM.Visible = false;
        EMCalculadoM.Visible = false;
        MCalculadoM.Visible = false;
        VerificaDatos.Visible = false;
        EDiagnosticoM.Visible = false;
        DRazonSolicitudM.Visible = false;
        RazonSolicitudM.Visible = false;
        EUnidadM.Visible = false;
        DUnidad.Visible = false;
        UnidadM.Visible = false;
        DTipoGrado.Visible = false;
        EGradoM.Visible = false;
        GradoM.Visible = false;
        ETiempoServicioM.Visible = false;
        TiempoServicioM.Visible = false;
        EBeneficiarioM.Visible = false;
        DTipoBeneficiarioM.Visible = false;
        TipoBeneficiarioM.Visible = false;
        NombreBeneficiarioM.Visible = false;
        OtroBeneficiarioM.Visible = false;
        UnidadM.Visible = false;
        EDocumenM.Visible = false;
        DocumentosRecibidosM.Visible = false;
        EObservacionM.Visible = false;
        ObservacionSolicitudM.Visible = false;
        DUnidad.ClearSelection();
        DTipoGrado.ClearSelection();
        TiempoServicioM.Text = "0";
        CodTramite.Text = "";
    }
    protected void OtroSocio_Click(object sender, ImageClickEventArgs e)
    {
        Cedula.Enabled = true;
        InicializaDatos();
        Cedula.Focus();
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
                    + "ValorAprobado, TotalPagado "
                    + "From ASocios.APSociosAyudas  Where NumSolicitud = "
                    + SolicitudNum;

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
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
                if (!DBNull.Value.Equals(reader.Rows[0][4]))
                {
                    Monto.Text = (string.Format("{0:0.00}", reader.Rows[0][4]));
                }
                if (!DBNull.Value.Equals(reader.Rows[0][5]))
                {
                    Porcentaje.Text = (string.Format("{0:0.00}", reader.Rows[0][5]));
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

            }
            cmd.Connection.Close();
        }
    }

    private void OcultaDatGenMod()
    {
        EFechaTramite.Visible = false;
        FechaTramite.Visible = false;
        ESecuencialM.Visible = false;
        Secuencial.Visible = false;
        ETramite.Visible = false;
        DTramite.Visible = false;
        CodTramite.Visible = false;
        EFechaResultado.Visible = false;
        FechaResultado.Visible = false;
        EResultado.Visible = false;
        EDocumenM.Visible = false;
        DocumentosRecibidosM.Visible = false;
        ResultadoTramite.Visible = false;
        EObservacion.Visible = false;
        ObservacionProceso.Visible = false;
        CalculaAyudas.Visible = false;
    }
    private void DesplegaDatGenMod()
    {
        EFechaTramite.Visible = true;
        FechaTramite.Visible = true;
        ESecuencialM.Visible = true;
        Secuencial.Visible = true;
        ETramite.Visible = true;
        DTramite.Visible = true;
        CodTramite.Visible = true;
        DTramite.ClearSelection();

        EFechaResultado.Visible = true;
        FechaResultado.Visible = true;
        EResultado.Visible = true;
        ResultadoTramite.Visible = true;
        EObservacion.Visible = true;
        ObservacionProceso.Visible = true;

        Secuencial.Text = "0";
        CodTramite.Text = "";
        FechaResultado.Text = FechaTramite.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
        ResultadoTramite.Text = "OK";
        ObservacionProceso.Text = "";
    }
    private void GrabaTramite()
    {
        SqlCommand cmd;
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Insert INTO ASocios.APSociosAyudasProceso "
                        + "(Identificacion, NumSolicitud, "
                        + "CodProceso, FechaProceso, "
                        + "Secuencial, Proceso,"
                        + "FechaProxProb, ResultadoProceso, "
                        + "Observacion, "
                        + "UserProceso, FechaCompProceso )"
                        + " VALUES ('" + Cedula.Text + "', '" + NumSolicitud.Text + "', "
                        + "'" + CodTramite.Text + "', '" + FechaTramite.Text + "', "
                        + "'" + Secuencial.Text + "', '" + DTramite.SelectedItem.Text + "', "
                        + "'" + FechaResultado.Text + "', '" + ResultadoTramite.Text + "', "
                        + "'" + ObservacionProceso.Text + "', "
                        + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "')";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                OtroTramite.Visible = true;
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

    private void PreparaModGastos()
    {
        MontoM.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        PorcentajeM.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MCalculadoM.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        MontoM.Text = Monto.Text;
        PorcentajeM.Text = Porcentaje.Text;
        MCalculadoM.Text = MCalculado.Text;
        DocumentosRecibidosM.Text = DocumentosRecibidos.Text;
        ObservSolicitudM.Text = ObservSolicitud.Text;

        EMontoM.Visible = true;
        MontoM.Visible = true;
        EPorcentajeM.Visible = true;
        PorcentajeM.Visible = true;
        EMCalculadoM.Visible = true;
        MCalculadoM.Visible = true;
        EDocumenM.Visible = true;
        DocumentosRecibidosM.Visible = true;
        EObservM.Visible = true;
        ObservSolicitudM.Visible = true;
        CalculaAyudas.Visible = true;
        MontoM.Focus();
    }
    private void ProcesaGastos()
    {
        if (MontoM.Text=="" || PorcentajeM.Text=="" || MCalculadoM.Text=="" || DocumentosRecibidosM.Text == "")
        {
            MensajeR.Text = "Error en Datos Ingresados";
            MontoM.Focus();
        }
        else
        {
            RegistrarProceso.Visible = true;
            RegistrarProceso.Focus();
        }
    }
    private void GrabaGastos()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            //            PorcentajeM.Text = (string.Format("{0:0.00}", PorcentajeM.Text));
            //            decimal MontoD, PorcentajeD, MCalculadoD;

            //            MontoD = Convert.ToDecimal(MontoM.Text);
            //            PorcentajeD = Convert.ToDecimal(PorcentajeM.Text);
            //            MCalculadoD = Convert.ToDecimal(MCalculadoM.Text);

            //            MontoM.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);
            //            PorcentajeM.Text = PorcentajeD.ToString("0.00", CultureInfo.InvariantCulture);
            //            MCalculadoM.Text = MCalculadoD.ToString("0.00", CultureInfo.InvariantCulture);
            CalculoDeAyudas();
 //           MontoM.Text = MontoM.Text.Replace(".", ",");
 //           PorcentajeM.Text = PorcentajeM.Text.Replace(".", ",");
 //           MCalculadoM.Text = MCalculadoM.Text.Replace(".", ",");

            string sql = "UPDATE ASocios.APSociosAyudas "
                    + "SET MontoDeGastos=" + MontoM.Text
                    + ", PorcentajeAyuda=" + PorcentajeM.Text
                    + ", ValorSolicitado=" + MCalculadoM.Text
                    + ", DocRecibidos='" + DocumentosRecibidosM.Text + "' "
                    + ", ObservSolicitud='" + ObservSolicitudM.Text + "' "
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

    private void PreparaModRazonSol()
    {
        DRazonSolicitudM.ClearSelection();
        RazonSolicitudM.Text = RazonSolicitud.Text;
        ObservacionSolicitudM.Text = ObservSolicitud.Text;

        EDiagnosticoM.Visible = true;
        DRazonSolicitudM.Visible = true;
        RazonSolicitudM.Visible = true;
        EObservacionM.Visible = true;
        ObservacionSolicitudM.Visible = true;
        DRazonSolicitudM.Focus();
    }
    private void ProcesaRazonSolicitud()
    {
        if (DRazonSolicitudM.SelectedItem.Text=="-----")
        {
            MensajeR.Text = DRazonSolicitudM.SelectedItem.Text;
        }
        else
        {
            RazonSolicitudM.Text = DRazonSolicitudM.SelectedItem.Text;
        }

        if (RazonSolicitudM.Text=="")
        {
            MensajeR.Text = "Error en Razon de Solicitud";
        }
        else
        {
            GrabaRazonSolicitud();
        }
    }
    private void GrabaRazonSolicitud()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE ASocios.APSociosAyudas "
                    + "SET RazonSolicitud='" + RazonSolicitudM.Text + "', "
                    + "ObservSolicitud='" + ObservacionSolicitudM.Text + "' "
                    + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }

    private void PreparaModBeneficiarios()
    {
        DTipoBeneficiarioM.ClearSelection();
        TipoBeneficiarioM.Text = TipoBeneficiario.Text;
        NombreBeneficiarioM.Text = NombreBeneficiario.Text;
        OtroBeneficiarioM.Text = OtroBeneficiario.Text;

        EBeneficiarioM.Visible = true;
        DTipoBeneficiarioM.Visible = true;
        TipoBeneficiarioM.Visible = true;
        NombreBeneficiarioM.Visible = true;
        OtroBeneficiarioM.Visible = true;
        DTipoBeneficiarioM.Focus();
    }
    private void ProcesaBeneficiarios()
    {
        if (DTipoBeneficiarioM.SelectedIndex != 0)
        {
            TipoBeneficiarioM.Text = DTipoBeneficiarioM.SelectedItem.Value;
        }

        if (TipoBeneficiarioM.Text == "")
        {
            MensajeR.Text = "Error en Tipo de Beneficiario";
            DTipoBeneficiarioM.Focus();
        }
        else
        {
            if (TipoBeneficiarioM.Text != "S")
            {
                if (NombreBeneficiarioM.Text=="")
                {
                    MensajeR.Text = "Error en Nombre de Beneficiario";
                    NombreBeneficiarioM.Focus();
                }
                else
                {
                    GrabaBeneficiarios();
                }
            }
            else
            {
                GrabaBeneficiarios();
            }
        }
    }
    private void GrabaBeneficiarios()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE ASocios.APSociosAyudas "
                    + "SET TipoBeneficiario='" + TipoBeneficiarioM.Text + "', "
                    + "NombreBeneficiario='" + NombreBeneficiarioM.Text + "', "
                    + "OtroBeneficiario='" + OtroBeneficiarioM.Text + "' "
                    + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }

    private void PreparaModEspecialidad()
    {
        EspecialidadM.Text = Especialidad.Text;
        CasaDeSaludM.Text = CasaDeSalud.Text;
        MedicoQueCertificaM.Text = MedicoQueCertifica.Text;

        EEspecialidadM.Visible = true;
        EspecialidadM.Visible = true;
        ECasaSaludM.Visible = true;
        CasaDeSaludM.Visible = true;
        EMedicoCertificaM.Visible = true;
        MedicoQueCertificaM.Visible = true;
        EspecialidadM.Focus();
    }
    private void ProcesaEspecialidad()
    {
        if (EspecialidadM.Text == "")
        {
            MensajeR.Text = "Error en Especialidad";
            EspecialidadM.Focus();
        }
        else
        {
            GrabaEspecialidad();
        }
    }
    private void GrabaEspecialidad()
    {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                string sql = "UPDATE ASocios.APSociosAyudas "
                        + "SET Especialidad='" + EspecialidadM.Text + "', "
                        + "CasaSalud='" + CasaDeSaludM.Text + "', "
                        + "MedicoCertifica='" + MedicoQueCertificaM.Text + "' "
                        + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
    }

    private void PreparaModUnidad()
    {
        UnidadM.Text = Unidad.Text;
        GradoM.Text = Grado.Text;
        TiempoServicioM.Text = TiempoServicio.Text;
        DUnidad.ClearSelection();
        DTipoGrado.ClearSelection();

        EUnidadM.Visible = true;
        DUnidad.Visible = true;
        UnidadM.Visible = true;
        EGradoM.Visible = true;
        DTipoGrado.Visible = true;
        GradoM.Visible = true;
        ETiempoServicioM.Visible = true;
        TiempoServicioM.Visible = true;
        DUnidad.Focus();
        VerificaDatos.Visible = true;
    }
    private void ProcesaUnidad()
    {
        if (DUnidad.SelectedIndex != 0)
        {
            UnidadM.Text = DUnidad.SelectedItem.Text;
        }
        if (DTipoGrado.SelectedIndex != 0)
        {
            GradoM.Text = DTipoGrado.SelectedItem.Text;
        }

        if (UnidadM.Text == "" || GradoM.Text=="" || TiempoServicioM.Text=="")
        {
            MensajeR.Text = "Error en Unidad";
            DUnidad.Focus();
        }
        else
        {
            GrabaUnidad();
        }
    }
    private void GrabaUnidad()
    {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                string sql = "UPDATE ASocios.APSociosAyudas "
                        + "SET Unidad='" + UnidadM.Text + "', "
                        + "Grado='" + GradoM.Text + "', "
                        + "AServ='" + Convert.ToString(Convert.ToInt16(TiempoServicioM.Text)/12) + "' "
                        + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
    }

    protected void RegistrarProceso_Click(object sender, EventArgs e)
    {
        switch (CodTramite.Text)
        {
                case "310":  // Modifica Gastos
                    {
                        GrabaGastos();
                        break;
                    }
            case "320":  // Modifica Beneficiarios
                {
                    GrabaBeneficiarios();
                    break;
                }
            case "330":  // Modifica Diagnostico
                {
                    GrabaRazonSolicitud();
                    break;
                }
            case "340":  // Modifica Especialida / Casa Salud / Medico
                {
                    GrabaEspecialidad();
                    break;
                }
            case "350":   // Modifica Unidad / Grado / T. Servicio
                {
                    GrabaUnidad();
                    break;
                }
            default:
                {
                        break;
                }
        }
 //       GrabaTramite();
        OcultaDatGenMod();
        OcultaCamposAModificar();
  //      MensajeR.Text = "Proceso Registrado CORRECTAMENTE";
        RegistrarProceso.Visible = false;
        DesplegaDatGenMod();
        MensajeR.Visible = true;
        SolicitudesDeAyuda.DataSourceID = "";
        SolicitudesDeAyuda.DataSourceID = (string)(TabSolicitudesAyuda.UniqueID);
        LeeSolAyuda();
    }
    private void OcultaCamposAModificar()
    {
        EMontoM.Visible = false;
        MontoM.Visible = false;
        EPorcentajeM.Visible = false;
        PorcentajeM.Visible = false;
        EMCalculadoM.Visible = false;
        MCalculadoM.Visible = false;
        EDiagnosticoM.Visible = false;
        DRazonSolicitudM.Visible = false;
        RazonSolicitudM.Visible = false;

        EUnidadM.Visible = false;
        UnidadM.Visible = false;
        EGradoM.Visible = false;
        GradoM.Visible = false;
        ETiempoServicioM.Visible = false;
        TiempoServicioM.Visible = false;

        EBeneficiarioM.Visible = false;
        DTipoBeneficiarioM.Visible = false;
        NombreBeneficiarioM.Visible = false;
        TipoBeneficiarioM.Visible = false;
        OtroBeneficiarioM.Visible = false;
        EEspecialidadM.Visible = false;
        EspecialidadM.Visible = false;
        ECasaSaludM.Visible = false;
        CasaDeSaludM.Visible = false;
        EMedicoCertificaM.Visible = false;
        MedicoQueCertificaM.Visible = false;
        VerificaDatos.Visible = false;
    }
    protected void VerificaDatos_Click(object sender, ImageClickEventArgs e)
    {
        switch (CodTramite.Text)
        {
            case "310":  // Modifica Gastos
                {
                    ProcesaGastos();
                    break;
                }
            case "320":  // Modifica Beneficiarios
                {
                    ProcesaBeneficiarios();
                    break;
                }
            case "330":  // Modifica Diagnostico
                {
                    ProcesaRazonSolicitud();
                    break;
                }
            case "340":  // Modifica Especialida / Casa Salud / Medico
                {
                    ProcesaEspecialidad();
                    break;
                }
            case "350":   // Modifica Unidad / Grado / T. Servicio
                {
                    ProcesaUnidad();
                    break;
                }
            default:
                {
                    break;
                }
        }
        RegistrarProceso.Visible = true;
    }
    protected void SolicitudesDeAyuda_SelectedIndexChanged(object sender, EventArgs e)
    {
        int NF;
        MensajeR.Text = "Error en ";
        decimal ValorEntregado, ValorAprobado;
        NF = SolicitudesDeAyuda.SelectedIndex;
        if ((string.IsNullOrEmpty(SolicitudesDeAyuda.Rows[NF].Cells[7].Text))
            || (SolicitudesDeAyuda.Rows[NF].Cells[6].Text == "&nbsp;")
            )
        {
            MensajeR.Text = "Valor Aprobado,";
        }
        else
        {
            ValorAprobado = Convert.ToDecimal(SolicitudesDeAyuda.Rows[NF].Cells[7].Text);
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
            NombreBeneficiario.Text = SolicitudesDeAyuda.Rows[NF].Cells[10].Text;
        }

        if ((string.IsNullOrEmpty(SolicitudesDeAyuda.Rows[NF].Cells[8].Text))
                || (SolicitudesDeAyuda.Rows[NF].Cells[8].Text == "&nbsp;")
               )
        {
            MensajeR.Text = MensajeR.Text + " Valor Entregado, ";
            ValorEntregado = 0;
        }
        else
        {
            ValorEntregado = Convert.ToDecimal(SolicitudesDeAyuda.Rows[NF].Cells[8].Text);
        }

        if (ValorEntregado <= 0)
        {
            NumSolicitud.Text = SolicitudesDeAyuda.Rows[NF].Cells[1].Text;
            LeeSolAyuda();
        }
        else
        {
            MensajeR.Text = "Ayuda ya fue Entregada, no se puede modificar";
            NumSolicitud.Text = SolicitudesDeAyuda.Rows[NF].Cells[1].Text;
        }
        InicializaModificaciones();
        DesplegaDatGenMod();
        FechaTramite.Focus();
    }
    protected void DTramite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CodTramite.Text = DTramite.SelectedValue;
        OcultaCamposAModificar();
        VerificaDatos.Visible = true;
        switch (CodTramite.Text)
        {
            case "310":  // Modifica Gastos
                {
                    PreparaModGastos();
                    break;
                }
            case "320":  // Modifica Beneficiarios
                {
                    PreparaModBeneficiarios();
                    break;
                }
            case "330":  // Modifica Diagnostico
                {
                    PreparaModRazonSol();
                    break;
                }
            case "340":  // Modifica Especialida / Casa Salud / Medico
                {
                    PreparaModEspecialidad();
                    break;
                }
            case "350":   // Modifica Unidad / Grado / T. Servicio
                {
                    PreparaModUnidad();
                    break;
                }
            default:
                {
                    break;
                }
        }

    }


    protected void OtroTramite_Click(object sender, ImageClickEventArgs e)
    {
        InicializaModificaciones();
        DTramite.ClearSelection();
        DesplegaDatGenMod();
        FechaTramite.Focus();
        //       DTramite.SelectedItem.Text = "-----";
    }

    private void CalculoDeAyudas()
    {
        MontoM.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        PorcentajeM.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MCalculadoM.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        decimal MontoD, PorcentajeD, MCalculadoD;
        MontoM.Text = MontoM.Text.Replace(".", ",");

        MontoD = System.Convert.ToDecimal(MontoM.Text);

        if (MontoD <= MontoSupGastosP)
        {
            PorcentajeD = PorcenSuperP;
        }
        else
        {
            PorcentajeD = PorcenInferP;
        }

        MCalculadoD = MontoD * PorcentajeD / 100;

        if (MCalculadoD > MontoMaxAyudaP)
        {
            MCalculadoD = MontoMaxAyudaP;
            PorcentajeD = 100;
        }

        PorcentajeM.Text = PorcentajeD.ToString("0.00", CultureInfo.InvariantCulture);
        MCalculadoM.Text = MCalculadoD.ToString("0.00", CultureInfo.InvariantCulture);
        MontoM.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);

        //       PorcentajeM.Text = PorcentajeM.Text.Replace(".", ",");
        //       MCalculadoM.Text = MCalculadoM.Text.Replace(".", ",");

    }
    protected void CalculaAyudas_Click(object sender, ImageClickEventArgs e)
    {
        CalculoDeAyudas();
    }
}
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
public partial class IngresoSolicitud : System.Web.UI.Page
{
    static string CodServicio;
    static decimal MontoSupGastosP, MontoMaxAyudaP, PorcenInferP, PorcenSuperP, MontoExterAyudaP;
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    static DateTime FechaConvertida;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LeeParamAyudas();
            OtroSocio.Focus();
            InicializaDatos();
            OcultaSolicitud();
        }
    }

    protected void InicializaDatos()
    {
        Cedula.Text = "";
        Nombres.Text = "";
        Servicio.Text = "";
        Situacion.Text = "";
        EMailSocio.Text = "";
        TelefonoConv.Text = "";
        TelefonoCel.Text = "";
        FechaAfiliacion.Text = "";
        FechaSolicitud.Text = "";
        NDocumento.Text = "";
        EDocumento.Enabled = false;
        NDocumento.Text = "0";
        NDocumento.Enabled = false;
        RegistrarSolicitud.Enabled = false;
        btn_ImprimeSolicitud.Enabled = false;
        FechaSolicitud.Text = "";
        Situacion.BackColor = System.Drawing.Color.White;
        btn_AgregarDiagnosticos.Visible = false;

        Monto.Text = "0";
        Porcentaje.Text = "0";
        MCalculado.Text = "0";

        InicializaSolicitud();
    }

    private void InicializaSolicitud()
    {
        FechaSolicitud.Text = "";
        Unidad.Text = "";
        Grado.Text = "";
        RazonSolicitud.Text = "";
        TiempoServicio.Text = "";
        TipoAyuda.Text = "";
        NombreBeneficiario.Text = "";
        TipoBeneficiario.Text = "";
        OtroBeneficiario.Text = "";
        Especialidad.Text = "";
        CasaDeSalud.Text = "";
        MedicoQueCertifica.Text = "";
        DocumentosRecibidos.Text = "";
        ObservSolicitud.Text = "";
        DTipoGrado.SelectedIndex = -1;
        DUnidad.SelectedIndex = -1;
        DTipoAyuda.SelectedIndex = -1;
        DTipoBeneficiario.SelectedIndex = -1;
        DEspecialidad.SelectedIndex = -1;
        DRazonSolicitud.SelectedIndex = -1;
    }
    protected void OtroSocio_Click(object sender, ImageClickEventArgs e)
    {
        Cedula.Enabled = true;
 //       btn_ImprimeSolicitud.Visible = false;
        InicializaDatos();
        Cedula.Focus();
        OcultaSolicitud();
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
                MontoSupGastosP= Convert.ToDecimal(reader.Rows[0][0]);
                MontoMaxAyudaP = Convert.ToDecimal(reader.Rows[0][1]);
                PorcenSuperP = Convert.ToDecimal(reader.Rows[0][2]);
                PorcenInferP = Convert.ToDecimal(reader.Rows[0][3]);
                MontoExterAyudaP = Convert.ToDecimal(reader.Rows[0][4]);
            }
            return;
        }
    }
    public string LeeNumSolicitud()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select UNumSolAyuda From ASeguridad.DatosEmpresa ";
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
    public void ActualNumSolicitud()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int UNumSol = Convert.ToInt32(NDocumento.Text) + 1;
            string sql = "Update ASeguridad.DatosEmpresa " 
                + "Set UNumSolAyuda='" + UNumSol + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
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
                MensajeR.Text =  CodSituacion + "SITUACION NO IDENTIFICADA";
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
        btn_ActualizaMedios.Visible = true;
        btn_ModificaMedios.Visible = true;
        btn_HabilitaIngresoSol.Visible = true;

        FechaConvertida = DateTime.Now;
        FechaSolicitud.Text = FechaConvertida.ToString("dd/MM/yyyy");

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
                Servicio.Text =(string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                Servicio.Visible = true;
                Nombres.Text = (string)reader.Rows[0][2] + " " + (string)reader.Rows[0][3];
                FechaAfiliacion.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][4]));
                if (!DBNull.Value.Equals(reader.Rows[0][5]))
                {
                    EMailSocio.Text = (string)reader.Rows[0][5];
                }

                if (!DBNull.Value.Equals(reader.Rows[0][6]))
                {
                    if ((string)reader.Rows[0][2] == "D" || (string)reader.Rows[0][2] == "F")
                    {
                        //                     Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][15]));
                        //                     EFechaDesafiliacion.Visible = true;
                        Situacion.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        if ((string)reader.Rows[0][6] == "N")
                        {
                            Situacion.BackColor = System.Drawing.Color.Aqua;
                        }
                        else
                        {
                            Situacion.BackColor = System.Drawing.Color.Salmon;
                        }
                    }
                }

                if (!DBNull.Value.Equals(reader.Rows[0][6]))
                {
                    Situacion.Text = LeeSituacion((string)reader.Rows[0][6]);
                }

                //               TipoIF.Text = (string)reader.Rows[0][7];
                
                if (!DBNull.Value.Equals(reader.Rows[0][12]))
                {
                    TelefonoConv.Text = (string)reader.Rows[0][12];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][13]))
                {
                    TelefonoCel.Text = (string)reader.Rows[0][13];
                }
                if ((string)reader.Rows[0][6] == "D")
                {
                    Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][14]));
 //                   RegistrarSolicitud.Enabled = true;
                    OtroSocio.Focus();
                }
                else
                {
                    FechaSolicitud.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
 //                   RegistrarSolicitud.Enabled = false;
                    FechaSolicitud.Focus();
                }
                ActivaNuevaAyuda.Visible = true;
                btn_ModificaMedios.Focus();
                OtroSocio.Visible = false;
                btn_HabilitaIngresoSol.Visible = true;
            }
            cmd.Connection.Close();
        }
    }
    protected void RegistrarSolicitud_Click(object sender, EventArgs e)
    {
        // MontoDeGastos   decimal(18, 2)  Checked
        // PorcentajeAyuda real    Checked
        // ValorSolicitado decimal(18, 2)  Checked
        NDocumento.Text = LeeNumSolicitud();
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Insert INTO ASocios.APSociosAyudas "
                    + "(NumSolicitud, Identificacion, Servicio, "
                    + "FechaSolicitud, TipoAyuda, TipoBeneficiario, "
                    + "NombreBeneficiario, OtroBeneficiario, RazonSolicitud, "
                    + "ObservSolicitud, Grado, Unidad, "
                    + "Especialidad, CasaSalud, MedicoCertifica, "
                    + "MontoDeGastos, PorcentajeAyuda, ValorSolicitado, "
                    + "NombresU, TelefonosRef, DocRecibidos, "
                    + "UserAlta, FechaCompAlta, CentroCostos )"
                    + " VALUES ('" + NDocumento.Text + "', '" + Cedula.Text + "', '" + Servicio.Text + "', "
                    + "'" + FechaSolicitud.Text + "', '" + TipoAyuda.Text + "', '" + TipoBeneficiario.Text + "',  "
                    + "'" + NombreBeneficiario.Text + "', '" + OtroBeneficiario.Text + "', '" + RazonSolicitud.Text + "', "
                    + "'" + ObservSolicitud.Text + "', '" + Grado.Text + "', '" + Unidad.Text + "',  "
                    + "'" + Especialidad.Text + "', '" + CasaDeSalud.Text + "', '" + MedicoQueCertifica.Text + "',  "
                    + "'" + Monto.Text + "', '" + Porcentaje.Text + "', '" + MCalculado.Text + "',  "
                    + "'" + Nombres.Text + "', '" + TelefonoConv.Text + " / " + TelefonoCel.Text + "', '" + DocumentosRecibidos.Text + "',  "
                    + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "','" + (string)(Session["CCostos"]) + "')";
            MensajeR.Text = NDocumento.Text + " " + sql;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
                ActualNumSolicitud();
                SolicitudesDeAyuda.DataSourceID = "";
                SolicitudesDeAyuda.DataSourceID = (string)(TabSolicitudesAyuda.UniqueID);
                MensajeR.Text = "Solicitud REGISTRADA CORRECTAMENTE";
                MensajeR.Visible = true;
                btn_ImprimeSolicitud.Enabled = true;
                btn_ImprimeSolicitud.Visible = true;
                btn_ImprimeSolicitud.Focus();
                NDocumento.Enabled = false;
                RegistrarSolicitud.Visible = false;
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString() + " " + sql;
            }
            cmd.Connection.Close();
 

        }
    }
    private void OcultaSolicitud()
    {
        btn_HabilitaIngresoSol.Visible = false;
        btn_ActualizaMedios.Visible = false;
        btn_ModificaMedios.Visible = false;
        btn_AgregarDiagnosticos.Visible = false;
        btn_AgregarEspecialidad.Visible = false;

        EDocumento.Enabled = false;
        NDocumento.Enabled = false;
        EFechaSolicitud.Visible = false;
        FechaSolicitud.Visible = false;
        EGrado.Visible = false;
        DTipoGrado.Visible = false;
        Grado.Visible = false;
        EUnidad.Visible = false;
        DUnidad.Visible = false;
        Unidad.Visible = false;
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

        EDocRecibida.Visible = false;
        DocumentosRecibidos.Visible = false;
        EObservSol.Visible = false;
        ObservSolicitud.Visible = false;

        EMonto.Visible = false;
        Monto.Visible = false;
        VerificaDatos.Visible = false;
        EPorcentaje.Visible = false;
        Porcentaje.Visible = false;
        EMCalculado.Visible = false;
        MCalculado.Visible = false;

        btn_ImprimeSolicitud.Visible = false;
        EDocumento.Visible = false;
        NDocumento.Visible = false;
    }

    private void DesplegaSolicitud()
    {
        EFechaSolicitud.Visible = true;
        FechaSolicitud.Visible = true;
        EGrado.Visible = true;
        DTipoGrado.Visible = true;
        Grado.Visible = true;
        EUnidad.Visible = true;
        Unidad.Visible = true;
        DUnidad.Visible = true;
        Unidad.Visible = true;
        EAserv.Visible = true;
        TiempoServicio.Visible = true;

        ETipoAyuda.Visible = true;
        DTipoAyuda.Visible = true;
        TipoAyuda.Visible = true;
        EDiagnostico.Visible = true;
        DRazonSolicitud.Visible = true;
        RazonSolicitud.Visible = true;
        EEspecialidad.Visible = true;
        DEspecialidad.Visible = true;
        Especialidad.Visible = true;

        ECasaSalud.Visible = true;
        DCasaSalud.Visible = true;
        CasaDeSalud.Visible = true;
        EMedicoCertifica.Visible = true;
        MedicoQueCertifica.Visible = true;

        EBeneficiario.Visible = true;
        DTipoBeneficiario.Visible = true;
        TipoBeneficiario.Visible = true;
        NombreBeneficiario.Visible = true;
        OtroBeneficiario.Visible = true;

        EDocRecibida.Visible = true;
        DocumentosRecibidos.Visible = true;
        EObservSol.Visible = true;
        ObservSolicitud.Visible = true;

        EMonto.Visible = true;
        Monto.Visible = true;
        VerificaDatos.Visible = true;
        btn_AgregarDiagnosticos.Visible = true;
        btn_AgregarEspecialidad.Visible = true;

        //       DUnidad.SelectedIndex = 0;
        //       DUnidad.SelectedItem.Text = "-----";

        //       DTipoAyuda.SelectedIndex = 0;
        //       DTipoAyuda.SelectedItem.Text = "-----";
    }

    protected void VerificaDatos_Click(object sender, ImageClickEventArgs e)
    {
        Unidad.Text = DUnidad.SelectedItem.Value;
        if (DRazonSolicitud.SelectedItem.Text != "-----")
        {
            RazonSolicitud.Text = DRazonSolicitud.SelectedItem.Text;
        }
        if (DEspecialidad.SelectedItem.Text != "-----")
        {
            Especialidad.Text = DEspecialidad.SelectedItem.Text;
        }
        if (DCasaSalud.SelectedItem.Text != "-----")
        {
            CasaDeSalud.Text = DCasaSalud.SelectedItem.Text;
        }
        TipoBeneficiario.Text = DTipoBeneficiario.SelectedItem.Value;
        Grado.Text = DTipoGrado.SelectedItem.Value;
        TipoAyuda.Text = DTipoAyuda.SelectedItem.Value;
        RazonSolicitud.Text = RazonSolicitud.Text.ToUpper();
  //      Unidad.Visible = true;
  //      RazonSolicitud.Visible = true;
  //      TipoBeneficiario.Visible = true;
  //      Grado.Visible = true;
  //      TipoAyuda.Visible = true;

        Monto.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        Porcentaje.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MCalculado.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        decimal MontoD, PorcentajeD, MCalculadoD;

        Monto.Text = Monto.Text.Replace(".", ",");
        Porcentaje.Text = Porcentaje.Text.Replace(".", ",");
        MCalculado.Text = MCalculado.Text.Replace(".", ",");

        MontoD = System.Convert.ToDecimal(Monto.Text);

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

        Monto.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);
        Porcentaje.Text = PorcentajeD.ToString("0.00", CultureInfo.InvariantCulture);
        MCalculado.Text = MCalculadoD.ToString("0.00", CultureInfo.InvariantCulture);

        EPorcentaje.Visible = true;
        Porcentaje.Visible = true;
        EMCalculado.Visible = true;
        MCalculado.Visible = true;
        //       Monto.Text = Monto.Text.Replace(".", ",");
        //       Porcentaje.Text = Porcentaje.Text.Replace(".", ",");
        //       MCalculado.Text = MCalculado.Text.Replace(".", ",");

        MensajeR.Text = "Errores en " + Monto.Text + " " + MontoD;

        if (Unidad.Text == "" || Unidad.Text == "99" || String.IsNullOrEmpty(Unidad.Text))
        {
            MensajeR.Text = MensajeR.Text + "Unidad, ";
        }
        else
        {
            if (RazonSolicitud.Text == "" || RazonSolicitud.Text == "-----" || String.IsNullOrEmpty(RazonSolicitud.Text))
            {
                MensajeR.Text = MensajeR.Text + " " + "Razon Solicitud, ";
            }
            else
            {
                if (TipoBeneficiario.Text == "" || TipoBeneficiario.Text == "--" || String.IsNullOrEmpty(TipoBeneficiario.Text))
                {
                    MensajeR.Text = MensajeR.Text + " " + "Tipo de Beneficiario, ";
                }
                else
                {
                    if (Grado.Text == "" || Grado.Text == "--" || String.IsNullOrEmpty(Unidad.Text))
                    {
                        MensajeR.Text = MensajeR.Text + " " + "Grado, ";
                    }
                    else
                    {
                        if (TipoAyuda.Text == "" || TipoAyuda.Text == "--" || String.IsNullOrEmpty(Unidad.Text))
                        {
                            MensajeR.Text = MensajeR.Text + " " + "Ayuda, ";
                        }
                        else
                        {
                            RegistrarSolicitud.Enabled = true;
                            RegistrarSolicitud.Visible = true;
                            EDocumento.Visible = true;
                            NDocumento.Visible = true;
                            NDocumento.Enabled = true;
                            NDocumento.Focus();
                        }
                    }
                }
            }
        }
    }





    protected void btn_ImprimeSolicitud_Click(object sender, EventArgs e)
    {
        //       ImpresionSolicitud();
        //       CierraReporte.Visible = true;
        Session["NumSolicitud"] = NDocumento.Text;
        Response.Write("<script>window.open('VisorSolicitudAyuda.aspx');</script>");
    }
    protected void DRazonSolicitud_SelectedIndexChanged(object sender, EventArgs e)
    {
        RazonSolicitud.Text = DRazonSolicitud.SelectedItem.Text; ;
    }

    protected void DEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Especialidad.Text = DEspecialidad.SelectedItem.Text;
    }

    protected void ActivaNuevaAyuda_Click(object sender, ImageClickEventArgs e)
    {
        OtroSocio.Visible = true;
        OtroSocio.Focus();
        ActivaNuevaAyuda.Visible = false;
    }

    protected void btn_HabilitaIngresoSol_Click(object sender, ImageClickEventArgs e)
    {
        DesplegaSolicitud();
        FechaSolicitud.Focus();
        btn_HabilitaIngresoSol.Visible = false;
    }

    protected void btn_ActualizaMedios_Click(object sender, ImageClickEventArgs e)
    {
        if (DOperadora.SelectedItem.Text != "----")
        {
            OperadoraCel.Text = DOperadora.SelectedItem.Value;
        }
        else
        {
            OperadoraCel.Text = "";
        }

        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE ASocios.[APSocios] SET "
                        + "[TelefonoConv] = '" + TelefonoConv.Text + "', "
                        + "[OperadoraCel] = '" + OperadoraCel.Text + "', "
                        + "[TelefonoCel] = '" + TelefonoCel.Text + "', "
                        + "[Email] = '" + EMailSocio.Text + "' "
                        + "WHERE [Identificacion] = '" + Cedula.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        btn_HabilitaIngresoSol.Focus();
    }
    protected void btn_ModificaMedios_Click(object sender, ImageClickEventArgs e)
    {
        EMailSocio.Enabled = true;
        TelefonoConv.Enabled = true;
        DOperadora.Enabled = true;
        TelefonoCel.Enabled = true;
        EMailSocio.Focus();
    }

    protected void btn_AgregarDiagnosticos_Click(object sender, ImageClickEventArgs e)
    {
        if (RazonSolicitud.Text == "" || String.IsNullOrEmpty(RazonSolicitud.Text))
        {
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
        }
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
}
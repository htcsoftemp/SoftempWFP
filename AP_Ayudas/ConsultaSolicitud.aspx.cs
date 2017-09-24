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
public partial class ConsultaSolicitud : System.Web.UI.Page
{
    static string CodServicio;
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        OtroSocio.Focus();
    }
    protected void InicializaDatos()
    {
        NSolicitud.Text = "";
        ANombres.Text = "";
 //       NombresSimilares.Text = "";
        Cedula.Text = "";
        BuscaPorNombre.Visible = false;
        BuscarSolicitud.Visible = false;
        EBuscaSolicitud.Visible = false;
        Nombres.Text = "";
        Servicio.Text = "";
        Situacion.Text = "";
        FechaAfiliacion.Text = "";
        FechaSolicitud.Text = "";
        NumSolicitud.Text = "";
        Unidad.Text = "";
        Grado.Text = "";
        TiempoServicio.Text = "";
        TipoAyuda.Text = "";
        RazonSolicitud.Text = "";
        TipoBeneficiario.Text = "";
        NombreBeneficiario.Text = "";
        Especialidad.Text = "";
        CasaDeSalud.Text = "";
        DocumentosRecibidos.Text ="";
        ObservSolicitud.Text = "";
    }

    protected void OtroSocio_Click(object sender, ImageClickEventArgs e)
    {
        InicializaDatos();

        EBuscaSolicitud.Visible = true;
        NSolicitud.Visible = true;
        NSolicitud.Focus();
        BuscarSolicitud.Visible = true;
        EIdentificacion.Visible = true;
        Cedula.Visible = true;
        BuscarSocios.Visible = true;
        Cedula.Enabled = true;
  //      Cedula.Focus();
        ImprimirSolicitud.Visible = false;
        ImprimirAprobacion.Visible = false;
        ImprimirFacturas.Visible = false;
        btn_ImprimeInformeAyudas.Visible = false;
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
        if (Cedula.Text =="")
        {
            EBuscaNom.Visible = true;
            ANombres.Visible = true;
            ParamNombres.Visible = true;
            ANombres.Enabled = true;
            ParamNombres.Enabled = true;
            ANombres.Focus();
        }
        else
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                SqlCommand cmd;
                string sql = "Select Identificacion, TipoSocio, "
                                    + "Apellidos, Nombres, "
                                    + "FechaAfil, Email, "
                                    + "SituacionSocio, "
                                    + "TipoIF, NombreIF, Codigo_BCE, "
                                    + "TipoCuentaIF, NumCuenta, "
                                    + "TelefonoConv, TelefonoCel, FechaDesafil "
                                    + "From ASocios.APSocios  Where Identificacion = '"
                                    + Cedula.Text + "'";
                cmd = new SqlCommand(sql, conn);
                try
                {
                    cmd.Connection.Open();
                    DataTable reader = new DataTable();
                    reader.Load(cmd.ExecuteReader());
                    if (reader.Rows.Count == 0)
                    {
                        // do something if the query return no rows
                        // you may insert the relevant redirection you asked for
                        MensajeR.Text = "SOCIO NO REGISTRADO, CON ESA IDENTIFICACION, BUSCAR POR NOMBRES";
                        MensajeR.Visible = true;
                        ANombres.Enabled = true;
                        ParamNombres.Enabled = true;
                        ANombres.Focus();
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
                        if ((string)reader.Rows[0][6] == "D"
                            || (string)reader.Rows[0][6] == "F"
                            || (string)reader.Rows[0][6] == "M"
                            || (string)reader.Rows[0][6] == "I")
                        {
                            Situacion.BackColor = System.Drawing.Color.Red;
                        }

   //                     Situacion.Text = LeeSituacion((string)reader.Rows[0][6]);


                        if ((string)reader.Rows[0][6] == "D" || (string)reader.Rows[0][6] == "F")
                        {
                            //                     Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][15]));
                            //                     EFechaDesafiliacion.Visible = true;
   //                         DSituacion.BackColor = System.Drawing.Color.Red;
                            Situacion.BackColor = System.Drawing.Color.Red;
   //                         FechaDesafiliacion.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][15]));
   //                         FechaDesafiliacion.Visible = true;
                        }
                        else
                        {
                            if ((string)reader.Rows[0][6] == "N")
                            {

                                //                      EFechaDesafiliacion.Visible = false;
  //                              DSituacion.BackColor = System.Drawing.Color.Aqua;
                                Situacion.BackColor = System.Drawing.Color.Aqua;
  //                              FechaDesafiliacion.Visible = false;
                            }
                            else
                            {
  //                              DSituacion.BackColor = System.Drawing.Color.Salmon;
                                Situacion.BackColor = System.Drawing.Color.Red;
  //                              FechaDesafiliacion.Visible = false;
                            }
                        }

                        if ((string)reader.Rows[0][6] == "D")
                        {
                            Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][14]));
                            Situacion.BackColor = System.Drawing.Color.Red;
                            OtroSocio.Focus();
                        }
                        else
                        {
                            OtroSocio.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                //    cmd.Connection.Close();
                    MensajeR.Text = ex.ToString();
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
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
                if ((string)reader.Rows[0][6] == "D"
                    || (string)reader.Rows[0][6] == "F"
                    || (string)reader.Rows[0][6] == "M"
                    || (string)reader.Rows[0][6] == "I")
                {
                    Situacion.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    Situacion.BackColor = System.Drawing.Color.Aqua;
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
                    + "ValorAprobado, TotalPagado, Identificacion "
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


                if (!DBNull.Value.Equals(reader.Rows[0][0]))
                {
                    Grado.Text = (string)reader.Rows[0][0];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][1]))
                {
                    Unidad.Text = (string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                }
                if (!DBNull.Value.Equals(reader.Rows[0][3]))
                {
                    DocumentosRecibidos.Text = (string)reader.Rows[0][3];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][7]))
                {
                    Especialidad.Text = (string)reader.Rows[0][7];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][8]))
                {
                    CasaDeSalud.Text = (string)reader.Rows[0][8];
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
                Cedula.Text = (string)reader.Rows[0][20]; ;
                DesplegaDatImpresion();
            }
            cmd.Connection.Close();
            ImprimirSolicitud.Visible = true;
           ImprimirFacturas.Visible = true;
            ImprimirAprobacion.Visible = true;
            btn_ImprimeInformeAyudas.Visible = true;
        }
    }

    private void OcultaDatImpresion()
    {
        EDocumento.Visible = false;
        NumSolicitud.Visible = false;
        EAfiliado.Visible = false;
        FechaAfiliacion.Visible = false;
        Situacion.Visible = false;
    }
    private void DesplegaDatImpresion()
    {
        EDocumento.Visible = true;
        NumSolicitud.Visible = true;
        EAfiliado.Visible = true;
        FechaAfiliacion.Visible = true;
        Situacion.Visible = true;
    }

    protected void ParamNombres_Click(object sender, ImageClickEventArgs e)
    {
        if (ANombres.Text == "")
        {
            MensajeR.Text = "Parametro invalido para buscar por nombre";
            EBuscaSolicitud.Visible = true;
            NSolicitud.Visible = true;
            BuscarSolicitud.Visible = true;
            MensajeR.Visible = true;
            NSolicitud.Focus();
        }
        else
        {
            if (ANombres.Text.Length < 5)
            {
                MensajeR.Text = "Parametro para buscar nombre debe ser mas de 5 caracteres";
                ANombres.Enabled = true;
                ParamNombres.Enabled = true;
                MensajeR.Visible = true;
                Cedula.Focus();
            }
            else
            {
                NombresSimilares.Visible = true;
                BuscaPorNombre.Visible = true;
            }
        }
    }

    protected void BuscaPorNombre_Click(object sender, ImageClickEventArgs e)
    {
        if (ANombres.Text == "")
        {
            NSolicitud.Visible = true;
            NSolicitud.Enabled = true;
            BuscarSolicitud.Visible = true;
            BuscarSolicitud.Enabled = true;
            NSolicitud.Focus();
        }
        else
        { 
            Cedula.Text = NombresSimilares.SelectedItem.Value;
            LeeDatosSocio();
        }
    }

    protected void RegistrarProceso_Click(object sender, EventArgs e)
    {
            //          string CodTran = "3110";
            //          FechaPago.Text = (string)(DateTime.Now.ToShortDateString());
            // IdenProp nvarchar(15)    Unchecked
            // NumSolicitud    int Unchecked
            // FechaProceso    date    Unchecked
            // Secuencial  smallint    Unchecked
            // Proceso nvarchar(MAX)   Checked
            // FechaProxProb   datetime    Checked
            // ResultadoProceso    nvarchar(50)    Checked
            // Observacion nvarchar(MAX)   Checked
            // UserProceso nvarchar(15)    Checked
            // FechaCompProceso    datetime    Checked

        MensajeR.Visible = true;
        SolicitudesDeAyuda.DataSourceID = "";
        SolicitudesDeAyuda.DataSourceID = (string)(TabSolicitudesAyuda.UniqueID);
        LeeSolAyuda();
    }

    protected void SolicitudesDeAyuda_SelectedIndexChanged(object sender, EventArgs e)
    {
        int NF;
        MensajeR.Text = "Error en ";
        decimal ValorEntregado, ValorAprobado;
        NF = SolicitudesDeAyuda.SelectedIndex;
        if ((string.IsNullOrEmpty(SolicitudesDeAyuda.Rows[NF].Cells[7].Text))
            || (SolicitudesDeAyuda.Rows[NF].Cells[6].Text == "&nbsp;"))
        {
            MensajeR.Text = "Valor Aprobado,";
        }
        else
        {
            ValorAprobado = Convert.ToDecimal(SolicitudesDeAyuda.Rows[NF].Cells[7].Text);
        }

        if ((string.IsNullOrEmpty(SolicitudesDeAyuda.Rows[NF].Cells[10].Text))
                || (SolicitudesDeAyuda.Rows[NF].Cells[10].Text == "&nbsp;"))
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

            //        if (ValorEntregado <= 0)
            //        {
            NumSolicitud.Text = SolicitudesDeAyuda.Rows[NF].Cells[1].Text;
            LeeSolAyuda();
            //        }
            //        else
            //        {
            //           MensajeR.Text = "Ayuda ya fue Entregada, no se puede modificar";
            //           NumSolicitud.Text = SolicitudesDeAyuda.Rows[NF].Cells[1].Text;
            //        }
            //       InicializaModificaciones();
            //       DesplegaDatGenMod();

        // }
    }


    
    protected void ImprimirSolicitud_Click(object sender, EventArgs e)
    {

 //       ImpresionSolicitud();
 //       SolicitudAprobada.Visible = false;
 //       CierraReporte.Visible = true;
        Session["NumSolicitud"] = NumSolicitud.Text;
        MensajeR.Text = (string)(Session["NumSolicitud"]);
        Response.Write("<script>window.open('VisorSolicitudAyuda.aspx');</script>");

        //        Session["NumSolicitud"] = NumSolicitud.Text;
        //        Response.Write("<script>window.open('DesplegaRep.aspx', '_self');</script>");

    }

    protected void ImprimirAprobacion_Click(object sender, EventArgs e)
    {
        //     ImpresionAprobacion();
        //     SolicitudAyuda.Visible = false;
        //     CierraReporte.Visible = true;
        Session["NumSolicitud"] = NumSolicitud.Text;
        Response.Write("<script>window.open('VisorSolicitudAprobada.aspx');</script>");
    }
    protected void btn_ImprimeInformeAyudas_Click(object sender, EventArgs e)
    {
        Session["NumSolicitud"] = NumSolicitud.Text;
        Response.Write("<script>window.open('VisorInformeAyuda.aspx');</script>");
    }
    protected void BuscarSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        if (NSolicitud.Text == "")
        {
            EBuscaNom.Visible = true;
            ANombres.Visible = true;
            ParamNombres.Visible = true;
            ANombres.Enabled = true;
            ParamNombres.Enabled = true;
            ANombres.Focus();
        }
        else
        { 
            NumSolicitud.Text = NSolicitud.Text;
            LeeSolAyuda();
            LeeDatosSocio();
            ImprimirAprobacion.Visible = true;
            ImprimirSolicitud.Visible = true;
            btn_ImprimeInformeAyudas.Visible = true;
        }
    }

    protected void ImprimirFacturas_Click(object sender, EventArgs e)
    {
        Session["NumSolicitud"] = NumSolicitud.Text;
        Response.Write("<script>window.open('VisorAyudaFacturas.aspx');</script>");

    }

    protected void NSolicitud_TextChanged(object sender, EventArgs e)
    {
        if (NSolicitud.Text == "")
        {
            EBuscaNom.Visible = true;
            ANombres.Visible = true;
            ParamNombres.Visible = true;
            ANombres.Enabled = true;
            ParamNombres.Enabled = true;
            ANombres.Focus();
        }
        else
        {
            NumSolicitud.Text = NSolicitud.Text;
            LeeSolAyuda();
            LeeDatosSocio();
            ImprimirAprobacion.Visible = true;
            ImprimirSolicitud.Visible = true;
            btn_ImprimeInformeAyudas.Visible = true;
        }
    }
}

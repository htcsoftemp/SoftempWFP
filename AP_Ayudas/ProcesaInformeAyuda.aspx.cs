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
public partial class AP_Ayudas_ProcesaInformeAyuda : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;

    static string AsuntoEG="Ayuda por Emfermedad Grave solicitada por el Socio/a: ";
    static string AsuntoCD = "Ayuda por Calamidad Doméstica solicitada por el Socio/a: ";

    static string AntecedentesEG1 = "Acude a esta Entidad el socio/a, solicitando una Ayuda Economica ";
    static string AntecedentesEG2 = "para solventar los gastos por enfermedad grave, ";
    static string AntecedentesEG3 = "para lo cual presenta la siguiente documentación: ";
    static string Doc1 = "Solicitud dirigida al Señor Presidente de la Fundación Polinal, ";
    static string Doc2 = "Certificado Médico, ";
    static string Doc3 = "Facturas de los Gastos realizados por Hospitalización, Medicación y Exámenes, ";
    static string Doc4A = "Partida de Nacimiento, ";
    static string Doc4B = "Partida de Matrimonio, ";
    static string Doc4C = "Informacion Sumaria, ";
    static string Doc4D = "Certificación de Procesamiento de Datos, ";

    static string Doc5 = "Copias de Cédulas y Credenciales respectivas";


    static string VerificacionDocEG1 = "El/a Sr/a. ";
    static string VerificacionDocEG1H = "El/a menor: ";
    static string VerificacionDocEG2 = "fue atendido en el ";
    static string VerificacionDocEG3 = "por el ";
    static string VerificacionDocEG4 = "en la Especialidad de ";
    static string VerificacionDocEG5 = "el paciente presenta como Diagnóstico:";
    static string VerificacionDocEG6 = "emfermedad que es considerada como grave.";

    static string RecomendacionesEG1 = "Una vez analizada e investigada la documentación se recomienda a los miembros del Consejo Ejecutivo, concederle al socio/a, ";
    static string RecomendacionesEG2 = " una ayuda económica de USD. ";
    static string RecomendacionesEG3 = " que corresponde al ";
    static string RecomendacionesEG4 = "% de acuerdo a lo estipulado en el Reglamento de Ayudas por Emfermedad Grave y/o Calamidad Doméstica Vigente.";
    static string RecomendacionesEG4M = " MONTO MAXIMO de acuerdo a lo estipulado en el Reglamento de Ayudas por Emfermedad Grave y/o Calamidad Doméstica Vigente.";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NumSolicitud.Text = (string)(Session["NumSolicitud"]);
            if (NumSolicitud.Text == "")
            {
                NumSolicitud.Focus();
            }
            else
            {
                LeeInforme();
                btn_ActualizarInformeAyudas.Focus();
            }
        }
        
    }
    public string LeeGrado(String CodGrado)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select DetalleGrado From ASocios.APSociosGrados "
                         + " Where CodigoGrado = '" + CodGrado + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodGrado + "GRADO NO IDENTIFICADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Grado";
            }
            else
            {
                cmd.Connection.Close();
                return (string)reader.Rows[0][0];
            }
        }
    }
    private void ArmarInforme()
    {
        switch (TipoAyuda.Text)
        {
            case "EG":
                Asunto.Text = AsuntoEG + Grado.Text + "  " + Nombres.Text + ".";
                break;
            case "CD":
                Asunto.Text = AsuntoCD + Grado.Text + "  " + Nombres.Text + ".";
                break;
            case "EX":
                Asunto.Text = AsuntoEG + Grado.Text + "  " + Nombres.Text + ".";
                break;
            default:
                Asunto.Text = AsuntoEG + Grado.Text + "  " + Nombres.Text + ".";
                break;
        }

        Antecedentes.Text = AntecedentesEG1 + AntecedentesEG2 + AntecedentesEG3 + " " + Doc1 + Doc2 + Doc3;
        switch (TipoBeneficiario.Text)
        {
            case "H":
                Antecedentes.Text += Doc4A;
                break;
            case "C":
                Antecedentes.Text += Doc4B;
                break;
            case "CV":
                Antecedentes.Text += Doc4C;
                break;
            case "P":
                Antecedentes.Text += Doc4D;
                break;
            default:
                break;
        }
        Antecedentes.Text += Doc5;

        switch (TipoBeneficiario.Text)
        {
            case "H":
                VerificacionDatos.Text = VerificacionDocEG1H+ NombreBeneficiario.Text;
                break;
            case "C":
                VerificacionDatos.Text = VerificacionDocEG1 + "  " + Nombres.Text;
                break;
            case "CV":
                VerificacionDatos.Text = VerificacionDocEG1 + "  " + Nombres.Text;
                break;
            case "P":
                VerificacionDatos.Text = VerificacionDocEG1 + "  " + Nombres.Text;
                break;
            default:
                VerificacionDatos.Text = VerificacionDocEG1 + Grado.Text + "  " + Nombres.Text;
                break;
        }
            VerificacionDatos.Text = VerificacionDatos.Text + " " + VerificacionDocEG2 + " " + CasaDeSalud.Text
            + " " + VerificacionDocEG3 + " " + MedicoQueCertifica.Text
            + " " + VerificacionDocEG4 + " " + Especialidad.Text
            + " " + VerificacionDocEG5 + " " + RazonSolicitud.Text + " " + VerificacionDocEG6;

        if (Convert.ToDecimal(Porcentaje.Text) == 40 || Convert.ToDecimal(Porcentaje.Text) == 50)
        {
            Recomendacion.Text = RecomendacionesEG1 + Grado.Text + "  " + Nombres.Text 
                               + RecomendacionesEG2 + MCalculado.Text 
                               + RecomendacionesEG3 + Porcentaje.Text 
                               + RecomendacionesEG4;
        }
        else
        {
            Recomendacion.Text = RecomendacionesEG1 + Grado.Text + "  " + Nombres.Text 
                               + RecomendacionesEG2 + MCalculado.Text 
                               + RecomendacionesEG3 
                               + RecomendacionesEG4M;
        }
    }
    private void ImpresionAprobacion()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                int SolicitudNum;
                String sql;
                sql = "DROP VIEW [dbo].[VistaInformeAyuda]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {

                }
                SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
                sql = "CREATE VIEW [dbo].[VistaInformeAyuda] AS "
                    + "SELECT  ASocios.APSociosAyudas.NumSolicitud, ASocios.APSociosAyudas.Identificacion, "
                    + "ASocios.APSocios.NombresU, ASocios.APSociosAyudas.Grado, ASocios.APSociosAyudasInformes.Doc1, "
                    + "ASocios.APSociosAyudasInformes.Doc2, ASocios.APSociosAyudasInformes.Doc3, ASocios.APSociosAyudasInformes.Doc4, "
                    + "ASocios.APSociosAyudasInformes.Doc5, ASocios.APSociosAyudas.CasaSalud, "
                    + "ASocios.APSociosAyudas.Especialidad, ASocios.APSociosAyudas.RazonSolicitud, "
                    + "ASocios.APSociosAyudas.MedicoCertifica, ASocios.APSociosAyudas.ValorAprobado, "
                    + "ASocios.APSociosAyudas.PorcentajeAyuda "
                    + "FROM ASocios.APSociosAyudas INNER JOIN "
                    + "ASocios.APSociosAyudasInformes ON ASocios.APSociosAyudas.NumSolicitud = ASocios.APSociosAyudasInformes.NumSolicitud INNER JOIN"
                    + "ASocios.APSocios ON ASocios.APSociosAyudas.Identificacion = ASocios.APSocios.Identificacion"
                    + "WHERE ASocios.APSociosAyudas.NumSolicitud =" + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "Select NumSolicitud, Identificacion, Grado, NombresU, Doc1, Doc2, Doc3, Doc4, Doc5 "
                         + "CasaSalud, Especialidad, RazonSolicitud, MedicoCertifica, ValorAprobado, PorcentajeAyuda "
                         + "From VistaInformeAyuda  Where NumSolicitud = "
                         + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                //              cmd.Connection.Open();
                //              cmd = new SqlCommand(sql, conn);
                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("SolicitudAyudaPre.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
  //                  CierraReporte.Visible = true;
                } // end using adaptador
            } // end try
            catch (Exception ex)
            {
                MensajeR.Text = NumSolicitud.Text + ex.Message;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            } // end using comando
        }
    }

    protected void BuscarInformeSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        LeeInforme();
    }
    private void LeeSolicitud()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int SolicitudNum;
            SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
            string sql = "Select NumSolicitud, Identificacion, Grado, NombresU,  "
                         + "CasaSalud, Especialidad, RazonSolicitud, MedicoCertifica, "
                         + "ValorAprobado, PorcentajeAyuda, "
                         + "Asunto, Antecedentes, VerificacionDoc, CertificacionInfor, "
                         + "Recomendaciones, TipoBeneficiario, TipoAyuda, NombreBeneficiario "
                         + "From VistaGenInformesAyudas  Where NumSolicitud = "
                         + SolicitudNum;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = "INFORME NO REGISTRADO";
                MensajeR.Visible = true;
  //              CreaInforme();
            }
            else
            {
                if (!DBNull.Value.Equals(reader.Rows[0][1]))
                {
                    Cedula.Text = (string)reader.Rows[0][1];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][2]))
                {
                    Grado.Text = LeeGrado((string)reader.Rows[0][2]);
                }
                if (!DBNull.Value.Equals(reader.Rows[0][3]))
                {
                    Nombres.Text = (string)reader.Rows[0][3];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][4]))
                {
                    CasaDeSalud.Text = (string)reader.Rows[0][4];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][5]))
                {
                    Especialidad.Text = (string)reader.Rows[0][5];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][6]))
                {
                    RazonSolicitud.Text = (string)reader.Rows[0][6];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][7]))
                {
                    MedicoQueCertifica.Text = (string)reader.Rows[0][7];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][8]))
                {
                    MCalculado.Text = (string.Format("{0:0.00}", reader.Rows[0][8]));
                }
                if (!DBNull.Value.Equals(reader.Rows[0][9]))
                {
                    Porcentaje.Text = (string.Format("{0:0}", reader.Rows[0][9]));
                }
                if (!DBNull.Value.Equals(reader.Rows[0][10]))
                {
                    Asunto.Text = (string)reader.Rows[0][10];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][11]))
                {
                    Antecedentes.Text = (string)reader.Rows[0][11];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][12]))
                {
                    VerificacionDatos.Text = (string)reader.Rows[0][12];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][13]))
                {
                    Certificacion.Text = (string)reader.Rows[0][13];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][14]))
                {
                    Recomendacion.Text = (string)reader.Rows[0][14];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][15]))
                {
                    TipoBeneficiario.Text = (string)reader.Rows[0][15];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][16]))
                {
                    TipoAyuda.Text = (string)reader.Rows[0][16];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][17]))
                {
                    NombreBeneficiario.Text = (string)reader.Rows[0][17];
                }
            }
            cmd.Connection.Close();
        }
    }

    private void LeeInforme()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int SolicitudNum;
            SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
            string sql = "Select NumSolicitud, Identificacion, Grado, NombresU,  "
                         + "CasaSalud, Especialidad, RazonSolicitud, MedicoCertifica, "
                         + "ValorAprobado, PorcentajeAyuda, "
                         + "Asunto, Antecedentes, VerificacionDoc, CertificacionInfor, "
                         + "Recomendaciones, TipoBeneficiario, TipoAyuda, NombreBeneficiario "
                         + "From VistaGenInformesAyudas  Where NumSolicitud = "
                         + SolicitudNum;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = "INFORME NO REGISTRADO";
                MensajeR.Visible = true;
                CreaInforme();
                LeeSolicitud();
            }
            else
            {
                if (!DBNull.Value.Equals(reader.Rows[0][1]))
                {
                    Cedula.Text = (string)reader.Rows[0][1];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][2]))
                {
                    Grado.Text = LeeGrado((string)reader.Rows[0][2]);
                }
                if (!DBNull.Value.Equals(reader.Rows[0][3]))
                {
                    Nombres.Text = (string)reader.Rows[0][3];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][4]))
                {
                    CasaDeSalud.Text = (string)reader.Rows[0][4];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][5]))
                {
                    Especialidad.Text = (string)reader.Rows[0][5];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][6]))
                {
                    RazonSolicitud.Text = (string)reader.Rows[0][6];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][7]))
                {
                    MedicoQueCertifica.Text = (string)reader.Rows[0][7];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][8]))
                {
                    MCalculado.Text = (string.Format("{0:0.00}", reader.Rows[0][8]));
                }
                if (!DBNull.Value.Equals(reader.Rows[0][9]))
                {
                    Porcentaje.Text = (string.Format("{0:0}", reader.Rows[0][9]));
                }
                if (!DBNull.Value.Equals(reader.Rows[0][10]))
                {
                    Asunto.Text = (string)reader.Rows[0][10];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][11]))
                {
                    Antecedentes.Text = (string)reader.Rows[0][11];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][12]))
                {
                    VerificacionDatos.Text = (string)reader.Rows[0][12];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][13]))
                {
                    Certificacion.Text = (string)reader.Rows[0][13];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][14]))
                {
                    Recomendacion.Text = (string)reader.Rows[0][14];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][15]))
                {
                    TipoBeneficiario.Text = (string)reader.Rows[0][15];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][16]))
                {
                    TipoAyuda.Text = (string)reader.Rows[0][16];
                }
                if (!DBNull.Value.Equals(reader.Rows[0][17]))
                {
                    NombreBeneficiario.Text = (string)reader.Rows[0][17];
                }
            }
            cmd.Connection.Close();
        }
    }

    protected void btn_ArmaInforme_Click(object sender, ImageClickEventArgs e)
    {
        ArmarInforme();
    }
    private void CreaInforme()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {

            string sql = "Insert INTO ASocios.APSociosAyudasInformes "
                        + "(NumSolicitud, Asunto, "
                        + "Antecedentes, VerificacionDoc, "
                        + "CertificacionInfor, Recomendaciones, "
                        + "UserAlta, FechaCompAlta )"
                        + " VALUES ('" + NumSolicitud.Text + "', " + "'" + Asunto.Text + "', "
                        + "'" + Antecedentes.Text + "', " + "'" + VerificacionDatos.Text + "', "
                        + "'" + Certificacion.Text + "', " + "'" + Recomendacion.Text + "', "
                        + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
    protected void btn_ActualizarInformeAyudas_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE ASocios.APSociosAyudasInformes "
                    + "SET Asunto='" + Asunto.Text
                    + "', Antecedentes='" + Antecedentes.Text
                    + "', VerificacionDoc='" + VerificacionDatos.Text
                    + "', CertificacionInfor='" + Certificacion.Text
                    + "', Recomendaciones='" + Recomendacion.Text + "' "
                    + "Where NumSolicitud = '" + NumSolicitud.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                MensajeR.Text = "Proceso OK" + sql;
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + ex.ToString();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    protected void btn_ImprimeInformeAyudas_Click(object sender, EventArgs e)
    {
        Session["NumSolicitud"] = NumSolicitud.Text;
        Response.Write("<script>window.open('VisorInformeAyuda.aspx');</script>");
    }

    protected void OtroSocio_Click(object sender, ImageClickEventArgs e)
    {
        NumSolicitud.Text = "";
        TipoAyuda.Text = "";
        Cedula.Text = "";
        Grado.Text = "";
        Nombres.Text = "";
        RazonSolicitud.Text = "";
        MCalculado.Text = "";
        Porcentaje.Text = "";
        TipoBeneficiario.Text = "";
        Especialidad.Text = "";
        CasaDeSalud.Text = "";
        MedicoQueCertifica.Text = "";
        Asunto.Text = "";
        Antecedentes.Text = "";
        VerificacionDatos.Text = "";
        Certificacion.Text = "";
        Recomendacion.Text = "";

        NumSolicitud.Focus();
    }
}
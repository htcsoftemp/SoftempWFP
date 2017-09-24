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
public partial class AP_Ayudas_VisorSolicitudAprobada : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            MensajeR.Text= (string)(Session["NumSolicitud"]);
            NumSolicitud.Text = (string)(Session["NumSolicitud"]);
            SolicitudAyuda.Visible = true;
            ImpresionAprobacion();
        }
        else
        {
            NumSolicitud.Text = (string)(Session["NumSolicitud"]);
            SolicitudAyuda.Visible = true;
            ImpresionAprobacion();
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
                sql = "DROP VIEW [AAyudas].[VistaSolAyuda]";
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
                sql = "CREATE VIEW [AAyudas].[VistaSolAyuda] AS "
                    + "SELECT  ASocios.APSociosAyudas.NumSolicitud, ASocios.APSociosAyudas.Identificacion, "
                    + "ASocios.APSociosAyudas.FechaSolicitud, ASocios.APSociosAyudas.TipoAyuda, "
                    + "ASocios.APSociosAyudas.TipoBeneficiario, ASocios.APSociosAyudas.RazonSolicitud, "
                    + "ASocios.APSociosAyudas.Grado, ASocios.APSociosAyudas.Unidad, "
                    + "ASocios.APSociosAyudas.ObservSolicitud, ASocios.APSocios.NombresU, ASocios.APSocios.TipoSocio "
                    + "FROM ASocios.APSocios INNER JOIN ASocios.APSociosAyudas "
                    + "ON ASocios.APSocios.Identificacion = ASocios.APSociosAyudas.Identificacion "
                    + "WHERE ASocios.APSociosAyudas.NumSolicitud =" + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "Select NumSolicitud, Identificacion, Grado, NombresU, "
                         + "FechaSolicitud, TipoAyuda, RazonSolicitud, Unidad "
                         + "From AAyudas.VistaSolAyuda  Where NumSolicitud = "
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
                    SolicitudAyuda.ReportSource = reporte;
                    SolicitudAyuda.DataBind();
                    SolicitudAyuda.RefreshReport();
                    SolicitudAyuda.Visible = true;
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
}
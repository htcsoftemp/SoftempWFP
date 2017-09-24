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
public partial class AP_Ayudas_VisorInformeAyuda : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        NumSolicitud.Text = (string)(Session["NumSolicitud"]);
        InformeAyuda.Visible = true;
        ImpresionInforme();
    }
    protected void CierraReporte_Click(object sender, ImageClickEventArgs e)
    {
        InformeAyuda.Visible = false;
    }
    private void ImpresionInforme()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                int SolicitudNum;
                String sql;
                sql = "DROP VIEW [AAyudas].[VistaInformeAyudas]";
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
                sql = "CREATE VIEW [AAyudas].[VistaInformeAyudas] AS "
                    + "SELECT  ASocios.APSociosAyudasInformes.NumSolicitud, "
                    + "ASocios.APSociosAyudasInformes.Asunto, "
                    + "ASocios.APSociosAyudasInformes.Antecedentes, "
                    + "ASocios.APSociosAyudasInformes.VerificacionDoc, "
                    + "ASocios.APSociosAyudasInformes.CertificacionInfor, "
                    + "ASocios.APSociosAyudasInformes.Recomendaciones "
                    + "FROM ASocios.APSociosAyudasInformes "
                    + "WHERE ASocios.APSociosAyudasInformes.NumSolicitud =" + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "SELECT NumSolicitud, Asunto, Antecedentes, VerificacionDoc, CertificacionInfor, Recomendaciones "
                    + "FROM ASocios.APSociosAyudasInformes "
                    + "WHERE NumSolicitud =" + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                //              cmd.Connection.Open();
                //              cmd = new SqlCommand(sql, conn);
                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("SolicitudAyudaInforme.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    InformeAyuda.ReportSource = reporte;
                    InformeAyuda.DataBind();
                    InformeAyuda.RefreshReport();
                    InformeAyuda.Visible = true;
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

    }
}
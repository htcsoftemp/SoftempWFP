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
        NumSolicitud.Text = (string)(Session["NumSolicitud"]);
        SolicitudAprobada.Visible = true;
        ImpresionAprobacion();
    }
    protected void CierraReporte_Click(object sender, ImageClickEventArgs e)
    {
        SolicitudAprobada.Visible = false;
    }
    private void ImpresionAprobacion()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                int SolicitudNum;
                String sql;
                sql = "DROP VIEW [AAyudas].[VistaAprobAyuda]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {

                }
/*
SELECT ASocios.APSociosAyudas.NumSolicitud, ASocios.APSociosAyudas.Identificacion, ASocios.APSociosAyudas.FechaSolicitud, ASocios.APSociosAyudas.TipoAyuda, ASocios.APSociosAyudas.TipoBeneficiario, 
                         ASocios.APSociosAyudas.RazonSolicitud, ASocios.APSociosAyudas.Grado, ASocios.APSociosAyudas.Unidad, ASocios.APSociosAyudas.ObservSolicitud, ASocios.APSocios.NombresU, 
                         ASocios.APSocios.TipoSocio, ASocios.APSociosAyudas.NombreBeneficiario, ASocios.APSociosAyudas.Especialidad, ASocios.APSociosAyudas.CasaSalud, ASocios.APSociosAyudas.MedicoCertifica, 
                         ASocios.APSociosAyudas.MontoDeGastos, ASocios.APSociosAyudas.PorcentajeAyuda, ASocios.APSociosAyudas.ValorSolicitado, ASocios.APSociosAyudas.ValorAprobado, 
                         ASocios.APSociosAyudas.ValorAprobado0, ASocios.APSociosAyudas.Beneficiario0, ASocios.APSociosAyudas.ValorAprobado1, ASocios.APSociosAyudas.Beneficiario1, ASocios.APSociosAyudas.ValorAprobado2, 
                         ASocios.APSociosAyudas.Beneficiario2, ASocios.APSociosAyudas.FechaAprobRechaz, ASocios.APSociosAyudas.ReferAprobRechaz, ASocios.APSociosAyudas.MontoFacturas, 
                         ASocios.APSociosAyudas.MontoProformas, ASocios.APSociosAyudas.MontoDonaciones, 
                         ASocios.APSociosAyudas.PorcentajeProformas, ASocios.APSociosAyudas.PorcentajeFacturas, 
                         ASocios.APSociosAyudas.AyudaFacturas, ASocios.APSociosAyudas.AyudaProformas
FROM            ASocios.APSocios INNER JOIN
                         ASocios.APSociosAyudas ON ASocios.APSocios.Identificacion = ASocios.APSociosAyudas.Identificacion
WHERE(ASocios.APSociosAyudas.NumSolicitud = 200483)
*/

                SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
                sql = "CREATE VIEW [AAyudas].[VistaAprobAyuda] AS "
                    + "SELECT  ASocios.APSociosAyudas.NumSolicitud, ASocios.APSociosAyudas.Identificacion, "
                    + "ASocios.APSociosAyudas.FechaSolicitud, ASocios.APSociosAyudas.TipoAyuda, "
                    + "ASocios.APSociosAyudas.TipoBeneficiario, ASocios.APSociosAyudas.RazonSolicitud, "
                    + "ASocios.APSociosAyudas.Grado, ASocios.APSociosAyudas.Unidad, "
                    + "ASocios.APSociosAyudas.ObservSolicitud, ASocios.APSocios.NombresU, ASocios.APSocios.TipoSocio, "
                    + "NombreBeneficiario, Especialidad, CasaSalud, "
                    + "MedicoCertifica, MontoDeGastos, PorcentajeAyuda, "
                    + "ValorSolicitado, ValorAprobado, ValorAprobado0, Beneficiario0, "
                    + "ValorAprobado1, Beneficiario1, ValorAprobado2, Beneficiario2, "
                    + "FechaAprobRechaz, ReferAprobRechaz,  "
                    + "ASocios.APSociosAyudas.MontoFacturas, "
                    + "ASocios.APSociosAyudas.MontoProformas, "
                    + "ASocios.APSociosAyudas.MontoDonaciones, "
                    + "ASocios.APSociosAyudas.PorcentajeProformas, "
                    + "ASocios.APSociosAyudas.PorcentajeFacturas, "
                    + "ASocios.APSociosAyudas.AyudaFacturas, "
                    + "ASocios.APSociosAyudas.AyudaProformas "
                    + "FROM ASocios.APSocios INNER JOIN ASocios.APSociosAyudas "
                    + "ON ASocios.APSocios.Identificacion = ASocios.APSociosAyudas.Identificacion "
                    + "WHERE ASocios.APSociosAyudas.NumSolicitud =" + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "Select NumSolicitud, Identificacion, Grado, NombresU, "
                        + "FechaSolicitud, TipoAyuda, RazonSolicitud, Unidad, "
                        + "ObservSolicitud, NombresU, TipoSocio, "
                        + "NombreBeneficiario, Especialidad, CasaSalud, "
                        + "MedicoCertifica, MontoDeGastos, PorcentajeAyuda, "
                        + "ValorSolicitado, ValorAprobado, ValorAprobado0, Beneficiario0, "
                        + "ValorAprobado1, Beneficiario1, ValorAprobado2, Beneficiario2, "
                        + "FechaAprobRechaz, ReferAprobRechaz, "
                        + "MontoFacturas, MontoProformas, MontoDonaciones, PorcentajeProformas "
                        + "PorcentajeFacturas, AyudaFacturas, AyudaProformas "
                        + "FROM AAyudas.VistaAprobAyuda  WHERE NumSolicitud = "
                        + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                //              cmd.Connection.Open();
                //              cmd = new SqlCommand(sql, conn);
                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("SolicitudAyudaAprob.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    SolicitudAprobada.ReportSource = reporte;
                    SolicitudAprobada.DataBind();
                    SolicitudAprobada.RefreshReport();
                    SolicitudAprobada.Visible = true;
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
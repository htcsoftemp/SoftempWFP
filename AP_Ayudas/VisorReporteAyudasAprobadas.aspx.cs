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
public partial class AP_Ayudas_VisorReporteAyudasAprobadas : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    static DateTime FechaConvertida;
    protected void Page_Load(object sender, EventArgs e)
    {
        FechaInicial.Focus();
    }
    protected void CierraReporte_Click(object sender, ImageClickEventArgs e)
    {
//        SolicitudAprobada.Visible = false;
    }
    private void ImpresionReporteAprobadas()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                String sql;
                sql = "DROP VIEW [AAyudas].[VistaReporteAyudasAprob]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {

                }
                //              SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
                //               MensajeR.Text = "Procesa Vista ";
                sql = "CREATE VIEW [AAyudas].[VistaReporteAyudasAprob] AS "
                    + "SELECT  ASocios.APSociosAyudas.NumSolicitud, ASocios.APSociosAyudas.Identificacion, "
                    + "ASocios.APSociosAyudas.FechaSolicitud, ASocios.APSociosAyudas.TipoAyuda, "
                    + "ASocios.APSociosAyudas.TipoBeneficiario, ASocios.APSociosAyudas.RazonSolicitud, "
                    + "ASocios.APSociosAyudas.Grado, ASocios.APSociosAyudas.Unidad, "
                    + "ASocios.APSociosAyudas.ObservSolicitud, ASocios.APSocios.TipoSocio, "
                    + "NombreBeneficiario, Especialidad, CasaSalud, ASocios.APSocios.NombresU, "
                    + "MedicoCertifica, MontoDeGastos, PorcentajeAyuda, "
                    + "ValorSolicitado, ValorAprobado, ValorAprobado0, Beneficiario0, "
                    + "ValorAprobado1, Beneficiario1, ValorAprobado2, Beneficiario2, "
                    + "MontoFacturas, MontoProformas, MontoDonaciones, "
                    + "FechaAprobRechaz, ReferAprobRechaz, ASeguridad.DatosEmpresa.NombreEmpresa, "
                    + "ASeguridad.DatosEmpresa.TrabajadoraSocial, "
                    + "'" +FechaInicial.Text + "' As FechaInicial, '" + FechaFinal.Text + "' As FechaFinal "
                    + "FROM ASocios.APSocios INNER JOIN ASocios.APSociosAyudas "
                    + "ON ASocios.APSocios.Identificacion = ASocios.APSociosAyudas.Identificacion "
                    + "CROSS JOIN ASeguridad.DatosEmpresa "
                    + "WHERE (ASocios.APSociosAyudas.FechaAprobRechaz >='" + FechaInicial.Text + "' "
                    + " AND  ASocios.APSociosAyudas.FechaAprobRechaz <='" + FechaFinal.Text + "') "
                    + " AND (ASocios.APSociosAyudas.ValorAprobado > 0) ";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
 //               MensajeR.Text = "Procesa Lectura";
                sql = "Select NumSolicitud, Identificacion, Grado, NombresU, "
                        + "FechaSolicitud, TipoAyuda, RazonSolicitud, Unidad, "
                        + "ObservSolicitud, TipoSocio, "
                        + "NombreBeneficiario, Especialidad, CasaSalud, "
                        + "MedicoCertifica, MontoDeGastos, PorcentajeAyuda, "
                        + "ValorSolicitado, ValorAprobado, ValorAprobado0, Beneficiario0, "
                        + "ValorAprobado1, Beneficiario1, ValorAprobado2, Beneficiario2, "
                        + "MontoFacturas, MontoProformas, MontoDonaciones, "
                        + "FechaAprobRechaz, ReferAprobRechaz, NombreEmpresa, TrabajadoraSocial, "
                        + "FechaInicial, FechaFinal "
                        + "From AAyudas.VistaReporteAyudasAprob "
                        + "WHERE FechaAprobRechaz >= '" + FechaInicial.Text + "' "
                        + " AND  FechaAprobRechaz <= '" + FechaFinal.Text + "'"
                        + " ORDER BY NombresU ";

                cmd = new SqlCommand(sql, conn);
                //              cmd.Connection.Open();
                //              cmd = new SqlCommand(sql, conn);
                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("SolicitudAyudasReporteAprob.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    AyudasAprobadas.ReportSource = reporte;
                    AyudasAprobadas.DataBind();
  //                  MensajeR.Text = sql;
                    AyudasAprobadas.RefreshReport();
                    AyudasAprobadas.Visible = true;
  //                  CierraReporte.Visible = true;
                } // end using adaptador
            } // end try
            catch (Exception ex)
            {
                MensajeR.Text += ex.Message;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            } // end using comando
        }
    }

    protected void VerificaDatos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FechaConvertida = Convert.ToDateTime(FechaInicial.Text); ;
            FechaInicial.Text = FechaConvertida.ToString("dd/MM/yyyy");
            try
            {
                FechaConvertida = Convert.ToDateTime(FechaFinal.Text); ;
                FechaFinal.Text = FechaConvertida.ToString("dd/MM/yyyy");
                ImpresionReporteAprobadas();
            }
            catch (Exception ex)
            {
                MensajeR.Text = "Fecha Final errada " + ex.ToString();
                FechaFinal.Focus();
            }

        }
        catch (Exception ex)
        {
            MensajeR.Text = "Fecha Inicial errada " + ex.ToString();
            FechaInicial.Focus();
        }
    }

    protected void AyudasAprobadas_Init(object sender, EventArgs e)
    {

    }
}
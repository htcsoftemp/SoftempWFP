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
public partial class AP_Ayudas_VisorAyudaFacturas : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            MensajeR.Text= (string)(Session["NumSolicitud"]);
            NumSolicitud.Text = (string)(Session["NumSolicitud"]);
            if (NumSolicitud.Text == "")
            {
                NumSolicitud.Enabled = true;
                NumSolicitud.Focus();
            }
            else
            {
                NumSolicitud.Enabled = false;
                SolicitudAyuda.Visible = true;
                ImpresionFacturas();
            }
        }
//        else
//        {
//            NumSolicitud.Text = (string)(Session["NumSolicitud"]);
//            if (NumSolicitud.Text == "")
//            {
//                NumSolicitud.Enabled = true;
//                NumSolicitud.Focus();
//            }
//            else
//            {
//                NumSolicitud.Enabled = false;
//                SolicitudAyuda.Visible = true;
//                ImpresionFacturas();
//            }
//        }
    }

    private void ImpresionFacturas()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                int SolicitudNum;
                String sql;
                sql = "DROP VIEW [AAyudas].[VistaAyudaFacturas]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                MensajeR.Text = sql;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text += ex.Message;
                }
                SolicitudNum = Convert.ToInt32(NumSolicitud.Text);
                sql = "CREATE VIEW [AAyudas].[VistaAyudaFacturas] AS "
                    + "SELECT ASocios.APSociosAyudas.NumSolicitud, "
                    + "ASocios.APSociosAyudas.Identificacion, ASocios.APSocios.NombresU, "
                    + "ASeguridad.DatosEmpresa.NombreEmpresa, ASocios.APSociosAyudasFacturas.Secuencial, " 
                    + "ASocios.APSociosAyudasFacturas.Valorfactura, ASocios.APSociosAyudasFacturas.ValorProforma, "
                    + "ASocios.APSociosAyudasFacturas.ValorDonacion, "
                    + "ASocios.APSociosAyudasFacturas.Valorfactura "
                    + " + ASocios.APSociosAyudasFacturas.ValorProforma "
                    + " + ASocios.APSociosAyudasFacturas.ValorDonacion AS VTR, "
                    + "ASocios.APSociosAyudasFacturas.ObservFactura, "
                    + "ASocios.APSociosAyudas.MontoDeGastos, ASocios.APSociosAyudas.PorcentajeAyuda, "
                    + "ASocios.APSociosAyudas.ValorSolicitado,ASocios.APSociosAyudas.AyudaFacturas, "
                    + "ASocios.APSociosAyudas.AyudaProformas, ASocios.APSociosAyudas.MontoDonaciones "
                    + "FROM ASocios.APSociosAyudas "
                    + "INNER JOIN ASocios.APSocios ON ASocios.APSociosAyudas.Identificacion = ASocios.APSocios.Identificacion "
                    + "INNER JOIN ASocios.APSociosAyudasFacturas ON ASocios.APSociosAyudas.NumSolicitud = ASocios.APSociosAyudasFacturas.NumSolicitud "
                    + "CROSS JOIN ASeguridad.DatosEmpresa "
                    + "WHERE ASocios.APSociosAyudas.NumSolicitud =" + SolicitudNum;
                cmd = new SqlCommand(sql, conn);
                MensajeR.Text = sql;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text += ex.Message;
                }

                sql = "Select NumSolicitud, Identificacion, NombresU, "
                         + "NombreEmpresa, Secuencial, "
                         + "ValorFactura, ValorProforma, ValorDonacion, VTR, "
                         + "ObservFactura, "
                         + "MontoDeGastos, PorcentajeAyuda, ValorSolicitado, AyudaFacturas, AyudaProformas, MontoDonaciones "
                         + "From AAyudas.VistaAyudaFacturas  Where NumSolicitud = '"
                         + SolicitudNum + "'";
              //  MensajeR.Text = sql;

                cmd = new SqlCommand(sql, conn);
                //              cmd.Connection.Open();
                //              cmd = new SqlCommand(sql, conn);
                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("SolicitudAyudaFacturas.rpt"));
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
                MensajeR.Text += ex.Message;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            } // end using comando
        }
    }

    protected void BuscarFacturasSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        ImpresionFacturas();
    }
}
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
public partial class VisorRetencionesIR : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
//    int NumDocumento;
    static string sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            MensajeR.Text= (string)(Session["DocTranSocios"]);

 //           tbx_OficinaI.Text = (string)(Session["NumSucursal"]);
 //           tbx_NumCaja.Text = (string)(Session["NumCaja"]);
            tbx_FechaProceso.Text = "2016-10-01";
  //          tbx_NumPreFactura.Text = (string)(Session["NumPreFactura"]);
            if (tbx_FechaProceso.Text != "")
            {
 //               ImpresionFactura();
            }
        }
        else
        {
//            DocTranSocios.Text = (string)(Session["DocTranSocios"]);

//            ImpresionTranSocios();
        }
    }

    private void ImpresionCuadroGenActivos()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "DROP TABLE [ATrabajo].[CuadroGenActivos]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.Message;
                }

                sql = "SELECT        "
                    + "AFijos.APActivosFijosHistor.CodigoActivo, SUM(AFijos.APActivosFijosHistor.ValorTran) AS SumaT, "
                    + "SUM(AFijos.APActivosFijosHistor.ValorCompras)AS VCompras, "
                    + "SUM(AFijos.APActivosFijosHistor.ValorDepreciacionNormal) AS VDepreciacionN, "
                    + "AFijos.APActivosFijos.NombreDelActivo, AFijos.APActivosFijos.Categoria, "
                    + "AFijos.APActivosFijos.Grupo, AFijos.APActivosFijos.CentroCostos, "
                    + "AFijos.APActivosFijos.ValorActual, AFijos.APActivosFijos.ValorInicial, "
                    + "AFijos.APActivosFijos.FechaUltimaDepreciacion, AFijos.APActivosFijos.NumDeSerie, "
                    + "ASeguridad.TCentrosCosto.NombreCentro, AFijos.APActivosFijosTCategoria.Descripcion AS DetCategoria, "
                    + "AFijos.APActivosFijosTGrupos.Descripcion AS DetGrupo,"
                    + "'" + tbx_FechaProceso.Text + "' AS FechaProceso, ASeguridad.DatosEmpresa.NombreEmpresa "
                    + "INTO ATrabajo.CuadroGenActivos "
                    + "FROM AFijos.APActivosFijosHistor "
                    + "INNER JOIN AFijos.APActivosFijos ON AFijos.APActivosFijosHistor.CodigoActivo = AFijos.APActivosFijos.CodigoActivo "
                    + "INNER JOIN ASeguridad.TCentrosCosto ON AFijos.APActivosFijos.CentroCostos = ASeguridad.TCentrosCosto.CodigoCentro "
                    + "INNER JOIN AFijos.APActivosFijosTCategoria ON AFijos.APActivosFijos.Categoria = AFijos.APActivosFijosTCategoria.Categoria "
                    + "INNER JOIN AFijos.APActivosFijosTGrupos ON AFijos.APActivosFijos.Grupo = AFijos.APActivosFijosTGrupos.CodGrupoActivo "
                    + "CROSS JOIN ASeguridad.DatosEmpresa "
                    + "WHERE(AFijos.APActivosFijosHistor.FechaTransaccion <=CONVERT(DATETIME, '" + tbx_FechaProceso.Text + "', 102)) "
                    + "GROUP BY AFijos.APActivosFijosHistor.CodigoActivo, AFijos.APActivosFijos.NombreDelActivo, "
                    + "AFijos.APActivosFijos.Categoria, AFijos.APActivosFijos.Grupo, AFijos.APActivosFijos.CentroCostos, "
                    + "AFijos.APActivosFijos.ValorActual, AFijos.APActivosFijos.ValorInicial,  AFijos.APActivosFijos.NumDeSerie, "
                    + "AFijos.APActivosFijos.FechaUltimaDepreciacion, FechaProceso, ASeguridad.DatosEmpresa.NombreEmpresa, "
                    + "ASeguridad.TCentrosCosto.NombreCentro, AFijos.APActivosFijosTCategoria.Descripcion, AFijos.APActivosFijosTGrupos.Descripcion ";
                cmd = new SqlCommand(sql, conn);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.Message;
                }
                         
                

                

                sql = "SELECT  "
                    + "CodigoActivo, SumaT, "
                    + "VCompras, "
                    + "VDepreciacionN, "
                    + "NombreDelActivo, Categoria, "
                    + "Grupo, CentroCostos, "
                    + "ValorActual, ValorInicial, "
                    + "FechaUltimaDepreciacion, DetCategoria, DetGrupo, NombreCentro "
                    + "FechaProceso, NombreEmpresa "
                    + "FROM ATrabajo.CuadroGenActivos ";

                cmd = new SqlCommand(sql, conn);

                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("ReporteCuadroGenActivos.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    rv_CuadroActivos.ReportSource = reporte;
                    rv_CuadroActivos.DataBind();
                    rv_CuadroActivos.RefreshReport();
                    rv_CuadroActivos.Visible = true;
                } // end using adaptador
            } // end try
            catch (Exception ex)
            {
                MensajeR.Text += sql + " "  + ex.Message;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            } // end using comando
        }
    }








    protected void btn_ReportePorFecha_Click(object sender, ImageClickEventArgs e)
    {
        ImpresionCuadroGenActivos();
    }


}
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
public partial class AP_Compras_VisorDeCompras : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    static DateTime FechaConvertida;
    static string anioC, mesC, diaC, mesI;
    static int anioD, mesD;
    static string sql;
    static SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            GeneraFechaCompras();
            tbx_FechaInicial.Focus();
        }
    }

    protected void GeneraFechaCompras()
    {
        anioC = DateTime.Now.Year.ToString();
        mesC = DateTime.Now.Month.ToString();

        if (mesC == "1" || mesC == "01")
        {
            mesC = "12";
            anioD = Convert.ToInt16(anioC) - 1;
            anioC = Convert.ToString(anioD);
        }
        else
        {
            anioD = Convert.ToInt16(anioC);
            mesD = Convert.ToInt16(mesC) - 1;
            mesC = string.Format("{0:00}", mesD);
            //           mesC = Convert.ToString(mesD);
        }


        switch (mesC)
        {
            case "1":
            case "01":
            case "3":
            case "03":
            case "5":
            case "05":
            case "7":
            case "07":
            case "8":
            case "08":
            case "10":
            case "12":
                diaC = "31";
                break;
            case "2":
            case "02":
                if ((Convert.ToInt16(anioD) % 4) == 0)
                {
                    diaC = "29";
                }
                else
                {
                    diaC = "28";
                }
                break;
            case "4":
            case "04":
            case "6":
            case "06":
            case "9":
            case "09":
            case "11":
                diaC = "30";
                break;
            default:
                break;
        }

        tbx_FechaInicial.Text = "01" + "/" + mesC + "/" + anioC;
        FechaConvertida = Convert.ToDateTime(tbx_FechaInicial.Text);
        tbx_FechaInicial.Text = FechaConvertida.ToString("dd/MM/yyyy");
        tbx_FechaFinal.Text = diaC + "/" + mesC + "/" + anioC;

        //       MensajeR.Text = tbx_FechaFinal.Text;

        FechaConvertida = Convert.ToDateTime(tbx_FechaFinal.Text);
        tbx_FechaFinal.Text = FechaConvertida.ToString("dd/MM/yyyy");
        /*
                if (string.Compare(tbx_FechaConciliacion.Text, FechaBase) >= 0)
                {

                }
                else
                {
                    Response.Write("<script>window.alert('No puede procesar fechas anteriores al 01-10-2016 ');</script>");
                }
        */

    }

    protected void tbx_FechaInicial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FechaConvertida = Convert.ToDateTime(tbx_FechaInicial.Text);
            tbx_FechaInicial.Text = FechaConvertida.ToString("dd/MM/yyyy");
            tbx_FechaFinal.Focus();
        }
        catch (Exception ex)
        {
            MensajeR.Text = " Error en formato de Fecha " + ex.ToString();
            tbx_FechaInicial.Focus();
        }

    }

    protected void tbx_FechaFinal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FechaConvertida = Convert.ToDateTime(tbx_FechaFinal.Text);
            tbx_FechaFinal.Text = FechaConvertida.ToString("dd/MM/yyyy");
            ImprimeReporteDeCompras.Focus();
        }
        catch (Exception ex)
        {
            MensajeR.Text = " Error en formato de Fecha " + ex.ToString();
            tbx_FechaFinal.Focus();
        }
    }


    private void ArmaRetenciones()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
                sql = "DELETE [AContabilidad].[ReporteRetencionesPorFechas]";
                cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.Message;
                }

            sql = "INSERT INTO AContabilidad.ReporteRetencionesPorFechas "
                    + "(FI, FF, Diario, CentroCostos, tpIdProv, idProv, "
                    + "fechaRegistro, CodConcepto, "
                    + "baseImponible, baseImpGrav, baseImpExe, montoIce, montoIva, "
                    + "valRetBien10, valRetServ20, valorRetBienes, valRetServ50, valorRetServicios, valRetServ100, "
                    + "ValorRetIVAB, TasaRetenIVAB, ValorRetIVAS,TasaRetenIVAS, "
                    + "SucReten, NumReten, NombreEmpresa, NombreCentro, NSucursal) "
                    + "SELECT " 
                    + "'" + tbx_FechaInicial.Text + "' AS FI, '" + tbx_FechaFinal.Text + "' AS FF, "
                    + "AContabilidad.APContabCompras.Diario, "
                    + "AContabilidad.APContabCompras.CentroCostos, "
                    + "AContabilidad.APContabCompras.tpIdProv, "
                    + "AContabilidad.APContabCompras.idProv, "
                    + "AContabilidad.APContabCompras.fechaRegistro, "
                    + "AContabilidad.APContabCompras.CodRetIVA, "
                    + "AContabilidad.APContabCompras.baseImponible, "
                    + "AContabilidad.APContabCompras.baseImpGrav, "
                    + "AContabilidad.APContabCompras.baseImpExe, "
                    + "AContabilidad.APContabCompras.montoIce, "
                    + "AContabilidad.APContabCompras.montoIva, "
                    + "AContabilidad.APContabCompras.valRetBien10, "
                    + "AContabilidad.APContabCompras.valRetServ20, "
                    + "AContabilidad.APContabCompras.valorRetBienes, "
                    + "AContabilidad.APContabCompras.valRetServ50, "
                    + "AContabilidad.APContabCompras.valorRetServicios, "
                    + "AContabilidad.APContabCompras.valRetServ100, "
                    + "AContabilidad.APContabCompras.ValorRetIVAB, "
                    + "AContabilidad.APContabCompras.TasaRetenIVAB, "
                    + "AContabilidad.APContabCompras.ValorRetIVAS, "
                    + "AContabilidad.APContabCompras.TasaRetenIVAS, "
                    + "AContabilidad.APContabCompras.estabRetencion1, "
                    + "AContabilidad.APContabCompras.secRetencion1, "
                    + "ASeguridad.DatosEmpresa.NombreEmpresa, "
                    + "ASeguridad.TCentrosCosto.NombreCentro, "
                    + "ASeguridad.TCentrosCosto.NSucursal "
                    + "FROM AContabilidad.APContabCompras "
                    + "RIGHT JOIN ASeguridad.TCentrosCosto "
                    + "ON AContabilidad.APContabCompras.estabRetencion1 = ASeguridad.TCentrosCosto.NSucursal "
                    + "CROSS JOIN ASeguridad.DatosEmpresa "
                    + "WHERE (AContabilidad.APContabCompras.fechaRegistro "
                    + "BETWEEN '" + tbx_FechaInicial.Text + "' AND '" + tbx_FechaFinal.Text + "')"
                    + "AND idProv<>'9999999999999' "
                    + "AND ((AContabilidad.APContabCompras.ValorRetIVAB+AContabilidad.APContabCompras.ValorRetIVAS) > 0)";
                cmd = new SqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.Message;
                }

            sql = "INSERT INTO AContabilidad.ReporteRetencionesPorFechas "
                + "(FI, FF, Diario, CentroCostos, tpIdProv, idProv, "
                + "fechaRegistro, montoIce, CodConcepto, "
                + "BaseImpon, TasaReten, Retencion, "
                + "SucReten, NumReten, NombreEmpresa, NombreCentro, NSucursal) "
                + "SELECT "
                + "'" + tbx_FechaInicial.Text + "' AS FI, '" + tbx_FechaFinal.Text + "' AS FF, "
                + "AContabilidad.APContabCompras.Diario, "
                + "AContabilidad.APContabCompras.CentroCostos, "
                + "AContabilidad.APContabCompras.tpIdProv, "
                + "AContabilidad.APContabCompras.idProv, "
                + "AContabilidad.APContabCompras.fechaRegistro, "
                + "AContabilidad.APContabCompras.montoIce, "
                + "AContabilidad.APContabCompras.CodConceptoB, "
                + "AContabilidad.APContabCompras.BaseImponIRB, "
                + "AContabilidad.APContabCompras.TasaRetIRB, "
                + "AContabilidad.APContabCompras.ValorRetIRB, "
                + "AContabilidad.APContabCompras.estabRetencion1, "
                + "AContabilidad.APContabCompras.secRetencion1, "
                + "ASeguridad.DatosEmpresa.NombreEmpresa, "
                + "ASeguridad.TCentrosCosto.NombreCentro, "
                + "ASeguridad.TCentrosCosto.NSucursal "
                + "FROM AContabilidad.APContabCompras RIGHT JOIN ASeguridad.TCentrosCosto "
                + "ON AContabilidad.APContabCompras.estabRetencion1 = ASeguridad.TCentrosCosto.NSucursal "
                + "CROSS JOIN ASeguridad.DatosEmpresa "
                + "WHERE (AContabilidad.APContabCompras.fechaRegistro "
                + "BETWEEN '" + tbx_FechaInicial.Text + "' AND '" + tbx_FechaFinal.Text + "')"
                + "AND idProv<>'9999999999999' "
                + "AND ((AContabilidad.APContabCompras.BaseImponIRS) > 0)";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + ex.Message;
            }

            sql = "INSERT INTO AContabilidad.ReporteRetencionesPorFechas "
                    + "(FI, FF, Diario, CentroCostos, tpIdProv, idProv, "
                    + "fechaRegistro, montoIce, CodConcepto, "
                    + "BaseImpon, TasaReten, Retencion, "
                    + "SucReten, NumReten, NombreEmpresa, NombreCentro, NSucursal) "
                    + "SELECT "
                    + "'" + tbx_FechaInicial.Text + "' AS FI, '" + tbx_FechaFinal.Text + "' AS FF, "
                    + "AContabilidad.APContabCompras.Diario, "
                    + "AContabilidad.APContabCompras.CentroCostos, "
                    + "AContabilidad.APContabCompras.tpIdProv, "
                    + "AContabilidad.APContabCompras.idProv, "
                    + "AContabilidad.APContabCompras.fechaRegistro, "
                    + "AContabilidad.APContabCompras.montoIce, "
                    + "AContabilidad.APContabCompras.CodConceptoS, "
                    + "AContabilidad.APContabCompras.BaseImponIRS + AContabilidad.APContabCompras.montoIce AS BaseImpon, "
                    + "AContabilidad.APContabCompras.TasaRetIRS, AContabilidad.APContabCompras.ValorRetIRS, "
                    + "AContabilidad.APContabCompras.estabRetencion1, "
                    + "AContabilidad.APContabCompras.secRetencion1, "
                    + "ASeguridad.DatosEmpresa.NombreEmpresa, "
                    + "ASeguridad.TCentrosCosto.NombreCentro, "
                    + "ASeguridad.TCentrosCosto.NSucursal "
                    + "FROM AContabilidad.APContabCompras RIGHT JOIN ASeguridad.TCentrosCosto "
                    + "ON AContabilidad.APContabCompras.estabRetencion1 = ASeguridad.TCentrosCosto.NSucursal "
                    + "CROSS JOIN ASeguridad.DatosEmpresa "
                    + "WHERE (AContabilidad.APContabCompras.fechaRegistro "
                    + "BETWEEN '" + tbx_FechaInicial.Text + "' AND '" + tbx_FechaFinal.Text + "')"
                    + "AND idProv<>'9999999999999' "
                    + "AND ((AContabilidad.APContabCompras.BaseImponIRS) > 0)";
                cmd = new SqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    conn.Dispose();
                }
        }
    }

    protected void ImprimeReporteRetenciones_Click(object sender, ImageClickEventArgs e)
    {
        cr_Compras.Visible = false;
        cr_RetenIR.Visible = false;
        cr_RetenIVA.Visible = false;
        MensajeR.Text = "";
        ArmaRetencionesIR();
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {

                sql = "Select "
                            + "FI, FF, "
                            + "Diario, CentroCostos, "
                            + "tpIdProv, "
                            + "idProv, "
                            + "fechaRegistro, montoIce, "
                            + "CodConcepto, BaseImpon, TasaReten, Retencion, "
                            + " CodRetIVA, "
                            + " baseImponible, "
                            + " baseImpGrav, "
                            + " baseImpExe, "
                            + " montoIce, "
                            + " montoIva, "
                            + " valRetBien10, "
                            + " valRetServ20, "
                            + " valorRetBienes, "
                            + " valRetServ50, "
                            + " valorRetServicios, "
                            + " valRetServ100, "
                            + " ValorRetIVAB, "
                            + " TasaRetenIVAB, "
                            + " ValorRetIVAS, "
                            + " TasaRetenIVAS, "
                            + "SucReten, NumReten, "
                            + "NombreEmpresa, NombreCentro "
                            + "FROM AContabilidad.ReporteRetencionesIRPorFechas "
                            + "ORDER BY fechaRegistro, Diario ASC";
                cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();

                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("ReporteRetencionesIREntreFechas.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    cr_RetenIR.ReportSource = reporte;
                    cr_RetenIR.DataBind();
                    cr_RetenIR.RefreshReport();
                    cr_RetenIR.Visible = true;
                    cr_RetenIVA.Visible = false;
                    cr_Compras.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();

            }
            conn.Close();
        }
    }
}
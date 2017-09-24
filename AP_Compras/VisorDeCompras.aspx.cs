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

    protected void ImprimeReporteDeCompras_Click(object sender, ImageClickEventArgs e)
    {
        cr_Compras.Visible = false;
        cr_RetenIR.Visible = false;
        cr_RetenIVA.Visible = false;

        MensajeR.Text = "";

        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                String sql;
                sql = "DROP VIEW [AContabilidad].[VistaCompras]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = ex.Message;
                }
                sql = "CREATE VIEW [AContabilidad].[VistaCompras] AS SELECT "
                    + "'" + tbx_FechaInicial.Text + "' AS FI, '" +  tbx_FechaFinal.Text + "' AS FF, "
                    + "AContabilidad.APContabCompras.TipoDiario, AContabilidad.APContabCompras.Diario, "
                    + "AContabilidad.APContabCompras.CentroCostos, "
                    + "AContabilidad.APContabCompras.codSustento, AContabilidad.APContabCompras.tpIdProv, "
                    + "AContabilidad.APContabCompras.idProv, AContabilidad.APContabCompras.tipoComprobante,  "
                    + "AContabilidad.APContabCompras.parteRel, AContabilidad.APContabCompras.fechaRegistro, "
                    + "AContabilidad.APContabCompras.establecimiento, AContabilidad.APContabCompras.puntoEmision, "
                    + "AContabilidad.APContabCompras.secuencial, AContabilidad.APContabCompras.fechaEmision, "
                    + "AContabilidad.APContabCompras.autorizacion, AContabilidad.APContabCompras.baseNoGraIva, "
                    + "AContabilidad.APContabCompras.baseImponible, AContabilidad.APContabCompras.baseImpGrav, "
                    + "AContabilidad.APContabCompras.baseImpExe, AContabilidad.APContabCompras.montoIce, "
                    + "AContabilidad.APContabCompras.montoIva, AContabilidad.APContabCompras.valRetBien10, "
                    + "AContabilidad.APContabCompras.valRetServ20, AContabilidad.APContabCompras.valorRetBienes, "
                    + "AContabilidad.APContabCompras.valRetServ50, AContabilidad.APContabCompras.valorRetServicios, "
                    + "AContabilidad.APContabCompras.valRetServ100, AContabilidad.APContabCompras.totbasesImpReemb, "
                    + "AContabilidad.APContabCompras.formaPago, AContabilidad.APContabCompras.CodRetIVA, "
                    + "AContabilidad.APContabCompras.ValorRetIVAB, AContabilidad.APContabCompras.TasaRetenIVAB, "
                    + "AContabilidad.APContabCompras.ValorRetIVAS, AContabilidad.APContabCompras.TasaRetenIVAS, "
                    + "AContabilidad.APContabCompras.CodConceptoB, AContabilidad.APContabCompras.BaseImponIRB, "
                    + "AContabilidad.APContabCompras.TasaRetIRB, AContabilidad.APContabCompras.ValorRetIRB, "
                    + "AContabilidad.APContabCompras.CodConceptoS, AContabilidad.APContabCompras.BaseImponIRS, "
                    + "AContabilidad.APContabCompras.TasaRetIRS, AContabilidad.APContabCompras.ValorRetIRS, "
                    + "AContabilidad.APContabCompras.estabRetencion1, AContabilidad.APContabCompras.ptoEmiRetencion1, "
                    + "AContabilidad.APContabCompras.secRetencion1, AContabilidad.APContabCompras.autRetencion1, "
                    + "AContabilidad.APContabCompras.fechaEmiRet1, AContabilidad.APContabCompras.NombreContrib, "
                    + "AContabilidad.APContabCompras.DetalleComprob, ASeguridad.DatosEmpresa.NombreEmpresa, "
                    + "ASeguridad.TCentrosCosto.NombreCentro, "
                    + "ASeguridad.TCentrosCosto.NSucursal "
                    + "FROM AContabilidad.APContabCompras RIGHT JOIN ASeguridad.TCentrosCosto "
                    + "ON AContabilidad.APContabCompras.CentroCostos = ASeguridad.TCentrosCosto.CodigoCentro "
                    + "CROSS JOIN ASeguridad.DatosEmpresa "
                    + "WHERE (AContabilidad.APContabCompras.fechaRegistro "
                    + "BETWEEN '" + tbx_FechaInicial.Text + "' AND '" + tbx_FechaFinal.Text + "')"
                    + "AND idProv<>'9999999999999'";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "Select "
                            + "FI, FF, "
                            + "TipoDiario, Diario, CentroCostos, "
                            + "codSustento, tpIdProv, "
                            + "idProv, tipoComprobante,  "
                            + "parteRel, fechaRegistro, "
                            + "establecimiento, puntoEmision, "
                            + "secuencial, fechaEmision, "
                            + "autorizacion, baseNoGraIva, "
                            + "baseImponible, baseImpGrav, "
                            + "baseImpExe, montoIce, "
                            + "montoIva, valRetBien10, "
                            + "valRetServ20, valorRetBienes, "
                            + "valRetServ50, valorRetServicios, "
                            + "valRetServ100, totbasesImpReemb, "
                            + "formaPago, CodRetIVA, "
                            + "ValorRetIVAB, TasaRetenIVAB, "
                            + "ValorRetIVAS, TasaRetenIVAS, "
                            + "CodConceptoB, BaseImponIRB, "
                            + "TasaRetIRB, ValorRetIRB, "
                            + "CodConceptoS, BaseImponIRS, "
                            + "TasaRetIRS, ValorRetIRS, "
                            + "estabRetencion1, ptoEmiRetencion1, "
                            + "secRetencion1, autRetencion1, "
                            + "fechaEmiRet1, NombreContrib, "
                            + "DetalleComprob, NombreEmpresa, NombreCentro "
                            + "FROM AContabilidad.VistaCompras "
                            + "ORDER BY fechaRegistro, Diario ASC";
                cmd = new SqlCommand(sql, conn);
                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("ReporteDeCompras.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    cr_Compras.ReportSource = reporte;
                    cr_Compras.DataBind();
                    cr_Compras.RefreshReport();
                    cr_Compras.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();

            }
            conn.Close();
        }
    }

    /*
SELECT        '01/05/2017' AS FI, '31/05/2017' AS FF, AContabilidad.APContabCompras.TipoDiario, AContabilidad.APContabCompras.Diario, AContabilidad.APContabCompras.CentroCostos, 
                         AContabilidad.APContabCompras.codSustento, AContabilidad.APContabCompras.tpIdProv, AContabilidad.APContabCompras.idProv, AContabilidad.APContabCompras.tipoComprobante, 
                         AContabilidad.APContabCompras.parteRel, AContabilidad.APContabCompras.fechaRegistro, AContabilidad.APContabCompras.establecimiento, AContabilidad.APContabCompras.puntoEmision, 
                         AContabilidad.APContabCompras.secuencial, AContabilidad.APContabCompras.fechaEmision, AContabilidad.APContabCompras.autorizacion, AContabilidad.APContabCompras.baseNoGraIva, 
                         AContabilidad.APContabCompras.baseImponible, AContabilidad.APContabCompras.baseImpGrav, AContabilidad.APContabCompras.baseImpExe, AContabilidad.APContabCompras.montoIce, 
                         AContabilidad.APContabCompras.montoIva, AContabilidad.APContabCompras.valRetBien10, AContabilidad.APContabCompras.valRetServ20, AContabilidad.APContabCompras.valorRetBienes, 
                         AContabilidad.APContabCompras.valRetServ50, AContabilidad.APContabCompras.valorRetServicios, AContabilidad.APContabCompras.valRetServ100, AContabilidad.APContabCompras.totbasesImpReemb, 
                         AContabilidad.APContabCompras.formaPago, AContabilidad.APContabCompras.CodRetIVA, AContabilidad.APContabCompras.ValorRetIVAB, AContabilidad.APContabCompras.TasaRetenIVAB, 
                         AContabilidad.APContabCompras.ValorRetIVAS, AContabilidad.APContabCompras.TasaRetenIVAS, AContabilidad.APContabCompras.CodConceptoB, AContabilidad.APContabCompras.BaseImponIRB, 
                         AContabilidad.APContabCompras.TasaRetIRB, AContabilidad.APContabCompras.ValorRetIRB, AContabilidad.APContabCompras.CodConceptoS, AContabilidad.APContabCompras.BaseImponIRS, 
                         AContabilidad.APContabCompras.TasaRetIRS, AContabilidad.APContabCompras.ValorRetIRS, AContabilidad.APContabCompras.estabRetencion1, AContabilidad.APContabCompras.ptoEmiRetencion1, 
                         AContabilidad.APContabCompras.secRetencion1, AContabilidad.APContabCompras.autRetencion1, AContabilidad.APContabCompras.fechaEmiRet1, AContabilidad.APContabCompras.NombreContrib, 
                         AContabilidad.APContabCompras.DetalleComprob, ASeguridad.DatosEmpresa.NombreEmpresa, ASeguridad.TCentrosCosto.NombreCentro, 
                         AContabilidad.APContabCompras.valRetBien10 + AContabilidad.APContabCompras.valRetServ20 + AContabilidad.APContabCompras.valorRetBienes + AContabilidad.APContabCompras.valRetServ50 + AContabilidad.APContabCompras.valorRetServicios
                          + AContabilidad.APContabCompras.valRetServ100 AS TotRetIVA, ASeguridad.TCentrosCosto.NSucursal
FROM            AContabilidad.APContabCompras INNER JOIN
                         ASeguridad.TCentrosCosto ON AContabilidad.APContabCompras.estabRetencion1 = ASeguridad.TCentrosCosto.NSucursal CROSS JOIN
                         ASeguridad.DatosEmpresa
                WHERE        (AContabilidad.APContabCompras.fechaRegistro BETWEEN '01/05/2017' AND '31/05/2017') AND (AContabilidad.APContabCompras.idProv <> '9999999999999') AND 
                         (AContabilidad.APContabCompras.valRetBien10 + AContabilidad.APContabCompras.valRetServ20 + AContabilidad.APContabCompras.valorRetBienes + AContabilidad.APContabCompras.valRetServ50 + AContabilidad.APContabCompras.valorRetServicios
                          + AContabilidad.APContabCompras.valRetServ100 > 0)
*/

    protected void ImprimeReporteRetencionesIVA_Click(object sender, ImageClickEventArgs e)
    {
        cr_Compras.Visible = false;
        cr_RetenIR.Visible = false;
        cr_RetenIVA.Visible = false;

        MensajeR.Text = "";

        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                String sql;
                sql = "DROP VIEW [AContabilidad].[VistaRetencionesIVACompras]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = ex.Message;
                }
                sql = "CREATE VIEW [AContabilidad].[VistaRetencionesIVACompras] AS SELECT "
                    + "'" + tbx_FechaInicial.Text + "' AS FI, '" + tbx_FechaFinal.Text + "' AS FF, "
                    + "AContabilidad.APContabCompras.TipoDiario, AContabilidad.APContabCompras.Diario, "
                    + "AContabilidad.APContabCompras.CentroCostos, "
                    + "AContabilidad.APContabCompras.codSustento, AContabilidad.APContabCompras.tpIdProv, "
                    + "AContabilidad.APContabCompras.idProv, AContabilidad.APContabCompras.tipoComprobante,  "
                    + "AContabilidad.APContabCompras.parteRel, AContabilidad.APContabCompras.fechaRegistro, "
                    + "AContabilidad.APContabCompras.establecimiento, AContabilidad.APContabCompras.puntoEmision, "
                    + "AContabilidad.APContabCompras.secuencial, AContabilidad.APContabCompras.fechaEmision, "
                    + "AContabilidad.APContabCompras.autorizacion, AContabilidad.APContabCompras.baseNoGraIva, "
                    + "AContabilidad.APContabCompras.baseImponible, AContabilidad.APContabCompras.baseImpGrav, "
                    + "AContabilidad.APContabCompras.baseImpExe, AContabilidad.APContabCompras.montoIce, "
                    + "AContabilidad.APContabCompras.montoIva, AContabilidad.APContabCompras.valRetBien10, "
                    + "AContabilidad.APContabCompras.valRetServ20, AContabilidad.APContabCompras.valorRetBienes, "
                    + "AContabilidad.APContabCompras.valRetServ50, AContabilidad.APContabCompras.valorRetServicios, "
                    + "AContabilidad.APContabCompras.valRetServ100, AContabilidad.APContabCompras.totbasesImpReemb, "
                    + "AContabilidad.APContabCompras.formaPago, AContabilidad.APContabCompras.CodRetIVA, "
                    + "AContabilidad.APContabCompras.ValorRetIVAB, AContabilidad.APContabCompras.TasaRetenIVAB, "
                    + "AContabilidad.APContabCompras.ValorRetIVAS, AContabilidad.APContabCompras.TasaRetenIVAS, "
                    + "AContabilidad.APContabCompras.CodConceptoB, AContabilidad.APContabCompras.BaseImponIRB, "
                    + "AContabilidad.APContabCompras.TasaRetIRB, AContabilidad.APContabCompras.ValorRetIRB, "
                    + "AContabilidad.APContabCompras.CodConceptoS, AContabilidad.APContabCompras.BaseImponIRS, "
                    + "AContabilidad.APContabCompras.TasaRetIRS, AContabilidad.APContabCompras.ValorRetIRS, "
                    + "AContabilidad.APContabCompras.estabRetencion1, AContabilidad.APContabCompras.ptoEmiRetencion1, "
                    + "AContabilidad.APContabCompras.secRetencion1, AContabilidad.APContabCompras.autRetencion1, "
                    + "AContabilidad.APContabCompras.fechaEmiRet1, AContabilidad.APContabCompras.NombreContrib, "
                    + "AContabilidad.APContabCompras.DetalleComprob, ASeguridad.DatosEmpresa.NombreEmpresa, "
                    + "ASeguridad.TCentrosCosto.NombreCentro, "
                    + "AContabilidad.APContabCompras.valRetBien10 + "
                    + "AContabilidad.APContabCompras.valRetServ20 + AContabilidad.APContabCompras.valorRetBienes  + "
                    + "AContabilidad.APContabCompras.valRetServ50 + AContabilidad.APContabCompras.valorRetServicios + "
                    + "AContabilidad.APContabCompras.valRetServ100 AS TotRetIVA, "
                    + "ASeguridad.TCentrosCosto.NSucursal "
                    + "FROM AContabilidad.APContabCompras INNER JOIN ASeguridad.TCentrosCosto "
                    + "ON AContabilidad.APContabCompras.estabRetencion1 = ASeguridad.TCentrosCosto.NSucursal "
                    + "CROSS JOIN ASeguridad.DatosEmpresa "
                    + "WHERE (AContabilidad.APContabCompras.fechaRegistro "
                    + "BETWEEN '" + tbx_FechaInicial.Text + "' AND '" + tbx_FechaFinal.Text + "')"
                    + "AND idProv<>'9999999999999' "
                    + "AND (AContabilidad.APContabCompras.valRetBien10 + AContabilidad.APContabCompras.valRetServ20 + "
                    + "AContabilidad.APContabCompras.valorRetBienes + AContabilidad.APContabCompras.valRetServ50 + "
                    + "AContabilidad.APContabCompras.valorRetServicios + AContabilidad.APContabCompras.valRetServ100 > 0)";

                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "Select "
                            + "FI, FF, "
                            + "TipoDiario, Diario, CentroCostos, "
                            + "codSustento, tpIdProv, "
                            + "idProv, tipoComprobante,  "
                            + "parteRel, fechaRegistro, "
                            + "establecimiento, puntoEmision, "
                            + "secuencial, fechaEmision, "
                            + "autorizacion, baseNoGraIva, "
                            + "baseImponible, baseImpGrav, "
                            + "baseImpExe, montoIce, "
                            + "montoIva, valRetBien10, "
                            + "valRetServ20, valorRetBienes, "
                            + "valRetServ50, valorRetServicios, "
                            + "valRetServ100, totbasesImpReemb, "
                            + "formaPago, CodRetIVA, "
                            + "ValorRetIVAB, TasaRetenIVAB, "
                            + "ValorRetIVAS, TasaRetenIVAS, "
                            + "CodConceptoB, BaseImponIRB, "
                            + "TasaRetIRB, ValorRetIRB, "
                            + "CodConceptoS, BaseImponIRS, "
                            + "TasaRetIRS, ValorRetIRS, "
                            + "estabRetencion1, ptoEmiRetencion1, "
                            + "secRetencion1, autRetencion1, "
                            + "fechaEmiRet1, NombreContrib, "
                            + "DetalleComprob, NombreEmpresa, NombreCentro, TotRetIVA "
                            + "FROM AContabilidad.VistaRetencionesIVACompras "
                            + "ORDER BY fechaRegistro, Diario ASC";
                cmd = new SqlCommand(sql, conn);
                using (var adaptador = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    adaptador.Fill(ds);
                    var reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("ReporteRetencionesIVAEntreFechas.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    cr_RetenIVA.ReportSource = reporte;
                    cr_RetenIVA.DataBind();
                    cr_RetenIVA.RefreshReport();
                    cr_RetenIVA.Visible = true;

                }

            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();

            }
            conn.Close();
        }
    }

    private void ArmaRetencionesIR()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
                sql = "DELETE [AContabilidad].[ReporteRetencionesIRPorFechas]";
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

            /*               sql = "INSERT INTO AContabilidad.ReporteRetencionesIRPorFechas "
                               + "(CodConcepto, BaseImpon, TasaReten, Retencion, FechaComprob, "
                               + "SucReten, NumReten, NumComprob, DetalleComprob, "
                               + "FI, FF, "
                               + "TipoIden, Identificacion, RazonSocial, ValorCompra, BaseImponIR) "
                               + "SELECT CodConceptoB, BaseImponIRB, TasaRetenIRB, RetencionIRB, fechaRegistro, "
                               + "SucReten, NumReten, NumComprob,  SUBSTRING(DetalleComprob,1, 100), "
                               + "'" + tbx_FechaInicial.Text + "', '" + tbx_FechaFinal.Text + "', "
                               + "TipoIdContrib, IdContrib, NombreContrib, ValorBienes, BaseImponIRB "
                               + "FROM AContabilidad.APContabCompras "
                               + "WHERE (fechaRegistro >= CONVERT(DATETIME, '" + tbx_FechaInicial.Text + "', 102)) "
                               + "AND(fechaRegistro <= CONVERT(DATETIME, '" + tbx_FechaFinal.Text + "', 102)) "
                               + "AND (idProv <> '9999999999999') "
                               + "AND(RetencionIRB > 0.009) ";
                           cmd = new SqlCommand(sql, conn);

                           try
                           {
                               cmd.ExecuteNonQuery();
                           }
                           catch (Exception ex)
                           {
                               MensajeR.Text = sql + " " + ex.Message;
                           }

            */
            sql = "INSERT INTO AContabilidad.ReporteRetencionesIRPorFechas "
                    + "(FI, FF, Diario, CentroCostos, tpIdProv, idProv, "
                    + "fechaRegistro, montoIce, CodConcepto, BaseImpon, TasaReten, Retencion, "
                    + "SucReten, NumReten, NombreEmpresa, NombreCentro, NSucursal) "
                    + "SELECT "
                    + "'" + tbx_FechaInicial.Text + "' AS FI, '" + tbx_FechaFinal.Text + "' AS FF, "
                    + "AContabilidad.APContabCompras.Diario, "
                    + "AContabilidad.APContabCompras.CentroCostos, "
                    + "AContabilidad.APContabCompras.tpIdProv, "
                    + "AContabilidad.APContabCompras.idProv, "
                    + "AContabilidad.APContabCompras.fechaRegistro, "
                    + "AContabilidad.APContabCompras.montoIce, "
                    + "AContabilidad.APContabCompras.CodConceptoB, AContabilidad.APContabCompras.BaseImponIRB, "
                    + "AContabilidad.APContabCompras.TasaRetIRB, AContabilidad.APContabCompras.ValorRetIRB, "
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
                    + "AND ((AContabilidad.APContabCompras.BaseImponIRB) > 0)";
                cmd = new SqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.Message;
                }
                /*               sql = "INSERT INTO AContabilidad.ReporteRetencionesPorFechas "
                                   + "(CodConcepto, BaseImpon, TasaReten, Retencion, FechaComprob, "
                                   + "SucReten, NumReten, NumComprob, DetalleComprob, "
                                   + "TipoIden, Identificacion, RazonSocial, "
                                   + "ValorCompra, BaseImponIR) "
                                   + "SELECT CodConceptoS, BaseImponIRS, TasaRetenIRS, RetencionIRS, FechaComprob, "
                                   + "SucReten, NumReten, NumComprob,  SUBSTRING(DetalleComprob,1, 100), "
                                   + "TipoIdContrib, IdContrib, NombreContrib, ValorServicios, BaseImponIRS "
                                   + "FROM AContabilidad.APContabRetenciones "
                                   + "WHERE (FechaComprob >= CONVERT(DATETIME, '" + tbx_FechaI.Text + "', 102)) "
                                   + "AND(FechaComprob <= CONVERT(DATETIME, '" + tbx_FechaF.Text + "', 102)) "
                                   + "AND(RetencionIRS > 0.009) ";
               */
                sql = "INSERT INTO AContabilidad.ReporteRetencionesIRPorFechas "
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
                } // end using comando
        }
    }

    protected void ImprimeReporteRetencionesIR_Click(object sender, ImageClickEventArgs e)
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
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
using System.Configuration;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Drawing.Printing;
using System.Xml.Schema;
using System.IO.Compression;
using System.Net;

public partial class AP_Contabilidad_GeneradorATS : System.Web.UI.Page
{
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    static DateTime FechaConvertida;
    static string FechaBase = "01/10/2016";
    static DateTime FechaBaseD, FechaInicialB, FechaFinalB, FechaAnulReten;
    static string diaI;
    static string sql;
    static SqlConnection conn;
    static string totVentas = "0";
    static int NCompras, NAnulados; // NVentas, , exex;
                                    //    string zipPath;
    public string NombrePathFiles;
    //    public static string NombreFileDetalle = "c:/inetpub/wwwroot/SoftempWFP/AP_Contabilidad/GenATS2.xml";
    public static string NombreFileDetalle, NombreFileDetalleD = "C:/CATS/";

    static TextWriter twd; //n= new StreamWriter(NombreFileDetalle);
                           //    string DirEsquemas;


    static string anioI, anioC;
    static string mesI, mesC;
    static int anioD, mesD;
    // COMPRAS
    static string codSustento, tpIdProv, idProv, tipoComprobante;
    static string parteRel, fechaRegistro, establecimiento, puntoEmision;
    static string secuencial, fechaEmision, autorizacion, autorizacionA;
    static string baseNoGraIVA, baseImponible, baseImpGrav;
    static string baseImpExe, montoIce, montoIva, valRetBien10, valRetServ20, valorRetBienes, valRetBien50;
    static string valorRetServicios, valRetServ100, totbasesImpReemb, formaPago;
    // Pagos al exterior en compras
    static string pagoLocExt, paisEfecPago, aplicConvDobTrib, pagExtSujRetNorLeg;
    // Retenciones en compras de Impuesto a la Renta
    static string codRetAirB, baseImpAirB, porcentajeAirB, valRetAirB;
    static string codRetAirS, baseImpAirS, porcentajeAirS, valRetAirS;
    static string baseImpAir;
    static decimal baseImpAirD;
    // Datos de comprobante de Retencion
    static string estabRetencion1, ptoEmiRetencion1, secRetencion1, autRetencion1, fechaEmiRet1;
    // VENTAS
    static string tpIdCliente, idCliente, parteRelVtas, DenoCli; // tpCliente,
    static string tipoComprobanteV, tipoEmision, numeroComprobantes;
    static string baseNoGraIvaV, baseImponibleV, baseImpGravV;
    static string montoIvaV, montoIceV, valorRetIva, valorRetRenta;
    //    static string formPagoV;
    // Ventas por establecimiento
    //    static string codEstab, VentasEstab, ivaComp;
    // Anulados
    //    static string tipoComprobanteA, establecimientoA, puntoEmisionA;
    static string secuencialInicio, secuencialFin, secuencialAux, establecimientoAux; //ptoEmisionAux, autorizacionA, 

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            OcultaProcesos();
            ddl_Year.Items.Clear();
            ddl_Year.DataSourceID = "";
            ddl_Year.DataSourceID = (string)(TYear.UniqueID);
            ddl_Year.DataBind();
            ddl_Year.Items.Insert(0, "  Seleccione Año que desea generar....");
            ddl_Year.SelectedIndex = 0;

            ddl_Mes.DataSourceID = "";
            ddl_Mes.DataSourceID = (string)(TMes.UniqueID);
            ddl_Mes.DataBind();
            ddl_Mes.Items.Insert(0, "  Seleccione Mes que desea generar....");
            ddl_Mes.SelectedIndex = 0;

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
                mesD = Convert.ToInt16(mesC) - 1;
                mesC = string.Format("{0:00}", mesD);
            }

            MensajeR.Text = anioI + " " + mesI;
            anioI = anioC;
            mesI = mesC;

            tbx_AnioI.Text = anioI;
            tbx_MesI.Text = mesI;

            MensajeR.Visible = false;
            MensajeR.Text = "";
            MensajeR.Visible = true;
        }
    }

    protected void tbx_FechaInicial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FechaConvertida = Convert.ToDateTime(tbx_FechaInicial.Text);
            tbx_FechaInicial.Text = FechaConvertida.ToString("dd/MM/yyyy");
            FechaInicialB = FechaConvertida;
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
            FechaFinalB = FechaConvertida;
            btn_ValidaParametros.Focus();
        }
        catch (Exception ex)
        {
            MensajeR.Text = " Error en formato de Fecha " + ex.ToString();
            tbx_FechaFinal.Focus();
        }
    }
    protected void ProcesaVentasTotales()
    {
        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select SUM(baseNoGraIVA) + SUM(baseImponible)+ SUM(baseImpGrav) AS TotalVentas "
            + "FROM AHoteleria.APHotelFacturaCabecera "
            + "WHERE (SituacionFactura='F') "
            + " AND (FechaFactura BETWEEN '" + tbx_FechaInicial.Text + "' "
            + " AND  '" + tbx_FechaFinal.Text + "') ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            MensajeR.Text = sql;
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Ventas no registradas ";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    if (!DBNull.Value.Equals(reader.Rows[0][0]))
                    {
                        totVentas = (string.Format("{0:0.00}", reader.Rows[0][0]));
                        totVentas = totVentas.Replace(",", ".");
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    protected void ProcesaVentasEstablecimientos()
    {
        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select SUM(baseNoGraIVA) + SUM(baseImponible)+ SUM(baseImpGrav) AS TotalVentas, NumSucursal "
            + "FROM AHoteleria.APHotelFacturaCabecera "
            + "WHERE (SituacionFactura='F') "
            + " AND (FechaFactura BETWEEN '" + tbx_FechaInicial.Text + "' "
            + " AND  '" + tbx_FechaFinal.Text + "') "
            + " GROUP BY NumSucursal";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            MensajeR.Text = sql;
            twd.WriteLine("<ventasEstablecimiento>");
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Ventas no registradas ";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    int NOficinas = reader.Rows.Count;
                    string numEstab;
                    for (int X = 0; X < NOficinas; X++)
                    {
                        if (!DBNull.Value.Equals(reader.Rows[X][0]))
                        {
                            twd.WriteLine("<ventaEst>");
                            totVentas = (string.Format("{0:0.00}", reader.Rows[X][0]));
                            totVentas = totVentas.Replace(",", ".");
                            numEstab = (string.Format("{0:000}", reader.Rows[X][1]));
                            twd.WriteLine("<codEstab>" + numEstab + "</codEstab>");
                            twd.WriteLine("<ventasEstab>" + totVentas + "</ventasEstab>");
                            twd.WriteLine("<ivaComp>0.00</ivaComp>");
                            twd.WriteLine("</ventaEst>");
                        }

                    }
                    twd.WriteLine("</ventasEstablecimiento>");
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    protected void btn_GeneradorATS_Click(object sender, EventArgs e)
    {
        NombreFileDetalle = NombreFileDetalleD + "ATS_" + mesI + anioI + "_O.xml";
        twd = new StreamWriter(NombreFileDetalle);
        string tipoInforma = "R";
        string idInforma = "1791241592001";
        string rSocial = "FUNDACION POLINAL ECUADOR";
        string numEstab = "002";
        string codOper = "IVA";
        ProcesaVentasTotales();

        twd.WriteLine("<?xml version=" + '"' + "1.0" + '"' + " encoding=" + '"' + "UTF-8" + '"' + "?>");
        twd.WriteLine("<iva>");
        twd.WriteLine("<TipoIDInformante>" + tipoInforma + "</TipoIDInformante>");
        twd.WriteLine("<IdInformante>" + idInforma + "</IdInformante>");
        twd.WriteLine("<razonSocial>" + rSocial + "</razonSocial>");
        twd.WriteLine("<Anio>" + anioI + "</Anio>");
        twd.WriteLine("<Mes>" + mesI + "</Mes>");
        twd.WriteLine("<numEstabRuc>" + numEstab + "</numEstabRuc>");
        twd.WriteLine("<totalVentas>" + totVentas + "</totalVentas>");
        twd.WriteLine("<codigoOperativo>" + codOper + "</codigoOperativo>");
        ProcesaCompras();
        ProcesaVentas();
        ProcesaVentasEstablecimientos();
        ProcesaAnulados();
        ProcesaAnuladosRetencion();
        twd.WriteLine("</iva>");
        twd.Close();
        conn.Close();

        //       MensajeR.Text += "Proceso de Generacion ATS Terminado";
        btn_DescargaATS.Visible = true;
    }

    protected void ProcesaCompras()
    {

        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select codSustento, tpIdProv, idProv, tipoComprobante, "
                       + "parteRel, fechaRegistro, establecimiento, puntoEmision, secuencial, "
                       + "fechaEmision, autorizacion, baseNoGraIVA, baseImponible, baseImpGrav, "
                       + "baseImpExe, montoIce, montoIva, "
                       + "valRetBien10, valRetServ20, valorRetBienes, valRetServ50, "
                       + "valorRetServicios, valRetServ100, totbasesImpReemb, formaPago, "
                       + "CodConceptoB, BaseImponIRB, TasaRetIRB, ValorRetIRB, "
                       + "CodConceptoS, BaseImponIRS, TasaRetIRS, ValorRetIRS, "
                       + "estabRetencion1, ptoEmiRetencion1, secRetencion1, "
                       + "autRetencion1, fechaEmiRet1 "
                       + "From AContabilidad.APContabCompras "
                       + "WHERE fechaRegistro BETWEEN '" + tbx_FechaInicial.Text + "' AND '" + tbx_FechaFinal.Text + "' "
                       + "AND tpIdProv != 'N'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Compras no registradas ";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    twd.WriteLine("<compras>");
                    NCompras = reader.Rows.Count;
                    for (int X = 0; X < NCompras; X++)
                    {
                        MensajeR.Text = "";
                        if (!DBNull.Value.Equals(reader.Rows[X][0]))
                        {
                            codSustento = (string)reader.Rows[X][0];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][1]))
                        {
                            tpIdProv = (string)reader.Rows[X][1];
                            //                           tpIdProv = tpIdProv.Substring(0, tpIdProv.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][2]))
                        {
                            idProv = (string)reader.Rows[X][2];
                            //                          idProv = idProv.Substring(0, idProv.IndexOf(' '));
                        }
                        MensajeR.Text = "40";
                        if (!DBNull.Value.Equals(reader.Rows[X][3]))
                        {
                            tipoComprobante = (string)reader.Rows[X][3];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][4]))
                        {
                            parteRel = (string)reader.Rows[X][4];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][5]))
                        {
                            fechaRegistro = (string.Format("{0:dd/MM/yyyy}", reader.Rows[X][5]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][6]))
                        {
                            establecimiento = (string)reader.Rows[X][6];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][7]))
                        {
                            puntoEmision = (string)reader.Rows[X][7];
                        }
                        MensajeR.Text = "60";
                        if (!DBNull.Value.Equals(reader.Rows[X][8]))
                        {
                            secuencial = (string)reader.Rows[X][8];
                            if (secuencial.IndexOf(' ') > 0)
                            {
                                secuencial = secuencial.Substring(0, secuencial.IndexOf(' '));
                            }
                        }
                        MensajeR.Text = "62";

                        if (!DBNull.Value.Equals(reader.Rows[X][9]))
                        {
                            fechaEmision = (string.Format("{0:dd/MM/yyyy}", reader.Rows[X][9]));
                        }
                        MensajeR.Text = "64" + secuencial;

                        if (!DBNull.Value.Equals(reader.Rows[X][10]))
                        {
                            autorizacion = (string)reader.Rows[X][10];
                            autorizacion = autorizacion.Substring(0, autorizacion.IndexOf(' '));
                        }
                        MensajeR.Text = "70 ";
                        if (!DBNull.Value.Equals(reader.Rows[X][11]))
                        {
                            baseNoGraIVA = (string.Format("{0:0.00}", reader.Rows[X][11]));
                            baseNoGraIVA = baseNoGraIVA.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][12]))
                        {
                            baseImponible = (string.Format("{0:0.00}", reader.Rows[X][12]));
                            baseImponible = baseImponible.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][13]))
                        {
                            baseImpGrav = (string.Format("{0:0.00}", reader.Rows[X][13]));
                            baseImpGrav = baseImpGrav.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][14]))
                        {
                            baseImpExe = (string.Format("{0:0.00}", reader.Rows[X][14]));
                            baseImpExe = baseImpExe.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][15]))
                        {
                            montoIce = (string.Format("{0:0.00}", reader.Rows[X][15]));
                            montoIce = montoIce.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][16]))
                        {
                            montoIva = (string.Format("{0:0.00}", reader.Rows[X][16]));
                            montoIva = montoIva.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][17]))
                        {
                            valRetBien10 = (string.Format("{0:0.00}", reader.Rows[X][17]));
                            valRetBien10 = valRetBien10.Replace(",", ".");
                        }
                        MensajeR.Text = "80";
                        if (!DBNull.Value.Equals(reader.Rows[X][18]))
                        {
                            valRetServ20 = (string.Format("{0:0.00}", reader.Rows[X][18]));
                            valRetServ20 = valRetServ20.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][19]))
                        {
                            valorRetBienes = (string.Format("{0:0.00}", reader.Rows[X][19]));
                            valorRetBienes = valorRetBienes.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][20]))
                        {
                            valRetBien50 = (string.Format("{0:0.00}", reader.Rows[X][20]));
                            valRetBien50 = valRetBien50.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][21]))
                        {
                            valorRetServicios = (string.Format("{0:0.00}", reader.Rows[X][21]));
                            valorRetServicios = valorRetServicios.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][22]))
                        {
                            valRetServ100 = (string.Format("{0:0.00}", reader.Rows[X][22]));
                            valRetServ100 = valRetServ100.Replace(",", ".");
                        }
                        totbasesImpReemb = "0.00";


                        formaPago = "20";


                        pagoLocExt = "01";
                        paisEfecPago = "NA";
                        aplicConvDobTrib = "NA";
                        pagExtSujRetNorLeg = "NA";
                        if (!DBNull.Value.Equals(reader.Rows[X][25]))
                        {
                            codRetAirB = (string)reader.Rows[X][25];
                            codRetAirB = codRetAirB.Substring(0, codRetAirB.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][26]))
                        {
                            baseImpAirB = (string.Format("{0:0.00}", reader.Rows[X][26]));
                            baseImpAirB = baseImpAirB.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][27]))
                        {
                            porcentajeAirB = (string.Format("{0:0.00}", reader.Rows[X][27]));
                            porcentajeAirB = porcentajeAirB.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][28]))
                        {
                            valRetAirB = (string.Format("{0:0.00}", reader.Rows[X][28]));
                            valRetAirB = valRetAirB.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][29]))
                        {
                            codRetAirS = (string)reader.Rows[X][29];
                            codRetAirS = codRetAirS.Substring(0, codRetAirS.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][30]))
                        {
                            baseImpAirS = (string.Format("{0:0.00}", reader.Rows[X][30]));
                            baseImpAirS = baseImpAirS.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][31]))
                        {
                            porcentajeAirS = (string.Format("{0:0.00}", reader.Rows[X][31]));
                            porcentajeAirS = porcentajeAirS.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][32]))
                        {
                            valRetAirS = (string.Format("{0:0.00}", reader.Rows[X][32]));
                            valRetAirS = valRetAirS.Replace(",", ".");
                        }

                        // estabRetencion1, ptoEmiRetencion1, secRetencion1, autRetencion1, fechaEmiRet1
                        if (!DBNull.Value.Equals(reader.Rows[X][32]))
                        {
                            estabRetencion1 = (string.Format("{0:000}", reader.Rows[X][33]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][34]))
                        {
                            ptoEmiRetencion1 = (string.Format("{0:000}", reader.Rows[X][34]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][35]))
                        {
                            secRetencion1 = (string)reader.Rows[X][35];
                            secRetencion1 = secRetencion1.Substring(0, secRetencion1.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][36]))
                        {
                            autRetencion1 = (string)reader.Rows[X][36];
                            //                         autRetencion1 = autRetencion1.Substring(0, autRetencion1.IndexOf(' '));
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][37]))
                        {
                            fechaEmiRet1 = (string.Format("{0:dd/MM/yyyy}", reader.Rows[X][37]));
                        }

                        twd.WriteLine("<detalleCompras>");
                        twd.WriteLine("<codSustento>" + codSustento + "</codSustento>");
                        twd.WriteLine("<tpIdProv>" + tpIdProv + "</tpIdProv>");
                        twd.WriteLine("<idProv>" + idProv + "</idProv>");
                        twd.WriteLine("<tipoComprobante>" + tipoComprobante + "</tipoComprobante>");
                        twd.WriteLine("<parteRel>" + parteRel + "</parteRel>");
                        twd.WriteLine("<fechaRegistro>" + fechaRegistro + "</fechaRegistro>");
                        twd.WriteLine("<establecimiento>" + establecimiento + "</establecimiento>");
                        twd.WriteLine("<puntoEmision>" + puntoEmision + "</puntoEmision>");
                        twd.WriteLine("<secuencial>" + secuencial + "</secuencial>");
                        twd.WriteLine("<fechaEmision>" + fechaEmision + "</fechaEmision>");
                        twd.WriteLine("<autorizacion>" + autorizacion + "</autorizacion>");
                        twd.WriteLine("<baseNoGraIva>" + baseNoGraIVA + "</baseNoGraIva>");
                        twd.WriteLine("<baseImponible>" + baseImponible + "</baseImponible>");
                        twd.WriteLine("<baseImpGrav>" + baseImpGrav + "</baseImpGrav>");
                        twd.WriteLine("<baseImpExe>" + baseImpExe + "</baseImpExe>");
                        twd.WriteLine("<montoIce>" + montoIce + "</montoIce>");
                        twd.WriteLine("<montoIva>" + montoIva + "</montoIva>");
                        twd.WriteLine("<valRetBien10>" + valRetBien10 + "</valRetBien10>");
                        twd.WriteLine("<valRetServ20>" + valRetServ20 + "</valRetServ20>");
                        twd.WriteLine("<valorRetBienes>" + valorRetBienes + "</valorRetBienes>");
                        twd.WriteLine("<valRetServ50>" + valRetBien50 + "</valRetServ50>");
                        twd.WriteLine("<valorRetServicios>" + valorRetServicios + "</valorRetServicios>");
                        twd.WriteLine("<valRetServ100>" + valRetServ100 + "</valRetServ100>");
                        twd.WriteLine("<totbasesImpReemb>" + totbasesImpReemb + "</totbasesImpReemb>");
                        twd.WriteLine("<pagoExterior>");
                        twd.WriteLine("<pagoLocExt>" + pagoLocExt + "</pagoLocExt>");
                        twd.WriteLine("<paisEfecPago>" + paisEfecPago + "</paisEfecPago>");
                        twd.WriteLine("<aplicConvDobTrib>" + aplicConvDobTrib + "</aplicConvDobTrib>");
                        twd.WriteLine("<pagExtSujRetNorLeg>" + pagExtSujRetNorLeg + "</pagExtSujRetNorLeg>");
                        twd.WriteLine("</pagoExterior>");
                        if ((Convert.ToDecimal(baseImponible.Replace(".", ",")) + Convert.ToDecimal(baseNoGraIVA.Replace(".", ",")) + Convert.ToDecimal(montoIce.Replace(".", ",")) + Convert.ToDecimal(montoIva.Replace(".", ",")) + Convert.ToDecimal(baseImpGrav.Replace(".", ","))) <= 1000)
                        {

                        }
                        else
                        {
                            twd.WriteLine("<formasDePago>");
                            twd.WriteLine("<formaPago>" + formaPago + "</formaPago>");
                            twd.WriteLine("</formasDePago>");
                        }

                        twd.WriteLine("<air>");
                        MensajeR.Text = "";
                        if ((valRetAirB != "0.00") || (valRetAirS != "0.00"))
                        {
                            if (valRetAirB != "0.00" || baseImpAirB != "0.00")
                            {
                                twd.WriteLine("<detalleAir>");
                                twd.WriteLine("<codRetAir>" + codRetAirB + "</codRetAir>");
                                twd.WriteLine("<baseImpAir>" + baseImpAirB + "</baseImpAir>");
                                twd.WriteLine("<porcentajeAir>" + porcentajeAirB + "</porcentajeAir>");
                                twd.WriteLine("<valRetAir>" + valRetAirB + "</valRetAir>");
                                twd.WriteLine("</detalleAir>");
                            }
                            if (valRetAirS != "0.00" || baseImpAirS != "0.00")
                            {
                                twd.WriteLine("<detalleAir>");
                                twd.WriteLine("<codRetAir>" + codRetAirS + "</codRetAir>");
                                twd.WriteLine("<baseImpAir>" + baseImpAirS + "</baseImpAir>");
                                twd.WriteLine("<porcentajeAir>" + porcentajeAirS + "</porcentajeAir>");
                                twd.WriteLine("<valRetAir>" + valRetAirS + "</valRetAir>");
                                twd.WriteLine("</detalleAir>");
                            }
                            if (montoIce != "0.00")
                            {
                                twd.WriteLine("<detalleAir>");
                                twd.WriteLine("<codRetAir>332</codRetAir>");
                                twd.WriteLine("<baseImpAir>" + montoIce + "</baseImpAir>");
                                twd.WriteLine("<porcentajeAir>0.00</porcentajeAir>");
                                twd.WriteLine("<valRetAir>0.00</valRetAir>");
                                twd.WriteLine("</detalleAir>");
                            }
                        }
                        else
                        {
                            //                          baseImpAirB = baseImpAirB.Replace(".", ",");
                            //                          baseImpAirS = baseImpAirS.Replace(".", ",");
                            baseImpAirD = Convert.ToDecimal(baseImpAirB.Replace(".", ","))
                                        + Convert.ToDecimal(baseImpAirS.Replace(".", ","))
                                        + Convert.ToDecimal(montoIce.Replace(".", ","));
                            if (baseImpAirD > 0)
                            {
                                baseImpAir = string.Format("{0:0.00}", baseImpAirD);
                                baseImpAir = baseImpAir.Replace(",", ".");
                                twd.WriteLine("<detalleAir>");
                                twd.WriteLine("<codRetAir>332</codRetAir>");
                                twd.WriteLine("<baseImpAir>" + baseImpAir + "</baseImpAir>");
                                twd.WriteLine("<porcentajeAir>0.00</porcentajeAir>");
                                twd.WriteLine("<valRetAir>0.00</valRetAir>");
                                twd.WriteLine("</detalleAir>");
                            }
                        }
                        twd.WriteLine("</air>");
                        sql = "";
                        MensajeR.Text = "100";
                        if ((secRetencion1 != "1" && secRetencion1 != "" && secRetencion1 != "0")
                              && (valRetAirB != "0.00" || valRetAirS != "0.00"
                                  || valorRetBienes != "0.00" || valorRetServicios != "0.00" || valRetServ100 != "0.00")
                           )
                        {
                            MensajeR.Text = "110";
                            twd.WriteLine("<estabRetencion1>" + estabRetencion1 + "</estabRetencion1>");
                            twd.WriteLine("<ptoEmiRetencion1>" + ptoEmiRetencion1 + "</ptoEmiRetencion1>");
                            twd.WriteLine("<secRetencion1>" + secRetencion1 + "</secRetencion1>");
                            twd.WriteLine("<autRetencion1>" + autRetencion1 + "</autRetencion1>");
                            twd.WriteLine("<fechaEmiRet1>" + fechaEmiRet1 + "</fechaEmiRet1>");
                        }
                        else
                        {
                            MensajeR.Text = "120";
                        }
                        twd.WriteLine("</detalleCompras>");
                    }
                    twd.WriteLine("</compras>");
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text += sql + " " + ex.Message;
                Response.Write("<script>window.alert('Error Proceso de Compras ...... ');</script>");
                //              twd.Close();
                //              conn.Close();
            }
        }
    }

    protected void ProcesaComprasSinICE()
    {
        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select codSustento, tpIdProv, idProv, tipoComprobante, "
                       + "parteRel, fechaRegistro, establecimiento, puntoEmision, secuencial, "
                       + "fechaEmision, autorizacion, baseNoGraIVA, baseImponible, baseImpGrav, "
                       + "baseImpExe, montoIce, montoIva, "
                       + "valRetBien10, valRetServ20, valorRetBienes, valRetServ50, "
                       + "valorRetServicios, valRetServ100, totbasesImpReemb, formaPago, "
                       + "CodConceptoB, BaseImponIRB, TasaRetIRB, ValorRetIRB, "
                       + "CodConceptoS, BaseImponIRS, TasaRetIRS, ValorRetIRS, "
                       + "estabRetencion1, ptoEmiRetencion1, secRetencion1, "
                       + "autRetencion1, fechaEmiRet1 "
                       + "From AContabilidad.APContabCompras "
                       + "WHERE fechaRegistro BETWEEN '" + tbx_FechaInicial.Text + "' AND '" + tbx_FechaFinal.Text + "' "
                       + "AND tpIdProv != 'N'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Compras no registradas ";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    twd.WriteLine("<compras>");
                    NCompras = reader.Rows.Count;
                    for (int X = 0; X < NCompras; X++)
                    {
                        if (!DBNull.Value.Equals(reader.Rows[X][0]))
                        {
                            codSustento = (string)reader.Rows[X][0];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][1]))
                        {
                            tpIdProv = (string)reader.Rows[X][1];
                            //                           tpIdProv = tpIdProv.Substring(0, tpIdProv.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][2]))
                        {
                            idProv = (string)reader.Rows[X][2];
                            //                          idProv = idProv.Substring(0, idProv.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][3]))
                        {
                            tipoComprobante = (string)reader.Rows[X][3];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][4]))
                        {
                            parteRel = (string)reader.Rows[X][4];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][5]))
                        {
                            fechaRegistro = (string.Format("{0:dd/MM/yyyy}", reader.Rows[X][5]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][6]))
                        {
                            establecimiento = (string)reader.Rows[X][6];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][7]))
                        {
                            puntoEmision = (string)reader.Rows[X][7];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][8]))
                        {
                            secuencial = (string)reader.Rows[X][8];
                            if (secuencial.IndexOf(' ') > 0)
                            {
                                secuencial = secuencial.Substring(0, secuencial.IndexOf(' '));
                            }
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][9]))
                        {
                            fechaEmision = (string.Format("{0:dd/MM/yyyy}", reader.Rows[X][9]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][10]))
                        {
                            autorizacion = (string)reader.Rows[X][10];
                            autorizacion = autorizacion.Substring(0, autorizacion.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][11]))
                        {
                            baseNoGraIVA = (string.Format("{0:0.00}", reader.Rows[X][11]));
                            baseNoGraIVA = baseNoGraIVA.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][12]))
                        {
                            baseImponible = (string.Format("{0:0.00}", reader.Rows[X][12]));
                            baseImponible = baseImponible.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][13]))
                        {
                            baseImpGrav = (string.Format("{0:0.00}", reader.Rows[X][13]));
                            baseImpGrav = baseImpGrav.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][14]))
                        {
                            baseImpExe = (string.Format("{0:0.00}", reader.Rows[X][14]));
                            baseImpExe = baseImpExe.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][15]))
                        {
                            montoIce = (string.Format("{0:0.00}", reader.Rows[X][15]));
                            montoIce = montoIce.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][16]))
                        {
                            montoIva = (string.Format("{0:0.00}", reader.Rows[X][16]));
                            montoIva = montoIva.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][17]))
                        {
                            valRetBien10 = (string.Format("{0:0.00}", reader.Rows[X][17]));
                            valRetBien10 = valRetBien10.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][18]))
                        {
                            valRetServ20 = (string.Format("{0:0.00}", reader.Rows[X][18]));
                            valRetServ20 = valRetServ20.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][19]))
                        {
                            valorRetBienes = (string.Format("{0:0.00}", reader.Rows[X][19]));
                            valorRetBienes = valorRetBienes.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][20]))
                        {
                            valRetBien50 = (string.Format("{0:0.00}", reader.Rows[X][20]));
                            valRetBien50 = valRetBien50.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][21]))
                        {
                            valorRetServicios = (string.Format("{0:0.00}", reader.Rows[X][21]));
                            valorRetServicios = valorRetServicios.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][22]))
                        {
                            valRetServ100 = (string.Format("{0:0.00}", reader.Rows[X][22]));
                            valRetServ100 = valRetServ100.Replace(",", ".");
                        }
                        totbasesImpReemb = "0.00";
                        formaPago = "20";

                        pagoLocExt = "01";
                        paisEfecPago = "NA";
                        aplicConvDobTrib = "NA";
                        pagExtSujRetNorLeg = "NA";
                        if (!DBNull.Value.Equals(reader.Rows[X][25]))
                        {
                            codRetAirB = (string)reader.Rows[X][25];
                            codRetAirB = codRetAirB.Substring(0, codRetAirB.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][26]))
                        {
                            baseImpAirB = (string.Format("{0:0.00}", reader.Rows[X][26]));
                            baseImpAirB = baseImpAirB.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][27]))
                        {
                            porcentajeAirB = (string.Format("{0:0.00}", reader.Rows[X][27]));
                            porcentajeAirB = porcentajeAirB.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][28]))
                        {
                            valRetAirB = (string.Format("{0:0.00}", reader.Rows[X][28]));
                            valRetAirB = valRetAirB.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][29]))
                        {
                            codRetAirS = (string)reader.Rows[X][29];
                            codRetAirS = codRetAirS.Substring(0, codRetAirS.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][30]))
                        {
                            baseImpAirS = (string.Format("{0:0.00}", reader.Rows[X][30]));
                            baseImpAirS = baseImpAirS.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][31]))
                        {
                            porcentajeAirS = (string.Format("{0:0.00}", reader.Rows[X][31]));
                            porcentajeAirS = porcentajeAirS.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][32]))
                        {
                            valRetAirS = (string.Format("{0:0.00}", reader.Rows[X][32]));
                            valRetAirS = valRetAirS.Replace(",", ".");
                        }

                        // estabRetencion1, ptoEmiRetencion1, secRetencion1, autRetencion1, fechaEmiRet1
                        if (!DBNull.Value.Equals(reader.Rows[X][32]))
                        {
                            estabRetencion1 = (string.Format("{0:000}", reader.Rows[X][33]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][34]))
                        {
                            ptoEmiRetencion1 = (string.Format("{0:000}", reader.Rows[X][34]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][35]))
                        {
                            secRetencion1 = (string)reader.Rows[X][35];
                            secRetencion1 = secRetencion1.Substring(0, secRetencion1.IndexOf(' '));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][36]))
                        {
                            autRetencion1 = (string)reader.Rows[X][36];
                            //                         autRetencion1 = autRetencion1.Substring(0, autRetencion1.IndexOf(' '));
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][37]))
                        {
                            fechaEmiRet1 = (string.Format("{0:dd/MM/yyyy}", reader.Rows[X][37]));
                        }

                        twd.WriteLine("<detalleCompras>");
                        twd.WriteLine("<codSustento>" + codSustento + "</codSustento>");
                        twd.WriteLine("<tpIdProv>" + tpIdProv + "</tpIdProv>");
                        twd.WriteLine("<idProv>" + idProv + "</idProv>");
                        twd.WriteLine("<tipoComprobante>" + tipoComprobante + "</tipoComprobante>");
                        twd.WriteLine("<parteRel>" + parteRel + "</parteRel>");
                        twd.WriteLine("<fechaRegistro>" + fechaRegistro + "</fechaRegistro>");
                        twd.WriteLine("<establecimiento>" + establecimiento + "</establecimiento>");
                        twd.WriteLine("<puntoEmision>" + puntoEmision + "</puntoEmision>");
                        twd.WriteLine("<secuencial>" + secuencial + "</secuencial>");
                        twd.WriteLine("<fechaEmision>" + fechaEmision + "</fechaEmision>");
                        twd.WriteLine("<autorizacion>" + autorizacion + "</autorizacion>");
                        twd.WriteLine("<baseNoGraIva>" + baseNoGraIVA + "</baseNoGraIva>");
                        twd.WriteLine("<baseImponible>" + baseImponible + "</baseImponible>");
                        twd.WriteLine("<baseImpGrav>" + baseImpGrav + "</baseImpGrav>");
                        twd.WriteLine("<baseImpExe>" + baseImpExe + "</baseImpExe>");
                        twd.WriteLine("<montoIce>" + montoIce + "</montoIce>");
                        twd.WriteLine("<montoIva>" + montoIva + "</montoIva>");
                        twd.WriteLine("<valRetBien10>" + valRetBien10 + "</valRetBien10>");
                        twd.WriteLine("<valRetServ20>" + valRetServ20 + "</valRetServ20>");
                        twd.WriteLine("<valorRetBienes>" + valorRetBienes + "</valorRetBienes>");
                        twd.WriteLine("<valRetServ50>" + valRetBien50 + "</valRetServ50>");
                        twd.WriteLine("<valorRetServicios>" + valorRetServicios + "</valorRetServicios>");
                        twd.WriteLine("<valRetServ100>" + valRetServ100 + "</valRetServ100>");
                        twd.WriteLine("<totbasesImpReemb>" + totbasesImpReemb + "</totbasesImpReemb>");
                        twd.WriteLine("<pagoExterior>");
                        twd.WriteLine("<pagoLocExt>" + pagoLocExt + "</pagoLocExt>");
                        twd.WriteLine("<paisEfecPago>" + paisEfecPago + "</paisEfecPago>");
                        twd.WriteLine("<aplicConvDobTrib>" + aplicConvDobTrib + "</aplicConvDobTrib>");
                        twd.WriteLine("<pagExtSujRetNorLeg>" + pagExtSujRetNorLeg + "</pagExtSujRetNorLeg>");
                        twd.WriteLine("</pagoExterior>");
                        if ((Convert.ToDecimal(baseImponible.Replace(".", ",")) + Convert.ToDecimal(baseImpGrav.Replace(".", ","))) <= 1000)
                        {
                        }
                        else
                        {
                            twd.WriteLine("<formasDePago>");
                            twd.WriteLine("<formaPago>" + formaPago + "</formaPago>");
                            twd.WriteLine("</formasDePago>");
                        }
                        if ((valRetAirB != "0.00") || (valRetAirS != "0.00"))
                        {
                            twd.WriteLine("<air>");
                            if (valRetAirB != "0.00" || baseImpAirB != "0.00")
                            {
                                twd.WriteLine("<detalleAir>");
                                twd.WriteLine("<codRetAir>" + codRetAirB + "</codRetAir>");
                                twd.WriteLine("<baseImpAir>" + baseImpAirB + "</baseImpAir>");
                                twd.WriteLine("<porcentajeAir>" + porcentajeAirB + "</porcentajeAir>");
                                twd.WriteLine("<valRetAir>" + valRetAirB + "</valRetAir>");
                                twd.WriteLine("</detalleAir>");
                            }
                            if (valRetAirS != "0.00" || baseImpAirS != "0.00")
                            {
                                twd.WriteLine("<detalleAir>");
                                twd.WriteLine("<codRetAir>" + codRetAirS + "</codRetAir>");
                                twd.WriteLine("<baseImpAir>" + baseImpAirS + "</baseImpAir>");
                                twd.WriteLine("<porcentajeAir>" + porcentajeAirS + "</porcentajeAir>");
                                twd.WriteLine("<valRetAir>" + valRetAirS + "</valRetAir>");
                                twd.WriteLine("</detalleAir>");
                            }
                            if (montoIce != "0.00")
                            {
                                /*   
                                      twd.WriteLine("<detalleAir>");
                                      twd.WriteLine("<codRetAir>332</codRetAir>");
                                      twd.WriteLine("<baseImpAir>" + montoIce + "</baseImpAir>");
                                      twd.WriteLine("<porcentajeAir>0.00</porcentajeAir>");
                                      twd.WriteLine("<valRetAir>0.00</valRetAir>");
                                      twd.WriteLine("</detalleAir>");
                                */
                            }
                            twd.WriteLine("</air>");
                        }
                        else
                        {
                            baseImpAirD = Convert.ToDecimal(baseImpAirB.Replace(".", ","))
                                        + Convert.ToDecimal(baseImpAirS.Replace(".", ","))
                                        + Convert.ToDecimal(montoIce.Replace(".", ","));
                            if (baseImpAirD > 0)
                            {
                                /*                             twd.WriteLine("<air>");
                                                               baseImpAir = string.Format("{0:0.00}", baseImpAirD);
                                                               baseImpAir = baseImpAir.Replace(",", ".");
                                                               twd.WriteLine("<detalleAir>");
                                                               twd.WriteLine("<codRetAir>332</codRetAir>");
                                                               twd.WriteLine("<baseImpAir>" + baseImpAir + "</baseImpAir>");
                                                               twd.WriteLine("<porcentajeAir>0.00</porcentajeAir>");
                                                               twd.WriteLine("<valRetAir>0.00</valRetAir>");
                                                               twd.WriteLine("</detalleAir>");
                                                               twd.WriteLine("</air>");
                                 */
                            }
                        }

                        if (
                               (secRetencion1 != "1" && secRetencion1 != "" && secRetencion1 != "0")
                            && (valRetAirB != "0.00" || valRetAirS != "0.00"
                                  || valorRetBienes != "0.00" || valorRetServicios != "0.00" || valRetServ100 != "0.00")
                            )
                        {
                            twd.WriteLine("<estabRetencion1>" + estabRetencion1 + "</estabRetencion1>");
                            twd.WriteLine("<ptoEmiRetencion1>" + ptoEmiRetencion1 + "</ptoEmiRetencion1>");
                            twd.WriteLine("<secRetencion1>" + secRetencion1 + "</secRetencion1>");
                            twd.WriteLine("<autRetencion1>" + autRetencion1 + "</autRetencion1>");
                            twd.WriteLine("<fechaEmiRet1>" + fechaEmiRet1 + "</fechaEmiRet1>");
                        }
                        twd.WriteLine("</detalleCompras>");
                    }
                    twd.WriteLine("</compras>");
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + ex.Message;
                Response.Write("<script>window.alert('Error en Proceso de Compras ');</script>");
                //              twd.Close();
                //              conn.Close();
            }
        }
    }
    protected void ProcesaVentas()
    {

        twd.WriteLine("<ventas>");
        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select TipoIdentificacion, Identificacion, ParteRelVenta, "
                + "TipoComprobante, TipoEmision, "
                + "COUNT(NumFactura) AS NComprobantes, "
                + "SUM(baseNoGraIVA) AS TotalSinIVA,  "
                + "SUM(baseImponible) AS TotalIVA0,  "
                + "SUM(baseImpGrav) AS TotalConIVA, "
                + "SUM(ValorIVA) AS TotalIVA, "
                + "SUM(ValorICE) AS TotalICE, "
                + "SUM(RetencionIVA) AS RetenIVA, "
                + "SUM(RetencionIR) AS RetenIR, "
                + "FormaPago, "
                + "MAX(NombreCompleto) AS NombreCliente "
                + "FROM AHoteleria.APHotelFacturaCabecera "
                + "WHERE (SituacionFactura='F') "
                + " AND (FechaFactura BETWEEN '" + tbx_FechaInicial.Text + "' "
                + " AND  '" + tbx_FechaFinal.Text + "') "
                + " GROUP BY TipoIdentificacion, Identificacion, TipoComprobante, "
                + " TipoEmision, FormaPago, ParteRelVenta";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            MensajeR.Text = sql;
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Ventas no registradas ";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    MensajeR.Text = "Inicio ";
                    NCompras = reader.Rows.Count;
                    for (int X = 0; X < NCompras; X++)
                    {
                        if (!DBNull.Value.Equals(reader.Rows[X][0]))
                        {
                            tpIdCliente = (string)reader.Rows[X][0];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][1]))
                        {
                            idCliente = (string)reader.Rows[X][1];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][2]))
                        {
                            parteRelVtas = (string)reader.Rows[X][2];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][3]))
                        {
                            tipoComprobanteV = (string)reader.Rows[X][3];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][4]))
                        {
                            tipoEmision = (string)reader.Rows[X][4];
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][5]))
                        {
                            numeroComprobantes = (string.Format("{0:0}", reader.Rows[X][5]));
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][6]))
                        {
                            baseNoGraIvaV = (string.Format("{0:0.00}", reader.Rows[X][6]));
                            baseNoGraIvaV = baseNoGraIvaV.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][7]))
                        {
                            baseImponibleV = (string.Format("{0:0.00}", reader.Rows[X][7]));
                            baseImponibleV = baseImponibleV.Replace(",", ".");
                        }
                        if (!DBNull.Value.Equals(reader.Rows[X][8]))
                        {
                            baseImpGravV = (string.Format("{0:0.00}", reader.Rows[X][8]));
                            baseImpGravV = baseImpGravV.Replace(",", ".");
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][9]))
                        {
                            montoIvaV = (string.Format("{0:0.00}", reader.Rows[X][9]));
                            montoIvaV = montoIvaV.Replace(",", ".");
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][10]))
                        {
                            montoIceV = (string.Format("{0:0.00}", reader.Rows[X][10]));
                            montoIceV = montoIceV.Replace(",", ".");
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][11]))
                        {
                            valorRetIva = (string.Format("{0:0.00}", reader.Rows[X][11]));
                            valorRetIva = valorRetIva.Replace(",", ".");
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][12]))
                        {
                            valorRetRenta = (string.Format("{0:0.00}", reader.Rows[X][12]));
                            valorRetRenta = valorRetRenta.Replace(",", ".");
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][13]))
                        {
                            formaPago = (string)reader.Rows[X][13];
                        }

                        if (!DBNull.Value.Equals(reader.Rows[X][14]))
                        {
                            DenoCli = (string)reader.Rows[X][14];
                            //                           DenoCli = DenoCli.Substring(0, DenoCli.IndexOf(' '));
                        }
                        twd.WriteLine("<detalleVentas>");
                        twd.WriteLine("<tpIdCliente>" + tpIdCliente + "</tpIdCliente>");
                        twd.WriteLine("<idCliente>" + idCliente + "</idCliente>");
                        if (tpIdCliente == "07")
                        {
                        }
                        else
                        {
                            twd.WriteLine("<parteRelVtas>" + parteRelVtas + "</parteRelVtas>");
                        }
                        if (tpIdCliente == "06")
                        {
                            twd.WriteLine("<tipoCliente>01</tipoCliente>");
                            twd.WriteLine("<denoCli>" + DenoCli + "</denoCli>");
                        }

                        twd.WriteLine("<tipoComprobante>" + tipoComprobanteV + "</tipoComprobante>");
                        twd.WriteLine("<tipoEmision>" + tipoEmision + "</tipoEmision>");
                        twd.WriteLine("<numeroComprobantes>" + numeroComprobantes + "</numeroComprobantes>");
                        twd.WriteLine("<baseNoGraIva>" + baseNoGraIvaV + "</baseNoGraIva>");
                        twd.WriteLine("<baseImponible>" + baseImponibleV + "</baseImponible>");
                        twd.WriteLine("<baseImpGrav>" + baseImpGravV + "</baseImpGrav>");
                        twd.WriteLine("<montoIva>" + montoIvaV + "</montoIva>");
                        twd.WriteLine("<montoIce>" + montoIceV + "</montoIce>");
                        twd.WriteLine("<valorRetIva>" + valorRetIva + "</valorRetIva>");
                        twd.WriteLine("<valorRetRenta>" + valorRetRenta + "</valorRetRenta>");
                        twd.WriteLine("<formasDePago>");
                        twd.WriteLine("<formaPago>" + formaPago + "</formaPago>");
                        twd.WriteLine("</formasDePago>");
                        twd.WriteLine("</detalleVentas>");
                    }
                    twd.WriteLine("</ventas>");
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text += ex.Message;
                //              twd.Close();
                //              conn.Close();
            }
        }

    }

    protected void ProcesaAnulados()
    {
        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select TipoComprobante, NumSucursal, NumCaja, NumPreFactura, NumAutorizacion "
                + "FROM AHoteleria.APHotelFacturaCabecera "
                + "WHERE (SituacionFactura='A') "
                + " AND (FechaPreFactura BETWEEN '" + tbx_FechaInicial.Text + "' "
                + " AND  '" + tbx_FechaFinal.Text + "') "
                + " ORDER BY TipoComprobante, NumSucursal, NumFactura ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            MensajeR.Text = sql;
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Anuladas no registradas ";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    secuencial = secuencialInicio = secuencialFin = "0";
                    twd.WriteLine("<anulados>");
                    NAnulados = reader.Rows.Count;
                    for (int X = 0; X < NAnulados; X++)
                    {
                        if (!DBNull.Value.Equals(reader.Rows[X][0]))
                        {
                            tipoComprobante = (string)reader.Rows[X][0];
                        }
                        //                       MensajeR.Text = "Tipo comprobante procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][1]))
                        {
                            establecimiento = (string.Format("{0:000}", reader.Rows[X][1]));
                        }
                        //                        MensajeR.Text = "Establecimiento procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][2]))
                        {
                            puntoEmision = (string.Format("{0:000}", reader.Rows[X][2]));
                        }
                        //                        MensajeR.Text = "Punto emision procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][3]))
                        {
                            secuencial = (string.Format("{0:0}", reader.Rows[X][3]));
                        }
//                        if (secuencial == "33760")
//                        {
//                            Response.Write("<script>window.alert('Autorizacion de Factura Anuladas " + (string)reader.Rows[X][4] + "');</script>");
//                        }
//                        MensajeR.Text = "secuencial procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][4]))
                        {
                            autorizacion = (string)reader.Rows[X][4];
                        }
                        else
                        {
                            MensajeR.Text = "Autorizacion con error " + (string)reader.Rows[X][4];
                            Response.Write("<script>window.alert('Autorizacion Errada en anulacion de factura " + secuencial + " " + (string)reader.Rows[X][4] + "');</script>");
                        }
                        //                        MensajeR.Text = "Autorizacion procesado";
                        if (secuencialInicio == "" || secuencialInicio == "0")
                        {
                            secuencialInicio = secuencial;
                            secuencialAux = secuencial;
                            autorizacionA = autorizacion;
                        }
                        else
                        {
                            if ((Convert.ToInt32(secuencial) - Convert.ToInt32(secuencialAux)) > 1 || (Convert.ToInt32(secuencial) - Convert.ToInt32(secuencialAux)) < 0)
                            {
                                secuencialFin = secuencialAux;
                                if (autorizacion=="")
                                {
                                    autorizacion = autorizacionA;
                                }
                                twd.WriteLine("<detalleAnulados>");
                                twd.WriteLine("<tipoComprobante>" + tipoComprobante + "</tipoComprobante>");
                                twd.WriteLine("<establecimiento>" + establecimiento + "</establecimiento>");
                                twd.WriteLine("<puntoEmision>" + puntoEmision + "</puntoEmision>");
                                twd.WriteLine("<secuencialInicio>" + secuencialInicio + "</secuencialInicio>");
                                twd.WriteLine("<secuencialFin>" + secuencialFin + "</secuencialFin>");
                                twd.WriteLine("<autorizacion>" + autorizacion + "</autorizacion>");
                                twd.WriteLine("</detalleAnulados>");
                                secuencialInicio = secuencial;
                                secuencialAux = secuencial;
                                secuencialFin = "";
                            }
                            else
                            {
                                secuencialAux = secuencial;
                            }
                        }

                    }
                    if (secuencialInicio != "" && secuencialFin == "")
                    {
                        secuencialFin = secuencialInicio;
                        twd.WriteLine("<detalleAnulados>");
                        twd.WriteLine("<tipoComprobante>" + tipoComprobante + "</tipoComprobante>");
                        twd.WriteLine("<establecimiento>" + establecimiento + "</establecimiento>");
                        twd.WriteLine("<puntoEmision>" + puntoEmision + "</puntoEmision>");
                        twd.WriteLine("<secuencialInicio>" + secuencialInicio + "</secuencialInicio>");
                        twd.WriteLine("<secuencialFin>" + secuencialFin + "</secuencialFin>");
                        twd.WriteLine("<autorizacion>" + autorizacion + "</autorizacion>");
                        twd.WriteLine("</detalleAnulados>");

                    }
                    //                   twd.WriteLine("</anulados>");
                }

            }
            catch (Exception ex)
            {
                MensajeR.Text += ex.Message;
                //              twd.Close();
                //              conn.Close();
            }
        }
    }
    protected void ProcesaAnuladosRetencion()
    {
        //       Response.Write("<script>window.alert('No puede procesar Retenciones Anuladas ');</script>");

        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select TipoReten, SucReten, VenReten, NumReten, AutorizaReten "
                + "FROM AContabilidad.APContabRetenciones "
                + "WHERE (SituacionReten='A') "
                + " AND (FechaComprob BETWEEN '" + tbx_FechaInicial.Text + "' "
                + " AND  '" + tbx_FechaFinal.Text + "') "
                + " ORDER BY SucReten, VenReten, NumReten ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            MensajeR.Text = sql;
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Retenciones Anuladas no registradas ";

                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    secuencial = secuencialInicio = secuencialFin = "0";
                    //                   twd.WriteLine("<anulados>");
                    NAnulados = reader.Rows.Count;
                    for (int X = 0; X < NAnulados; X++)
                    {
                        //                        if (!DBNull.Value.Equals(reader.Rows[X][0]))
                        //                        {
                        tipoComprobante = "07";
                        //                        }
                        //                       MensajeR.Text = "Tipo comprobante procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][1]))
                        {
                            establecimiento = (string.Format("{0:000}", reader.Rows[X][1]));

                        }
                        //                        MensajeR.Text = "Establecimiento procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][2]))
                        {
                            puntoEmision = (string.Format("{0:000}", reader.Rows[X][2]));
                        }
                        //                        MensajeR.Text = "Punto emision procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][3]))
                        {
                            secuencial = (string.Format("{0:0}", reader.Rows[X][3]));
                        }
                        //                        MensajeR.Text = "secuencial procesado";
                        if (!DBNull.Value.Equals(reader.Rows[X][4]))
                        {
                            autorizacion = (string)reader.Rows[X][4];
                        }
                        //                        MensajeR.Text = "Autorizacion procesado";
                        if (secuencialInicio == "" || secuencialInicio == "0")
                        {
                            secuencialInicio = secuencial;
                            secuencialAux = secuencial;
                            establecimientoAux = establecimiento;
                        }
                        else
                        {
                            if ((Convert.ToInt32(secuencial) - Convert.ToInt32(secuencialAux)) > 1 || (Convert.ToInt32(secuencial) - Convert.ToInt32(secuencialAux)) < 0)
                            {
                                secuencialFin = secuencialAux;
                                twd.WriteLine("<detalleAnulados>");
                                twd.WriteLine("<tipoComprobante>" + tipoComprobante + "</tipoComprobante>");
                                twd.WriteLine("<establecimiento>" + establecimiento + "</establecimiento>");
                                twd.WriteLine("<puntoEmision>" + puntoEmision + "</puntoEmision>");
                                twd.WriteLine("<secuencialInicio>" + secuencialInicio + "</secuencialInicio>");
                                twd.WriteLine("<secuencialFin>" + secuencialFin + "</secuencialFin>");
                                twd.WriteLine("<autorizacion>" + autorizacion + "</autorizacion>");
                                twd.WriteLine("</detalleAnulados>");
                                secuencialInicio = secuencial;
                                secuencialAux = secuencial;
                                secuencialFin = "";
                            }
                            else
                            {
                                secuencialAux = secuencial;
                            }
                        }

                    }
                    if (secuencialInicio != "" && secuencialFin == "")
                    {
                        secuencialFin = secuencialInicio;
                        twd.WriteLine("<detalleAnulados>");
                        twd.WriteLine("<tipoComprobante>" + tipoComprobante + "</tipoComprobante>");
                        twd.WriteLine("<establecimiento>" + establecimiento + "</establecimiento>");
                        twd.WriteLine("<puntoEmision>" + puntoEmision + "</puntoEmision>");
                        twd.WriteLine("<secuencialInicio>" + secuencialInicio + "</secuencialInicio>");
                        twd.WriteLine("<secuencialFin>" + secuencialFin + "</secuencialFin>");
                        twd.WriteLine("<autorizacion>" + autorizacion + "</autorizacion>");
                        twd.WriteLine("</detalleAnulados>");

                    }
                }
                twd.WriteLine("</anulados>");
            }
            catch (Exception ex)
            {
                MensajeR.Text += ex.Message;
                //              twd.Close();
                //              conn.Close();
            }
        }
    }
    protected void btn_DescargaATS_Click(object sender, EventArgs e)
    {
        /*     string fileName = "GenATS2.xml", myStringWebResource = null;
               string remoteUri = "~/SoftempWFP/AP_Contabilidad/";
               string DirDownload = "~\\C:\\CATS\\GenATS2.xml";
               // Create a new WebClient instance.
               WebClient myWebClient = new WebClient();
               // Concatenate the domain with the Web resource filename.
               myStringWebResource = remoteUri + fileName;
               MensajeR.Text = myStringWebResource + " " + DirDownload;
       */
        string filename = @"C:\CATS\" + "ATS_" + mesI + anioI + "_O.xml";
        FileInfo fileInfo = new FileInfo(filename);
        if (fileInfo.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.Flush();
            Response.TransmitFile(fileInfo.FullName);
            Response.End();

        }
    }

    protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        anioI = ddl_Year.SelectedItem.Text;
        tbx_AnioI.Text = anioI;

        OcultaProcesos();

    }

    protected void ddl_Mes_SelectedIndexChanged(object sender, EventArgs e)
    {
        mesI = ddl_Mes.SelectedItem.Value.ToString();
        tbx_MesI.Text = mesI;
        OcultaProcesos();

    }

    protected void OcultaProcesos()
    {
        btn_GeneradorATS.Visible = false;
        EFechaInicial.Visible = false;
        tbx_FechaInicial.Visible = false;
        EFechaFinal.Visible = false;
        tbx_FechaFinal.Visible = false;
        btn_RevisarInformacion.Visible = false;
        btn_ErrorDatosCompras.Visible = false;
        pnl_Procesos.Visible = false;
        OcultaPaneles();

    }
    protected void OcultaPaneles()
    {
        pnl_DetalleCompra.Visible = false;
        pnl_Compras.Visible = false;
        pnl_VentasCliente.Visible = false;
        pnl_VentasPeriodo.Visible = false;
        pnl_RetencionesAnuladas.Visible = false;
        pnl_FacturasAnuladas.Visible = false;
        pnl_ErrorCompras.Visible = false;
        pnl_DetalleVentas.Visible = false;
    }
    protected void btn_ValidaParametros_Click(object sender, ImageClickEventArgs e)
    {
        OcultaPaneles();
           
        FechaConvertida = Convert.ToDateTime(FechaBase);
        FechaBaseD = FechaConvertida;

        tbx_FechaInicial.Text = "01/" + mesI + "/" + anioI;
        FechaConvertida = Convert.ToDateTime(tbx_FechaInicial.Text);
        tbx_FechaInicial.Text = FechaConvertida.ToString("dd/MM/yyyy");
        FechaInicialB = FechaConvertida;

        switch (mesI)
        {
            case "1":
            case "3":
            case "5":
            case "7":
            case "8":
            case "01":
            case "03":
            case "05":
            case "07":
            case "08":
            case "10":
            case "12":
                diaI = "31";
                break;
            case "2":
            case "02":
                if ((Convert.ToInt16(anioI) % 4) == 0)
                {
                    diaI = "29";
                }
                else
                {
                    diaI = "28";
                }
                break;
            case "4":
            case "6":
            case "9":
            case "04":
            case "06":
            case "09":
            case "11":
                diaI = "30";
                break;
            default:
                break;
        }

        tbx_FechaFinal.Text = diaI + "/" + mesI + "/" + anioI;


        FechaConvertida = Convert.ToDateTime(tbx_FechaFinal.Text);
        tbx_FechaFinal.Text = FechaConvertida.ToString("dd/MM/yyyy");
        FechaFinalB = FechaConvertida;

        if (DateTime.Compare(FechaInicialB, FechaBaseD) >= 0)
        {
            EFechaFinal.Visible = true;
            EFechaInicial.Visible = true;
            tbx_FechaFinal.Visible = true;
            tbx_FechaInicial.Visible = true;
            btn_GeneradorATS.Visible = true;
            btn_RevisarInformacion.Visible = true;
            btn_ErrorDatosCompras.Visible = true;
            //           btn_DescargaATS.Visible = true;
            //           btn_GeneradorATSSinICE.Visible = true;
            MensajeR.Text = DateTime.Compare(FechaInicialB, FechaBaseD).ToString() + " " + FechaBase + " "
                + tbx_FechaInicial.Text + " " + tbx_FechaFinal.Text;

        }
        else
        {
            string MensajeError = "<script>window.alert('No puede procesar fechas anteriores al 01-10-2016 "
                + (string.Compare(tbx_FechaInicial.Text, FechaBase)).ToString() + " " + FechaBase + " "
                + tbx_FechaInicial.Text + " " + tbx_FechaFinal.Text + " ');</script>";
            Response.Write(MensajeError);
        }

        DesplegaRetenAnuladas();

        btn_FacturasAnuladas.Visible = true;
        btn_RetencionesAnuladas.Visible = true;
    }

    protected void btn_VerificaComprasXProveedor_Click(object sender, EventArgs e)
    {
        OcultaPaneles();

        dgv_Compras.Dispose();
        dgv_Compras.DataSourceID = "";
        dgv_Compras.DataSourceID = (string)(TComprasP.UniqueID);
        dgv_Compras.DataBind();

        pnl_Compras.Visible = true;
        ECompras.Visible = false;
        EComprasDiario.Visible = false;
        EComprasProv.Visible = true;
        pnl_Procesos.Enabled = false;
/*
        EDiario.Visible = false;
        tbx_NDiario.Visible = false;
        btn_VerificaComprasDiario.Visible = false;
        btn_VerificaVentas.Visible = false;
        ECliente.Visible = false;
        tbx_IdCliente.Visible = false;
        btn_VerificaVentas.Visible = false;
        btn_FacturasAnuladas.Visible = false;
        dgv_FacturasAnuladas.Visible = false;
        dgv_Ventas.Visible = false;
*/
/*
        dgv_ComprasProveedor.Dispose();
        dgv_ComprasProveedor.DataSourceID = "";
        dgv_ComprasProveedor.DataSourceID = (string)(TComprasP.UniqueID);
        dgv_ComprasProveedor.DataBind();
        pnl_ComprasProveedor.Visible = true;
*/


    }

    protected void btn_VerificaVentas_Click(object sender, EventArgs e)
    {
        OcultaPaneles();

        decimal baseSinIVA, baseIVA0, baseGraIVA, baseExentaIVA, IVAVentas, RetenIVAVentas, RetenIRVentas;
        dgv_VentasPeriodo.Dispose();
        dgv_VentasPeriodo.DataSourceID = "";
        dgv_VentasPeriodo.DataSourceID = (string)(TVentasCliente.UniqueID);
        dgv_VentasPeriodo.DataBind();
        baseSinIVA = baseIVA0 = baseGraIVA = baseExentaIVA = IVAVentas = RetenIVAVentas = RetenIRVentas = 0;
        int NTT = dgv_VentasPeriodo.Rows.Count;
        for (int X = 0; X < NTT; X++)
        {
            baseSinIVA += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[9].Text);
            baseIVA0 += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[10].Text);
            baseGraIVA += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[11].Text);
            IVAVentas += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[12].Text);
            RetenIVAVentas += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[13].Text);
            RetenIRVentas += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[14].Text);
        }
        tbx_NFacturasT.Text = (string.Format("{0:#,##0}", NTT));
        tbx_VentaGraIVAT.Text = (string.Format("{0:#,##0.00}", baseGraIVA));
        tbx_IVAVentasT.Text = (string.Format("{0:#,##0.00}", IVAVentas));
        tbx_RetIVAVentasT.Text = (string.Format("{0:#,##0.00}", RetenIVAVentas));
        tbx_RetenIRVentaT.Text = (string.Format("{0:#,##0.00}", RetenIRVentas));
        tbx_VentaExenIVAT.Text = (string.Format("{0:#,##0.00}", baseSinIVA));
        tbx_VentaConIVA0T.Text = (string.Format("{0:#,##0.00}", baseIVA0));
        pnl_VentasPeriodo.Visible = true;
        dgv_VentasPeriodo.Visible = true;
        EVentasPeriodo.Visible = false;
        EVentasCliente.Visible = true;
        pnl_Procesos.Enabled = false;
/*
        dgv_Ventas.Visible = true;


        EProveedor.Visible = false;
        tbx_IdProveedor.Visible = false;
        btn_VerificaComprasXProveedor.Visible = false;

        EDiario.Visible = false;
        tbx_NDiario.Visible = false;
        btn_VerificaComprasDiario.Visible = false;

        btn_FacturasAnuladas.Visible = false;
        //      dgv_Anulados.Visible = false;
        //      dgv_Compras.Visible = false;

        tbx_IdProveedor.Text = "";
        tbx_NDiario.Text = "";

        pnl_Compras.Visible = false;
        pnl_Compras.Visible = false;
        pnl_ErrorCompras.Visible = false;
        pnl_VentasCliente.Visible = true;
*/
    }

    protected void btn_RetencionesAnuladas_Click(object sender, EventArgs e)
    {

        /*
                sql = "SELECT estabRetencion1, MIN(secRetencion1) AS RInicial, MAX(secRetencion1) AS RFinal "
                    + "FROM SoftempBD.AContabilidad.APContabCompras "
                    + "WHERE (secRetencion1 > N'0') "
                    + "AND(fechaRegistro BETWEEN CONVERT(DATETIME, '" + tbx_FechaInicial.Text + "', 102) "
                    + "AND CONVERT(DATETIME, '2017-02-28 00:00:00', 102)) "
                    + "GROUP BY estabRetencion1 ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                DataTable reader = new DataTable();
                MensajeR.Text = sql;
                try
                {
                    reader.Load(cmd.ExecuteReader());
                    if (reader.Rows.Count == 0)
                    {
                        MensajeR.Text = sql + " Lectura de Rango de Retenciones errado ";

                        MensajeR.Visible = true;
                        cmd.Connection.Close();
                    }
                    else
                    {
                        string NSucursal, RInicial, RFinal;
                        for (int X = 0; X < reader.Rows.Count; X++)
                        {

                            if (!DBNull.Value.Equals(reader.Rows[X][0]))
                            {
                                NSucursal = (string)reader.Rows[X][0];
                            }
                            if (!DBNull.Value.Equals(reader.Rows[X][1]))
                            {
                                RInicial = (string)reader.Rows[X][1];
                            }
                            if (!DBNull.Value.Equals(reader.Rows[X][2]))
                            {
                                RFinal = (string)reader.Rows[X][2];
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MensajeR.Text = ex.Message.ToString();
                }
        */
        OcultaPaneles();
        dgv_RetenAnuladas.Dispose();
        dgv_RetenAnuladas.DataSourceID = "";
        dgv_RetenAnuladas.DataSourceID = (string)(TRetenAnuladas.UniqueID);
        dgv_RetenAnuladas.DataBind();

        pnl_RetencionesAnuladas.Visible = true;
        pnl_Procesos.Enabled = false;
/*
        tbx_IdProveedor.Text = "";
        tbx_IdCliente.Text = "";
        pnl_FacturasAnuladas.Visible = false;
        pnl_Compras.Visible = false;
        pnl_VentasCliente.Visible = false;
        pnl_ErrorCompras.Visible = false;
        dgv_Ventas.Visible = false;
        dgv_Compras.Visible = false;
        OcultaPaneles();
        pnl_RetencionesAnuladas.Visible = true;
        dgv_RetenAnuladas.Visible = true;
*/
    }

    protected void btn_GeneradorATSSinICE_Click(object sender, EventArgs e)
    {
        NombreFileDetalle = NombreFileDetalleD + "ATS_" + mesI + anioI + "_O.xml";
        twd = new StreamWriter(NombreFileDetalle);
        string tipoInforma = "R";
        string idInforma = "1791241592001";
        string rSocial = "FUNDACION POLINAL ECUADOR";
        string numEstab = "002";
        string codOper = "IVA";
        ProcesaVentasTotales();

        twd.WriteLine("<?xml version=" + '"' + "1.0" + '"' + " encoding=" + '"' + "UTF-8" + '"' + "?>");
        twd.WriteLine("<iva>");
        twd.WriteLine("<TipoIDInformante>" + tipoInforma + "</TipoIDInformante>");
        twd.WriteLine("<IdInformante>" + idInforma + "</IdInformante>");
        twd.WriteLine("<razonSocial>" + rSocial + "</razonSocial>");
        twd.WriteLine("<Anio>" + anioI + "</Anio>");
        twd.WriteLine("<Mes>" + mesI + "</Mes>");
        twd.WriteLine("<numEstabRuc>" + numEstab + "</numEstabRuc>");
        twd.WriteLine("<totalVentas>" + totVentas + "</totalVentas>");
        twd.WriteLine("<codigoOperativo>" + codOper + "</codigoOperativo>");
        ProcesaComprasSinICE();
        ProcesaVentas();
        ProcesaVentasEstablecimientos();
        ProcesaAnulados();
        //       Response.Write("<script>window.alert('No puede procesar Retenciones Anuladas ');</script>");
        ProcesaAnuladosRetencion();
        twd.WriteLine("</iva>");
        twd.Close();
        conn.Close();

        MensajeR.Text = "Proceso de Generacion ATS Terminado";
        btn_DescargaATS.Visible = true;
    }

    protected void btn_RevisarInformacion_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Text = "";
        OcultaDatosCompras();
        EProveedor.Visible = true;
        ECliente.Visible = true;
        EDiario.Visible = true;

        tbx_IdProveedor.Visible = true;
        tbx_IdCliente.Visible = true;
        tbx_NDiario.Visible = true;

        tbx_NDiario.Text = "";
        tbx_IdCliente.Text = "";
        tbx_IdProveedor.Text = "";

        btn_VerificaComprasXProveedor.Visible = true;
        btn_VerificaVentas.Visible = true;
        btn_VerificaVentasPeriodo.Visible = true;

        btn_VerificaComprasDiario.Visible = true;
        pnl_Procesos.Visible = true;
        pnl_Procesos.Enabled = true;
        OcultaPaneles();
    }

    protected void btn_OtroPeriodo_Click(object sender, ImageClickEventArgs e)
    {
        ddl_Mes.Enabled = true;
        ddl_Year.Enabled = true;
        tbx_AnioI.Enabled = true;
        tbx_MesI.Enabled = true;
        btn_ValidaParametros.Enabled = true;

        EFechaInicial.Visible = false;
        EFechaFinal.Visible = false;
        tbx_FechaInicial.Visible = false;
        tbx_FechaFinal.Visible = false;
        btn_GeneradorATS.Visible = false;
        EProveedor.Visible = false;
        ECliente.Visible = false;
        EDiario.Visible = false;
        tbx_NDiario.Visible = false;
        btn_VerificaComprasDiario.Visible = false;
        btn_ErrorDatosCompras.Visible = false;
        tbx_IdCliente.Visible = false;
        tbx_IdProveedor.Visible = false;
        dgv_Compras.Visible = false;
        dgv_Ventas.Visible = false;
        dgv_FacturasAnuladas.Visible = false;
        btn_VerificaComprasXProveedor.Visible = false;
        btn_VerificaVentas.Visible = false;
        btn_FacturasAnuladas.Visible = false;
        btn_DescargaATS.Visible = false;
        btn_RevisarInformacion.Visible = false;
        pnl_Procesos.Visible = false;
        OcultaPaneles();
        InicializaPeriodo();

    }
    protected void InicializaPeriodo()
    {
        dgv_ComprasErrorProv.Dispose();
        dgv_ComprasErrorProv.DataSourceID = "";
        dgv_ComprasErrorProv.DataSourceID = (string)(TComprasError.UniqueID);
        dgv_ComprasErrorProv.DataBind();


        dgv_Compras.Dispose();
        dgv_Compras.DataSourceID = "";
        dgv_Compras.DataSourceID = (string)(TComprasD.UniqueID);
        dgv_Compras.DataBind();

        dgv_VentasPeriodo.Dispose();
        dgv_VentasPeriodo.DataSourceID = "";
        dgv_VentasPeriodo.DataSourceID = (string)(TVentasPeriodo.UniqueID);
        dgv_VentasPeriodo.DataBind();

    }

    protected void OcultaDatosCompras()
    {
        ESustento.Visible = false;
        EDatProveedor.Visible = false;
        EComprobante.Visible = false;
        EBases.Visible = false;
        ERetenIVA.Visible = false;
        ERetenIRB.Visible = false;
        ERetenIRS.Visible = false;
        ddl_Sustento.Visible = false;
        tbx_codSustento.Visible = false;
        ddl_TipoIdContrib.Visible = false;
        tbx_tipIdProv.Visible = false;
        tbx_IdProveedN.Visible = false;
        ddl_TipoDocFuente.Visible = false;
        tbx_tipComprob.Visible = false;
        tbx_estab.Visible = false;
        tbx_punto.Visible = false;
        tbx_secuen.Visible = false;
        tbx_autoriza.Visible = false;

        EFEmision.Visible = false;
        tbx_FEmision.Visible = false;
        EComprobReten.Visible = false;
        tbx_estabR.Visible = false;
        tbx_secuenR.Visible = false;
        tbx_autorizaR.Visible = false;
        EFRegistro.Visible = false;
        tbx_FRegistro.Visible = false;

        tbx_valorConIva0.Visible = false;
        tbx_valorGravIVA.Visible = false;

        EIVA.Visible = false;
        EICE.Visible = false;
        tbx_valorICE.Visible = false;
        tbx_valorIVA.Visible = false;

        tbx_RIVA30.Visible = false;
        tbx_RIVA70.Visible = false;
        tbx_RIVA100.Visible = false;
        ddl_TipoBienes.Visible = false;
        tbx_codIRB.Visible = false;
        tbx_baseIRB.Visible = false;
        tbx_tasaIRB.Visible = false;
        tbx_RetenIRB.Visible = false;
        ddl_TipoServicios.Visible = false;
        tbx_codIRS.Visible = false;
        tbx_baseIRS.Visible = false;
        tbx_tasaIRS.Visible = false;
        tbx_RetenIRS.Visible = false;
        EClaveCompra.Visible = false;

        btn_HabilitaActualCompras.Visible = false;
        btn_ActualizaCompras.Visible = false;

        tbx_NumDiario.Visible = false;
        tbx_SecuenA.Visible = false;
        tbx_IdProvA.Visible = false;


    }
    protected void DesplegaDatosCompras()
    {
        pnl_DetalleCompra.Visible = true;
        ESustento.Visible = true;
        EDatProveedor.Visible = true;
        EComprobante.Visible = true;
        EBases.Visible = true;
        ERetenIVA.Visible = true;
        ERetenIRB.Visible = true;
        ERetenIRS.Visible = true;

        ddl_Sustento.Dispose();
        ddl_Sustento.DataSourceID = "";
        ddl_Sustento.DataSourceID = (string)(ds_Sustentos.UniqueID);
        ddl_Sustento.DataBind();
        ddl_Sustento.Items.Insert(0, "  Seleccione Sustento....");
        ddl_Sustento.SelectedIndex = 0;
        ddl_Sustento.Visible = true;

        tbx_codSustento.Visible = true;

        ddl_TipoIdContrib.Dispose();
        ddl_TipoIdContrib.DataSourceID = "";
        ddl_TipoIdContrib.DataSourceID = (string)(ds_TipoBenef.UniqueID);
        ddl_TipoIdContrib.DataBind();
        ddl_TipoIdContrib.Items.Insert(0, "  Seleccione Tipo de Identificacion....");
        ddl_TipoIdContrib.SelectedIndex = 0;
        ddl_TipoIdContrib.Visible = true;
        tbx_tipIdProv.Visible = true;
        tbx_IdProveedN.Visible = true;

        ddl_TipoDocFuente.Dispose();
        ddl_TipoDocFuente.DataSourceID = "";
        ddl_TipoDocFuente.DataSourceID = (string)(ds_TipoFuentes.UniqueID);
        ddl_TipoDocFuente.DataBind();
        ddl_TipoDocFuente.Items.Insert(0, "  Seleccione Tipo de Comprobante....");
        ddl_TipoDocFuente.SelectedIndex = 0;
        ddl_TipoDocFuente.Visible = true;

        tbx_tipIdProv.Visible = true;
        tbx_tipComprob.Visible = true;
        tbx_estab.Visible = true;
        tbx_punto.Visible = true;
        tbx_secuen.Visible = true;
        tbx_autoriza.Visible = true;

        EFEmision.Visible = true;
        tbx_FEmision.Visible = true;
        EComprobReten.Visible = true;
        tbx_estabR.Visible = true;
        tbx_ptoEmiR.Visible = true;
        tbx_secuenR.Visible = true;
        tbx_autorizaR.Visible = true;
        EFRegistro.Visible = true;
        tbx_FRegistro.Visible = true;

        tbx_valorConIva0.Visible = true;
        tbx_valorGravIVA.Visible = true;
        tbx_valorIVA.Visible = true;

        EIVA.Visible = true;
        EICE.Visible = true;
        tbx_valorICE.Visible = true;

        tbx_RIVA30.Visible = true;
        tbx_RIVA70.Visible = true;
        tbx_RIVA100.Visible = true;
        ddl_TipoBienes.Dispose();
        ddl_TipoBienes.DataSourceID = "";
        ddl_TipoBienes.DataSourceID = (string)(ds_ConceptosB.UniqueID);
        ddl_TipoBienes.DataBind();
        ddl_TipoBienes.Items.Insert(0, "  Cod. IR en Bienes....");
        ddl_TipoBienes.SelectedIndex = 0;
        ddl_TipoBienes.Visible = true;
        tbx_codIRB.Visible = true;
        tbx_baseIRB.Visible = true;
        tbx_tasaIRB.Visible = true;
        tbx_RetenIRB.Visible = true;
        ddl_TipoServicios.Dispose();
        ddl_TipoServicios.DataSourceID = "";
        ddl_TipoServicios.DataSourceID = (string)(ds_ConceptosS.UniqueID);
        ddl_TipoServicios.DataBind();
        ddl_TipoServicios.Items.Insert(0, "  Cod. IR en Servicios....");
        ddl_TipoServicios.SelectedIndex = 0;
        ddl_TipoServicios.Visible = true;
        tbx_codIRS.Visible = true;
        tbx_baseIRS.Visible = true;
        tbx_tasaIRS.Visible = true;
        tbx_RetenIRS.Visible = true;

        EParteRelCompras.Visible = true;

        tbx_ParteRelCompras.Visible = true;

        btn_HabilitaActualCompras.Visible = true;


    }
    protected void dgv_Compras_SelectedIndexChanged(object sender, EventArgs e)
    {
        DesplegaDatosCompras();
        int NF = dgv_Compras.SelectedIndex;
        tbx_codSustento.Text = dgv_Compras.Rows[NF].Cells[1].Text;
        tbx_tipIdProv.Text = dgv_Compras.Rows[NF].Cells[2].Text;
        tbx_IdProveedN.Text = dgv_Compras.Rows[NF].Cells[3].Text;
        tbx_IdProvA.Text = dgv_Compras.Rows[NF].Cells[3].Text;

        tbx_tipComprob.Text = dgv_Compras.Rows[NF].Cells[4].Text;
        tbx_estab.Text = dgv_Compras.Rows[NF].Cells[7].Text;
        tbx_punto.Text = dgv_Compras.Rows[NF].Cells[8].Text;
        tbx_secuen.Text = dgv_Compras.Rows[NF].Cells[9].Text;
        tbx_SecuenA.Text = dgv_Compras.Rows[NF].Cells[9].Text;
        tbx_FEmision.Text = dgv_Compras.Rows[NF].Cells[10].Text;
        tbx_autoriza.Text = dgv_Compras.Rows[NF].Cells[11].Text;
        tbx_NumDiario.Text = dgv_Compras.Rows[NF].Cells[12].Text;

        tbx_valorConIva0.Text = dgv_Compras.Rows[NF].Cells[13].Text;
        tbx_valorGravIVA.Text = dgv_Compras.Rows[NF].Cells[14].Text;
        tbx_valorIVA.Text = dgv_Compras.Rows[NF].Cells[15].Text;

        tbx_RIVA30.Text = dgv_Compras.Rows[NF].Cells[16].Text;
        tbx_RIVA70.Text = dgv_Compras.Rows[NF].Cells[17].Text;
        tbx_RIVA100.Text = dgv_Compras.Rows[NF].Cells[18].Text;

        tbx_estabR.Text = dgv_Compras.Rows[NF].Cells[19].Text;
        tbx_secuenR.Text = dgv_Compras.Rows[NF].Cells[20].Text;
        if (dgv_Compras.Rows[NF].Cells[21].Text != "&nbsp;")
        {
            tbx_autorizaR.Text = dgv_Compras.Rows[NF].Cells[21].Text;
        }
        else
        {
            tbx_autorizaR.Text = "";
        }
        tbx_FRegistro.Text = dgv_Compras.Rows[NF].Cells[22].Text;

        tbx_codIRB.Text = dgv_Compras.Rows[NF].Cells[23].Text;
        tbx_baseIRB.Text = dgv_Compras.Rows[NF].Cells[24].Text;
        tbx_tasaIRB.Text = dgv_Compras.Rows[NF].Cells[25].Text;
        tbx_RetenIRB.Text = dgv_Compras.Rows[NF].Cells[26].Text;

        tbx_codIRS.Text = dgv_Compras.Rows[NF].Cells[27].Text;
        tbx_baseIRS.Text = dgv_Compras.Rows[NF].Cells[28].Text;
        tbx_tasaIRS.Text = dgv_Compras.Rows[NF].Cells[29].Text;
        tbx_RetenIRS.Text = dgv_Compras.Rows[NF].Cells[30].Text;
        tbx_valorICE.Text = dgv_Compras.Rows[NF].Cells[31].Text;


        tbx_ParteRelCompras.Text = dgv_Compras.Rows[NF].Cells[39].Text;



    }








    protected void btn_VerificaComprasDiario_Click(object sender, EventArgs e)
    {
        OcultaPaneles();

        dgv_Compras.Dispose();
        dgv_Compras.DataSourceID = "";
        dgv_Compras.DataSourceID = (string)(TComprasD.UniqueID);
        dgv_Compras.DataBind();

        pnl_Compras.Visible = true;
        ECompras.Visible = false;
        EComprasDiario.Visible = true;
        EComprasProv.Visible = false;
        pnl_Procesos.Enabled = false;
/*
        tbx_IdCliente.Text = "";
        EProveedor.Visible = false;
        tbx_IdProveedor.Visible = false;
        btn_VerificaComprasXProveedor.Visible = false;
        ECliente.Visible = false;
        tbx_IdCliente.Visible = false;
        btn_VerificaVentas.Visible = false;
        btn_FacturasAnuladas.Visible = false;

        dgv_ComprasDiario.Dispose();
        dgv_ComprasDiario.DataSourceID = "";
        dgv_ComprasDiario.DataSourceID = (string)(TComprasD.UniqueID);
        dgv_ComprasDiario.DataBind();

        pnl_FacturasAnuladas.Visible = false;
        pnl_VentasCliente.Visible = false;
        pnl_ErrorCompras.Visible = false;
        pnl_ComprasXDiario.Visible = true;
        dgv_Compras.Visible = true;
*/
    }



    protected void btn_ActualizaCompras_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE AContabilidad.APContabCompras "
                + "Set "
                + "CodSustento='" + tbx_codSustento.Text + "', "
                + "tpIdProv='" + tbx_tipIdProv.Text + "', "
                + "idProv='" + tbx_IdProveedN.Text + "', "
                + "tipoComprobante='" + tbx_tipComprob.Text + "', "
                + "establecimiento='" + tbx_estab.Text + "', "
                + "puntoEmision='" + tbx_punto.Text + "', "
                + "secuencial='" + tbx_secuen.Text + "', "
                + "autorizacion='" + tbx_autoriza.Text + "', "
                + "baseImponible=" + tbx_valorConIva0.Text.Replace(",", ".") + ", "
                + "baseImpGrav=" + tbx_valorGravIVA.Text.Replace(",", ".") + ", "
                + "montoIVA=" + tbx_valorIVA.Text.Replace(",", ".") + ", "
                + "valorRetBienes=" + tbx_RIVA30.Text.Replace(",", ".") + ", "
                + "valorRetServicios=" + tbx_RIVA70.Text.Replace(",", ".") + ", "
                + "valRetServ100=" + tbx_RIVA100.Text.Replace(",", ".") + ", "
                + "CodConceptoB='" + tbx_codIRB.Text + "', "
                + "BaseImponIRB=" + tbx_baseIRB.Text.Replace(",", ".") + ", "
                + "TasaRetIRB=" + tbx_tasaIRB.Text.Replace(",", ".") + ", "
                + "ValorRetIRB=" + tbx_RetenIRB.Text.Replace(",", ".") + ", "
                + "CodConceptoS='" + tbx_codIRS.Text + "', "
                + "BaseImponIRS=" + tbx_baseIRS.Text.Replace(",", ".") + ", "
                + "TasaRetIRS=" + tbx_tasaIRS.Text.Replace(",", ".") + ", "
                + "ValorRetIRS=" + tbx_RetenIRS.Text.Replace(",", ".") + ", "
                + "montoIce=" + tbx_valorICE.Text.Replace(",", ".") + ", "
                + "fechaEmision='" + tbx_FEmision.Text + "', "
                + "ptoEmiRetencion1='" + tbx_ptoEmiR.Text + "', "
                + "estabRetencion1='" + tbx_estabR.Text.Trim() + "', "
                + "secRetencion1='" + tbx_secuenR.Text.Trim() + "', "
                + "autRetencion1='" + tbx_autorizaR.Text.Trim() + "', "
                + "fechaEmiRet1='" + tbx_FRegistro.Text + "', "
                + "parteRel='" + tbx_ParteRelCompras.Text + "', "
                + "UsuarioMod='" + (string)(Session["CodUsuarioM"]) + "', "
                + "FechaCompMod='" + DateTime.Now + "' "
                + "WHERE Diario='" + tbx_NumDiario.Text + "' "
                + "AND idProv='" + tbx_IdProvA.Text + "' "
                + "AND Secuencial='" + tbx_SecuenA.Text + "' ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
                MensajeR.Text = "Ok ";
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.Message + " " + sql;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
    protected void btn_HabilitaActualCompras_Click(object sender, ImageClickEventArgs e)
    {
        EClaveCompra.Visible = true;
        tbx_NumDiario.Visible = true;
        tbx_IdProvA.Visible = true;
        tbx_SecuenA.Visible = true;
        btn_ActualizaCompras.Visible = true;
    }

    protected void ddl_Sustento_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_codSustento.Text = ddl_Sustento.SelectedItem.Value.ToString();
    }

    protected void ddl_TipoIdContrib_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_tipIdProv.Text = ddl_TipoIdContrib.SelectedItem.Value.ToString();
    }

    protected void ddl_TipoBienes_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_codIRB.Text = ddl_TipoBienes.SelectedItem.Value.ToString();

    }

    protected void ddl_TipoServicios_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_codIRS.Text = ddl_TipoServicios.SelectedItem.Value.ToString();

    }



    protected void ddl_TipoDocFuente_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_tipComprob.Text = ddl_TipoDocFuente.SelectedItem.Value.ToString(); ;
    }

    protected void btn_ErrorDatosCompras_Click(object sender, ImageClickEventArgs e)
    {
        OcultaPaneles();
        dgv_ComprasErrorProv.Dispose();
        dgv_ComprasErrorProv.DataSourceID = "";
        dgv_ComprasErrorProv.DataSourceID = (string)(TComprasError.UniqueID);
        dgv_ComprasErrorProv.DataBind();

        pnl_ErrorCompras.Visible = true;
        dgv_ComprasErrorProv.Visible = true;

        pnl_FacturasAnuladas.Visible = false;
        pnl_Compras.Visible = false;
        pnl_VentasCliente.Visible = false;

    }

    protected void dgv_ComprasErrorProv_SelectedIndexChanged(object sender, EventArgs e)
    {
        DesplegaDatosCompras();
        int NF = dgv_ComprasErrorProv.SelectedIndex;
        tbx_codSustento.Text = dgv_ComprasErrorProv.Rows[NF].Cells[1].Text;
        tbx_tipIdProv.Text = dgv_ComprasErrorProv.Rows[NF].Cells[2].Text;
        tbx_IdProveedN.Text = dgv_ComprasErrorProv.Rows[NF].Cells[3].Text;
        tbx_IdProvA.Text = dgv_ComprasErrorProv.Rows[NF].Cells[3].Text;

        tbx_tipComprob.Text = dgv_ComprasErrorProv.Rows[NF].Cells[4].Text;
        tbx_estab.Text = dgv_ComprasErrorProv.Rows[NF].Cells[7].Text;
        tbx_punto.Text = dgv_ComprasErrorProv.Rows[NF].Cells[8].Text;
        tbx_secuen.Text = dgv_ComprasErrorProv.Rows[NF].Cells[9].Text;
        tbx_SecuenA.Text = dgv_ComprasErrorProv.Rows[NF].Cells[9].Text;
        tbx_FEmision.Text = dgv_ComprasErrorProv.Rows[NF].Cells[10].Text;
        tbx_autoriza.Text = dgv_ComprasErrorProv.Rows[NF].Cells[11].Text;
        tbx_NumDiario.Text = dgv_ComprasErrorProv.Rows[NF].Cells[12].Text;

        tbx_valorConIva0.Text = dgv_ComprasErrorProv.Rows[NF].Cells[13].Text;
        tbx_valorGravIVA.Text = dgv_ComprasErrorProv.Rows[NF].Cells[14].Text;
        tbx_valorIVA.Text = dgv_ComprasErrorProv.Rows[NF].Cells[15].Text;

        tbx_RIVA30.Text = dgv_ComprasErrorProv.Rows[NF].Cells[16].Text;
        tbx_RIVA70.Text = dgv_ComprasErrorProv.Rows[NF].Cells[17].Text;
        tbx_RIVA100.Text = dgv_ComprasErrorProv.Rows[NF].Cells[18].Text;

        tbx_estabR.Text = dgv_ComprasErrorProv.Rows[NF].Cells[19].Text;
        tbx_ptoEmiR.Text = dgv_ComprasErrorProv.Rows[NF].Cells[32].Text;
        tbx_secuenR.Text = dgv_ComprasErrorProv.Rows[NF].Cells[20].Text;
        if (dgv_ComprasErrorProv.Rows[NF].Cells[21].Text != "&nbsp;")
        {
            tbx_autorizaR.Text = dgv_ComprasErrorProv.Rows[NF].Cells[21].Text;
        }
        else
        {
            tbx_autorizaR.Text = "";
        }
        tbx_FRegistro.Text = dgv_ComprasErrorProv.Rows[NF].Cells[22].Text;

        tbx_codIRB.Text = dgv_ComprasErrorProv.Rows[NF].Cells[23].Text;
        tbx_baseIRB.Text = dgv_ComprasErrorProv.Rows[NF].Cells[24].Text;
        tbx_tasaIRB.Text = dgv_ComprasErrorProv.Rows[NF].Cells[25].Text;
        tbx_RetenIRB.Text = dgv_ComprasErrorProv.Rows[NF].Cells[26].Text;

        tbx_codIRS.Text = dgv_ComprasErrorProv.Rows[NF].Cells[27].Text;
        tbx_baseIRS.Text = dgv_ComprasErrorProv.Rows[NF].Cells[28].Text;
        tbx_tasaIRS.Text = dgv_ComprasErrorProv.Rows[NF].Cells[29].Text;
        tbx_RetenIRS.Text = dgv_ComprasErrorProv.Rows[NF].Cells[30].Text;
        tbx_valorICE.Text = dgv_ComprasErrorProv.Rows[NF].Cells[31].Text;

        tbx_ParteRelCompras.Text = dgv_ComprasErrorProv.Rows[NF].Cells[5].Text;
    }

    protected void btn_VerificaVentasPeriodo_Click(object sender, EventArgs e)
    {
        OcultaPaneles();

        decimal baseSinIVA, baseIVA0, baseGraIVA, baseExentaIVA, IVAVentas, RetenIVAVentas, RetenIRVentas;
        dgv_VentasPeriodo.Dispose();
        dgv_VentasPeriodo.DataSourceID = "";
        dgv_VentasPeriodo.DataSourceID = (string)(TVentasPeriodo.UniqueID);
        dgv_VentasPeriodo.DataBind();
        baseSinIVA= baseIVA0= baseGraIVA= baseExentaIVA= IVAVentas= RetenIVAVentas= RetenIRVentas = 0;
        int NTT = dgv_VentasPeriodo.Rows.Count;
        for (int X = 0; X < NTT; X++)
        {
            baseSinIVA += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[9].Text);
            baseIVA0 += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[10].Text);
            baseGraIVA += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[11].Text);
            IVAVentas += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[12].Text);
            RetenIVAVentas += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[13].Text);
            RetenIRVentas += decimal.Parse(dgv_VentasPeriodo.Rows[X].Cells[14].Text);
        }
        tbx_NFacturasT.Text= (string.Format("{0:#,##0}", NTT));
        tbx_VentaGraIVAT.Text = (string.Format("{0:#,##0.00}", baseGraIVA));
        tbx_IVAVentasT.Text = (string.Format("{0:#,##0.00}", IVAVentas));
        tbx_RetIVAVentasT.Text = (string.Format("{0:#,##0.00}", RetenIVAVentas)); 
        tbx_RetenIRVentaT.Text= (string.Format("{0:#,##0.00}", RetenIRVentas));
        tbx_VentaExenIVAT.Text= (string.Format("{0:#,##0.00}", baseSinIVA));
        tbx_VentaConIVA0T.Text = (string.Format("{0:#,##0.00}", baseIVA0));
        pnl_VentasPeriodo.Visible = true;
        dgv_VentasPeriodo.Visible = true;
        EVentasPeriodo.Visible = true;
        EVentasCliente.Visible = false;
        pnl_Procesos.Enabled = false;

    }



    protected void dgv_VentasPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnl_DetalleVentas.Visible = true;
        ProcesoModVentas();

    }
    protected void ProcesoModVentas()
    {
        int NF = dgv_VentasPeriodo.SelectedIndex;
        tbx_tipComprobVenta.Text = dgv_VentasPeriodo.Rows[NF].Cells[1].Text;

        tbx_estabVenta.Text = dgv_VentasPeriodo.Rows[NF].Cells[2].Text;
        tbx_puntoVenta.Text = dgv_VentasPeriodo.Rows[NF].Cells[3].Text;
        tbx_NumComprobVenta.Text = dgv_VentasPeriodo.Rows[NF].Cells[4].Text;
        tbx_estabVentaA.Text = dgv_VentasPeriodo.Rows[NF].Cells[2].Text;
        tbx_puntoVentaA.Text = dgv_VentasPeriodo.Rows[NF].Cells[3].Text;
        tbx_NumComprobVentaA.Text = dgv_VentasPeriodo.Rows[NF].Cells[4].Text;
        tbx_tipIdCliente.Text = dgv_VentasPeriodo.Rows[NF].Cells[5].Text;
        tbx_IdenCliente.Text = dgv_VentasPeriodo.Rows[NF].Cells[6].Text;
        tbx_ParteRelacion.Text = dgv_VentasPeriodo.Rows[NF].Cells[7].Text;
        tbx_TipoEmision.Text = dgv_VentasPeriodo.Rows[NF].Cells[8].Text;
        tbx_VentaExenIVA.Text = dgv_VentasPeriodo.Rows[NF].Cells[9].Text;
        tbx_VentaConIVA0.Text = dgv_VentasPeriodo.Rows[NF].Cells[10].Text;
        tbx_VentaGraIVA.Text = dgv_VentasPeriodo.Rows[NF].Cells[11].Text;
        tbx_ValorIVAenVenta.Text = dgv_VentasPeriodo.Rows[NF].Cells[12].Text;
        tbx_RetIVAVentas.Text = dgv_VentasPeriodo.Rows[NF].Cells[13].Text;
        tbx_RetenIRVenta.Text = dgv_VentasPeriodo.Rows[NF].Cells[14].Text;
        tbx_FormaPagoV.Text = dgv_VentasPeriodo.Rows[NF].Cells[15].Text;
        tbx_FechaComprobVenta.Text = dgv_VentasPeriodo.Rows[NF].Cells[15].Text;


    }
    protected void btn_HabilitaActualVenta_Click(object sender, ImageClickEventArgs e)
    {
        btn_ActualizaVenta.Visible = true;
    }

    protected void btn_ActualizaVenta_Click(object sender, ImageClickEventArgs e)
    {

    }







    protected void btn_FacturasAnuladas_Click(object sender, EventArgs e)
    {
        OcultaPaneles();
        dgv_FacturasAnuladas.Dispose();
        dgv_FacturasAnuladas.DataSourceID = "";
        dgv_FacturasAnuladas.DataSourceID = (string)(TFacturasAnuladas.UniqueID);
        dgv_FacturasAnuladas.DataBind();
        pnl_FacturasAnuladas.Visible = true;
        dgv_FacturasAnuladas.Visible = true;
        pnl_Procesos.Enabled = false;
/*
        tbx_IdProveedor.Text = "";
        tbx_IdCliente.Text = "";
        pnl_FacturasAnuladas.Visible = true;
        pnl_RetencionesAnuladas.Visible = false;
        
        pnl_Compras.Visible = false;
        pnl_VentasCliente.Visible = false;
        pnl_ErrorCompras.Visible = false;
        dgv_Ventas.Visible = false;
        dgv_Compras.Visible = false;
*/

    }

    protected void dgv_RetenAnuladas_SelectedIndexChanged(object sender, EventArgs e)
    {

        int NF = dgv_RetenAnuladas.SelectedIndex;
        tbx_tipoRetenAnul.Text = "07";
        tbx_estabRetenAnul.Text = dgv_RetenAnuladas.Rows[NF].Cells[2].Text;
        tbx_puntoRetenAnul.Text = "1";
        tbx_secuenRetenAnul.Text = dgv_RetenAnuladas.Rows[NF].Cells[3].Text;
        LeeRetenAnulada();
        btn_ModificaRetenAnul.Visible = false;
        btn_GrabaRetenAnul.Visible = false;

    }
    protected void LeeRetenAnulada()
    {
        using (conn = new SqlConnection(DatabaseConnectionString))
        {
            sql = "Select TipoReten, SucReten, VenReten, NumReten, AutorizaReten, FechaComprob "
                + "FROM AContabilidad.APContabRetenciones "
                + "WHERE SucReten='" + tbx_estabRetenAnul.Text + "' "
                + " AND VenReten='001' "
                + " AND NumReten='" + tbx_secuenRetenAnul.Text + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            MensajeR.Text = sql;
            try
            {
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = sql + " Retenciones Anuladas no registradas ";

                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    tipoComprobante = "07";
                    if (!DBNull.Value.Equals(reader.Rows[0][1]))
                    {

                        tbx_estabRetenAnul.Text = (string.Format("{0:000}", reader.Rows[0][1]));
                        estabRetencion1 = tbx_estabRetenAnul.Text;

                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][2]))
                    {
                        tbx_puntoRetenAnul.Text = (string.Format("{0:000}", reader.Rows[0][2]));
                        ptoEmiRetencion1 = tbx_puntoRetenAnul.Text;
                    }
                    //                        MensajeR.Text = "Punto emision procesado";
                    if (!DBNull.Value.Equals(reader.Rows[0][3]))
                    {
                        tbx_secuenRetenAnul.Text = (string.Format("{0:0}", reader.Rows[0][3]));
                        secRetencion1 = tbx_secuenRetenAnul.Text;

                    }
                    //                        MensajeR.Text = "secuencial procesado";
                    if (!DBNull.Value.Equals(reader.Rows[0][4]))
                    {
                        tbx_AutorRetenAnul.Text = (string)reader.Rows[0][4];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][5]))
                    {
                        tbx_FechaAnulReten.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][5]));

                    }
                }
                btn_HabilitaModRetenAnul.Visible = true;
                btn_EliminaRetenAnul.Visible = true;
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.Message.ToString();
            }
        }
    }


    protected void ArmaRetencion()
    {
        sql = "INSERT INTO AContabilidad.APContabRetenciones "
             + "(SucReten, VenReten, NumReten "
             + ", AutorizaReten, FechaComprob, SituacionReten, FechaCaducaReten "
             + ", CentroCosto, TipoComprob, NumComprob, DetalleComprob "
             + ", TipoContrib, TipoIdContrib, IdContrib, NombreContrib, MailContrib"
             + ", DireccionContrib, TelefonoContrib"
             + ", CodTipoFuente, TipoFuente "
             + ", SucFuente, VenFuente, NumFuente"
             + ", AutorizaFuente, FechaCaducaFuente"
             + ", CodConceptoB, CodRetIVAB"
             + ", BaseImponIRB, TasaRetenIRB "
             + ", RetencionIRB, BaseImponIVAB"
             + ", TasaRetenIVAB, RetencionIVAB"
             + ", CodConceptoS, CodRetIVAS"
             + ", BaseImponIRS, TasaRetenIRS"
             + ", RetencionIRS, BaseImponIVAS"
             + ", TasaRetenIVAS, RetencionIVAS"
             + ", ValorBienes, ValorServicios"
             + ", UserAlta, FechaCompAlta"
             + ") "
             + " VALUES ("
             + "'" + tbx_estabRetenAnul.Text + "', '" + tbx_puntoRetenAnul.Text + "', '" + tbx_secuenRetenAnul.Text + "', "
             + "'" + tbx_AutorRetenAnul.Text + "', '" + tbx_FechaAnulReten.Text + "', 'A', '1900-01-01', "
             + "'', '', '', '', "
             + "'', '', '9999999999', '', '', "
             + "'', '', "
             + "'', '', "
             + "'', '', '', "
             + "'', '', "
             + "'', '', "
             + "0, 0, "
             + "0, 0, "
             + "0, 0, "
             + "'', '', "
             + "0, 0, "
             + "0, 0, "
             + "0, 0, "
             + "0, 0, "
             + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
             + ")";
    }

    protected void btn_GrabaRetenAnul_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Text = "";
        FechaConvertida = Convert.ToDateTime(tbx_FechaInicial.Text);
        FechaInicialB = FechaConvertida;
        FechaConvertida = Convert.ToDateTime(tbx_FechaFinal.Text);
        FechaFinalB = FechaConvertida;
        FechaConvertida = Convert.ToDateTime(tbx_FechaAnulReten.Text);
        tbx_FechaAnulReten.Text = FechaConvertida.ToString("dd/MM/yyyy");
        FechaAnulReten = FechaConvertida;
        if ((DateTime.Compare(FechaAnulReten, FechaInicialB) >= 0) && (DateTime.Compare(FechaAnulReten, FechaFinalB) <= 0))
        {
            if (tbx_AutorRetenAnul.Text.Length >= 10)
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                    conn.Open();
                    try
                    {
                        ArmaRetencion();
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd = conn.CreateCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MensajeR.Text = sql + " " + ex.Message.ToString();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                DesplegaRetenAnuladas();
            }
            else
            {
                MensajeR.Text = tbx_FechaAnulReten.Text;
                Response.Write("<script>window.alert('Error en Autorizacion de Retención Anulada ...... ');</script>");
                tbx_AutorRetenAnul.Focus();
            }
        }
        else
        {
            Response.Write("<script>window.alert('Error en Fecha de Anulación ...... ');</script>");
            tbx_FechaAnulReten.Focus();
        }
    }

    protected void btn_NuevaRetenAnul_Click(object sender, ImageClickEventArgs e)
    {


        HabilitaAnulReten();
        tbx_secuenRetenAnul.Text = "";
        tbx_estabRetenAnul.Text = "";
        tbx_AutorRetenAnul.Text = "";
        tbx_FechaAnulReten.Text = "";

        btn_GrabaRetenAnul.Visible = true;
    }

    protected void btn_CopiaAutorRetenAnul_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Text = "";
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select NumAutorizaReten "
                + "From ASeguridad.TCentrosCosto "
                + "WHERE NSucursal='" + tbx_estabRetenAnul.Text + "'";
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    MensajeR.Text = "SUCURSAL NO IDENTIFICADO";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                    Response.Write("<script>window.alert('Establecimiento de retencion errado');</script>");
                    tbx_AutorRetenAnul.Focus();
                }
                else
                {
                    if (!DBNull.Value.Equals(reader.Rows[0][0]))
                    {
                        tbx_AutorRetenAnul.Text = Convert.ToString(reader.Rows[0][0]);
                    }
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = "Error en Proceso Lectura de Autorizacion" + ex.Message.ToString();

            }
        }
    }

    protected void ddl_estabRetenAnul_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_estabRetenAnul.Text = ddl_estabRetenAnul.SelectedItem.Value.ToString();
    }

    protected void HabilitaAnulReten()
    {
        ddl_estabRetenAnul.Enabled = true;
        tbx_estabRetenAnul.Enabled = true;
        tbx_secuenRetenAnul.Enabled = true;
        tbx_FechaAnulReten.Enabled = true;
        btn_CopiaAutorRetenAnul.Enabled = true;
        tbx_AutorRetenAnul.Enabled = true;

        ddl_estabRetenAnul.Items.Clear();
        ddl_estabRetenAnul.DataSourceID = "";
        ddl_estabRetenAnul.DataSourceID = (string)(ds_Sucursales.UniqueID);
        ddl_estabRetenAnul.DataBind();
        ddl_estabRetenAnul.Items.Insert(0, "  Seleccione establecimiento propietario de retención ....");
        ddl_estabRetenAnul.SelectedIndex = 0;
    }
    protected void DesHabilitaAnulReten()
    {
        ddl_estabRetenAnul.Enabled = false;
        tbx_estabRetenAnul.Enabled = false;
        tbx_secuenRetenAnul.Enabled = false;
        tbx_FechaAnulReten.Enabled = false;
        btn_CopiaAutorRetenAnul.Enabled = false;
        tbx_AutorRetenAnul.Enabled = false;
    }
    protected void btn_HabilitaModRetenAnul_Click(object sender, ImageClickEventArgs e)
    {

        HabilitaAnulReten();
        btn_ModificaRetenAnul.Visible = true;
        btn_EliminaRetenAnul.Visible = false;

    }

    protected void btn_ModificaRetenAnul_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "UPDATE AContabilidad.APContabRetenciones "
                + "SET "
                + "SucReten='" + tbx_estabRetenAnul.Text + "', "
                + "VenReten='" + tbx_puntoRetenAnul.Text + "', "
                + "NumReten='" + tbx_secuenRetenAnul.Text + "', "
                + "AutorizaReten='" + tbx_AutorRetenAnul.Text + "', "
                + "FechaComprob='" + tbx_FechaAnulReten.Text + "', "
                + "SituacionReten='A', "
                + "UserReversa='" + (string)(Session["CodUsuarioM"]) + "', "
                + "FechaCompReversa='" + DateTime.Now + "' "
                + "WHERE SucReten='" + estabRetencion1 + "' "
                + "AND VenReten='" + ptoEmiRetencion1 + "' "
                + "AND NumReten='" + secRetencion1 + "' ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
                DesplegaRetenAnuladas();
                MensajeR.Text = "Ok ";
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.Message + " " + sql;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    protected void DesplegaRetenAnuladas()
    {
        dgv_RetenAnuladas.Dispose();
        dgv_RetenAnuladas.DataSourceID = "";
        dgv_RetenAnuladas.DataSourceID = (string)(TRetenAnuladas.UniqueID);
        dgv_RetenAnuladas.DataBind();
    }
    protected void btn_EliminaRetenAnul_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "Delete FROM AContabilidad.APContabRetenciones "
                    + "WHERE SucReten='" + estabRetencion1 + "' "
                    + "AND VenReten='" + ptoEmiRetencion1 + "' "
                    + "AND NumReten='" + secRetencion1 + "' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                MensajeR.Text = sql;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                DesplegaRetenAnuladas();
                btn_EliminaRetenAnul.Visible = false;

            }
            catch (Exception ex)
            {
                MensajeR.Visible = true;
                MensajeR.Text += ex.Message;
            }
        }

    }



    protected void dgv_FacturasAnuladas_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btn_VerificaCompras_Click(object sender, EventArgs e)
    {
        OcultaPaneles();

        dgv_Compras.Dispose();
        dgv_Compras.DataSourceID = "";
        dgv_Compras.DataSourceID = (string)(TCompras.UniqueID);
        dgv_Compras.DataBind();

        pnl_Compras.Visible = true;
        ECompras.Visible = true;
        EComprasDiario.Visible = false;
        EComprasProv.Visible = false;
//        pnl_Procesos.Visible = false;
        pnl_Procesos.Enabled = false;
    }
}


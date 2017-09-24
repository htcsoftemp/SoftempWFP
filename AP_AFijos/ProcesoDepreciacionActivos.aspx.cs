
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

public partial class ProcesoDepreciacionActivos : System.Web.UI.Page
{
    static DateTime FechaConvertida;
    static string CUsuario;
    static string CodServicio = "";
    static int SWINC;
    static string sql;
    static int NL;
    static SqlCommand cmd;

    static string CentroCostosT, CodigoActivoT;
    static string CuentaContabDepreciaT, CuentaContabGastoDepreciaT;

    static decimal ValorDepreciacionNormalT;
    static string FechaDepreciacionT, FechaIngresoT, FechaUltimaDepreciacionT;
    static int VidaUtilT;
    static decimal PorcenResidualT, ValorResidualT, ValorInicialT;
    static decimal DepreciacionAcumuladaT, ValorActualT;
    static decimal TotalDepreciaXCuentaD, TotalDepreciaXCuentaCCD;
    static string TotalDepreciaXCuentaT, TotalDepreciaXCuentaCCT;

    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    //    decimal Valor, ValorE, ValorD, 
    //    decimal Valor, ValorC;
    //    int NPagos;
    //    void RegAccesos As New RegistraAccesos;
    SqlTransaction transaction;
    SqlCommand cmdCR; //, cmd;
    //   string UsuarioR, IPClienteR, EQClienteR, ProcesoR, ReferenciaR, ObservacionR;
    //   DateTime FechaSR, FechaCR;
    RegistraAccesos RA;
    ValidaPeriodoContable VPC;
    NumComprobDiario NCD;
    static string diaC, mesC, anioC;
    static int anioD, mesD;
    protected void Page_Load(object sender, EventArgs e)
    {
        CUsuario = (string)(Session["CodUsuarioM"]);
        GeneraFechaProceso();
        if (CUsuario == ""
            || String.IsNullOrEmpty(CUsuario)
            || String.IsNullOrEmpty((string)(Session["GrupoU"])))
        {

        }
        else
        {
            if (!IsPostBack)
            {
                if (CUsuario == "")
                {
                    Response.Write("<script>window.close();</script>");
                }
                else
                {
                    MensajeR.Text = "";

                    RA = new RegistraAccesos((string)(Session["CodUsuarioM"]),
                        (string)(Session["FechaProceso"]),
                        DateTime.Now,
                        (string)(Session["IPCliente"]),
                        (string)(Session["IPCliente"]),
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                        "Ingreso a Pago de Proveedores",
                        new System.IO.FileInfo(Page.Request.Url.AbsolutePath).Name);


                }
            }
        }
        
    }

    protected void GeneraFechaProceso()
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


        tbx_FechaDepreciacion.Text = diaC + "/" + mesC + "/" + anioC;

        //       MensajeR.Text = tbx_FechaFinal.Text;

        FechaConvertida = Convert.ToDateTime(tbx_FechaDepreciacion.Text);
        tbx_FechaDepreciacion.Text = FechaConvertida.ToString("dd/MM/yyyy");
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

    protected void GrabaComprobante()
    {
        int SWNC = 0;
        RA = new RegistraAccesos((string)(Session["CodUsuarioM"]), (string)(Session["FechaProceso"]), DateTime.Now,
            (string)(Session["IPCliente"]), (string)(Session["IPCliente"]),
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            tbx_NumDiario.Text, new System.IO.FileInfo(Page.Request.Url.AbsolutePath).Name);
        if (tbx_FechaDepreciacion.Text == "")
        {
            tbx_FechaDepreciacion.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        FechaConvertida = Convert.ToDateTime(tbx_FechaDepreciacion.Text);
        tbx_FechaDepreciacion.Text = FechaConvertida.ToString("dd/MM/yyyy");
        VPC = new ValidaPeriodoContable(tbx_FechaDepreciacion.Text);
        if (VPC.SituacionPeriodo != "A")
        {
            MensajeR.Text = "Estado de Periodo Contable Errado (" + VPC.SituacionPeriodo + ")";
            MensajeR.Visible = true;
        }
        else
        {

                if (tbx_NumDiario.Text == "" || tbx_NumDiario.Text == "0")
                {
                    NCD = new NumComprobDiario(tbx_TipoDiario.Text);
                    tbx_NumDiario.Text = Convert.ToString(NCD.NumComprobDiarioE);
                    SWNC = 0;
                }
                else
                {
                    SWNC = 1;
                }
                MensajeR.Text = tbx_NumDiario.Text;
                MensajeR.Visible = true;
                //      FechaTran = FechaDesafiliacion.Text;
                string sql = "";
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                    try
                    {
                        sql = "Insert INTO AContabilidad.APContabDiario "
                                  + "(TipoDiario, NumDiario, "
                                  + "FechaDiario, NombreBenef, "
                                  + "DetalleDiario, OfiRecep, "
                                  + "TipoBenef, IdBenef, "
                                  + "DireccionBenef, TelefConvBenef, TelefCelBenef, "
                                  + "EMailBenef, AutorizacionFuente, "
                                  + "UserAlta, FechaCompAlta "
                                  + ") "
                                  + " VALUES ("
                                  + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                                  + "'" + tbx_FechaDepreciacion.Text + "', '', "
                                  + "'Depreciacion ', '" + (string)(Session["CCostos"]) + "', "
                                  + "'', '', "
                                  + "'', '', '', "
                                  + "'', '', "
                                  + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                                  + ")";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        if (SWNC == 0)
                        {
                            NCD = new NumComprobDiario(tbx_TipoDiario.Text, Convert.ToInt32(tbx_NumDiario.Text));
                        }

                        //                ActualNumComprobante();


                    }
                    catch (Exception ex)
                    {
                        MensajeR.Text += ex.Message;
                    }
                }
            }
    }



    protected void btn_GrabarComprobante_Click(object sender, ImageClickEventArgs e)
    {
        /*
        RA = new RegistraAccesos((string)(Session["CodUsuarioM"]), (string)(Session["FechaProceso"]), DateTime.Now,
            (string)(Session["IPCliente"]), (string)(Session["IPCliente"]),
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            tbx_NumDiario.Text, new System.IO.FileInfo(Page.Request.Url.AbsolutePath).Name);
        if (tbx_IdContrib.Text == "")
        {
            MensajeR.Text = "Id. de Proveedor errado";
            MensajeR.Visible = true;
            tbx_IdContrib.Focus();
        }
        else
        { 
            if (tbx_FechaComprob.Text == "")
            {
                tbx_FechaComprob.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            FechaConvertida = Convert.ToDateTime(tbx_FechaComprob.Text);
            tbx_FechaComprob.Text = FechaConvertida.ToString("dd/MM/yyyy");

            VPC = new ValidaPeriodoContable(tbx_FechaComprob.Text);
            if (VPC.SituacionPeriodo != "A")
            {
                Response.Write("<script>window.alert('Periodo Contable Errado ');</script>");
                MensajeR.Text = "Periodo Contable Errado " + VPC.SituacionPeriodo;
            }
            else
            {
                if (tbx_NumDiario.Text == "" || tbx_NumDiario.Text == "0")
                {
                    NCD = new NumComprobDiario(tbx_TipoDiario.Text);
                    tbx_NumDiario.Text = Convert.ToString(NCD.NumComprobDiarioE);
                    GeneraComprobante();
                    NCD = new NumComprobDiario(tbx_TipoDiario.Text, Convert.ToInt32(tbx_NumDiario.Text));
                }
                else
                {
                    GeneraComprobante();
                }
                CodImp = false;
                Situacion = false;
                btn_IngresaTransacciones.Visible = true;
                btn_VistaPreComprobante.Visible = true;
                btn_GrabarComprobante.Visible = false;
                btn_IngresaTransacciones.Visible = true;
                DesplegaDatosFuente();
                tbx_NumDocFuente.Focus();
            }
        }
        */
    }

    protected string ProcesaActivosADepreciar()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "DELETE FROM AFijos.APActivosFijosMovDepreciaPer ";
                cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                sql = "Select "
                    + "CentroCostos, CodigoActivo, "
                    + "AFijos.APActivosFijosTGrupos.CuentaDepreciacion, "
                    + "FechaUltimaDepreciacion, FechaIngreso, VidaUtil, "
                    + "ValorResidual, ValorInicial,  "
                    + "DepreciacionAcumulada, ValorActual, "
                    + "AFijos.APActivosFijosTGrupos.CuentaGastos "
                    + "FROM AFijos.APActivosFijos "
                    + "INNER JOIN AFijos.APActivosFijosTGrupos "
                    + "ON AFijos.APActivosFijos.Grupo=AFijos.APActivosFijosTGrupos.CodGrupoActivo "
                    + "WHERE Categoria='10'";

                cmd = new SqlCommand(sql, conn);

                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                NL = reader.Rows.Count;
                if (NL == 0)
                {
                    // do something if the query return no rows 
                    // you may insert the relevant redirection you asked for
                    MensajeR.Text = "NO SE ENCONTARON ACTIVOS";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                    return "0";
                }
                else
                {
                    MensajeR.Text = "";
                    for (int i = 0; i < NL; i++)
                    {
                        if (!DBNull.Value.Equals(reader.Rows[i][0]))
                        {
                            CentroCostosT = (string)reader.Rows[i][0];
                            MensajeR.Text += " Centro Costo=" + (string)reader.Rows[i][0] + " ";
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][1]))
                        {
                            CodigoActivoT = (string)reader.Rows[i][1];
                            MensajeR.Text += " Codigo=" + (string)reader.Rows[i][1] + " ";
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][2]))
                        {
                            CuentaContabDepreciaT = (string)reader.Rows[i][2];
                            MensajeR.Text += " Grupo=" + (string)reader.Rows[i][2];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][3]))
                        {
                           FechaUltimaDepreciacionT = (string.Format("{0:dd/MM/yyyy}", reader.Rows[i][3]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][4]))
                        {
                            FechaIngresoT = (string.Format("{0:dd/MM/yyyy}", reader.Rows[i][4]));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][5]))
                        {
                            VidaUtilT = Convert.ToInt16((string.Format("{0:0}", reader.Rows[i][5])));
 //                           Response.Write("<script>window.alert('" + CodigoActivoT + " " + (string.Format("{0:0}", reader.Rows[i][5])) + "');</script>");
                        }

                        if (!DBNull.Value.Equals(reader.Rows[i][6]))
                        {
                            ValorResidualT = Convert.ToDecimal((string.Format("{0:0.00}", reader.Rows[i][6])));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][7]))
                        {
                            ValorInicialT = Convert.ToDecimal((string.Format("{0:0.00}", reader.Rows[i][7])));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][8]))
                        {
                            DepreciacionAcumuladaT = Convert.ToDecimal((string.Format("{0:0.00}", reader.Rows[i][8])));
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][9]))
                        {
                           ValorActualT = Convert.ToDecimal((string.Format("{0:0.00}", reader.Rows[i][9])));
                        }

                        if (!DBNull.Value.Equals(reader.Rows[i][10]))
                        {
                            CuentaContabGastoDepreciaT = (string)reader.Rows[i][10];
                        }

                        ValorDepreciacionNormalT = 0;
                        if (VidaUtilT != 0)
                        {
                            if ((ValorInicialT-DepreciacionAcumuladaT)<=ValorResidualT)
                            {
                                ValorDepreciacionNormalT = 0;
                            }
                            else
                            {
                                int NPerParaDepreciar;
                                TimeSpan ts = Convert.ToDateTime(tbx_FechaDepreciacion.Text) - Convert.ToDateTime(FechaUltimaDepreciacionT);  
                                int differenceInDays = ts.Days;
                                NPerParaDepreciar = differenceInDays / 30;
                                ValorDepreciacionNormalT = ((ValorInicialT - ValorResidualT) / VidaUtilT) * NPerParaDepreciar;
 //                               Response.Write("<script>window.alert('" + NPerParaDepreciar + " " + differenceInDays + "');</script>");

                                if ((ValorInicialT - DepreciacionAcumuladaT- ValorDepreciacionNormalT) <= ValorResidualT)
                                {
                                    ValorDepreciacionNormalT = (ValorInicialT - DepreciacionAcumuladaT - ValorResidualT);
                                }
                            }
                        }
                        sql = "Insert INTO AFijos.APActivosFijosMovDepreciaPer ("
                            + "Secuen, CentroCostos, CodigoActivo, "
                            + "CuentaContabDeprecia, CuentaContabGasto, "
                            + "ValorDepreciacionNormal, FechaDepreciacion, "
                            + "ValorInicial, FechaUltimDeprecia, "
                            + "DepreciacionAcumul, ValorResidual, "
                            + "VidaUtil, ValorActual, "
                            + "UsuarioProceso, FechaCompProceso "
                            + ") "
                            + " VALUES (" + i + ", "
                            + "'" + CentroCostosT + "', '" + CodigoActivoT + "', "
                            + "'" + CuentaContabDepreciaT + "', '" + CuentaContabGastoDepreciaT + "', "
                            + (string.Format("{0:0.00}", ValorDepreciacionNormalT)).Replace(",",".") + ", " 
                            + "'" + tbx_FechaDepreciacion.Text + "', "
                            + (string.Format("{0:0.00}", ValorInicialT)).Replace(",", ".") + ", "
                            + "'" + FechaUltimaDepreciacionT + "', "
                            + (string.Format("{0:0.00}", DepreciacionAcumuladaT)).Replace(",", ".") + ", "
                            + (string.Format("{0:0.00}", ValorResidualT)).Replace(",", ".") + ", "
                            + (string.Format("{0:0}", VidaUtilT))  + ", "
                            + (string.Format("{0:0.00}", ValorActualT)).Replace(",", ".") + ", "
                            + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "'"
                            + ")";
                        cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                    }
                    // Get the value of Col1 in the 3rd row (0 is the first row)
                    // Col1Value = (int)dt.Rows[2]["Col1"];

                    /*                  if (!DBNull.Value.Equals(reader.Rows[0][0]))
                                      {
                                          tbx_CentroCostos.Text = (string)reader.Rows[0][0];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][1]))
                                      {
                                          tbx_CodigoActivo.Text = (string)reader.Rows[0][1];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][2]))
                                      {
                                          tbx_Categoria.Text = (string)reader.Rows[0][2];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][3]))
                                      {
                                          tbx_GrupoActivos.Text = (string)reader.Rows[0][3];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][4]))
                                      {
                                          tbx_NombreActivo.Text = (string)reader.Rows[0][4];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][5]))
                                      {
                                          tbx_DescripcionActivo.Text = (string)reader.Rows[0][5];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][6]))
                                      {
                                          tbx_MarcaActivo.Text = (string)reader.Rows[0][6];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][7]))
                                      {
                                          tbx_SerieActivo.Text = (string)reader.Rows[0][7];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][8]))
                                      {
                                          tbx_NumHabitacion.Text = (string)reader.Rows[0][8];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][9]))
                                      {
                                          tbx_Ubicacion.Text = (string)reader.Rows[0][9];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][10]))
                                      {
                                          tbx_Responsable.Text = (string)reader.Rows[0][10];
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][11]))
                                      {
                                          tbx_RazonIngreso.Text = (string)reader.Rows[0][11];
                                      }




                                      if (!DBNull.Value.Equals(reader.Rows[0][14]))
                                      {
                                          tbx_PorcenValResid.Text = (string.Format("{0:0.00}", reader.Rows[0][14]));
                                      }



                                      if (!DBNull.Value.Equals(reader.Rows[0][16]))
                                      {
                                          tbx_ValorInicial.Text = (string.Format("{0:0.00}", reader.Rows[0][16]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][17]))
                                      {
                                          tbx_FUltimaDeprecia.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][17]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][18]))
                                      {
                                          tbx_DepreciaNormAcum.Text = (string.Format("{0:0.00}", reader.Rows[0][18]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][19]))
                                      {
                                          tbx_ValorActual.Text = (string.Format("{0:0.00}", reader.Rows[0][19]));
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][20]))
                                      {
                                          tbx_FUltimReval.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][20]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][21]))
                                      {
                                          tbx_ValorReval.Text = (string.Format("{0:0.00}", reader.Rows[0][21]));
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][22]))
                                      {
                                          tbx_FUltimaDepReval.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][22]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][23]))
                                      {
                                          tbx_ValorDepreReval.Text = (string.Format("{0:0.00}", reader.Rows[0][23]));
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][24]))
                                      {
                                          tbx_FUltimaMejora.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][24]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][25]))
                                      {
                                          tbx_ValorMejoras.Text = (string.Format("{0:0.00}", reader.Rows[0][25]));
                                      }
                                      if (!DBNull.Value.Equals(reader.Rows[0][26]))
                                      {
                                          tbx_FUltimDepMejoras.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][26]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][27]))
                                      {
                                          tbx_ValorDepMejoras.Text = (string.Format("{0:0.00}", reader.Rows[0][27]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][28]))
                                      {
                                          tbx_ValorDepAcelerada.Text = (string.Format("{0:0.00}", reader.Rows[0][28]));
                                      }

                                      if (!DBNull.Value.Equals(reader.Rows[0][29]))
                                      {
                                          tbx_ValorDepEspecial.Text = (string.Format("{0:0.00}", reader.Rows[0][29]));
                                      }


                                      + "FechaUltimaMejora, MejorasAcumuladas, "
                                      + "FechaUltimaDeprecMejoras, DepreciacionMejoras, "
                                      + "DepreciacionAceleradaAcumulada, DepreciacionEspecialAcumulada "
                  */
                    return (string.Format("{0:0.00}", reader.Rows[0][0]));


                    // or just make the other redirection from your question
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = VidaUtilT + " " + NL + " " + sql + " Error en Lectura de Activo Fijo " + ex.Message.ToString();
                MensajeR.Visible = true;
                return "0";
            }
        }
    }

    protected void btn_PreparaDepreciacion_Click(object sender, ImageClickEventArgs e)
    {
        if (tbx_FechaDepreciacion.Text == "")
        { 
            tbx_FechaDepreciacion.Focus();
        }
        else
        {
            FechaConvertida = Convert.ToDateTime(tbx_FechaDepreciacion.Text);
            tbx_FechaDepreciacion.Text = FechaConvertida.ToString("dd/MM/yyyy");
            ProcesaActivosADepreciar();
            gv_DepreciacionPeriodo.Visible = true;
            btn_ValidaDepreciacion.Visible = true;
        }
    }

    protected void btn_GrabarDiarioDepreciacion_Click(object sender, ImageClickEventArgs e)
    {
        FechaConvertida = Convert.ToDateTime(tbx_FechaDepreciacion.Text);
        tbx_FechaDepreciacion.Text = FechaConvertida.ToString("dd/MM/yyyy");

        VPC = new ValidaPeriodoContable(tbx_FechaDepreciacion.Text);
        if (VPC.SituacionPeriodo != "A")
        {
            Response.Write("<script>window.alert('Periodo Contable Errado ');</script>");
            MensajeR.Text = "Periodo Contable Errado " + VPC.SituacionPeriodo;
        }
        else
        {
            if (tbx_NumDiario.Text == "" || tbx_NumDiario.Text == "0")
            {
                NCD = new NumComprobDiario(tbx_TipoDiario.Text);
                tbx_NumDiario.Text = Convert.ToString(NCD.NumComprobDiarioG);
                //                       GeneraComprobante();
                NCD = new NumComprobDiario(tbx_TipoDiario.Text, Convert.ToInt32(tbx_NumDiario.Text));
                SWINC = 0;
            }
            else
            {
                SWINC = 1;
                //                       GeneraComprobante();
            }

        }
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            try
            {
                sql = "INSERT INTO AFijos.APActivosFijosHistor "
                    + " (CodigoActivo,CodigoTransaccion,FechaTransaccion, Documento, Secuencial, CentroCostos, "
                    + "	 ValorTran, ValorDepreciacionNormal, UserAlta, FechaCompAlta) "
                    + "SELECT  CodigoActivo, '31', FechaDepreciacion, " + tbx_NumDiario.Text + ", 2, CentroCostos, "
                    + " -ValorDepreciacionNormal, ValorDepreciacionNormal, UsuarioProceso, FechaCompProceso "
                    + "FROM    AFijos.APActivosFijosMovDepreciaPer ";

                cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                sql = "DROP TABLE ATrabajo.APActivosFijosAcumValTrans ";
                cmd = new SqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = ex.Message.ToString();

                }

                sql = "SELECT CodigoActivo, SUM(ValorTran) AS sValorTran, "
                    + "  SUM(ValorCompras) AS sValorCompras, "
                    + "  SUM(ValorMejoras) AS sValorMejoras, "
                    + "  SUM(ValorRevalorizacion) AS sValorRevalorizacion, "
                    + "  SUM(ValorDepreciacionNormal) AS sValorDepreciacionNormal, "
                    + "  SUM(ValorDepreciacionAcelerada) AS sValorDepreciacionAcelerada, "
                    + "  SUM(ValorDepreciacionEspecial) AS sValorDepreciacionEspecial, "
                    + "  SUM(ValorDepreciacionRevalorizacion) AS sValorDepreciacionRevalorizacion, "
                    + "  SUM(ValorDepreciacionMejoras) AS sValorDepreciacionMejoras,"
                    + "  SUM(ValorBaja) AS sValorBaja "
                    + "  INTO  ATrabajo.APActivosFijosAcumValTrans "
                    + "  FROM  AFijos.APActivosFijosHistor "
                    + "  GROUP BY CodigoActivo ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "UPDATE AFijos.APActivosFijos"
                    + " SET ValorActual = ATrabajo.APActivosFijosAcumValTrans.sValorTran, "
                    + " DepreciacionAcumulada = ATrabajo.APActivosFijosAcumValTrans.sValorDepreciacionNormal, "
                    + " RevalorizacionAcumulada = ATrabajo.APActivosFijosAcumValTrans.sValorRevalorizacion, "
                    + " DepreciacionRevAcumulada = ATrabajo.APActivosFijosAcumValTrans.sValorDepreciacionRevalorizacion, "
                    + " MejorasAcumuladas = ATrabajo.APActivosFijosAcumValTrans.sValorMejoras, "
                    + " DepreciacionMejoras = ATrabajo.APActivosFijosAcumValTrans.sValorDepreciacionMejoras, "
                    + " DepreciacionAceleradaAcumulada = ATrabajo.APActivosFijosAcumValTrans.sValorDepreciacionAcelerada, "
                    + " DepreciacionEspecialAcumulada = ATrabajo.APActivosFijosAcumValTrans.sValorDepreciacionEspecial "
                    + " FROM ATrabajo.APActivosFijosAcumValTrans INNER JOIN "
                    + " AFijos.APActivosFijos ON ATrabajo.APActivosFijosAcumValTrans.CodigoActivo = AFijos.APActivosFijos.CodigoActivo ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "Insert INTO AContabilidad.APContabDiario "
                                  + "(TipoDiario, NumDiario, "
                                  + "FechaDiario, NombreBenef, "
                                  + "DetalleDiario, OfiRecep, "
                                  + "TipoBenef, IdBenef, "
                                  + "DireccionBenef, TelefConvBenef, TelefCelBenef, "
                                  + "EMailBenef, AutorizacionFuente, "
                                  + "UserAlta, FechaCompAlta "
                                  + ") "
                                  + " VALUES ("
                                  + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                                  + "'" + tbx_FechaDepreciacion.Text + "', '', "
                                  + "'Depreciacion correspondiente al "  + tbx_FechaDepreciacion.Text + "', '" + (string)(Session["CCostos"]) + "', "
                                  + "'', '', "
                                  + "'', '', '', "
                                  + "'', '', "
                                  + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                                  + ")";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "SELECT FechaDepreciacion, CuentaContabDeprecia, CuentaContabGasto, "
                    + "SUM(ValorDepreciacionNormal) AS VDepreciaPer "
                    + "FROM AFijos.APActivosFijosMovDepreciaPer "
                    + "GROUP BY CuentaContabDeprecia, FechaDepreciacion, CuentaContabGasto "
                    + "HAVING SUM(ValorDepreciacionNormal) > 0.0099 ";

                cmd = new SqlCommand(sql, conn);
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                NL = reader.Rows.Count;
                if (NL == 0)
                {
                    // do something if the query return no rows 
                    // you may insert the relevant redirection you asked for
                    MensajeR.Text = "NO SE ENCONTARON ACTIVOS";
                    MensajeR.Visible = true;
                    cmd.Connection.Close();
                }
                else
                {
                    MensajeR.Text = "";
                    for (int i = 0; i < NL; i++)
                    {
                        if (!DBNull.Value.Equals(reader.Rows[i][1]))
                        {
                            CuentaContabDepreciaT = (string)reader.Rows[i][1];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][2]))
                        {
                            CuentaContabGastoDepreciaT = (string)reader.Rows[i][2];
                        }
                        if (!DBNull.Value.Equals(reader.Rows[i][3]))
                        {
                            TotalDepreciaXCuentaT = (string.Format("{0:0.00}", reader.Rows[i][3]));
                        }
                        sql = "Insert INTO AContabilidad.APContabTransacciones "
                          + "(TipoDiario, NumDiario, "
                          + "FechaDiario, "
                          + "CodigoCuenta, SecuenTran, "
                          + "ValorDebe, Valorhaber, "
                          + "TipoTran, SituacionTran, CentroCostos, "
                          + "TipoDocTran, NumDocTran, "
                          + "AutorizaDocTran, ReferDocTran, "
                          + "Observtran, NotaTran, "
                          + "UserAlta, FechaCompAlta "
                          + ") "
                          + " VALUES ("
                          + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                          + "'" + tbx_FechaDepreciacion.Text + "', "
                          + "'" + CuentaContabDepreciaT + "', '0', "
                          + TotalDepreciaXCuentaT.Replace(",", ".") + ", 0, "
                          + "'D', '0', '" + 000 + "', "
                          + "'', '', "
                          + "'', '', "
                          + "'Depreciacion', '', "
                          + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                          + ")";
                        cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                        sql = "Insert INTO AContabilidad.APContabTransacciones "
                              + "(TipoDiario, NumDiario, "
                              + "FechaDiario, "
                              + "CodigoCuenta, SecuenTran, "
                              + "ValorDebe, Valorhaber, "
                              + "TipoTran, SituacionTran, CentroCostos, "
                              + "TipoDocTran, NumDocTran, "
                              + "AutorizaDocTran, ReferDocTran, "
                              + "Observtran, NotaTran, "
                              + "UserAlta, FechaCompAlta "
                              + ") "
                              + " VALUES ("
                              + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                              + "'" + tbx_FechaDepreciacion.Text + "', "
                              + "'" + CuentaContabGastoDepreciaT + "', " + CuentaContabDepreciaT.Substring(2,6) + ", "
                              + "0, " + TotalDepreciaXCuentaT.Replace(",", ".") + ", "
                              + "'H', '0', '" + 000 + "', "
                              + "'', '', "
                              + "'', '', "
                              + "'Depreciacion', '', "
                              + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                              + ")";
                        cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                    }

                    sql = "SELECT CuentaContabDeprecia, CentroCostos, SUM(ValorDepreciacionNormal) AS VDepreciaPer "
                        + "FROM AFijos.APActivosFijosMovDepreciaPer "
                        + "GROUP BY CuentaContabDeprecia, CentroCostos "
                        + "HAVING SUM(ValorDepreciacionNormal) > 0.0099 ";

                    cmd = new SqlCommand(sql, conn);
                    reader = new DataTable();
                    reader.Load(cmd.ExecuteReader());
                    NL = reader.Rows.Count;
                    if (NL == 0)
                    {
                        // do something if the query return no rows 
                        // you may insert the relevant redirection you asked for
                        MensajeR.Text = "NO SE ENCONTARON ACTIVOS";
                        MensajeR.Visible = true;
                        cmd.Connection.Close();
                    }
                    else
                    {
                        MensajeR.Text = "";
                        for (int i = 0; i < NL; i++)
                        {
                            if (!DBNull.Value.Equals(reader.Rows[i][0]))
                            {
                                CuentaContabDepreciaT = (string)reader.Rows[i][0];
                            }
                            if (!DBNull.Value.Equals(reader.Rows[i][1]))
                            {
                                    CentroCostosT = (string)reader.Rows[i][1];
                            }
                            if (!DBNull.Value.Equals(reader.Rows[i][2]))
                            {
                                TotalDepreciaXCuentaT = (string.Format("{0:0.00}", reader.Rows[i][2]));
                            }
                            sql = "Insert INTO AContabilidad.APContabTransaccionesXCC "
                                  + "(TipoDiario, NumDiario, "
                                  + "FechaDiario, "
                                  + "CodigoCuenta, SecuenTran, "
                                  + "CentroDeCostos, "
                                  + "ValorDebe, Valorhaber, "
                                  + "TipoTran, TipoDocTranCC, "
                                  + "NumDocTranCC, "
                                  + "ObservTranCC, NotaCC, "
                                  + "UserAlta, FechaCompAlta "
                                  + ") "
                                  + " VALUES ("
                                  + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                                  + "'" + tbx_FechaDepreciacion.Text + "', "
                                  + "'" + CuentaContabDepreciaT + "', '" + 0 + "', "
                                  + "'" + CentroCostosT + "', "
                                  + TotalDepreciaXCuentaT.Replace(",",".") + ", 0, "
                                  + "'D', '', '', "
                                  + "'Depreciacion ', '', "
                                  + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                                  + ")";
                            cmd = new SqlCommand(sql, conn);
                            cmd.ExecuteNonQuery();

                            sql = "Insert INTO AContabilidad.APContabTransaccionesXCC "
                                  + "(TipoDiario, NumDiario, "
                                  + "FechaDiario, "
                                  + "CodigoCuenta, SecuenTran, "
                                  + "CentroDeCostos, "
                                  + "ValorDebe, Valorhaber, "
                                  + "TipoTran, TipoDocTranCC, "
                                  + "NumDocTranCC, "
                                  + "ObservTranCC, NotaCC, "
                                  + "UserAlta, FechaCompAlta "
                                  + ") "
                                  + " VALUES ("
                                  + "'" + tbx_TipoDiario.Text + "', " + tbx_NumDiario.Text + ", "
                                  + "'" + tbx_FechaDepreciacion.Text + "', "
                                  + "'" + CuentaContabGastoDepreciaT + "', '" + CuentaContabDepreciaT.Substring(2,6) + "', "
                                  + "'" + CentroCostosT + "', "
                                  + "0, " + TotalDepreciaXCuentaT.Replace(",", ".") + ", "
                                  + "'H', '', '', "
                                  + "'Depreciacion ', '', "
                                  + "'" + (string)(Session["CodUsuarioM"]) + "','" + DateTime.Now + "'"
                                  + ")";
                            cmd = new SqlCommand(sql, conn);
                            cmd.ExecuteNonQuery();


                            sql = "UPDATE AFijos.APActivosFijos "
                                + "SET FechaUltimaDepreciacion = AFijos.APActivosFijosMovDepreciaPer.FechaDepreciacion "
                                + "FROM AFijos.APActivosFijosMovDepreciaPer INNER JOIN  AFijos.APActivosFijos "
                                + "ON AFijos.APActivosFijosMovDepreciaPer.CodigoActivo = AFijos.APActivosFijos.CodigoActivo ";
                            cmd = new SqlCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                 if (SWINC == 0)
                {
                    NCD = new NumComprobDiario(tbx_TipoDiario.Text, Convert.ToInt32(tbx_NumDiario.Text));
                }

            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + ex.Message.ToString();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
    protected void btn_ValidaDepreciacion_Click(object sender, ImageClickEventArgs e)
    {
        pnl_Controles.Visible = true;
    }
}






























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































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
public partial class ProcesaFacturasAyuda : System.Web.UI.Page
{
    static string CodServicio;
    static decimal MontoSupGastosP, MontoMaxAyudaP, PorcenInferP, PorcenSuperP, MontoExterAyudaP;
    static int IndEliminacion;
    static string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SoftempBDFPD"].ConnectionString;
    static decimal VFacturaD, VProformaD, VDonacionD;

    static decimal MontoD, PorcentajeD, MCalculadoD;

    static decimal ProformasD, PPorcentaje, MCalculadoPD;

    static decimal DonacionD;
    static decimal VSolicitadoD, TGeneralD;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NSolicitud.Enabled = true;
            NSolicitud.Text = (string)(Session["NumSolicitud"]);
            NSolicitud.Focus();
            LeeParamAyudas();
            OcultaSolicitud();
            if (NSolicitud.Text == "")
            {
                OtroSocio.Focus();
            }
            else
            {
                LeeSolAyuda();
                DesplegaSolicitud();
                RecalculaFacturas();
                LeeDatosSocio();
                dg_ValorFacturas.Visible = true;
                VFactura.Focus();
            }
        }
        else
        {
            if (VFactura.Text!="" || VProforma.Text!="" || VDonacion.Text!="")
            {
                if (IndEliminacion == 0)
                {
                    ProcesaAgregarFactura();
                }
            }
            else
            {
                IndEliminacion = 0;
            }
        }
    }
    protected void InicializaDatos()
    {
        MensajeR.Text = "";
        NSolicitud.Text = "";
        Cedula.Text = "";
        Nombres.Text = "";
        Servicio.Text = "";
        Situacion.Text = "";
        FechaAfiliacion.Text = "";
        FechaSolicitud.Text = "";
        VFactura.Text = "0";
        VProforma.Text = "0";
        VDonacion.Text = "0";
        OcultaSolicitud();
    }

    protected void OtroSocio_Click(object sender, ImageClickEventArgs e)
    {
        Cedula.Enabled = true;
        InicializaDatos();
        OcultaSolicitud();
        NSolicitud.Enabled = true;
        NSolicitud.Focus();
    }
    public string LeeServicio(String CodServicio)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select DetalleServicio From ASocios.APSociosTServicio "
                         + " Where CodigoServicio = '" + CodServicio + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "SERVICIO NO IDENTIFICADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Servicio";
            }
            else
            {
                cmd.Connection.Close();
                return (string)reader.Rows[0][0];
            }
        }
    }
    public string LeeDetalleTipoCuentaIF(string TipoCuenta)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select DetalleTipoCuenta From ASocios.APSociosTIFISTipoCuenta "
                         + " Where CodigoTipoCuenta = '" + TipoCuenta + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "SERVICIO NO IDENTIFICADO";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Servicio";
            }
            else
            {
                cmd.Connection.Close();
                return (string)reader.Rows[0][0];
            }
        }
    }
    public string LeeSituacion(String CodSituacion)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select DetalleSituacion From ASocios.APSociosTSituacion "
                         + " Where CodigoSituacion = '" + CodSituacion + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodSituacion + "SITUACION NO IDENTIFICADA";
                MensajeR.Visible = true;
                cmd.Connection.Close();
                return "Error en Lectura de Situacion";
            }
            else
            {
                cmd.Connection.Close();
                return (string)reader.Rows[0][0];
            }
        }
    }
    protected void BuscarSocios_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select Identificacion, TipoSocio, "
                                + "Apellidos, Nombres, "
                                + "FechaAfil, Email, "
                                + "SituacionSocio, "
                                + "TipoIF, NombreIF, Codigo_BCE, "
                                + "TipoCuentaIF, NumCuenta, "
                                + "TelefonoConv, TelefonoCel, FechaDesafil "
                                + "From ASocios.APSocios  Where Identificacion = '"
                                + Cedula.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                // do something if the query return no rows
                // you may insert the relevant redirection you asked for
                MensajeR.Text = "CEDULA  NO REGISTRADA, BUSCAR POR NUMERO DE SOLICITUD";
                MensajeR.Visible = true;
                NSolicitud.Visible = true;
                EBuscaSolicitud.Visible = true;
                BuscarSolicitud.Visible = true;
                NSolicitud.Enabled = true;
                NSolicitud.Focus();

            }
            else
            {
                // Get the value of Col1 in the 3rd row (0 is the first row)
                // Col1Value = (int)dt.Rows[2]["Col1"];
                CodServicio = (string)reader.Rows[0][1];
                Servicio.Text = (string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                Servicio.Visible = true;
                Nombres.Text = (string)reader.Rows[0][2] + " " + (string)reader.Rows[0][3];
                FechaAfiliacion.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][4]));
                Situacion.Text = LeeSituacion((string)reader.Rows[0][6]);

                if ((string)reader.Rows[0][6] == "D")
                {
                    Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][14]));
                    OtroSocio.Focus();
                }
                else
                {
                    FechaSolicitud.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));
                    FechaSolicitud.Focus();
                }
            }
            cmd.Connection.Close();
        }


    }
    protected void LeeSolAyuda()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            int SolicitudNum;
            SolicitudNum = Convert.ToInt32(NSolicitud.Text);
            string sql = "Select Grado, Unidad, "
                    + "TelefonosRef, DocRecibidos, "
                    + "MontoDeGastos, PorcentajeAyuda, ValorSolicitado, "
                    + "Especialidad, CasaSalud, MedicoCertifica, "
                    + "AServ, ObservSolicitud, OtroBeneficiario, "
                    + "FechaSolicitud, TipoAyuda, RazonSolicitud, "
                    + "TipoBeneficiario, NombreBeneficiario, "
                    + "ValorAprobado, TotalPagado, "
                    + "ValorAprobado0, Beneficiario0, "
                    + "ValorAprobado1, Beneficiario1, "
                    + "ValorAprobado2, Beneficiario2, "
                    + "FechaAprobRechaz, ReferAprobRechaz, Identificacion, "
                    + "MontoProformas, MontoDonaciones, MontoFacturas, "
                    + "PorcentajeProformas, AyudaFacturas, AyudaProformas "
                    + "From ASocios.APSociosAyudas  Where NumSolicitud = "
                    + SolicitudNum;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                DataTable reader = new DataTable();
                reader.Load(cmd.ExecuteReader());
                if (reader.Rows.Count == 0)
                {
                    // do something if the query return no rows
                    // you may insert the relevant redirection you asked for
                    MensajeR.Text = "SOLICITUD NO REGISTRADA";
                    MensajeR.Visible = true;
                }
                else
                {
                    // Get the value of Col1 in the 3rd row (0 is the first row)
                    // Col1Value = (int)dt.Rows[2]["Col1"];

                    Monto.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    Porcentaje.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    MCalculado.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    Proformas.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    Donacion.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                    if (!DBNull.Value.Equals(reader.Rows[0][0]))
                    {
                        Grado.Text = (string)reader.Rows[0][0];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][1]))
                    {
                        Unidad.Text = (string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][10]))
                    {
                        TiempoServicio.Text = (string.Format("{0:0}", reader.Rows[0][10]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][3]))
                    {
                        DocumentosRecibidos.Text = (string)reader.Rows[0][3];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][31]))
                    {
                        Monto.Text = (string.Format("{0:0.00}", reader.Rows[0][31]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][4]))
                    {
                        TGeneral.Text = (string.Format("{0:0.00}", reader.Rows[0][4]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][5]))
                    {
                        Porcentaje.Text = (string.Format("{0:0}", reader.Rows[0][5]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][6]))
                    {
                        VSolicitado.Text = (string.Format("{0:0.00}", reader.Rows[0][6]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][29]))
                    {
                        Proformas.Text = (string.Format("{0:0.00}", reader.Rows[0][29]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][30]))
                    {
                        Donacion.Text = (string.Format("{0:0.00}", reader.Rows[0][30]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][10]))
                    {
                        TiempoServicio.Text = (string.Format("{0:0}", reader.Rows[0][10]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][11]))
                    {
                        ObservSolicitud.Text = (string)reader.Rows[0][11];
                    }
   
                    if (!DBNull.Value.Equals(reader.Rows[0][13]))
                    {
                        FechaSolicitud.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][13]));
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][14]))
                    {
                        TipoAyuda.Text = (string)reader.Rows[0][14];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][15]))
                    {
                        RazonSolicitud.Text = (string)reader.Rows[0][15];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][16]))
                    {
                        TipoBeneficiario.Text = (string)reader.Rows[0][16];
                    }
                    if (!DBNull.Value.Equals(reader.Rows[0][17]))
                    {
                        NombreBeneficiario.Text = (string)reader.Rows[0][17];
                    }           
                    Cedula.Text = (string)reader.Rows[0][28];

                    if (!DBNull.Value.Equals(reader.Rows[0][34]))
                    {
                        MCalculadoP.Text = (string.Format("{0:0.00}", reader.Rows[0][34]));
                     }

                    if (!DBNull.Value.Equals(reader.Rows[0][31]))
                    {
                        MCalculado.Text = (string.Format("{0:0.00}", reader.Rows[0][33]));
                    }

                    if (!DBNull.Value.Equals(reader.Rows[0][32]))
                    {
                        PorcentajeP.Text = (string.Format("{0:0}", reader.Rows[0][32]));
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " +  ex.ToString();
            }
            cmd.Connection.Close();
        }
    }
   protected void OcultaSolicitud()
    {
        EFechaSolicitud.Visible = false;
        FechaSolicitud.Visible = false;
        EUnidad.Visible = false;

        Unidad.Visible = false;
        EGrado.Visible = false;

        Grado.Visible = false;
        EAserv.Visible = false;
        TiempoServicio.Visible = false;
        ETipoAyuda.Visible = false;
        TipoAyuda.Visible = false;
        RazonSolicitud.Visible = false;
        EBeneficiario.Visible = false;
        TipoBeneficiario.Visible = false;
        NombreBeneficiario.Visible = false;
        EMonto.Visible = false;
        Monto.Visible = false;
        EPorcentaje.Visible = false;
        Porcentaje.Visible = false;
        MCalculado.Visible = false;
        EDocumentos.Visible = false;
        DocumentosRecibidos.Visible = false;
        EObservSolicitud.Visible = false;
        ObservSolicitud.Visible = false;


        EFactura.Visible = false;
        SecuenFacturas.Visible = false;
        EVFactura.Visible = false;
        VFactura.Visible = false;

        EVProforma.Visible = false;
        VProforma.Visible = false;
        EVDonacion.Visible = false;
        VDonacion.Visible = false;
        TProformas.Visible = false;
        TDonaciones.Visible = false;

        EObservFactura.Visible = false;
        ObservFactura.Visible = false;
        btn_AgregarFactura.Visible = false;
        btn_EliminarFactura.Visible = false;
        btn_TerminarIngresoFacturas.Visible = false;
        btn_TerminarIngresoFacturas0.Visible = false;
        ImprimirFacturas.Visible = false;
        ETFacturas.Visible = false;
        TGeneral.Visible = false;
        ETFacturas0.Visible = false;
        TFacturas.Visible = false;
        EProformas.Visible = false;
        Proformas.Visible = false;
        PorcentajeP.Visible = false;
        MCalculadoP.Visible = false;
        EDonacion.Visible = false;
        Donacion.Visible = false;
        VSolicitado.Visible = false;
        ETAyuda.Visible = false;

    }
    protected void DesplegaSolicitud()
    {
        EFechaSolicitud.Visible = true;
        FechaSolicitud.Visible = true;
        EUnidad.Visible = true;
        Unidad.Visible = true;
        EGrado.Visible = true;
        Grado.Visible = true;
        EAserv.Visible = true;
        TiempoServicio.Visible = true;
        ETipoAyuda.Visible = true;
        TipoAyuda.Visible = true;
        RazonSolicitud.Visible = true;
       
        EBeneficiario.Visible = true;
        TipoBeneficiario.Visible = true;
        NombreBeneficiario.Visible = true;
        EMonto.Visible = true;
        Monto.Visible = true;
        EPorcentaje.Visible = true;
        Porcentaje.Visible = true;
        MCalculado.Visible = true;
        EDocumentos.Visible = true;
        DocumentosRecibidos.Visible = true;
        //       EObservacion.Visible = true;
        EObservSolicitud.Visible = true;
        ObservSolicitud.Visible = true;

        EFactura.Visible = true;
        SecuenFacturas.Visible = true;
        EVFactura.Visible = true;
        VFactura.Visible = true;

        EVProforma.Visible = true;
        VProforma.Visible = true;
        EVDonacion.Visible = true;
        VDonacion.Visible = true;
        TProformas.Visible = true;
        TDonaciones.Visible = true;

        EObservFactura.Visible = true;
        ObservFactura.Visible = true;
        btn_AgregarFactura.Visible = true;
        btn_TerminarIngresoFacturas.Visible = true;
        btn_TerminarIngresoFacturas0.Visible = true;
        ImprimirFacturas.Visible = true;
        ETFacturas.Visible = true;
        TGeneral.Visible = true;
        ETFacturas0.Visible = true;
        TFacturas.Visible = true;

        EProformas.Visible = true;
        Proformas.Visible = true;
        PorcentajeP.Visible = true;
        MCalculadoP.Visible = true;
        EDonacion.Visible = true;
        Donacion.Visible = true;
        VSolicitado.Visible = true;
        ETAyuda.Visible = true;
    }
    public void LeeParamAyudas()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select MontoSupGastos, MontoMaximoAyuda, PorcenSuper, PorcenInfer, MontoExterAyuda From ASeguridad.DatosEmpresa ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                MensajeR.Text = CodServicio + "EMPRESA NO IDENTIFICADA";
                MensajeR.Visible = true;
                cmd.Connection.Close();
            }
            else
            {
                cmd.Connection.Close();
                MontoSupGastosP = Convert.ToDecimal(reader.Rows[0][0]);
                MontoMaxAyudaP = Convert.ToDecimal(reader.Rows[0][1]);
                PorcenSuperP = Convert.ToDecimal(reader.Rows[0][2]);
                PorcenInferP = Convert.ToDecimal(reader.Rows[0][3]);
                MontoExterAyudaP = Convert.ToDecimal(reader.Rows[0][4]);
            }
            return;
        }
    }
    protected void BuscarSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        MensajeR.Text = "";
        if (NSolicitud.Text == "")
        {
            Cedula.Focus();
        }
        else
        { 
            LeeSolAyuda();
            DesplegaSolicitud();
            RecalculaFacturas();
            LeeDatosSocio();
            dg_ValorFacturas.Visible = true;
            VFactura.Focus();
            ImprimirFacturas.Visible = true;
        }
    }

    protected void LeeDatosSocio()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Select Identificacion, TipoSocio, "
                                + "Apellidos, Nombres, "
                                + "FechaAfil, Email, "
                                + "SituacionSocio, "
                                + "TipoIF, NombreIF, Codigo_BCE, "
                                + "TipoCuentaIF, NumCuenta, "
                                + "TelefonoConv, TelefonoCel, FechaDesafil "
                                + "From ASocios.APSocios  Where Identificacion = '"
                                + Cedula.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            DataTable reader = new DataTable();
            reader.Load(cmd.ExecuteReader());
            if (reader.Rows.Count == 0)
            {
                // do something if the query return no rows
                // you may insert the relevant redirection you asked for
                MensajeR.Text = "SOCIO NO REGISTRADO";
                MensajeR.Visible = true;
            }
            else
            {
                // Get the value of Col1 in the 3rd row (0 is the first row)
                // Col1Value = (int)dt.Rows[2]["Col1"];
                CodServicio = (string)reader.Rows[0][1];
                Servicio.Text = (string)reader.Rows[0][1]; // LeeServicio((string)reader.Rows[0][1]);
                Servicio.Visible = true;
                Nombres.Text = (string)reader.Rows[0][2] + " " + (string)reader.Rows[0][3];
                FechaAfiliacion.Text = (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][4]));
                Situacion.Text = LeeSituacion((string)reader.Rows[0][6]);

                if ((string)reader.Rows[0][6] == "D")
                {
                    Situacion.Text += (string.Format("{0:dd/MM/yyyy}", reader.Rows[0][14]));

                    OtroSocio.Focus();
                }
                else
                {
                    FechaSolicitud.Text = (string.Format("{0:dd/MM/yyyy}", DateTime.Now));

                    FechaSolicitud.Focus();
                }
            }
            cmd.Connection.Close();
        }
    }
    protected void btn_HabilitaProcesoFacturas_Click(object sender, ImageClickEventArgs e)
    {

        dg_ValorFacturas.Visible = true;
        EFactura.Visible = true;
        SecuenFacturas.Visible = true;
        EVFactura.Visible = true;
        VFactura.Visible = true;
        EObservFactura.Visible = true;
        ObservFactura.Visible = true;
        btn_AgregarFactura.Visible = true;
        btn_TerminarIngresoFacturas.Visible = true;
        btn_TerminarIngresoFacturas0.Visible = true;

        SecuenFacturas.Focus();
        RecalculaFacturas();
        ETFacturas.Visible = true;
        TGeneral.Visible = true;
        ETFacturas0.Visible = true;
        TFacturas.Visible = true;
    }
    protected void dg_ValorFacturas_SelectedIndexChanged(object sender, EventArgs e)
    {
        IndEliminacion = 1;
        int NF = dg_ValorFacturas.SelectedIndex;
        if ((string.IsNullOrEmpty(dg_ValorFacturas.Rows[NF].Cells[1].Text))
            || (dg_ValorFacturas.Rows[NF].Cells[1].Text == "&nbsp;"))
        {
            MensajeR.Text = NF + "Error Factura ";
        }
        else
        {
            MensajeR.Text = NF + "Factura Procesada correctamente ";
            SecuenFacturas.Text = dg_ValorFacturas.Rows[NF].Cells[1].Text;
            ObservFactura.Text = dg_ValorFacturas.Rows[NF].Cells[2].Text;

            VFactura.Text = dg_ValorFacturas.Rows[NF].Cells[3].Text;
            VProforma.Text = dg_ValorFacturas.Rows[NF].Cells[4].Text;
            VDonacion.Text = dg_ValorFacturas.Rows[NF].Cells[5].Text;


            VFactura.Text = VFactura.Text.Replace(".", ",");
            VProforma.Text = VProforma.Text.Replace(".", ",");
            VDonacion.Text = VDonacion.Text.Replace(".", ",");

            VFacturaD = System.Convert.ToDecimal(VFactura.Text);
            VProformaD = System.Convert.ToDecimal(VProforma.Text);
            VDonacionD = System.Convert.ToDecimal(VDonacion.Text);

        }

        btn_EliminarFactura.Visible = true;

        SecuenFacturas.Focus();
        VFacturaAEliminar.Text = (string.Format("{0:###0.00}", (VFacturaD + VProformaD+VDonacionD)));
  //      VFactura.Text = "";
        VFacturaAEliminar.Visible = true;
    }
    protected void ProcesaAgregarFactura()
    {
        if (VFactura.Text=="")
        {
            VFactura.Text = "0";
        }
        if (VProforma.Text == "")
        {
            VProforma.Text = "0";
        }
        if (VDonacion.Text == "")
        {
            VDonacion.Text = "0";
        }

        VFactura.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        VProforma.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        VDonacion.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        VFactura.Text = VFactura.Text.Replace(".", ",");
        VProforma.Text = VProforma.Text.Replace(".", ",");
        VDonacion.Text = VDonacion.Text.Replace(".", ",");

        VFacturaD = System.Convert.ToDecimal(VFactura.Text);
        VProformaD = System.Convert.ToDecimal(VProforma.Text);
        VDonacionD = System.Convert.ToDecimal(VDonacion.Text);


        VFactura.Text = (string.Format("{0:###0.00}", Convert.ToDecimal(VFactura.Text)));
        VFactura.Text = VFactura.Text.Replace(",", ".");

        VProforma.Text = (string.Format("{0:###0.00}", Convert.ToDecimal(VProforma.Text)));
        VProforma.Text = VProforma.Text.Replace(",", ".");

        VDonacion.Text = (string.Format("{0:###0.00}", Convert.ToDecimal(VDonacion.Text)));
        VDonacion.Text = VDonacion.Text.Replace(",", ".");


        if (
                (VFactura.Text == "" || String.IsNullOrEmpty(VFactura.Text) || (VFacturaD <= 0)) &&
                (VProforma.Text == "" || String.IsNullOrEmpty(VProforma.Text) || (VProformaD <= 0)) &&
                (VDonacion.Text == "" || String.IsNullOrEmpty(VDonacion.Text) || (VDonacionD <= 0)) 
           )
        {
            MensajeR.Text = "Valores Errados " + VFactura.Text + " " + VProforma.Text + " " + VDonacion.Text;
            SecuenFacturas.Text = "";
            VFactura.Text = "";
            VProforma.Text = "";
            VDonacion.Text = "";
            ObservFactura.Text = "OK";
            VFactura.Focus();
        }
        else
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                string sql = "Insert INTO ASocios.APSociosAyudasFacturas "
                        + "(NumSolicitud, Secuencial, "
                        + "Valorfactura, ValorProforma, ValorDonacion, ObservFactura, "
                        + "UserAlta, FechaCompAlta) "
                        + "VALUES (" + NSolicitud.Text + ", " + SecuenFacturas.Text + ", "
                        + VFactura.Text + ", " + VProforma.Text + ", " + VDonacion.Text + ", '" + ObservFactura.Text + "', "
                        + "'" + (string)(Session["CodUsuarioM"]) + "', '" + DateTime.Now + "')";
                //             MensajeR.Text = NDocumento.Text + " " + sql;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MensajeR.Text = sql + " " + ex.ToString();
                }
                cmd.Connection.Close();
                MensajeR.Text = "Factura Registrada Correctamente";
                RecalculaFacturas();
 //               Monto.Text = TGeneral.Text;
                CalculaValores();
                GrabaMontoGastos();
 //               SecuenFacturas.Text = "";
                VFactura.Text = "";
                VProforma.Text = "";
                VDonacion.Text = "";
                ObservFactura.Text = "OK";
                VFactura.Focus();
            }
        }
    }
    protected void btn_AgregarFactura_Click(object sender, ImageClickEventArgs e)
    {
        ProcesaAgregarFactura();
    }

    protected void btn_EliminarFactura_Click(object sender, ImageClickEventArgs e)
    {
        IndEliminacion = 1;
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "Delete FROM ASocios.APSociosAyudasFacturas "
                    + "WHERE NumSolicitud=" + NSolicitud.Text
                    + " AND Secuencial=" + SecuenFacturas.Text;

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = sql + " " + ex.ToString();
            }
            cmd.Connection.Close();
            SecuenFacturas.Text = "";
            VFactura.Text = "";
            VProforma.Text = "";
            VDonacion.Text = "";
            ObservFactura.Text = "OK";
            RecalculaFacturas();
            CalculaValores();
            GrabaMontoGastos();
            VFactura.Focus();
            IndEliminacion = 0;
            VFacturaAEliminar.Visible = false;
            btn_EliminarFactura.Visible = false;
        }
    }

    protected void CalculaValores()
    {
        Monto.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        Porcentaje.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        MCalculado.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        TGeneral.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        VSolicitado.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        PorcentajeD = 0;
        PPorcentaje = 0;
       
        Monto.Text = Monto.Text.Replace(".", ",");
        Proformas.Text = Proformas.Text.Replace(".", ",");
        Donacion.Text = Donacion.Text.Replace(".", ",");
        TGeneral.Text = TGeneral.Text.Replace(".", ",");
        if (Monto.Text!="")
        {
            MontoD = System.Convert.ToDecimal(Monto.Text);
        }
        else
        {
            MontoD = 0;
        }

        if (Proformas.Text != "")
        {
            ProformasD = System.Convert.ToDecimal(Proformas.Text);
        }
        else
        {
            ProformasD = 0;
        }

        if (Donacion.Text != "")
        {
            DonacionD = System.Convert.ToDecimal(Donacion.Text);
        }
        else
        {
            DonacionD = 0;
        }

        TGeneralD = System.Convert.ToDecimal(TGeneral.Text);

        if (MontoD <= 0 && ProformasD <= 0 && DonacionD <= 0)
        {
            MensajeR.Text = "Monto de F/P/D no puede ser 0";
            VFactura.Focus();
        }
        else
        {
            if (MontoD <= MontoSupGastosP)
            {
                PorcentajeD = PorcenSuperP;
            }
            else
            {
                PorcentajeD = PorcenInferP;
            }

            if (ProformasD <= MontoSupGastosP)
            {
                PPorcentaje = PorcenSuperP;
            }
            else
            {
                PPorcentaje = PorcenInferP;
            }
            MCalculadoPD = ProformasD * PPorcentaje / 100;
            MCalculadoD = MontoD * PorcentajeD / 100;

            if (MCalculadoD > MontoMaxAyudaP)
            {
                if (TipoAyuda.Text == "AX")
                {
                    if (MCalculadoD > MontoMaxAyudaP * 2)
                    {
                        MCalculadoD = MontoMaxAyudaP * 2;
                    }
                }
                else
                {
                    MCalculadoD = MontoMaxAyudaP;
                }
                PorcentajeD = 100;
            }

            if (MontoD <=0 )
            {
                PorcentajeD = 0;
            }
            if (ProformasD<=0)
            {
                PPorcentaje = 0;
            }

            VSolicitadoD = MCalculadoD + MCalculadoPD + DonacionD;

            if (VSolicitadoD > MontoMaxAyudaP)
            {
                VSolicitadoD = MontoMaxAyudaP;
                MCalculadoPD = VSolicitadoD - MCalculadoD;
            }

            VSolicitadoD = MCalculadoD + MCalculadoPD + DonacionD;

            if (VSolicitadoD > MontoMaxAyudaP)
            {
                VSolicitadoD = MontoMaxAyudaP;
                DonacionD = VSolicitadoD - MCalculadoD - MCalculadoPD;
            }

            VSolicitadoD = MCalculadoD + MCalculadoPD + DonacionD;


            Monto.Text = MontoD.ToString("0.00", CultureInfo.InvariantCulture);
            Porcentaje.Text = PorcentajeD.ToString("0", CultureInfo.InvariantCulture);
            MCalculado.Text = MCalculadoD.ToString("0.00", CultureInfo.InvariantCulture);

            Proformas.Text = ProformasD.ToString("0.00", CultureInfo.InvariantCulture);
            PorcentajeP.Text = PPorcentaje.ToString("0", CultureInfo.InvariantCulture);
            MCalculadoP.Text=MCalculadoPD.ToString("0.00", CultureInfo.InvariantCulture);

            Donacion.Text = DonacionD.ToString("0.00", CultureInfo.InvariantCulture);
            TGeneral.Text = TGeneralD.ToString("0.00", CultureInfo.InvariantCulture);

            VSolicitado.Text = VSolicitadoD.ToString("0.00", CultureInfo.InvariantCulture); ;

            EPorcentaje.Visible = true;
            Porcentaje.Visible = true;
            MCalculado.Visible = true;
            PorcentajeP.Visible = true;
            MCalculadoP.Visible = true;

        }

    }
    protected void btn_TerminarIngresoFacturas_Click(object sender, ImageClickEventArgs e)
    {
        Monto.Text = TFacturas.Text;
        Proformas.Text = TProformas.Text;
        Donacion.Text = TDonaciones.Text;
        CalculaValores();
        GrabaMontoGastos();
        OtroSocio.Focus();
    }
    private void GrabaMontoGastos()
    {
        using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
        {
            string sql = "";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Connection.Open();
            try
            {
                sql = "UPDATE ASocios.APSociosAyudas "
                        + "SET MontoDeGastos=" + TGeneral.Text
                        + ", MontoFacturas=" + Monto.Text
                        + ", PorcentajeAyuda=" + Porcentaje.Text
                        + ", AyudaFacturas=" + MCalculado.Text
                        + ", MontoProformas=" + Proformas.Text
                        + ", PorcentajeProformas=" + PorcentajeP.Text
                        + ", AyudaProformas=" + MCalculadoP.Text
                        + ", MontoDonaciones=" + Donacion.Text
                        + ", ValorSolicitado=" + VSolicitado.Text
                        + " Where NumSolicitud = '" + NSolicitud.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensajeR.Text = ex.ToString();
            }
            cmd.Connection.Close();
        }
    }
    private void RecalculaFacturas()
    {
        int UF=-1;
        TGeneral.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        TFacturas.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        TProformas.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        TDonaciones.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        dg_ValorFacturas.Dispose();
        dg_ValorFacturas.DataSourceID = "";
        dg_ValorFacturas.DataSourceID = (string)(TabValorFacturas.UniqueID);
        dg_ValorFacturas.DataBind();

        decimal suma = 0, sumaF=0, sumaP=0, sumaD=0;
        for (int X = 0; X < dg_ValorFacturas.Rows.Count; X++)
        {
            sumaF += decimal.Parse(dg_ValorFacturas.Rows[X].Cells[3].Text);
            sumaP += decimal.Parse(dg_ValorFacturas.Rows[X].Cells[4].Text);
            sumaD += decimal.Parse(dg_ValorFacturas.Rows[X].Cells[5].Text);
            UF = X;
        }
        suma = sumaF + sumaP + sumaD;
        if (UF>=0)
        {
            SecuenFacturas.Text = (string.Format("{0:##0}", (int.Parse(dg_ValorFacturas.Rows[UF].Cells[1].Text) + 10)));
        }
        else
        {
            SecuenFacturas.Text = "10";
        }
        TGeneral.Text = (string.Format("{0:##0.00}", suma));
        TFacturas.Text = (string.Format("{0:##0.00}", sumaF));
        TProformas.Text = (string.Format("{0:##0.00}", sumaP));
        TDonaciones.Text = (string.Format("{0:##0.00}", sumaD));
        Donacion.Text = TDonaciones.Text;
        Monto.Text = TFacturas.Text;
        Proformas.Text = TProformas.Text;
    }

    protected void ImprimirFacturas_Click(object sender, EventArgs e)
    {
        Session["NumSolicitud"] = NSolicitud.Text;
        Response.Write("<script>window.open('VisorAyudaFacturas.aspx');</script>");
    }


}
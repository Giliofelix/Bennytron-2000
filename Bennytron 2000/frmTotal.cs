using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bennytron_2000
{
    public partial class frmTotal : Form
    {
        Nucleo _nucleo;
        Calculo _calculo;

        public frmTotal(Nucleo nucleo, Calculo calculo)
        {
            _nucleo = nucleo;
            _calculo = calculo;

            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTotal_Load(object sender, EventArgs e)
        {
            Decimal precioDolar = decimal.Parse(_nucleo.Parametro("Precio_dólar"));

            DataTable dtTotales = new DataTable("Totales");

            dtTotales.Columns.Add("Concepto", System.Type.GetType("System.String"));
            dtTotales.Columns.Add("Dólares", System.Type.GetType("System.String"));
            dtTotales.Columns.Add("Pesos", System.Type.GetType("System.String"));

            DataRow drEt2 = dtTotales.NewRow();
            drEt2[0] = "Importe Tablero: ";
            drEt2[1] = "$ " + _calculo.ImporteTablero.ToString("N"); 
            drEt2[2] = "$ " + (_calculo.ImporteTablero * precioDolar).ToString("N");
            dtTotales.Rows.Add(drEt2);

            DataRow drProt = dtTotales.NewRow();
            drProt[0] = "Importe Proteciones ITM:";
            drProt[1] = "$ " + _calculo.ImporteProtecionesITM.ToString("N");
            drProt[2] = "$ " + (_calculo.ImporteProtecionesITM * precioDolar).ToString("N");
            dtTotales.Rows.Add(drProt);

            DataRow drMod = dtTotales.NewRow();
            drMod[0] = "Costo Modulos:";
            drMod[1] = "$ " + _calculo.CostoModulos.ToString("N");
            drMod[2] = "$ " + (_calculo.CostoModulos * precioDolar).ToString("N");
            dtTotales.Rows.Add(drMod);

            DataRow drMicro = dtTotales.NewRow();
            drMicro[0] = "Costo Microinversores:";
            drMicro[1] = "$ " + _calculo.CostoMicroinversores.ToString("N");
            drMicro[2] = "$ " + (_calculo.CostoMicroinversores * precioDolar).ToString("N");
            dtTotales.Rows.Add(drMicro);

            DataRow drInver = dtTotales.NewRow();
            drInver[0] = "Costo Inversores:";
            drInver[1] = "$ 0.00"; // "$ " + _calculo..ToString("N");
            drInver[2] = "$ 0.00"; // + (_calculo. * precioDolar).ToString("N");
            dtTotales.Rows.Add(drInver);

            DataRow drElec = dtTotales.NewRow();
            drElec[0] = "Costo Electrico:";
            drElec[1] = "$ " + _calculo.CostoElectrico.ToString("N");
            drElec[2] = "$ " + (_calculo.CostoElectrico * precioDolar).ToString("N");
            dtTotales.Rows.Add(drElec);

            DataRow drEstruc = dtTotales.NewRow();
            drEstruc[0] = "Costo Estructural:";
            drEstruc[1] = "$ 0.00"; // "$ " + _calculo.CostoElectrico.ToString("N");
            drEstruc[2] = "$ 0.00"; // + (_calculo.CostoElectrico * precioDolar).ToString("N");
            dtTotales.Rows.Add(drEstruc);

            DataRow drFerr = dtTotales.NewRow();
            drFerr[0] = "Costo Ferretero:";
            drFerr[1] = "$ 0.00"; // "$ " + _calculo..ToString("N");
            drFerr[2] = "$ 0.00"; // + (_calculo. * precioDolar).ToString("N");
            dtTotales.Rows.Add(drFerr);

             // TODO: sumar costo estructural y ferretero
            DataRow drTot = dtTotales.NewRow();
            drTot[0] = "Total:";
            drTot[1] = "$ " + _calculo.CostoElectrico.ToString("N");
            drTot[2] = "$ " + (_calculo.CostoElectrico * precioDolar).ToString("N");
            dtTotales.Rows.Add(drTot);

            dgvTotal.DataSource = dtTotales;
            dgvTotal.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTotal.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTotal.AllowUserToAddRows = false;
            dgvTotal.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Precio dólar capturado: $ " + _nucleo.Parametro("Precio_dólar").ToString() + " MXN.");
        }
    }
}

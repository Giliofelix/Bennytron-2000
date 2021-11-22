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
    public partial class frmBusqueda : Form
    {
        Nucleo _nucleo;
        CriterioBusqueda[] _criterios;
        string _tabla;
        string _retornar;
        DataTable dt;
        public string Resultado;

        public frmBusqueda(Nucleo nucleo, CriterioBusqueda[] criterios, string tabla, string retornar)
        {
            InitializeComponent();
            _nucleo = nucleo;
            _tabla = tabla;
            _criterios = new CriterioBusqueda[criterios.Length];
            criterios.CopyTo(_criterios, 0);
            _retornar = retornar;
            dt = null;
        }

        private void frmBusqueda_Load(object sender, EventArgs e)
        {
            this.cmbCampos.Items.Clear();

            foreach (CriterioBusqueda criterio in _criterios)
            {
                if (criterio.Tipo != "decimal")
                {
                    this.cmbCampos.Items.Add(criterio.Mostrar);
                }
            }

            if (cmbCampos.Items.Count > 0)
                this.cmbCampos.SelectedIndex = 0;

            if (this.txtBuscar.Text != "")
                btnBuscar_Click(sender, e);

            this.txtBuscar.Focus();
        }

        #region Métodos

        private string ParteSelect()
        {
            string select = "select " + _retornar + " ";


            foreach (CriterioBusqueda criterio in _criterios)
            {
                select += ", " + criterio.Campo + " as [" + criterio.Mostrar + "]";
            }

            return select;
        }

        private string ParteFrom()
        {
            return "  from " + _tabla;
        }
        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string consulta = this.ParteSelect();

            consulta += this.ParteFrom();

            if (this.cmbCampos.SelectedIndex >= 0)
            {
                switch (_criterios[this.cmbCampos.SelectedIndex].Tipo)
                {
                    case "int":
                        consulta += " where " + _retornar + " like '" + this.txtBuscar.Text + "'"
                            + " OR " + _criterios[this.cmbCampos.SelectedIndex].Campo;
                        consulta += " = " + this.txtBuscar.Text;
                        break;
                    case "string":
                        consulta += " where " + _retornar + " like '" + this.txtBuscar.Text + "'"
                            + " OR " + _criterios[this.cmbCampos.SelectedIndex].Campo;
                        consulta += " like '%" + this.txtBuscar.Text + "%'";
                        break;
                    case "decimal":
                        break;
                }
            }

            dt = _nucleo.Obtener(consulta);

            this.dtgResultado.DataSource = dt;
        }

        private void dtgResultado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt != null)
            {
                //retornar clave primaria del registro selecicionado.
                this.Resultado = dtgResultado.CurrentRow.Cells[_retornar].Value.ToString();
            }
            this.Close();
        }
    }
}

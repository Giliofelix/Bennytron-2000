
namespace Bennytron_2000
{
    partial class frmEstructura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbTechoExistente = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbAnclaje = new System.Windows.Forms.ComboBox();
            this.cmbDetalleAnclaje = new System.Windows.Forms.ComboBox();
            this.lblMaterialEstructural = new System.Windows.Forms.Label();
            this.dgvMaterialEstructural = new System.Windows.Forms.DataGridView();
            this.txtSegmentosPanel = new System.Windows.Forms.TextBox();
            this.lblMaterialFerretero = new System.Windows.Forms.Label();
            this.dgvMaterialFerretero = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialEstructural)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialFerretero)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTechoExistente
            // 
            this.cmbTechoExistente.FormattingEnabled = true;
            this.cmbTechoExistente.Items.AddRange(new object[] {
            "Lámina trapezoidal",
            "Lámina Engargolada",
            "Concreto",
            "Teja"});
            this.cmbTechoExistente.Location = new System.Drawing.Point(12, 79);
            this.cmbTechoExistente.Name = "cmbTechoExistente";
            this.cmbTechoExistente.Size = new System.Drawing.Size(262, 21);
            this.cmbTechoExistente.TabIndex = 4;
            this.cmbTechoExistente.Text = "Elija un opción...";
            this.cmbTechoExistente.SelectedIndexChanged += new System.EventHandler(this.cmbTechoExistente_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(262, 23);
            this.label14.TabIndex = 3;
            this.label14.Text = "Techo existente";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Información sobre la instalación existente";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Segmentos (Panel)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Anclaje";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(262, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Detalles del anclaje";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(262, 23);
            this.label5.TabIndex = 3;
            this.label5.Text = "Detalles de la nueva instalación";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbAnclaje
            // 
            this.cmbAnclaje.FormattingEnabled = true;
            this.cmbAnclaje.Items.AddRange(new object[] {
            "Microinversor",
            "Inversor"});
            this.cmbAnclaje.Location = new System.Drawing.Point(12, 243);
            this.cmbAnclaje.Name = "cmbAnclaje";
            this.cmbAnclaje.Size = new System.Drawing.Size(262, 21);
            this.cmbAnclaje.TabIndex = 4;
            this.cmbAnclaje.Text = "Mini riel";
            this.cmbAnclaje.SelectedIndexChanged += new System.EventHandler(this.cmbAnclaje_SelectedIndexChanged);
            // 
            // cmbDetalleAnclaje
            // 
            this.cmbDetalleAnclaje.FormattingEnabled = true;
            this.cmbDetalleAnclaje.Items.AddRange(new object[] {
            "Microinversor",
            "Inversor"});
            this.cmbDetalleAnclaje.Location = new System.Drawing.Point(12, 293);
            this.cmbDetalleAnclaje.Name = "cmbDetalleAnclaje";
            this.cmbDetalleAnclaje.Size = new System.Drawing.Size(262, 21);
            this.cmbDetalleAnclaje.TabIndex = 4;
            this.cmbDetalleAnclaje.Text = "Lámina trapezoidal";
            this.cmbDetalleAnclaje.SelectedIndexChanged += new System.EventHandler(this.cmbDetalleAnclaje_SelectedIndexChanged);
            // 
            // lblMaterialEstructural
            // 
            this.lblMaterialEstructural.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblMaterialEstructural.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMaterialEstructural.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterialEstructural.Location = new System.Drawing.Point(280, 9);
            this.lblMaterialEstructural.Name = "lblMaterialEstructural";
            this.lblMaterialEstructural.Size = new System.Drawing.Size(262, 41);
            this.lblMaterialEstructural.TabIndex = 3;
            this.lblMaterialEstructural.Text = "Material estructural para Mini riel con Lámina trapezoidal";
            this.lblMaterialEstructural.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvMaterialEstructural
            // 
            this.dgvMaterialEstructural.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialEstructural.Location = new System.Drawing.Point(280, 53);
            this.dgvMaterialEstructural.Name = "dgvMaterialEstructural";
            this.dgvMaterialEstructural.Size = new System.Drawing.Size(262, 261);
            this.dgvMaterialEstructural.TabIndex = 7;
            // 
            // txtSegmentosPanel
            // 
            this.txtSegmentosPanel.Location = new System.Drawing.Point(12, 129);
            this.txtSegmentosPanel.Name = "txtSegmentosPanel";
            this.txtSegmentosPanel.Size = new System.Drawing.Size(262, 20);
            this.txtSegmentosPanel.TabIndex = 8;
            this.txtSegmentosPanel.Text = "1";
            this.txtSegmentosPanel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSegmentosPanel.TextChanged += new System.EventHandler(this.txtSegmentosPanel_TextChanged);
            // 
            // lblMaterialFerretero
            // 
            this.lblMaterialFerretero.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblMaterialFerretero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMaterialFerretero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterialFerretero.Location = new System.Drawing.Point(548, 9);
            this.lblMaterialFerretero.Name = "lblMaterialFerretero";
            this.lblMaterialFerretero.Size = new System.Drawing.Size(262, 41);
            this.lblMaterialFerretero.TabIndex = 3;
            this.lblMaterialFerretero.Text = "Material ferretero para Mini riel con Lámina trapezoidal";
            this.lblMaterialFerretero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvMaterialFerretero
            // 
            this.dgvMaterialFerretero.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialFerretero.Location = new System.Drawing.Point(548, 53);
            this.dgvMaterialFerretero.Name = "dgvMaterialFerretero";
            this.dgvMaterialFerretero.Size = new System.Drawing.Size(262, 261);
            this.dgvMaterialFerretero.TabIndex = 7;
            // 
            // frmEstructura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 331);
            this.Controls.Add(this.txtSegmentosPanel);
            this.Controls.Add(this.dgvMaterialFerretero);
            this.Controls.Add(this.dgvMaterialEstructural);
            this.Controls.Add(this.cmbDetalleAnclaje);
            this.Controls.Add(this.cmbAnclaje);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTechoExistente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMaterialFerretero);
            this.Controls.Add(this.lblMaterialEstructural);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label14);
            this.Name = "frmEstructura";
            this.Text = "Estructural y Ferretero";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEstructura_FormClosing);
            this.Load += new System.EventHandler(this.frmEstructura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialEstructural)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialFerretero)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTechoExistente;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbAnclaje;
        private System.Windows.Forms.ComboBox cmbDetalleAnclaje;
        private System.Windows.Forms.Label lblMaterialEstructural;
        private System.Windows.Forms.DataGridView dgvMaterialEstructural;
        private System.Windows.Forms.TextBox txtSegmentosPanel;
        private System.Windows.Forms.Label lblMaterialFerretero;
        private System.Windows.Forms.DataGridView dgvMaterialFerretero;
    }
}
namespace SAMM
{
    partial class Inicio
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            txtMessage = new RichTextBox();
            btnDecode = new Button();
            btnSearch = new Button();
            dgvMsg = new DataGridView();
            cboType = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvMsg).BeginInit();
            SuspendLayout();
            // 
            // txtMessage
            // 
            txtMessage.BackColor = Color.Teal;
            txtMessage.BorderStyle = BorderStyle.None;
            txtMessage.ForeColor = Color.SpringGreen;
            txtMessage.Location = new Point(15, 13);
            txtMessage.Margin = new Padding(4, 3, 4, 3);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(355, 357);
            txtMessage.TabIndex = 0;
            txtMessage.Text = "";
            txtMessage.TextChanged += TxtMessage_TextChanged;
            txtMessage.Enter += TxtMessage_Enter;
            txtMessage.Leave += TxtMessage_Leave;
            // 
            // btnDecode
            // 
            btnDecode.BackColor = Color.DarkCyan;
            btnDecode.FlatAppearance.BorderColor = Color.LawnGreen;
            btnDecode.FlatStyle = FlatStyle.Flat;
            btnDecode.ForeColor = Color.MediumSpringGreen;
            btnDecode.Location = new Point(15, 376);
            btnDecode.Margin = new Padding(4, 3, 4, 3);
            btnDecode.Name = "btnDecode";
            btnDecode.Size = new Size(356, 30);
            btnDecode.TabIndex = 1;
            btnDecode.Text = "Traducir";
            btnDecode.UseVisualStyleBackColor = false;
            btnDecode.Click += btnDecode_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.DarkCyan;
            btnSearch.FlatAppearance.BorderColor = Color.FloralWhite;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.FloralWhite;
            btnSearch.Location = new Point(378, 376);
            btnSearch.Margin = new Padding(4, 3, 4, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(508, 30);
            btnSearch.TabIndex = 15;
            btnSearch.Text = "Buscar datos";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvMsg
            // 
            dgvMsg.AllowUserToAddRows = false;
            dgvMsg.AllowUserToDeleteRows = false;
            dgvMsg.AllowUserToOrderColumns = true;
            dgvMsg.AllowUserToResizeColumns = false;
            dgvMsg.AllowUserToResizeRows = false;
            dgvMsg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMsg.BackgroundColor = Color.DarkCyan;
            dgvMsg.BorderStyle = BorderStyle.None;
            dgvMsg.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvMsg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvMsg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMsg.ColumnHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.Teal;
            dataGridViewCellStyle4.Font = new Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = Color.SpringGreen;
            dataGridViewCellStyle4.SelectionBackColor = Color.Teal;
            dataGridViewCellStyle4.SelectionForeColor = Color.SpringGreen;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvMsg.DefaultCellStyle = dataGridViewCellStyle4;
            dgvMsg.Enabled = false;
            dgvMsg.GridColor = Color.DarkSeaGreen;
            dgvMsg.Location = new Point(378, 12);
            dgvMsg.Name = "dgvMsg";
            dgvMsg.ReadOnly = true;
            dgvMsg.RowHeadersVisible = false;
            dgvMsg.RowHeadersWidth = 51;
            dgvMsg.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvMsg.Size = new Size(509, 358);
            dgvMsg.TabIndex = 16;
            // 
            // cboType
            // 
            cboType.BackColor = Color.SlateGray;
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.ForeColor = Color.SpringGreen;
            cboType.FormattingEnabled = true;
            cboType.Items.AddRange(new object[] { "Todos", "Direccion de viento", "Velocidad de viento", "Temperatura Balistica", "Densidad Balistica" });
            cboType.Location = new Point(15, 412);
            cboType.Name = "cboType";
            cboType.Size = new Size(872, 30);
            cboType.SelectedIndex = 0;
            // 
            // Inicio
            // 
            AutoScaleDimensions = new SizeF(10F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(899, 452);
            Controls.Add(cboType);
            Controls.Add(dgvMsg);
            Controls.Add(btnSearch);
            Controls.Add(btnDecode);
            Controls.Add(txtMessage);
            Font = new Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Inicio";
            Text = "Inicio";
            ((System.ComponentModel.ISupportInitialize)dgvMsg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox txtMessage;
        private Button btnDecode;
        private Button btnSearch;
        private DataGridView dgvMsg;
        private ComboBox cboType;
    }
}

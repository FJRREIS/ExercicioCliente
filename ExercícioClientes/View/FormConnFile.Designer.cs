namespace ExercícioClientes.View
{
    partial class FormConnFile
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblInfoText = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.lboxFiles = new System.Windows.Forms.ListBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.ToolTip_btnFolder = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip_btnConectar = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblInfoText
            // 
            this.lblInfoText.AutoSize = true;
            this.lblInfoText.Location = new System.Drawing.Point(27, 28);
            this.lblInfoText.Name = "lblInfoText";
            this.lblInfoText.Size = new System.Drawing.Size(322, 13);
            this.lblInfoText.TabIndex = 0;
            this.lblInfoText.Text = "Escolha um arquivo para iniciar a conexão com o banco de dados:";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(65, 80);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(37, 13);
            this.lblFolder.TabIndex = 1;
            this.lblFolder.Text = "Pasta:";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(254, 77);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(24, 19);
            this.btnFolder.TabIndex = 2;
            this.btnFolder.Text = "...";
            this.ToolTip_btnFolder.SetToolTip(this.btnFolder, "Selecionar pasta");
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnConectar.Location = new System.Drawing.Point(108, 218);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(140, 44);
            this.btnConectar.TabIndex = 3;
            this.btnConectar.Text = "Conectar";
            this.ToolTip_btnConectar.SetToolTip(this.btnConectar, "Conecta ao banco de dados");
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // lboxFiles
            // 
            this.lboxFiles.FormattingEnabled = true;
            this.lboxFiles.Location = new System.Drawing.Point(108, 104);
            this.lboxFiles.Name = "lboxFiles";
            this.lboxFiles.Size = new System.Drawing.Size(140, 108);
            this.lboxFiles.TabIndex = 4;
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(108, 77);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(140, 20);
            this.txtFolder.TabIndex = 6;
            this.txtFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FormConnFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 303);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.lboxFiles);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.lblInfoText);
            this.MaximumSize = new System.Drawing.Size(377, 341);
            this.MinimumSize = new System.Drawing.Size(377, 341);
            this.Name = "FormConnFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Clientes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfoText;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.ListBox lboxFiles;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.ToolTip ToolTip_btnFolder;
        private System.Windows.Forms.ToolTip ToolTip_btnConectar;
    }
}


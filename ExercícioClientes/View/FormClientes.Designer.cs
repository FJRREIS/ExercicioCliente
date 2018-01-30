namespace ExercícioClientes.View
{
    partial class FormClientes
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.conexãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDesconectar = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPesquisar = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInserir = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemExcluir = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblDataNasc = new System.Windows.Forms.Label();
            this.lblSobrenome = new System.Windows.Forms.Label();
            this.lblCEP = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.lblComplemento = new System.Windows.Forms.Label();
            this.lblLogradouro = new System.Windows.Forms.Label();
            this.lblSexo = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblBairro = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.txtSobrenome = new System.Windows.Forms.TextBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.txtLogradouro = new System.Windows.Forms.TextBox();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.cboxSexo = new System.Windows.Forms.ComboBox();
            this.cboxEstado = new System.Windows.Forms.ComboBox();
            this.btnOperacao = new System.Windows.Forms.Button();
            this.gboxCliente = new System.Windows.Forms.GroupBox();
            this.lblIndicePesquisa = new System.Windows.Forms.Label();
            this.txtDataNasc = new System.Windows.Forms.MaskedTextBox();
            this.txtCEP = new System.Windows.Forms.MaskedTextBox();
            this.lblExcluído = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.gboxEndereço = new System.Windows.Forms.GroupBox();
            this.btnProximo = new System.Windows.Forms.Button();
            this.btnUltimo = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnPrimeiro = new System.Windows.Forms.Button();
            this.ToolTip_btnProximo = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip_btnUltimo = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip_btnAnterior = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip_btnPrimeiro = new System.Windows.Forms.ToolTip(this.components);
            this.informaçãoDaConexãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.gboxCliente.SuspendLayout();
            this.gboxEndereço.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conexãoToolStripMenuItem,
            this.editarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(777, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // conexãoToolStripMenuItem
            // 
            this.conexãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemDesconectar,
            this.informaçãoDaConexãoToolStripMenuItem});
            this.conexãoToolStripMenuItem.Name = "conexãoToolStripMenuItem";
            this.conexãoToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.conexãoToolStripMenuItem.Text = "Conexão";
            // 
            // MenuItemDesconectar
            // 
            this.MenuItemDesconectar.Name = "MenuItemDesconectar";
            this.MenuItemDesconectar.Size = new System.Drawing.Size(139, 22);
            this.MenuItemDesconectar.Text = "Desconectar";
            this.MenuItemDesconectar.Click += new System.EventHandler(this.MenuItemDesconectar_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemPesquisar,
            this.MenuItemInserir,
            this.MenuItemExcluir});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // MenuItemPesquisar
            // 
            this.MenuItemPesquisar.Name = "MenuItemPesquisar";
            this.MenuItemPesquisar.Size = new System.Drawing.Size(124, 22);
            this.MenuItemPesquisar.Text = "Pesquisar";
            this.MenuItemPesquisar.Click += new System.EventHandler(this.MenuItemPesquisar_Click);
            // 
            // MenuItemInserir
            // 
            this.MenuItemInserir.Name = "MenuItemInserir";
            this.MenuItemInserir.Size = new System.Drawing.Size(124, 22);
            this.MenuItemInserir.Text = "Inserir";
            this.MenuItemInserir.Click += new System.EventHandler(this.MenuItemInserir_Click);
            // 
            // MenuItemExcluir
            // 
            this.MenuItemExcluir.Name = "MenuItemExcluir";
            this.MenuItemExcluir.Size = new System.Drawing.Size(124, 22);
            this.MenuItemExcluir.Text = "Excluir";
            this.MenuItemExcluir.Click += new System.EventHandler(this.MenuItemExcluir_Click);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(40, 35);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(38, 13);
            this.lblNome.TabIndex = 7;
            this.lblNome.Text = "Nome:";
            // 
            // lblDataNasc
            // 
            this.lblDataNasc.AutoSize = true;
            this.lblDataNasc.Location = new System.Drawing.Point(40, 108);
            this.lblDataNasc.Name = "lblDataNasc";
            this.lblDataNasc.Size = new System.Drawing.Size(105, 13);
            this.lblDataNasc.TabIndex = 9;
            this.lblDataNasc.Text = "Data de nascimento:";
            // 
            // lblSobrenome
            // 
            this.lblSobrenome.AutoSize = true;
            this.lblSobrenome.Location = new System.Drawing.Point(40, 71);
            this.lblSobrenome.Name = "lblSobrenome";
            this.lblSobrenome.Size = new System.Drawing.Size(64, 13);
            this.lblSobrenome.TabIndex = 8;
            this.lblSobrenome.Text = "Sobrenome:";
            // 
            // lblCEP
            // 
            this.lblCEP.AutoSize = true;
            this.lblCEP.Location = new System.Drawing.Point(40, 184);
            this.lblCEP.Name = "lblCEP";
            this.lblCEP.Size = new System.Drawing.Size(31, 13);
            this.lblCEP.TabIndex = 11;
            this.lblCEP.Text = "CEP:";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(40, 219);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(47, 13);
            this.lblNumero.TabIndex = 12;
            this.lblNumero.Text = "Numero:";
            // 
            // lblComplemento
            // 
            this.lblComplemento.AutoSize = true;
            this.lblComplemento.Location = new System.Drawing.Point(40, 255);
            this.lblComplemento.Name = "lblComplemento";
            this.lblComplemento.Size = new System.Drawing.Size(74, 13);
            this.lblComplemento.TabIndex = 13;
            this.lblComplemento.Text = "Complemento:";
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.AutoSize = true;
            this.lblLogradouro.Location = new System.Drawing.Point(30, 35);
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.Size = new System.Drawing.Size(64, 13);
            this.lblLogradouro.TabIndex = 8;
            this.lblLogradouro.Text = "Logradouro:";
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Location = new System.Drawing.Point(40, 146);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(34, 13);
            this.lblSexo.TabIndex = 10;
            this.lblSexo.Text = "Sexo:";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(30, 156);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(43, 13);
            this.lblEstado.TabIndex = 10;
            this.lblEstado.Text = "Estado:";
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.Location = new System.Drawing.Point(30, 76);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(37, 13);
            this.lblBairro.TabIndex = 11;
            this.lblBairro.Text = "Bairro:";
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Location = new System.Drawing.Point(30, 114);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(43, 13);
            this.lblCidade.TabIndex = 12;
            this.lblCidade.Text = "Cidade:";
            // 
            // txtSobrenome
            // 
            this.txtSobrenome.Location = new System.Drawing.Point(169, 68);
            this.txtSobrenome.MaxLength = 200;
            this.txtSobrenome.Name = "txtSobrenome";
            this.txtSobrenome.Size = new System.Drawing.Size(158, 20);
            this.txtSobrenome.TabIndex = 1;
            this.txtSobrenome.TextChanged += new System.EventHandler(this.txtGeneric_TextChanged);
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(169, 217);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(158, 20);
            this.txtNumero.TabIndex = 5;
            this.txtNumero.TextChanged += new System.EventHandler(this.txtGeneric_TextChanged);
            // 
            // txtComplemento
            // 
            this.txtComplemento.Location = new System.Drawing.Point(169, 253);
            this.txtComplemento.MaxLength = 50;
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(158, 20);
            this.txtComplemento.TabIndex = 6;
            this.txtComplemento.TextChanged += new System.EventHandler(this.txtGeneric_TextChanged);
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.Enabled = false;
            this.txtLogradouro.Location = new System.Drawing.Point(119, 32);
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.Size = new System.Drawing.Size(158, 20);
            this.txtLogradouro.TabIndex = 18;
            // 
            // txtBairro
            // 
            this.txtBairro.Enabled = false;
            this.txtBairro.Location = new System.Drawing.Point(119, 74);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(158, 20);
            this.txtBairro.TabIndex = 19;
            // 
            // txtCidade
            // 
            this.txtCidade.Enabled = false;
            this.txtCidade.Location = new System.Drawing.Point(119, 111);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(158, 20);
            this.txtCidade.TabIndex = 20;
            // 
            // cboxSexo
            // 
            this.cboxSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSexo.FormattingEnabled = true;
            this.cboxSexo.Items.AddRange(new object[] {
            "",
            "Masculino",
            "Feminino"});
            this.cboxSexo.Location = new System.Drawing.Point(169, 143);
            this.cboxSexo.Name = "cboxSexo";
            this.cboxSexo.Size = new System.Drawing.Size(121, 21);
            this.cboxSexo.TabIndex = 3;
            this.cboxSexo.SelectedIndexChanged += new System.EventHandler(this.txtGeneric_TextChanged);
            // 
            // cboxEstado
            // 
            this.cboxEstado.Enabled = false;
            this.cboxEstado.FormattingEnabled = true;
            this.cboxEstado.Location = new System.Drawing.Point(119, 154);
            this.cboxEstado.Name = "cboxEstado";
            this.cboxEstado.Size = new System.Drawing.Size(121, 21);
            this.cboxEstado.TabIndex = 0;
            // 
            // btnOperacao
            // 
            this.btnOperacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnOperacao.Location = new System.Drawing.Point(434, 269);
            this.btnOperacao.Name = "btnOperacao";
            this.btnOperacao.Size = new System.Drawing.Size(270, 30);
            this.btnOperacao.TabIndex = 2;
            this.btnOperacao.Text = "Inserir";
            this.btnOperacao.UseVisualStyleBackColor = true;
            this.btnOperacao.Click += new System.EventHandler(this.btnOperacao_Click);
            // 
            // gboxCliente
            // 
            this.gboxCliente.Controls.Add(this.lblIndicePesquisa);
            this.gboxCliente.Controls.Add(this.txtDataNasc);
            this.gboxCliente.Controls.Add(this.txtCEP);
            this.gboxCliente.Controls.Add(this.lblExcluído);
            this.gboxCliente.Controls.Add(this.txtNome);
            this.gboxCliente.Controls.Add(this.lblNome);
            this.gboxCliente.Controls.Add(this.lblDataNasc);
            this.gboxCliente.Controls.Add(this.cboxSexo);
            this.gboxCliente.Controls.Add(this.lblSobrenome);
            this.gboxCliente.Controls.Add(this.lblCEP);
            this.gboxCliente.Controls.Add(this.lblNumero);
            this.gboxCliente.Controls.Add(this.lblComplemento);
            this.gboxCliente.Controls.Add(this.txtComplemento);
            this.gboxCliente.Controls.Add(this.lblSexo);
            this.gboxCliente.Controls.Add(this.txtNumero);
            this.gboxCliente.Controls.Add(this.txtSobrenome);
            this.gboxCliente.Location = new System.Drawing.Point(12, 37);
            this.gboxCliente.Name = "gboxCliente";
            this.gboxCliente.Size = new System.Drawing.Size(363, 323);
            this.gboxCliente.TabIndex = 1;
            this.gboxCliente.TabStop = false;
            this.gboxCliente.Text = "Cliente";
            // 
            // lblIndicePesquisa
            // 
            this.lblIndicePesquisa.AutoSize = true;
            this.lblIndicePesquisa.Location = new System.Drawing.Point(292, 294);
            this.lblIndicePesquisa.Name = "lblIndicePesquisa";
            this.lblIndicePesquisa.Size = new System.Drawing.Size(39, 13);
            this.lblIndicePesquisa.TabIndex = 15;
            this.lblIndicePesquisa.Text = "X de X";
            // 
            // txtDataNasc
            // 
            this.txtDataNasc.Location = new System.Drawing.Point(169, 105);
            this.txtDataNasc.Mask = "00/00/0000";
            this.txtDataNasc.Name = "txtDataNasc";
            this.txtDataNasc.Size = new System.Drawing.Size(70, 20);
            this.txtDataNasc.TabIndex = 2;
            this.txtDataNasc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDataNasc.ValidatingType = typeof(System.DateTime);
            this.txtDataNasc.TextChanged += new System.EventHandler(this.txtGeneric_TextChanged);
            // 
            // txtCEP
            // 
            this.txtCEP.Location = new System.Drawing.Point(169, 181);
            this.txtCEP.Mask = "00000-000";
            this.txtCEP.Name = "txtCEP";
            this.txtCEP.Size = new System.Drawing.Size(64, 20);
            this.txtCEP.TabIndex = 4;
            this.txtCEP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCEP.TextChanged += new System.EventHandler(this.txtGeneric_TextChanged);
            this.txtCEP.Leave += new System.EventHandler(this.txtCEP_Leave);
            // 
            // lblExcluído
            // 
            this.lblExcluído.AutoSize = true;
            this.lblExcluído.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblExcluído.ForeColor = System.Drawing.Color.Red;
            this.lblExcluído.Location = new System.Drawing.Point(116, 283);
            this.lblExcluído.Name = "lblExcluído";
            this.lblExcluído.Size = new System.Drawing.Size(125, 18);
            this.lblExcluído.TabIndex = 14;
            this.lblExcluído.Text = "Registro excluído!";
            this.lblExcluído.Visible = false;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(169, 32);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(158, 20);
            this.txtNome.TabIndex = 0;
            this.txtNome.TextChanged += new System.EventHandler(this.txtGeneric_TextChanged);
            // 
            // gboxEndereço
            // 
            this.gboxEndereço.Controls.Add(this.lblLogradouro);
            this.gboxEndereço.Controls.Add(this.lblEstado);
            this.gboxEndereço.Controls.Add(this.cboxEstado);
            this.gboxEndereço.Controls.Add(this.lblBairro);
            this.gboxEndereço.Controls.Add(this.txtCidade);
            this.gboxEndereço.Controls.Add(this.lblCidade);
            this.gboxEndereço.Controls.Add(this.txtBairro);
            this.gboxEndereço.Controls.Add(this.txtLogradouro);
            this.gboxEndereço.Location = new System.Drawing.Point(416, 37);
            this.gboxEndereço.Name = "gboxEndereço";
            this.gboxEndereço.Size = new System.Drawing.Size(301, 198);
            this.gboxEndereço.TabIndex = 7;
            this.gboxEndereço.TabStop = false;
            this.gboxEndereço.Text = "Endereço do Cliente";
            // 
            // btnProximo
            // 
            this.btnProximo.Enabled = false;
            this.btnProximo.Location = new System.Drawing.Point(572, 316);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(63, 28);
            this.btnProximo.TabIndex = 5;
            this.btnProximo.Text = ">>";
            this.ToolTip_btnProximo.SetToolTip(this.btnProximo, "Próximo");
            this.btnProximo.UseVisualStyleBackColor = true;
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click);
            // 
            // btnUltimo
            // 
            this.btnUltimo.Enabled = false;
            this.btnUltimo.Location = new System.Drawing.Point(641, 316);
            this.btnUltimo.Name = "btnUltimo";
            this.btnUltimo.Size = new System.Drawing.Size(63, 28);
            this.btnUltimo.TabIndex = 6;
            this.btnUltimo.Text = ">l";
            this.ToolTip_btnUltimo.SetToolTip(this.btnUltimo, "Último");
            this.btnUltimo.UseVisualStyleBackColor = true;
            this.btnUltimo.Click += new System.EventHandler(this.btnUltimo_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Enabled = false;
            this.btnAnterior.Location = new System.Drawing.Point(503, 316);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(63, 28);
            this.btnAnterior.TabIndex = 4;
            this.btnAnterior.Text = "<<";
            this.ToolTip_btnAnterior.SetToolTip(this.btnAnterior, "Anterior");
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnPrimeiro
            // 
            this.btnPrimeiro.Enabled = false;
            this.btnPrimeiro.Location = new System.Drawing.Point(434, 316);
            this.btnPrimeiro.Name = "btnPrimeiro";
            this.btnPrimeiro.Size = new System.Drawing.Size(63, 28);
            this.btnPrimeiro.TabIndex = 3;
            this.btnPrimeiro.Text = "l<";
            this.ToolTip_btnPrimeiro.SetToolTip(this.btnPrimeiro, "Primeiro");
            this.btnPrimeiro.UseVisualStyleBackColor = true;
            this.btnPrimeiro.Click += new System.EventHandler(this.btnPrimeiro_Click);
            // 
            // informaçãoDaConexãoToolStripMenuItem
            // 
            this.informaçãoDaConexãoToolStripMenuItem.Name = "informaçãoDaConexãoToolStripMenuItem";
            this.informaçãoDaConexãoToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.informaçãoDaConexãoToolStripMenuItem.Text = "Informações da Conexão";
            this.informaçãoDaConexãoToolStripMenuItem.Click += new System.EventHandler(this.MenuItemConInfo_Click);
            // 
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 374);
            this.Controls.Add(this.btnPrimeiro);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnUltimo);
            this.Controls.Add(this.btnProximo);
            this.Controls.Add(this.gboxEndereço);
            this.Controls.Add(this.gboxCliente);
            this.Controls.Add(this.btnOperacao);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(793, 412);
            this.MinimumSize = new System.Drawing.Size(793, 412);
            this.Name = "FormClientes";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Clientes";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gboxCliente.ResumeLayout(false);
            this.gboxCliente.PerformLayout();
            this.gboxEndereço.ResumeLayout(false);
            this.gboxEndereço.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem conexãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDesconectar;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPesquisar;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInserir;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExcluir;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblDataNasc;
        private System.Windows.Forms.Label lblSobrenome;
        private System.Windows.Forms.Label lblCEP;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label lblComplemento;
        private System.Windows.Forms.Label lblLogradouro;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.TextBox txtSobrenome;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.TextBox txtLogradouro;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.ComboBox cboxSexo;
        private System.Windows.Forms.ComboBox cboxEstado;
        private System.Windows.Forms.Button btnOperacao;
        private System.Windows.Forms.GroupBox gboxCliente;
        private System.Windows.Forms.GroupBox gboxEndereço;
        private System.Windows.Forms.Button btnProximo;
        private System.Windows.Forms.Button btnUltimo;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnPrimeiro;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblExcluído;
        private System.Windows.Forms.MaskedTextBox txtCEP;
        private System.Windows.Forms.MaskedTextBox txtDataNasc;
        private System.Windows.Forms.Label lblIndicePesquisa;
        private System.Windows.Forms.ToolTip ToolTip_btnProximo;
        private System.Windows.Forms.ToolTip ToolTip_btnUltimo;
        private System.Windows.Forms.ToolTip ToolTip_btnAnterior;
        private System.Windows.Forms.ToolTip ToolTip_btnPrimeiro;
        private System.Windows.Forms.ToolStripMenuItem informaçãoDaConexãoToolStripMenuItem;
    }
}
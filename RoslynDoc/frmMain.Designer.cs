namespace RoslynDoc
{
	partial class frmMain
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
			this.cmdGenerateDocuments = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.cboDocumentType = new Bau.Controls.Combos.ComboBoxExtended();
			this.chkShowPrivate = new System.Windows.Forms.CheckBox();
			this.chkShowProtected = new System.Windows.Forms.CheckBox();
			this.chkShowInternal = new System.Windows.Forms.CheckBox();
			this.chkShowPublic = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.fnTemplate = new Bau.Controls.Files.TextBoxSelectFile();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.trvNodes = new Bau.Controls.Tree.TreeViewExtended();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pthTarget = new Bau.Controls.Files.TextBoxSelectPath();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.fnSolution = new Bau.Controls.Files.TextBoxSelectFile();
			this.tabControl2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdGenerateDocuments
			// 
			this.cmdGenerateDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGenerateDocuments.Location = new System.Drawing.Point(626, 178);
			this.cmdGenerateDocuments.Name = "cmdGenerateDocuments";
			this.cmdGenerateDocuments.Size = new System.Drawing.Size(132, 23);
			this.cmdGenerateDocuments.TabIndex = 13;
			this.cmdGenerateDocuments.Text = "Generar documentación";
			this.cmdGenerateDocuments.UseVisualStyleBackColor = true;
			this.cmdGenerateDocuments.Click += new System.EventHandler(this.cmdGenerateDocuments_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label4.Location = new System.Drawing.Point(19, 113);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Tipo:";
			// 
			// cboDocumentType
			// 
			this.cboDocumentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDocumentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboDocumentType.ForeColor = System.Drawing.Color.Black;
			this.cboDocumentType.FormattingEnabled = true;
			this.cboDocumentType.Location = new System.Drawing.Point(84, 109);
			this.cboDocumentType.Name = "cboDocumentType";
			this.cboDocumentType.SelectedID = null;
			this.cboDocumentType.Size = new System.Drawing.Size(315, 21);
			this.cboDocumentType.TabIndex = 5;
			// 
			// chkShowPrivate
			// 
			this.chkShowPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowPrivate.AutoSize = true;
			this.chkShowPrivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkShowPrivate.ForeColor = System.Drawing.Color.Black;
			this.chkShowPrivate.Location = new System.Drawing.Point(585, 134);
			this.chkShowPrivate.Name = "chkShowPrivate";
			this.chkShowPrivate.Size = new System.Drawing.Size(156, 17);
			this.chkShowPrivate.TabIndex = 11;
			this.chkShowPrivate.Text = "Documentar datos privados";
			this.chkShowPrivate.UseVisualStyleBackColor = true;
			// 
			// chkShowProtected
			// 
			this.chkShowProtected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowProtected.AutoSize = true;
			this.chkShowProtected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkShowProtected.ForeColor = System.Drawing.Color.Black;
			this.chkShowProtected.Location = new System.Drawing.Point(416, 134);
			this.chkShowProtected.Name = "chkShowProtected";
			this.chkShowProtected.Size = new System.Drawing.Size(165, 17);
			this.chkShowProtected.TabIndex = 10;
			this.chkShowProtected.Text = "Documentar datos protegidos";
			this.chkShowProtected.UseVisualStyleBackColor = true;
			// 
			// chkShowInternal
			// 
			this.chkShowInternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowInternal.AutoSize = true;
			this.chkShowInternal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkShowInternal.ForeColor = System.Drawing.Color.Black;
			this.chkShowInternal.Location = new System.Drawing.Point(585, 111);
			this.chkShowInternal.Name = "chkShowInternal";
			this.chkShowInternal.Size = new System.Drawing.Size(153, 17);
			this.chkShowInternal.TabIndex = 9;
			this.chkShowInternal.Text = "Documentar datos internos";
			this.chkShowInternal.UseVisualStyleBackColor = true;
			// 
			// chkShowPublic
			// 
			this.chkShowPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowPublic.AutoSize = true;
			this.chkShowPublic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkShowPublic.ForeColor = System.Drawing.Color.Black;
			this.chkShowPublic.Location = new System.Drawing.Point(416, 111);
			this.chkShowPublic.Name = "chkShowPublic";
			this.chkShowPublic.Size = new System.Drawing.Size(155, 17);
			this.chkShowPublic.TabIndex = 8;
			this.chkShowPublic.Text = "Documentar datos públicos";
			this.chkShowPublic.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label7.Location = new System.Drawing.Point(18, 85);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(46, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Plantilla:";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Maroon;
			this.label5.Location = new System.Drawing.Point(194, 183);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(426, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Al generar la documentación se elimina el contenido del directorio destino";
			// 
			// fnTemplate
			// 
			this.fnTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fnTemplate.BackColorEdit = System.Drawing.SystemColors.Window;
			this.fnTemplate.FileName = "C:\\Users\\jbautistam\\Proyectos\\Development\\Net\\Testing\\RoslynDoc\\RoslynDoc.sln";
			this.fnTemplate.Filter = "Archivos de plantilla (*.xml)|*.xml";
			this.fnTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fnTemplate.Location = new System.Drawing.Point(84, 81);
			this.fnTemplate.Margin = new System.Windows.Forms.Padding(0);
			this.fnTemplate.MaximumSize = new System.Drawing.Size(11667, 20);
			this.fnTemplate.MinimumSize = new System.Drawing.Size(117, 20);
			this.fnTemplate.Name = "fnTemplate";
			this.fnTemplate.Size = new System.Drawing.Size(649, 20);
			this.fnTemplate.TabIndex = 1;
			this.fnTemplate.Type = Bau.Controls.Files.TextBoxSelectFile.FileSelectType.Load;
			// 
			// tabControl2
			// 
			this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl2.Controls.Add(this.tabPage4);
			this.tabControl2.Controls.Add(this.tabPage3);
			this.tabControl2.Location = new System.Drawing.Point(7, 207);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(756, 458);
			this.tabControl2.TabIndex = 1;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.trvNodes);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(748, 432);
			this.tabPage4.TabIndex = 1;
			this.tabPage4.Text = "Nodos";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// trvNodes
			// 
			this.trvNodes.CheckRecursive = false;
			this.trvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvNodes.Location = new System.Drawing.Point(3, 3);
			this.trvNodes.Name = "trvNodes";
			this.trvNodes.ShowNodeToolTips = true;
			this.trvNodes.Size = new System.Drawing.Size(742, 426);
			this.trvNodes.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.txtLog);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(747, 225);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Log";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// txtLog
			// 
			this.txtLog.AcceptsReturn = true;
			this.txtLog.AcceptsTab = true;
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(3, 3);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(741, 219);
			this.txtLog.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.pthTarget);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.fnSolution);
			this.groupBox1.Controls.Add(this.chkShowPrivate);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.chkShowPublic);
			this.groupBox1.Controls.Add(this.chkShowProtected);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.fnTemplate);
			this.groupBox1.Controls.Add(this.chkShowInternal);
			this.groupBox1.Controls.Add(this.cboDocumentType);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.groupBox1.Location = new System.Drawing.Point(10, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(749, 160);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Parámetros de generación";
			// 
			// pthTarget
			// 
			this.pthTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pthTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pthTarget.Location = new System.Drawing.Point(84, 53);
			this.pthTarget.Margin = new System.Windows.Forms.Padding(0);
			this.pthTarget.MaximumSize = new System.Drawing.Size(11667, 20);
			this.pthTarget.MinimumSize = new System.Drawing.Size(233, 20);
			this.pthTarget.Name = "pthTarget";
			this.pthTarget.PathName = "C:\\Users\\jbautistam\\Proyectos\\Development\\Net\\Testing\\RoslynDoc\\RoslynDoc\\Data";
			this.pthTarget.Size = new System.Drawing.Size(646, 20);
			this.pthTarget.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label2.Location = new System.Drawing.Point(18, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Solución:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label3.Location = new System.Drawing.Point(18, 57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Salida:";
			// 
			// fnSolution
			// 
			this.fnSolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fnSolution.BackColorEdit = System.Drawing.SystemColors.Window;
			this.fnSolution.FileName = "C:\\Users\\jbautistam\\Proyectos\\Development\\Net\\Testing\\RoslynDoc\\RoslynDoc.sln";
			this.fnSolution.Filter = "Archivos de solución (*.sln)|*.sln|Archivos de proyecto (*.csproj)|*.csproj";
			this.fnSolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fnSolution.Location = new System.Drawing.Point(84, 25);
			this.fnSolution.Margin = new System.Windows.Forms.Padding(0);
			this.fnSolution.MaximumSize = new System.Drawing.Size(11667, 20);
			this.fnSolution.MinimumSize = new System.Drawing.Size(117, 20);
			this.fnSolution.Name = "fnSolution";
			this.fnSolution.Size = new System.Drawing.Size(649, 20);
			this.fnSolution.TabIndex = 13;
			this.fnSolution.Type = Bau.Controls.Files.TextBoxSelectFile.FileSelectType.Load;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 669);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tabControl2);
			this.Controls.Add(this.cmdGenerateDocuments);
			this.Controls.Add(this.label5);
			this.Name = "frmMain";
			this.Text = "Generador de documentación con Roslyn";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.tabControl2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button cmdGenerateDocuments;
		private System.Windows.Forms.Label label4;
		private Bau.Controls.Combos.ComboBoxExtended cboDocumentType;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TextBox txtLog;
		private Bau.Controls.Tree.TreeViewExtended trvNodes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chkShowPrivate;
		private System.Windows.Forms.CheckBox chkShowProtected;
		private System.Windows.Forms.CheckBox chkShowInternal;
		private System.Windows.Forms.CheckBox chkShowPublic;
		private System.Windows.Forms.Label label7;
		private Bau.Controls.Files.TextBoxSelectFile fnTemplate;
		private System.Windows.Forms.GroupBox groupBox1;
		private Bau.Controls.Files.TextBoxSelectFile fnSolution;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private Bau.Controls.Files.TextBoxSelectPath pthTarget;
	}
}


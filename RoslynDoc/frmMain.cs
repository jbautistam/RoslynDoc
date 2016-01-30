using System;
using System.Windows.Forms;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols;
using Bau.Libraries.LibRoslynDocument;
using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynManager;

namespace RoslynDoc
{
	/// <summary>
	///		Formulario principal
	/// </summary>
	public partial class frmMain : Form
	{ 
		public frmMain()
		{	InitializeComponent();
		}

		/// <summary>
		///		Inicializa los controles del formulario
		/// </summary>
		private void InitForm()
		{ // Carga la configuración
				Configuration.Load();
			// Inicializa el combo de tipos
				cboDocumentType.Items.Clear();
				cboDocumentType.AddItem((int) DocumentationParameters.DocumentationType.Html, "Html");
				cboDocumentType.AddItem((int) DocumentationParameters.DocumentationType.Xml, "Xml");
				cboDocumentType.AddItem((int) DocumentationParameters.DocumentationType.Nhtml, "Nhtml");
				cboDocumentType.SelectedID = (int) Configuration.Parameters.IDType;
			// Inicializa los checkbox
				chkShowPublic.Checked = Configuration.Parameters.ShowPublic;
				chkShowInternal.Checked = Configuration.Parameters.ShowInternal;
				chkShowProtected.Checked = Configuration.Parameters.ShowProtected;
				chkShowPrivate.Checked = Configuration.Parameters.ShowPrivate;
			// Archivos y directorios
				fnSolution.FileName = Configuration.SolutionFileName;
				pthTarget.PathName = Configuration.OutputPath;
				fnTemplate.FileName = Configuration.Parameters.TemplateFileName;
		}

		/// <summary>
		///		Comprueba los datos introducidos de la solución
		/// </summary>
		private bool ValidateData()
		{ bool blnValidate = false;

				// Comprueba los datos
					if (fnSolution.FileName.IsEmpty() || !System.IO.File.Exists(fnSolution.FileName))
						Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione un archivo de solución o proyecto");
					else if (fnTemplate.FileName.IsEmpty() || !System.IO.File.Exists(fnTemplate.FileName))
						Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione un archivo de plantilla");
					else if (pthTarget.PathName.IsEmpty())
						Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione el directorio donde se genera la documentación");
					else
						blnValidate = true;
				// Devuelve el valor que indica si los datos son correctos
					return blnValidate;
		}

		/// <summary>
		///		Gemera la documentación
		/// </summary>
		private void GenerateDocuments()
		{ if (ValidateData())
				{ DocumentFileModelCollection objColDocuments;
					ProgramModel objProgram = new ProgramParser().ParseSolution(fnSolution.FileName);

						// Borra el directorio destino
							Bau.Libraries.LibHelper.Files.HelperFiles.KillPath(pthTarget.PathName);
						// Genera la documentación
							objColDocuments = new DocumentationGenerator(GetParametersDocuments(), pthTarget.PathName).Process(objProgram);
						// Muestra la documentación
							txtLog.Text = objColDocuments.Debug(0);
						// Muestra los nodos
							trvNodes.Nodes.Clear();
							ShowDocumentNodes(objColDocuments, null);
							trvNodes.ExpandAll();
				}
		}

		/// <summary>
		///		Obtiene los parámetros de generación del documento
		/// </summary>
		private DocumentationParameters GetParametersDocuments()
		{ DocumentationParameters objParameters = new DocumentationParameters();

				// Asigna los valores del formulario
					objParameters.TemplateFileName = fnTemplate.FileName;
					objParameters.IDType = (DocumentationParameters.DocumentationType) (cboDocumentType.SelectedID ?? 0);
				// Inicializa los checkbox
					objParameters.ShowPublic = chkShowPublic.Checked;
					objParameters.ShowInternal = chkShowInternal.Checked;
					objParameters.ShowProtected = chkShowProtected.Checked;
					objParameters.ShowPrivate = chkShowPrivate.Checked;
				// Graba la configuración de la documentación
					Configuration.Save(fnSolution.FileName, pthTarget.PathName, objParameters);
				// Devuelve los parámetros
					return objParameters;
		}

		/// <summary>
		///		Muestra los documentos en el árbol
		/// </summary>
		private void ShowDocumentNodes(DocumentFileModelCollection objColDocuments, TreeNode trnParent)
		{ foreach (DocumentFileModel objDocument in objColDocuments)
				{ TreeNode trnNode = trvNodes.AddNode(trnParent, 0, 0, objDocument.Name, false);

						// Añade los documentos hijo
							ShowDocumentNodes(objDocument.Childs, trnNode);
				}
		}

		private void cmdGenerateDocuments_Click(Object sender, EventArgs e)
		{ GenerateDocuments();
		}

		private void frmMain_Load(Object sender, EventArgs e)
		{ InitForm();
		}
	}
}

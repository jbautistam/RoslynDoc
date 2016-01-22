using System;

namespace Bau.Libraries.LibRoslynDocument
{
	/// <summary>
	///		Parámetros de documentación
	/// </summary>
	public class DocumentationParameters
	{	// Enumerados públicos
			/// <summary>
			///		Tipo de documentación a generar
			/// </summary>
			public enum DocumentationType
				{ 
					/// <summary>Archivos Html</summary>
					Html,
					/// <summary>Archivos Nhtml</summary>
					Nhtml,
					/// <summary>Archivos Xml</summary>
					Xml
				}
		/// <summary>
		///		Modo de documentación
		/// </summary>
		public enum DocumentationMode
			{ 
				/// <summary>Indica que se muestra una página por clase / estructura</summary>
				SimpleStructs,
				/// <summary>Indica que se muestra una página por cada clase, estructura, método...</summary>
				ComplexStructs
			}
		
		/// <summary>
		///		Tipo de documentación
		/// </summary>
		public DocumentationType IDType { get; set; } = DocumentationType.Nhtml;

		/// <summary>
		///		Modo de generación de la documentación
		/// </summary>
		public DocumentationMode Mode { get; set; } = DocumentationMode.SimpleStructs;

		/// <summary>
		///		Indica si se documentan las estructuras públicas
		/// </summary>
		public bool ShowPublic { get; set; } = true;

		/// <summary>
		///		Indica si se documentan las estructuras internas
		/// </summary>
		public bool ShowInternal { get; set; } = true;

		/// <summary>
		///		Indica si se documentan las estructuras protegidas
		/// </summary>
		public bool ShowProtected { get; set; } = true;

		/// <summary>
		///		Indica si se documentan las estructuras privadas
		/// </summary>
		public bool ShowPrivate { get; set; } = true;
	}
}
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEASAPP.Models
{
    public class Aporte
    {
		public int Id { get; set; }
		public int CNegocio { get; set; }
		public DateTime FechaAporte { get; set; }
		public DateTime FechaRespuesta { get; set; }
		public int CTipoAporte { get; set; }
		public int CCategoriaAporte { get; set; }
		public string DComentario { get; set; }
		public int CCalificacion { get; set; }
		public int DPuntosOtorgados { get; set; }
		public string DRespuesta { get; set; }
		public int CEstado { get; set; }


		public ImageSource SourceFoto { get; set; }
		public string NombrePersona { get; set; }
		public string NombreEmpresa { get; set; }	
		public string VistaCalificacion { get; set; }	
		public string VistaCategoriaAporte { get; set; }	
		public string VistaEstado { get; set; }	
		public string VistaTipoAporte { get; set; }
	}
}

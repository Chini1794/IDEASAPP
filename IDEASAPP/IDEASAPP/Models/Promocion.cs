using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEASAPP.Models
{
	public class Promocion
	{
		public int Id { get; set; }
		public string DNombre { get; set; }
		public int DPorcentaje { get; set; }
		public string DDescripcion { get; set; }
		public DateTime DCreacion { get; set; }
		public DateTime DVigencia { get; set; }
		public int DRecompensaPuntos { get; set; }
		public byte[] DFoto { get; set; }
		public int CNegocio { get; set; }
		public bool DEstadoActiva { get; set; }
		public int CTipoPromocion { get; set; }

		public ImageSource SourceFoto { get; set; }
		public string NombreEmpresa { get; set; }
	}
}

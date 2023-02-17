using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEASAPP.Models
{
    public class NegocioMiembro
    {
		public int Id { get; set; }
		public string CIdMiembro { get; set; }
		public DateTime FechaIngreso { get; set; }
		public string DCedula { get; set; }
		public int DTipoCedula { get; set; }
		public string DNombreJurIdico { get; set; }
		public string DNombreComercial { get; set; }
		public string DDireccion { get; set; }
		public string DDescripcion { get; set; }
		public string DAcronimo { get; set; }
		public byte[] DLogo { get; set; }
		public string DEmailEmpresarial { get; set; }
		public string DTelefono1 { get; set; }
		public string DTelefono2 { get; set; }
		public string DContactoRepresentante { get; set; }
		public string DRepresentanteLegal { get; set; }
		public byte[] DFoto { get; set; }
		public string DContactoAdministrador { get; set; }
		public string DAdministrador { get; set; }

		public ImageSource SourceFoto { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IDEASAPP.Models
{
    public class PersonaMiembro
    {
		public int Id { get; set; }
		public string CIdMiembro { get; set; }
		public DateTime FechaIngreso { get; set; }
		public string DCedula { get; set; }
		public int DTipoCedula { get; set; }
		public string DNombre { get; set; }
		public string DEmail { get; set; }
		public string DContrasena { get; set; }
		public string DTelefonoCelular { get; set; }
		public string DTelefonoOficina { get; set; }
		public string DTelefonoCasa { get; set; }
		public byte[] DFoto { get; set; }
	}
}

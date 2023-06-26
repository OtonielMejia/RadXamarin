using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RADXamarin.Models
{
   public  class Contactos
    {
        [PrimaryKey,AutoIncrement]
        public int IdContacto { get; set; }
        [MaxLength(50)]
        public string Nombres { get; set; }
        [MaxLength(50)]
        public string Apellidos { get; set; }
        [MaxLength(50)]
        public int Edad { get; set;}
        [MaxLength(100)]
        public string Pais { get; set;}
        [MaxLength(50)]
        public string Nota { get; set; }
        [MaxLength(150)]
        public float Latitud { get; set;}
        [MaxLength(50)]
        public float Longitud { get; set;}
        [MaxLength(50)]
        public byte Imagen { get; set;}

    }
}

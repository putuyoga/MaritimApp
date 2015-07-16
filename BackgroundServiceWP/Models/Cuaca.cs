using System;
using System.Collections.Generic;
using System.Text;

namespace MaritimApp.Models
{
    public class Cuaca
    {
        public int IDLokasi { get; set; }
        public DateTime Waktu { get; set; }
        public int KodeCuaca { get; set; }
        public string ArahAngin { get; set; }
        public string ArahGelombang { get; set; }
        public int KecepatanAngin { get; set; }
        public float TinggiGelombangMaks { get; set; }
        public float TinggiGelombang { get; set; }
        public int JarakPandang { get; set; }
        public int TemperaturAir { get; set; }
        public int Kelembapan { get; set; }
    }
}

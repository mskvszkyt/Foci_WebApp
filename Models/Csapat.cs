namespace Foci_WebApp.Models
{
    public class Csapat
    {
        public int Helyezes { get; set; }
        public string Nev {  get; set; }
        public int MeccsekSzama { get; set; } =0;
        public int Gyozelem { get; set; } = 0;
        public int Dontetlen { get; set; } = 0;
        public int Vereseg { get; set; } = 0;
        public int Pont { get; set; } = 0;
        public int LottGol { get; set; } = 0;   
        public int KapottGol { get; set; } = 0;
    }
}

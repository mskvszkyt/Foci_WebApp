using Foci_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Foci_WebApp;
using Microsoft.EntityFrameworkCore;

namespace Foci_WebApp.Pages
{
    public class RangsorModel : PageModel
    {
        private readonly Foci_WebApp.Models.FociDbContext _context;

        public RangsorModel(Foci_WebApp.Models.FociDbContext context)
        {
            _context = context;
        }

        public IList<Meccs> Meccs { get; set; } = default!;
        public IList<Csapat> Csapat { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Meccs = await _context.Meccsek.ToListAsync();

            foreach (var item in Meccs)
            {
                if (Csapat.Any(y => y.Nev == item.VendegCsapat) || Csapat.Any(x => x.Nev == item.HazaiCsapat))
                {
                    Csapat csapat;
                    if (Csapat.Any(y => y.Nev == item.VendegCsapat))
                    {
                        csapat = Csapat.First(y => y.Nev == item.VendegCsapat);
                        csapat.Nev = item.VendegCsapat;
                        csapat.MeccsekSzama++;
                        csapat.LottGol += item.VendegVeg;
                        csapat.KapottGol += item.HazaiVeg;

                        if (item.VendegVeg > item.HazaiVeg) csapat.Gyozelem++;
                        else if (item.VendegVeg < item.HazaiVeg) csapat.Vereseg++;
                        else csapat.Dontetlen++;

                        csapat.Pont = csapat.Dontetlen + csapat.Gyozelem * 3;
                    }
                    else
                    {
                        csapat = Csapat.First(y => y.Nev == item.HazaiCsapat);
                        csapat.Nev = item.HazaiCsapat;
                        csapat.MeccsekSzama++;
                        csapat.LottGol += item.HazaiVeg;
                        csapat.KapottGol += item.VendegVeg;

                        if (item.VendegVeg > item.HazaiVeg) csapat.Vereseg++;
                        else if (item.VendegVeg < item.HazaiVeg) csapat.Gyozelem++;
                        else csapat.Dontetlen++;

                        csapat.Pont = csapat.Dontetlen + csapat.Gyozelem * 3;
                    }
                }
                else
                {
                    Csapat cs1 = new();
                    Csapat cs2 = new();

                    cs1.Nev = item.VendegCsapat;
                    cs2.Nev = item.HazaiCsapat;

                    cs1.MeccsekSzama++;
                    cs2.MeccsekSzama++;

                    cs1.LottGol += item.VendegVeg;
                    cs2.LottGol += item.HazaiVeg;

                    cs1.KapottGol += item.HazaiVeg;
                    cs2.KapottGol += item.VendegVeg;

                    if (item.VendegVeg > item.HazaiVeg)
                    {
                        cs1.Gyozelem++; cs2.Vereseg++;
                    }
                    else if (item.VendegVeg < item.HazaiVeg)
                    {
                        cs1.Vereseg++; cs2.Gyozelem++;
                    }
                    else
                    {
                        cs1.Dontetlen++; cs2.Dontetlen++;
                    }

                    cs1.Pont = cs1.Dontetlen + cs1.Gyozelem * 3;
                    cs2.Pont = cs2.Dontetlen + cs2.Gyozelem * 3;

                    Csapat.Add(cs1);
                    Csapat.Add(cs2);
                }

            }

            int i = 1;
            foreach (var item in Csapat.OrderByDescending(x=>x.Pont))
            {
                item.Helyezes = i;
                i++;
            }
        }
    }
}

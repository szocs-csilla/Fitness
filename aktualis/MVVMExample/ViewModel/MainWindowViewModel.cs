using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private const String SEPARATOR_LISTAZAS = " ";
        public List<Ugyfelek> ugyfelek { get; set; }
        public String ugyfelekLista { get; set; }
        public Boolean isAdmin { get; set; }

        public List<Ugyfelek> Ugyfelek
        {
            get
            {
                return ugyfelek;
            }

            set
            {
                ugyfelek = value;
                RaisePropertyChanged("Ugyfelek");
            }
        }


        private MyDBContext dc = new MyDBContext();
        public MainWindowViewModel(Boolean isAdmin)
        {
            this.isAdmin = isAdmin;
            var item = dc.Ugyfelek.ToList();
            Ugyfelek = item;
            this.ugyfelekLista = ugyfelekListazas();

        }
        public MainWindowViewModel()
        {
            this.isAdmin = false;
            var item = dc.Ugyfelek.ToList();
            Ugyfelek = item;
            this.ugyfelekLista = ugyfelekListazas();
        }

        private String ugyfelekListazas()
        {
            StringBuilder sb = new StringBuilder();
            this.Ugyfelek.ForEach(ugyfel =>
            {
                sb.Append(ugyfel.Nev);
                sb.Append(SEPARATOR_LISTAZAS);
                sb.Append(ugyfel.E_mail);
                sb.Append(SEPARATOR_LISTAZAS);
                sb.Append(ugyfel.Szuletesi_Datum);
                sb.Append(SEPARATOR_LISTAZAS);
                sb.Append(Environment.NewLine + "Berletek: " + Environment.NewLine);
                List<Ugyfel_Berlet> ugyfelBerletek = dc.Ugyfel_Berlet.Where(berlet => berlet.Ugyfel_ID == ugyfel.Ugyfel_ID).ToList();
                ugyfelBerletek.ForEach(berlet => {
                    sb.Append(berlet.Berletek.Nev);
                    sb.Append(SEPARATOR_LISTAZAS);
                    sb.Append(berlet.Kartya_Szam);
                    sb.Append(SEPARATOR_LISTAZAS);
                    sb.Append(berlet.Berletek.Aktivitas);
                    sb.Append(SEPARATOR_LISTAZAS);
                    sb.Append(berlet.Berletek.Ar);
                    sb.Append(SEPARATOR_LISTAZAS);
                    sb.Append(berlet.Berletek.Belepes);
                    sb.Append(SEPARATOR_LISTAZAS);
                    sb.Append(berlet.Berletek.Mikortol);
                    sb.Append(SEPARATOR_LISTAZAS);
                    sb.Append(berlet.Berletek.Idotartam);
                    sb.Append(Environment.NewLine);
                });
                sb.Append("-------------------------------------------------------------" + Environment.NewLine);
            });
            return sb.ToString();
        }
    }
}

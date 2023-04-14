using Maok.App.Modules.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Plugin.Calendar.Models;

namespace Maok.App.Modules.Home.Models
{
    public class HomeModel : BaseModel
    {
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";
        public string Logo { get; set; } = "logo_preto.png";
        public string Festa { get; set; } = "festa.png";
        public string VoucherId { get; set; }
        public decimal Opacity { get; set; } = 1;
        public bool IsHome { get; set; } = true;
        public bool IsTicket { get; set; } = false;
        public bool IsCalendar { get; set; } = false;
        public bool IsProfile { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public bool PopUp { get; set; } = false;
        public EventModel Event { get; set; }
        public ObservableCollection<ContentModel> Items { get; set; } = new ObservableCollection<ContentModel>();
        public List<ContentModel> ItemsOld { get; set; }
        public ContentModel ItemHighlight { get; set; }
        public EventCollection Calendars { get; set; }
        public CultureInfo Culture { get; set; }

        public new bool IsValid
        {
            get
            {
                ResetError();

                if (IsTicket)
                {
                    if (string.IsNullOrEmpty(VoucherId))
                        Erros.AppendLine("Campo voucher é obrigatório");
                }

                return Erros.Length <= 0;
            }
        }

        private void ResetError()
        {
            Erros.Clear();
        }
    }
}
using Maok.App.Utils;
using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Maok.App.Modules.Shared.Models
{
    [AddINotifyPropertyChangedInterface]
    public class BaseModel : INotifyPropertyChanged
    {
        [DoNotNotify]
        public string Hash { get; set; }

        public bool IsBusy { get; set; }

        public virtual bool IsValid { get; }

        [DoNotNotify]
        public StringBuilder Erros { get; } = new StringBuilder();

        public virtual void ResetError() => Erros.Clear();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

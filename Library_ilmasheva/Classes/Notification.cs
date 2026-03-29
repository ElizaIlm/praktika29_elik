using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library_ilmasheva.Classes
{
    public class Notification : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

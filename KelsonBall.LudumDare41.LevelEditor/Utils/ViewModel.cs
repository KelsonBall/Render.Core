using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual void Set(Action setAction, [CallerMemberName] string name = null, string notify = null)
        {
            setAction();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            if (!string.IsNullOrEmpty(notify))
                OnPropertyChanged(notify);
        }
    }
}

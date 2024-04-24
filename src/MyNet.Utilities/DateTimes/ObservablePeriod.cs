// Copyright (c) Stéphane ANDRE. All Right Reserved.	
// See the LICENSE file in the project root for more information.	

using System;
using System.ComponentModel;

namespace MyNet.Utilities.DateTimes
{
    public class ObservablePeriod : Period, INotifyPropertyChanged
    {
        public ObservablePeriod(DateTime start, DateTime end) : base(start, end) { }

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add => PropertyChangedHandler += value;
            remove => PropertyChangedHandler -= value;
        }

        private event PropertyChangedEventHandler? PropertyChangedHandler;

        protected void RaisePropertyChanged(string? propertyName) => PropertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public override void SetInterval(DateTime start, DateTime end)
        {
            var oldStart = Start;
            var oldEnd = End;
            base.SetInterval(start, end);
            if (oldStart != Start)
                RaisePropertyChanged(nameof(Start));
            if (oldEnd != End)
                RaisePropertyChanged(nameof(End));
        }
    }
}

﻿using System;
using System.Windows.Input;

namespace KelsonBall.LudumDare41.LevelEditor.Utils
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> _action;

        public ActionCommand(Action action)
        {
            _action = parameter => action();
        }

        public ActionCommand(Action<object> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _action?.Invoke(parameter);
    }
}

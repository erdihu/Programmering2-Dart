using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Visual
{
    /// <summary>
    /// This class was not originally coded by me.
    /// I believe the code was based off Microsoft's PRISM framework (as opposed to an oft used RelayCommand class)
    /// </summary>
    class DelegateCommand : ICommand
    {

        //Delegate to check whether an action can be exectued. This has upshots such as automatically enabling/disabling UI components
        Func<object, bool> canExecute;

        //Delegate to hold the expression that should be executed
        Action<object> executeAction;

        //a cache used to prevent a CanExecuteChanged from firing if the value has changed, despite other events possibly having been triggered
        bool canExecuteCache;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }


        /// <summary>
        /// This method returns the state of the object and whether the action can be exectued at the time of calling
        /// </summary>
        /// <param name="parameter">Expression to calculate if the action can be executed</param>
        /// <returns>True if the action can be executed, otherwise false</returns>
        public bool CanExecute(object parameter)
        {
            bool temp = canExecute(parameter);

            if (canExecuteCache != temp)
            {
                canExecuteCache = temp;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, new EventArgs());
            }

            return canExecuteCache;
        }

        /// <summary>
        /// Execute the action that has been provided
        /// </summary>
        /// <param name="parameter">Action to execute</param>
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}

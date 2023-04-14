using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils.EventArgs;
using Maok.App.Utils.Handlers;
using Plugin.Connectivity;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Utils
{
    public class CommandWaitCustom<T> : ICommand
    {
        private readonly Action<T, TaskCompletionSource<bool>> _execute;
        private readonly Func<T, TaskCompletionSource<bool>, Task> _funcExecute;

        private volatile bool _inProgress;
        private TaskCompletionSource<bool> _tcs;
        private string _callerName;
        private WeakReference<object> _parent;

        public CommandWaitCustom(Action<T, TaskCompletionSource<bool>> execute, object parent, [CallerMemberName] string callerName = "")
        {
            _execute = execute;
            SetCommandMetadata(parent, callerName);
        }

        public CommandWaitCustom(Func<T, TaskCompletionSource<bool>, Task> execute, object parent, [CallerMemberName] string callerName = "")
        {
            _funcExecute = execute;
            SetCommandMetadata(parent, callerName);
        }

        public CommandWaitCustom(Action<TaskCompletionSource<bool>> execute, object parent, [CallerMemberName] string callerName = "")
            : this((o, tsc) => execute(tsc), parent, callerName)
        {
        }

        public CommandWaitCustom(Func<TaskCompletionSource<bool>, Task> execute, object parent, [CallerMemberName] string callerName = "")
            : this(async (o, tsc) => await execute(tsc), parent, callerName)
        {
        }

        private void SetCommandMetadata(object parent, string callerName)
        {
            _callerName = callerName;
            _parent = new WeakReference<object>(parent);
        }

        public bool CanExecute(object parameter)
        {
            return !_inProgress;
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            bool parentAlive = _parent.TryGetTarget(out object parent);
            string className = parent.GetType().Name;

            try
            {
                if (_inProgress)
                    return;

                ChangeCanExecute(true);

                var isConnected = CrossConnectivity.Current.IsConnected;

                if (!isConnected)
                {
                    //await PageMethods.ShowAlertAsync("Não foi possível efetuar a consulta, verifique sua conexão com a internet.");
                    ChangeCanExecute(false);
                    return;
                }

                _tcs = new TaskCompletionSource<bool>();
                if (parameter != null)
                    _execute?.Invoke((T)Convert.ChangeType(parameter, typeof(T)), _tcs);
                else
                    _execute?.Invoke(default, _tcs);

                if (_funcExecute != null)
                {
                    if (parameter != null)
                        await _funcExecute((T)Convert.ChangeType(parameter, typeof(T)), _tcs);
                    else
                        await _funcExecute(default, _tcs);
                }
            }
            catch (Exception ex)
            {
                AsyncErrorHandler.HandleException(ex);
                //await PageMethods.ShowAlertAsync("Ocorreu um erro ao efetuar esta ação, nossa equipe foi informada deste problema. Tente novamente mais tarde.");
                await PageMethods.InvokeOnCurrentPageModel<BasePageModel>(pageModel => pageModel.IsBusy = false);
                ChangeCanExecute(false);
            }
            finally
            {
                if (_tcs != null)
                    ChangeCanExecute(!await _tcs.Task);
            }
        }

        public void ChangeCanExecute(bool changed)
        {
            _inProgress = changed;
            CanExecuteChanged?.Invoke(this, new CommandExecuteChagendEventArgs(_inProgress));
        }
    }

    public class CommandWaitCustom : CommandWaitCustom<object>
    {
        public CommandWaitCustom(Action<object, TaskCompletionSource<bool>> execute, object parent, [CallerMemberName] string callerName = "")
            : base(execute, parent, callerName)
        {
        }

        public CommandWaitCustom(Func<object, TaskCompletionSource<bool>, Task> execute, object parent, [CallerMemberName] string callerName = "")
            : base(execute, parent, callerName)
        {
        }

        public CommandWaitCustom(Action<TaskCompletionSource<bool>> execute, object parent, [CallerMemberName] string callerName = "")
            : base(execute, parent, callerName)
        {
        }

        public CommandWaitCustom(Func<TaskCompletionSource<bool>, Task> execute, object parent, [CallerMemberName] string callerName = "")
            : base(execute, parent, callerName)
        {
        }
    }
}
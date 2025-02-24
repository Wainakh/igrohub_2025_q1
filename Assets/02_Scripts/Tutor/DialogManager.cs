using System;
using System.Collections.Generic;
using UnityEngine;

namespace Igrohub
{
    public class DialogManager : IDialogManager
    {
        private DialogView _view;
        private List<DialogConfig> _configs;
        private DialogController _controller;
        
        public DialogManager()
        {
            _configs = new List<DialogConfig>(Resources.LoadAll<DialogConfig>(""));
            _view = CreateDialogView();
            _view.HideAll();
        }

        private DialogView CreateDialogView() => UnityEngine.Object.FindFirstObjectByType<DialogView>();

        public void InterruptDialog()
        {
            if (_controller != null)
                _controller.Interrupt();
        }

        public void StartDialog(int configId, Action onFinish)
        {
            StartDialog(FindConfig(configId), onFinish);
        }

        private DialogConfig FindConfig(int configId)
        {
            foreach (var config in _configs)
                if (config.Id.Equals(configId))
                    return config;
            return null;
        }

        public void StartDialog(DialogConfig config, Action onFinish)
        {
            InterruptDialog();
        
            if(config == null)
                return;

            _controller = new DialogController(config, _view, () =>
            {
                _controller.Interrupt();
                _controller = null;
                onFinish?.Invoke();
            });
            _controller.Start();
        }
    }
}
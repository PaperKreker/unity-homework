using System;
using SampleGame.SaveSystem;
using Zenject;

namespace Game.Gameplay
{
    public class ControlsPresenter : IControlsPresenter
    {
        private SaveManager _saveManager;
        
        [Inject]
        public ControlsPresenter(SaveManager saveManager)
        {
            _saveManager = saveManager;
        }
        
        public void Save(Action<bool, int> callback)
        {
            _saveManager.Save(callback).Forget();
        }

        public void Load(string version, Action<bool, int> callback)
        {
            if (int.TryParse(version, out int versionIndex))
            {
                _saveManager.Load(callback, versionIndex).Forget();
            }
            else
            {
                _saveManager.Load(callback).Forget();
            }
        }
    }
}
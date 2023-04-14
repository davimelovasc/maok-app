using Maok.App.Modules.Home.Models;
using Maok.App.Modules.Shared.Models;
using Maok.App.Modules.Shared.PageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maok.App.Modules.Home.PageModels
{
    public class ProfilePageModel : BasePageModel<ProfileModel>
    {
        private ComboBoxModel _genderSelected;
        public ComboBoxModel GenderSelected
        {
            get
            {
                return _genderSelected;
            }
            set
            {
                SetProperty(ref _genderSelected, value);
            }
        }

        public ProfilePageModel()
        {
        }
    }
}
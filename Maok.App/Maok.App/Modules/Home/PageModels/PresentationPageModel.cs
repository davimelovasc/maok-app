using Maok.App.Modules.Shared.Models;
using Maok.App.Modules.Shared.PageModels;
using Maok.App.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maok.App.Modules.Home.PageModels
{
    public class PresentationPageModel : BasePageModel<BaseModel>
    {
        public ICommand GoToLoginCommand => new CommandWaitCustom(DoToLoginAsync, this);
        public string ImageBackground { get; set; } = "degrade_fundo_1.png";

        public PresentationPageModel()
        {
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            //RandonBackgroundImages();
        }

        private async Task DoToLoginAsync(TaskCompletionSource<bool> tsc)
        {
            //await CoreMethods.PushPageModelWithNewNavigation<LoginPageModel>(new LoginSetupParameter(ImageBackground));
            tsc.SetResult(true);
        }

        //private void RandonBackgroundImages()
        //{
        //    List<string> images = new List<string> { "", "degrade_fundo_1.png", "degrade_fundo_1.png", "degrade_fundo_1.png" };
        //    var rand = new Random();
        //    var index = rand.Next(4);
        //    ImageBackground = images[index];
        //}
    }
}
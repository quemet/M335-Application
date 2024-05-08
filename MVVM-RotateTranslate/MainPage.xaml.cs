using MVVM_RotateTranslate.ViewModels;

namespace MVVM_RotateTranslate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var vm = BindingContext as AnimationViewModel;

            vm.RotateBoxUIAction = RotateUI;

            #region move
            vm.MoveBoxUIAction = (int x) =>
            {
                this.box.TranslationX += x;
            };
            #endregion
        }

        private void RotateUI(int angle)
        {
            this.box.RotateTo(angle);
        }
    }

}

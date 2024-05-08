namespace RotateTranslate
{
    public partial class MainPage : ContentPage
    {
        private int speed = 1;
        private string label = "Rotate 90°";
        public MainPage()
        {
            InitializeComponent();

            slider.Value = speed;

            lbox.Text = label;
			
			lblslider.Text = "Speed " + speed
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (label == "Rotate 90°") {
                this.box.RotateTo(90, 2000);
                label = "Rotate Back";
            } else {
                this.box.RotateTo(0, 2000);
                label = "Rotate 90°";
            }

            lbox.Text = label;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            speed = (int)slider.Value;
            this.box.TranslateTo(X - speed, Y);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            speed = (int)slider.Value;
            this.box.TranslateTo(X + speed, Y);
        }
    }

}

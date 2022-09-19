using UraniumUI.Pages;

namespace UraniumApp;

public partial class MainPage : UraniumContentPage
{
    public MainPage()
    {
        InitializeComponent();

        //myEditor.HandlerChanged += (s, e) =>
        //{
        //    Console.WriteLine(myEditor.Handler.PlatformView.GetType());
        //};
    }

    //private async void OnCounterClicked(object sender, EventArgs e)
    //{
    //    count++;

    //    if (count == 1)
    //        CounterBtn.Text = $"Clicked {count} time";
    //    else
    //        CounterBtn.Text = $"Clicked {count} times";

    //    SemanticScreenReader.Announce(CounterBtn.Text);
    //}
}
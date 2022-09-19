using InputKit.Handlers.IconView;
using InputKit.Shared.Controls;
using Microsoft.Maui.Handlers;
using UraniumUI.Material.Controls;



#if __IOS__ || MACCATALYST
using NativeView = UIKit.UITextView;
using UIKit;
#elif ANDROID
using NativeView = Android.Widget.EditText;
using Android.Views;
#elif WINDOWS
using NativeView = Microsoft.UI.Xaml.Controls.WebView2;
#elif NET6_0
using NativeView = System.Object;
#endif

namespace UraniumUI.Material.Handlers;
public class SelectableTextHandler : ViewHandler<SelectableText, NativeView>
{
    public static IPropertyMapper<SelectableText, SelectableTextHandler> SelectableTextMapper => new PropertyMapper<SelectableText, SelectableTextHandler>(ViewMapper)
    {
        [nameof(SelectableText.Text)] = MapText
    };
    public SelectableTextHandler() : base(SelectableTextMapper, ViewCommandMapper)
    {

    }
#if __IOS__ || MACCATALYST
    protected override NativeView CreatePlatformView()
    {
        var control = new NativeView();

        control.Selectable = true;
        control.Editable = false;
        control.ScrollEnabled = false;
        control.TextContainerInset = UIEdgeInsets.Zero;
        control.TextContainer.LineFragmentPadding = 0;

        return control;
    }

    static void MapText(SelectableTextHandler handler, SelectableText view)
    {
        handler.PlatformView.Text = view.Text;
    }

#elif ANDROID
    protected override NativeView CreatePlatformView()
    {
        var control = new NativeView(Context);

        control.Background = null;
        control.SetPadding(0, 0, 0, 0);
        control.ShowSoftInputOnFocus = false;
        control.SetTextIsSelectable(true);
        control.CustomSelectionActionModeCallback = new CustomSelectionActionModeCallback();
        control.CustomInsertionActionModeCallback = new CustomInsertionActionModeCallback();

        return control;
    }
    
    static void MapText(SelectableTextHandler handler, SelectableText view)
    {
        handler.PlatformView.Text = view.Text;
    }

    private class CustomInsertionActionModeCallback : Java.Lang.Object, ActionMode.ICallback
    {
        public bool OnCreateActionMode(ActionMode mode, IMenu menu) => false;

        public bool OnActionItemClicked(ActionMode m, IMenuItem i) => false;

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu) => true;

        public void OnDestroyActionMode(ActionMode mode) { }
    }

    private class CustomSelectionActionModeCallback : Java.Lang.Object, ActionMode.ICallback
    {
        private const int CopyId = Android.Resource.Id.Copy;

        public bool OnActionItemClicked(ActionMode m, IMenuItem i) => false;

        public bool OnCreateActionMode(ActionMode mode, IMenu menu) => true;

        public void OnDestroyActionMode(ActionMode mode) { }

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            try
            {
                var copyItem = menu.FindItem(CopyId);
                var title = copyItem.TitleFormatted;
                menu.Clear();
                menu.Add(0, CopyId, 0, title);
            }
            catch
            {
                // ignored
            }

            return true;
        }
    }
#elif WINDOWS
    protected override NativeView CreatePlatformView()
    {
        var control = new NativeView();

        var transparentBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0)); ;
        //control.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
        //control.IsReadOnly = true;
        //control.TextWrapping = Microsoft.UI.Xaml.TextWrapping.Wrap;
        //control.Background = transparentBrush;

        return control;
    }
    static async void MapText(SelectableTextHandler handler, SelectableText view)
    {
        await Task.Delay(2000);
        handler.PlatformView.NavigateToString("<p>" + view.Text + "</p>");
    }

#elif NET6_0
    protected override NativeView CreatePlatformView()
    {
        return new NativeView();
    }
    
    static void MapText(SelectableTextHandler handler, SelectableText view)
    {
        
    }
#endif
}

using Windows.UI.Xaml;
using System;

namespace OneUI.Xaml.Controls
{
    public class XamlControlsResources : ResourceDictionary
    {
        public XamlControlsResources()
        {
            this.Source = new Uri("ms-appx:///OneUI.for.UWP/Assets/XamlControlsResources.xaml");
        }
    }
}

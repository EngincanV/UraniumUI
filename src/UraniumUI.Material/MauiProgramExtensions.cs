using UraniumUI.Material.Controls;
using UraniumUI.Material.Handlers;

namespace UraniumUI.Material;
public static class MauiProgramExtensions
{
    public static IMauiHandlersCollection AddMaterialHandlers(this IMauiHandlersCollection collection)
    {
        return collection
            .AddHandler(typeof(SelectableText), typeof(SelectableTextHandler))
            ;
    }
}

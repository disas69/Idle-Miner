using UI.Structure.Base;
using UI.Structure.Base.View;

namespace UI.Structure
{
    public interface INavigationProvider
    {
        Screen CurrentScreen { get; }
        void OpenScreen<T>() where T : Screen;
        void ShowPopup<T>() where T : Popup;
        void Back();
    }
}
namespace HospitalUi.ViewModels
{
    public abstract class BasePageViewModel : BaseViewModel
    {
        public abstract string Title { get; }

        public abstract void GetData();
        public abstract void Refresh();

    }
}

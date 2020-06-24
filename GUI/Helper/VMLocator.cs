using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GUI.ViewModel;

namespace GUI.Helper
{
    class VMLocator
    {
        public VMLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RequestFileViewModel>();
        }
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public RequestFileViewModel RequestFileViewModel => ServiceLocator.Current.GetInstance<RequestFileViewModel>();
    }
}

namespace Core
{
    public class TabbedItemPage : SearchPage
    {
        object _parameter;
        public void SetParameter(object parameter)
            => _parameter = parameter;

        public object GetParameter()
           => _parameter;
    }
}

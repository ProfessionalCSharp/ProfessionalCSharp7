namespace PatternMatching
{
    public class HealthPackage
    {
        public void CheckHealth()
        {
        }
    }
    public class HttpManager
    {
        public void Send<T>(T package)
        {
            if (package is HealthPackage hp)
            {
                hp.CheckHealth();
            }
            //...
        }
    }
}

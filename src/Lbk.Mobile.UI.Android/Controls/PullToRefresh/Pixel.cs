namespace Lbk.Mobile.UI.Android.Controls.PullToRefresh
{
    using global::Android.Content.Res;

    public class Pixel
    {
        private readonly Resources _resources;
        private readonly int _value;

        public Pixel(int value, Resources resources)
        {
            this._value = value;
            this._resources = resources;
        }

        public float ToDp()
        {
            return this._value*this._resources.DisplayMetrics.Density + 0.5f;
        }
    }
}
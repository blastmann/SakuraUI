using System;
using Windows.System.Display;

namespace Yangwenyi.WindowsPhone.Listen.Frameworks
{
    class DisplayRequestHelper
    {
        private static readonly DisplayRequest DisplayRequest = new DisplayRequest();

        public static void RequestActive()
        {
            try
            {
                DisplayRequest.RequestActive();
            }
            catch (Exception)
            {

            }
        }

        public static void Release()
        {
            try
            {
                DisplayRequest.RequestRelease();
            }
            catch (Exception)
            {

            }
        }
    }
}

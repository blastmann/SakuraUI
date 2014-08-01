using System;
using Windows.System.Display;

namespace SakuraUI.Utilities
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

#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("bWOBzMAkV0/hkm2UnukTJ78xHnH2H0q2L4apxh+P8pJCucOGBkWE6PdWDZDxQcnJYqox/js02jkxkVwtN7S6tYU3tL+3N7S0tRTKVkJCS9kQ4J8x3/xiyPB4oIwKP+KRO8/nAugk5ATDyqQJ4OUy82j+2+kOg93VYCmhvfH2nJzXCd5CmK7jUHtoUYzkx2lWbgGBkC4XCh5TKErM9aDluPJM0bAwun+lripJI3URIzuv46ji7xsYJaBHC5/KpOpHwm0aJHN4BpmFN7SXhbizvJ8z/TNCuLS0tLC1tt429uKDoManlGDPjqfUleK3G2QI7GCzXgKyQtNQ5b+73xKQ9AdifVAbry+j19N44nXeCjlkXoqu1D24Rd0mfcrKvq6pfre2tLW0");
        private static int[] order = new int[] { 9,2,3,3,5,8,8,10,10,11,11,12,12,13,14 };
        private static int key = 181;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif

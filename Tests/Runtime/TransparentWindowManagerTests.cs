#if UNITY_EDITOR
using NUnit.Framework;
using RAStudio.TransparentOverlay;
using UnityEngine;

namespace RAStudio.TransparentOverlay.Tests
{
    public class TransparentWindowManagerTests
    {
        private TransparentWindowManager windowManager;

        [SetUp]
        public void Setup()
        {
            windowManager = TransparentWindowManager.Instance;
        }

        [TearDown]
        public void Teardown()
        {
            windowManager.Dispose();
        }

        [Test]
        public void Initialize_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => windowManager.Initialize());
        }

        [Test]
        public void SetUIMode_ChangesUIMode()
        {
            windowManager.SetUIMode(UIMode.Standard);
            Assert.DoesNotThrow(() => windowManager.UpdateClickThrough());

            windowManager.SetUIMode(UIMode.UIToolkit);
            Assert.DoesNotThrow(() => windowManager.UpdateClickThrough());
        }
    }
}
#endif
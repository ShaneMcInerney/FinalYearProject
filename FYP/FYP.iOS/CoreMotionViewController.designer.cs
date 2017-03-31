using System;
using System.Collections.Generic;
using System.Text;
using Foundation;

namespace FYP.iOS
{
        [Register("CoreMotionViewController")]
        partial class CoreMotionViewController
        {
            [Outlet]
            UIKit.UILabel lblX { get; set; }

            [Outlet]
            UIKit.UILabel lblY { get; set; }

            [Outlet]
            UIKit.UILabel lblZ { get; set; }

            void ReleaseDesignerOutlets()
            {
                if (lblX != null)
                {
                    lblX.Dispose();
                    lblX = null;
                }

                if (lblY != null)
                {
                    lblY.Dispose();
                    lblY = null;
                }

                if (lblZ != null)
                {
                    lblZ.Dispose();
                    lblZ = null;
                }
            }
        }
    }


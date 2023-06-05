using VRCFaceTracking.Core.Params.Expressions;

namespace VRCFaceTracking.Babble;
public partial class BabbleOSC
{
    public TwoKeyDictionary<UnifiedExpressions, string, float> BabbleUniqueExpressionMap = new()
    {
        { UnifiedExpressions.CheekPuffLeft, "/cheekPuffLeft", 0f },
        { UnifiedExpressions.CheekPuffRight, "/cheekPuffRight", 0f },
        { UnifiedExpressions.CheekSuckLeft, "/cheekSuckLeft", 0f },
        { UnifiedExpressions.CheekSuckRight, "/cheekSuckRight", 0f },
        { UnifiedExpressions.JawOpen, "/jawOpen", 0f },
        { UnifiedExpressions.JawForward, "/jawForward", 0f },
        { UnifiedExpressions.JawLeft, "/jawLeft", 0f },
        { UnifiedExpressions.JawRight, "/jawRight", 0f },
        { UnifiedExpressions.NoseSneerLeft, "/noseSneerLeft", 0f },
        { UnifiedExpressions.NoseSneerRight, "/noseSneerRight", 0f },

        // "/mouthFunnel"
        { UnifiedExpressions.LipFunnelLowerLeft, "/lipFunnelLowerLeft", 0f },
        { UnifiedExpressions.LipFunnelLowerRight, "/lipFunnelLowerRight", 0f },
        { UnifiedExpressions.LipFunnelUpperLeft, "/lipFunnelUpperLeft", 0f },
        { UnifiedExpressions.LipFunnelUpperLeft, "/lipFunnelUpperLeft", 0f },

        // "/mouthPucker"
        { UnifiedExpressions.LipPuckerLowerLeft, "/lipPuckerLowerLeft", 0f },
        { UnifiedExpressions.LipPuckerLowerRight, "/lipPuckerLowerRight", 0f },
        { UnifiedExpressions.LipPuckerUpperLeft, "/lipPuckerUpperLeft", 0f },
        { UnifiedExpressions.LipPuckerUpperRight, "/lipPuckerUpperRight", 0f },

        { UnifiedExpressions.MouthClosed, "/mouthClose", 0f },
        { UnifiedExpressions.MouthCornerPullLeft, "/mouthSmileLeft", 0f },
        { UnifiedExpressions.MouthCornerPullRight, "/mouthSmileRight", 0f },
        { UnifiedExpressions.MouthFrownLeft, "/mouthFrownLeft", 0f },
        { UnifiedExpressions.MouthFrownRight, "/mouthFrownRight", 0f },
        { UnifiedExpressions.MouthDimpleLeft, "/mouthDimpleLeft", 0f },
        { UnifiedExpressions.MouthDimpleRight, "/mouthDimpleRight", 0f },
        { UnifiedExpressions.MouthUpperUpLeft, "/mouthUpperUpLeft", 0f },
        { UnifiedExpressions.MouthUpperUpRight, "/mouthUpperUpRight", 0f },
        { UnifiedExpressions.MouthLowerDownLeft, "/mouthLowerDownLeft", 0f },
        { UnifiedExpressions.MouthLowerDownRight, "/mouthLowerDownRight", 0f },
        { UnifiedExpressions.MouthPressLeft, "/mouthPressLeft", 0f },
        { UnifiedExpressions.MouthPressRight, "/mouthPressRight", 0f },
        { UnifiedExpressions.MouthStretchLeft, "/mouthStretchLeft", 0f },
        { UnifiedExpressions.MouthStretchRight, "/mouthStretchRight", 0f },
        { UnifiedExpressions.TongueOut, "/tongueOut", 0f },
        { UnifiedExpressions.TongueUp, "/tongueUp", 0f },
        { UnifiedExpressions.TongueDown, "/tongueDown", 0f },
        { UnifiedExpressions.TongueLeft, "/tongueLeft", 0f },
        { UnifiedExpressions.TongueRight, "/tongueRight", 0f },
        { UnifiedExpressions.TongueRoll, "/tongueRoll", 0f },
        { UnifiedExpressions.TongueBendDown, "/tongueBendDown", 0f },
        { UnifiedExpressions.TongueCurlUp, "/tongueCurlUp", 0f },
        { UnifiedExpressions.TongueSquish, "/tongueSquish", 0f },
        { UnifiedExpressions.TongueFlat, "/tongueFlat", 0f },
        { UnifiedExpressions.TongueTwistLeft, "/tongueTwistLeft", 0f },
        { UnifiedExpressions.TongueTwistRight, "/tongueTwistRight", 0f }
    };
}

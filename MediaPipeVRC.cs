using System.Reflection;
using VRCFaceTracking.Core.Params.Expressions;

namespace VRCFaceTracking.MediaPipe;
public class MediaPipeVRC : ExtTrackingModule
{
    private MediaPipeOSC babbleOSC;

    public override (bool SupportsEye, bool SupportsExpression) Supported => (true, true);

    public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
    {
        babbleOSC = new MediaPipeOSC();

        List<Stream> streams = new List<Stream>();
        Assembly a = Assembly.GetExecutingAssembly();
        //var hmdStream = a.GetManifestResourceStream
        //   ("VRCFaceTracking.MediaPipe.BabbleLogo.png");
        //streams.Add(hmdStream);
        ModuleInformation = new ModuleMetadata()
        {
            Name = "MediaPipe Eye and Face Tracking",
            //StaticImages = streams
        };

        return (true, true);
    }

    public override void Teardown() => babbleOSC.Teardown();
    public override void Update()
    {
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.TongueOut].Weight = babbleOSC.BabbleExpressionMap["/tongueOut"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawOpen].Weight = babbleOSC.BabbleExpressionMap["/jawOpen"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawLeft].Weight = babbleOSC.BabbleExpressionMap["/jawLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawRight].Weight = babbleOSC.BabbleExpressionMap["/jawRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawForward].Weight = babbleOSC.BabbleExpressionMap["/jawForward"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.NoseSneerLeft].Weight = babbleOSC.BabbleExpressionMap["/noseSneerLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.NoseSneerRight].Weight = babbleOSC.BabbleExpressionMap["/noseSneerRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthClosed].Weight = babbleOSC.BabbleExpressionMap["/mouthClose"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthPucker"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerRight].Weight = babbleOSC.BabbleExpressionMap["/mouthPucker"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthPucker"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperRight].Weight = babbleOSC.BabbleExpressionMap["/mouthPucker"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelLowerLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthFunnel"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelLowerRight].Weight = babbleOSC.BabbleExpressionMap["/mouthFunnel"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelUpperLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthFunnel"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelUpperRight].Weight = babbleOSC.BabbleExpressionMap["/mouthFunnel"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthUpperUpLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthUpperUpLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthUpperUpRight].Weight = babbleOSC.BabbleExpressionMap["/mouthUpperUpRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthLowerDownLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthLowerDownLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthLowerDownRight].Weight = babbleOSC.BabbleExpressionMap["/mouthLowerDownRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthPressLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthPressLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthPressRight].Weight = babbleOSC.BabbleExpressionMap["/mouthPressRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthStretchLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthStretchLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthStretchRight].Weight = babbleOSC.BabbleExpressionMap["/mouthStretchRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthDimpleLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthDimpleLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthDimpleRight].Weight = babbleOSC.BabbleExpressionMap["/mouthDimpleRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerPullLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthSmileLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerPullRight].Weight = babbleOSC.BabbleExpressionMap["/mouthSmileRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthFrownLeft].Weight = babbleOSC.BabbleExpressionMap["/mouthFrownLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthFrownRight].Weight = babbleOSC.BabbleExpressionMap["/mouthFrownRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekPuffLeft].Weight = babbleOSC.BabbleExpressionMap["/cheekPuff"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekPuffRight].Weight = babbleOSC.BabbleExpressionMap["/cheekPuff"];

        //UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.Mouth].Weight = babbleOSC.BabbleExpressionMap["/mouthRollLower"];
        //UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.Mouth].Weight = babbleOSC.BabbleExpressionMap["/mouthRollUpper"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowInnerUpLeft].Weight = babbleOSC.BabbleExpressionMap["/browInnerUp"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowInnerUpRight].Weight = babbleOSC.BabbleExpressionMap["/browInnerUp"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowLowererLeft].Weight = babbleOSC.BabbleExpressionMap["/browDownLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowLowererRight].Weight = babbleOSC.BabbleExpressionMap["/browDownRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowOuterUpLeft].Weight = babbleOSC.BabbleExpressionMap["/browOuterUpLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowOuterUpRight].Weight = babbleOSC.BabbleExpressionMap["/browOuterUpRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeSquintLeft].Weight = babbleOSC.BabbleExpressionMap["/eyeSquintLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeSquintRight].Weight = babbleOSC.BabbleExpressionMap["/eyeSquintRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeWideLeft].Weight = -babbleOSC.BabbleExpressionMap["/eyeWideLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeWideRight].Weight = babbleOSC.BabbleExpressionMap["/eyeWideRight"];
        
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekSquintLeft].Weight = babbleOSC.BabbleExpressionMap["/cheekSquintLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekSquintRight].Weight = babbleOSC.BabbleExpressionMap["/cheekSquintRight"];
        
        UnifiedTracking.Data.Eye.Left.Openness = 1 - babbleOSC.BabbleExpressionMap["/eyeBlinkLeft"];
        UnifiedTracking.Data.Eye.Right.Openness = 1 - babbleOSC.BabbleExpressionMap["/eyeBlinkRight"];
        UnifiedTracking.Data.Eye.Left.Gaze.x = -babbleOSC.BabbleExpressionMap["/eyeLookOutLeft"] + babbleOSC.BabbleExpressionMap["/eyeLookInLeft"];
        UnifiedTracking.Data.Eye.Left.Gaze.y = -babbleOSC.BabbleExpressionMap["/eyeLookDownLeft"] + babbleOSC.BabbleExpressionMap["/eyeLookUpLeft"];
        UnifiedTracking.Data.Eye.Right.Gaze.x = -babbleOSC.BabbleExpressionMap["/eyeLookInRight"] + babbleOSC.BabbleExpressionMap["/eyeLookOutRight"];
        UnifiedTracking.Data.Eye.Right.Gaze.y = -babbleOSC.BabbleExpressionMap["/eyeLookDownRight"] + babbleOSC.BabbleExpressionMap["/eyeLookUpRight"];
    }
}

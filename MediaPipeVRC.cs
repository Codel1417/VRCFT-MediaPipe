using System.Reflection;
using VRCFaceTracking.Core.Params.Expressions;

namespace VRCFaceTracking.MediaPipe;
public class MediaPipeVRC : ExtTrackingModule
{
    private MediaPipeOSC mediaPipeOsc;

    public override (bool SupportsEye, bool SupportsExpression) Supported => (true, true);

    public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
    {
        mediaPipeOsc = new MediaPipeOSC();

        List<Stream> streams = new List<Stream>();
        Assembly a = Assembly.GetExecutingAssembly();
        var hmdStream = a.GetManifestResourceStream
           ("VRCFaceTracking.MediaPipe.google_mediapipe_logo_notMine.png");
        streams.Add(hmdStream);
        ModuleInformation = new ModuleMetadata()
        {
            Name = "MediaPipe Eye and Face Tracking",
            StaticImages = streams
        };

        return (true, true);
    }

    public override void Teardown() => mediaPipeOsc.Teardown();
    public override void Update()
    {
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.TongueOut].Weight = mediaPipeOsc.BabbleExpressionMap["/tongueOut"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawOpen].Weight = mediaPipeOsc.BabbleExpressionMap["/jawOpen"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/jawLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawRight].Weight = mediaPipeOsc.BabbleExpressionMap["/jawRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawForward].Weight = mediaPipeOsc.BabbleExpressionMap["/jawForward"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.NoseSneerLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/noseSneerLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.NoseSneerRight].Weight = mediaPipeOsc.BabbleExpressionMap["/noseSneerRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthClosed].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthClose"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthPucker"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthPucker"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthPucker"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthPucker"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelLowerLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthFunnel"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelLowerRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthFunnel"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelUpperLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthFunnel"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelUpperRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthFunnel"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthUpperUpLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthUpperUpLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthUpperUpRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthUpperUpRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthLowerDownLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthLowerDownLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthLowerDownRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthLowerDownRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthPressLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthPressLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthPressRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthPressRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthStretchLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthStretchLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthStretchRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthStretchRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthDimpleLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthDimpleLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthDimpleRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthDimpleRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerPullLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthSmileLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerPullRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthSmileRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthFrownLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthFrownLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthFrownRight].Weight = mediaPipeOsc.BabbleExpressionMap["/mouthFrownRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekPuffLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/cheekPuff"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekPuffRight].Weight = mediaPipeOsc.BabbleExpressionMap["/cheekPuff"];

        //UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.Mouth].Weight = babbleOSC.BabbleExpressionMap["/mouthRollLower"];
        //UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.Mouth].Weight = babbleOSC.BabbleExpressionMap["/mouthRollUpper"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowInnerUpLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/browInnerUp"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowInnerUpRight].Weight = mediaPipeOsc.BabbleExpressionMap["/browInnerUp"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowLowererLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/browDownLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowLowererRight].Weight = mediaPipeOsc.BabbleExpressionMap["/browDownRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowOuterUpLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/browOuterUpLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.BrowOuterUpRight].Weight = mediaPipeOsc.BabbleExpressionMap["/browOuterUpRight"];

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeSquintLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/eyeSquintLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeSquintRight].Weight = mediaPipeOsc.BabbleExpressionMap["/eyeSquintRight"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeWideLeft].Weight = -mediaPipeOsc.BabbleExpressionMap["/eyeWideLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.EyeWideRight].Weight = mediaPipeOsc.BabbleExpressionMap["/eyeWideRight"];
        
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekSquintLeft].Weight = mediaPipeOsc.BabbleExpressionMap["/cheekSquintLeft"];
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekSquintRight].Weight = mediaPipeOsc.BabbleExpressionMap["/cheekSquintRight"];
        
        UnifiedTracking.Data.Eye.Left.Openness = 1 - mediaPipeOsc.BabbleExpressionMap["/eyeBlinkLeft"];
        UnifiedTracking.Data.Eye.Right.Openness = 1 - mediaPipeOsc.BabbleExpressionMap["/eyeBlinkRight"];
        UnifiedTracking.Data.Eye.Left.Gaze.x = -mediaPipeOsc.BabbleExpressionMap["/eyeLookOutLeft"] + mediaPipeOsc.BabbleExpressionMap["/eyeLookInLeft"];
        UnifiedTracking.Data.Eye.Left.Gaze.y = -mediaPipeOsc.BabbleExpressionMap["/eyeLookDownLeft"] + mediaPipeOsc.BabbleExpressionMap["/eyeLookUpLeft"];
        UnifiedTracking.Data.Eye.Right.Gaze.x = -mediaPipeOsc.BabbleExpressionMap["/eyeLookInRight"] + mediaPipeOsc.BabbleExpressionMap["/eyeLookOutRight"];
        UnifiedTracking.Data.Eye.Right.Gaze.y = -mediaPipeOsc.BabbleExpressionMap["/eyeLookDownRight"] + mediaPipeOsc.BabbleExpressionMap["/eyeLookUpRight"];
    }
}

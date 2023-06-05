using System.Reflection;
using VRCFaceTracking.Core.Params.Expressions;

namespace VRCFaceTracking.Babble;
public class BabbleVRC : ExtTrackingModule
{
    private BabbleOSC babble;
    private UnifiedExpressions[] unifiedExpressions;

    public override (bool SupportsEye, bool SupportsExpression) Supported => (false, true);

    public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
    {
        babble = new BabbleOSC(Logger);

        // Don't just pull the enum - we only support a subset of distinct UnifiedExpressions
        unifiedExpressions = babble.BabbleUniqueExpressionMap.OuterKeys.ToArray();

        List<Stream> streams = new List<Stream>();
        Assembly a = Assembly.GetExecutingAssembly();
        var hmdStream = a.GetManifestResourceStream
            ("VRCFaceTracking.Babble.BabbleLogo.png");
        streams.Add(hmdStream);
        ModuleInformation = new ModuleMetadata()
        {
            Name = "Project Babble Face Tracking\nInference Model v2.1.0",
            StaticImages = streams 
        };

        return (false, true);
    }

    public override void Teardown() => babble.Teardown();
    public override void Update()
    {
        foreach (var expression in unifiedExpressions)
        {
            UnifiedTracking.Data.Shapes[(int)expression].Weight = babble.BabbleUniqueExpressionMap.GetByKey1(expression);
        }

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelLowerLeft].Weight = babble.MouthFunnel;
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelLowerRight].Weight = babble.MouthFunnel;
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelUpperLeft].Weight = babble.MouthFunnel;
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipFunnelUpperLeft].Weight = babble.MouthFunnel;

        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerLeft].Weight = babble.MouthFunnel;
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerRight].Weight = babble.MouthFunnel;
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperLeft].Weight = babble.MouthFunnel;
        UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperRight].Weight = babble.MouthFunnel;

        Thread.Sleep(10);
    }
}
